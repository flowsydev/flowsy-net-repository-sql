using System.Data;
using Dapper;
using Flowsy.Core;
using Flowsy.Localization;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

/// <summary>
/// Provides the implementation of a repository which data store is a SQL database.
/// </summary>
/// <typeparam name="TEntity">The type of entity handled by the repository.</typeparam>
/// <typeparam name="TIdentity">The type of the property that uniquely identifies each entity of the repository.</typeparam>
public abstract partial class DbRepository<TEntity, TIdentity> : AbstractRepository<TEntity, TIdentity> where TEntity : class, IEntity
{
    /// <summary>
    /// Creates a new instance of the repository.
    /// </summary>
    /// <param name="connectionFactory">The factory to create database connections.</param>
    /// <param name="exceptionHandler">An optional exception handler</param>
    protected DbRepository(IDbConnectionFactory connectionFactory, IExceptionHandler? exceptionHandler = null) : base(exceptionHandler)
    {
        ConnectionFactory = connectionFactory;
    }

    /// <summary>
    /// Creates a new instance of the repository.
    /// </summary>
    /// <param name="transaction">The transaction for the repository to participate in.</param>
    /// <param name="exceptionHandler">An optional exception handler</param>
    protected DbRepository(IDbTransaction transaction, IExceptionHandler? exceptionHandler = null) : base(exceptionHandler)
    {
        Transaction = transaction;
    }

    /// <summary>
    /// The connection factory used to obtain a database connection if the Transaction property is not set.
    /// </summary>
    protected IDbConnectionFactory? ConnectionFactory { get; }
    
    /// <summary>
    /// If set, the Transaction property will be the first choice to obtain a connection to allow this repository to participate in a transaction with other repositories.
    /// If not set, the ConnectionFactory property will be used to obtain a database connection. 
    /// </summary>
    protected IDbTransaction? Transaction { get; }

    /// <summary>
    /// Indicates if the repository is participating in a transaction.
    /// </summary>
    protected bool InTransaction => Transaction is not null;
    
    /// <summary>
    /// Repository configuration with conventions used when executing queries against de underlying data store.
    /// DbRepositoryConfiguration allows to set a default configuration if none is set for a specific repository type.
    /// </summary>
    protected virtual DbRepositoryConfiguration Configuration
        => DbRepositoryConfiguration.Resolve(GetType());

    /// <summary>
    /// Obtains a database connection from the current transaction or the specified connection factory.
    /// </summary>
    /// <returns>A database connection</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the repository was not able to obtain a connection from the current transaction or connection factory.
    /// </exception>
    protected IDbConnection GetConnection()
        => Transaction?.Connection 
           ?? ConnectionFactory?.GetConnection(Configuration.ConnectionKey) 
           ?? throw new InvalidOperationException("CouldNotGetValidConnection".Localize());
    
    /// <summary>
    /// The name of the property that uniquely identifies an entity of the repository. 
    /// </summary>
    public override string IdentityPropertyName => Configuration.ResolveIdentityPropertyName(typeof(TEntity));
    
    /// <summary>
    /// Resolves a stored routine name by applying the schema name, prefix, naming convention and suffix configured for the repository.  
    /// </summary>
    /// <param name="simpleName">A simple name like UserCreate or CustomerGetMany.</param>
    /// <returns>A full routine name like public.fn_user_create or public.fn_customer_get_many.</returns>
    protected virtual string ResolveRoutineName(string simpleName)
    {
        var schemaPrefix = string.IsNullOrEmpty(Configuration.SchemaName) ? string.Empty : $"{Configuration.SchemaName}.";
        var routineName = $"{Configuration.RoutineConvention.Prefix}{simpleName.ApplyNamingConvention(Configuration.RoutineConvention.Naming)}{Configuration.RoutineConvention.Suffix}";

        return $"{schemaPrefix}{routineName}";
    }

    /// <summary>
    /// Resolves the statement required to call a stored procedure or stored function based on the given routine type or the routine type configured for the repository.
    /// </summary>
    /// <param name="routineSimpleName">A simple name like UserCreate or CustomerGetMany.</param>
    /// <param name="param">The routine parameters.</param>
    /// <param name="routineType">The routine type (stored procedure/stored function)</param>
    /// <returns>A value like public.sp_user_create for stored procedures or select * from public.fn_customer_get_many(@p_city_name) for stored functions.</returns>
    protected virtual string ResolveRoutineStatement(string routineSimpleName, DynamicParameters param, DbRoutineType? routineType = null)
    {
        var routineName = ResolveRoutineName(routineSimpleName);
        var finalRoutineType = routineType ?? Configuration.RoutineConvention.RoutineType;
        
        if ((finalRoutineType) != DbRoutineType.StoredFunction)
            return routineName;
        
        var parameterNames = string.Join(
            ", ",
            param.ParameterNames.Select(
                parameterName => Configuration.ParameterConvention.ResolvePlaceholder(
                    routineName,
                    parameterName,
                    param.Get<object?>(parameterName),
                    finalRoutineType
                    )
                )
            );
        return $"select * from {routineName}({parameterNames})";
    }

