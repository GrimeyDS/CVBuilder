
namespace CVBuilder.Users.Requests;


public sealed class UpdateUserRequest(string email, string entraId)
{
    public string Email { get; set; } = email;

    public string EntraId { get; set; } = entraId;
}
