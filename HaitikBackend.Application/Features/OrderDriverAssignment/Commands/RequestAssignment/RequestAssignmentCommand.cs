using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignment.Commands.RequestAssignment;

public sealed record RequestAssignmentCommand(int OrderId, int DriverId) : IRequest<Result<DriverOffer>>;
