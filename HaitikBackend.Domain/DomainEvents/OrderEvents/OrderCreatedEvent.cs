using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OrderEvents;

public sealed record OrderCreatedEvent(Order order) : INotification;

