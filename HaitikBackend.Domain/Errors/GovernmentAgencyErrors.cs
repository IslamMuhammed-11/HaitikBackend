using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class GovernmentAgencyErrors
{
    public static Error AgencyNotFound(int id) => Error.Create("GovernmentAgency.NotFound", $"Government agency with id {id} was not found.", enErrorTypes.NotFound);

    public static Error AgencyNameExists => Error.Create("GovernmentAgency.NameExists", "A government agency with the provided name already exists.", enErrorTypes.Conflict);
}
