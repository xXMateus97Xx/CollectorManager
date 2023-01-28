using CollectorManager.Services.Auth;
using CollectorManager.Services.CollectionAuthors;
using CollectorManager.Services.CollectionFormats;
using CollectorManager.Services.CollectionItems;
using CollectorManager.Services.Collections;
using CollectorManager.Services.Security;
using CollectorManager.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CollectorManager.Services;

public static class DependencyRegister
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddSettings<SecuritySettings>(nameof(SecuritySettings));

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ICollectionService, CollectionService>();
        services.AddScoped<ICollectionItemFormatService, CollectionItemFormatService>();
        services.AddScoped<ICollectionAuthorService, CollectionAuthorService>();
        services.AddScoped<ICollectionItemService, CollectionItemService>();

        services.AddAutoMapper(typeof(MappingConfiguration));
    }

    public static void AddSettings<T>(this IServiceCollection services, string name) where T : class
    {
        services.AddSingleton(sp => sp.GetRequiredService<IConfiguration>().GetSection(name).Get<T>());
    }
}