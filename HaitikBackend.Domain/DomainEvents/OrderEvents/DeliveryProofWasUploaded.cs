using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderEvents;

public sealed record DeliveryProofWasUploaded(int orderId) : INotification;
