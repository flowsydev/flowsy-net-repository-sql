using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    /// <summary>
    /// Updates an entitiy in the underlying data store.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected records.</returns>
    public override Task<int> UpdateAsync<T>(T entity, CancellationToken cancellationToken) where T : class
        => UpdateAsync(entity.ToReadonlyDictionary(), cancellationToken);
    
    /// <summary>
    /// Updates an entitiy in the underlying data store.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected records.</returns>
    public override Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        => UpdateAsync<TEntity>(entity, cancellationToken);

    /// <summary>
    /// Updates an entitiy in the underlying data store.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected records.</returns>
    public override Task<int> UpdateAsync(dynamic entity, CancellationToken cancellationToken)
        => UpdateAsync(((object) entity).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Updates an entitiy in the underlying data store.
    /// </summary>
    /// <param name="properties">The property names and values of the entity to be updated.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected records.</returns>
    public override Task<int> UpdateAsync(
        IReadOnlyDictionary<string, object?> properties,
        CancellationToken cancellationToken
        )
    {
        var action = Configuration.Actions.Update;
        var param = ToDynamicParameters(properties.ExceptBy(action.ExcludedProperties));
        return ExecuteAsync(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
        );
    }
}