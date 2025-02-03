using MediatR;

namespace CVBuilder.Profiles.Commands;

internal sealed record UpdateProfileCommand(int ProfileId, string FirstName, string LastName, DateTime BirthDate, string Description, string? PictureName, string CurrentRole) : IRequest<UpdateProfileCommandResponse>
{
    public int ProfileId = ProfileId;
    public string FirstName = FirstName;
    public string LastName = LastName;
    public DateTime BirthDate = BirthDate;
    public string Description = Description;
    public string? PictureName = PictureName;
    public string CurrentRole = CurrentRole;
}

internal sealed record UpdateProfileCommandResponse(int Id);