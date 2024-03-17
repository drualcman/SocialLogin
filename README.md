# SocialLogin
Created from a training with Miguel Muñoz Serafin. With this 2 projects you can easy implement OAuth authentication using any IDP support OAuth and OpenID.

## SocialLogin.Client.Blazor
Project to manage the Social Login for Blazor WebAssembly, or similar.

### How to use
```csharp
builder.Services.AddSocialLoginBlazorServices( 
    userEndpoints => builder.Configuration.GetSection(UserEndpointsOptions.SectionKey).Bind(userEndpoints),
    appOptions => builder.Configuration.GetSection(AppOptions.SectionKey).Bind(appOptions));
builder.Services.AddSocialLoginMessasgeLocalizer();
```
In appsetting.json add
```json
{
  "UserEndpoints": {
    "WebApiBaseAddress": "https://domain.com"
  },
  "appoptions": {
    "AuthorizationEndpoint": "https://domain.com/oauth2/authorize",
    "ClientId": "DDLive",
    "RedirectUri": "https://domain.com/oauth2/callback",
    "TokenEndpoint": "https://domain.com/oauth2/token",
    "IDPs": [
      {
        "ProviderId": "DDLive"
      }
    ]

  }
}
```
Razor login component
```razor
@using SocialLogin.Client.Blazor.Components
<ExternalIDPButtonsComponent ScopeAction=ScopeAction.Login ReturnUri="dashboard" Context="Provider">
    <span class="button is-primary" @onclick="()=> IsActive = !IsActive">
        Login
    </span>
</ExternalIDPButtonsComponent>
```
Razor callback page
```razor
@page "/oauth2/callback"
@using SocialLogin.Client.Blazor.Components
@inherits ExternalCallBackEndpointBase

<div class="columns is-mobile">
    <div class="column has-text-centered">
        <h3>@Message</h3>
    </div>
</div>

@code {
    string Message = "Redirecting...";

    protected override void OnError(string message, IEnumerable<SocialLogin.Client.Entities.ValueObjects.MembershipError> errors)
    {
        Message = message;
    }
}
```

## SocialLogin.WebApi
Project to manage the private action VS the IDP and then protect your login action from the Blazor WebAssembly Client

#### Considerations
If you want to use server side, must be create your own implementatino about

* IUserManagerService
* ITokensRepository
* IOAuthStateService

### How to use
```csharp
builder.Services.AddSocialLoginServices(
    jwtOptions => builder.Configuration
                        .GetSection(JwtOptions.SectionKey)
                        .Bind(jwtOptions),
    appClientInfoOptions => appClientInfoOptions.AppClients =
                            builder.Configuration.GetSection(
                                AppClientInfoOptions.SectionKey).Get<AppClientInfo[]>(),
    iDPClientInfoOptions => iDPClientInfoOptions.IDPClients =
                            builder.Configuration.GetSection(IDPClientInfoOptions.SectionKey)
                            .Get<IDPClientInfo[]>());
builder.Services.AddRefreshTokenMemoryCacheService();
builder.Services.AddSocialLoginApiMessasgeLocalizer();

app.AddSocialLoginExceptionHandler();
app.UseSocialLoginEndpoints(membershipEndpoints =>
    builder.Configuration.GetSection(MembershipEndpointsOptions.SectionKey)
                         .Bind(membershipEndpoints));
```
In appsetting.json add
```
{
  "UserEndpoints": {
    "WebApiBaseAddress": "https://domain.com"
  },
  "Jwt": {
    "SecurityKey": "",
    "ValidIssuer": "https://google.com",
    "ValidAudience": "api://domain",
    "ExpireInMinutes": 10080,
    "ClockSkewInMinutes": 2,
    "RefreshTokenExpireInMinutes": 20160,
    "ValidateIssuer": true,
    "ValidateIssuerSigninLey": true,
    "ValidateAudience": true,
    "ValidateLifeTime": true
  },
  "MembershipEndpoints": {
    "WebApiBaseAddress": "https://domain.com"
  },
  "OAth2": {
    "AppClients": [
      {
        "ClientID": "DDLive",
        "RedirectUri": "https://domain.com/oauth2/callback"
      }
    ],
    "IDPClients": [
      {
        "ProviderId": "DDLive",
        "AuthorizeEndpoint": "https://google.com/connect/authorize",
        "TokenEndpoint": "https://google/connect/token",
        "ClientId": "BlazorClient",
        "ClientSecret": "",
        "RedirectUri": "https://domain.com/oauth2/authorizecallback",
        "SupportsS256CodeChallengeMethod": true,
        "Scope": "openid profile email domain.read domain.write roles"
      }
    ]
  }}
```
