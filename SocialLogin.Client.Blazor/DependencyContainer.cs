namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginBlazorServices(this IServiceCollection services,
        Action<UserEndpointsOptions> userEndpointOptionsSetter,
        Action<AppOptions> appOptionsOptionsSetter)
    {
        services.AddAuthorizationCore();
        services.AddMembershipAuthenticationStateProvider();
        services.AddMembershipGateways(userEndpointOptionsSetter);
        services.AddMembershipRepository();
        services.AddMembershipServices(appOptionsOptionsSetter);
        services.AddOauthService();
        return services;
    }
}