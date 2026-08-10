using HaitikBackend.Domain.Interfaces.Repositories;
using HaitikBackend.Domain.Interfaces.UnitOfWork;
using HaitikBackend.Infrastructure.Presistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace HaitikBackend.Infrastructure.Presistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly HaitikDbContext _context;
    private IDbContextTransaction? _transaction;

    // Backing fields for lazy initialization
    private IBulkUploadBatchRepository? _bulkUploadBatchs;
    private IBulkUploadRejectedRowRepository? _bulkUploadRejectedRows;
    private IDeliveryProofRepository? _deliveryProofs;
    private IDriverLocationPingRepository? _driverLocationPings;
    private IDriverRepository? _drivers;
    private IGovernmentAgencyRepository? _agencies;
    private INotfiactionLogRepository? _notfiactionLogs;
    private IOrderDriverAssignmentRepository? _orderDriverAssignments;
    private IOrderRepository? _orders;
    private IOrderStatusHistoryRepository? _orderStatusHistory;
    private IOtpCodeRepository? _otpCodes;
    private IRefreshTokenRepository? _refreshTokens;
    private IReturnsRepository? _returns;
    private IRoleRepository? _roles;
    private IUserRepository? _users;

    public UnitOfWork(HaitikDbContext context)
    {
        _context = context;
    }

    public IBulkUploadBatchRepository BulkUploadBatchs =>
        _bulkUploadBatchs ??= new BulkUploadBatchRepository(_context);

    public IBulkUploadRejectedRowRepository BulkUploadRejectedRows =>
        _bulkUploadRejectedRows ??= new BulkUploadRejectedRowRepository(_context);

    public IDeliveryProofRepository DeliveryProofs =>
        _deliveryProofs ??= new DeliveryProofRepository(_context);

    public IDriverLocationPingRepository DriverLocationPings =>
        _driverLocationPings ??= new DriverLocationPingRepository(_context);

    public IDriverRepository Drivers =>
        _drivers ??= new DriverRepository(_context);

    public IGovernmentAgencyRepository Agencies =>
        _agencies ??= new GovernmentAgencyRepository(_context);

    public INotfiactionLogRepository NotfiactionLogs =>
        _notfiactionLogs ??= new NotfiactionLogRepository(_context);

    public IOrderDriverAssignmentRepository OrderDriverAssignments =>
        _orderDriverAssignments ??= new OrderDriverAssignmentRepository(_context);

    public IOrderRepository Orders =>
        _orders ??= new OrderRepository(_context);

    public IOrderStatusHistoryRepository OrderStatusHistory =>
        _orderStatusHistory ??= new OrderStatusHistoryRepository(_context);

    public IOtpCodeRepository OtpCodes =>
        _otpCodes ??= new OtpCodeRepository(_context);

    public IRefreshTokenRepository RefreshTokens =>
        _refreshTokens ??= new RefreshTokenRepository(_context);

    public IReturnsRepository Returns =>
        _returns ??= new ReturnsRepository(_context);

    public IRoleRepository Roles =>
        _roles ??= new RoleRepository(_context);

    public IUserRepository Users =>
        _users ??= new UserRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException("No active transaction to commit.");

        await _transaction.CommitAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException("No active transaction to roll back.");

        await _transaction.RollbackAsync(cancellationToken);
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
