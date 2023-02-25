namespace Flowsy.Repository.Sql;

public static class DbFormats
{
    public static readonly IEnumerable<string> DateOnly = 
        new []
        {
            "yyyy-MM-dd",
            "yy-MM-dd",
            "yyyy-M-d",
            "yy-M-d",
            "MM/dd/yyyy",
            "MM/dd/yy",
            "M/d/yyyy",
            "M/d/yy",
        };
    
    public static readonly IEnumerable<string> TimeOnly = 
        new []
        {
            "H:mm:ss",
            "H:mm:ss.f",
            "H:mm:ss.ff",
            "H:mm:ss.fff",
            "H:mm:ss.ffff",
            "H:mm:ss.fffff",
            "H:mm:ss.ffffff",
            "h:mm:ss tt",
            "h:mm:ss.f tt",
            "h:mm:ss.ff tt",
            "h:mm:ss.fff tt",
            "h:mm:ss.ffff tt",
            "h:mm:ss.fffff tt",
            "h:mm:ss.ffffff tt"
        };

    public static readonly IEnumerable<string> DateTime =
        new []
        {
            "M/d/yyyy h:mm:ss tt",
            "dd/MM/yyyy H:mm:ss",
            "MM/dd/yyyy H:mm:ss",
            "yyyy-MM-dd H:mm:ss",
            "yyyy-MM-dd H:mm:ss.f",
            "yyyy-MM-dd H:mm:ss.ff",
            "yyyy-MM-dd H:mm:ss.fff",
            "yyyy-MM-dd H:mm:ss.ffff",
            "yyyy-MM-dd H:mm:ss.fffff",
            "yyyy-MM-dd H:mm:ss.ffffff"
        };

    public static IEnumerable<string> Temporal => DateOnly.Concat(TimeOnly).Concat(DateTime);
}