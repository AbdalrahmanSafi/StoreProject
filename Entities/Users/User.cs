
namespace StoreProject.Entities.Users;

public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}