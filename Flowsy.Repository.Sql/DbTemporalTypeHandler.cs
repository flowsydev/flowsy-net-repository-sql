using System.Data;
using System.Globalization;
using Dapper;
using Flowsy.Localization;

namespace Flowsy.Repository.Sql;

public class DbTemporalTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    private readonly string _parameterFormat;
    private readonly IEnumerable<string> _parsingFormats;

    public DbTemporalTypeHandler(string parameterFormat, IEnumerable<string>? parsingFormats = null)
    {
        _parameterFormat = parameterFormat;
        _parsingFormats = parsingFormats ?? DbFormats.Temporal;
    }
    
    public override void SetValue(IDbDataParameter parameter, T value)
    {
        parameter.DbType = DbType.Date;
        if (value == null)
        {
            parameter.Value = null;
            return;
        }
        
        parameter.Value = value switch
        {
            DateOnly dateOnly => dateOnly.ToString(_parameterFormat),
            TimeOnly timeOnly => timeOnly.ToString(_parameterFormat),
            DateTime dateTime => dateTime.ToString(_parameterFormat),
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToString(_parameterFormat),
            _ => throw new ArgumentException(
                $"{"InvalidValueForParameter".Localize()} {parameter.ParameterName}",
                nameof(value)
                )
        };
    }
    
    public override T Parse(object value)
    {
        var stringValue = value.ToString();
        if (stringValue is null)
            return default!;
        
        var valueType = typeof(T);
        var dateOnlyType = typeof(DateOnly);
        var timeOnlyType = typeof(TimeOnly);
        var dateTimeType = typeof(DateTime);
        var dateTimeOffsetType = typeof(DateTimeOffset);
        
        foreach (var format in _parsingFormats)
            if (valueType == dateOnlyType && DateOnly.TryParseExact(stringValue, format, out var dateOnly))
                return (T) Convert.ChangeType(dateOnly, valueType);
            else if (valueType == timeOnlyType && TimeOnly.TryParseExact(stringValue, format, out var timeOnly))
                return (T) Convert.ChangeType(timeOnly, valueType);
            else if (valueType == dateTimeType && DateTime.TryParseExact(stringValue, format, null, DateTimeStyles.None, out var dateTime))
                return (T) Convert.ChangeType(dateTime, valueType);
            else if (valueType == dateTimeOffsetType && DateTimeOffset.TryParseExact(stringValue, format, null, DateTimeStyles.None, out var dateTimeOffset))
                return (T) Convert.ChangeType(dateTimeOffset, valueType);

        throw new ArgumentException($"{"CouldNotParseValue".Localize()}: {value}", nameof(value));
    }
}