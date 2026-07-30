using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.Return.Queries.GetRequestsPage;

public sealed record GetRequestsPageQuery(enReturnStatus? Status, int PageSize, int PageNumber) : IRequest<Result<HaitikBackend.Application.Features.Return.Queries.Responses.ReturnsPageResponse>>;
