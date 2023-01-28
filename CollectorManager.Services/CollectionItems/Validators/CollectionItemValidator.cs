using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.CollectionItems.DTOs;
using FluentValidation;

namespace CollectorManager.Services.CollectionItems.Validators;

internal class CollectionItemValidator : AbstractValidator<CollectionItemModel>
{
    private readonly IRepository<Collection> _collectionRepository;
    private readonly IRepository<CollectionAuthor> _authorRepository;
    private readonly IRepository<Data.Domains.CollectionFormat> _formatRepository;
    private readonly IAppContext _appContext;

    public CollectionItemValidator(IRepository<Collection> collectionRepository,
        IRepository<CollectionAuthor> authorRepository,
        IRepository<Data.Domains.CollectionFormat> formatRepository,
        IAppContext appContext)
    {
        _collectionRepository = collectionRepository;
        _authorRepository = authorRepository;
        _formatRepository = formatRepository;
        _appContext = appContext;

        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.CollectionId).GreaterThan(0)
            .MustAsync(BeCollectionOwnerAsync)
            .WithMessage("Coleção Inválida");
        RuleFor(x => x.AuthorsIds).NotEmpty()
            .MustAsync(BeOwnerOfAllAuthorsAsync)
            .WithMessage("Autor inválido foi selecionado");
        RuleFor(x => x.FormatId).GreaterThan(0)
            .MustAsync(BeFormatOwnerAsync)
            .WithMessage("Formato inválido foi selecionado");
    }

    public async Task<bool> BeCollectionOwnerAsync(int collectionId, CancellationToken cancellationToken)
    {
        var user = await _appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);

        return await _collectionRepository.AnyAsync(x => x.Id == collectionId && x.UserId == user.Id, cancellationToken);
    }

    public async Task<bool> BeOwnerOfAllAuthorsAsync(CollectionItemModel model, List<int> authors, ValidationContext<CollectionItemModel> context, CancellationToken cancellationToken)
    {
        var user = await _appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);

        var quantity = await _authorRepository.CountAsync(x => authors.Contains(x.Id) &&
                    x.Collection.UserId == user.Id &&
                    x.CollectionId == model.CollectionId, cancellationToken);

        return quantity == authors.Count;
    }

    public async Task<bool> BeFormatOwnerAsync(CollectionItemModel model, int formatId, ValidationContext<CollectionItemModel> context, CancellationToken cancellationToken)
    {
        var user = await _appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);

        return await _formatRepository.AnyAsync(x => x.Id == formatId &&
                x.Collection.UserId == user.Id &&
                x.CollectionId == model.CollectionId, cancellationToken);
    }
}
