using HaitikBackend.Application.Features.GovernmentEmployees.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentEmployees.Queries.GetAgencyEmployeesPage;

public sealed record GetAgencyEmployeesPageQuery(int AgencyId, int PageSize, int PageNumber) : IRequest<Result<EmployeesPageResponse>>;
