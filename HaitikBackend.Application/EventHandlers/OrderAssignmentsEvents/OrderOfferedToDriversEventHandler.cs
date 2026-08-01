using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;
using MediatR;

namespace HaitikBackend.Application.EventHandlers.OrderAssignmentsEvents;

public class OrderOfferedToDriversEventHandler : INotificationHandler<OrderOfferedToDriversEvent>
{

    private readonly IBackgroundJobs _assignmentSchedular;

    public OrderOfferedToDriversEventHandler(IBackgroundJobs assignmentSchedular)
    {
        _assignmentSchedular = assignmentSchedular;
    }

    public Task Handle(OrderOfferedToDriversEvent notification, CancellationToken cancellationToken)
    {

        return _assignmentSchedular.ScheduleFallbackCheck(notification.orderId, notification.acceptanceWindow);
        
    }
}
