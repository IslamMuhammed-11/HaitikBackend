using HaitikBackend.Domain.Models.Driver;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.FallBackCheck;

public sealed record FallbackCheckCommand(int orderId, ICollection<DriverWithActiveOrdersCount> drivers) : IRequest;

