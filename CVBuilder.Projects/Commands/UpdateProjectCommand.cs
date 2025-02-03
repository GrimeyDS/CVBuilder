using MediatR;

namespace CVBuilder.Projects.Commands;

internal sealed record UpdateProjectCommand(int ProjectId, string Title, string Customer, DateTime StartDate, DateTime? EndDate, string Description, string? PictureName) : IRequest<UpdateProjectCommandResponse>
{
    public int ProjectId = ProjectId;
    public string Title = Title;
    public string Customer = Customer;
    public DateTime StartDate = StartDate;
    public DateTime? EndDate = EndDate;
    public string Description = Description;
    public string? PictureName = PictureName;
}

internal sealed record UpdateProjectCommandResponse(int Id);
