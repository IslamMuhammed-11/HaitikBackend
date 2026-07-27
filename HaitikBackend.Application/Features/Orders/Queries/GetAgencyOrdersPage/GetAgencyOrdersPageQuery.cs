using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.Orders.Queries.GetAgencyOrdersPage;

public sealed record GetAgencyOrdersPageQuery(int AgencyId, enOrderStatus? Status, int PageSize, int PageNumber) : IRequest<Result<OrdersPageResponse>>;
