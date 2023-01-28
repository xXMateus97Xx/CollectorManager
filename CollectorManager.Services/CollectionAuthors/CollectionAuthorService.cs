using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.CollectionAuthors.DTOs;

namespace CollectorManager.Services.CollectionAuthors;

internal class CollectionAuthorService : BaseCrudService<CollectionAuthor, AuthorResumeModel,
    AuthorModel, AuthorPageableSearchModel, AuthorSearchOneModel>, ICollectionAuthorService
{
    private readonly IRepository<Collection> _collectionRepository;

    public CollectionAuthorService(IRepository<CollectionAuthor> repository,
        IAppContext appContext,
        IRepository<Collection> collectionRepository)
        : base(repository, appContext)
    {
        _collectionRepository = collectionRepository;
    }

    protected override Task BeforeInsertValidationAsync(AuthorModel model, IAppContext appContext, CancellationToken cancellationToken) => BeforeValidationAsync(model, appContext);

    protected override Task BeforeUpdateValidationAsync(AuthorModel model, IAppContext appContext, CancellationToken cancellationToken) => BeforeValidationAsync(model, appContext);

    protected override async Task BeforeSearchAsync(AuthorPageableSearchModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        model.UserId = user.Id;
    }

    protected override async Task BeforeSearchByIdAsync(AuthorSearchOneModel request, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        request.UserId = user.Id;
    }

    protected override async Task BeforeUpdateAsync(AuthorModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        model.UserId = user.Id;
    }

    private Task BeforeValidationAsync(AuthorModel model, IAppContext appContext)
    {
        model.CollectionRepository = _collectionRepository;
        model.AppContext = appContext;
        return Task.CompletedTask;
    }
}
