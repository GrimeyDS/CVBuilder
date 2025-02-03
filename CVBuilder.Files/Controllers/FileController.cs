using CVBuilder.Files.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CVBuilder.Files.Commands;
using CVBuilder.Shared.Queries;
using CVBuilder.Shared.Constants;
using CVBuilder.Files.Constants;

namespace CVBuilder.Files.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> UploadPicture([FromForm] UploadBlobRequest request)
    {
        if (!Enum.TryParse<ContainerNames>(request.ContainerName, true, out var containerName))
            throw new Exception(FileErrorMessages.InvalidContainerName);

        var command = new UploadBlobCommand(request.File, request.Name, containerName);
        var response = await _mediator.Send(command);

        return Ok(response.BlobName);
    }

    [HttpGet]
    public async Task<IActionResult> GetPicture(string blobName, string containerName)
    {
        var query = new GetBlobQuery(blobName, containerName);
        var response = await _mediator.Send(query);

        return File(response.FileContent, "image/png");
    }
}
