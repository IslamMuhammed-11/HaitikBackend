using HaitikBackend.Domain.Common.Results;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Command.UpdatePhoneNumber;

public sealed record UpdatePhoneNumberCommand(int Id, string PhoneNumber) : IRequest<Result>;

