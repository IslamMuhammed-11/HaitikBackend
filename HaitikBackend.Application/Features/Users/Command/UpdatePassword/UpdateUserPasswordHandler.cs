using HaitikBackend.Application.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces;
using MediatR;


namespace HaitikBackend.Application.Features.Users.Command.UpdatePassword;

public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, Result>
{

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserPasswordHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id , cancellationToken);

        if (user == null)
            return Result.Failed(UserErrors.UserNotFound(request.Id));

        bool verify = _passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash);

        if (!verify)
            return Result.Failed(UserErrors.InvalidCreds);

        string newPasswordHash = _passwordHasher.HashPassword(request.NewPassword);

        user.ChangePassword(newPasswordHash);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
