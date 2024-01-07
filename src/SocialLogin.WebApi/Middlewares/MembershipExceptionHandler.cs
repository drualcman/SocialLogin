namespace SocialLogin.WebApi.Middlewares;
public static class MembershipExceptionHandler
{
    static Dictionary<Type, Delegate> ExceptionHandlers = new();
    public static void AddExceptionsHandlers(Assembly assembly)
    {
        IEnumerable<Type> handlerTypes = assembly
            .GetTypes()
            .Where(t => t.Name.EndsWith("ExceptionHandler") &&
                       t.GetMethods().Any(m => m.Name == "Handle" &&
                                          m.GetParameters().Length == 2));
        foreach(Type type in handlerTypes)
        {
            MethodInfo method = type.GetMethod("Handle");
            Type exceptionType = method.GetParameters()[0].ParameterType;

            ParameterExpression exceptionParameter = Expression.Parameter(exceptionType, "ex");
            ParameterExpression localizaerParameter = Expression.Parameter(typeof(IMembershipMessageLocalizer), "localizer");
            MethodCallExpression bodyParameter = Expression.Call(null, method, exceptionParameter, localizaerParameter);
            LambdaExpression lambda = Expression.Lambda(bodyParameter, exceptionParameter, localizaerParameter);
            ExceptionHandlers.TryAdd(exceptionType, lambda.Compile());
        }
    }

    public static void AddHttp400Handler<T>() =>
        AddHttp400Handler<T>(null);

    public static void AddHttp400Handler<T>(object extensions)
    {
        string exceptionTypeName = typeof(T).Name;
        AddHandler(typeof(T), (T ex, IMembershipMessageLocalizer localizer) =>
        {
            return new ProblemDetails().FromHttp400BadRequest(
            localizer[$"{exceptionTypeName}Message"],
            exceptionTypeName,
            extensions);
        });
    }

    public static void AddHandler(Type exceptionType, Delegate @delegate) =>
        ExceptionHandlers.TryAdd(exceptionType, @delegate);

    public static async Task<bool> WriteResponse(HttpContext context, IMembershipMessageLocalizer localizer)
    {
        IExceptionHandlerFeature exceptionDetail = context.Features.Get<IExceptionHandlerFeature>();
        Exception exceptionError = exceptionDetail?.Error;

        bool handled = true;

        if(exceptionError != null)
        {
            if(ExceptionHandlers.TryGetValue(exceptionError.GetType(), out Delegate handler))
            {
                await WriteProblemDetailsAsync(context,
                                               handler.DynamicInvoke(exceptionError, localizer) as ProblemDetails);
            }
            else
            {
                handled = false;
            }
        }

        return handled;
    }

    public static ProblemDetails FromHttp400BadRequest(this ProblemDetails problem,
        string title, string instance, object extensions = null)
    {
        problem.Status = StatusCodes.Status400BadRequest;
        problem.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        problem.Title = title;
        problem.Instance = instance;
        if(extensions != null)
            problem.Extensions.Add("errors", extensions);
        return problem;
    }

    static async Task WriteProblemDetailsAsync(HttpContext context, ProblemDetails details)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = details.Status.Value;
        Stream stream = context.Response.Body;
        await JsonSerializer.SerializeAsync(stream, details);
    }
}
