namespace HaitikBackend.Domain.Errors;

public static class DriveLocationPingErrors
{
    public static Error NoProvidedLocationForThisDriver(int DriverId) => Error.Create("DriverLocationPing.NoProvidedLocation", $"No Provided Location was found for the Driver {DriverId}", Enums.enErrorTypes.Conflict);
}
