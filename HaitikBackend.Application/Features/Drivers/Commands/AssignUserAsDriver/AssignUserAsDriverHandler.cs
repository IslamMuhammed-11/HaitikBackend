using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Commands.AssignUserAsDriver;

public class AssignUserAsDriverHandler : IRequestHandler<AssignUserAsDriverCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignUserAsDriverHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(AssignUserAsDriverCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result<int>.Failed(UserErrors.UserNotFound(request.UserId));

        var driver = user.AssignAsDriver(request.MaximumOrderPerDay);

        _unitOfWork.Drivers.Add(driver);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(driver.UserId);
    }
}
