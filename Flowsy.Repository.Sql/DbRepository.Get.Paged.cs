using System.Data;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    protected virtual IDictionary<string, object?> ToDictionary(EntityPageCriteria criteria)
    {
        var excludedProperties = new []
        {
            "CountTotal",
            "TotalCountProperty",
            "PageNumber",
            "PageSize"
        };
        var dictionary = criteria.ToDictionary();
        
        foreach (var excluded in excludedProperties)
            if (dictionary.ContainsKey(excluded))
                dictionary.Remove(excluded);
        
        criteria.Translate(out var offset, out var limit);
        dictionary["Offset"] = offset;
        dictionary["Limit"] = limit;

        return dictionary;
    }

    protected virtual EntityPage<TCriteria, TPageEntity> ResolveEntityPage<TCriteria, TPageEntity>(
        DbRepositoryAction action,
        TCriteria criteria,
        IEnumerable<TPageEntity> entities
        )
        where TCriteria : EntityPageCriteria
        where TPageEntity : class
    {
        var list = entities.ToList();
        
        long? totalItemCount = null;
        var totalCountProperty = criteria.TotalCountProperty ?? action.TotalCountProperty;
        if (criteria.CountTotal && !string.IsNullOrEmpty(totalCountProperty) && list.Any())
        {
            var firstResult = list.First();
            var property = firstResult.GetType().GetProperty(totalCountProperty);
            var value = property?.GetValue(firstResult);
            if (value is not null)
                totalItemCount = Convert.ToInt64(value);
        }
        
        return new EntityPage<TCriteria, TPageEntity>(criteria, list, totalItemCount);
    }

    /// <summary>
    /// Gets a page of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of the criteria and pagination options.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override async Task<EntityPage<TCriteria, TResult>> GetPageAsync<TCriteria, TResult>(
        TCriteria criteria,
        CancellationToken cancellationToken
        )
    {
        var action = Configuration.GetManyPaged;
        var results = await QueryAsync<TResult>(
            ResolveRoutineName($"{EntityName}{action.Name}"),
            (IReadOnlyDictionary<string, object?>) ToDictionary(criteria),
            CommandType.StoredProcedure,
            cancellationToken
            );
        return ResolveEntityPage(action, criteria, results);
    }

    /// <summary>
    /// Gets a page of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of the criteria and pagination options.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override Task<EntityPage<TCriteria, TEntity>> GetPageAsync<TCriteria>(
        TCriteria criteria,
        CancellationToken cancellationToken
        )
        => GetPageAsync<TCriteria, TEntity>(criteria, cancellationToken);

    /// <summary>
    /// Gets a page of the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of the criteria and pagination options.</typeparam>
    /// <typeparam name="TResult">The type of the entities expected as the result of the query.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override async Task<EntityPage<TCriteria, TResult>> GetPageExtendedAsync<TCriteria, TResult>(
        TCriteria criteria,
        CancellationToken cancellationToken
        )
    {
        var action = Configuration.GetManyExtendedPaged;
        var results = await QueryAsync<TResult>(
            ResolveRoutineName($"{EntityName}{action.Name}"),
            (IReadOnlyDictionary<string, object?>) ToDictionary(criteria),
            CommandType.StoredProcedure,
            cancellationToken
            );
        return ResolveEntityPage(action, criteria, results);
    }

    /// <summary>
    /// Gets a page of the extended version of one or more entities matching the specified criteria.
    /// </summary>
    /// <param name="criteria">The criteria and paging options to find the entities.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="TCriteria">The type of the criteria and pagination options.</typeparam>
    /// <returns>The page of entities matching the provided criteria.</returns>
    public override Task<EntityPage<TCriteria, TEntity>> GetPageExtendedAsync<TCriteria>(
        TCriteria criteria,
        CancellationToken cancellationToken
        )
        => GetPageExtendedAsync<TCriteria, TEntity>(criteria, cancellationToken);
}