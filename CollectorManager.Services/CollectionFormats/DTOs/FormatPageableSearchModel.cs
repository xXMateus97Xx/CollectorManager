using CollectorManager.Data.Domains;
using System.Linq.Expressions;

namespace CollectorManager.Services.CollectionFormats.DTOs;

public class FormatPageableSearchModel : PageableSearchRequest<CollectionFormat>
{
    public FormatPageableSearchModel()
    {
        Name = string.Empty;
    }

    public string Name { get; set; }

    public int CollectionId { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<CollectionFormat, bool>>? Filter => x => x.CollectionId == CollectionId && x.Collection.UserId == UserId;

    public override Func<IQueryable<CollectionFormat>, IQueryable<CollectionFormat>> OrderBy => x => x.OrderBy(y => y.Name);

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