    /// <summary>
    /// Resolves the proper command type for the given routine type or the routine type configured for the repository.
    /// </summary>
    /// <param name="routineType">The routine type (stored procedure/stored function)</param>
    /// <returns>CommandType.StoredProcedure/CommandType.Text</returns>
    protected virtual CommandType ResolveRoutineCommandType(DbRoutineType? routineType = null)
        => (routineType ?? Configuration.RoutineConvention.RoutineType) switch
        {
            DbRoutineType.StoredFunction => CommandType.Text,
            _ => CommandType.StoredProcedure
        };

    /// <summary>
    /// Resolves the CommandDefinition for the routine
    /// </summary>
    /// <param name="routineSimpleName">A simple name like UserCreate or CustomerGetMany.</param>
    /// <param name="param">The parameters for the routine.</param>
    /// <param name="routineType">The routine type (stored procedure/stored function)</param>
    /// <param name="cancellationToken">The cancellation token for the operation.</param>
    /// <returns></returns>
    protected virtual CommandDefinition ResolveRoutineCommandDefinition(
        string routineSimpleName,
        DynamicParameters param,
        DbRoutineType? routineType = null,
        CancellationToken? cancellationToken = null
        )
        => new (
            ResolveRoutineStatement(routineSimpleName, param, routineType),
            param,
            commandType: ResolveRoutineCommandType(routineType),
            transaction: Transaction,
            cancellationToken: cancellationToken ?? CancellationToken.None
            );

    /// <summary>
    /// Resolves a parameter name by applying the prefix, naming convention and suffix configured for the repository.
    /// </summary>
    /// <param name="simpleName">A simple name like UserId or EmailAddress.</param>
    /// <returns>A full parameter name like p_user_id or p_email_address.</returns>
    protected virtual string ResolveRoutineParameterName(string simpleName) =>
        $"{Configuration.ParameterConvention.Prefix}{simpleName.ApplyNamingConvention(Configuration.ParameterConvention.Naming)}{Configuration.ParameterConvention.Suffix}";
    
    /// <summary>
    /// Creates a DynamicParameters instance from the properties of the given object.
    /// </summary>
    /// <param name="obj">The object to read the properties from.</param>
    /// <returns></returns>
    protected virtual DynamicParameters ToDynamicParameters(object obj)
        => ToDynamicParameters(obj.ToReadonlyDictionary());
    
    /// <summary>
    /// Creates a DynamicParameters instance from the values of the given dictionary.
    /// </summary>
    /// <param name="properties">The property names and values of an object.</param>
    /// <returns></returns>
    protected virtual DynamicParameters ToDynamicParameters(IReadOnlyDictionary<string, object?> properties)
    {
        var parameters = new DynamicParameters();

        foreach (var (key, value) in properties)
        {
            var parameter = BuildParameter(key, value);
            parameters.Add(parameter.Name, parameter.Value, parameter.Type, parameter.Direction, parameter.Size);
        }
        
        return parameters;
    }
    
    /// <summary>
    /// Builds a database parameter from a property name and value read from an object.
    /// </summary>
    /// <param name="sourcePropertyName">The name of property.</param>
    /// <param name="value">The value of the property.</param>
    /// <returns>An instance of DbParameterInfo</returns>
    protected virtual DbParameterInfo BuildParameter(string sourcePropertyName, object? value)
    {
        var parameterName = ResolveRoutineParameterName(sourcePropertyName);
        
        if (value is null)
            return new DbParameterInfo(parameterName, null, null, null, null);

        return value switch
        {
            DateTime => new DbParameterInfo(parameterName, DbType.DateTime2, null, null, value),
            DateTimeOffset dateTimeOffset => new DbParameterInfo(parameterName, DbType.DateTimeOffset, null, null, Configuration.DateTimeOffsetFormat == DbDateTimeOffsetFormat.Utc ? dateTimeOffset.UtcDateTime : dateTimeOffset.LocalDateTime),
            DateOnly => new DbParameterInfo(parameterName, DbType.Date, null, null, value),
            Enum e => new DbParameterInfo(parameterName, Configuration.EnumConvention.Format == DbEnumFormat.Ordinal ? ResolveEnumOrdinalType(e) : DbType.String, null, null, ResolveEnumValue(e)),
            IEnumerable<sbyte> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<byte> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<short> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<int> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<long> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<float> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<double> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<decimal> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<bool> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            IEnumerable<string> enumerable => new DbParameterInfo(parameterName, null, null, null, enumerable.ToArray()),
            _ => new DbParameterInfo(parameterName, null, null, null, value)
        };
    }

