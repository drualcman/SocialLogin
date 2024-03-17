namespace SocialLogin.WebApi.Core.Controllers;
internal class AuthorizeController
{
    public static void Map(WebApplication app, MembershipEndpointsOptions endpointsOptions)
    {
        app.MapGet(endpointsOptions.Authorize,
            async (HttpRequest request, IAuthorizeInputPort inputPort) =>
            {
                AppClientAuthorizeRequestInfo info = new()
                {
                    ClientId = request.Query["client_id"],
                    RedirectUri = request.Query["redirect_uri"],
                    Scope = request.Query["scope"],
                    State = request.Query["state"],
                    CodeChallenge = request.Query["code_challenge"],
                    CodeChallengeMethod = request.Query["code_challenge_method"],
                    Nonce = request.Query["nonce"]
                };
                string url = await inputPort.GetAuthorizeRequestRedirectUri(info);
                return Results.Redirect(url);
            });
    }
}
