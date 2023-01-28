using CollectorManager.Services.Users.DTOs;

namespace CollectorManager.Services.Users;

public interface IUserService
{
    Task<ServiceResponse> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken);
    Task<ServiceResponse> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken);
}
