namespace SocialLogin.Client.Blazor.Shared.Localizaer;

public class SocialLoginMessasgeLocalizer : IMembershipMessageLocalizer
{
    readonly Dictionary<string, string> Messages_Es = new()
    {
        {MessageKeys.DisplayLoginButtonMessage, "Login" },
        {MessageKeys.DisplayLogoutButtonMessage, "Logout" },

        {MessageKeys.LoginUserExceptionMessage, "Wrong credentials." },
        {MessageKeys.RefreshTokenCompromisedExceptionMessage, "Compromised token." },
        {MessageKeys.RefreshTokenExpiredExceptionMessage, "expired token." },
        {MessageKeys.RefreshTokenNotFoundExceptionMessage, "Token not found." },

        {MessageKeys.MissingCallbackStateParameterExceptionMessage, "Missing Callback."  },
        {MessageKeys.UnableToGetIDPTokensExceptionMessage, "ISP exception."  },
        {MessageKeys.InvalidAuthorizationCodeExceptionMessage , "Invalid code."  },
        {MessageKeys.InvalidRedirectUriExceptionMessage , "Invalid redirect_uri."  },
        {MessageKeys.InvalidClientIdExceptionMessage , "Invalid client_id."  },
        {MessageKeys.InvalidScopeExceptionMessage , "Invalid scope."  },
        {MessageKeys.InvalidCodeVerifierExceptionMessage , "Invalid code_verifier."  },
        {MessageKeys.InvalidScopeActionExceptionMessage , "Invalid action."  },

        {MessageKeys.UnableToGetExternalUserTokensMessage , "Can't process the request."  },
        {MessageKeys.InvalidStateValueMessage , "Invalid state."  },
        {MessageKeys.MissingAuthorizeCallbackParametersMessage , "Invalid params."  },
        {MessageKeys.InvalidNonceValueMessage , "Invalid nonce."  },
    };

    public string this[string key]
    {
        get
        {
            Messages_Es.TryGetValue(key, out string message);
            return string.IsNullOrEmpty(message) ? key : message;
        }
    }
}
