namespace Flowsy.Repository.Sql;

/// <summary>
/// Represents an action to be performed by a repository. 
/// </summary>
public class DbRepositoryAction
{
    public DbRepositoryAction(string name) : this(name, Array.Empty<string>())
    {
        
    }

    public DbRepositoryAction(string name, IEnumerable<string> excludedProperties)
    {
        Name = name;
        ExcludedProperties = excludedProperties;
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
    /// Holds default configurations for the actions supported by a DbRepository.
    /// </summary>
    public static class Default
    {
        /// <summary>
        /// Action used to create a new entity.
        /// </summary>
        public static DbRepositoryAction Insert { get; set; }
            = new("Insert");

        /// <summary>
        /// Action used to update an entity.
        /// </summary>
        public static DbRepositoryAction Update { get; set; }
            = new("Update");
        
        /// <summary>
        /// Action used to patch an entity.
        /// </summary>
        public static DbRepositoryAction Patch { get; set; }
            = new("Patch");

        /// <summary>
        /// Action used to delete an entity.
        /// </summary>
        public static DbRepositoryAction DeleteById { get; set; }
            = new("DeleteById");

        /// <summary>
        /// Action used to delete multiple entities at once.
        /// </summary>
        public static DbRepositoryAction DeleteMany { get; set; }
            = new("DeleteMany");
        
        /// <summary>
        /// Action used to get an entity identified by a given value.
        /// </summary>
        public static DbRepositoryAction SelectById { get; set; } 
            = new("SelectById");
        
        /// <summary>
        /// Action used to get the extended version of an entity identified by a given value.
        /// </summary>
        public static DbRepositoryAction SelectByIdExtended { get; set; } 
            = new("SelectByIdExtended");
        
        /// <summary>
        /// Action used to get the extended and translated version of an entity identified by a given value.
        /// </summary>
        public static DbRepositoryAction SelectByIdExtendedTranslated { get; set; } 
            = new("SelectByIdExtendedTranslated");
        
        /// <summary>
        /// Action used to get the translated version of an entity identified by a given value.
        /// </summary>
        public static DbRepositoryAction SelectByIdTranslated { get; set; } 
            = new("SelectByIdTranslated");
        
        /// <summary>
        /// Action used to get one or more entities matching a specified filter.
        /// </summary>
        public static DbRepositoryAction SelectMany { get; set; } 
            = new("SelectMany");
        
        /// <summary>
        /// Action used to get the extended version of one or more entities matching a specified filter.
        /// </summary>
        public static DbRepositoryAction SelectManyExtended { get; set; } 
            = new("SelectManyExtended");
        
        /// <summary>
        /// Action used to get the extended and translated version of one or more entities matching a specified filter.
        /// </summary>
        public static DbRepositoryAction SelectManyExtendedTranslated { get; set; } 
            = new("SelectManyExtendedTranslated");
        
        /// <summary>
        /// Action used to get the translated version of one or more entities matching a specified filter.
        /// </summary>
        public static DbRepositoryAction SelectManyTranslated { get; set; } 
            = new("SelectManyTranslated");

        /// <summary>
        /// Action used to get a single entity matching a specified filter.
        /// </summary>
        public static DbRepositoryAction SelectOne { get; set; } 
            = new("SelectOne");
        
        /// <summary>
        /// Action used to get the extended version of a single entity matching a specified filter.
        /// </summary>
        public static DbRepositoryAction SelectOneExtended { get; set; } 
            = new("SelectOneExtended");
        
        /// <summary>
        /// Action used to get the extended and translated version of a single entity matching a specified filter.
        /// </summary>
        public static DbRepositoryAction SelectOneExtendedTranslated { get; set; } 
            = new("SelectOneExtendedTranslated");
        
        /// <summary>
        /// Action used to get the translated version of a single entity matching a specified filter.
        /// </summary>
        public static DbRepositoryAction SelectOneTranslated { get; set; } 
            = new("SelectOneTranslated");
    }
}