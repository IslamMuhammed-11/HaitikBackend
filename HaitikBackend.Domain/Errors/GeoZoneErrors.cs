using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class GeoZoneErrors
{
    public static Error GeoZoneNotFound(int id) => Error.Create("GeoZone.NotFound", $"GeoZone with id {id} was not found.", enErrorTypes.NotFound);

    public static Error GeoZoneNameExists => Error.Create("GeoZone.NameExists", "GeoZone with the provided name already exists.", enErrorTypes.Conflict);

    public static Error InvalidArea => Error.Create("GeoZone.InvalidArea", "Provided area for GeoZone is invalid.", enErrorTypes.Validation);
}
