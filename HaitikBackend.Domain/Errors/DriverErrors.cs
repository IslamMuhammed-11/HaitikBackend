using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class DriverErrors
{
    public static Error DriverNotFound(int id) => Error.Create("Driver.NotFound", $"Driver with id {id} was not found.", enErrorTypes.NotFound);

    public static Error DriverAlreadyOnline => Error.Create("Driver.AlreadyOnline", "Driver is already online.", enErrorTypes.Conflict);

    public static Error DriverAlreadyOffline => Error.Create("Driver.AlreadyOffline", "Driver is already offline.", enErrorTypes.Conflict);

    public static Error GeoZoneNotFound(int id) => Error.Create("Driver.GeoZoneNotFound", $"GeoZone with id {id} was not found.", enErrorTypes.NotFound);
}
