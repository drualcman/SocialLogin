namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddMembershipAuthenticationStateProvider(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationStateProvider, JWTAuthenticationStateProvider>();
        services.AddScoped(provider => (AuthenticationStateProvider)provider.GetRequiredService<IAuthenticationStateProvider>());
        return services;
    }
}