using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderEvents;

public sealed record OrderCreatedEvent(int orderId, DateTime createdAt, GeoLocation pickupLocatin) : INotification;

