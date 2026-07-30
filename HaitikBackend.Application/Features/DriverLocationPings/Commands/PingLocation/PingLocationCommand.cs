using HaitikBackend.Application.Features.DriverLocationPings.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Application.Features.DriverLocationPings.Commands.PingLocation;

public sealed record PingLocationCommand(int DriverId, GeoLocation CurrentLocation, DateTime TimeStamp) : IRequest<Result<LocationPing>>;

