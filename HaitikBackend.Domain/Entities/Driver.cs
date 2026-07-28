using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Errors;

namespace HaitikBackend.Domain.Entities;

public partial class Driver : BaseEntity
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public enDriverStatus Status { get; private set; }

    public short? MaximumOrdersPerDay { get; private set; }

    public int GeoZoneId { get; private set; }

    private Driver()
    {
    }

    private Driver(int userId, enDriverStatus status, short? maximumOrdersPerDay, int geoZoneId)
    {
        UserId = userId;
        Status = status;
        MaximumOrdersPerDay = maximumOrdersPerDay;
        GeoZoneId = geoZoneId;
    }

    internal static Driver Create(int userId, short? maximumOrdersPerDay, int geoZoneId, enDriverStatus status = enDriverStatus.Offline)
    {
        return new Driver(userId, status, maximumOrdersPerDay, geoZoneId);


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

    public virtual ICollection<DriverLocationPing> DriverLocationPings { get; private set; } = new List<DriverLocationPing>();

    public virtual ICollection<OrderDriverAssignment> OrderDriverAssignments { get; private set; } = new List<OrderDriverAssignment>();

    public virtual ICollection<Order> Orders { get; private set; } = [];

    public virtual GeoZone GeoZone { get; private set; } = null!;

    public virtual User User { get; private set; } = null!;
}
