namespace StoreProject.Requests;

public class LogoutRequest
{
    public string RefreshToken { get; set; } = null!;
}