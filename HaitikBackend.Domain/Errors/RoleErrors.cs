using HaitikBackend.Domain.Enums;

namespace HaitikBackend.Domain.Errors;

public static class RoleErrors
{
    public static Error RoleNotFound(int id) => Error.Create("Role.NotFound", $"Role with id {id} was not found.", enErrorTypes.NotFound);

    public static Error RoleNotFound(string name) => Error.Create("Role.NotFound", $"Role with name '{name}' was not found.", enErrorTypes.NotFound);

    public static Error RoleNameExists => Error.Create("Role.NameExists", "Role with the provided name already exists.", enErrorTypes.Conflict);
}
