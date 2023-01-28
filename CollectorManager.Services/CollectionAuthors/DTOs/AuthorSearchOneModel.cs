using CollectorManager.Data.Domains;
using System.Linq.Expressions;

namespace CollectorManager.Services.CollectionAuthors.DTOs;

public class AuthorSearchOneModel : SearchOneRequest<CollectionAuthor>
{
    public int CollectionId { get; set; }

    internal int UserId { get; set; }

    public override Expression<Func<CollectionAuthor, bool>> Filter => x => x.Id == Id && x.CollectionId == CollectionId && x.Collection.UserId == UserId;
}
