namespace Flowsy.Repository.Sql;

/// <summary>
/// Enables customization of the behavior of repositories.
/// </summary>
public class DbRepositoryConfiguration
{
    /// <summary>
    /// Creates a new DbRepositoryConfigurationBuilder to set repository configurations 
    /// </summary>
    /// <returns></returns>
    public static DbRepositoryConfigurationBuilder Build() => new ();
    
    /// <summary>
    /// Holds the default configuration for repositories.
    /// </summary>
    internal static DbRepositoryConfiguration Default { get; set; } = new();
    
    /// <summary>
    /// Holds configurations for specific repository types.
    /// </summary>
    internal static IDictionary<Type, DbRepositoryConfiguration> Configurations { get; private set; } = new Dictionary<Type, DbRepositoryConfiguration>();

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
    /// <param name="actions">The action set used to perform actions on the underlying database.</param>
    public DbRepositoryConfiguration(
        string? connectionKey = default, 
        
        string? schemaName = default,
        Func<Type, string>? resolveIdentityPropertyName = default,
        bool? autoIdentity = default,
        
        DbRoutineConvention? routineConvention = default,
        DbRoutineParameterConvention? parameterConvention = default,
        DbDateTimeOffsetFormat? dateTimeOffsetFormat = default,
        DbEnumConvention? enumConvention = default,
        DbRepositoryActionSet? actions = default
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
        _actions = actions;
    }

    private string? _connectionKey;
    
    private string? _schemaName;
    private Func<Type, string>? _resolveIdentityPropertyName;
    private bool? _autoIdentity;

    private DbRoutineConvention? _routineConvention;
    private DbRoutineParameterConvention? _parameterConvention;
    private DbDateTimeOffsetFormat? _dateTimeOffsetFormat;
    private DbEnumConvention? _enumConvention;
    private DbRepositoryActionSet? _actions;

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
    public DbRoutineConvention RoutineConvention
    {
        get => _routineConvention ?? (this == Default ? DbRoutineConvention.Default : Default.RoutineConvention);
        set => _routineConvention = value;
    }
    
    /// <summary>
    /// The conventions used to build names for database parameters.
    /// </summary>
    public DbRoutineParameterConvention ParameterConvention
    {
        get => _parameterConvention ?? (this == Default ? DbRoutineParameterConvention.Default : Default.ParameterConvention);
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
    /// The set of conventions used to perform actions on the underlying database.
    /// </summary>
    public DbRepositoryActionSet Actions
    {
        get => _actions ?? (this == Default ? DbRepositoryActionSet.Default : Default.Actions);
        set => _actions = value;
    }

    /// <summary>
    /// Casts the DbRepositoryConfiguration.Actions property to a type inherited from DbRepositoryActionSet.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T? CastActions<T>() where T : DbRepositoryActionSet => Actions as T;
}