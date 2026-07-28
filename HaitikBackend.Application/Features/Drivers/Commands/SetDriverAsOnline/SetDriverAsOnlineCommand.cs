using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Commands.SetDriverAsOnline;

public sealed record SetDriverAsOnlineCommand(int DriverId) : IRequest<Result>;
