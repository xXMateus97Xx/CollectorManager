using CollectorManager.Services.Auth.DTOs;

namespace CollectorManager.Services.Auth;

public interface IAuthenticationService
{
    Task<ServiceResponse<LoginResponse?>> AuthenticateAsync(LoginRequest loginRequest, CancellationToken cancellationToken);
    Task<T> GetAuthorizedUserAsync<T>(string username, CancellationToken cancellationToken);
}
