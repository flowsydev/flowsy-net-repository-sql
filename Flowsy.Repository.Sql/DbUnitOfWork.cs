using System.Data;
using System.Data.Common;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Implements a unit of work wrapping a database transaction.
/// </summary>
public class DbUnitOfWork : IUnitOfWork
{
    protected DbUnitOfWork(IDbConnection connection)
    {
        Connection = connection;
        Connection.Open();
        Transaction = Connection.BeginTransaction();
    }

    ~DbUnitOfWork()
    {
        Dispose(false);
    }

    /// <summary>
    /// The database connection.
    /// </summary>
    protected IDbConnection Connection { get; }
    
    /// <summary>
    /// The transaction in course.
    /// </summary>
    protected IDbTransaction Transaction { get; }

    private bool _disposed;

    /// <summary>
    /// Persists all the changes made during the unit of work by committing the transaction to the underlying database.
    /// </summary>
    public void Commit()
    {
        try
        {
            Transaction.Commit();
        }
        catch
        {
            Transaction.Rollback();
            throw;
        }
    }

    
    /// <summary>
    /// Asynchronously persists all the changes made during the unit of work by committing the transaction to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (Transaction is DbTransaction dbTransaction)
                await dbTransaction.CommitAsync(cancellationToken);
            else
                Transaction.Commit();
        }
        catch
        {
            if (Transaction is DbTransaction dbTransaction)
                await dbTransaction.RollbackAsync(cancellationToken);
            else
                Transaction.Rollback();

            throw;
        }
    }

    /// <summary>
    /// Triggers the disposal of the underlying connection which rolls back the uncommitted transaction. 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Asynchronously triggers the disposal of the underlying connection which rolls back the uncommitted transaction. 
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the underlying connection which rolls back the uncommitted transaction.
    /// </summary>
    /// <param name="disposing">Indicates whether the object is being disposed.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            Transaction.Dispose();
            Connection.Dispose();
        }

        _disposed = true;
    }
    
    /// <summary>
    /// Asynchronously disposes the underlying connection which rolls back the uncommitted transaction.
    /// </summary>
    /// <param name="disposing">Indicates whether the object is being disposed.</param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            if (Transaction is DbTransaction dbTransaction)
                await dbTransaction.DisposeAsync();
            else 
                Transaction.Dispose();

            if (Connection is DbConnection dbConnection)
                await dbConnection.DisposeAsync();
            else
                Connection.Dispose();
        }

        _disposed = true;
    }
}