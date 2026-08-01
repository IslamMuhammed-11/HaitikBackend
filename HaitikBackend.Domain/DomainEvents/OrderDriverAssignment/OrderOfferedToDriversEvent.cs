using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;

public sealed record OrderOfferedToDriversEvent(int orderId, List<int> driverIds, TimeSpan acceptanceWindow) : INotification;
