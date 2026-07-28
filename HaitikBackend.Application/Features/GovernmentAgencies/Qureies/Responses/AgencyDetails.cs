namespace HaitikBackend.Application.Features.GovernmentAgencies.Qureies.Responses;

public sealed record AgencyDetails
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;
}
