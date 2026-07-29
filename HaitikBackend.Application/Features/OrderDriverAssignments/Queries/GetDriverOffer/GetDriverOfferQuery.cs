using HaitikBackend.Application.Features.OrderDriverAssignment.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.OrderDriverAssignments.Queries.GetDriverOffer;

public sealed record GetDriverOfferQuery(int OrderId, int DrvierId) : IRequest<Result<DriverOffer>>;

