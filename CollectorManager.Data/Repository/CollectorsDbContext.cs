using CollectorManager.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CollectorManager.Data.Repository;

public class CollectorsDbContext : DbContext
{
    public CollectorsDbContext(DbContextOptions<CollectorsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var mappingTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityMapping<>)));

        foreach (var mappingType in mappingTypes)
        {
            var mapping = (IMappingConfiguration?)Activator.CreateInstance(mappingType);
            mapping?.ApplyConfiguration(modelBuilder);
        }

        base.OnModelCreating(modelBuilder);
    }
}
