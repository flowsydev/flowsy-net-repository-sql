using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    protected IDictionary<string, object?> ResolvePageQueryCriteria<TQuery>(TQuery query) where TQuery : EntityPageQuery
    {
        var excludedProperties = new []
        {
            "CountTotal",
            "TotalCountProperty",
            "PageNumber",
            "PageSize"
        };
        var criteria = query
            .ToDictionary();
        
        foreach (var excluded in excludedProperties)
            if (criteria.ContainsKey(excluded))
                criteria.Remove(excluded);
        
        query.Translate(out var offset, out var limit);
        criteria["Offset"] = offset;
        criteria["Limit"] = limit;

        return criteria;
    }

    protected EntityPageQueryResult<TQuery, TResult> ResolvePageQueryResult<TQuery, TResult>(
        DbRepositoryAction action,
        TQuery query,
        IEnumerable<TResult> results
        ) 
        where TQuery : EntityPageQuery
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
        
        return new EntityPageQueryResult<TQuery, TResult>(query, list, totalItemCount);
    }

    /// <summary>
    /// Gets a page of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override async Task<EntityPageQueryResult<TQuery, TResult>> GetPageAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken cancellationToken
        )
    {
        var action = Configuration.GetManyPaged;
        var results = await QueryAsync<TResult>(
            ResolveRoutineName($"{EntityName}{action.Name}"),
            (IReadOnlyDictionary<string, object?>) ResolvePageQueryCriteria(query),
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
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override Task<EntityPageQueryResult<TQuery, TEntity>> GetPageAsync<TQuery>(
        TQuery query,
        CancellationToken cancellationToken
        )
        => GetPageAsync<TQuery, TEntity>(query, cancellationToken);

    /// <summary>
    /// Gets a page of the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override async Task<EntityPageQueryResult<TQuery, TResult>> GetPageExtendedAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken cancellationToken
        )
    {
        var action = Configuration.GetManyExtendedPaged;
        var results = await QueryAsync<TResult>(
            ResolveRoutineName($"{EntityName}{action.Name}"),
            (IReadOnlyDictionary<string, object?>) ResolvePageQueryCriteria(query),
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
    /// <typeparam name="TCriteria">The type of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override Task<EntityPageQueryResult<TCriteria, TEntity>> GetPageExtendedAsync<TCriteria>(
        TCriteria query,
        CancellationToken cancellationToken
        )
        => GetPageExtendedAsync<TCriteria, TEntity>(query, cancellationToken);
}