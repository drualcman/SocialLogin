namespace SocialLogin.Client.Blazor.Components;

public class LogoutComponentBase : ComponentBase
{
    [Inject] IUserWebApiGateway Gateway { get; set; }
    [Inject] IAuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] protected IMembershipMessageLocalizer Localizer { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    [Parameter] public EventCallback OnLogOut { get; set; }

    protected async void Logout()
    {
        UserTokensDto storedTokens = await AuthenticationStateProvider.GetUserTokensAsync();
        if(storedTokens is not null)
            await Gateway.LogoutAsync(storedTokens);
        await AuthenticationStateProvider.LogoutAsync();
        if (OnLogOut.HasDelegate)
            await OnLogOut.InvokeAsync();
        else
            NavigationManager.NavigateTo("", true);
    }
}