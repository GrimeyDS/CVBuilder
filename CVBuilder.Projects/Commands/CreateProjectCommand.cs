using MediatR;

namespace CVBuilder.Projects.Commands;

internal sealed record CreateProjectCommand(int ProfileId, string Title, string Customer, DateTime StartDate, DateTime? EndDate, string Description, string? PictureName) : IRequest<CreateProjectCommandResponse>
{
    public int ProfileId = ProfileId;
    public string Title = Title;
    public string Customer = Customer;
    public DateTime StartDate = StartDate;
    public DateTime? EndDate = EndDate;
    public string Description = Description;
    public string? PictureName = PictureName;
}

internal sealed record CreateProjectCommandResponse(int Id);
