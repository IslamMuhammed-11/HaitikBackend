using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderEvents;

public sealed record OrderLoactionChanged(int orderId, GeoLocation oldLocation, GeoLocation newLocation) : INotification;

