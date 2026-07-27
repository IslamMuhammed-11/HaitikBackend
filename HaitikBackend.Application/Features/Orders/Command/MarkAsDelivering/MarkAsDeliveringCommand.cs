using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.MarkAsDelivering;

public sealed record MarkAsDeliveringCommand(int OrderId) : IRequest<Result>;
