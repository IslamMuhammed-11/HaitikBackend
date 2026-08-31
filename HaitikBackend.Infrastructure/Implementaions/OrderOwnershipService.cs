using HaitikBackend.Application.Abstractions;
using HaitikBackend.Application.Features.Orders.Queries.Responses;
using HaitikBackend.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HaitikBackend.Infrastructure.Implementaions;

public sealed class OrderOwnershipService : IOrderOwnershipService
{
    private readonly HaitikDbContext _dbContext;

    public OrderOwnershipService(HaitikDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public bool CanAccess(OrderDetails order, ClaimsPrincipal user)
    {
        if (user.IsInRole("admin"))
            return true;

        if (!int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var identityId))
            return false;

        if (user.IsInRole("agency"))
            return order.AgencyId == identityId;

        if (user.IsInRole("driver"))
            return order.AssignedDriver == identityId;

        return false;
    }

    /// <inheritdoc />
    public async Task<bool> CanAccessAsync(int orderId, ClaimsPrincipal user)
    {
        if (user.IsInRole("admin"))
            return true;

        if (!int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var identityId))
            return false;

        if (user.IsInRole("agency"))
            return await _dbContext.Orders
                .AsNoTracking()
                .AnyAsync(o => o.Id == orderId && o.AgencyId == identityId);

        if (user.IsInRole("driver"))
            return await _dbContext.Orders
                .AsNoTracking()
                .AnyAsync(o => o.Id == orderId && o.AssignedDriver == identityId);

        return false;
    }
}
