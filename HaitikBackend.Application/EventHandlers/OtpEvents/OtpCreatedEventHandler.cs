using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models.Email;
using HaitikBackend.Domain.DomainEvents.OTP;
using MediatR;

namespace HaitikBackend.Application.EventHandlers.OtpEvents;

public class OtpCreatedEventHandler : INotificationHandler<OtpCreatedEvent>
{
    private readonly IEmailService _emailService;

    public OtpCreatedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(OtpCreatedEvent notification, CancellationToken cancellationToken)
    {

        if (notification.email is null)
            return;

        var message = EmailMessage.Create("OTP Verification", $"Here's your OTP:\n {notification.Otp}", notification.email);

        await _emailService.SendEmailAsync(message, cancellationToken);

    }
}
