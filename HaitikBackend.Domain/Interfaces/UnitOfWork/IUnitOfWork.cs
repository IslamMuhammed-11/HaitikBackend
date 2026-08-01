using HaitikBackend.Domain.Interfaces.Repositories;

namespace HaitikBackend.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork
{

    IBulkUploadBatchRepository BulkUploadBatchs { get; }

    IBulkUploadRejectedRowRepository BulkUploadRejectedRows { get; }


    IDeliveryProofRepository DeliveryProofs { get; }

    IDriverLocationPingRepository DriverLocationPings { get; }

    IDriverRepository Drivers { get; }


    IGovernmentAgencyRepository Agencies { get; }


    INotfiactionLogRepository NotfiactionLogs { get; }

    IOrderDriverAssignmentRepository OrderDriverAssignments { get; }

    IOrderRepository Orders { get; }

    IOrderStatusHistoryRepository OrderStatusHistory { get; }

    IOtpCodeRepository OtpCodes { get; }

    IRefreshTokenRepository RefreshTokens { get; }

    IReturnsRepository Returns { get; }

    IRoleRepository Roles { get; }

    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

}
