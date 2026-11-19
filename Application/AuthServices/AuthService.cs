
using StoreProject.Requests;
using StoreProject.Responses;
using StoreProject.Data;
using StoreProject.Entities.Users;
using StoreProject.Entities.Users.Tokens;
using StoreProject.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace StoreProject.Services;

public class AuthService(AppDbContext context, ITokenService tokenService) : IAuthService
{
    public async Task<AuthResponse> RegisterClientAsync(
        RegisterClientRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        bool emailExists = await context.User
            .AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (emailExists)
            throw new Exception("Email is already registered.");

        var clientRole = await context.Role
        .FirstOrDefaultAsync(r => r.RoleName == "Client", cancellationToken)
        ?? throw new Exception("Default 'Client' role not found in database.");
        var Client = new Client
        {
            UserId = Guid.NewGuid(),
            Username = $"{request.FirstName} {request.LastName}",
            Email = request.Email,
            Phone = request.Phone,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Gender = request.Gender == "Female",
        };
        Client.Roles.Add(clientRole);
        await context.Client.AddAsync(Client, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var (accessToken, expiresAt) = tokenService.GenerateToken(Client);

        var refreshToken = await CreateRefreshTokenAsync(
            Client,
            cancellationToken);

        return new AuthResponse(
            Client.UserId,
            Client.Username,
            Client.Email,
            Client.Roles,
            accessToken,
            expiresAt,
            refreshToken);
    }

    public async Task<AuthResponse> RegisterStoreAdminAsync(
        RegisterStoreAdminRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        bool emailExists = await context.User
            .AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (emailExists)
            throw new Exception("Email is already registered.");

        var clientRole = await context.Role
        .FirstOrDefaultAsync(r => r.RoleName == "Client", cancellationToken)
        ?? throw new Exception("Default 'Client' role not found in database.");
        var Client = new Client
        {
            UserId = Guid.NewGuid(),
            Username = $"{request.FirstName} {request.LastName}",
            Email = request.Email,
            Phone = request.Phone,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        Client.Roles.Add(clientRole);
        await context.Client.AddAsync(Client, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var (accessToken, expiresAt) = tokenService.GenerateToken(Client);

        var refreshToken = await CreateRefreshTokenAsync(
            Client,
            cancellationToken);

        return new AuthResponse(
            Client.UserId,
            Client.Username,
            Client.Email,
            Client.Roles,
            accessToken,
            expiresAt,
            refreshToken);
    }

    public async Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await context.User
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(
                u => u.Email == request.Email,
                cancellationToken);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new Exception("Invalid email or password.");

        var (accessToken, expiresAt) = tokenService.GenerateToken(user);

        var refreshToken = await CreateRefreshTokenAsync(
            user,
            cancellationToken);

        return new AuthResponse(
            user.UserId,
            user.Username,
            user.Email,
            user.Roles,
            accessToken,
            expiresAt,
            refreshToken);
    }
    private async Task<string> CreateRefreshTokenAsync(
    User user,
    CancellationToken cancellationToken)
    {
        var refreshToken = tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            RefreshTokenId = Guid.NewGuid(),
            UserId = user.UserId,
            TokenHash = tokenService.HashRefreshToken(refreshToken),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            RevokedAt = null
        };

        await context.RefreshTokens.AddAsync(
            refreshTokenEntity,
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return refreshToken;
    }

    public async Task<IActionResult> LogoutAsync(
        LogoutRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return new NoContentResult();

        var tokenHash = tokenService.HashRefreshToken(request.RefreshToken);

        var token = await context.RefreshTokens
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash, cancellationToken);

        if (token is null)
            return new NoContentResult();

        context.RefreshTokens.Remove(token);
        await context.SaveChangesAsync(cancellationToken);

        return new OkObjectResult(new { message = "Logged out successfully." });
    }
}