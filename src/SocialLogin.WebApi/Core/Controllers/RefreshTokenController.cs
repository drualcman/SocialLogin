namespace SocialLogin.WebApi.Core.Controllers;
internal class RefreshTokenController
{
    public static void Map(WebApplication app, MembershipEndpointsOptions endpointsOptions)
    {
        app.MapPost(endpointsOptions.Refresh,
            async (UserTokensDto userTokens, HttpContext context,
                   IRefreshTokenInputPort inputPort, IRefreshTokenOutputPort outputPort) =>
            {
                context.Response.Headers.TryAdd("Cache-Control", "no-store");
                IResult result;
                try
                {
                    await inputPort.RefreshTokenAsync(userTokens);
                    result = Results.Ok(outputPort.UserTokens);
                }
                catch(RefreshTokenNotFoundException)
                {   
                    result = Results.Unauthorized();
                }
                catch(Exception ex)
                {           
                    result = Results.BadRequest(ex.Message);
                }
                return result;
            });
    }
}
