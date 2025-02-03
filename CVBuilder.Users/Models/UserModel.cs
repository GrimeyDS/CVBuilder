namespace CVBuilder.Users.Models;

internal class UserModel
{
    public int Id { get; set; }
    public string EntraId { get; set; } = default!;
    public string Email { get; set; } = default!;
}