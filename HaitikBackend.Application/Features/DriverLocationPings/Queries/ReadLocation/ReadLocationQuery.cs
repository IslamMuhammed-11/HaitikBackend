using HaitikBackend.Application.Features.DriverLocationPings.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.DriverLocationPings.Queries.ReadLocation;

public sealed record ReadLocationQuery(int DriverId) : IRequest<Result<LocationPing>>;
