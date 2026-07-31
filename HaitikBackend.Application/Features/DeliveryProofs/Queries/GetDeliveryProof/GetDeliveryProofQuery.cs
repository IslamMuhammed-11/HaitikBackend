using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.DeliveryProofs.Queries.GetDeliveryProof;

public sealed record GetDeliveryProofQuery(int OrderId) : IRequest<Result<HaitikBackend.Application.Features.DeliveryProofs.Queries.Responses.DeliveryProofResponse>>;
