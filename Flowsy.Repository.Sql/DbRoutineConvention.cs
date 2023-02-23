using Flowsy.Core;

namespace Flowsy.Repository.Sql;

public class DbRoutineConvention : DbConvention
{
    private static DbRoutineConvention? _default;

    public new static DbRoutineConvention Default
        => _default ??= new DbRoutineConvention(
            DbRoutineType.StoredProcedure, 
            NamingConvention.LowerSnakeCase,
            string.Empty,
            string.Empty
            );
    
    public DbRoutineConvention(
        DbRoutineType routineType,
        NamingConvention? naming,
        string prefix = "",
        string suffix = ""
        ) : base(naming, prefix, suffix)
    {
        RoutineType = routineType;
    }
    
    public DbRoutineType RoutineType { get; set; }
}