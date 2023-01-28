using CollectorManager.Data.Domains;
using CollectorManager.Services.CollectionFormats;
using CollectorManager.Services.CollectionFormats.DTOs;

namespace CollectorManager.Controllers;

public class FormatController : BaseCrudController<ICollectionItemFormatService, CollectionFormat, FormatResumeModel,
    FormatModel, FormatPageableSearchModel, FormatSearchOneModel>
{
    public FormatController(ICollectionItemFormatService service)
        : base(service)
    {
    }

    protected override FormatModel? PrepareCreateModel()
    {
        var collectionIdStr = Request.Query["collectionId"];
        if (!int.TryParse(collectionIdStr, out var collectionId))
            return null;

        return new FormatModel { CollectionId = collectionId };
    }

    protected override object? GetIndexRouteValues(FormatModel model) => new { collectionId = model.CollectionId };
}
