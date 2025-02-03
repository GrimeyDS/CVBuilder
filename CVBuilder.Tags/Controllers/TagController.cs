using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Queries;
using CVBuilder.Tags.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Tags.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var query = new GetTagQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTag(int id)
    {
        var query = new GetTagQuery(id);
        var response = await _mediator.Send(query);

        if (!response.Tags.Any())
            return NotFound();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest request)
    {
        var command = new CreateTagCommand(request.Title);
        var response = await _mediator.Send(command);

        return Ok(response.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag([FromBody] UpdateTagRequest request, int id)
    {
        var command = new UpdateTagCommand(id, request.Title);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag([FromBody] DeleteTagRequest request, int id)
    {
        var command = new DeleteTagCommand(id, request.OriginId);
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}
