namespace StoreProject.Entities.Items;

public class Item
{
    public Guid ItemId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int CurrentQuantity { get; set; }
}
