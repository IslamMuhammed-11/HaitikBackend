using System;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Queries.Responses;

public sealed record EmployeeDetails
{
    public int Id { get; init; }

    public int UserId { get; init; }

    public int AgencyId { get; init; }

    public string AgencyName { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string PhoneNumber { get; init; } = null!;
}
