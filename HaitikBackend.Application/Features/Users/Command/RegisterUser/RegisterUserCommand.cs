using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Command.RegisterUser;

public sealed record RegisterUserCommand(string FirstName, string LastName, string Email, string PhoneNumber, string Password, int RoleId) : IRequest<Result<int>>;

