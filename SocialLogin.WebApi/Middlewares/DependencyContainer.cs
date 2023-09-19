namespace Microsoft.Extensions.DependencyInjection;
public static partial class DependencyContainer
{
    public static IApplicationBuilder AddSocialLoginExceptionHandler(this IApplicationBuilder app)
    {
        MembershipExceptionHandler.AddExceptionsHandlers(Assembly.GetExecutingAssembly());
        AddHandlers();
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async (context) =>
                await MembershipExceptionHandler
                        .WriteResponse(context, app.ApplicationServices
                                                   .GetRequiredService<IMembershipMessageLocalizer>()));
        });
        return app;
    }

    static void AddHandlers()
    {
        //MembershipExceptionHandler.AddHttp400Handler<LoginUserException>();
        MembershipExceptionHandler.AddHandler(typeof(UnauthorizedAccessException),
            (UnauthorizedAccessException ex, IMembershipMessageLocalizer localizer) =>
                new ProblemDetails
                {
                    Title = nameof(UnauthorizedAccessException),
                    Detail = ex.Message,
                    Status = 401
                }
            );
    }
}