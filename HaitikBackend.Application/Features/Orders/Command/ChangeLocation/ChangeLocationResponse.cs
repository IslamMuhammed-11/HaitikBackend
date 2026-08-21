namespace HaitikBackend.Application.Features.Orders.Command.ChangeLocation;

public sealed record ChangeLocationResponse(int OrderId, double Latitude, double Longitude, DateTime ChangedAt);
