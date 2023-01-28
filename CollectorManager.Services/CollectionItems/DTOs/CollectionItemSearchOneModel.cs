using CollectorManager.Data.Domains;
using System.Linq.Expressions;

namespace CollectorManager.Services.CollectionItems.DTOs;

public class CollectionItemSearchOneModel : SearchOneRequest<CollectionItem>
{
    public int CollectionId { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<CollectionItem, bool>> Filter => x => x.Id == Id && x.CollectionId == CollectionId && x.Collection.UserId == UserId;
}
