namespace HaitikBackend.Application.Abstractions;

public interface IOwnershipService
{
    Task<bool> CanAccessDriverAsync(int driverId, int orderId);

    Task<bool> CanAccessAgencyAsync(int agencyId, int orderId);
}
