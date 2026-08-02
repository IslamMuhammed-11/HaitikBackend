using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class DeliveryProofConfig : IEntityTypeConfiguration<DeliveryProof>
{
    public void Configure(EntityTypeBuilder<DeliveryProof> builder)
    {


        builder.HasKey(e => e.OrderId).HasName("PK_DeliveryProof_OrderId");

        builder.ToTable("DeliveryProof");

        builder.Property(e => e.DeliveryNotes).HasMaxLength(500);
        builder.Property(e => e.ImageUrl)
            .HasMaxLength(2048)
            .IsUnicode(false)
            .HasColumnName("ImageURL");

        builder.Property(e => e.ReciverName).HasMaxLength(70);

        builder.Property(e => e.DeliverdAt).HasColumnType("datetime2");

        builder.HasOne(d => d.Order).WithOne(p => p.DeliveryProof)
            .HasForeignKey<DeliveryProof>(d => d.OrderId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK__DeliveryP__Order__71D1E811");

    }
}
