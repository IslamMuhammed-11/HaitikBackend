namespace HaitikBackend.Application.Features.Users.Queries.GetUsersPage;

public sealed record GetUsersPageResponse(IReadOnlyCollection<UserDetails> Page, int PageSize, int PageNumber , int TotalCount);


public sealed record UserDetails(int Id, string FullName, string Email, string PhoneNumber, string Role);
