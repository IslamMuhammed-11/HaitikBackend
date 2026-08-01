using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.AutoAssignment;

public sealed record AutoAssignmentCommand(int orderId) : IRequest;

