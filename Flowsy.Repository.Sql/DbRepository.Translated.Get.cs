using System.Data;
using Flowsy.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TEntityTranslated, TIdentity> 
{
    /// <summary>
    /// Gets the translated version of the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public Task<T?> GetByIdAsync<T>(
        TIdentity id,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetByIdTranslated;
        var param = ToDynamicParameters(new Dictionary<string, object?>
        {
            [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id)),
            ["CultureId"] = cultureId
        }); 
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }
    
    /// <summary>
    /// Gets the translated version of the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public Task<TEntityTranslated?> GetByIdAsync(
        TIdentity id,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetByIdAsync<TEntityTranslated>(id, cultureId, cancellationToken);

    /// <summary>
    /// Gets the extended and translated version of the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public Task<T?> GetByIdExtendedAsync<T>(
        TIdentity id,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetByIdExtendedTranslated;
        var param = ToDynamicParameters(new Dictionary<string, object?>
        {
            [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id)),
            ["CultureId"] = cultureId
        }); 
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }

    /// <summary>
    /// Gets the extended and translated version of the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public Task<TEntityTranslated?> GetByIdExtendedAsync(
        TIdentity id,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetByIdExtendedAsync<TEntityTranslated>(id, cultureId, cancellationToken);
    
    /// <summary>
    /// Gets the translated version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided criteria.</returns>
    public Task<T?> GetOneAsync<T>(
        dynamic criteria, 
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneAsync<T>(((object) criteria).ToReadonlyDictionary(), cultureId, cancellationToken);

    /// <summary>
    /// Gets the translated version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided criteria.</returns>
    public Task<T?> GetOneAsync<T>(
        IReadOnlyDictionary<string, object?> criteria,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetOneTranslated;
        var param = ToDynamicParameters(new Dictionary<string, object?>(criteria)
        {
            ["CultureId"] = cultureId
        });
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }

    /// <summary>
    /// Gets the translated version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided criteria.</returns>
    public Task<TEntityTranslated?> GetOneAsync(
        dynamic criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetOneAsync(((object) criteria).ToReadonlyDictionary(), cultureId, cancellationToken);

    /// <summary>
    /// Gets the translated version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided criteria.</returns>
    public Task<TEntityTranslated?> GetOneAsync(
        IReadOnlyDictionary<string, object?> criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetOneAsync<TEntityTranslated>(criteria, cultureId, cancellationToken);
    
    /// <summary>
    /// Gets an extended and translated version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided criteria.</returns>
    public Task<T?> GetOneExtendedAsync<T>(
        dynamic criteria,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneExtendedAsync<T>(((object) criteria).ToReadonlyDictionary(), cultureId, cancellationToken);

    /// <summary>
    /// Gets the extended version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided criteria.</returns>
    public Task<T?> GetOneExtendedAsync<T>(
        IReadOnlyDictionary<string, object?> criteria,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetOneExtendedTranslated;
        var param = ToDynamicParameters(new Dictionary<string, object?>(criteria)
        {
            ["CultureId"] = cultureId
        }); 
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }

    /// <summary>
    /// Gets the extended and translated version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided criteria.</returns>
    public Task<TEntityTranslated?> GetOneExtendedAsync(
        dynamic criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetOneExtendedAsync(((object) criteria).ToReadonlyDictionary(), cultureId, cancellationToken);
    
    /// <summary>
    /// Gets the extended and translated version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided criteria.</returns>
    public Task<TEntityTranslated?> GetOneExtendedAsync(
        IReadOnlyDictionary<string, object?> criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetOneExtendedAsync<TEntityTranslated>(criteria, cultureId, cancellationToken);

    /// <summary>
    /// Gets the translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided criteria.</returns>
    public Task<IEnumerable<T>> GetManyAsync<T>(
        dynamic criteria,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => GetManyAsync<T>(
            ((object) criteria).ToReadonlyDictionary(),
            cultureId,
            cancellationToken
        );

    /// <summary>
    /// Gets the translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided criteria.</returns>
    public Task<IEnumerable<T>> GetManyAsync<T>(
        IReadOnlyDictionary<string, object?> criteria,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetManyTranslated;
        var param = ToDynamicParameters(new Dictionary<string, object?>(criteria)
        {
            ["CultureId"] = cultureId
        });
        return QueryAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }
    
    /// <summary>
    /// Gets the translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public Task<IEnumerable<TEntityTranslated>> GetManyAsync(
        dynamic criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetManyAsync<TEntityTranslated>(
            ((object) criteria).ToReadonlyDictionary(),
            cultureId,
            cancellationToken
        );

    /// <summary>
    /// Gets the translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public Task<IEnumerable<TEntityTranslated>> GetManyAsync(
        IReadOnlyDictionary<string, object?> criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetManyAsync<TEntityTranslated>(criteria, cultureId, cancellationToken);

    /// <summary>
    /// Gets the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public Task<IEnumerable<T>> GetManyExtendedAsync<T>(
        dynamic criteria,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => GetManyExtendedAsync<T>(
            ((object) criteria).ToReadonlyDictionary(),
            cultureId,
            cancellationToken
            );

    /// <summary>
    /// Gets the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public Task<IEnumerable<T>> GetManyExtendedAsync<T>(
        IReadOnlyDictionary<string, object?> criteria,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetManyExtendedTranslated;
        var param = ToDynamicParameters(new Dictionary<string, object?>(criteria)
        {
            ["CultureId"] = cultureId
        });
        return QueryAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }
    
    /// <summary>
    /// Gets the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public Task<IEnumerable<TEntityTranslated>> GetManyExtendedAsync(
        dynamic criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetManyExtendedAsync<TEntityTranslated>(
            ((object) criteria).ToReadonlyDictionary(),
            cultureId,
            cancellationToken
            );
    
    /// <summary>
    /// Gets the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public Task<IEnumerable<TEntityTranslated>> GetManyExtendedAsync(
        IReadOnlyDictionary<string, object?> criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetManyExtendedAsync<TEntityTranslated>(
            criteria,
            cultureId,
            cancellationToken
            );
}