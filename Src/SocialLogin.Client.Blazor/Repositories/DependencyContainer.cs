namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddSocialLoginRepository(this IServiceCollection services)
    {
        services.TryAddScoped<ITokensRepository, TokensRepository>();
        return services;
    }
}