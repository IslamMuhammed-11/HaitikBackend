using HaitikBackend.Application.Features.Drivers.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Queries.GetDriverDetails;

public sealed record GetDriverDetailsQuery(int Id) : IRequest<Result<DriverDetails>>;
