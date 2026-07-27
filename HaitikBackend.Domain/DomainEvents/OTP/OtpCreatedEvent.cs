using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Domain.DomainEvents.OTP;

public sealed record OtpCreatedEvent(int orderId, string Otp) : INotification;

