using System.Data;

namespace Flowsy.Repository.Sql;

public class DbDateOnlyNullableTypeHandler : DbTemporalTypeHandler<DateOnly?>
{
    public DbDateOnlyNullableTypeHandler(
        DbType parameterType = DbType.Date,
        string parameterFormat = "yyyy-MM-dd",
        IEnumerable<string>? parsingFormats = null
        ) 
        : base(parameterType, parameterFormat, parsingFormats)
    {
    }

    public override void SetValue(IDbDataParameter parameter, DateOnly? value)
    {
        parameter.DbType = ParameterType;
        parameter.Value = value?.ToString(ParameterFormat);
    }

    public override DateOnly? Parse(object value)
    {
        if (value is DateTime dateTime)
            return DateOnly.FromDateTime(dateTime);
        
        var stringValue = value.ToString();
        if (stringValue is null)
            return null;
        
        foreach (var format in ParsingFormats)
            if (DateOnly.TryParseExact(stringValue, format, out var dateOnly))
                return dateOnly;
        
        return base.Parse(value);
    }
}