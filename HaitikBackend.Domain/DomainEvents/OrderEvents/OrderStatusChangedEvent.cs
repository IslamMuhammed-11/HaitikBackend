using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderEvents;

public sealed record OrderStatusChangedEvent(int orderId, enOrderStatus currentStatus, DateTime changedAt) : INotification;
