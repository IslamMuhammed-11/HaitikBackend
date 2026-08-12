using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Qureies.Responses;

public sealed record AgencyDetails
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public double longitude { get; init; }

    public double latitude { get; init; }

    public int TotalOrders { get; init; }
}
