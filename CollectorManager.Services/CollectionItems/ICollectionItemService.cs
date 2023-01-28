using CollectorManager.Data.Domains;
using CollectorManager.Services.CollectionItems.DTOs;

namespace CollectorManager.Services.CollectionItems;

public interface ICollectionItemService : IBaseCrudService<CollectionItem, CollectionItemResumeModel,
    CollectionItemModel, CollectionItemPageableSearchModel, CollectionItemSearchOneModel>
{
}
