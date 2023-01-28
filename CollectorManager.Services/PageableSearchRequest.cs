using CollectorManager.Data;
using System.Linq.Expressions;

namespace CollectorManager.Services;

public abstract class PageableSearchRequest<TEntity> where TEntity : BaseEntity
{
    public PageableSearchRequest()
    {
        PageSize = 10;
        PageIndex = 1;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    public abstract Expression<Func<TEntity, bool>>? Filter { get; }
    public virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> OrderBy => x => x;

    public PageableSearch<TEntity> PageableSearch => new PageableSearch<TEntity>(PageIndex, PageSize, Filter, OrderBy);

    public virtual Dictionary<string, object> ToRouteValues() => new Dictionary<string, object>() { { nameof(PageSize), PageSize } };
}
