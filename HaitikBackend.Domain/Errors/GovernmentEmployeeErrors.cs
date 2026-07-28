using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class GovernmentEmployeeErrors
{
    public static Error EmployeeNotFound(int Id) => Error.Create("GovernmentEmployee.NotFound", $"Employee With this Id was not Found : {Id}", enErrorTypes.NotFound);
}
