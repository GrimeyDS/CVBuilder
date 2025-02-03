using CVBuilder.Shared.Constants;

namespace CVBuilder.Files.Models;

public sealed class GetBlobRequest
{
    public required string Name { get; set; }
    public required ContainerNames ContainerName { get; set; }
}
