using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Constants;
using CVBuilder.Experiences.Queries;
using CVBuilder.Experiences.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Experiences.Controllers;

[ApiController]
[Route("[controller]")]
public class ExperienceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExperienceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetExperiences()
    {
        var query = new GetExperienceQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExperience(int id)
    {
        var query = new GetExperienceQuery(id);
        var response = await _mediator.Send(query);

        if (!response.Experiences.Any())
            return NotFound();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExperience([FromBody] CreateExperienceRequest request)
    {
        if (!Enum.TryParse<ExperienceTypes>(request.Type, true, out var type))
            throw new Exception(ErrorMessages.InvalidType);

        var command = new CreateExperienceCommand(request.ProfileId, request.Title, request.Description, type, request.StartDate, request.EndDate);
        var response = await _mediator.Send(command);

        return Ok(response.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExperience([FromBody] UpdateExperienceRequest request, int id)
    {
        var command = new UpdateExperienceCommand(id, request.ProfileId, request.Title, request.Description, request.StartDate, request.EndDate);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExperience(int id)
    {
        var command = new DeleteExperienceCommand(id);
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}
