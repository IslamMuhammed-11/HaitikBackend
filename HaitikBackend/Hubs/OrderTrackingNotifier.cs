using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Enums;
using Microsoft.AspNetCore.SignalR;

namespace HaitikBackend.API.Hubs;

public sealed class OrderTrackingNotifier : IOrderTrackingNotifier
{
    private readonly IHubContext<OrderTrackingHub> _hubContext;

    public OrderTrackingNotifier(IHubContext<OrderTrackingHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyStatusChangedAsync(int orderId, enOrderStatus status, DateTime changedAt, CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.Group(OrderTrackingHub.GetGroupName(orderId))
            .SendAsync("orderStatusChanged", new { orderId, status, changedAt }, cancellationToken);
    }

    public Task NotifyDriverLocationChangedAsync(int orderId, double latitude, double longitude, DateTime timestamp, CancellationToken cancellationToken = default)
    {
        return _hubContext.Clients.Group(OrderTrackingHub.GetGroupName(orderId))
            .SendAsync("driverLocationChanged", new { orderId, latitude, longitude, timestamp }, cancellationToken);
    }
}
