namespace HaitikBackend.Application.Common.Models.OfferNotificationModel;

public sealed record OfferNotificationModel(int DriverId, int OrderId, TimeSpan AcceptanceWindow);

