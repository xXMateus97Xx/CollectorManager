using CollectorManager.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectorManager.Data.Mappings.DomainsMappings;

internal class CollectionAuthorMapping : EntityMapping<CollectionAuthor>
{
    public override void Configure(EntityTypeBuilder<CollectionAuthor> builder)
    {
        builder.ToTable("CollectionAuthor");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100);

        builder.HasOne(x => x.Collection)
            .WithMany()
            .HasForeignKey(x => x.CollectionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Items)
            .WithOne(x => x.CollectionAuthor)
            .HasForeignKey(x => x.AuthorId);
    }
}
