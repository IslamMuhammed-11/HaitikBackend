using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Commands.SetDriverAsOffline;

public class SetDriverAsOfflineHandler : IRequestHandler<SetDriverAsOfflineCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public SetDriverAsOfflineHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SetDriverAsOfflineCommand request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(request.DriverId, cancellationToken);

        if (driver is null)
            return Result.Failed(DriverErrors.DriverNotFound(request.DriverId));

        var result = driver.SetAsOffline();

        if (!result.IsSuccess)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
