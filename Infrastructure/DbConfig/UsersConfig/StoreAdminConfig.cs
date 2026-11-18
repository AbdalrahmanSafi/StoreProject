using StoreProject.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreProject.Data.Configurations;

public class StoreAdminConfiguration : IEntityTypeConfiguration<StoreAdmin>
{
    public void Configure(EntityTypeBuilder<StoreAdmin> builder) { }
}