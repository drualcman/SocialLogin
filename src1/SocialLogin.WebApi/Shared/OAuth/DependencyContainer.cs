namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddOauthService(this IServiceCollection services)
    {
        services.TryAddSingleton<IOAuthService, OAuthService>();
        return services;
    }
}