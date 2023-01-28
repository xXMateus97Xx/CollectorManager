using CollectorManager.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectorManager.Data.Mappings.DomainsMappings;

internal class CollectionMapping : EntityMapping<Collection>
{
    public override void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.ToTable("Collection");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Type).IsRequired();

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Collection)
            .HasForeignKey(x => x.CollectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}
