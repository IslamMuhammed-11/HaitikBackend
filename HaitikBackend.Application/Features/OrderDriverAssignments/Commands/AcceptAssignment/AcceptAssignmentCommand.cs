using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.AcceptAssignment;

public sealed record AcceptAssignmentCommand(int OrderId, int DriverId) : IRequest<Result>;
