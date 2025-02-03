using MediatR;

namespace CVBuilder.Profiles.Commands;

internal sealed record CreateProfileCommand(int UserId, string FirstName, string LastName, DateTime BirthDate, string Description, string? PictureName, string CurrentRole) : IRequest<CreateProfileCommandResponse>
{
    public int UserId = UserId;
    public string FirstName = FirstName;
    public string LastName = LastName;
    public DateTime BirthDate = BirthDate;
    public string Description = Description;
    public string? PictureName = PictureName;
    public string CurrentRole = CurrentRole;
}

internal sealed record CreateProfileCommandResponse(int Id);
