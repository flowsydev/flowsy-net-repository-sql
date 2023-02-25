using System.Data;
using System.Globalization;
using Dapper;
using Flowsy.Localization;

namespace Flowsy.Repository.Sql;

public class DbTemporalTypeConverter<T> : SqlMapper.TypeHandler<T>
{
    private readonly IEnumerable<string> _parameterFormats;
    private readonly IEnumerable<string> _parsingFormats;

    public DbTemporalTypeConverter(IEnumerable<string>? parameterFormats = null, IEnumerable<string>? parsingFormats = null)
    {
        _parameterFormats = parameterFormats ?? DbFormats.Temporal;
        _parsingFormats = parsingFormats ?? DbFormats.Temporal;
    }

    protected virtual string? TryFormat(DateOnly value)
    {
        string? str = null;
        
        foreach (var format in _parameterFormats)
        {
            try
            {
                str = value.ToString(format);
                break;
            }
            catch
            {
                str = null;
            }
        }

        return str;
    }
    
    protected virtual string? TryFormat(TimeOnly value)
    {
        string? str = null;
        
        foreach (var format in _parameterFormats)
        {
            try
            {
                str = value.ToString(format);
                break;
            }
            catch
            {
                str = null;
            }
        }

        return str;
    }

    protected virtual string? TryFormat(DateTime value)
    {
        string? str = null;
        
        foreach (var format in _parameterFormats)
        {
            try
            {
                str = value.ToString(format);
                break;
            }
            catch
            {
                str = null;
            }
        }

        return str;
    }

    protected virtual string? TryFormat(DateTimeOffset value)
    {
        string? str = null;
        
        foreach (var format in _parameterFormats)
        {
            try
            {
                str = value.ToString(format);
                break;
            }
            catch
            {
                str = null;
            }
        }

        return str;
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
            DateOnly dateOnly => TryFormat(dateOnly),
            TimeOnly timeOnly => TryFormat(timeOnly),
            DateTime dateTime => TryFormat(dateTime),
            DateTimeOffset dateTimeOffset => TryFormat(dateTimeOffset),
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
        foreach (var format in _parsingFormats)
            if (valueType == typeof(DateOnly) && DateOnly.TryParseExact(stringValue, format, out var dateOnly))
                return (T) Convert.ChangeType(dateOnly, valueType);
            else if (valueType == typeof(TimeOnly) && TimeOnly.TryParseExact(stringValue, format, out var timeOnly))
                return (T) Convert.ChangeType(timeOnly, valueType);
            else if (valueType == typeof(DateTime) && DateTime.TryParseExact(stringValue, format, null, DateTimeStyles.None, out var dateTime))
                return (T) Convert.ChangeType(dateTime, valueType);
            else if (valueType == typeof(DateTimeOffset) && DateTimeOffset.TryParseExact(stringValue, format, null, DateTimeStyles.None, out var dateTimeOffset))
                return (T) Convert.ChangeType(dateTimeOffset, valueType);

        throw new ArgumentException($"{"CouldNotParseValue".Localize()}: {value}", nameof(value));
    }
}