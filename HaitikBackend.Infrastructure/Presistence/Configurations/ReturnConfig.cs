using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class ReturnConfig : IEntityTypeConfiguration<Return>
{
    public void Configure(EntityTypeBuilder<Return> builder)
    {
        builder.HasKey(e => e.OrderId).HasName("PK_Returns_OrderId");

        builder.Property(e => e.OrderId)
            .ValueGeneratedNever();

        builder.Property(e => e.Reason)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.HasOne(e => e.Agency).WithMany(e => e.Returns)
            .HasForeignKey(e => e.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Returns_AgencyId");

        builder.HasOne(e => e.User).WithMany(e => e.Returns)
            .HasForeignKey(e => e.ReviewedById)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Returns_ReviewedById");
    }
}
