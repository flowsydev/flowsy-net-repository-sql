using System.Data;
using Dapper;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    /// <summary>
    /// Creates a new entity in the underlying data store.
    /// </summary>
    /// <param name="entity">The entity to be created.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <returns>The identifier of the new entity.</returns>
    public override async Task<TIdentity> CreateAsync<T>(T entity, CancellationToken cancellationToken)
        where T : class
    {
        var id = await CreateAsync(entity.ToReadonlyDictionary(), cancellationToken);

        if (Configuration.AutoIdentity)
            entity.SetProperty(IdentityPropertyName, id);
        
        return id;
    }
    
    /// <summary>
    /// Creates a new entity in the underlying data store.
    /// </summary>
    /// <param name="entity">The entity to be created.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The identifier of the new entity.</returns>
    public override Task<TIdentity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        => CreateAsync<TEntity>(entity, cancellationToken);

    /// <summary>
    /// Creates a new entity in the underlying data store.
    /// </summary>
    /// <param name="entity">The entity to be created.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The identifier of the new entity.</returns>
    public override async Task<TIdentity> CreateAsync(dynamic entity, CancellationToken cancellationToken)
    {
        var id = await CreateAsync(((object) entity).ToReadonlyDictionary(), cancellationToken);
        
        if (Configuration.AutoIdentity)
            entity.SetProperty(IdentityPropertyName, id);
        
        return id;
    }

    /// <summary>
    /// Creates a new entity in the underlying data store.
    /// </summary>
    /// <param name="properties">The property names and values of the entity to be created.</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns>The identifier of the new entity.</returns>
    public override async Task<TIdentity> CreateAsync(IReadOnlyDictionary<string, object?> properties, CancellationToken cancellationToken)
    {
        IDbConnection? connection = null;
        try
        {
            var action = Configuration.Create;
            var excludedProperties = new List<string>(action.ExcludedProperties);
            if (Configuration.AutoIdentity)
                excludedProperties.Add(IdentityPropertyName);
            
            connection = GetConnection();
            return await connection.QueryFirstOrDefaultAsync<TIdentity>(new CommandDefinition(
                ResolveRoutineName($"{EntityName}{action.Name}"), 
                parameters: ToDynamicParameters(properties.ExceptBy(excludedProperties)),
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
            ));
        }
        catch
        {
            if (connection is not null && !InTransaction)
                connection.Dispose();

            throw;
        }
    }
}