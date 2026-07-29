using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Queries.GetOffersPage;

public sealed record GetOffersPageQuery(enOrderDriverAssignmentStatus? Status, int PageSize, int PageNumber) : IRequest<Result<HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses.OffersPageResponse>>;
