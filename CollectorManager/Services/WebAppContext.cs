using CollectorManager.Services.AppContext;
using CollectorManager.Services.Auth;
using CollectorManager.Services.Auth.DTOs;
using System.Security.Claims;

namespace CollectorManager.Services;

public class WebAppContext : IAppContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthenticationService _authenticationService;
    private readonly Dictionary<Type, object?> _cache;

    public WebAppContext(IHttpContextAccessor httpContextAccessor,
        IAuthenticationService authenticationService)
    {
        _httpContextAccessor = httpContextAccessor;
        _authenticationService = authenticationService;
        _cache = new Dictionary<Type, object?>();
    }

    public ValueTask<UserResume> GetCurrentUserAsync(CancellationToken cancellationToken) => GetCurrentUserAsync<UserResume>(cancellationToken);


    public async ValueTask<T> GetCurrentUserAsync<T>(CancellationToken cancellationToken)
    {
        var type = typeof(T);
        if (_cache.ContainsKey(type) && _cache[type] is T user)
            return user;

        var claim = _httpContextAccessor?.HttpContext?.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
        if (claim == null)
            throw new UnauthorizedAccessException();

        user = await _authenticationService.GetAuthorizedUserAsync<T>(claim.Value, cancellationToken);

        _cache[type] = user;

        return user;
    }

    public bool IsAuthenticated => _httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
