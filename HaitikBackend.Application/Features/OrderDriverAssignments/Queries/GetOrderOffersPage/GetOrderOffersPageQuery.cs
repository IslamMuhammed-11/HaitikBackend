using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Queries.GetOrderOffersPage;

public sealed record GetOrderOffersPageQuery(enOrderDriverAssignmentStatus? Status, int OrderId, int PageSize, int PageNumber) : IRequest<Result<HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses.OffersPageResponse>>;
