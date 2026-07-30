using MediatR;

namespace HaitikBackend.Domain.DomainEvents.ReturnEvents;

public sealed record ReturnRequestCreatedEvent(int orderId, int agencyId, string reason) : INotification;
