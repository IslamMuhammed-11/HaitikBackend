using HaitikBackend.Application.Common.Interfaces;
using HaitikBackend.Application.Common.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
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

            await _unitOfWork.SaveChangesAsync();

            return result;
        }



        var agency = await _unitOfWork.Agencies.GetByEmail(request.Email, cancellationToken);

        if (agency is not null)
        {
            var result = HandleAgencyLogin(request.Password, agency);

            await _unitOfWork.SaveChangesAsync();

            return result;
        }


        return Result<LoginResponse>.Failed(AuthErrors.Unauthorized);
    }




    private Result<LoginResponse> HandleUserLogin(string password, User user)
    {

        if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
            return Result<LoginResponse>.Failed(AuthErrors.Unauthorized);

        var accessToken = _tokenService.GenerateAcceesToken(user.Id, user.Email, user.Role.Name);

        var refreshToken = _tokenService.GenerateRefreshToken();


        var hashedToken = _passwordHasher.HashPassword(refreshToken);

        user.CreateRefreshToken(hashedToken, DateTime.UtcNow.AddDays(7));

        var response = new LoginResponse(accessToken, refreshToken);



        return Result<LoginResponse>.Success(response);

    }


    private Result<LoginResponse> HandleAgencyLogin(string password, GovernmentAgency agency)
    {
        if (!_passwordHasher.VerifyPassword(password, agency.PasswordHash))
            return Result<LoginResponse>.Failed(AuthErrors.Unauthorized);

        var accessToken = _tokenService.GenerateAcceesToken(agency.Id, agency.Email, "agency");

        var refreshToken = _tokenService.GenerateRefreshToken();

        var hashedToken = _passwordHasher.HashPassword(refreshToken);

        agency.CreateRefreshToken(hashedToken, DateTime.UtcNow.AddDays(7));

        var response = new LoginResponse(accessToken, refreshToken);

        return Result<LoginResponse>.Success(response);

    }



}
