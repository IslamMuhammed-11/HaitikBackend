namespace HaitikBackend.Application.Features.DriverLocationPings.Queries.Responses;

public sealed record LocationPing(int DriverId, double Latitude, double Longitude, DateTime TimeStamp);
