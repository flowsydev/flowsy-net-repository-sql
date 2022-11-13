namespace Flowsy.Repository.Sql;

/// <summary>
/// Represents an action to be performed by a repository. 
/// </summary>
public class DbRepositoryAction
{
    public DbRepositoryAction(string name) : this(name, Array.Empty<string>())
    {
        
    }

    public DbRepositoryAction(string name, IEnumerable<string> excludedProperties, string? totalCountProperty = null)
    {
        Name = name;
        ExcludedProperties = excludedProperties;
        TotalCountProperty = totalCountProperty;
    }

    /// <summary>
    /// The name of the action. Could be a value like SelectOne, Insert, Update, Delete, DeleteMany, and so on.
    /// This value is intended to be used to build the name of the final stored routine to be executed in a query. 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The name of the property within paged query results used to determine the total entity count.
    /// </summary>
    public string? TotalCountProperty { get; set; }
    
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
        public static DbRepositoryAction Create { get; set; }
            = new(nameof(Create));

        /// <summary>
        /// Action used to update an entity.
        /// </summary>
        public static DbRepositoryAction Update { get; set; }
            = new(nameof(Update));
        
        /// <summary>
        /// Action used to patch an entity.
        /// </summary>
        public static DbRepositoryAction Patch { get; set; }
            = new(nameof(Patch));

        /// <summary>
        /// Action used to delete an entity.
        /// </summary>
        public static DbRepositoryAction DeleteById { get; set; }
            = new(nameof(DeleteById));

        /// <summary>
        /// Action used to delete multiple entities at once.
        /// </summary>
        public static DbRepositoryAction DeleteMany { get; set; }
            = new(nameof(DeleteMany));
        
        /// <summary>
        /// Action used to get an entity identified by a given value.
        /// </summary>
        public static DbRepositoryAction GetById { get; set; } 
            = new(nameof(GetById));
        
        /// <summary>
        /// Action used to get the extended version of an entity identified by a given value.
        /// </summary>
        public static DbRepositoryAction GetByIdExtended { get; set; } 
            = new(nameof(GetByIdExtended));
        
        /// <summary>
        /// Action used to get the extended and translated version of an entity identified by a given value.
        /// </summary>
        public static DbRepositoryAction GetByIdExtendedTranslated { get; set; } 
            = new(nameof(GetByIdExtendedTranslated));
        
        /// <summary>
        /// Action used to get the translated version of an entity identified by a given value.
        /// </summary>
        public static DbRepositoryAction GetByIdTranslated { get; set; } 
            = new(nameof(GetByIdTranslated));
        
        /// <summary>
        /// Action used to get a single entity matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetOne { get; set; } 
            = new(nameof(GetOne));
        
        /// <summary>
        /// Action used to get the extended version of a single entity matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetOneExtended { get; set; } 
            = new(nameof(GetOneExtended));
        
        /// <summary>
        /// Action used to get the extended and translated version of a single entity matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetOneExtendedTranslated { get; set; } 
            = new(nameof(GetOneExtendedTranslated));
        
        /// <summary>
        /// Action used to get the translated version of a single entity matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetOneTranslated { get; set; } 
            = new(nameof(GetOneTranslated));
        
        /// <summary>
        /// Action used to get one or more entities matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetMany { get; set; } 
            = new(nameof(GetMany));
        
        /// <summary>
        /// Action used to get a page of one or more entities matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetManyPaged { get; set; } 
            = new(nameof(GetManyPaged));
        
        /// <summary>
        /// Action used to get the extended version of one or more entities matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetManyExtended { get; set; } 
            = new(nameof(GetManyExtended));
        
        /// <summary>
        /// Action used to get a page of the extended version of one or more entities matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetManyExtendedPaged { get; set; } 
            = new(nameof(GetManyExtendedPaged));
        
        /// <summary>
        /// Action used to get the extended and translated version of one or more entities matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetManyExtendedTranslated { get; set; } 
            = new(nameof(GetManyExtendedTranslated));
        
        /// <summary>
        /// Action used to get a page of the extended and translated version of one or more entities matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetManyExtendedTranslatedPaged { get; set; } 
            = new(nameof(GetManyExtendedTranslatedPaged));
        
        /// <summary>
        /// Action used to get the translated version of one or more entities matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetManyTranslated { get; set; } 
            = new(nameof(GetManyTranslated));
        
        /// <summary>
        /// Action used to get the translated version of one or more entities matching a specified criteria.
        /// </summary>
        public static DbRepositoryAction GetManyTranslatedPaged { get; set; } 
            = new(nameof(GetManyTranslatedPaged));
        
        /// <summary>
        /// The name of the property within paged query results used to determine the total entity count.
        /// </summary>
        public static string PagedQueryTotalCountProperty { get; set; } 
            = "TotalCount";
    }
}