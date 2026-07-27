using System.Collections.Generic;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Queries.Responses;

public sealed record EmployeesPageResponse(IReadOnlyCollection<EmployeeDetails> Employees, int PageSize, int PageNumber, int TotalCount);
