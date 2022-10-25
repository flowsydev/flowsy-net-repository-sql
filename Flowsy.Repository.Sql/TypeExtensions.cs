using System.Reflection;
using Dapper;
using Flowsy.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Extension methods for Type class.
/// </summary>
public static class TypeExtensions
{
    public static Type SetColumnMapping(this Type type, NamingConvention columnNamingConvention)
    {
        SqlMapper.RemoveTypeMap(type);
        return type.SetColumnMapping(
            (entityType, columnName) => 
                entityType
                    .GetRuntimeProperties()
                    .FirstOrDefault(p => p.Name.ApplyNamingConvention(columnNamingConvention) == columnName)
            );
    }
    
    public static Type SetColumnMapping(this Type type, Func<Type, string, PropertyInfo?> mapColumn)
    {
        SqlMapper.RemoveTypeMap(type);
        SqlMapper.SetTypeMap(type, new CustomPropertyTypeMap(type, mapColumn));
        return type;
    }
}