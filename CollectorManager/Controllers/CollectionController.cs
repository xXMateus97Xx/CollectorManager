using CollectorManager.Data.Domains;
using CollectorManager.Helpers;
using CollectorManager.Services.Collections;
using CollectorManager.Services.Collections.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CollectorManager.Controllers;

public class CollectionController : BaseCrudController<ICollectionService, Collection, CollectionResumeModel,
    CollectionModel, CollectionPageableSearchModel, CollectionSearchOneModel>
{
    public CollectionController(ICollectionService collectionService)
        : base(collectionService)
    {

    }

    protected override CollectionModel PrepareCreateModel() => new CollectionModel();

    protected override Task LoadFilterDataAsync(CancellationToken cancellationToken)
    {
        LoadCollectionTypeList(true);

        return Task.CompletedTask;
    }

    protected override Task LoadDataAsync(CancellationToken cancellationToken)
    {
        LoadCollectionTypeList(false);

        return Task.CompletedTask;
    }

    private void LoadCollectionTypeList(bool includeEmpty)
    {
        var list = new List<SelectListItem>();

        if (includeEmpty)
            list.Add(new SelectListItem());

        list.Add(CollectionType.Music.ToSelectListItem());
        list.Add(CollectionType.Books.ToSelectListItem());
        list.Add(CollectionType.Games.ToSelectListItem());

        ViewBag.Types = list;
    }
}
