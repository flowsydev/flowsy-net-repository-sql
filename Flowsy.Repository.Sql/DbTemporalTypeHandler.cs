using System.Data;
using Dapper;
using Flowsy.Localization;

namespace Flowsy.Repository.Sql;

public abstract class DbTemporalTypeHandler<T> : DbTypeHandler<T>
{
    protected DbTemporalTypeHandler(DbType parameterType, string parameterFormat, IEnumerable<string>? parsingFormats = null) : base(parameterType)
    {
        ParameterFormat = parameterFormat;
        ParsingFormats = parsingFormats ?? DbFormats.Temporal;
    }
    
    public string ParameterFormat { get; protected set; }
    public IEnumerable<string> ParsingFormats { get; protected set; }
}