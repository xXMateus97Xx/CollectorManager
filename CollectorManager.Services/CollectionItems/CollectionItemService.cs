using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.CollectionItems.DTOs;

namespace CollectorManager.Services.CollectionItems;

internal class CollectionItemService : BaseCrudService<CollectionItem, CollectionItemResumeModel,
    CollectionItemModel, CollectionItemPageableSearchModel, CollectionItemSearchOneModel>, ICollectionItemService
{
    private readonly IRepository<Collection> _collectionRepository;
    private readonly IRepository<Data.Domains.CollectionFormat> _formatRepository;
    private readonly IRepository<CollectionAuthor> _authorRepository;

    public CollectionItemService(IRepository<CollectionItem> repository,
        IRepository<Collection> collectionRepository,
        IRepository<Data.Domains.CollectionFormat> formatRepository,
        IRepository<CollectionAuthor> authorRepository,
        IAppContext appContext)
        : base(repository, appContext)
    {
        _collectionRepository = collectionRepository;
        _formatRepository = formatRepository;
        _authorRepository = authorRepository;
    }

    protected override Task BeforeInsertAsync(CollectionItemModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        model.CreatedAt = model.LastUpdateAt = DateTime.Now;
        return Task.CompletedTask;
    }

    protected override Task BeforeInsertValidationAsync(CollectionItemModel model, IAppContext appContext, CancellationToken cancellationToken) => BeforeValidationAsync(model, appContext);

    protected override async Task BeforeSearchAsync(CollectionItemPageableSearchModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        model.UserId = user.Id;
    }

    protected override async Task BeforeSearchByIdAsync(CollectionItemSearchOneModel request, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        request.UserId = user.Id;
    }

    protected override async Task BeforeUpdateAsync(CollectionItemModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        model.LastUpdateAt = DateTime.Now;

        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        model.UserId = user.Id;
    }

    protected override Task BeforeUpdateValidationAsync(CollectionItemModel model, IAppContext appContext, CancellationToken cancellationToken) => BeforeValidationAsync(model, appContext);

    private Task BeforeValidationAsync(CollectionItemModel model, IAppContext appContext)
    {
        model.CollectionRepository = _collectionRepository;
        model.FormatRepository = _formatRepository;
        model.AuthorRepository = _authorRepository;
        model.AppContext = appContext;
        return Task.CompletedTask;
    }
}
