using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Application.Abstractions;

public interface IOrderTrackingNotifier
{
    Task NotifyStatusChangedAsync(int orderId, enOrderStatus status, DateTime changedAt, CancellationToken cancellationToken = default);

    Task NotifyDriverLocationChangedAsync(int orderId, double latitude, double longitude, DateTime timestamp, CancellationToken cancellationToken = default);
}
