using System.Text.Json.Serialization;

namespace StoreProject.Entities.Users;

public class Role
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = null!;
    [JsonIgnore]
    public ICollection<User> Users { get; set; } = new List<User>();
}
