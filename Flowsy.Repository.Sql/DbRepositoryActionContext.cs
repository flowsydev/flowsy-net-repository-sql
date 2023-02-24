using System.Data;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public class DbRepositoryActionContext : IRepositoryActionContext
{
    public DbRepositoryActionContext(IRepository repository, string actionName, object? parameters, CommandType? commandType, IDbTransaction? transaction)
    {
        Repository = repository;
        ActionName = actionName;
        Parameters = parameters;
        CommandType = commandType;
        Transaction = transaction;
    }
    
    public IRepository Repository { get; }
    public string ActionName { get; }
    public object? Parameters { get; }
    public IDictionary<string, object?> Details { get; set; }
    public CommandType? CommandType { get; }
    public IDbTransaction? Transaction { get; }
}