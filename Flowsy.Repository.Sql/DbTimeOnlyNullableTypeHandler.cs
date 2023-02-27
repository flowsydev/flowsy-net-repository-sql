using System.Data;

namespace Flowsy.Repository.Sql;

public class DbTimeOnlyNullableTypeHandler : DbTemporalTypeHandler<TimeOnly?>
{
    public DbTimeOnlyNullableTypeHandler(
        DbType parameterType = DbType.Time,
        string parameterFormat = "H:mm:ss.ffffff",
        IEnumerable<string>? parsingFormats = null
        ) 
        : base(parameterType, parameterFormat, parsingFormats)
    {
    }

    public override void SetValue(IDbDataParameter parameter, TimeOnly? value)
    {
        parameter.DbType = ParameterType;
        parameter.Value = value?.ToString(ParameterFormat);
    }

    public override TimeOnly? Parse(object value)
    {
        var stringValue = value.ToString();
        if (stringValue is null)
            return null;
        
        foreach (var format in ParsingFormats)
            if (TimeOnly.TryParseExact(stringValue, format, out var dateOnly))
                return dateOnly;
        
        return base.Parse(value);
    }
}