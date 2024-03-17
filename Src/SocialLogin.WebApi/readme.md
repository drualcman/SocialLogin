# SocialLogin.WebApi
Object to add in a web api to use OAuth and OpenId

### Considerations
If you want to use server side, must be create your own implementatino about

* IUserManagerService
* ITokensRepository
* IOAuthStateService

## How to use
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


