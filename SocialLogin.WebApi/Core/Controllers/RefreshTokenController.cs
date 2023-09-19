namespace SocialLogin.WebApi.Core.Controllers;
internal class RefreshTokenController
{
    public static void Map(WebApplication app, MembershipEndpointsOptions endpointsOptions)
    {
        app.MapPost(endpointsOptions.Refresh,
            async (UserTokensDto userTokens, HttpContext context,
                   IRefreshTokenInputPort inputPort, IRefreshTokenOutputPort outputPort) =>
            {
                await inputPort.RefreshTokenAsync(userTokens);
                context.Response.Headers.Add("Cache-Control", "no-store");
                return Results.Ok(outputPort.UserTokens);
            });
    }
}
