using System.Data;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Provides the implementation of a repository which data store is a SQL database and which entities can be translated to a given culture.
/// </summary>
/// <typeparam name="TEntity">The type of the main entity.</typeparam>
/// <typeparam name="TEntityTranslated">The type of the translated version of the entity.</typeparam>
/// <typeparam name="TIdentity">The type of the underlying unique identifier.</typeparam>
public abstract partial class DbRepositoryTranslated<TEntity, TEntityTranslated, TIdentity> :
    DbRepository<TEntity, TIdentity>,
    IRepositoryTranslated<TEntity, TEntityTranslated, TIdentity>
    where TEntity : class, IEntity
    where TEntityTranslated : class, TEntity, IEntityTranslated
{
    protected DbRepositoryTranslated(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    protected DbRepositoryTranslated(IDbTransaction transaction) : base(transaction)
    {
    }
}