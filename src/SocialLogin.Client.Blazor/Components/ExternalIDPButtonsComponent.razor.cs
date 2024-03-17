namespace SocialLogin.Client.Blazor.Components;

public partial class ExternalIDPButtonsComponent
{
    [Inject] IAuthorizeService AuthorizeService { get; set; }
    [Parameter] public ScopeAction ScopeAction { get; set; }
    [Parameter] public string ReturnUri { get; set; }
    [Parameter] public RenderFragment<string> ChildContent { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> Attributes { get; set; }

    ExternalIDPInfo[] IDPs => AuthorizeService.IDPs;
    string ContentPath => $"_content/{GetType().Assembly.GetName().Name}";
    string ImagePath(ExternalIDPInfo idpInfo) =>
        string.IsNullOrWhiteSpace(idpInfo.ImagePath) ? $"{ContentPath}/images/{idpInfo.ProviderId}.png" : idpInfo.ImagePath;

    async Task BuildUrl(string idp)
    {
        await AuthorizeService.AuthorizeAsync(idp, ScopeAction, ReturnUri);
    }
}