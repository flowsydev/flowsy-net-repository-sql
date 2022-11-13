using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepositoryTranslated<TEntity, TEntityTranslated, TIdentity>
{
    /// <summary>
    /// Gets a page of the translated version of the entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of crieteria for the query.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public async Task<EntityPageQueryResult<TCriteria, TResult>> GetManyAsync<TCriteria, TResult>(
        EntityPageQuery<TCriteria> query,
        string? cultureId,
        CancellationToken cancellationToken
        ) 
        where TCriteria : class where TResult : class
    {
        var action = Configuration.GetManyExtendedTranslatedPaged;
        var criteria = query.Criteria is not null 
            ? query.Criteria.ToDictionary() 
            : new Dictionary<string, object?>();

        criteria["CultureId"] = cultureId;
        
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
    /// Gets a page of the translated version of the entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of crieteria for the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public Task<EntityPageQueryResult<TCriteria, TEntityTranslated>> GetManyAsync<TCriteria>(
        EntityPageQuery<TCriteria> query,
        string? cultureId,
        CancellationToken cancellationToken
        )
        where TCriteria : class
        =>
            GetManyAsync<TCriteria, TEntityTranslated>(query, cultureId, cancellationToken);

    /// <summary>
    /// Gets a page of the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of crieteria for the query.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public async Task<EntityPageQueryResult<TCriteria, TResult>> GetManyExtendedAsync<TCriteria, TResult>(EntityPageQuery<TCriteria> query, string? cultureId,
        CancellationToken cancellationToken) where TCriteria : class where TResult : class
    {
        var action = Configuration.GetManyExtendedTranslatedPaged;
        var criteria = query.Criteria is not null 
            ? query.Criteria.ToDictionary() 
            : new Dictionary<string, object?>();

        criteria["CultureId"] = cultureId;
        
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
    /// Gets a page of the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of crieteria for the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public Task<EntityPageQueryResult<TCriteria, TEntityTranslated>> GetManyExtendedAsync<TCriteria>(
        EntityPageQuery<TCriteria> query,
        string? cultureId,
        CancellationToken cancellationToken
        )
        where TCriteria : class
        =>
            GetManyExtendedAsync<TCriteria, TEntityTranslated>(query, cultureId, cancellationToken);
}