using CollectorManager.Services.Collections.DTOs;
using FluentValidation;

namespace CollectorManager.Services.Collections.Validators;

internal class CollectionModelValidator : AbstractValidator<CollectionModel>
{
    public CollectionModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.UserId).GreaterThan(0);
    }
}
