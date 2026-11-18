using StoreProject.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreProject.Data.Configurations;

public class ItemInfoWhenOrderedConfiguration : IEntityTypeConfiguration<ItemInfoWhenOrdered>
{
    public void Configure(EntityTypeBuilder<ItemInfoWhenOrdered> builder)
    {
        builder.HasKey(ii => ii.ItemInfoWhenOrderedId);

        builder.HasOne(ii => ii.Order)
            .WithMany(o => o.ItemInfos)
            .HasForeignKey(ii => ii.OrderId);

        builder.HasMany(ii => ii.Items)
            .WithMany()
            .UsingEntity(Intermediate => Intermediate.ToTable("OrderItemsSnapshot"));
    }
}