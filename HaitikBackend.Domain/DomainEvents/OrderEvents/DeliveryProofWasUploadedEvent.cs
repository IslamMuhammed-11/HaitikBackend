using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderEvents;

public sealed record DeliveryProofWasUploadedEvent(int orderId) : INotification;
