using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderEvents;

public sealed record OrderStatusChangedEvent(int orderId,string? customerEmail, enOrderStatus currentStatus, DateTime changedAt) : INotification;
