using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Features.Auth.Login;
using HaitikBackend.Application.Features.Auth.RefreshToken;
using HaitikBackend.Application.Features.Drivers.Commands.RegiesterDriver;
using HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;
using HaitikBackend.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HaitikBackend.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPasswordHasher _passwordHasher;

    public AuthController(IMediator mediator, IPasswordHasher passwordHasher)
    {
        _mediator = mediator;

        _passwordHasher = passwordHasher;
    }

    [HttpPost("register/driver")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterDriverCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    // Register agency
    [HttpPost("register/agency")]
    public async Task<IActionResult> RegisterAgency([FromBody] RegisterAgencyCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    // Login: accepts email (for users) or username (for agencies) and password
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var result = await _mediator.Send(request);

        return result.ToActionResult();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);

        return result.ToActionResult();
    }


    // Logout: revoke refresh tokens for a user or agency
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        //var query = _unitOfWork.RefreshTokens.Query();

        //var tokens = await query
        //    .Where(t => (request.UserId.HasValue && t.UserId == request.UserId) || (request.AgencyId.HasValue && t.AgencyId == request.AgencyId))
        //    .ToListAsync();

        //if (!tokens.Any())
        //    return Result.Success().ToActionResult();

        //foreach (var t in tokens)
        //    t.RevokeToken();

        //await _unitOfWork.SaveChangesAsync();

        //return Result.Success().ToActionResult();

        throw new NotImplementedException();
    }

    // DTOs
    public sealed record LoginRequest(string Identifier, string Password);

    public sealed record LogoutRequest(int? UserId, int? AgencyId);
}
