using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Commands.RejectReturn;

public sealed record RejectReturnCommand(int orderId, int userId) : IRequest<Result>;

