namespace HaitikBackend.Application.Features.Users.Queries.GetUserDetails;

public class GetUserDetailsResponse
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int RoleId { get; set; }

}
