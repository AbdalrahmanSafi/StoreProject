
using StoreProject.Requests;
using StoreProject.Responses;
using StoreProject.Data;
using StoreProject.Entities.Users;
using StoreProject.Security;
using Microsoft.EntityFrameworkCore;

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

        var (token, expiresAt) = tokenService.GenerateToken(Client);

        return new AuthResponse(
            Client.UserId,
            Client.Username,
            Client.Email,
            Client.Roles,
            token,
            expiresAt);
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

        var (token, expiresAt) = tokenService.GenerateToken(Client);

        return new AuthResponse(
            Client.UserId,
            Client.Username,
            Client.Email,
            Client.Roles,
            token,
            expiresAt);
    }

    public async Task<AuthResponse> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await context.User
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new Exception("Invalid email or password.");

        var (token, expiresAt) = tokenService.GenerateToken(user);

        return new AuthResponse(
            user.UserId,
            user.Username,
            user.Email,
            user.Roles,
            token,
            expiresAt);
    }
}
