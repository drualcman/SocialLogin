namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginMessasgeLocalizer(this IServiceCollection services)
    {
        services.TryAddSingleton<IMembershipMessageLocalizer, MembershipMessasgeLocalizer>();
        return services;
    }
}