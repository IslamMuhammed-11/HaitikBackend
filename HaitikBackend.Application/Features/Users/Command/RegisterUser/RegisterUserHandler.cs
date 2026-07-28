using HaitikBackend.Application.Interfaces.PhoneNumberChecker;
using HaitikBackend.Application.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Command.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<int>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPhoneNumberChecker _phoneNumberChecker;

    public RegisterUserHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IPhoneNumberChecker phoneNumberChecker)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _phoneNumberChecker = phoneNumberChecker;
    }

    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var exist = await _unitOfWork.Users.DoesExistByEmail(request.Email, cancellationToken);

        if (exist)
            return Result<int>.Failed(UserErrors.EmailAlreadyExists);


        string hashedPassword = _passwordHasher.HashPassword(request.Password);

        bool isPhoneNumberValid = _phoneNumberChecker.CheckPhoneNumber(hashedPassword);

        if (!isPhoneNumberValid)
            return Result<int>.Failed(UserErrors.InvalidPhoneNumber);


        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            hashedPassword,
            request.RoleId
        );

        _unitOfWork.Users.Add(user);

        await _unitOfWork.Users.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(user.Id);
    }
}

