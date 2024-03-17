namespace SocialLogin.Client.HttpHandlers.HttpMessageHandlers;
internal class ExceptionDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        if(!response.IsSuccessStatusCode)
        {
            string errorMessage = await response.Content.ReadAsStringAsync();
            string source = null;
            string message = null;
            IEnumerable<MembershipError> errors = null;
            bool isValidProblemDetails = false;

            try
            {
                JsonElement jsonResponse = JsonSerializer.Deserialize<JsonElement>(errorMessage);
                if(jsonResponse.TryGetProperty("instance", out JsonElement instanceValue))
                {
                    string value = instanceValue.ToString();
                    if(value.ToLower().StartsWith("problemdetails"))
                    {
                        source = value;
                        if(jsonResponse.TryGetProperty("title", out JsonElement titleValue))
                            message = titleValue.ToString();
                        if(jsonResponse.TryGetProperty("errors", out JsonElement errorValue))
                            errors = errorValue.Deserialize<IEnumerable<MembershipError>>();
                        isValidProblemDetails = true;
                    }
                }
            }
            catch { }
            if(!isValidProblemDetails)
            {
                message = errorMessage;
                source = null;
                errors = null;
            }
            HttpRequestException ex = new HttpRequestException(message, null, response.StatusCode);
            ex.Source = source;
            if(errors != null)
                ex.Data.Add("Errors", errors);
            throw ex;
        }
        return response;
    }
}
