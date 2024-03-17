namespace SocialLogin.WebApi.Shared.ValueObject;
public class MembershipError
{
    public string Code { get; }
    public string Description { get; }

    public MembershipError(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Code}: {Description}";
    }
}
