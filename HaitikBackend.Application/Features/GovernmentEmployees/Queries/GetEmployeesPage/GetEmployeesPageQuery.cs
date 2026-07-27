using HaitikBackend.Application.Features.GovernmentEmployees.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Queries.GetEmployeesPage;

public sealed record GetEmployeesPageQuery(int PageSize, int PageNumber) : IRequest<Result<EmployeesPageResponse>>;
