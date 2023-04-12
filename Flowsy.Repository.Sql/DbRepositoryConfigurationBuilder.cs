using System.Data;
using System.Reflection;
using Dapper;
using Flowsy.Core;

namespace Flowsy.Repository.Sql;

public class DbRepositoryConfigurationBuilder
{
    internal DbRepositoryConfigurationBuilder()
    {
    }

    public DbRepositoryConfigurationBuilder Default(DbRepositoryConfiguration configuration)
    {
        DbRepositoryConfiguration.Default = configuration;
        return this;
    }
    
    /// <summary>
    /// Registers a configuration associated to a specific repository type.
    /// </summary>
    /// <param name="repositoryType">The repository type.</param>
    /// <param name="configuration">The repository configuration.</param>
    public DbRepositoryConfigurationBuilder ForType(Type repositoryType, DbRepositoryConfiguration configuration)
    {
        DbRepositoryConfiguration.ForType(repositoryType, configuration);
        return this;
    }

    public DbRepositoryConfigurationBuilder WithColumnMapping(NamingConvention columnNamingConvention, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
            assembly.RegisterDatabaseColumnMapping(columnNamingConvention);
        
        return this;
    }

    public DbRepositoryConfigurationBuilder WithColumnMapping(Func<Type, string, PropertyInfo?> mapColumn, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
            assembly.RegisterDatabaseColumnMapping(mapColumn);
        
        return this;
    }
    
    public DbRepositoryConfigurationBuilder WithDateOnlyTypeHandlers(
        DbType parameterType = DbType.Date,
        string parameterFormat = "yyyy-MM-dd",
        IEnumerable<string>? parsingFormats = null
        )
    {
        var formats = parsingFormats?.ToArray();
        SqlMapper.AddTypeHandler(new DbDateOnlyTypeHandler(parameterType, parameterFormat, formats));
        SqlMapper.AddTypeHandler(new DbDateOnlyNullableTypeHandler(parameterType, parameterFormat, formats));
        return this;
    }
    
    public DbRepositoryConfigurationBuilder WithTimeOnlyTypeHandlers(
        DbType parameterType = DbType.Time,
        string parameterFormat = "H:mm:ss.ffffff",
        IEnumerable<string>? parsingFormats = null
        )
    {
        var formats = parsingFormats?.ToArray();
        SqlMapper.AddTypeHandler(new DbTimeOnlyTypeHandler(parameterType, parameterFormat, formats));
        SqlMapper.AddTypeHandler(new DbTimeOnlyNullableTypeHandler(parameterType, parameterFormat, formats));
        return this;
    }

    public DbRepositoryConfigurationBuilder WithTypeHandler<T>(DbTypeHandler<T> typeHandler)
    {
        SqlMapper.AddTypeHandler(typeHandler);
        return this;
    }
}