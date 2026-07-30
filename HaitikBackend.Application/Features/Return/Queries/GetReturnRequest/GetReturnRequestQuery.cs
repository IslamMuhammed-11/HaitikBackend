using HaitikBackend.Application.Features.Return.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Queries.GetReturnRequest;

public sealed record GetReturnRequestQuery(int OrderId) : IRequest<Result<ReturnRequest>>;
