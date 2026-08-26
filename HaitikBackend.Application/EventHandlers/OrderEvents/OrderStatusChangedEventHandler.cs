using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.EventHandlers.OrderEvents;

public class OrderStatusChangedEventHandler : INotificationHandler<OrderStatusChangedEvent>
{
    private readonly IBackgroundJobs _backgroundJobs;
    private readonly IOrderTrackingNotifier _trackingNotifier;

    public OrderStatusChangedEventHandler(IBackgroundJobs backgroundJobs, IOrderTrackingNotifier trackingNotifier)
    {
        _backgroundJobs = backgroundJobs;
        _trackingNotifier = trackingNotifier;
    }

    public async Task Handle(OrderStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        await _trackingNotifier.NotifyStatusChangedAsync(
            notification.orderId,
            notification.currentStatus,
            notification.changedAt,
            cancellationToken);

        if (notification.currentStatus == enOrderStatus.Delivering)
            await _backgroundJobs.EnqueueCreateTrackingAccess(notification.orderId , notification.customerEmail);
    }
}
