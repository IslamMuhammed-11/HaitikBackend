using HaitikBackend.Application.Features.GovernmentAgencies.Qureies.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Qureies.GetAgenciesPage;

public sealed record GetAgenciesPageQuery(int PageSize, int PageNumber) : IRequest<Result<AgenciesPageResponse>>;
