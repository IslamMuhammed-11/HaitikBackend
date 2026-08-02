using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public  class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Roles__3213E83FECAAC897");

        builder.HasIndex(e => e.Name, "UQ_Roles_Name").IsUnique();

        builder.Property(e => e.Id).HasColumnName("Id");
        builder.Property(e => e.Name).HasMaxLength(50);
    }
}
