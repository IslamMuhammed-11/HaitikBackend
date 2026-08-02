using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure.Presistence;

public partial class HaitikDbContext : DbContext
{
    public HaitikDbContext()
    {
    }

    public HaitikDbContext(DbContextOptions<HaitikDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BulkUploadBatch> BulkUploadBatches { get; set; }

    public virtual DbSet<BulkUploadRejectedRow> BulkUploadRejectedRows { get; set; }

    public virtual DbSet<DeliveryProof> DeliveryProofs { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverLocationPing> DriverLocationPings { get; set; }

    public virtual DbSet<GovernmentAgency> GovernmentAgencies { get; set; }

    public virtual DbSet<NotfiactionLog> NotfiactionLogs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    public virtual DbSet<OrderDriverAssignment> OrderDriverAssignments { get; set; }

    public virtual DbSet<Return> Returns { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<OtpCode> OtpCodes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder
        .UseSqlServer("Server=.;Database=HaitikDB;User Id=sa; Password=sa123456;TrustServerCertificate=True;",
            sqloptions => sqloptions.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HaitikDbContext).Assembly);


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
