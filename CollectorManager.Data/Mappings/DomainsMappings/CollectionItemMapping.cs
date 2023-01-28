using CollectorManager.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectorManager.Data.Mappings.DomainsMappings;

internal class CollectionItemMapping : EntityMapping<CollectionItem>
{
    public override void Configure(EntityTypeBuilder<CollectionItem> builder)
    {
        builder.ToTable("CollectionItem");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.LastUpdateAt).IsRequired();

        builder.HasOne(x => x.Format)
            .WithMany()
            .HasForeignKey(x => x.FormatId)
            .IsRequired();

        builder.HasMany(x => x.ItemAuthors)
            .WithOne(x => x.CollectionItem)
            .HasForeignKey(x => x.CollectionItemId);

        builder.HasOne(x => x.Collection)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CollectionId);
    }
}
