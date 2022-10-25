using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    /// <summary>
    /// Updates only one property of the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <param name="propertyName">The name of the property to be updated.</param>
    /// <param name="value">The new value for the property to be updated.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected entities.</returns>
    public override Task<int> PatchAsync(TIdentity id, string propertyName, object value, CancellationToken cancellationToken)
        => ExecuteAsync(
            ResolveRoutineName($"{EntityName}{Configuration.Patch.Name}{propertyName}"),
            new Dictionary<string, object?>
            {
                [IdentityPropertyName] = id,
                [propertyName] = value
            },
            CommandType.StoredProcedure,
            cancellationToken
        );

    /// <summary>
    /// Updates only certain properties of an entity.
    /// </summary>
    /// <param name="entity">An object with properties used to identify and update an entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected entities.</returns>
    public override Task<int> PatchAsync(dynamic entity, CancellationToken cancellationToken) 
        => PatchAsync(((object) entity).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Updates only certain properties of an entity.
    /// </summary>
    /// <param name="properties">The property names and values of an object used to identify and update an entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected entities.</returns>
    public override Task<int> PatchAsync(
        IReadOnlyDictionary<string, object?> properties,
        CancellationToken cancellationToken
        )
    {
        var action = Configuration.Patch;
        
        return ExecuteAsync(
            ResolveRoutineName($"{EntityName}{Configuration.Patch.Name}"),
            ToDynamicParameters(properties.ExceptBy(action.ExcludedProperties)),
            CommandType.StoredProcedure,
            cancellationToken
        );
    }
}