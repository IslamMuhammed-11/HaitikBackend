using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class AgencyConfig : IEntityTypeConfiguration<GovernmentAgency>
{
    public void Configure(EntityTypeBuilder<GovernmentAgency> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Governme__3213E83FED04A28D");

        builder.HasIndex(e => e.Name, "UQ_Gov_Name").IsUnique();

        builder.Property(e => e.Id).HasColumnName("id");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(e => e.PasswordHash)
            .IsUnicode(false)
            .HasMaxLength(255)
            .IsRequired();

        builder.OwnsOne(e => e.Location, e =>
        {
            e.Property(e => e.CurrentLocation)
            .HasColumnType("geography")
            .HasColumnName("Location")
            .IsRequired();
        });

    }
}