    /// <summary>
    /// Resolves the final value for an enum sent to the underlying database. 
    /// </summary>
    /// <param name="e">The enum value</param>
    /// <returns>The final value sent to the underlying database.</returns>
    protected virtual object ResolveEnumValue(Enum e)
    {
        var enumConvention = Configuration.EnumConvention;
        return enumConvention.Format == DbEnumFormat.Name
            ? $"{enumConvention.Prefix}{e.ToString()?.ApplyNamingConvention(enumConvention.Naming) ?? ResolveEnumOrdinalValue(e)}{enumConvention.Suffix}"
            : ResolveEnumOrdinalValue(e);
    }

    /// <summary>
    /// Resolves the ordinal value for an enum sent to the underlying database.
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    protected virtual object ResolveEnumOrdinalValue(Enum e)
        => e.GetTypeCode() switch
        {
            TypeCode.Byte => Convert.ToByte(e),
            TypeCode.Int16 => Convert.ToInt16(e),
            TypeCode.Int64 => Convert.ToInt64(e),
            _ => Convert.ToInt32(e)
        };
    
    /// <summary>
    /// Resolves the type for an enum sent to the underlying database.
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    protected virtual DbType ResolveEnumOrdinalType(Enum e)
        => e.GetTypeCode() switch
        {
            TypeCode.Byte => DbType.Byte,
            TypeCode.Int16 => DbType.Int16,
            TypeCode.Int64 => DbType.Int64,
            _ => DbType.Int32
        };

    /// <summary>
    /// Executes a query against the underlying data store expecting to get a list of entities.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A list of entities.</returns>
    protected virtual Task<IEnumerable<T>> QueryAsync<T>(
        string sql,
        CommandType commandType,
        CancellationToken cancellationToken
        ) =>
        QueryAsync<T>(sql, null as DynamicParameters, commandType, cancellationToken);

    /// <summary>
    /// Executes a query against the underlying data store expecting to get a list of entities.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="param">An object with properties used to generate the parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A list of entities.</returns>
    protected virtual Task<IEnumerable<T>> QueryAsync<T>(
        string sql,
        dynamic param,
        CommandType commandType,
        CancellationToken cancellationToken
        ) => 
        QueryAsync<T>(
            sql,
            ((object) param).ToReadonlyDictionary(),
            commandType,
            cancellationToken
        );
    
    /// <summary>
    /// Executes a query against the underlying data store expecting to get a list of entities.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="properties">The property names and values of an object to generate the parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A list of entities.</returns>
    protected virtual Task<IEnumerable<T>> QueryAsync<T>(
        string sql,
        IReadOnlyDictionary<string, object?> properties,
        CommandType commandType,
        CancellationToken cancellationToken
        ) =>
        QueryAsync<T>(
            sql,
            ToDynamicParameters(properties),
            commandType,
            cancellationToken
        );

    /// <summary>
    /// Executes a query against the underlying data store expecting to get a list of entities.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="param">The parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A list of entities.</returns>
    protected virtual async Task<IEnumerable<T>> QueryAsync<T>(
        string sql,
        DynamicParameters? param,
        CommandType commandType,
        CancellationToken cancellationToken
        )
    {
        IDbConnection? connection = null;
        try
        {
            connection = GetConnection();
            return await connection.QueryAsync<T>(new CommandDefinition(
                sql,
                param,
                commandType: commandType,
                transaction: Transaction,
                cancellationToken: cancellationToken
                ));
        }
        catch(Exception exception)
        {
            if (connection is not null && !InTransaction)
                connection.Dispose();

            if (ExceptionHandler is null)
                throw;
            
            throw ExceptionHandler.Translate(exception, new DbExecutionContext(this, sql, param, commandType, Transaction));
        }
    }

    /// <summary>
    /// Executes a query against the underlying data store expecting to get a single entity.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A single entity or a null value if not found.</returns>
    protected virtual Task<T?> QueryFirstOrDefaultAsync<T>(
        string sql,
        CommandType commandType,
        CancellationToken cancellationToken
        )
        => QueryFirstOrDefaultAsync<T>(sql, null as DynamicParameters, commandType, cancellationToken);

