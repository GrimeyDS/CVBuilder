using CVBuilder.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CVBuilder.Files.Commands;

internal sealed record UploadBlobCommand(IFormFile File, string Name, ContainerNames ContainerName) : IRequest<UploadBlobCommandResponse>
{
    public IFormFile File = File;
    public string Name = Name;
    public ContainerNames ContainerName = ContainerName;
}

internal sealed record UploadBlobCommandResponse(string BlobName);