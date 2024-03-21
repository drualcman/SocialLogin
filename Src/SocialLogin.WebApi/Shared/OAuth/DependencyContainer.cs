namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginOauthService(this IServiceCollection services)
    {
        services.TryAddSingleton<IOAuthService, OAuthService>();
        return services;
    }
}