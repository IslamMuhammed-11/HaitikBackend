using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using MediatR;

namespace HaitikBackend.Application.EventHandlers.OrderEvents;

public class OrderStatusChangedEventHandler : INotificationHandler<OrderStatusChangedEvent>
{
    private readonly IBackgroundJobs _backgroundJobs;

    public OrderStatusChangedEventHandler(IBackgroundJobs backgroundJobs)
    {
        _backgroundJobs = backgroundJobs;
    }

    public Task Handle(OrderStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        return _backgroundJobs.EnqueueOrderStatusNotification(notification.orderId, notification.currentStatus, notification.changedAt);
    }
}
