namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginBlazorServices(this IServiceCollection services,
        Action<UserEndpointsOptions> userEndpointOptionsSetter,
        Action<AppOptions> appOptionsOptionsSetter)
    {
        services.AddAuthorizationCore();
        services.AddSocialLoginGateways(userEndpointOptionsSetter);
        services.AddSocialLoginServices(appOptionsOptionsSetter);
        services.AddSocialLoginAuthenticationStateProvider();
        services.AddSocialLoginRepository();
        services.AddSocialLoginOauthService();
        return services;
    }
}