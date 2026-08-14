using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Auth.RefreshToken;

public sealed record RefreshTokenCommand(string Email, string RefreshToken) : IRequest<Result<RefreshTokenResponse>>;
