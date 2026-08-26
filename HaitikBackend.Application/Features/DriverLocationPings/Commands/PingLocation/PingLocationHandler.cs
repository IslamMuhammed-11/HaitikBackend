using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Features.DriverLocationPings.Queries.Responses;
using HaitikBackend.Domain.Abstractions.UnitOfWork;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.ValueObjects;
using MediatR;

namespace HaitikBackend.Application.Features.DriverLocationPings.Commands.PingLocation;

public class PingLocationHandler : IRequestHandler<PingLocationCommand, Result<LocationPing>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderTrackingNotifier _trackingNotifier;

    public PingLocationHandler(IUnitOfWork unitOfWork, IOrderTrackingNotifier trackingNotifier)
    {
        _unitOfWork = unitOfWork;
        _trackingNotifier = trackingNotifier;
    }

    public async Task<Result<LocationPing>> Handle(PingLocationCommand request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.GetDriverWithLocationById(request.DriverId, cancellationToken);

        if (driver is null)
            return Result<LocationPing>.Failed(DriverErrors.DriverNotFound(request.DriverId));

        var currentLocation = GeoLocation.Create(request.Latitude, request.Longitude);

        driver.PingLocation(currentLocation, request.TimeStamp);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        foreach (var order in driver.Orders.Where(order => order.Status == enOrderStatus.Delivering))
        {
            await _trackingNotifier.NotifyDriverLocationChangedAsync(
                order.Id,
                request.Latitude,
                request.Longitude,
                request.TimeStamp,
                cancellationToken);
        }

        var location = new LocationPing(driver.UserId, request.Latitude, request.Longitude, driver.DriverLocationPing!.Timestamp);

        return Result<LocationPing>.Success(location);
    }




}
