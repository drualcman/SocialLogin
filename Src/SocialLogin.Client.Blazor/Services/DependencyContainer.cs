namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginServices(this IServiceCollection services,
        Action<AppOptions> appOptionsOptionsSetter)
    {
        services.AddOptions<AppOptions>().Configure(appOptionsOptionsSetter);
        services.AddScoped<IAuthorizeService, AuthorizeService>();
        services.TryAddScoped<IOAuthStateService, OAuthStateService>();
        services.AddHttpClient<TokenService>()
            .AddSocialLoginExceptionDelegatingHandler();
        return services;
    }
}