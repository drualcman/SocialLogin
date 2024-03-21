namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginRefreshTokenMemoryCacheService(this IServiceCollection services)
    {
        services.TryAddSingleton<IMemoryCache, MemoryCache>();
        services.TryAddScoped<IRefreshTokenService, RefreshTokenService>();
        return services;
    }
}