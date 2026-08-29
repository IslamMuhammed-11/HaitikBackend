using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HaitikBackend.Authorization;

public sealed class OrderOwnershipHandler : AuthorizationHandler<OrderOwnershipRequirement , OrderDetails >
{
    private readonly IOwnershipService _ownershipService;

    public OrderOwnershipHandler(IOwnershipService ownershipService)
    {
        _ownershipService = ownershipService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OrderOwnershipRequirement requirement,
        OrderDetails? order)
    {
        if (context.User.IsInRole("admin"))
        {
            context.Succeed(requirement);
            return;
        }

        var httpContext = context.Resource as HttpContext;
        if (!int.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var identityId)) // might be DriverId might be AgencyId
            return;

        if (!int.TryParse(httpContext?.GetRouteValue("id")?.ToString(), out var orderId))
            return;

        var canAccess = context.User.IsInRole("agency")
            ? await _ownershipService.CanAccessAgencyAsync(identityId, orderId)
            : context.User.IsInRole("driver") &&
              await _ownershipService.CanAccessDriverAsync(identityId, orderId);

        if (canAccess)
            context.Succeed(requirement);
    }
}

public sealed class AgencyOwnershipHandler : AuthorizationHandler<AgencyOwnershipRequirement, int?>
{


    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AgencyOwnershipRequirement requirement,
        int? agencyId)
    {
        if (context.User.IsInRole("admin"))
        {
            context.Succeed(requirement);
            return;
        }

        if (!context.User.IsInRole("agency") ||
            !int.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var signedAgencyId))
            return;



        if (agencyId is not null && agencyId == signedAgencyId)
            context.Succeed(requirement);

    }




}
