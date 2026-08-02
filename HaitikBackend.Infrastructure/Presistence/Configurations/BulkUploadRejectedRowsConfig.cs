using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class BulkUploadRejectedRowsConfig 
    : IEntityTypeConfiguration<BulkUploadRejectedRow>
{
    public void Configure(EntityTypeBuilder<BulkUploadRejectedRow> builder)
    {

            builder.HasKey(e => e.Id).HasName("PK__BulkUplo__3213E83F750DA1E2");

            builder.HasIndex(e => e.BatchId, "IDX_BulkUploadRejectedRows_BatchId");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Reason).HasMaxLength(255);
            builder.Property(e => e.Row).HasMaxLength(255);

            builder.HasOne(d => d.Batch).WithMany(p => p.BulkUploadRejectedRows)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BulkUploa__Batch__72C60C4A");
    }
}
