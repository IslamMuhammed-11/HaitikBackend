using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class DriverErrors
{
    public static Error DriverNotFound(int id) => Error.Create("Driver.NotFound", $"Driver with id {id} was not found.", enErrorTypes.NotFound);

    public static Error DriverAlreadyOnline => Error.Create("Driver.AlreadyOnline", "Driver is already online.", enErrorTypes.Conflict);

    public static Error DriverAlreadyOffline => Error.Create("Driver.AlreadyOffline", "Driver is already offline.", enErrorTypes.Conflict);

    public static Error GeoZoneNotFound(int id) => Error.Create("Driver.GeoZoneNotFound", $"GeoZone with id {id} was not found.", enErrorTypes.NotFound);

    public static Error InvalidMaximumOrdersPerDay => Error.Create("Driver.InvalidMaximumOrdersPerDay", "Provided maximum orders per day is invalid.", enErrorTypes.Validation);

    public static Error DriverReachedMaximumOrdersPerDay(int driverId) => Error.Create("Driver.MaximumOrdersReached", $"Driver with id {driverId} has reached the maximum orders per day.", enErrorTypes.Conflict);
}
