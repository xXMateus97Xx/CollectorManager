using CollectorManager.Services.Users.Validators;
using FluentValidation.Results;

namespace CollectorManager.Services.Users.DTOs;

public class SignUpRequest : ServiceRequest
{
    public SignUpRequest()
    {
        Name = string.Empty;
        UserName = string.Empty;
        Password = string.Empty;
        ConfirmPassword = string.Empty;
        HashedPassword = string.Empty;
        PasswordSalt = string.Empty;
    }

    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    internal string HashedPassword { get; set; }
    internal string PasswordSalt { get; set; }

    private ValidationResult? _validationResult;
    public override async ValueTask<ValidationResult> ValidateAsync(CancellationToken cancellationToken)
    {
        return _validationResult ??= (await new SignUpRequestValidator()
            .ValidateAsync(this, cancellationToken));
    }
}
