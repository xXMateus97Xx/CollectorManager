using CollectorManager.Data;
using CollectorManager.Data.Domains;
using CollectorManager.Helpers;
using CollectorManager.Services.AppContext;
using CollectorManager.Services.CollectionAuthors;
using CollectorManager.Services.CollectionFormats;
using CollectorManager.Services.CollectionItems;
using CollectorManager.Services.CollectionItems.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollectorManager.Controllers;

public class CollectionItemController : BaseCrudController<ICollectionItemService, CollectionItem, CollectionItemResumeModel,
    CollectionItemModel, CollectionItemPageableSearchModel, CollectionItemSearchOneModel>
{
    private readonly ICollectionItemFormatService _formatService;
    private readonly ICollectionAuthorService _authorService;
    private readonly IAppContext _appContext;

    public CollectionItemController(ICollectionItemService service,
        ICollectionItemFormatService formatService,
        ICollectionAuthorService authorService,
        IAppContext appContext)
        : base(service)
    {
        _formatService = formatService;
        _authorService = authorService;
        _appContext = appContext;
    }

    protected override CollectionItemModel? PrepareCreateModel()
    {
        var collectionIdStr = Request.Query["collectionId"];
        if (!int.TryParse(collectionIdStr, out var collectionId))
            return null;

        return new CollectionItemModel { CollectionId = collectionId };
    }

    protected override object? GetIndexRouteValues(CollectionItemModel model) => new { collectionId = model.CollectionId };

    protected override async Task LoadDataAsync(CancellationToken cancellationToken)
    {
        if (!int.TryParse(Request.Query["collectionId"], out var collectionId))
            if (!int.TryParse(Request.Form["CollectionId"], out collectionId))
                return;

        var user = await _appContext.GetCurrentUserAsync<BaseEntityModel>(cancellationToken);

        var formats = await _formatService.GetAllAsync<SelectListItem>(
            filter: x => x.CollectionId == collectionId && x.Collection.UserId == user.Id,
            orderBy: x => x.OrderBy(y => y.Name),
            cancellationToken);

        ViewBag.Formats = formats.Data?.PrependDefault();

        var authors = await _authorService.GetAllAsync<SelectListItem>(
            filter: x => x.CollectionId == collectionId && x.Collection.UserId == user.Id,
            orderBy: x => x.OrderBy(y => y.Name),
            cancellationToken);

        ViewBag.Authors = authors.Data;
    }

    protected override async Task LoadFilterDataAsync(CancellationToken cancellationToken)
    {
        var collectionIdStr = Request.Query["collectionId"];
        if (!int.TryParse(collectionIdStr, out var collectionId))
            return;

        var formats = await _formatService.GetAllAsync<SelectListItem>(
            filter: x => x.CollectionId == collectionId,
            orderBy: x => x.OrderBy(y => y.Name),
            cancellationToken);

        ViewBag.Formats = formats.Data;
    }
}
