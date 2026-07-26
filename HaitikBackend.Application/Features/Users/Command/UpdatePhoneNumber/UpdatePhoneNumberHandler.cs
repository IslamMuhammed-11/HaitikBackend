using HaitikBackend.Application.Interfaces.PhoneNumberChecker;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Command.UpdatePhoneNumber;

public class UpdatePhoneNumberHandler : IRequestHandler<UpdatePhoneNumberCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IPhoneNumberChecker _phoneNumberChecker;

    public UpdatePhoneNumberHandler(IUserRepository userRepository
                                        , IPhoneNumberChecker phoneNumberChecker)
    {
        _userRepository = userRepository;
        _phoneNumberChecker = phoneNumberChecker;
    }

    public async Task<Result> Handle(UpdatePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
            return Result.Failed(UserErrors.UserNotFound(request.Id));

        if (!_phoneNumberChecker.CheckPhoneNumber(request.PhoneNumber))
            return Result.Failed(UserErrors.InvalidPhoneNumber);

        user.UpdatePhoneNumber(request.PhoneNumber);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


}
