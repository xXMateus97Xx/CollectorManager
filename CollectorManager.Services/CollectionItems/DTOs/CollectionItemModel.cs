using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.CollectionItems.Validators;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CollectorManager.Services.CollectionItems.DTOs;

public class CollectionItemModel : UpdateEntityRequest<CollectionItem>
{
    public CollectionItemModel()
    {
        Name = string.Empty;
        AuthorsIds = new List<int>();
    }

    public string Name { get; set; }

    public List<int> AuthorsIds { get; set; }

    public int Quantity { get; set; }

    public int FormatId { get; set; }

    public int CollectionId { get; set; }

    internal DateTime LastUpdateAt { get; set; }
    internal DateTime? CreatedAt { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<CollectionItem, bool>> UpdateFilter => x => x.Id == Id && x.Collection.UserId == UserId;

    public override IQueryable<CollectionItem> UpdateInclude(IQueryable<CollectionItem> query) => query.Include(x => x.ItemAuthors);

    internal IRepository<Collection>? CollectionRepository { get; set; }
    internal IRepository<CollectionAuthor>? AuthorRepository { get; set; }
    internal IRepository<Data.Domains.CollectionFormat>? FormatRepository { get; set; }
    internal IAppContext? AppContext { get; set; }

    private ValidationResult? _validationResult;
    public override async ValueTask<ValidationResult> ValidateAsync(CancellationToken cancellationToken)
    {
        if (CollectionRepository == null) throw new ArgumentNullException(nameof(CollectionRepository));
        if (AuthorRepository == null) throw new ArgumentNullException(nameof(AuthorRepository));
        if (FormatRepository == null) throw new ArgumentNullException(nameof(FormatRepository));
        if (AppContext == null) throw new ArgumentNullException(nameof(AppContext));

        return _validationResult ??= (await new CollectionItemValidator(CollectionRepository, AuthorRepository, FormatRepository, AppContext)
            .ValidateAsync(this, cancellationToken));
    }
}
