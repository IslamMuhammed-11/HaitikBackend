using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Command.UpdatePassword;

public sealed record UpdateUserPasswordCommand(int Id, string CurrentPassword, string NewPassword) : IRequest<Result>;

