using CollectorManager.Data.Domains;
using CollectorManager.Services.Collections.Validators;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace CollectorManager.Services.Collections.DTOs;

public class CollectionModel : UpdateEntityRequest<Collection>
{
    public CollectionModel()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }
    public CollectionType Type { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<Collection, bool>> UpdateFilter => x => x.Id == Id && x.UserId == UserId;


    private ValidationResult? _validationResult;
    public override async ValueTask<ValidationResult> ValidateAsync(CancellationToken cancellationToken)
    {
        return _validationResult ??= (await new CollectionModelValidator()
            .ValidateAsync(this, cancellationToken));
    }
}
