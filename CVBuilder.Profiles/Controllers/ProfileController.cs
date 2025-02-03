using CVBuilder.Profiles.Commands;
using CVBuilder.Shared.Queries;
using CVBuilder.Profiles.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CVBuilder.Shared.Requests;

namespace CVBuilder.Profiles.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IActionResult> GetProfiles()
    {
        var query = new GetProfileQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfile(int id)
    {
        var query = new GetProfileQuery(id);
        var response = await _mediator.Send(query);

        if (!response.Profiles.Any())
            return NotFound();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileRequest request)
    {
        var command = new CreateProfileCommand(request.UserId, request.FirstName, request.LastName, request.BirthDate, request.Description, request.PictureName, request.CurrentRole);
        var response = await _mediator.Send(command);

        return Ok(response.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request, int id)
    {
        var command = new UpdateProfileCommand(id, request.FirstName, request.LastName, request.BirthDate, request.Description, request.PictureName, request.CurrentRole);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(int id)
    {
        var command = new DeleteProfileCommand(id);
        var response = await _mediator.Send(command);
        return Ok(response.Id);
    }

    [HttpPut("UpdateTags/{id}")]
    public async Task<IActionResult> UpdateTagsByProfileId([FromBody] UpdateTagsRequest request, int id)
    {
        var command = new UpdateProfileTagsCommand(id, request.TagIds);
        var response = await _mediator.Send(command);
        return Ok(response.Id);
    }
}
