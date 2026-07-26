using HaitikBackend.Application.Interfaces.PhoneNumberChecker;
using HaitikBackend.Application.Interfaces.Security;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Command.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<int>>
{

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IPhoneNumberChecker _phoneNumberChecker;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IPhoneNumberChecker phoneNumberChecker)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _phoneNumberChecker = phoneNumberChecker;
    }

    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var exist = await _userRepository.DoesExistByEmail(request.Email, cancellationToken);

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

        _userRepository.Add(user.Value!);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(user.Value!.Id);
    }
}

