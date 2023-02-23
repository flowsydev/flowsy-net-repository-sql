using Flowsy.Core;

namespace Flowsy.Repository.Sql;

public class DbRoutineParameterConvention : DbConvention
{
    public new static DbRoutineParameterConvention Default { get; set; } = new(
        NamingConvention.LowerSnakeCase,
        string.Empty,
        string.Empty,
        (_, parameterName, _, _) => $"@{parameterName}"
        );

    public DbRoutineParameterConvention(
        NamingConvention? naming,
        string prefix = "",
        string suffix = "",
        ResolveRoutineParameterPlaceholder? resolvePlaceholder = null
        ) : base(naming, prefix, suffix)
    {
        ResolvePlaceholder = resolvePlaceholder ?? Default.ResolvePlaceholder;
    }
    
    public ResolveRoutineParameterPlaceholder ResolvePlaceholder { get; set; }
}