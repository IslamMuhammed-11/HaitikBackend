using HaitikBackend.Application.Common.Interfaces.BackgroundJobs;
using HaitikBackend.Domain.DomainEvents.OrderEvents;
using MediatR;

namespace HaitikBackend.Application.EventHandlers.OrderEvents;

public class DeliveryProofWasUploadedEventHandler : INotificationHandler<DeliveryProofWasUploadedEvent>
{
    private readonly IBackgroundJobs _backgroundJobs;

    public DeliveryProofWasUploadedEventHandler(IBackgroundJobs backgroundJobs)
    {
        _backgroundJobs = backgroundJobs;
    }


    public Task Handle(DeliveryProofWasUploadedEvent notification, CancellationToken cancellationToken)
    {
        return _backgroundJobs.EnqueueSendOrderDeliveryOtp(notification.orderId);
    }
}
