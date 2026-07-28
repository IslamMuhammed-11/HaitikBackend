using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Commands.AssignUserAsDriver;

public sealed record AssignUserAsDriverCommand(int UserId, short? MaximumOrderPerDay, int GeoZoneId) : IRequest<Result<int>>;
