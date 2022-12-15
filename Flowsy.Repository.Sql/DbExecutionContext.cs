using System.Data;
using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public class DbExecutionContext : IExecutionContext
{
    public DbExecutionContext(IRepository repository, string action, object? parameters, CommandType commandType, IDbTransaction? transaction)
    {
        Repository = repository;
        Action = action;
        Parameters = parameters;
        CommandType = commandType;
        Transaction = transaction;
    }
    
    public IRepository Repository { get; }
    public string Action { get; }
    public object? Parameters { get; }
    public CommandType CommandType { get; }
    public IDbTransaction? Transaction { get; }
}