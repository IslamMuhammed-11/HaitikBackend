using HaitikBackend.Application.Common.Interfaces.PhoneNumberChecker;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Users.Command.UpdatePhoneNumber;

public class UpdatePhoneNumberHandler : IRequestHandler<UpdatePhoneNumberCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhoneNumberChecker _phoneNumberChecker;

    public UpdatePhoneNumberHandler(IUnitOfWork unitOfWork
                                        , IPhoneNumberChecker phoneNumberChecker)
    {
        _unitOfWork = unitOfWork;
        _phoneNumberChecker = phoneNumberChecker;
    }

    public async Task<Result> Handle(UpdatePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
            return Result.Failed(UserErrors.UserNotFound(request.Id));

        if (!_phoneNumberChecker.CheckPhoneNumber(request.PhoneNumber))
            return Result.Failed(UserErrors.InvalidPhoneNumber);

        user.UpdatePhoneNumber(request.PhoneNumber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


}
