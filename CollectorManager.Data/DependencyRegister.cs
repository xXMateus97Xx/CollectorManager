using CollectorManager.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CollectorManager.Data;

public static class DependencyRegister
{
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddDbContext<CollectorsDbContext>(options =>
        {
            options.UseSqlite("Data Source=collectors.db");
        });

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }
}
