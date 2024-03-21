namespace SocialLogin.WebApi.Shared.Localizaer;

public class SocialLoginMembershipApiMessasgeLocalizer : IMembershipMessageLocalizer
{
    readonly Dictionary<string, string> Messages_Es = new()
    {
        {MessageKeys.LoginUserExceptionMessage, "This user don't have access to this application." },
        {MessageKeys.RefreshTokenCompromisedExceptionMessage, "Compromised token." },
        {MessageKeys.RefreshTokenExpiredExceptionMessage, "Expired token." },
        {MessageKeys.RefreshTokenNotFoundExceptionMessage, "Token not found." },

        {MessageKeys.MissingCallbackStateParameterExceptionMessage, "Invalid state parameter."  },
        {MessageKeys.UnableToGetIDPTokensExceptionMessage, "Unable to get token."  },
        {MessageKeys.InvalidAuthorizationCodeExceptionMessage , "Invalid Authorization code."  },
        {MessageKeys.InvalidRedirectUriExceptionMessage , "invalid redirect uri."  },
        {MessageKeys.InvalidClientIdExceptionMessage , "Invalid client_id."  },
        {MessageKeys.InvalidScopeExceptionMessage , "Invalid scope."  },
        {MessageKeys.InvalidCodeVerifierExceptionMessage , "Invalid code_verifier."  },
        {MessageKeys.InvalidScopeActionExceptionMessage , "Invalid action."  },

        {MessageKeys.UnableToGetExternalUserTokensMessage , "Unagle to get external user."  },
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
