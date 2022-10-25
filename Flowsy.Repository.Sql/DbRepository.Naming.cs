using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract partial class DbRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    /// <summary>
    /// The name of the property that uniquely identifies an entity of the repository. 
    /// </summary>
    public override string IdentityPropertyName => Configuration.ResolveIdentityPropertyName(typeof(TEntity));
    
    /// <summary>
    /// Resolves a stored routine name by applying the schema name, prefix, naming convention and suffix configured for the repository.  
    /// </summary>
    /// <param name="simpleName">A simple name like UserInsert or CustomerSelectMany.</param>
    /// <returns>A full routine name like public.fn_user_insert or public.fn_customer_select_many.</returns>
    protected virtual string ResolveRoutineName(string simpleName)
    {
        var schemaPrefix = string.IsNullOrEmpty(Configuration.SchemaName) ? string.Empty : $"{Configuration.SchemaName}.";
        var routineName = $"{Configuration.RoutineConvention.Prefix}{simpleName.ApplyNamingConvention(Configuration.RoutineConvention.Naming)}{Configuration.RoutineConvention.Suffix}";

        return $"{schemaPrefix}{routineName}";
    }

    /// <summary>
    /// Resolves a parameter name by applying the prefix, naming convention and suffix configured for the repository.
    /// </summary>
    /// <param name="simpleName">A simple name like UserId or EmailAddress.</param>
    /// <returns>A full parameter name like p_user_id or p_email_address.</returns>
    protected virtual string ResolveRoutineParameterName(string simpleName) =>
        $"{Configuration.ParameterConvention.Prefix}{simpleName.ApplyNamingConvention(Configuration.ParameterConvention.Naming)}{Configuration.ParameterConvention.Suffix}";   
}