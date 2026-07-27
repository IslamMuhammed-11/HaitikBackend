using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.AssignDriver;

public sealed record AssignDriverCommand(int OrderId, int DriverId) : IRequest<Result>;
