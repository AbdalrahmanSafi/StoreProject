using StoreProject.Entities.Users;
using StoreProject.Entities.Users.Tokens;
using StoreProject.Entities.Orders;
using StoreProject.Entities.Items;
using StoreProject.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace StoreProject.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    // Users Hierarchy
    public DbSet<User> User => Set<User>();
    public DbSet<Admin> Admin => Set<Admin>();
    public DbSet<StoreAdmin> StoreAdmin => Set<StoreAdmin>();
    public DbSet<Client> Client => Set<Client>();
    public DbSet<Role> Role => Set<Role>();

    // Orders
    public DbSet<Order> Order => Set<Order>();
    public DbSet<ItemInfoWhenOrdered> ItemInfos => Set<ItemInfoWhenOrdered>();

    // Tokens
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    // Items
    public DbSet<Item> Item => Set<Item>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().UseTptMappingStrategy();

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(UserConfiguration).Assembly);
    }
}