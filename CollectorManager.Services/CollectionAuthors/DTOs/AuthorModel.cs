using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.CollectionAuthors.Validators;
using FluentValidation.Results;
using System.Linq.Expressions;

namespace CollectorManager.Services.CollectionAuthors.DTOs;

public class AuthorModel : UpdateEntityRequest<CollectionAuthor>
{
    public AuthorModel()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }

    public int CollectionId { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<CollectionAuthor, bool>> UpdateFilter => x => x.Id == Id && x.Collection.UserId == UserId;

    internal IRepository<Collection>? CollectionRepository { get; set; }
    internal IAppContext? AppContext { get; set; }

    private ValidationResult? _validationResult;
    public override async ValueTask<ValidationResult> ValidateAsync(CancellationToken cancellationToken)
    {
        if (CollectionRepository == null) throw new ArgumentNullException(nameof(CollectionRepository));
        if (AppContext == null) throw new ArgumentNullException(nameof(AppContext));

        return _validationResult ??= (await new AuthorModelValidator(CollectionRepository, AppContext)
            .ValidateAsync(this, cancellationToken));
    }
}
