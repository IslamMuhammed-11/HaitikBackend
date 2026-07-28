using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Domain.DomainEvents.OrderDriverAssignment;

public sealed record DriverAcceptedOrderEvent(int orderId , int driverId) : INotification;

