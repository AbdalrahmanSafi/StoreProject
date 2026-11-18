using StoreProject.Entities.Users;

namespace StoreProject.Entities.Orders;

public class Order
{
    public Guid OrderId { get; set; }
    public DateTime OrderedAt { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TotalAmount { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public List<ItemInfoWhenOrdered> ItemInfos { get; set; } = [];
}
