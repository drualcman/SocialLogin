namespace SocialLogin.WebApi.Entities;
public class UserEntity
{
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string FullName => $"{FirstName} {LastName}";
    public IEnumerable<Claim> Claims { get; set; }

    public UserEntity(string email, string firstName, string lastName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}
