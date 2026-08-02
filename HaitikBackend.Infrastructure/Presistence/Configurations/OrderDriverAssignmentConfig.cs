using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class OrderDriverAssignmentConfig : IEntityTypeConfiguration<OrderDriverAssignment>
{
    public void Configure(EntityTypeBuilder<OrderDriverAssignment> builder)
    {
        builder.HasKey(x => new { x.DriverId, x.OrderId })
            .HasName("PK__OrderDri__1D88C8B8E18FB95F");

        builder.Property(e => e.Status)
            .HasColumnType("nvarchar(20)")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(e => e.CreatedAt).HasColumnType("datetime2").IsRequired();

        builder.Property(e => e.RespondedAt).HasColumnType("datetime2").IsRequired(false);

        builder.HasOne(e => e.Driver)
            .WithMany(d => d.OrderDriverAssignments)
            .HasForeignKey(e => e.DriverId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OrderDriverAssignments_DriverId");

        builder.HasOne(e => e.Order)
            .WithMany(d => d.OrderDriverAssignments)
            .HasForeignKey(e => e.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OrderDriverAssignments_OrderId");
    }
}
