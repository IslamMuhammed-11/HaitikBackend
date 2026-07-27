using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Command.MarkAsRecived;

public sealed record MarkAsRecivedCommand(int OrderId) : IRequest<Result>;
