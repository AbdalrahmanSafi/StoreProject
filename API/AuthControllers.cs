using StoreProject.Services;
using StoreProject.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Ghorfa.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register/Client")]
    public async Task<IActionResult> RegisterClient(
        RegisterClientRequest request,
        CancellationToken cancellationToken)
    {
        var response = await authService.RegisterClientAsync(request, cancellationToken);
        return CreatedAtAction(nameof(RegisterClient), response);
    }

    [HttpPost("register/StoreAdmin")]
    public async Task<IActionResult> RegisterStoreAdmin(
        RegisterStoreAdminRequest request,
        CancellationToken cancellationToken)
    {
        var response = await authService.RegisterStoreAdminAsync(request, cancellationToken);
        return CreatedAtAction(nameof(RegisterStoreAdmin), response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("logout")]
    public Task<IActionResult> Logout(
        [FromBody] LogoutRequest request,
        CancellationToken cancellationToken)
    {
        return authService.LogoutAsync(request, cancellationToken);
    }
}
