using Microsoft.AspNetCore.Http;

namespace CVBuilder.Files.Models;

public sealed class UploadBlobRequest
{
    public required IFormFile File { get; set; }
    public string Name { get; set; } = string.Empty;
    public required string ContainerName { get; set; }
}
