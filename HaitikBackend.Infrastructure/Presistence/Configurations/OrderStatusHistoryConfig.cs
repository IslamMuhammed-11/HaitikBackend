using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class OrderStatusHistoryConfig : IEntityTypeConfiguration<OrderStatusHistory>
{
    public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
    {

        builder.HasKey(e => e.Id).HasName("PK__OrderSta__3213E83F48C09E9A");

        builder.ToTable("OrderStatusHistory");

        builder.HasIndex(e => e.OrderId, "IDX_OrderStatusHistory_OrderId");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.CurrentStatus).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(e => e.LastStatus).HasConversion<string>().HasMaxLength(20).IsRequired();

        builder.Property(e => e.UpdatedAt).HasColumnType("datetime2").IsRequired();

        builder.HasOne(d => d.Order).WithMany(p => p.OrderStatusHistories)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__OrderStat__Order__70DDC3D8");
    }
}
