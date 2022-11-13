using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    protected EntityPageQueryResult<TCriteria, TResult> ResolvePageQueryResult<TCriteria, TResult>(
        DbRepositoryAction action,
        EntityPageQuery<TCriteria> query,
        IEnumerable<TResult> results
        ) 
        where TCriteria : class
        where TResult : class
    {
        var list = results.ToList();
        
        long? totalItemCount = null;
        var totalCountProperty = query.TotalCountProperty ?? action.TotalCountProperty;
        if (query.CountTotal && !string.IsNullOrEmpty(totalCountProperty) && list.Any())
        {
            var firstResult = list.First();
            var property = firstResult.GetType().GetProperty(totalCountProperty);
            var value = property?.GetValue(firstResult);
            if (value is not null)
                totalItemCount = Convert.ToInt64(value);
        }
        
        return new EntityPageQueryResult<TCriteria, TResult>(query, list, totalItemCount);
    }

    /// <summary>
    /// Gets a page of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of crieteria for the query.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override async Task<EntityPageQueryResult<TCriteria, TResult>> GetManyAsync<TCriteria, TResult>(
        EntityPageQuery<TCriteria> query,
        CancellationToken cancellationToken
        )
    {
        var action = Configuration.GetManyPaged;
        var criteria =
            query.Criteria is not null ? query.Criteria.ToDictionary() : new Dictionary<string, object?>();
        
        query.Translate(out var offset, out var limit);
        criteria["Offset"] = offset;
        criteria["Limit"] = limit;
        
        var results = await QueryAsync<TResult>(
            ResolveRoutineName($"{EntityName}{action.Name}"),
            (IReadOnlyDictionary<string, object?>) criteria,
            CommandType.StoredProcedure,
            cancellationToken
            );
        return ResolvePageQueryResult(action, query, results);
    }

    /// <summary>
    /// Gets a page of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of crieteria for the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override Task<EntityPageQueryResult<TCriteria, TEntity>> GetManyAsync<TCriteria>(
        EntityPageQuery<TCriteria> query,
        CancellationToken cancellationToken
        )
        => GetManyAsync<TCriteria, TEntity>(query, cancellationToken);

    /// <summary>
    /// Gets a page of the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of crieteria for the query.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override async Task<EntityPageQueryResult<TCriteria, TResult>> GetManyExtendedAsync<TCriteria, TResult>(
        EntityPageQuery<TCriteria> query,
        CancellationToken cancellationToken
        )
    {
        var action = Configuration.GetManyExtendedPaged;
        var criteria =
            query.Criteria is not null ? query.Criteria.ToDictionary() : new Dictionary<string, object?>();

        query.Translate(out var offset, out var limit);
        criteria["Offset"] = offset;
        criteria["Limit"] = limit;

        var results = await QueryAsync<TResult>(
            ResolveRoutineName($"{EntityName}{action.Name}"),
            (IReadOnlyDictionary<string, object?>) criteria,
            CommandType.StoredProcedure,
            cancellationToken
            );
        return ResolvePageQueryResult(action, query, results);
    }

    /// <summary>
    /// Gets a page of the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of crieteria for the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override Task<EntityPageQueryResult<TCriteria, TEntity>> GetManyExtendedAsync<TCriteria>(
        EntityPageQuery<TCriteria> query,
        CancellationToken cancellationToken
        )
        => GetManyExtendedAsync<TCriteria, TEntity>(query, cancellationToken);
}