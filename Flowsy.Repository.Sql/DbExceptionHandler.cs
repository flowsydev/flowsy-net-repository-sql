using Flowsy.Repository.Core;

namespace Flowsy.Repository.Sql;

public abstract class DbExceptionHandler : IExceptionHandler
{
    public virtual Exception Translate(Exception exception, IExecutionContext context) => exception;
}