using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    /// <summary>
    /// Deletes the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>The number of affected entities.</returns>
    public override Task<int> DeleteByIdAsync(TIdentity id, CancellationToken cancellationToken)
    {
        var action = Configuration.Actions.DeleteById;
        var param = ToDynamicParameters(new Dictionary<string, object?>
        {
            [IdentityPropertyName] = id
        });
        return ExecuteAsync(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(), 
            cancellationToken
            );
    }

    /// <summary>
    /// Deletes one or more entities matching the specified filter.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to delete entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected entities.</returns>
    public override Task<int> DeleteManyAsync(dynamic criteria, CancellationToken cancellationToken) 
        => DeleteManyAsync(((object) criteria).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Deletes one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object that will be used as criteria to delete entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected entities.</returns>
    public override Task<int> DeleteManyAsync(
        IReadOnlyDictionary<string, object?> criteria,
        CancellationToken cancellationToken
    )
    {
        var action = Configuration.Actions.DeleteMany;
        var param = ToDynamicParameters(criteria.ExceptBy(action.ExcludedProperties));
        return ExecuteAsync(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(), 
            cancellationToken
            );
    }
}