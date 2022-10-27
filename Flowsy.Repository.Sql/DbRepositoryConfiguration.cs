using Flowsy.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Enables customization of the behavior of repositories.
/// </summary>
public class DbRepositoryConfiguration
{
    private static readonly IDictionary<Type, DbRepositoryConfiguration> Configurations = new Dictionary<Type, DbRepositoryConfiguration>();
    
    /// <summary>
    /// Holds the default configuration for repositories.
    /// </summary>
    public static DbRepositoryConfiguration Default { get; set; } = new();
    
    /// <summary>
    /// Registers a configuration associated to a specific repository type.
    /// </summary>
    /// <param name="repositoryType">The repository type.</param>
    /// <param name="configuration">The repository configuration.</param>
    public static void Register(Type repositoryType, DbRepositoryConfiguration configuration)
    {
        Configurations[repositoryType] = configuration;
    }
    
    /// <summary>
    /// Gets the configuration registered for a type of repository or the Default instance if none was found.
    /// </summary>
    /// <param name="repositoryType">The repository type.</param>
    /// <returns>A repository configuration.</returns>
    public static DbRepositoryConfiguration Resolve(Type repositoryType)
        => Configurations.ContainsKey(repositoryType) ? Configurations[repositoryType] : Default;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="connectionKey">The database connection to be used by a repository.</param>
    /// <param name="schemaName">If supported by the underlying database, specifies the schema that contains the stored routines to be executed by the repository.</param>
    /// <param name="resolveIdentityPropertyName">The function to invoke in order to resolve the identity property name of a given entity type.</param>
    /// <param name="autoIdentity">Specifies whether or not the value for the identity property will be generated automatically.</param>
    /// <param name="routineConvention">The conventions used to build names for stored routines.</param>
    /// <param name="parameterConvention">The conventions used to build names for database parameters.</param>
    /// <param name="enumConvention">The conventions used to send enum values to the underlying database.</param>
    /// <param name="insert">Action used to create a new entity.</param>
    /// <param name="update">Action used to update an entity.</param>
    /// <param name="patch">Action used to patch an entity.</param>
    /// <param name="deleteById">Action used to delete an entity.Action used to patch an entity.</param>
    /// <param name="deleteMany">Action used to delete multiple entities at once.</param>
    /// <param name="selectById">Action used to get an entity identified by a given value.</param>
    /// <param name="selectByIdExtended">Action used to get the extended version of an entity identified by a given value.</param>
    /// <param name="selectByIdExtendedTranslated">Action used to get the extended and translated version of an entity identified by a given value.</param>
    /// <param name="selectByIdTranslated">Action used to get the translated version of an entity identified by a given value.</param>
    /// <param name="selectMany">Action used to get one or more entities matching a specified filter.</param>
    /// <param name="selectManyExtended">Action used to get the extended version of one or more entities matching a specified filter.</param>
    /// <param name="selectManyExtendedTranslated">Action used to get the extended and translated version of one or more entities matching a specified filter.</param>
    /// <param name="selectManyTranslated">Action used to get the translated version of one or more entities matching a specified filter.</param>
    /// <param name="selectOne">Action used to get a single entity matching a specified filter.</param>
    /// <param name="selectOneExtended">Action used to get the extended version of a single entity matching a specified filter.</param>
    /// <param name="selectOneExtendedTranslated">Action used to get the extended and translated version of a single entity matching a specified filter.</param>
    /// <param name="selectOneTranslated">Action used to get the translated version of a single entity matching a specified filter.</param>
    public DbRepositoryConfiguration(
        string? connectionKey = default, 
        
        string? schemaName = default,
        Func<Type, string>? resolveIdentityPropertyName = default,
        bool? autoIdentity = default,
        
        DbConvention? routineConvention = default,
        DbConvention? parameterConvention = default,
        DbEnumConvention? enumConvention = default,

        DbRepositoryAction? insert = default,
        DbRepositoryAction? update = default,
        DbRepositoryAction? patch = default,
        DbRepositoryAction? deleteById = default,
        DbRepositoryAction? deleteMany = default,
        
        DbRepositoryAction? selectById = default,
        DbRepositoryAction? selectByIdExtended = default,
        DbRepositoryAction? selectByIdExtendedTranslated = default,
        DbRepositoryAction? selectByIdTranslated = default,
        
        DbRepositoryAction? selectMany = default,
        DbRepositoryAction? selectManyExtended = default,
        DbRepositoryAction? selectManyExtendedTranslated = default,
        DbRepositoryAction? selectManyTranslated = default,
        
        DbRepositoryAction? selectOne = default,
        DbRepositoryAction? selectOneExtended = default,
        DbRepositoryAction? selectOneExtendedTranslated = default,
        DbRepositoryAction? selectOneTranslated = default
    )
    {
        _connectionKey = connectionKey;
        
        _schemaName = schemaName;
        _resolveIdentityPropertyName = resolveIdentityPropertyName;
        _autoIdentity = autoIdentity;
        
        _routineConvention = routineConvention;
        _parameterConvention = parameterConvention;
        _enumConvention = enumConvention;

        _insert = insert;
        _update = update;
        _patch = patch;
        _deleteById = deleteById;
        _deleteMany = deleteMany;
        
        _selectById = selectById;
        _selectByIdExtended = selectByIdExtended;
        _selectByIdExtendedTranslated = selectByIdExtendedTranslated;
        _selectByIdTranslated = selectByIdTranslated;

        _selectMany = selectMany;
        _selectManyExtended = selectManyExtended;
        _selectManyExtendedTranslated = selectManyExtendedTranslated;
        _selectManyTranslated = selectManyTranslated;
        
        _selectOne = selectOne;
        _selectOneExtended = selectOneExtended;
        _selectOneExtendedTranslated = selectOneExtendedTranslated;
        _selectOneTranslated = selectOneTranslated;
    }

