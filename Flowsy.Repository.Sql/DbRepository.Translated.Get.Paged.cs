using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TEntityTranslated, TIdentity>
{
    /// <summary>
    /// Gets a page of the translated version of the entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of the criteria and pagination options.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public virtual async Task<EntityPage<TCriteria, TResult>> GetPageAsync<TCriteria, TResult>(
        TCriteria criteria,
        string? cultureId,
        CancellationToken cancellationToken
        ) 
        where TCriteria : EntityPageCriteria
        where TResult : class
    {
        var criteriaDictionary = ToDictionary(criteria);
        criteriaDictionary["CultureId"] = cultureId;

        var action = Configuration.Actions.GetManyTranslatedPaged;
        var param = ToDynamicParameters(criteriaDictionary);
        var results = await QueryAsync<TResult>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
        );
        return ResolveEntityPage(action, criteria, results);
    }

    /// <summary>
    /// Gets a page of the translated version of the entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of the criteria and pagination options.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public virtual Task<EntityPage<TCriteria, TEntityTranslated>> GetPageAsync<TCriteria>(
        TCriteria criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        where TCriteria : EntityPageCriteria
        =>
            GetPageAsync<TCriteria, TEntityTranslated>(criteria, cultureId, cancellationToken);

    /// <summary>
    /// Gets a page of the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of the criteria and pagination options.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public virtual async Task<EntityPage<TCriteria, TResult>> GetPageExtendedAsync<TCriteria, TResult>(
        TCriteria criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        where TCriteria : EntityPageCriteria
        where TResult : class
    {
        var criteriaDictionary = ToDictionary(criteria);
        criteriaDictionary["CultureId"] = cultureId;
        
        var action = Configuration.Actions.GetManyExtendedTranslatedPaged;
        var param = ToDynamicParameters(criteriaDictionary);
        var results = await QueryAsync<TResult>(
            ResolveRoutineStatement($"{EntityName}{action.Name}", param),
            param,
            ResolveRoutineCommandType(),
            cancellationToken
        );
        return ResolveEntityPage(action, criteria, results);
    }

    /// <summary>
    /// Gets a page of the extended and translated version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria and paging options to find the entities.</param>
    /// <param name="cultureId">The culture identifier.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of the criteria and pagination options.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public virtual Task<EntityPage<TCriteria, TEntityTranslated>> GetPageExtendedAsync<TCriteria>(
        TCriteria criteria,
        string? cultureId,
        CancellationToken cancellationToken
        )
        where TCriteria : EntityPageCriteria
        =>
            GetPageExtendedAsync<TCriteria, TEntityTranslated>(criteria, cultureId, cancellationToken);
}