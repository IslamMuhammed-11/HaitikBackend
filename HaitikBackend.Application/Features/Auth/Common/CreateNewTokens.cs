using HaitikBackend.Application.Abstractions;
using HaitikBackend.Domain.Entities;

namespace HaitikBackend.Application.Features.Auth.Common;

public static class CreateNewTokens
{

    public static TokensResponse CreateTokenForUser(ITokenService _tokenService, IPasswordHasher _passwordHasher, User user)
    {
        var accessToken = _tokenService.GenerateAcceesToken(user.Id, user.Email, user.Role.Name);

        var refreshToken = _tokenService.GenerateRefreshToken();


        var hashedToken = _passwordHasher.HashPassword(refreshToken);

        user.CreateRefreshToken(hashedToken, DateTime.UtcNow.AddDays(7));

        return new TokensResponse(accessToken, refreshToken);

    }


    public static TokensResponse CreateTokenForAgency(ITokenService _tokenService, IPasswordHasher _passwordHasher, GovernmentAgency agency)
    {
        var accessToken = _tokenService.GenerateAcceesToken(agency.Id, agency.Email, "agency");

        var refreshToken = _tokenService.GenerateRefreshToken();

        var hashedToken = _passwordHasher.HashPassword(refreshToken);

        agency.CreateRefreshToken(hashedToken, DateTime.UtcNow.AddDays(7));

        return new TokensResponse(accessToken, refreshToken);

    }

}
