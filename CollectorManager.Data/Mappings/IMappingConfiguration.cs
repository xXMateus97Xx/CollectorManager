using Microsoft.EntityFrameworkCore;

namespace CollectorManager.Data.Mappings;

internal interface IMappingConfiguration
{
    void ApplyConfiguration(ModelBuilder modelBuilder);
}
