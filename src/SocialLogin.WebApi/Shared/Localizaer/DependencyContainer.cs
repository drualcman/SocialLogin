namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginApiMessasgeLocalizer(this IServiceCollection services)
    {
        services.TryAddSingleton<IMembershipMessageLocalizer, MembershipApiMessasgeLocalizer>();
        return services;
    }
}