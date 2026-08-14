using HaitikBackend.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HaitikBackend.Infrastructure.Services;

public class TokenService : ITokenService
{
    public string GenerateAcceesToken(int Id, string email, string role)
    {

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Email , email),
            new Claim(ClaimTypes.Role , role),
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_123456"));


        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
        (
            issuer: "HaitikBackend",
            audience: "HaitikBackendUsers",
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
