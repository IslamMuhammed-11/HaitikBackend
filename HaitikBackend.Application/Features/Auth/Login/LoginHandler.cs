using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Features.Auth.Common;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.Auth.Login;

public class LoginHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    public LoginHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }


    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        var user = await _unitOfWork.Users.GetUserAndRoleByEmail(request.Email, cancellationToken);

        if (user is not null)
        {
            var result = HandleUserLogin(request.Password, user);

            var activeToken = await _unitOfWork.RefreshTokens.GetUserActiveToken(user.Id);

            if (activeToken is not null)
                activeToken.RevokeToken();


            await _unitOfWork.SaveChangesAsync();

            return result;
        }



        var agency = await _unitOfWork.Agencies.GetByEmail(request.Email, cancellationToken);

        if (agency is not null)
        {
            var result = HandleAgencyLogin(request.Password, agency);

            var activeToken = await _unitOfWork.RefreshTokens.GetAgencyActiveToken(agency.Id);

            if (activeToken is not null)
                activeToken.RevokeToken();

            await _unitOfWork.SaveChangesAsync();

            return result;
        }


        return Result<LoginResponse>.Failed(AuthErrors.Unauthorized);
    }




    private Result<LoginResponse> HandleUserLogin(string password, User user )
    {

        if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
            return Result<LoginResponse>.Failed(AuthErrors.Unauthorized);


        var tokens = CreateNewTokens.CreateTokenForUser(_tokenService, _passwordHasher, user);

        var response = new LoginResponse(tokens.AccessToken, tokens.RefreshToken);

        return Result<LoginResponse>.Success(response);

    }


    private Result<LoginResponse> HandleAgencyLogin(string password, GovernmentAgency agency)
    {
        if (!_passwordHasher.VerifyPassword(password, agency.PasswordHash))
            return Result<LoginResponse>.Failed(AuthErrors.Unauthorized);


        var tokens = CreateNewTokens.CreateTokenForAgency(_tokenService, _passwordHasher, agency);

        var response = new LoginResponse(tokens.AccessToken, tokens.RefreshToken);

        return Result<LoginResponse>.Success(response);

    }



}
