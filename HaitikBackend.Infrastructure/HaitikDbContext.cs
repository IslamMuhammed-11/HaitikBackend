using System;
using System.Collections.Generic;
using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HaitikBackend.Infrastructure;

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

    public virtual DbSet<DeliveryAdmin> DeliveryAdmins { get; set; }

    public virtual DbSet<DeliveryProof> DeliveryProofs { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverLocationPing> DriverLocationPings { get; set; }

    public virtual DbSet<GeoZone> GeoZones { get; set; }

    public virtual DbSet<GovernmentAgency> GovernmentAgencies { get; set; }

    public virtual DbSet<GovernmentEmployee> GovernmentEmployees { get; set; }

    public virtual DbSet<NotfiactionLog> NotfiactionLogs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    public virtual DbSet<OtpCode> OtpCodes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=HaitikDB;User Id=sa; Password=sa123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BulkUploadBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BulkUplo__3213E83F4FE9EB03");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<BulkUploadRejectedRow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BulkUplo__3213E83F750DA1E2");

            entity.HasIndex(e => e.BatchId, "IDX_BulkUploadRejectedRows_BatchId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.Row).HasMaxLength(255);

            entity.HasOne(d => d.Batch).WithMany(p => p.BulkUploadRejectedRows)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BulkUploa__Batch__72C60C4A");
        });

        modelBuilder.Entity<DeliveryAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F48073DF5");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.User).WithMany(p => p.DeliveryAdmins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeliveryA__UserI__6D0D32F4");
        });

        modelBuilder.Entity<DeliveryProof>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3213E83F9B382602");

            entity.ToTable("DeliveryProof");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DeliveryNotes).HasMaxLength(500);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.ReciverName).HasMaxLength(70);

            entity.HasOne(d => d.Order).WithMany(p => p.DeliveryProofs)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DeliveryP__Order__71D1E811");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Driver__3213E83F240E3EF9");

            entity.ToTable("Driver");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.GeoZone).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.GeoZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Driver__GeoZoneI__6E01572D");

            entity.HasOne(d => d.User).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Driver__UserId__693CA210");
        });

        modelBuilder.Entity<DriverLocationPing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DriverLo__3213E83F59D9D910");

            entity.ToTable("DriverLocationPing");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Longitude ");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("Timestamp ");

            entity.HasOne(d => d.Driver).WithMany(p => p.DriverLocationPings)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DriverLoc__Drive__6A30C649");
        });

        modelBuilder.Entity<GeoZone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GeoZone__3213E83FFEA3F6A5");

            entity.ToTable("GeoZone");

            entity.HasIndex(e => e.Name, "IDX_GeoZone_Name");

            entity.HasIndex(e => e.Name, "UQ_GeoZone_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<GovernmentAgency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Governme__3213E83FED04A28D");

            entity.HasIndex(e => e.Name, "UQ_Gov_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<GovernmentEmployee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Governme__3213E83F8051DF16");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Agency).WithMany(p => p.GovernmentEmployees)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Governmen__Agenc__6B24EA82");

            entity.HasOne(d => d.User).WithMany(p => p.GovernmentEmployees)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Governmen__UserI__6C190EBB");
        });

        modelBuilder.Entity<NotfiactionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notfiact__3213E83F58DF3368");

            entity.ToTable("NotfiactionLog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3213E83F649AF5EA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.PickupLat).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.PickupLong).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Agency).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__AgencyId__6FE99F9F");

            entity.HasOne(d => d.GeoZoneNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.GeoZone)
                .HasConstraintName("FK__Orders__GeoZone__6EF57B66");
        });

        modelBuilder.Entity<OrderStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderSta__3213E83F48C09E9A");

            entity.ToTable("OrderStatusHistory");

            entity.HasIndex(e => e.OrderId, "IDX_OrderStatusHistory_OrderId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrentStatus).HasMaxLength(20);
            entity.Property(e => e.LastStatus).HasMaxLength(20);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderStatusHistories)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderStat__Order__70DDC3D8");
        });

        modelBuilder.Entity<OtpCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OtpCodes__3213E83FAF0E23CA");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Otphashed)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("OTPHashed");
            entity.Property(e => e.Purpose).HasMaxLength(20);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3213E83FECAAC897");

            entity.HasIndex(e => e.Name, "UQ_Roles_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83FBBAC1914");

            entity.HasIndex(e => e.Email, "IDX_Users_Email");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => new { e.FirstName, e.LastName }, "UQ_Users_Name").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ_Users_PhoneNumber").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__68487DD7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
