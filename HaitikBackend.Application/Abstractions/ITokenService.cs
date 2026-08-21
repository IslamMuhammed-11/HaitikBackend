namespace HaitikBackend.Application.Abstractions;

public interface ITokenService
{

    string GenerateAcceesToken(int id, string identifier, string role);

    string GenerateRefreshToken();

}
