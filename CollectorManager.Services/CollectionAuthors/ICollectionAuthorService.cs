using CollectorManager.Data.Domains;
using CollectorManager.Services.CollectionAuthors.DTOs;

namespace CollectorManager.Services.CollectionAuthors;

public interface ICollectionAuthorService : IBaseCrudService<CollectionAuthor, AuthorResumeModel,
    AuthorModel, AuthorPageableSearchModel, AuthorSearchOneModel>
{
}
