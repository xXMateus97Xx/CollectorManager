using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Data.Repository;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.CollectionFormats.DTOs;

namespace CollectorManager.Services.CollectionFormats;

internal class CollectionItemFormatService : BaseCrudService<CollectionFormat, FormatResumeModel,
    FormatModel, FormatPageableSearchModel, FormatSearchOneModel>, ICollectionItemFormatService
{
    private readonly IRepository<Collection> _collectionRepository;

    public CollectionItemFormatService(IRepository<Data.Domains.CollectionFormat> repository,
        IRepository<Collection> collectionRepository,
        IAppContext appContext)
        : base(repository, appContext)
    {
        _collectionRepository = collectionRepository;
    }

    protected override Task BeforeInsertValidationAsync(FormatModel model, IAppContext appContext, CancellationToken cancellationToken) => BeforeValidationAsync(model, appContext);

    protected override Task BeforeUpdateValidationAsync(FormatModel model, IAppContext appContext, CancellationToken cancellationToken) => BeforeValidationAsync(model, appContext);

    protected override async Task BeforeSearchAsync(FormatPageableSearchModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        model.UserId = user.Id;
    }

    protected override async Task BeforeSearchByIdAsync(FormatSearchOneModel request, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        request.UserId = user.Id;
    }

    protected override async Task BeforeUpdateAsync(FormatModel model, IAppContext appContext, CancellationToken cancellationToken)
    {
        var user = await appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);
        model.UserId = user.Id;
    }

    private Task BeforeValidationAsync(FormatModel model, IAppContext appContext)
    {
        model.CollectionRepository = _collectionRepository;
        model.AppContext = appContext;

        return Task.CompletedTask;
    }
}
