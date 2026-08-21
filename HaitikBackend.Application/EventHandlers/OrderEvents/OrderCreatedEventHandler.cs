using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using MediatR;

namespace HaitikBackend.Application.EventHandlers.OrderEvents;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{

    private readonly IBackgroundJobs _assignmentSchedular;

    public OrderCreatedEventHandler(IBackgroundJobs assignmentSchedular)
    {
        _assignmentSchedular = assignmentSchedular;
    }

    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        return _assignmentSchedular.EnqueueAutoAssignment(notification.order.Id);

    }
}
