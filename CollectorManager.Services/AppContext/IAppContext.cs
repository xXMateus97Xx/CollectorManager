using CollectorManager.Services.Auth.DTOs;

namespace CollectorManager.Services.AppContext;

public interface IAppContext
{
    bool IsAuthenticated { get; }
    ValueTask<UserResume> GetCurrentUserAsync(CancellationToken cancellationToken);
    ValueTask<T> GetCurrentUserAsync<T>(CancellationToken cancellationToken);
}
