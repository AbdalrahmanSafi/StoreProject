using StoreProject.Entities.Users.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreProject.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {

        builder.HasKey(rt => rt.RefreshTokenId);

        builder.Property(rt => rt.TokenHash)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(rt => rt.CreatedAt)
            .IsRequired();

        builder.Property(rt => rt.ExpiresAt)
            .IsRequired();

        builder.Property(rt => rt.RevokedAt)
            .IsRequired(false);

        builder.HasIndex(rt => rt.TokenHash)
            .IsUnique();

        builder.HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(rt => rt.UserId);

        builder.ToTable("RefreshTokens");

    }
}
