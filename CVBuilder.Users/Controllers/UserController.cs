using CVBuilder.Users.Commands;
using CVBuilder.Users.Queries;
using CVBuilder.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVBuilder.Users.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetUserQuery(null);
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var query = new GetUserQuery(id);
        var response = await _mediator.Send(query);

        if (!response.Users.Any())
            return NotFound();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand(request.EntraId, request.Email);
        var response = await _mediator.Send(command);
        
        return Ok(response.Id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request, int id)
    {
        var command = new UpdateUserCommand(id, request.Email, request.EntraId);
        var response = await _mediator.Send(command);

        return Ok(response.Id);
    }

}