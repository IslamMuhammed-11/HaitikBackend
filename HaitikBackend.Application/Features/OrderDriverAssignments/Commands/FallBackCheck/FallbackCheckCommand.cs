using HaitikBackend.Domain.Entities;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.FallBackCheck;

public sealed record FallbackCheckCommand(int orderId, ICollection<Driver> drivers) : IRequest;

