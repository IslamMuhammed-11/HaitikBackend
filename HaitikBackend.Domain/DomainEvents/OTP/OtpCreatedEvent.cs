using MediatR;

namespace HaitikBackend.Domain.DomainEvents.OTP;

public sealed record OtpCreatedEvent(int orderId, string? email, string Otp) : INotification;

