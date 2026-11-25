using Microsoft.AspNetCore.Mvc;
using StoreProject.Requests;
using StoreProject.Responses;

namespace StoreProject.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterClientAsync(
        RegisterClientRequest request,
        CancellationToken cancellationToken = default);

    Task<AuthResponse> RegisterStoreAdminAsync(
        RegisterStoreAdminRequest request,
        CancellationToken cancellationToken = default);

    Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default);
    Task<IActionResult> LogoutAsync(
    LogoutRequest request,
    CancellationToken cancellationToken = default);
}