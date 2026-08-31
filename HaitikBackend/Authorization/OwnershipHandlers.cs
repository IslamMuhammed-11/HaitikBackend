using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HaitikBackend.Authorization;

/// <summary>
/// Handles the "agency-ownership" policy.
/// Verifies that the signed-in agency is the same as the resource agency ID.
/// Used for self-access checks (e.g. GET /agency/{id}, GET /orders?agencyId=).
/// This is distinct from Order ownership and is intentionally kept as a reusable policy.
/// </summary>
public sealed class AgencyOwnershipHandler : AuthorizationHandler<AgencyOwnershipRequirement, int?>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AgencyOwnershipRequirement requirement,
        int? agencyId)
    {
        if (context.User.IsInRole("admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (!context.User.IsInRole("agency") ||
            !int.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var signedAgencyId))
            return Task.CompletedTask;

        if (agencyId is not null && agencyId == signedAgencyId)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
