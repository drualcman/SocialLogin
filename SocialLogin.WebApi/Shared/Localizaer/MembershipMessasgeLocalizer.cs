using SocialLogin.WebApi.Shared.Constants;
using SocialLogin.WebApi.Shared.Interfaces;

namespace SocialLogin.WebApi.Shared.Localizaer;

public class MembershipMessasgeLocalizer : IMembershipMessageLocalizer
{
    readonly Dictionary<string, string> Messages_Es = new()
    {
        {MessageKeys.RegisterUserExceptionMessage, "Error al registrar el usuario." },
        {MessageKeys.LoginUserExceptionMessage, "Las credenciales proporcionadas son incorrectas." },
        {MessageKeys.RefreshTokenCompromisedExceptionMessage, "El token de actualizacion fue comprometido." },
        {MessageKeys.RefreshTokenExpiredExceptionMessage, "El token de actualizacion ha expirado." },
        {MessageKeys.RefreshTokenNotFoundExceptionMessage, "El token de actualizacion no fue encontrado." },

        {MessageKeys.MissingCallbackStateParameterExceptionMessage, "No se recibio el parámetro State requerido."  },
        {MessageKeys.UnableToGetIDPTokensExceptionMessage, "No fue posible obtener la respuesta del servidor de identidad externo."  },
        {MessageKeys.InvalidAuthorizationCodeExceptionMessage , "El código de autorización del cliente no es valido."  },
        {MessageKeys.InvalidRedirectUriExceptionMessage , "El valor redirect_uri no es válido."  },
        {MessageKeys.InvalidClientIdExceptionMessage , "El valor client_id no es válido."  },
        {MessageKeys.InvalidScopeExceptionMessage , "El valor scope no es válido."  },
        {MessageKeys.InvalidCodeVerifierExceptionMessage , "El valor code_verifier no es válido."  },
        {MessageKeys.InvalidScopeActionExceptionMessage , "La acción solicitada no es válida."  },

        {MessageKeys.UnableToGetExternalUserTokensMessage , "No se pudo realizar la operacion solicitada."  },
        {MessageKeys.InvalidStateValueMessage , "Se recibio un valor state incorrecto."  },
        {MessageKeys.MissingAuthorizeCallbackParametersMessage , "No se receibieron los parametros esperados."  },
        {MessageKeys.InvalidNonceValueMessage , "El valor nonce no es valido."  },
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
