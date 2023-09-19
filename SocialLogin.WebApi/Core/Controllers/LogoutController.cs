namespace SocialLogin.WebApi.Core.Controllers;
internal class LogoutController
{
    public static void Map(WebApplication app, MembershipEndpointsOptions endpointsOptions)
    {
        app.MapPost(endpointsOptions.Logout,
            async (UserTokensDto userTokens, ILogoutInputPort inputPort) =>
            {
                await inputPort.LogoutAsync(userTokens);
                return Results.Ok();
            });
    }
}
