using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    /// <summary>
    /// Gets the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public override Task<T?> GetByIdAsync<T>(TIdentity id, CancellationToken cancellationToken) where T : class 
        => QueryFirstOrDefaultAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.SelectById.Name}"),
            new Dictionary<string, object?>
            {
                [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id))
            },
            CommandType.StoredProcedure,
            cancellationToken
            );
    
    /// <summary>
    /// Gets the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public override Task<TEntity?> GetByIdAsync(TIdentity id, CancellationToken cancellationToken)
        => GetByIdAsync<TEntity>(id, cancellationToken);
    
    /// <summary>
    /// Gets an extended version of the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public override Task<T?> GetByIdExtendedAsync<T>(TIdentity id, CancellationToken cancellationToken) where T : class 
        => QueryFirstOrDefaultAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.SelectByIdExtended.Name}"),
            new Dictionary<string, object?>
            {
                [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id))
            },
            CommandType.StoredProcedure,
            cancellationToken
            );

    /// <summary>
    /// Gets an extended version of the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public override Task<TEntity?> GetByIdExtendedAsync(TIdentity id, CancellationToken cancellationToken)
        => GetByIdExtendedAsync<TEntity>(id, cancellationToken);
    
    /// <summary>
    /// Gets an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided filter.</returns>
    public override Task<T?> GetOneAsync<T>(
        dynamic filter,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneAsync<T>(((object) filter).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided filter.</returns>
    public override Task<T?> GetOneAsync<T>(
        IReadOnlyDictionary<string, object?> filter,
        CancellationToken cancellationToken
        ) where T : class
        => QueryFirstOrDefaultAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.SelectOne.Name}"),
            ToDynamicParameters(filter),
            CommandType.StoredProcedure,
            cancellationToken
        );

    /// <summary>
    /// Gets an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided filter.</returns>
    public override Task<TEntity?> GetOneAsync(dynamic filter, CancellationToken cancellationToken)
        => GetOneAsync(((object) filter).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided filter.</returns>
    public override Task<TEntity?> GetOneAsync(
        IReadOnlyDictionary<string, object?> filter,
        CancellationToken cancellationToken
        )
        => GetOneAsync<TEntity>(filter, cancellationToken);
    
    /// <summary>
    /// Gets the extended version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided filter.</returns>
    public override Task<T?> GetOneExtendedAsync<T>(
        dynamic filter,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneExtendedAsync<T>(((object) filter).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets the extended version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided filter.</returns>
    public override Task<T?> GetOneExtendedAsync<T>(
        IReadOnlyDictionary<string, object?> filter,
        CancellationToken cancellationToken
        ) where T : class
        => QueryFirstOrDefaultAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.SelectOneExtended.Name}"),
            ToDynamicParameters(filter),
            CommandType.StoredProcedure,
            cancellationToken
            );

    /// <summary>
    /// Gets the extended version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided filter.</returns>
    public override Task<TEntity?> GetOneExtendedAsync(dynamic filter, CancellationToken cancellationToken)
        => GetOneExtendedAsync(((object) filter).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets the extended version of an entity matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided filter.</returns>
    public override Task<TEntity?> GetOneExtendedAsync(
        IReadOnlyDictionary<string, object?> filter,
        CancellationToken cancellationToken
        )
        => GetOneExtendedAsync<TEntity>(filter, cancellationToken);
    
    /// <summary>
    /// Gets one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided filter.</returns>
    public override Task<IEnumerable<T>> GetManyAsync<T>(dynamic filter, CancellationToken cancellationToken) where T : class 
        => GetManyAsync<T>(((object) filter).ToReadonlyDictionary(), cancellationToken);
    
    /// <summary>
    /// Gets one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided filter.</returns>
    public override Task<IEnumerable<T>> GetManyAsync<T>(IReadOnlyDictionary<string, object?> filter, CancellationToken cancellationToken) where T : class
        => QueryAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.SelectMany.Name}"),
            filter,
            CommandType.StoredProcedure,
            cancellationToken
            );

    /// <summary>
    /// Gets one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public override Task<IEnumerable<TEntity>> GetManyAsync(dynamic filter, CancellationToken cancellationToken) 
        => GetManyAsync<TEntity>(((object) filter).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public override Task<IEnumerable<TEntity>> GetManyAsync(IReadOnlyDictionary<string, object?> filter, CancellationToken cancellationToken)
        => GetManyAsync<TEntity>(filter, cancellationToken);
    
    /// <summary>
    /// Gets the extended version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided filter.</returns>
    public override Task<IEnumerable<T>> GetManyExtendedAsync<T>(dynamic filter, CancellationToken cancellationToken) where T : class 
        => GetManyExtendedAsync<T>(((object) filter).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets the extended version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided filter.</returns>
    public override Task<IEnumerable<T>> GetManyExtendedAsync<T>(IReadOnlyDictionary<string, object?> filter, CancellationToken cancellationToken) where T : class
        => QueryAsync<T>(
            ResolveRoutineName($"{EntityName}{Configuration.SelectManyExtended.Name}"),
            filter,
            CommandType.StoredProcedure,
            cancellationToken
            );

    /// <summary>
    /// Gets the extended version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">An object with properties to be used as a filter to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public override Task<IEnumerable<TEntity>> GetManyExtendedAsync(dynamic filter, CancellationToken cancellationToken) 
        => GetManyExtendedAsync<TEntity>(((object) filter).ToReadonlyDictionary(), cancellationToken);
    
    /// <summary>
    /// Gets the extended version of one or more entities matching the specified filter.
    /// </summary>
    /// <param name="filter">The property names and values of an object to be used as a filter to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided filter.</returns>
    public override Task<IEnumerable<TEntity>> GetManyExtendedAsync(IReadOnlyDictionary<string, object?> filter, CancellationToken cancellationToken)
        => GetManyExtendedAsync<TEntity>(filter, cancellationToken);
}