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
    /// <param name="dateTimeOffsetFormat">The format used for DateTimeOffset values when resolving database parameters.</param>
    /// <param name="enumConvention">The conventions used to send enum values to the underlying database.</param>
    /// <param name="create">Action used to create a new entity.</param>
    /// <param name="update">Action used to update an entity.</param>
    /// <param name="patch">Action used to patch an entity.</param>
    /// <param name="deleteById">Action used to delete an entity.Action used to patch an entity.</param>
    /// <param name="deleteMany">Action used to delete multiple entities at once.</param>
    /// <param name="getById">Action used to get an entity identified by a given value.</param>
    /// <param name="getByIdExtended">Action used to get the extended version of an entity identified by a given value.</param>
    /// <param name="getByIdExtendedTranslated">Action used to get the extended and translated version of an entity identified by a given value.</param>
    /// <param name="getByIdTranslated">Action used to get the translated version of an entity identified by a given value.</param>
    /// <param name="getOne">Action used to get a single entity matching a specified filter.</param>
    /// <param name="getOneExtended">Action used to get the extended version of a single entity matching a specified filter.</param>
    /// <param name="getOneExtendedTranslated">Action used to get the extended and translated version of a single entity matching a specified filter.</param>
    /// <param name="getOneTranslated">Action used to get the translated version of a single entity matching a specified filter.</param>
    /// <param name="getMany">Action used to get one or more entities matching a specified filter.</param>
    /// <param name="getManyPaged">Action used to get a page of one or more entities matching a specified filter.</param>
    /// <param name="getManyExtended">Action used to get the extended version of one or more entities matching a specified filter.</param>
    /// <param name="getManyExtendedPaged">Action used to get a page of the extended version of one or more entities matching a specified filter.</param>
    /// <param name="getManyExtendedTranslated">Action used to get the extended and translated version of one or more entities matching a specified filter.</param>
    /// <param name="getManyExtendedTranslatedPaged">Action used to get a page of the extended and translated version of one or more entities matching a specified filter.</param>
    /// <param name="getManyTranslated">Action used to get the translated version of one or more entities matching a specified filter.</param>
    /// <param name="getManyTranslatedPaged">Action used to get a page of the translated version of one or more entities matching a specified filter.</param>
    public DbRepositoryConfiguration(
        string? connectionKey = default, 
        
        string? schemaName = default,
        Func<Type, string>? resolveIdentityPropertyName = default,
        bool? autoIdentity = default,
        
        DbConvention? routineConvention = default,
        DbConvention? parameterConvention = default,
        DbDateTimeOffsetFormat? dateTimeOffsetFormat = default,
        DbEnumConvention? enumConvention = default,

        DbRepositoryAction? create = default,
        DbRepositoryAction? update = default,
        DbRepositoryAction? patch = default,
        DbRepositoryAction? deleteById = default,
        DbRepositoryAction? deleteMany = default,
        
        DbRepositoryAction? getById = default,
        DbRepositoryAction? getByIdExtended = default,
        DbRepositoryAction? getByIdExtendedTranslated = default,
        DbRepositoryAction? getByIdTranslated = default,
        
        DbRepositoryAction? getOne = default,
        DbRepositoryAction? getOneExtended = default,
        DbRepositoryAction? getOneExtendedTranslated = default,
        DbRepositoryAction? getOneTranslated = default,
        
        DbRepositoryAction? getMany = default,
        DbRepositoryAction? getManyPaged = default,
        DbRepositoryAction? getManyExtended = default,
        DbRepositoryAction? getManyExtendedPaged = default,
        DbRepositoryAction? getManyExtendedTranslated = default,
        DbRepositoryAction? getManyExtendedTranslatedPaged = default,
        DbRepositoryAction? getManyTranslated = default,
        DbRepositoryAction? getManyTranslatedPaged = default
    )
    {
        _connectionKey = connectionKey;
        
        _schemaName = schemaName;
        _resolveIdentityPropertyName = resolveIdentityPropertyName;
        _autoIdentity = autoIdentity;
        
        _routineConvention = routineConvention;
        _parameterConvention = parameterConvention;
        _dateTimeOffsetFormat = dateTimeOffsetFormat;
        _enumConvention = enumConvention;

        _create = create;
        _update = update;
        _patch = patch;
        _deleteById = deleteById;
        _deleteMany = deleteMany;
        
        _getById = getById;
        _getByIdExtended = getByIdExtended;
        _getByIdExtendedTranslated = getByIdExtendedTranslated;
        _getByIdTranslated = getByIdTranslated;
        
        _getOne = getOne;
        _getOneExtended = getOneExtended;
        _getOneExtendedTranslated = getOneExtendedTranslated;
        _getOneTranslated = getOneTranslated;

        _getMany = getMany;
        _getManyPaged = getManyPaged;
        _getManyExtended = getManyExtended;
        _getManyExtendedPaged = getManyExtendedPaged;
        _getManyExtendedTranslated = getManyExtendedTranslated;
        _getManyExtendedTranslatedPaged = getManyExtendedTranslatedPaged;
        _getManyTranslated = getManyTranslated;
        _getManyTranslatedPaged = getManyTranslatedPaged;
    }

    private string? _connectionKey;
    
    private string? _schemaName;
    private Func<Type, string>? _resolveIdentityPropertyName;
    private bool? _autoIdentity;

    private DbConvention? _routineConvention;
    private DbConvention? _parameterConvention;
    private DbDateTimeOffsetFormat? _dateTimeOffsetFormat;
    private DbEnumConvention? _enumConvention;

    private DbRepositoryAction? _create;
    private DbRepositoryAction? _update;
    private DbRepositoryAction? _patch;
    private DbRepositoryAction? _deleteById;
    private DbRepositoryAction? _deleteMany;
        
    private DbRepositoryAction? _getById;
    private DbRepositoryAction? _getByIdExtendedTranslated;
    private DbRepositoryAction? _getByIdExtended;
    private DbRepositoryAction? _getByIdTranslated;

    private DbRepositoryAction? _getOne;
    private DbRepositoryAction? _getOneExtended;
    private DbRepositoryAction? _getOneExtendedTranslated;
    private DbRepositoryAction? _getOneTranslated;

    private DbRepositoryAction? _getMany;
    private DbRepositoryAction? _getManyPaged;
    private DbRepositoryAction? _getManyExtended;
    private DbRepositoryAction? _getManyExtendedPaged;
    private DbRepositoryAction? _getManyExtendedTranslated;
    private DbRepositoryAction? _getManyExtendedTranslatedPaged;
    private DbRepositoryAction? _getManyTranslated;
    private DbRepositoryAction? _getManyTranslatedPaged;

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
    /// The format used for DateTimeOffset values when resolving database parameters.
    /// </summary>
    public DbDateTimeOffsetFormat DateTimeOffsetFormat
    {
        get => _dateTimeOffsetFormat ?? (this == Default ? DbDateTimeOffsetFormat.Utc : Default.DateTimeOffsetFormat);
        set => _dateTimeOffsetFormat = value;
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
    public DbRepositoryAction Create
    {
        get => _create ?? (this == Default ? DbRepositoryAction.Default.Create : Default.Create);
        set => _create = value;
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
    public DbRepositoryAction GetById
    {
        get => _getById ?? (this == Default ? DbRepositoryAction.Default.GetById : Default.GetById);
        set => _getById = value;
    }
    
    /// <summary>
    /// Action used to get the extended version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdExtended
    {
        get => _getByIdExtended ?? (this == Default ? DbRepositoryAction.Default.GetByIdExtended : Default.GetByIdExtended);
        set => _getByIdExtended = value;
    }
    
    /// <summary>
    /// Action used to get the extended and translated version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdExtendedTranslated
    {
        get => _getByIdExtendedTranslated ?? (this == Default ? DbRepositoryAction.Default.GetByIdExtendedTranslated : Default.GetByIdExtendedTranslated);
        set => _getByIdExtendedTranslated = value;
    }
    
    /// <summary>
    /// Action used to get the translated version of an entity identified by a given value.
    /// </summary>
    public DbRepositoryAction GetByIdTranslated
    {
        get => _getByIdTranslated ?? (this == Default ? DbRepositoryAction.Default.GetByIdTranslated : Default.GetByIdTranslated);
        set => _getByIdTranslated = value;
    }
    
    /// <summary>
    /// Action used to get a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOne
    {
        get => _getOne ?? (this == Default ? DbRepositoryAction.Default.GetOne : Default.GetOne);
        set => _getOne = value;
    }
    
    /// <summary>
    /// Action used to get the extended version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneExtended
    {
        get => _getOneExtended ?? (this == Default ? DbRepositoryAction.Default.GetOneExtended : Default.GetOneExtended);
        set => _getOneExtended = value;
    }
    
    /// <summary>
    /// Action used to get the extended and translated version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneExtendedTranslated
    {
        get => _getOneExtendedTranslated ?? (this == Default ? DbRepositoryAction.Default.GetOneExtendedTranslated : Default.GetOneExtendedTranslated);
        set => _getOneExtendedTranslated = value;
    }
    
    /// <summary>
    /// Action used to get the translated version of a single entity matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetOneTranslated
    {
        get => _getOneTranslated ?? (this == Default ? DbRepositoryAction.Default.GetOneTranslated : Default.GetOneTranslated);
        set => _getOneTranslated = value;
    }

    /// <summary>
    /// Action used to get one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetMany
    {
        get => _getMany ?? (this == Default ? DbRepositoryAction.Default.GetMany : Default.GetMany);
        set => _getMany = value;
    }

    /// <summary>
    /// Action used to get a page of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyPaged
    {
        get => _getManyPaged ?? (this == Default ? DbRepositoryAction.Default.GetManyPaged : Default.GetManyPaged);
        set => _getManyPaged = value;
    }
    
    /// <summary>
    /// Action used to get the extended version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtended
    {
        get => _getManyExtended ?? (this == Default ? DbRepositoryAction.Default.GetManyExtended : Default.GetManyExtended);
        set => _getManyExtended = value;
    }
    
    /// <summary>
    /// Action used to get a page of the extended version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedPaged
    {
        get => _getManyExtendedPaged ?? (this == Default ? DbRepositoryAction.Default.GetManyExtendedPaged : Default.GetManyExtendedPaged);
        set => _getManyExtendedPaged = value;
    }
    
    /// <summary>
    /// Action used to get the extended and translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedTranslated
    {
        get => _getManyExtendedTranslated ?? (this == Default ? DbRepositoryAction.Default.GetManyExtendedTranslated : Default.GetManyExtendedTranslated);
        set => _getManyExtendedTranslated = value;
    }
    
    /// <summary>
    /// Action used to get a page of the extended and translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyExtendedTranslatedPaged
    {
        get => _getManyExtendedTranslatedPaged ?? (this == Default ? DbRepositoryAction.Default.GetManyExtendedTranslatedPaged : Default.GetManyExtendedTranslatedPaged);
        set => _getManyExtendedTranslatedPaged = value;
    }
    
    /// <summary>
    /// Action used to get the translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyTranslated
    {
        get => _getManyTranslated ?? (this == Default ? DbRepositoryAction.Default.GetManyTranslated : Default.GetManyTranslated);
        set => _getManyTranslated = value;
    }
    
    /// <summary>
    /// Action used to get a page of the translated version of one or more entities matching a specified filter.
    /// </summary>
    public DbRepositoryAction GetManyTranslatedPaged
    {
        get => _getManyTranslatedPaged ?? (this == Default ? DbRepositoryAction.Default.GetManyTranslatedPaged : Default.GetManyTranslatedPaged);
        set => _getManyTranslatedPaged = value;
    }
}