namespace Flowsy.Repository.Sql;

/// <summary>
/// Cotains the information required to open a database connection.
/// Implementantions of IDbConnectionFactory can use one or more instances of DbConnectionConfiguration
/// to hold the information of each database required by the application.
/// </summary>
public class DbConnectionConfiguration
{
    /// <summary>
    /// Database connection identifier. Shall be unique amongst other instances stored in a single collection.
    /// </summary>
    public string Key { get; set; } = string.Empty;
    
    /// <summary>
    /// The unique name used to identify the database provider. This should be the same as the NuGet package name for the providers runtime.
    /// </summary>
    public string ProviderInvariantName { get; set; } = string.Empty;
    
    /// <summary>
    /// The string used to establish a connection to a data source.
    /// The exact contents of the connection string depend on the specific data source for this connection.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}