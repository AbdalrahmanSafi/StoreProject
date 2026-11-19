using StoreProject.Entities.Users;

namespace StoreProject.Responses;

public record AuthResponse(
    Guid UserId,
    string Username,
    string Email,
    ICollection<Role> Roles,
    string AccessToken,
    DateTime ExpiresAt,
    string RefreshToken
);