using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Domain.DomainEvents.ReturnEvents;

public sealed record ReturnRequestAcceptedEvent(int orderId , int userId ) : INotification;

