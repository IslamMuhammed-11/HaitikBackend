namespace HaitikBackend.Application.Features.Users.Queries.GetUsersPage;

public sealed record GetUsersPageResponse(IReadOnlyCollection<UserDetails> page, int pageSize, int pageNumber);


public sealed record UserDetails(int Id, string FullName, string Email, string PhoneNumber, string Role);
