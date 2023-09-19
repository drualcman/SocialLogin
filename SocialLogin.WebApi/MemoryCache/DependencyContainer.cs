namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddRefreshTokenMemoryCacheService(this IServiceCollection services)
    {
        services.TryAddSingleton<IMemoryCache, MemoryCache>();
        services.TryAddSingleton<IRefreshTokenService, RefreshTokenService>();
        return services;
    }
}