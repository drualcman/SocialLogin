# SocialLogin.Client.Blazor
Components for the Blazor Client or MVC Razor Pages

## How to use
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