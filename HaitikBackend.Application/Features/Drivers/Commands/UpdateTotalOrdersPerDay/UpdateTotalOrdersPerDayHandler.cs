using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Entities;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.Drivers.Commands.UpdateTotalOrdersPerDay;

public class UpdateTotalOrdersPerDayHandler : IRequestHandler<UpdateTotalOrdersPerDayCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTotalOrdersPerDayHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTotalOrdersPerDayCommand request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetByIdAsync(request.DriverId, cancellationToken);

        if (driver is null)
            return Result.Failed(DriverErrors.DriverNotFound(request.DriverId));

        var result = driver.UpdateMaximumOrdersPerDay(request.MaximumOrdersPerDay);

        if (!result.IsSuccess)
            return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
