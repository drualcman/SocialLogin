namespace SocialLogin.Client.Blazor.Components;

public partial class LogoutComponent
{
    [Inject] IUserWebApiGateway Gateway { get; set; }
    [Inject] IAuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] IMembershipMessageLocalizer Localizer { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    async void Logout()
    {
        UserTokensDto storedTokens = await AuthenticationStateProvider.GetUserTokensAsync();
        if(storedTokens is not null)
            await Gateway.LogoutAsync(storedTokens);
        await AuthenticationStateProvider.LogoutAsync();
        NavigationManager.NavigateTo("");
    }
}