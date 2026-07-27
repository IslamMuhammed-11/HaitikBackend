using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Application.Features.Orders.Queries.Responses;

public sealed record OrderDetails
{
    public int Id { get; init; }

    public enOrderStatus Status { get; init; }

    public string CustomerPhoneNumber { get; init; } = null!;

    public int? AssignedDriver { get; init; }

    public DateTime CreatedAt { get; init; }

    public GeoLocation PickupLocation { get; init; } = null!;

    public int AgencyId { get; init; }

    public string AgencyName { get; init; } = null!;
}
