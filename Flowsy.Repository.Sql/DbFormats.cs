namespace Flowsy.Repository.Sql;

public static class DbFormats
{
    public static readonly IEnumerable<string> DateOnly = 
        new []
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy"
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

    public static IEnumerable<string> Temporal => DateOnly.Concat(DateTime);
}