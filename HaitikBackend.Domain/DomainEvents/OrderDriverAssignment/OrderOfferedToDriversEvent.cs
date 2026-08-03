using HaitikBackend.Domain.Models.Driver;
using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;

public sealed record OrderOfferedToDriversEvent(int orderId, ICollection<DriverWithActiveOrdersCount> drivers, TimeSpan acceptanceWindow) : INotification;
