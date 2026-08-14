using HaitikBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaitikBackend.Infrastructure.Presistence.Configurations;

public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC07CC6018E8");


        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.TokenHash).HasMaxLength(255).IsRequired();

        builder.Property(e => e.Expiry).IsRequired();

        builder.Property(e => e.RevokedAt);

        builder.HasOne(e => e.User)
            .WithMany(e => e.RefreshTokens)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RefreshTokens_UserId");

        builder.HasOne(e => e.Agency)
            .WithMany(e => e.RefreshTokens)
            .HasForeignKey(e => e.AgencyId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RefreshTokens_AgencyID");
    }
}
