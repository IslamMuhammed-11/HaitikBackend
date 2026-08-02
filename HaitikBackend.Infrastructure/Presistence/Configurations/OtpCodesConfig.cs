using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class OtpCodesConfig : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__OtpCodes__3213E83FAF0E23CA");

        builder.Property(e => e.Id).HasColumnName("id");

        builder.Property(e => e.Otphashed)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("OTPHashed");

        builder.Property(e => e.ExpiryDate)
            .HasColumnType("datetime2")
            .HasColumnName("ExpiryDate");

        builder.Property(e => e.AttemptCount)
            .HasColumnType("smallint");

        builder.Property(e => e.Purpose).HasConversion<string>().HasMaxLength(20);
    }
}
