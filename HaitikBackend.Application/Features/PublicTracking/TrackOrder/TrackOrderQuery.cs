using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.PublicTracking.TrackOrder;

public sealed record TrackOrderQuery(string Token) : IRequest<Result<PublicTrackingResponse>>;
