using HaitikBackend.Application.Features.Orders.Queries.Responses;
using System.Security.Claims;

namespace HaitikBackend.Application.Abstractions;

/// <summary>
/// Answers the business question: is the current actor allowed to access or modify this Order?
///
/// Rule:
///   Admin  → always allowed
///   Agency → allowed only when the Order belongs to that agency
///   Driver → allowed only when the Order is assigned to that driver
///   Other  → denied
/// </summary>
public interface IOrderOwnershipService
{
    /// <summary>
    /// Checks ownership against an already-loaded <see cref="OrderDetails"/> DTO.
    /// Use this in read flows to avoid an extra database query.
    /// </summary>
    bool CanAccess(OrderDetails order, ClaimsPrincipal user);

    /// <summary>
    /// Checks ownership by querying the database for the given <paramref name="orderId"/>.
    /// Use this in write flows where the Order entity has not been loaded yet.
    /// </summary>
    Task<bool> CanAccessAsync(int orderId, ClaimsPrincipal user);
}
