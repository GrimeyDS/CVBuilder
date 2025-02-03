using CVBuilder.Projects.Commands;
using CVBuilder.Projects.Queries;
using CVBuilder.Projects.Requests;
using CVBuilder.Shared.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Projects.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IActionResult> GetProjects()
    {
        var query = new GetProjectsQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var query = new GetProjectsQuery(id);
        var response = await _mediator.Send(query);

        if (!response.Projects.Any())
            return NotFound();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        var command = new CreateProjectCommand(request.ProfileId, request.Title, request.Customer, request.StartDate, request.EndDate, request.Description, request.PictureName);
        var response = await _mediator.Send(command);

        return Ok(response.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request, int id)
    {
        var command = new UpdateProjectCommand(id, request.Title, request.Customer, request.StartDate, request.EndDate, request.Description, request.PictureName);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var command = new DeleteProjectCommand(id);
        var response = await _mediator.Send(command);

        return Ok(response.Id);
    }

    [HttpPut("UpdateTags/{id}")]
    public async Task<IActionResult> UpdateTagsByProjectId([FromBody] UpdateTagsRequest request, int id)
    {
        var command = new UpdateProjectTagsCommand(id, request.TagIds);
        var response = await _mediator.Send(command);
        return Ok(response.Id);
    }
}
