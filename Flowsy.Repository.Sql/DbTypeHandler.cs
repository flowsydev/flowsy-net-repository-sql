using System.Data;
using Dapper;
using Flowsy.Localization;

namespace Flowsy.Repository.Sql;

public abstract class DbTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    public DbTypeHandler(DbType parameterType)
    {
        ParameterType = parameterType;
    }

    public DbType ParameterType { get; protected set; }
    
    public override void SetValue(IDbDataParameter parameter, T value)
    {
        throw new NotSupportedException(string.Format("CouldNotSetValueXForParameterY".Localize(), value, parameter.ParameterName));
    }
    
    public override T Parse(object value)
    {
        throw new NotSupportedException($"{"CouldNotParseValue".Localize()}: {value}");
    }
}