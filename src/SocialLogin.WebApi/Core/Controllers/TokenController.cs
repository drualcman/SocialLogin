namespace SocialLogin.WebApi.Core.Controllers;
internal class TokenController
{
    public static void Map(WebApplication app, MembershipEndpointsOptions endpointsOptions)
    {
        app.MapPost(endpointsOptions.Token,
            async (HttpContext context, ITokenInputPort inputPort, IOAuthService authService, ILoginOutputPort presenter) =>
            {
                Dictionary<string, string> requestBody = context.Request.Form.ToDictionary(i => i.Key, i => i.Value.ToString());
                TokenRequestInfo requestInfo = authService.GetTokenRequestInfoFromRequestBody(requestBody);
                await inputPort.HandleTokenRequestAsync(requestInfo);
                context.Response.Headers.TryAdd("Cache-Control", "no-store");
                return Results.Ok(presenter.UserTokens);
            });
    }
}
