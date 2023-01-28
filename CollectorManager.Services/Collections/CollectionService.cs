using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.Collections.DTOs;

namespace CollectorManager.Services.Collections;

internal class CollectionService : BaseCrudService<Collection, CollectionResumeModel, CollectionModel,
    CollectionPageableSearchModel, CollectionSearchOneModel>, ICollectionService
{
    public CollectionService(IRepository<Collection> repository,
        IAppContext appContext)
        : base(repository, appContext)
    {
    }

    protected override Task BeforeInsertValidationAsync(CollectionModel model, IAppContext appContext, CancellationToken cancellationToken) => BeforeValidationAsync(model, appContext, cancellationToken);

    protected override Task BeforeUpdateValidationAsync(CollectionModel model, IAppContext appContext, CancellationToken cancellationToken) => BeforeValidationAsync(model, appContext, cancellationToken);

    protected override async Task BeforeSearchAsync(CollectionPageableSearchModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        model.UserId = user.Id;
    }

    protected override async Task BeforeSearchByIdAsync(CollectionSearchOneModel request, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        request.UserId = user.Id;
    }

    private async Task BeforeValidationAsync(CollectionModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        model.UserId = user.Id;
    }
}
