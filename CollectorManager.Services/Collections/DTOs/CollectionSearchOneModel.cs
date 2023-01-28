using CollectorManager.Data.Domains;
using System.Linq.Expressions;

namespace CollectorManager.Services.Collections.DTOs;

public class CollectionSearchOneModel : SearchOneRequest<Collection>
{
    internal int UserId { get; set; }

    public override Expression<Func<Collection, bool>> Filter => x => x.Id == Id && x.UserId == UserId;
}
