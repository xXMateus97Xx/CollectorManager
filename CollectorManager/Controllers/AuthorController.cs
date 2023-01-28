using CollectorManager.Data.Domains;
using CollectorManager.Services.CollectionAuthors;
using CollectorManager.Services.CollectionAuthors.DTOs;

namespace CollectorManager.Controllers;

public class AuthorController : BaseCrudController<ICollectionAuthorService, CollectionAuthor, AuthorResumeModel,
    AuthorModel, AuthorPageableSearchModel, AuthorSearchOneModel>
{
    public AuthorController(ICollectionAuthorService service)
        : base(service)
    {
    }

    protected override AuthorModel? PrepareCreateModel()
    {
        var collectionIdStr = Request.Query["collectionId"];
        if (!int.TryParse(collectionIdStr, out var collectionId))
            return null;

        return new AuthorModel { CollectionId = collectionId };
    }

    protected override object? GetIndexRouteValues(AuthorModel model) => new { collectionId = model.CollectionId };
}
