using HaitikBackend.Application.Common.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;


namespace HaitikBackend.Application.Features.Users.Command.UpdatePassword;

public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, Result>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserPasswordHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
            return Result.Failed(UserErrors.UserNotFound(request.Id));

        bool verify = _passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash);

        if (!verify)
            return Result.Failed(UserErrors.InvalidCreds);

        string newPasswordHash = _passwordHasher.HashPassword(request.NewPassword);

        user.ChangePassword(newPasswordHash);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
