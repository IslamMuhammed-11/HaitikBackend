using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Commands.SetDriverAsOffline;

public sealed record SetDriverAsOfflineCommand(int DriverId) : IRequest<Result>;
