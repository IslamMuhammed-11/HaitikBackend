using HaitikBackend.Application.Common.Models.OfferNotificationModel;

namespace HaitikBackend.Application.Common.Interfaces.Notification;

public interface INotificationPush
{
    Task<bool> SendOrderOfferNotification(OfferNotificationModel model, CancellationToken ct);
}
