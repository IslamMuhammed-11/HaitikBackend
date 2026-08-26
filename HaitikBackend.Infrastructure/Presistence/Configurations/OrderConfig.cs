using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasKey(e => e.Id).HasName("PK__Orders__3213E83F649AF5EA");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
        builder.Property(e => e.CustomerEmail).HasMaxLength(100);

        builder.OwnsOne(e => e.DeliveryLocation, dl =>
        {
            dl.Property(e => e.CurrentLocation)
            .HasColumnType("geography")
            .HasColumnName("DeliveryLocation")
            .IsRequired();
        });

        builder.Property(e => e.Status).HasMaxLength(20).HasConversion<string>();

        builder.Property(e => e.RowVersion).IsRowVersion();

        builder.Property(e => e.TrackingTokenHash);

        builder.HasOne(d => d.Agency).WithMany(p => p.Orders)
            .HasForeignKey(d => d.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Orders_AgencyId");

        builder.HasOne(e => e.Driver)
            .WithMany(d => d.Orders)
            .HasForeignKey(e => e.AssignedDriver)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Orders_AssignedDriverId");


    }
}
