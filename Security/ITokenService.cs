using StoreProject.Entities.Users;

namespace StoreProject.Security;

public interface ITokenService
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}