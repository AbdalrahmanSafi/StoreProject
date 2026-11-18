using StoreProject.Entities.Items;

namespace StoreProject.Entities.Orders;

public class ItemInfoWhenOrdered
{
    public Guid ItemInfoWhenOrderedId { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public List<Item> Items { get; set; } = [];
}