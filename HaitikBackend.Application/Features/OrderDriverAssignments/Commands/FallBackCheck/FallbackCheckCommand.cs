using HaitikBackend.Domain.Models.Driver;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Commands.FallBackCheck;

public sealed record FallbackCheckCommand(int orderId, List<DriverIdWithActiveOrdersCount> drivers) : IRequest;

