using CollectorManager.Data.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectorManager.Data.Mappings.DomainsMappings;

internal class CollectionFormatMapping : EntityMapping<CollectionFormat>
{
    public override void Configure(EntityTypeBuilder<CollectionFormat> builder)
    {
        builder.ToTable("CollectionFormat");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(50);

        builder.HasOne(x => x.Collection)
            .WithMany()
            .HasForeignKey(x => x.CollectionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
