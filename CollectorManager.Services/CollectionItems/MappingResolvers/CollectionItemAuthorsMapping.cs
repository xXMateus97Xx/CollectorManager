using AutoMapper;
using CollectorManager.Data.Domains;
using CollectorManager.Services.CollectionItems.DTOs;

namespace CollectorManager.Services.CollectionItems.MappingResolvers;

internal class CollectionItemAuthorsMapping : IValueResolver<CollectionItemModel, CollectionItem, ICollection<CollectionItemAuthor>>
{
    public ICollection<CollectionItemAuthor> Resolve(CollectionItemModel source, CollectionItem destination, ICollection<CollectionItemAuthor> destMember, ResolutionContext context)
    {
        var result = new HashSet<CollectionItemAuthor>();

        var newAuthors = source.AuthorsIds
            .Where(x => !destMember.Select(x => x.Id).Contains(x))
            .Select(x => new CollectionItemAuthor { AuthorId = x, CollectionItemId = source.Id });

        result.UnionWith(newAuthors);

        var authorsToKeep = destination.ItemAuthors
            .Where(x => source.AuthorsIds.Contains(x.Id));

        result.UnionWith(authorsToKeep);

        return result;
    }
}
