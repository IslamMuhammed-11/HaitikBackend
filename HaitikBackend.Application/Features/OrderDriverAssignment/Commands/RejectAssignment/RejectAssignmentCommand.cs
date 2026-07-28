using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.RejectAssignment;

public sealed record RejectAssignmentCommand(int OrderId, int DriverId) : IRequest<Result>;
