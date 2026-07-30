using HaitikBackend.Application.Features.Return.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Commands.RequestReturn;

public sealed record RequestReturnCommand(int orderId,  string reason) : IRequest<Result<ReturnRequest>>;
