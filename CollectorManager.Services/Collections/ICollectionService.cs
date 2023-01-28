using CollectorManager.Data.Domains;
using CollectorManager.Services.Collections.DTOs;

namespace CollectorManager.Services.Collections;

public interface ICollectionService : IBaseCrudService<Collection, CollectionResumeModel,
    CollectionModel, CollectionPageableSearchModel, CollectionSearchOneModel>
{
}
