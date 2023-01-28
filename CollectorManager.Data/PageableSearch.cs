using System.Linq.Expressions;

namespace CollectorManager.Data;

public class PageableSearch<T>
{
    public PageableSearch()
    {
        OrderBy = x => x;
    }

    public PageableSearch(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        OrderBy = x => x;
    }

    public PageableSearch(int pageIndex, int pageSize, Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IQueryable<T>> orderBy)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Filter = filter;
        OrderBy = orderBy;
    }


    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public Expression<Func<T, bool>>? Filter { get; set; }
    public Func<IQueryable<T>, IQueryable<T>> OrderBy { get; set; }
}

