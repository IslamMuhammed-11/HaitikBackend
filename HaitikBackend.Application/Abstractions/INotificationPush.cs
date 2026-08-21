using HaitikBackend.Application.Common.Models.OfferNotificationModel;

namespace HaitikBackend.Application.Abstractions;

public interface INotificationPush
{
    Task<bool> SendAsync(OfferNotificationModel model, CancellationToken ct);
}
