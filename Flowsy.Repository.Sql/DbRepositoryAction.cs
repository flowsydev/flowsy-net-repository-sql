namespace Flowsy.Repository.Sql;

/// <summary>
/// Represents an action to be performed by a repository. 
/// </summary>
public class DbRepositoryAction
{
    public DbRepositoryAction(string name, IEnumerable<string>? excludedProperties = null, string? totalCountProperty = null)
    {
        Name = name;
        ExcludedProperties = excludedProperties ?? Array.Empty<string>();
        TotalCountProperty = totalCountProperty;
    }

    /// <summary>
    /// The name of the action. Could be a value like SelectOne, Insert, Update, Delete, DeleteMany, and so on.
    /// This value is intended to be used to build the name of the final stored routine to be executed in a query. 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The names to be excluded when reading properties of an entity involved in the action being executed.
    /// </summary>
    public IEnumerable<string> ExcludedProperties { get; set; }
    
    /// <summary>
    /// The name of the property within paged query results used to determine the total entity count.
    /// </summary>
    public string? TotalCountProperty { get; set; }

    /// <summary>
    /// Returns a String which represents the object instance.
    /// </summary>
    /// <returns>The action name.</returns>
    public override string ToString()
    {
        return Name;
    }
}