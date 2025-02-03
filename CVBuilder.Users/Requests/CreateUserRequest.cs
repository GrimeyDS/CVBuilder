namespace CVBuilder.Users.Requests;

public sealed class CreateUserRequest(string entraId, string email)
{
    public string EntraId { get; set; } = entraId;
    public string Email { get; set; } = email;
}

