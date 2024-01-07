namespace SocialLogin.WebApi.Core.Services;
internal class UserService : IUserService
{
    readonly IHttpContextAccessor Context;

    public UserService(IHttpContextAccessor context) => Context = context;

    public bool IsAuthenticated => Context.HttpContext.User.Identity.IsAuthenticated;

    public string Email => Context.HttpContext.User.Identity.Name;

    public string FullName => Context.HttpContext.User.Claims
        .Where(c => c.Type == "FullName")
        .Select(c => c.Value)
        .FirstOrDefault();
}
