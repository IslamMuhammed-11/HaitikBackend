using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;
using MediatR;

namespace HaitikBackend.Application.EventHandlers.OrderAssignmentsEvents;

public class OrderOfferedToDriversEventHandler : INotificationHandler<OrderOfferedToDriversEvent>
{

    private readonly IBackgroundJobs _backgroundJobs;

    public OrderOfferedToDriversEventHandler(IBackgroundJobs backgroundJobs)
    {
        _backgroundJobs = backgroundJobs;
    }

    public Task Handle(OrderOfferedToDriversEvent notification, CancellationToken cancellationToken)
    {

        return _backgroundJobs.ScheduleFallbackCheck(notification.orderId, notification.drivers, notification.acceptanceWindow);

    }
}
