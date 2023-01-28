using CollectorManager.Services.AppContext;
using CollectorManager.Services.Security;
using CollectorManager.Services.Users.Validators;
using FluentValidation.Results;

namespace CollectorManager.Services.Users.DTOs;

public class ChangePasswordRequest : ServiceRequest
{
    public ChangePasswordRequest()
    {
        OldPassword = string.Empty;
        NewPassword = string.Empty;
        ConfirmPassword = string.Empty;
        HashedPassword = string.Empty;
        PasswordSalt = string.Empty;
    }

    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }

    internal string HashedPassword { get; set; }
    internal string PasswordSalt { get; set; }

    internal IAppContext? AppContext { get; set; }
    internal ISecurityService? SecurityService { get; set; }

    private ValidationResult? _validationResult;
    public override async ValueTask<ValidationResult> ValidateAsync(CancellationToken cancellationToken)
    {
        if (AppContext == null) throw new ArgumentNullException(nameof(AppContext));
        if (SecurityService == null) throw new ArgumentNullException(nameof(SecurityService));

        return _validationResult ??= (await new ChangePasswordRequestValidator(AppContext, SecurityService)
            .ValidateAsync(this, cancellationToken));
    }
}
