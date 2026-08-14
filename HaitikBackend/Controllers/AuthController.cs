using HaitikBackend.Application.Features.GovernmentAgencies.Commands.AddAgency;
using HaitikBackend.Application.Features.Users.Command.RegisterUser;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Extensions;
using HaitikBackend.Application.Common.Interfaces.Security;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    // Register user (drivers or other users)
    [HttpPost("register/user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
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
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // try user by email
        var user = await _unitOfWork.Users.Query()
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == request.Identifier);

        if (user is not null)
        {
            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid credentials" });

            var token = Guid.NewGuid().ToString();
            return Ok(new { type = "user", id = user.Id, token });
        }

        // try agency by username
        var agency = await _unitOfWork.Agencies.Query()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Username == request.Identifier);

        if (agency is not null)
        {
            if (!_passwordHasher.VerifyPassword(request.Password, agency.PasswordHash))
                return Unauthorized(new { message = "Invalid credentials" });

            var token = Guid.NewGuid().ToString();
            return Ok(new { type = "agency", id = agency.Id, token });
        }

        return Unauthorized(new { message = "Invalid credentials" });
    }

    // Logout: revoke refresh tokens for a user or agency
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        var query = _unitOfWork.RefreshTokens.Query();

        var tokens = await query
            .Where(t => (request.UserId.HasValue && t.UserId == request.UserId) || (request.AgencyId.HasValue && t.AgencyId == request.AgencyId))
            .ToListAsync();

        if (!tokens.Any())
            return Result.Success().ToActionResult();

        foreach (var t in tokens)
            t.RevokeToken();

        await _unitOfWork.SaveChangesAsync();

        return Result.Success().ToActionResult();
    }

    // DTOs
    public sealed record LoginRequest(string Identifier, string Password);

    public sealed record LogoutRequest(int? UserId, int? AgencyId);
}
