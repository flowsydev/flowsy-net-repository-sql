using System.Reflection;
using Flowsy.Core;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Extensions methods for the Assembly class.
/// </summary>
public static class AssemblyExtensions
{
    private static readonly Type EntityType = typeof(IEntity);
    
    private static IEnumerable<Type> GetEntityTypes(Assembly assembly)
        =>
            from t in assembly.GetTypes()
            where EntityType.IsAssignableFrom(t)
            select t;

    /// <summary>
    /// Registers a naming convention-based column mapping for all the entity types in the assembly. 
    /// </summary>
    /// <param name="assembly">The assembly containing the entity types.</param>
    /// <param name="columnNamingConvention">
    /// The convention applied in the database to name table columns.
    /// The entity property names will compared using this naming convention in order to match the corresponding table column.
    /// </param>
    /// <returns>The assembly.</returns>
    public static Assembly RegisterSqlColumnMapping(
        this Assembly assembly,
        NamingConvention columnNamingConvention
        )
    {
        foreach (var type in GetEntityTypes(assembly))
            type.SetColumnMapping(columnNamingConvention);

        return assembly;
    }
    
    /// <summary>
    /// Registers a naming convention-based column mapping for all the entity types in the list of assemblies. 
    /// </summary>
    /// <param name="assemblies">The assemblies containing the entity types.</param>
    /// <param name="columnNamingConvention">
    /// The convention applied in the database to name table columns.
    /// The entity property names will compared using this naming convention in order to match the corresponding table column.
    /// </param>
    public static void RegisterSqlColumnMapping(this IEnumerable<Assembly> assemblies, NamingConvention columnNamingConvention)
    {
        foreach (var assembly in assemblies)
            assembly.RegisterSqlColumnMapping(columnNamingConvention);
    }
    
    /// <summary>
    /// Registers a dynamic column mapping for all the entity types in the assembly. 
    /// </summary>
    /// <param name="assembly">The assembly containing the entity types.</param>
    /// <param name="mapColumn">
    /// A function that dynamically determines the entity property to be matched to the given column. 
    /// </param>
    /// <returns>The assembly.</returns>
    public static Assembly RegisterSqlColumnMapping(
        this Assembly assembly,
        Func<Type, string, PropertyInfo?> mapColumn
        )
    {
        foreach (var type in GetEntityTypes(assembly))
            type.SetColumnMapping(mapColumn);
        
        return assembly;
    }

    /// <summary>
    /// Registers a dynamic column mapping for all the entity types in the list of assemblies. 
    /// </summary>
    /// <param name="assemblies">The assemblies containing the entity types.</param>
    /// <param name="mapColumn">
    /// A function that dynamically determines the entity property to be matched to the given column. 
    /// </param>
    public static void RegisterSqlColumnMapping(
        this IEnumerable<Assembly> assemblies,
        Func<Type, string, PropertyInfo?> mapColumn
        )
    {
        foreach (var assembly in assemblies)
            assembly.RegisterSqlColumnMapping(mapColumn);
    }
}