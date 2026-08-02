using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class DriverLoactionPingConfig : IEntityTypeConfiguration<DriverLocationPing>
{
    public void Configure(EntityTypeBuilder<DriverLocationPing> builder)
    {
        builder.HasKey(e => e.DriverId).HasName("PK__DriverLo__F1B1CD046FAEBDC3");

        builder.Property(e => e.Timestamp).HasColumnType("datetime2").IsRequired();

        builder.OwnsOne(e => e.Location, dl =>
        {
            dl.Property(e => e.CurrentLocation)
            .HasColumnType("geography")
            .HasColumnName("Location")
            .IsRequired();
        });


        builder.HasOne(e => e.Driver)
            .WithOne(d => d.DriverLocationPing)
            .HasForeignKey<DriverLocationPing>(e => e.DriverId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_DriverLocationPing_DriverId");
    }
}
