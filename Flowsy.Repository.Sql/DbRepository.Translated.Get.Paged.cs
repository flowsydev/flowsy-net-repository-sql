using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TEntityTranslated, TIdentity>
{
    /// <summary>
    /// Gets a page of the translated version of the entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public async Task<EntityPageQueryResult<TQuery, TResult>> GetPageAsync<TQuery, TResult>(
        TQuery query,
        string? cultureId,
        CancellationToken cancellationToken
        ) 
        where TQuery : EntityPageQuery
        where TResult : class
    {
        var action = Configuration.GetManyExtendedTranslatedPaged;
        var criteria = ResolvePageQueryCriteria(query);
        
        criteria["CultureId"] = cultureId;
        
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
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public Task<EntityPageQueryResult<TQuery, TEntityTranslated>> GetPageAsync<TQuery>(
        TQuery query,
        string? cultureId,
        CancellationToken cancellationToken
        )
        where TQuery : EntityPageQuery
        =>
            GetPageAsync<TQuery, TEntityTranslated>(query, cultureId, cancellationToken);

    /// <summary>
    /// Gets a page of the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="query">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public async Task<EntityPageQueryResult<TQuery, TResult>> GetPageExtendedAsync<TQuery, TResult>(
        TQuery query,
        string? cultureId,
        CancellationToken cancellationToken
        )
        where TQuery : EntityPageQuery
        where TResult : class
    {
        var action = Configuration.GetManyExtendedTranslatedPaged;
        var criteria = ResolvePageQueryCriteria(query);

        criteria["CultureId"] = cultureId;
        
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
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public Task<EntityPageQueryResult<TQuery, TEntityTranslated>> GetPageExtendedAsync<TQuery>(
        TQuery query,
        string? cultureId,
        CancellationToken cancellationToken
        )
        where TQuery : EntityPageQuery
        =>
            GetPageExtendedAsync<TQuery, TEntityTranslated>(query, cultureId, cancellationToken);
}