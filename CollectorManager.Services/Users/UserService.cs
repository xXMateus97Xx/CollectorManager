using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.Security;
using CollectorManager.Services.Users.DTOs;
using System.Net;

namespace CollectorManager.Services.Users;

internal class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IAppContext _appContext;
    private readonly ISecurityService _securityService;

    public UserService(IRepository<User> repository,
        IAppContext appContext,
        ISecurityService securityService)
    {
        _repository = repository;
        _appContext = appContext;
        _securityService = securityService;
    }

    public async Task<ServiceResponse> SignUpAsync(SignUpRequest request, CancellationToken cancellationToken)
    {
        if (!await request.IsValidAsync(cancellationToken))
            return new ServiceResponse(HttpStatusCode.BadRequest);

        request.UserName = request.UserName.Trim();
        request.Name = request.Name.Trim();
        request.PasswordSalt = _securityService.CreatePasswordSalt();
        request.HashedPassword = _securityService.HashPassword(request.Password, request.PasswordSalt);

        await _repository.InsertAsync(request, cancellationToken);

        return new ServiceResponse(HttpStatusCode.OK);
    }

    public async Task<ServiceResponse> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        request.AppContext = _appContext;
        request.SecurityService = _securityService;

        if (!await request.IsValidAsync(cancellationToken))
            return new ServiceResponse(HttpStatusCode.BadRequest);

        request.Id = (await _appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken)).Id;
        request.PasswordSalt = _securityService.CreatePasswordSalt();
        request.HashedPassword = _securityService.HashPassword(request.NewPassword, request.PasswordSalt);

        await _repository.UpdateAsync(request, cancellationToken);

        return new ServiceResponse(HttpStatusCode.OK);
    }
}
