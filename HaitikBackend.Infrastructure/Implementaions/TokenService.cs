using HaitikBackend.Application.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace HaitikBackend.Infrastructure.Implementaions;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAcceesToken(int Id, string email, string role)
    {

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Email , email),
            new Claim(ClaimTypes.Role , role),
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured.")));


        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
             issuer: _configuration["Jwt:Issuer"] ?? "HaitikBackend",
             audience: _configuration["Jwt:Audience"] ?? "HaitikBackendUsers",
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds

        );


        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
