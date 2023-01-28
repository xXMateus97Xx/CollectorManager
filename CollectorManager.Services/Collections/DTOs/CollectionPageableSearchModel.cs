using CollectorManager.Data.Domains;
using CollectorManager.Data.Helpers;
using System.Linq.Expressions;

namespace CollectorManager.Services.Collections.DTOs;

public class CollectionPageableSearchModel : PageableSearchRequest<Collection>
{
    public string? Name { get; set; }
    public CollectionType? Type { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<Collection, bool>>? Filter
    {
        get
        {
            Expression<Func<Collection, bool>> filter = x => x.UserId == UserId;

            if (!string.IsNullOrWhiteSpace(Name))
                filter = filter.CombineAnd(x => x.Name.Contains(Name));

            if (Type.HasValue)
                filter = filter.CombineAnd(x => x.Type == Type.Value);

            return filter;
        }
    }

    public override Dictionary<string, object> ToRouteValues()
    {
        var dic = new Dictionary<string, object>();

        dic.Add(nameof(PageSize), PageSize);

        if (!string.IsNullOrWhiteSpace(Name))
            dic.Add(nameof(Name), Name);

        if (Type.HasValue)
            dic.Add(nameof(Type), Type);

        return dic;
    }
}
