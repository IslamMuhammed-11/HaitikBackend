using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
            builder.HasKey(e => e.Id).HasName("PK__Users__3213E83FBBAC1914");

            builder.HasIndex(e => e.Email, "IDX_Users_Email");

            builder.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            builder.HasIndex(e => new { e.FirstName, e.LastName }, "UQ_Users_Name").IsUnique();

            builder.HasIndex(e => e.PhoneNumber, "UQ_Users_PhoneNumber").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Email).HasMaxLength(60);
            builder.Property(e => e.FirstName).HasMaxLength(20);
            builder.Property(e => e.LastName).HasMaxLength(20);
            builder.Property(e => e.PasswordHash).HasMaxLength(255);
            builder.Property(e => e.PhoneNumber).HasMaxLength(20);
            builder.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__68487DD7");
        
    }
}
