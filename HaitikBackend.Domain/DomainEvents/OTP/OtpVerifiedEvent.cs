using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Domain.DomainEvents.OTP;

public sealed record OtpVerifiedEvent(int orderId, int OtpId) : INotification;
