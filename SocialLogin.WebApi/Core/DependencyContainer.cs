namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginServices(this IServiceCollection services,
        Action<JwtOptions> jwtOptionsSetter,
        Action<AppClientInfoOptions> appClientInfoOptionsSetter,
        Action<IDPClientInfoOptions> idpClientInfoOptionsSetter)
    {
        services.AddSocialLoginInteractors();
        services.AddSocialLoginPresenters();
        services.AddSocialLoginInternalServices(jwtOptionsSetter, appClientInfoOptionsSetter, idpClientInfoOptionsSetter);
        services.AddOauthService();
        return services;
    }
}