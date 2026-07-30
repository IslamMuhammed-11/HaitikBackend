using HaitikBackend.Application.Features.DriverLocationPings.Queries.Responses;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using MediatR;

namespace HaitikBackend.Application.Features.DriverLocationPings.Commands.PingLocation;

public class PingLocationHandler : IRequestHandler<PingLocationCommand, Result<LocationPing>>
{
    private readonly IUnitOfWork _unitOfWork;

    public PingLocationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LocationPing>> Handle(PingLocationCommand request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetDriverWithLocationById(request.DriverId, cancellationToken);

        if (driver is null)
            return Result<LocationPing>.Failed(DriverErrors.DriverNotFound(request.DriverId));

        driver.PingLocation(request.CurrentLocation, request.TimeStamp);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var location = new LocationPing(driver.UserId, driver.DriverLocationPing!.Location, driver.DriverLocationPing.Timestamp);

        return Result<LocationPing>.Success(location);
    }




}
