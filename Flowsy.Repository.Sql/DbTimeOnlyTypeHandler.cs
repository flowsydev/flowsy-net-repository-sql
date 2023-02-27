using System.Data;

namespace Flowsy.Repository.Sql;

public class DbTimeOnlyTypeHandler : DbTemporalTypeHandler<TimeOnly>
{
    public DbTimeOnlyTypeHandler(
        DbType parameterType = DbType.Time,
        string parameterFormat = "H:mm:ss.ffffff",
        IEnumerable<string>? parsingFormats = null
        ) 
        : base(parameterType, parameterFormat, parsingFormats)
    {
    }

    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.DbType = ParameterType;
        parameter.Value = value.ToString(ParameterFormat);
    }

    public override TimeOnly Parse(object value)
    {
        foreach (var format in ParsingFormats)
            if (TimeOnly.TryParseExact(value.ToString(), format, out var timeOnly))
                return timeOnly;
        
        return base.Parse(value);
    }
}