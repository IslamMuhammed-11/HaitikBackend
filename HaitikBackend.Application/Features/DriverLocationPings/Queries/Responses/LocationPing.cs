using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Application.Features.DriverLocationPings.Queries.Responses;

public sealed record LocationPing(int DriverId, GeoLocation Location, DateTime TimeStamp);
