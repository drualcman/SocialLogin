namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginInternalServices(this IServiceCollection services,
        Action<JwtOptions> jwtOptionsSetter,
        Action<AppClientInfoOptions> appClientInfoOptionsSetter,
        Action<IDPClientInfoOptions> idpClientInfoOptionsSetter)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IUserService, UserService>();
        services.AddOptions<JwtOptions>().Configure(jwtOptionsSetter);
        services.AddSingleton<IAccessTokenService, AccessTokenService>();
        services.AddOptions<AppClientInfoOptions>().Configure(appClientInfoOptionsSetter);
        services.AddSingleton<IAppClientService, AppClientService>();
        services.AddHttpClient();
        services.AddOptions<IDPClientInfoOptions>().Configure(idpClientInfoOptionsSetter);
        services.AddSingleton<IIDPService, IDPService>();
        services.TryAddSingleton<IMemoryCache, MemoryCache>();
        services.AddSingleton<IOAuthStateService, OAuthStateService>();
        return services;
    }
}