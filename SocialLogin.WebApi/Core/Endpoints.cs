namespace Microsoft.AspNetCore.Builder;
public static class Endpoints
{
    public static WebApplication UseSocialLoginEndpoints(this WebApplication app,
        Action<MembershipEndpointsOptions> userEndpointOptionsSetter = null)
    {
        MembershipEndpointsOptions endpointsOptions = new();
        if(userEndpointOptionsSetter != null)
            userEndpointOptionsSetter(endpointsOptions);
        LogoutController.Map(app, endpointsOptions);
        RefreshTokenController.Map(app, endpointsOptions);
        AuthorizeController.Map(app, endpointsOptions);
        AuthorizeCallbackController.Map(app, endpointsOptions);
        TokenController.Map(app, endpointsOptions);
        return app;
    }
}
