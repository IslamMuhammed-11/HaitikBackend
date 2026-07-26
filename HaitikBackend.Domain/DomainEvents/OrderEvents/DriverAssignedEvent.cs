using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderEvents;

public sealed record DriverAssignedEvent(int orderId, int driverId) : INotification;

