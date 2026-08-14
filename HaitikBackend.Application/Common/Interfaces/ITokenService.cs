namespace HaitikBackend.Application.Common.Interfaces;

public interface ITokenService
{

    string GenerateAcceesToken(int id, string identifier, string role);

    string GenerateRefreshToken();

}
