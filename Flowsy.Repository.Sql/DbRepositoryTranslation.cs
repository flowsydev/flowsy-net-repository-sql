using System.Data;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Provides the implementation of a repository which data store is a SQL database and which entities can be translated to a given culture.
/// </summary>
/// <typeparam name="TEntity">The type of the main entity.</typeparam>
/// <typeparam name="TEntityTranslation">The type of the entity translation.</typeparam>
/// <typeparam name="TIdentity">The type of the underlying unique identifier.</typeparam>
public abstract partial class DbRepositoryTranslation<TEntity, TEntityTranslation, TIdentity> :
    DbRepository<TEntity, TIdentity>,
    IRepositoryTranslation<TEntity, TEntityTranslation, TIdentity>
    where TEntity : class, IEntity
    where TEntityTranslation : class, TEntity, IEntityTranslation
{
    protected DbRepositoryTranslation(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    protected DbRepositoryTranslation(IDbTransaction transaction) : base(transaction)
    {
    }
}