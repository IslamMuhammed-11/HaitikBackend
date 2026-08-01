namespace HaitikBackend.Application.Common.Interfaces.Notification;

public interface INotificationPush
{
    Task<bool> SendNoificationAsync(int driverId, string title, string body, string token, CancellationToken ct);
}
