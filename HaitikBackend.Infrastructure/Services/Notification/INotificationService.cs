using HaitikBackend.Application.Common.Interfaces;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Infrastructure.Services.Notification;

public class NotificationService : INotificationService
{

    public Task SendOrderOfferNotificationAsync(int driverId, int orderId, TimeSpan acceptanceWindow, CancellationToken ct = default)
    {
        return Task.CompletedTask;
    }
}
