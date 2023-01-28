using CollectorManager.Data.Domains;
using System.Linq.Expressions;

namespace CollectorManager.Services.CollectionFormats.DTOs;

public class FormatSearchOneModel : SearchOneRequest<CollectionFormat>
{
    public int CollectionId { get; set; }

    internal int UserId { get; set; }
    public override Expression<Func<Data.Domains.CollectionFormat, bool>> Filter => x => x.Id == Id && x.CollectionId == CollectionId && x.Collection.UserId == UserId;
}
