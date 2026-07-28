using HaitikBackend.Domain.Enums;
using HaitikBackend.Domain.Common.Results;
using HaitikBackend.Domain.Errors;
using Microsoft.Extensions.Configuration;

namespace HaitikBackend.Domain.Entities;

public partial class Driver : BaseEntity
{
    public int Id { get; private set; }

    public int UserId { get; private set; }

    public enDriverStatus Status { get; private set; }

    public int GeoZoneId { get; private set; }

    private Driver()
    {
    }

    private Driver(int userId, enDriverStatus status, int geoZoneId)
    {
        UserId = userId;
        Status = status;
        GeoZoneId = geoZoneId;
    }

    internal static Result<Driver> Create(int userId, int geoZoneId, enDriverStatus status = enDriverStatus.Offline)
    {
        var driver = new Driver(userId, status, geoZoneId);

        return Result<Driver>.Success(driver);
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

    public virtual GeoZone GeoZone { get; private set; } = null!;

    public virtual User User { get; private set; } = null!;
}
