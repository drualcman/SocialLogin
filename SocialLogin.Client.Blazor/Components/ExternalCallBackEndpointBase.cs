namespace SocialLogin.Client.Blazor.Components;
public class ExternalCallBackEndpointBase : ComponentBase
{
    [Inject] TokenService TokenService { get; set; }
    [Inject] IAuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Inject] IMembershipMessageLocalizer Localizer { get; set; }
    [Parameter, SupplyParameterFromQuery] public string State { get; set; }
    [Parameter, SupplyParameterFromQuery] public string Code { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            TokenServiceResponse response = await TokenService.GetTokensAsync(State, Code);
            string action = response.Scope[..response.Scope.IndexOf("_")];
            if(Enum.TryParse(action, out ScopeAction scopeAction))
            {
                switch(scopeAction)
                {
                    case ScopeAction.Login:
                    case ScopeAction.Register:
                        await AuthenticationStateProvider.LoginAsync(response.Tokens);
                        break;
                }
            }
            if(string.IsNullOrWhiteSpace(response.ReturnUri))
                NavigationManager.NavigateTo("");
            else
                NavigationManager.NavigateTo(response.ReturnUri);
        }
        catch(HttpRequestException ex)
        {
            IEnumerable<MembershipError> errors = null;
            if(ex.Data.Contains("Erros"))
                errors = ex.Data["Errors"] as IEnumerable<MembershipError>;
            OnError(string.IsNullOrWhiteSpace(ex.Message) ? Localizer[MessageKeys.UnableToGetExternalUserTokensMessage] : ex.Message, errors);
        }
        catch(Exception ex)
        {
            OnError(string.IsNullOrWhiteSpace(ex.Message) ? Localizer[MessageKeys.UnableToGetExternalUserTokensMessage] : ex.Message, null);
        }
    }

    protected virtual void OnError(string message, IEnumerable<MembershipError> errors) { }

}
