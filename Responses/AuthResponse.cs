using StoreProject.Entities.Users;

namespace StoreProject.Responses;

public record AuthResponse(
    Guid UserId,
    string Name,
    string Email,
    ICollection<Role> Roles,
    string Token,
    DateTime ExpiresAt
);