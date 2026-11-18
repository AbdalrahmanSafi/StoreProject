using StoreProject.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreProject.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(rt => rt.Email)
            .IsRequired();

        builder.Property(rt => rt.Password)
            .IsRequired();

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(Intermediate => Intermediate.ToTable("UserRoles"));
    }
}