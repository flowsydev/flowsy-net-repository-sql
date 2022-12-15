using System.Data;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Provides the implementation of a repository which data store is a SQL database and which entities can be translated to a given culture.
/// </summary>
/// <typeparam name="TEntity">The type of the main entity.</typeparam>
/// <typeparam name="TEntityTranslated">The type of the translated entity.</typeparam>
/// <typeparam name="TIdentity">The type of the underlying unique identifier.</typeparam>
public abstract partial class DbRepository<TEntity, TEntityTranslated, TIdentity> :
    DbRepository<TEntity, TIdentity>,
    IRepository<TEntity, TEntityTranslated, TIdentity>
    where TEntity : class, IEntity
    where TEntityTranslated : class, TEntity, IEntityTranslation
{
    protected DbRepository(IDbConnectionFactory connectionFactory, IExceptionHandler? exceptionHandler = null) : base(connectionFactory, exceptionHandler)
    {
    }

    protected DbRepository(IDbTransaction transaction, IExceptionHandler? exceptionHandler = null) : base(transaction, exceptionHandler)
    {
    }
}