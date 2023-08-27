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
    {
        var action = Configuration.Actions.GetById;
        var param = ToDynamicParameters(new Dictionary<string, object?>
        {
            [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id))
        });
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }
    
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
    {
        var action = Configuration.Actions.GetByIdExtended;
        var param = ToDynamicParameters(new Dictionary<string, object?>
        {
            [IdentityPropertyName] = id ?? throw new ArgumentNullException(nameof(id))
        });
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }

    /// <summary>
    /// Gets an extended version of the entity identified by the provided value.
    /// </summary>
    /// <param name="id">The entity identifier</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity identified by the provided value or a null value if not found.</returns>
    public override Task<TEntity?> GetByIdExtendedAsync(TIdentity id, CancellationToken cancellationToken)
        => GetByIdExtendedAsync<TEntity>(id, cancellationToken);
    
    /// <summary>
    /// Executes a stored routine expecting to get a single entity.
    /// </summary>
    /// <param name="routineSimpleName">The routine simple name (UserGetOne, CustomerGetOne)</param>
    /// <param name="param">The parameters for the routine.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A single entity or a null value.</returns>
    protected virtual Task<T?> GetOneAsync<T>(
        string routineSimpleName,
        dynamic param,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneAsync<T>(routineSimpleName, param, null, cancellationToken);
    
    /// <summary>
    /// Executes a stored routine expecting to get a single entity.
    /// </summary>
    /// <param name="routineSimpleName">The routine simple name (UserGetOne, CustomerGetOne)</param>
    /// <param name="param">The parameters for the routine.</param>
    /// <param name="routineType">The type of routine (StoredProcedure, StoredFunction).</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A single entity or a null value.</returns>
    protected virtual Task<T?> GetOneAsync<T>(
        string routineSimpleName,
        dynamic param,
        DbRoutineType? routineType,
        CancellationToken cancellationToken
        ) where T : class
    {
        var dynamicParameters = ToDynamicParameters((object) param);
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement(routineSimpleName, dynamicParameters, routineType),
            dynamicParameters,
            ResolveRoutineCommandType(routineType),
            cancellationToken
            );
    }
    
    /// <summary>
    /// Gets an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided criteria.</returns>
    public override Task<T?> GetOneAsync<T>(
        dynamic criteria,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneAsync<T>(((object) criteria).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided criteria.</returns>
    public override Task<T?> GetOneAsync<T>(
        IReadOnlyDictionary<string, object?> criteria,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetOne;
        var param = ToDynamicParameters(criteria);
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }

    /// <summary>
    /// Gets an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided criteria.</returns>
    public override Task<TEntity?> GetOneAsync(dynamic criteria, CancellationToken cancellationToken)
        => GetOneAsync(((object) criteria).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided criteria.</returns>
    public override Task<TEntity?> GetOneAsync(
        IReadOnlyDictionary<string, object?> criteria,
        CancellationToken cancellationToken
        )
        => GetOneAsync<TEntity>(criteria, cancellationToken);
    
    /// <summary>
    /// Gets the extended version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided criteria.</returns>
    public override Task<T?> GetOneExtendedAsync<T>(
        dynamic criteria,
        CancellationToken cancellationToken
        ) where T : class
        => GetOneExtendedAsync<T>(((object) criteria).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets the extended version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entity matching the provided criteria.</returns>
    public override Task<T?> GetOneExtendedAsync<T>(
        IReadOnlyDictionary<string, object?> criteria,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetOneExtended;
        var param = ToDynamicParameters(criteria);
        return QueryFirstOrDefaultAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }

    /// <summary>
    /// Gets the extended version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided criteria.</returns>
    public override Task<TEntity?> GetOneExtendedAsync(dynamic criteria, CancellationToken cancellationToken)
        => GetOneExtendedAsync(((object) criteria).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets the extended version of an entity matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entity.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entity matching the provided criteria.</returns>
    public override Task<TEntity?> GetOneExtendedAsync(
        IReadOnlyDictionary<string, object?> criteria,
        CancellationToken cancellationToken
        )
        => GetOneExtendedAsync<TEntity>(criteria, cancellationToken);

    /// <summary>
    /// Executes a stored routine expecting to get a list of entities.
    /// </summary>
    /// <param name="routineSimpleName">The routine simple name (UserGetMany, CustomerGetMany)</param>
    /// <param name="param">The parameters for the routine.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A list of entities.</returns>
    protected virtual Task<IEnumerable<T>> GetManyAsync<T>(
        string routineSimpleName,
        dynamic param,
        CancellationToken cancellationToken
        ) where T : class
        => GetManyAsync<T>(routineSimpleName, param, null, cancellationToken);
    
    /// <summary>
    /// Executes a stored routine expecting to get a list of entities.
    /// </summary>
    /// <param name="routineSimpleName">The routine simple name (UserGetMany, CustomerGetMany)</param>
    /// <param name="param">The parameters for the routine.</param>
    /// <param name="routineType">The type of routine (StoredProcedure, StoredFunction).</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A list of entities.</returns>
    protected virtual Task<IEnumerable<T>> GetManyAsync<T>(
        string routineSimpleName,
        dynamic param,
        DbRoutineType? routineType,
        CancellationToken cancellationToken
        ) where T : class
    {
        var dynamicParameters = ToDynamicParameters((object) param);
        return QueryAsync<T>(
            ResolveRoutineStatement(routineSimpleName, dynamicParameters, routineType),
            dynamicParameters,
            ResolveRoutineCommandType(routineType),
            cancellationToken
           );
    }
    
    /// <summary>
    /// Gets one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided criteria.</returns>
    public override Task<IEnumerable<T>> GetManyAsync<T>(dynamic criteria, CancellationToken cancellationToken) where T : class 
        => GetManyAsync<T>(((object) criteria).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided criteria.</returns>
    public override Task<IEnumerable<T>> GetManyAsync<T>(
        IReadOnlyDictionary<string, object?> criteria,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetMany;
        var param = ToDynamicParameters(criteria);
        return QueryAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }

    /// <summary>
    /// Gets one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public override Task<IEnumerable<TEntity>> GetManyAsync(dynamic criteria, CancellationToken cancellationToken) 
        => GetManyAsync<TEntity>(((object) criteria).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public override Task<IEnumerable<TEntity>> GetManyAsync(IReadOnlyDictionary<string, object?> criteria, CancellationToken cancellationToken)
        => GetManyAsync<TEntity>(criteria, cancellationToken);
    
    /// <summary>
    /// Gets the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided criteria.</returns>
    public override Task<IEnumerable<T>> GetManyExtendedAsync<T>(dynamic criteria, CancellationToken cancellationToken) where T : class 
        => GetManyExtendedAsync<T>(((object) criteria).ToReadonlyDictionary(), cancellationToken);

    /// <summary>
    /// Gets the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">Tye type of entity to return.</typeparam>
    /// <returns>The entities matching the provided criteria.</returns>
    public override Task<IEnumerable<T>> GetManyExtendedAsync<T>(
        IReadOnlyDictionary<string, object?> criteria,
        CancellationToken cancellationToken
        ) where T : class
    {
        var action = Configuration.Actions.GetManyExtended;
        var param = ToDynamicParameters(criteria);
        return QueryAsync<T>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
            );
    }

    /// <summary>
    /// Gets the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">An object with properties to be used as criteria to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public override Task<IEnumerable<TEntity>> GetManyExtendedAsync(dynamic criteria, CancellationToken cancellationToken) 
        => GetManyExtendedAsync<TEntity>(((object) criteria).ToReadonlyDictionary(), cancellationToken);
    
    /// <summary>
    /// Gets the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The property names and values of an object to be used as criteria to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The entities matching the provided criteria.</returns>
    public override Task<IEnumerable<TEntity>> GetManyExtendedAsync(IReadOnlyDictionary<string, object?> criteria, CancellationToken cancellationToken)
        => GetManyExtendedAsync<TEntity>(criteria, cancellationToken);
}