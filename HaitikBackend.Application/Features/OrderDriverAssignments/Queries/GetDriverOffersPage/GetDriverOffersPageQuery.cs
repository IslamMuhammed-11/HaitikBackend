using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Queries.GetDriverOffersPage;

public sealed record GetDriverOffersPageQuery(enOrderDriverAssignmentStatus? Status, int DriverId, int PageSize, int PageNumber) : IRequest<Result<HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses.OffersPageResponse>>;
