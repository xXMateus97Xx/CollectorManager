using CollectorManager.Services.Users.DTOs;
using FluentValidation;

namespace CollectorManager.Services.Users.Validators;

internal class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.UserName).NotEmpty().Must(NoSpace).WithMessage("Barra de espaço não permitida").MaximumLength(20);
        RuleFor(x => x.Password).NotEmpty().Length(6, 20);
        RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
    }

    private bool NoSpace(string text) => !text.AsSpan().Trim().Contains(' ');
}
