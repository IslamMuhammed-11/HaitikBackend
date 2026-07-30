using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Commands.AcceptReturn;

public sealed record AcceptReturnCommand(int orderId , int userId) : IRequest<Result>;
