using CollectorManager.Services.AppContext;
using CollectorManager.Services.Security;
using CollectorManager.Services.Users.DTOs;
using FluentValidation;

namespace CollectorManager.Services.Users.Validators;

internal class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    private readonly IAppContext _appContext;
    private readonly ISecurityService _securityService;

    public ChangePasswordRequestValidator(IAppContext appContext,
        ISecurityService securityService)
    {
        _appContext = appContext;
        _securityService = securityService;

        RuleFor(x => x.OldPassword).NotEmpty()
            .MustAsync(HasTypedOldPasswordCorrectlyAsync)
            .WithMessage("Senha atual não está correta");
        RuleFor(x => x.NewPassword).NotEmpty().Length(6, 20);
        RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.NewPassword);
    }

    public async Task<bool> HasTypedOldPasswordCorrectlyAsync(ChangePasswordRequest request, string password, ValidationContext<ChangePasswordRequest> context, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        var currentUser = await _appContext.GetCurrentUserAsync<UserPassword>(cancellationToken);

        var passwordHash = _securityService.HashPassword(password, currentUser.PasswordSalt);

        return passwordHash == currentUser.Password;
    }
}
