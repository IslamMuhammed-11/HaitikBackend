using HaitikBackend.Application.Features.GovernmentAgencies.Qureies.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.GovernmentAgencies.Qureies.GetAgencyDetails;

public sealed record GetAgencyDetailsQuery(int Id) : IRequest<Result<AgencyDetails>>;
