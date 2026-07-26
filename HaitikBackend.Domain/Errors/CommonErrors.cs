using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class CommonErrors
{
    public static Error EntityNotFound(string entity, int id) => Error.Create($"{entity}.NotFound", $"{entity} with id {id} was not found.", enErrorTypes.NotFound);

    public static Error Validation(string code, string message) => Error.Create(code, message, enErrorTypes.Validation);
}
