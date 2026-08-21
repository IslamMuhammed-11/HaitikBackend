using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Common.Models;
using HaitikBackend.Application.Common.Models.Email;

namespace HaitikBackend.Infrastructure.Implementaions;

public class NotificationService : INotificationService
{
    private readonly IEmailService _emailService;

    public NotificationService(IEmailService emailService)
    {
        _emailService = emailService;
    }


    public async Task SendOrderOfferNotificationAsync(OrderNotificationModel model, TimeSpan acceptanceWindow, CancellationToken ct = default)
    {
        await _emailService.SendEmailAsync(EmailMessage.Create(
                                      "New Order!", @$"
                                    New Order is Out!
                                    Order Id = {model.OrderID},
                                    Latitude = {model.Latitude},
                                    Longtude = {model.Longitude}", model.DriverEmail));
    }
}
