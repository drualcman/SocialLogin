namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginInteractors(this IServiceCollection services)
    {
        services.AddScoped<ILogoutInputPort, LogoutInteractor>();
        services.AddScoped<IRefreshTokenInputPort, RefreshTokenInteractor>();
        services.AddScoped<IAuthorizeInputPort, AuthorizeInteractor>();
        services.AddScoped<IAuthorizeCallbackInputPort, AuthorizeCallbackInteractor>();
        services.AddScoped<ITokenInputPort, TokenInteractor>();
        return services;
    }
}