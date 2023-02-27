using System.Data;

namespace Flowsy.Repository.Sql;

public class DbDateOnlyTypeHandler : DbTemporalTypeHandler<DateOnly>
{
    public DbDateOnlyTypeHandler(
        DbType parameterType = DbType.Date,
        string parameterFormat = "yyyy-MM-dd",
        IEnumerable<string>? parsingFormats = null
        ) 
        : base(parameterType, parameterFormat, parsingFormats)
    {
    }

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = ParameterType;
        parameter.Value = value.ToString(ParameterFormat);
    }

    public override DateOnly Parse(object value)
    {
        foreach (var format in ParsingFormats)
            if (DateOnly.TryParseExact(value.ToString(), format, out var dateOnly))
                return dateOnly;
        
        return base.Parse(value);
    }
}