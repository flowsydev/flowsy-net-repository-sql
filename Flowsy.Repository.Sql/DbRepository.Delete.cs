using System.Data;
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
        => ExecuteAsync(
            ResolveRoutineName($"{EntityName}{Configuration.DeleteById.Name}"),
            new Dictionary<string, object?>
            {
                [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id))
            },
            CommandType.StoredProcedure,
            cancellationToken
        );

    /// <summary>
    /// Deletes one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to delete entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected entities.</returns>
    public override Task<int> DeleteManyAsync(dynamic filter, CancellationToken cancellationToken) 
        => DeleteManyAsync(((object) filter).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Deletes one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object that will be used as a filter to delete entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The number of affected entities.</returns>
    public override Task<int> DeleteManyAsync(
        IReadOnlyDictionary<string, object?> filter,
        CancellationToken cancellationToken
    )
    {
        var action = Configuration.DeleteMany;
        return ExecuteAsync(
            ResolveRoutineName($"{EntityName}{action.Name}"),
            ToDynamicParameters(filter.ExceptBy(action.ExcludedProperties)),
            CommandType.StoredProcedure, 
            cancellationToken
        );
    }
}