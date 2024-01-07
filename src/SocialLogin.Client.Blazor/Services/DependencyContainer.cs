namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddMembershipServices(this IServiceCollection services,
        Action<AppOptions> appOptionsOptionsSetter)
    {
        services.AddOptions<AppOptions>().Configure(appOptionsOptionsSetter);
        services.AddScoped<IAuthorizeService, AuthorizeService>();
        services.AddScoped<IOAuthStateService, OAuthStateService>();
        services.AddHttpClient<TokenService>()
            .AddExceptionDelegatingHandler();
        return services;
    }
}