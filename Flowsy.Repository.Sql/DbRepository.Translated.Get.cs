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
        => QueryFirstOrDefaultAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.GetByIdTranslated.Name}"),
            new Dictionary<string, object?>
            {
                [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id)),
                ["CultureId"] = cultureId
            },
            CommandType.StoredProcedure,
            cancellationToken
        );
    
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
        => QueryFirstOrDefaultAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.GetByIdExtendedTranslated.Name}"),
            new Dictionary<string, object?>
            {
                [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id)),
                ["CultureId"] = cultureId
            },
            CommandType.StoredProcedure,
            cancellationToken
            );

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
    /// Gets the translated version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided filter.</returns>
    public Task<T?> GetOneAsync<T>(
        dynamic filter, 
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneAsync<T>(((object) filter).ToReadonlyDictionary(), cultureId, cancellationToken);
    
    /// <summary>
    /// Gets the translated version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided filter.</returns>
    public Task<T?> GetOneAsync<T>(
        IReadOnlyDictionary<string, object?> filter,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => QueryFirstOrDefaultAsync<T>(
            $"{EntityName}{Configuration.GetOneTranslated}",
            ToDynamicParameters(filter),
            CommandType.StoredProcedure,
            cancellationToken
            );

    /// <summary>
    /// Gets the translated version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided filter.</returns>
    public Task<TEntityTranslated?> GetOneAsync(
        dynamic filter,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetOneAsync(((object) filter).ToReadonlyDictionary(), cultureId, cancellationToken);

    /// <summary>
    /// Gets the translated version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided filter.</returns>
    public Task<TEntityTranslated?> GetOneAsync(
        IReadOnlyDictionary<string, object?> filter,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetOneAsync<TEntityTranslated>(filter, cultureId, cancellationToken);
    
    /// <summary>
    /// Gets an extended and translated version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided filter.</returns>
    public Task<T?> GetOneExtendedAsync<T>(
        dynamic filter,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneExtendedAsync<T>(((object) filter).ToReadonlyDictionary(), cultureId, cancellationToken);

    /// <summary>
    /// Gets the extended version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided filter.</returns>
    public Task<T?> GetOneExtendedAsync<T>(
        IReadOnlyDictionary<string, object?> filter,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => QueryFirstOrDefaultAsync<T>(
            $"{EntityName}{Configuration.GetOneExtendedTranslated}",
            ToDynamicParameters(filter),
            CommandType.StoredProcedure,
            cancellationToken
            );

    /// <summary>
    /// Gets the extended and translated version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided filter.</returns>
    public Task<TEntityTranslated?> GetOneExtendedAsync(
        dynamic filter,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetOneExtendedAsync(((object) filter).ToReadonlyDictionary(), cultureId, cancellationToken);
    
    /// <summary>
    /// Gets the extended and translated version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided filter.</returns>
    public Task<TEntityTranslated?> GetOneExtendedAsync(
        IReadOnlyDictionary<string, object?> filter,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetOneExtendedAsync<TEntityTranslated>(filter, cultureId, cancellationToken);

    /// <summary>
    /// Gets the translated version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided filter.</returns>
    public Task<IEnumerable<T>> GetManyAsync<T>(
        dynamic filter,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => GetManyAsync<T>(
            ((object) filter).ToReadonlyDictionary(),
            cultureId,
            cancellationToken
        );

    /// <summary>
    /// Gets the translated version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided filter.</returns>
    public Task<IEnumerable<T>> GetManyAsync<T>(
        IReadOnlyDictionary<string, object?> filter,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => QueryAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.GetManyTranslated.Name}"),
            new Dictionary<string, object?>(filter)
            {
                ["CultureId"] = cultureId
            },
            CommandType.StoredProcedure,
            cancellationToken
            );
    
    /// <summary>
    /// Gets the translated version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public Task<IEnumerable<TEntityTranslated>> GetManyAsync(
        dynamic filter,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetManyAsync<TEntityTranslated>(
            ((object) filter).ToReadonlyDictionary(),
            cultureId,
            cancellationToken
        );

    /// <summary>
    /// Gets the translated version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public Task<IEnumerable<TEntityTranslated>> GetManyAsync(
        IReadOnlyDictionary<string, object?> filter,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetManyAsync<TEntityTranslated>(filter, cultureId, cancellationToken);

    /// <summary>
    /// Gets the extended and translated version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public Task<IEnumerable<T>> GetManyExtendedAsync<T>(
        dynamic filter,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => GetManyExtendedAsync<T>(
            ((object) filter).ToReadonlyDictionary(),
            cultureId,
            cancellationToken
            );
    
    /// <summary>
    /// Gets the extended and translated version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public Task<IEnumerable<T>> GetManyExtendedAsync<T>(
        IReadOnlyDictionary<string, object?> filter,
        string? cultureId,
        CancellationToken cancellationToken
        ) where T : class
        => QueryAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.GetManyExtendedTranslated.Name}"),
            new Dictionary<string, object?>(filter)
            {
                ["CultureId"] = cultureId
            },
            CommandType.StoredProcedure,
            cancellationToken
            );
    
    /// <summary>
    /// Gets the extended and translated version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public Task<IEnumerable<TEntityTranslated>> GetManyExtendedAsync(
        dynamic filter,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetManyExtendedAsync<TEntityTranslated>(
            ((object) filter).ToReadonlyDictionary(),
            cultureId,
            cancellationToken
            );
    
    /// <summary>
    /// Gets the extended and translated version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public Task<IEnumerable<TEntityTranslated>> GetManyExtendedAsync(
        IReadOnlyDictionary<string, object?> filter,
        string? cultureId,
        CancellationToken cancellationToken
        )
        => GetManyExtendedAsync<TEntityTranslated>(
            filter,
            cultureId,
            cancellationToken
            );
}