    /// <summary>
    /// Executes a query against the underlying data store expecting to get a single entity.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="param">An object with properties used to generate the parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A single entity or a null value if not found.</returns>
    protected virtual Task<T?> QueryFirstOrDefaultAsync<T>(
        string sql,
        dynamic param,
        CommandType commandType,
        CancellationToken cancellationToken
        ) =>
        QueryFirstOrDefaultAsync<T>(
            sql,
            ((object) param).ToReadonlyDictionary(),
            commandType,
            cancellationToken
        );

    /// <summary>
    /// Executes a query against the underlying data store expecting to get a single entity.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="properties">The property names and values of an object to generate the parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A single entity or a null value if not found.</returns>
    protected virtual Task<T?> QueryFirstOrDefaultAsync<T>(
        string sql,
        IReadOnlyDictionary<string, object?> properties,
        CommandType commandType,
        CancellationToken cancellationToken
        ) =>
        QueryFirstOrDefaultAsync<T>(
            sql,
            ToDynamicParameters(properties),
            commandType,
            cancellationToken
        );

    
    /// <summary>
    /// Executes a query against the underlying data store expecting to get a single entity.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="param">The parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <typeparam name="T">The type of entity to return.</typeparam>
    /// <returns>A single entity or a null value if not found.</returns>
    protected virtual async Task<T?> QueryFirstOrDefaultAsync<T>(
        string sql,
        DynamicParameters? param,
        CommandType commandType,
        CancellationToken cancellationToken
    )
    {
        IDbConnection? connection = null;
        try
        {
            connection = GetConnection();
            
            return await connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(
                sql,
                param,
                commandType: commandType,
                transaction: Transaction,
                cancellationToken: cancellationToken
                ));
        }
        catch (Exception exception)
        {
            if (connection is not null && !InTransaction)
                connection.Dispose();
            
            if (ExceptionHandler is null)
                throw;
            
            throw ExceptionHandler.Translate(exception, new DbExecutionContext(this, sql, param, commandType, Transaction));
        }
    }

    /// <summary>
    /// Executes a query against the underlying data store and returns the number of affected entities.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <returns>The number of affected entities.</returns>
    protected virtual Task<int> ExecuteAsync(
        string sql,
        CommandType commandType,
        CancellationToken cancellationToken
        ) =>
        ExecuteAsync(sql, null as DynamicParameters, commandType, cancellationToken);

    /// <summary>
    /// Executes a query against the underlying data store and returns the number of affected entities.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="param">An object with properties used to generate the parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <returns>The number of affected entities.</returns>
    protected virtual Task<int> ExecuteAsync(
        string sql,
        dynamic param,
        CommandType commandType,
        CancellationToken cancellationToken
        ) =>
        ExecuteAsync(
            sql,
            ((object) param).ToReadonlyDictionary(),
            commandType,
            cancellationToken
        );

    /// <summary>
    /// Executes a query against the underlying data store and returns the number of affected entities.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="properties">The property names and values of an object to generate the parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <returns>The number of affected entities.</returns>
    protected virtual Task<int> ExecuteAsync(
        string sql,
        IReadOnlyDictionary<string, object?> properties,
        CommandType commandType,
        CancellationToken cancellationToken
        ) =>
        ExecuteAsync(
            sql,
            ToDynamicParameters(properties),
            commandType,
            cancellationToken
        );


    /// <summary>
    /// Executes a query against the underlying data store and returns the number of affected entities.
    /// </summary>
    /// <param name="sql">The SQL statement or stored routine name.</param>
    /// <param name="param">The parameters for the query.</param>
    /// <param name="commandType">The command type.</param>
    /// <param name="cancellationToken">The cancellation token for the query.</param>
    /// <returns>The number of affected entities.</returns>
    protected virtual async Task<int> ExecuteAsync(
        string sql,
        DynamicParameters? param,
        CommandType commandType,
        CancellationToken cancellationToken
        )
    {
        IDbConnection? connection = null;
        try
        {
            connection = GetConnection();
            return await connection.ExecuteAsync(new CommandDefinition(
                sql,
                param,
                commandType: commandType,
                transaction: Transaction,
                cancellationToken: cancellationToken
                ));
        }
        catch (Exception exception)
        {
            if (connection is not null && !InTransaction)
                connection.Dispose();

            if (ExceptionHandler is null)
                throw;
            
            throw ExceptionHandler.Translate(exception, new DbExecutionContext(this, sql, param, commandType, Transaction));
        }
    }
}