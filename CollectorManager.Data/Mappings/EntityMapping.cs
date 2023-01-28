using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectorManager.Data.Mappings;

internal abstract class EntityMapping<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public void ApplyConfiguration(ModelBuilder modelBuilder) => modelBuilder.ApplyConfiguration(this);

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}
