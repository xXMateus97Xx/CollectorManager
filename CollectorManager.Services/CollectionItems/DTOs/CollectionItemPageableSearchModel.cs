using CollectorManager.Data.Domains;
using CollectorManager.Data.Helpers;
using System.Linq.Expressions;

namespace CollectorManager.Services.CollectionItems.DTOs;

public class CollectionItemPageableSearchModel : PageableSearchRequest<CollectionItem>
{
    public CollectionItemPageableSearchModel()
    {
        Name = string.Empty;
        Author = string.Empty;
        Format = new List<int>();
    }

    public string Name { get; set; }
    public string Author { get; set; }
    public List<int> Format { get; set; }

    public int CollectionId { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<CollectionItem, bool>>? Filter
    {
        get
        {
            Expression<Func<CollectionItem, bool>> filter = x => x.CollectionId == CollectionId && x.Collection.UserId == UserId;

            if (!string.IsNullOrWhiteSpace(Name))
                filter = filter.CombineAnd(x => x.Name.Contains(Name));

            if (!string.IsNullOrWhiteSpace(Author))
                filter = filter.CombineAnd(x => x.ItemAuthors.Any(y => y.CollectionAuthor.Name.Contains(Author)));

            if (Format.Count > 0)
                filter = filter.CombineAnd(x => Format.Contains(x.FormatId));

            return filter;
        }
    }

    public override Func<IQueryable<CollectionItem>, IQueryable<CollectionItem>> OrderBy => x => x.OrderBy(y => y.ItemAuthors.First().CollectionAuthor.Name);
}
