using Flowsy.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Contains data related to the naming conventions used to name objects of a database, such as tables, stored procedures and parameters.
/// </summary>
public class DbConvention
{
    private static DbConvention? _default;

    /// <summary>
    /// Used as a fallback in a context where no convention was provided.
    /// </summary>
    public static DbConvention Default
        => _default ??= new DbConvention(NamingConvention.LowerSnakeCase, string.Empty, string.Empty);
    
    public DbConvention(NamingConvention? naming, string prefix = "", string suffix = "")
    {
        Naming = naming;
        Prefix = prefix;
        Suffix = suffix;
    }

    /// <summary>
    /// Naming convention for a given object, for example: lower_snake_case, UpperPascalCase, UPPER-KEBAB-CASE. 
    /// </summary>
    public NamingConvention? Naming { get; set; }
    
    /// <summary>
    /// Text to prepend to a given object name
    /// </summary>
    public string Prefix { get; set; }
    
    /// <summary>
    /// Text to append to a given object name
    /// </summary>
    public string Suffix { get; set; }
}