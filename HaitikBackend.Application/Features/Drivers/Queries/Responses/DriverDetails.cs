using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Application.Features.Drivers.Queries.Responses;

public class DriverDetails
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public string UserFullName { get; init; } = null!;

    public enDriverStatus Status { get; init; }

    public short? MaximumOrdersPerDay { get; init; }

    public int TotalActiveOrders{ get; init; }
}
