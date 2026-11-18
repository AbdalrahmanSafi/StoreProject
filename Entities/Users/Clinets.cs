using StoreProject.Entities.Orders;
namespace StoreProject.Entities.Users;

public class Client : User
{
    public string Address { get; set; } = null!;
    public bool Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Order> Orders { get; set; } = [];
}