using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.PublicTracking.CreateTrackingAccess;

public sealed record CreateTrackingAccessCommand(int OrderId , string? email) : IRequest<Result<string>>;
