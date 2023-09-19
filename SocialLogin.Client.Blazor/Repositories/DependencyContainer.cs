namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IServiceCollection AddMembershipRepository(this IServiceCollection services)
    {
        services.AddScoped<ITokensRepository, TokensRepository>();
        return services;
    }
}