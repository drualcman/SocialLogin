namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IHttpClientBuilder AddExceptionDelegatingHandler(this IHttpClientBuilder builder)
    {
        builder.Services.TryAddTransient<ExceptionDelegatingHandler>();
        builder.AddHttpMessageHandler<ExceptionDelegatingHandler>();
        return builder;
    }

    public static IHttpClientBuilder AddMembershipBearerTokenHandler(this IHttpClientBuilder builder)
    {
        builder.Services.TryAddTransient<BearerTokenHandler>();
        builder.AddHttpMessageHandler<BearerTokenHandler>();
        return builder;
    }
}