    private string? _connectionKey;
    
    private string? _schemaName;
    private Func<Type, string>? _resolveIdentityPropertyName;
    private bool? _autoIdentity;

    private DbConvention? _routineConvention;
    private DbConvention? _parameterConvention;
    private DbEnumConvention? _enumConvention;

    private DbRepositoryAction? _insert;
    private DbRepositoryAction? _update;
    private DbRepositoryAction? _patch;
    private DbRepositoryAction? _deleteById;
    private DbRepositoryAction? _deleteMany;
        
    private DbRepositoryAction? _selectById;
    private DbRepositoryAction? _selectByIdExtended;
        
    private DbRepositoryAction? _selectOne;
    private DbRepositoryAction? _selectOneExtended;
        
    private DbRepositoryAction? _selectMany;
    private DbRepositoryAction? _selectManyExtended;

    private DbRepositoryAction? _selectByIdTranslated;
    private DbRepositoryAction? _selectByIdExtendedTranslated;
        
    private DbRepositoryAction? _selectOneTranslated;
    private DbRepositoryAction? _selectOneExtendedTranslated;
        
    private DbRepositoryAction? _selectManyTranslated;
    private DbRepositoryAction? _selectManyExtendedTranslated;
    
    /// <summary>
    /// The database connection to be used by a repository.
    /// </summary>
    public string? ConnectionKey
    {
        get => _connectionKey ?? (this == Default ? null : Default.ConnectionKey);
        set => _connectionKey = value;
    }
    
    /// <summary>
    /// If supported by the underlying database, specifies the schema that contains the stored routines to be executed by the repository.
    /// </summary>
    public string SchemaName
    {
        get => _schemaName ?? (this == Default ? string.Empty : Default.SchemaName);
        set => _schemaName = value;
    }
    
    /// <summary>
    /// The function to invoke in order to resolve the identity property name of a given entity type.
    /// </summary>
    public Func<Type, string> ResolveIdentityPropertyName
    {
        get => _resolveIdentityPropertyName ?? (this == Default ? (_ => "Id" ) : Default.ResolveIdentityPropertyName);
        set => _resolveIdentityPropertyName = value;
    }

    /// <summary>
    /// Specifies whether or not the value for the identity property will be generated automatically. 
    /// </summary>
    public bool AutoIdentity
    {
        get => _autoIdentity ?? (this == Default || Default.AutoIdentity);
        set => _autoIdentity = value;
    }

    /// <summary>
    /// The conventions used to build names for stored routines.
    /// </summary>
    public DbConvention RoutineConvention
    {
        get => _routineConvention ?? (this == Default ? DbConvention.Default : Default.RoutineConvention);
        set => _routineConvention = value;
    }
    
    /// <summary>
    /// The conventions used to build names for database parameters.
    /// </summary>
    public DbConvention ParameterConvention
    {
        get => _parameterConvention ?? (this == Default ? DbConvention.Default : Default.ParameterConvention);
        set => _parameterConvention = value;
    }

    /// <summary>
    /// The convention used to send enum values to the underlying database.
    /// </summary>
    public DbEnumConvention EnumConvention
    {
        get => _enumConvention ?? (this == Default ? DbEnumConvention.Default : Default.EnumConvention);
        set => _enumConvention = value;
    }
    
    /// <summary>
    /// Action used to create a new entity.
    /// </summary>
    public DbRepositoryAction Insert
    {
        get => _insert ?? (this == Default ? DbRepositoryAction.Default.Insert : Default.Insert);
        set => _insert = value;
    }
    
