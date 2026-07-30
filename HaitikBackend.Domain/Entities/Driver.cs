using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;
using HaitikBackend.Domain.ValueObjects;

namespace HaitikBackend.Domain.Entities;

public partial class Driver : BaseEntity
{
    public int UserId { get; private set; }

    public enDriverStatus Status { get; private set; }

    public short? MaximumOrdersPerDay { get; private set; }


    private Driver()
    {

    }

    private Driver(int userId, enDriverStatus status, short? maximumOrdersPerDay)
    {
        UserId = userId;
        Status = status;
        MaximumOrdersPerDay = maximumOrdersPerDay;
    }

    internal static Driver Create(int userId, short? maximumOrdersPerDay,  enDriverStatus status = enDriverStatus.Offline)
    {
        return new Driver(userId, status, maximumOrdersPerDay);


    }


    public void PingLocation(GeoLocation currentLocation, DateTime timestamp)
    {
        if (DriverLocationPing is null)
            CreateDriverLocationPing(currentLocation, timestamp);

        else
            UpdateLocationPing(currentLocation, timestamp);

    }


    private void UpdateLocationPing(GeoLocation currentLocation, DateTime timestamp)
    {
        DriverLocationPing!.UpdateLocation(currentLocation, timestamp);
    }

    private void CreateDriverLocationPing(GeoLocation currentLocation, DateTime timestamp)
    {
        DriverLocationPing = DriverLocationPing.Create(UserId, currentLocation, timestamp);
    }

    public Result UpdateMaximumOrdersPerDay(short? maximumOrdersPerDay)
    {
        if (maximumOrdersPerDay.HasValue && maximumOrdersPerDay.Value < 0)
            return Result.Failed(DriverErrors.InvalidMaximumOrdersPerDay);

        MaximumOrdersPerDay = maximumOrdersPerDay;

        return Result.Success();
    }

    public Result SetAsOffline()
    {
        if (Status == enDriverStatus.Offline)
            return Result.Failed(DriverErrors.DriverAlreadyOffline);

        ChangeStatus(enDriverStatus.Offline);

        return Result.Success();
    }

    public Result SetAsOnline()
    {
        if (Status == enDriverStatus.Online)
            return Result.Failed(DriverErrors.DriverAlreadyOnline);

        ChangeStatus(enDriverStatus.Online);

        return Result.Success();
    }

    private void ChangeStatus(enDriverStatus status)
    {
        Status = status;
    }

    public virtual DriverLocationPing? DriverLocationPing { get; private set; }

    public virtual ICollection<OrderDriverAssignment> OrderDriverAssignments { get; private set; } = new List<OrderDriverAssignment>();

    public virtual ICollection<Order> Orders { get; private set; } = [];

    public virtual User User { get; private set; } = null!;
}
