using HaitikBackend.Application.Common.Interfaces;
using HaitikBackend.Application.Common.Interfaces.Notification;
using HaitikBackend.Application.Common.Models.OfferNotificationModel;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Services.Notification;

public class NotificationService : INotificationService
{

    public Task SendOrderOfferNotificationAsync(Driver driver, int orderId, TimeSpan acceptanceWindow, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
