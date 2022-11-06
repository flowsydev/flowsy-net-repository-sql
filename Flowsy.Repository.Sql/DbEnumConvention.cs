using Flowsy.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Contains data related to the format and naming conventions used to send enum values to the underlying database.
/// </summary>
public class DbEnumConvention : DbConvention
{
    private static DbEnumConvention? _default;

    /// <summary>
    /// Used as a fallback in a context where no convention was provided.
    /// </summary>
    public new static DbEnumConvention Default
        => _default ??= new DbEnumConvention(DbEnumFormat.Name, NamingConvention.PascalCase, string.Empty, string.Empty);
    
    public DbEnumConvention(DbEnumFormat format, NamingConvention? naming, string prefix = "", string suffix = "") : base(naming, prefix, suffix)
    {
        Format = format;
    }

    /// <summary>
    /// The format used to send enum values to the underlying database
    /// </summary>
    public DbEnumFormat Format { get; set; } 
}