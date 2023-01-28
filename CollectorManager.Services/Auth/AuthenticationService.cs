using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.Auth.DTOs;
using CollectorManager.Services.Security;
using System.Net;

namespace CollectorManager.Services.Auth;

internal class AuthenticationService : IAuthenticationService
{
    private readonly ISecurityService _securityService;
    private readonly IRepository<User> _userRepository;

    public AuthenticationService(ISecurityService securityService,
        IRepository<User> userRepository)
    {
        _securityService = securityService;
        _userRepository = userRepository;
    }

    public async Task<ServiceResponse<LoginResponse?>> AuthenticateAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FirstOrDefaultAsync<LoginResponse>(x => x.UserName == loginRequest.Username, cancellationToken);
        if (user == null)
            return new ServiceResponse<LoginResponse?>(HttpStatusCode.NotFound);

        var hash = _securityService.HashPassword(loginRequest.Password, user.PasswordSalt);
        if (hash != user.Password)
            return new ServiceResponse<LoginResponse?>(HttpStatusCode.BadRequest);

        return new ServiceResponse<LoginResponse?>(user);
    }

    public async Task<T> GetAuthorizedUserAsync<T>(string username, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FirstOrDefaultAsync<T>(x => x.UserName == username, cancellationToken);

        return user ?? throw new Exception("User not found");
    }
}
