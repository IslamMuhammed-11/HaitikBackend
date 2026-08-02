using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class BulkUploadBatchesConfig : IEntityTypeConfiguration<BulkUploadBatch>
{
    public void Configure(EntityTypeBuilder<BulkUploadBatch> builder)
    {

        builder.HasKey(e => e.Id).HasName("PK__BulkUplo__3213E83F4FE9EB03");

        builder.Property(e => e.Id).HasColumnName("id");

        builder.Property(e => e.Counts).HasColumnType("smallint");


        builder.Property(e => e.Status).HasMaxLength(20);

        //builder.HasOne(e => e.UploadedBy).WithMany(u => u.BulkUploadBatches)
        //    .HasForeignKey(e => e.UploadedById)
        //    .OnDelete(DeleteBehavior.Cascade)
        //    .HasConstraintName("FK__BulkUploa__Uploa__3D5E1FD2");

    }
}
