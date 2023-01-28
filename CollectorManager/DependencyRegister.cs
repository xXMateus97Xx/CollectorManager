using CollectorManager.Services.Auth;

namespace CollectorManager;

public static class DependencyRegister
{
    public static void AddCookieAuthentication(this IServiceCollection services)
    {
        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = AuthenticationConstants.AuthenticationScheme;
            options.DefaultScheme = AuthenticationConstants.AuthenticationScheme;
        });

        authenticationBuilder.AddCookie(AuthenticationConstants.AuthenticationScheme, options =>
        {
            options.Cookie.Name = "CollectorsManagerAuthentication";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.LoginPath = "/login";
            options.AccessDeniedPath = "/denied";
        });
    }
}
