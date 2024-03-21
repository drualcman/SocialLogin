namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginGateways(this IServiceCollection services, 
        Action<UserEndpointsOptions> userEndpointOptionsSetter)
    {
        services.AddOptions<UserEndpointsOptions>().Configure(userEndpointOptionsSetter);
        services.AddHttpClient<IUserWebApiGateway, UserWebApiGateway>().AddSocialLoginExceptionDelegatingHandler();
        return services;
    }
}