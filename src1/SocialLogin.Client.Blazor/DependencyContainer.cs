namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginBlazorServices(this IServiceCollection services,
        Action<UserEndpointsOptions> userEndpointOptionsSetter,
        Action<AppOptions> appOptionsOptionsSetter)
    {
        services.AddAuthorizationCore();
        services.AddMembershipGateways(userEndpointOptionsSetter);
        services.AddMembershipServices(appOptionsOptionsSetter);
        services.AddMembershipAuthenticationStateProvider();
        services.AddMembershipRepository();
        services.AddOauthService();
        return services;
    }
}