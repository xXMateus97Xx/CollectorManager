using CollectorManager.Data.Domains;
using CollectorManager.Data.Helpers;
using System.Linq.Expressions;

namespace CollectorManager.Services.CollectionAuthors.DTOs;

public class AuthorPageableSearchModel : PageableSearchRequest<CollectionAuthor>
{
    public AuthorPageableSearchModel()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }

    public int CollectionId { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<CollectionAuthor, bool>>? Filter
    {
        get
        {
            Expression<Func<CollectionAuthor, bool>> filter = x => x.CollectionId == CollectionId && x.Collection.UserId == UserId;

            if (!string.IsNullOrWhiteSpace(Name))
                filter = filter.CombineAnd(x => x.Name.Contains(Name));

            return filter;
        }
    }

    public override Func<IQueryable<CollectionAuthor>, IQueryable<CollectionAuthor>> OrderBy => x => x.OrderBy(y => y.Name);

    public override Dictionary<string, object> ToRouteValues()
    {
        var dic = new Dictionary<string, object>();

        dic.Add(nameof(PageSize), PageSize);

        if (!string.IsNullOrWhiteSpace(Name))
            dic.Add(nameof(Name), Name);

        dic.Add(nameof(CollectionId), CollectionId);

        return dic;
    }
}
