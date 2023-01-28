using CollectorManager.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectorManager.Data.Mappings.DomainsMappings;

internal class CollectionItemAuthorMapping : EntityMapping<CollectionItemAuthor>
{
    public override void Configure(EntityTypeBuilder<CollectionItemAuthor> builder)
    {
        builder.ToTable("CollectionItemAuthor");

        builder.HasKey(x => new { x.Id, x.CollectionItemId, x.AuthorId });

        builder.HasOne(x => x.CollectionItem)
            .WithMany(x => x.ItemAuthors)
            .HasForeignKey(x => x.CollectionItemId);

        builder.HasOne(x => x.CollectionAuthor)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.AuthorId);
    }
}
