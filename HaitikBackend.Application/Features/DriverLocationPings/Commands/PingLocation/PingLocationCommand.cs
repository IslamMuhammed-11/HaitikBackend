using HaitikBackend.Application.Features.DriverLocationPings.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.DriverLocationPings.Commands.PingLocation;

public sealed record PingLocationCommand(int DriverId, double Latitude, double Longitude, DateTime TimeStamp) : IRequest<Result<LocationPing>>;

