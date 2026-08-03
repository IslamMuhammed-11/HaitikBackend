using HaitikBackend.Application.Common.Models.OfferNotificationModel;

namespace HaitikBackend.Application.Common.Interfaces.Notification;

public interface INotificationPush
{
    Task<bool> SendAsync(OfferNotificationModel model, CancellationToken ct);
}
