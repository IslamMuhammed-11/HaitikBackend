using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.MarkAsDelivered;

public sealed record MarkAsDeliveredCommand(int OrderId) : IRequest<Result>;