    /// <summary>
    /// Action used to update an entity.
    /// </summary>
    public DbRepositoryAction Update
    {
        get => _update ?? (this == Default ? DbRepositoryAction.Default.Update : Default.Update);
        set => _update = value;
    }
    
    /// <summary>
    /// Action used to patch an entity.
    /// </summary>
    public DbRepositoryAction Patch
    {
        get => _patch ?? (this == Default ? DbRepositoryAction.Default.Patch : Default.Patch);
        set => _patch = value;
    }
    
    /// <summary>
    /// Action used to delete an entity.
    /// </summary>
    public DbRepositoryAction DeleteById
    {
        get => _deleteById ?? (this == Default ? DbRepositoryAction.Default.DeleteById : Default.DeleteById);
        set => _deleteById = value;
    }
    
    /// <summary>
    /// Action used to delete multiple entities at once.
    /// </summary>
    public DbRepositoryAction DeleteMany
    {
        get => _deleteMany ?? (this == Default ? DbRepositoryAction.Default.DeleteMany : Default.DeleteMany);
        set => _deleteMany = value;
    }

    /// <summary>
    /// Action used to get an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction SelectById
    {
        get => _selectById ?? (this == Default ? DbRepositoryAction.Default.SelectById : Default.SelectById);
        set => _selectById = value;
    }
    
    /// <summary>
    /// Action used to get the extended version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction SelectByIdExtended
    {
        get => _selectByIdExtended ?? (this == Default ? DbRepositoryAction.Default.SelectByIdExtended : Default.SelectByIdExtended);
        set => _selectByIdExtended = value;
    }
    
    /// <summary>
    /// Action used to get the extended and translated version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction SelectByIdExtendedTranslated
    {
        get => _selectByIdExtendedTranslated ?? (this == Default ? DbRepositoryAction.Default.SelectByIdExtendedTranslated : Default.SelectByIdExtendedTranslated);
        set => _selectByIdExtendedTranslated = value;
    }
    
    /// <summary>
    /// Action used to get the translated version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction SelectByIdTranslated
    {
        get => _selectByIdTranslated ?? (this == Default ? DbRepositoryAction.Default.SelectByIdTranslated : Default.SelectByIdTranslated);
        set => _selectByIdTranslated = value;
    }

    /// <summary>
    /// Action used to get one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction SelectMany
    {
        get => _selectMany ?? (this == Default ? DbRepositoryAction.Default.SelectMany : Default.SelectMany);
        set => _selectMany = value;
    }
    
    /// <summary>
    /// Action used to get the extended version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction SelectManyExtended
    {
        get => _selectManyExtended ?? (this == Default ? DbRepositoryAction.Default.SelectManyExtended : Default.SelectManyExtended);
        set => _selectManyExtended = value;
    }
    
    /// <summary>
    /// Action used to get the extended and translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction SelectManyExtendedTranslated
    {
        get => _selectManyExtendedTranslated ?? (this == Default ? DbRepositoryAction.Default.SelectManyExtendedTranslated : Default.SelectManyExtendedTranslated);
        set => _selectManyExtendedTranslated = value;
    }
    
    /// <summary>
    /// Action used to get the translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction SelectManyTranslated
    {
        get => _selectManyTranslated ?? (this == Default ? DbRepositoryAction.Default.SelectManyTranslated : Default.SelectManyTranslated);
        set => _selectManyTranslated = value;
    }

    /// <summary>
    /// Action used to get a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction SelectOne
    {
        get => _selectOne ?? (this == Default ? DbRepositoryAction.Default.SelectOne : Default.SelectOne);
        set => _selectOne = value;
    }
    
    /// <summary>
    /// Action used to get the extended version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction SelectOneExtended
    {
        get => _selectOneExtended ?? (this == Default ? DbRepositoryAction.Default.SelectOneExtended : Default.SelectOneExtended);
        set => _selectOneExtended = value;
    }
    
    /// <summary>
    /// Action used to get the extended and translated version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction SelectOneExtendedTranslated
    {
        get => _selectOneExtendedTranslated ?? (this == Default ? DbRepositoryAction.Default.SelectOneExtendedTranslated : Default.SelectOneExtendedTranslated);
        set => _selectOneExtendedTranslated = value;
    }
    
    /// <summary>
    /// Action used to get the translated version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction SelectOneTranslated
    {
        get => _selectOneTranslated ?? (this == Default ? DbRepositoryAction.Default.SelectOneTranslated : Default.SelectOneTranslated);
        set => _selectOneTranslated = value;
    }
}