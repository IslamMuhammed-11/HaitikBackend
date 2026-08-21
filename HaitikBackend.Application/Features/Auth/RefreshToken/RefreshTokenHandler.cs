using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Features.Auth.Common;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.Auth.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public RefreshTokenHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetUserAndRoleByEmail(request.Email, cancellationToken);

        if (user is not null)
        {
            var activeToken = await _unitOfWork.RefreshTokens.GetUserActiveToken(user.Id);

            var result = RefreshUserToken(activeToken, user);

            await _unitOfWork.SaveChangesAsync();

            return result;

        }



        var agency = await _unitOfWork.Agencies.GetByEmail(request.Email, cancellationToken);

        if (agency is not null)
        {
            var activeToken = await _unitOfWork.RefreshTokens.GetAgencyActiveToken(agency.Id);

            var result = RefreshAgencyToken(activeToken, agency);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        return Result<RefreshTokenResponse>.Failed(AuthErrors.Unauthorized);

    }




    private Result<RefreshTokenResponse> RefreshUserToken(Domain.Entities.RefreshToken? activeToken, User user)
    {
        if (activeToken is null)
            return Result<RefreshTokenResponse>.Failed(AuthErrors.Unauthorized);

        activeToken.RevokeToken();


        var tokens = CreateNewTokens.CreateTokenForUser(_tokenService, _passwordHasher, user);

        var response = new RefreshTokenResponse(tokens.AccessToken, tokens.RefreshToken);

        return Result<RefreshTokenResponse>.Success(response);
    }



    private Result<RefreshTokenResponse> RefreshAgencyToken(Domain.Entities.RefreshToken? activeToken, GovernmentAgency agency)
    {
        if (activeToken is null)
            return Result<RefreshTokenResponse>.Failed(AuthErrors.Unauthorized);

        activeToken.RevokeToken();


        var tokens = CreateNewTokens.CreateTokenForAgency(_tokenService, _passwordHasher, agency);

        var response = new RefreshTokenResponse(tokens.AccessToken, tokens.RefreshToken);

        return Result<RefreshTokenResponse>.Success(response);
    }

}
