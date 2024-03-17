namespace SocialLogin.WebApi.Core.Controllers;
internal class AuthorizeCallbackController
{
    public static void Map(WebApplication app, MembershipEndpointsOptions endpointsOptions)
    {
        app.MapGet(endpointsOptions.AuthorizeCallback,
            async ([FromQuery] string state, [FromQuery] string code, IAuthorizeCallbackInputPort inputPort) =>
                Results.Redirect(await inputPort.HandleCallback(state, code)));
    }
}
