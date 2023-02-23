namespace Flowsy.Repository.Sql;

public delegate string ResolveRoutineParameterPlaceholder(
    string routineName,
    string parameterName,
    object? parameterValue,
    DbRoutineType routineType
    );