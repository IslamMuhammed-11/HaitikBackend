using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.DeliveryProofs.Queries.GetDeliveryProofsPage;

public sealed record GetDeliveryProofsPageQuery(int PageSize, int PageNumber) : IRequest<Result<HaitikBackend.Application.Features.DeliveryProofs.Queries.Responses.DeliveryProofsPageResponse>>;
