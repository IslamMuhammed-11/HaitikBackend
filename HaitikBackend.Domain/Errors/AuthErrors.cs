using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class AuthErrors
{
    public static Error InvalidCredentials => Error.Create("Auth.InvalidCredentials", "The provided credentials are invalid.", enErrorTypes.InvalidCreds);

    public static Error Unauthorized => Error.Create("Auth.Unauthorized", "You are not authorized to perform this action.", enErrorTypes.Unauthorized);

    public static Error Forbidden => Error.Create("Auth.Forbidden", "Access to this resource is forbidden.", enErrorTypes.ForBidden);
}
