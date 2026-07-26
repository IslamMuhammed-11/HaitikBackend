using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class NotificationErrors
{
    public static Error NotificationNotFound(int id) => Error.Create("Notification.NotFound", $"Notification with id {id} was not found.", enErrorTypes.NotFound);

    public static Error ProviderFailure => Error.Create("Notification.ProviderFailure", "External notification provider failed.", enErrorTypes.Conflict);
    public static Error RetryLimitReached => Error.Create("Notification.RetryLimitReached", "Notification retry limit has been reached.", enErrorTypes.Conflict);
}
