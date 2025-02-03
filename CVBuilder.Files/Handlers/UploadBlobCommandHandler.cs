using Azure.Storage.Blobs;
using CVBuilder.Files.Constants;
using CVBuilder.Files.Commands;
using MediatR;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;
using Microsoft.Extensions.Configuration;

namespace CVBuilder.Files.Handlers;

internal class UploadBlobCommandHandler : IRequestHandler<UploadBlobCommand, UploadBlobCommandResponse>
{
    private readonly BlobServiceClient _blobServiceClient;

    public UploadBlobCommandHandler(IConfiguration configuration)
    {
        string? connectionString = configuration["StorageAccount:ConnectionString"];

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception(FileErrorMessages.AzureStorageConnectionStringError);

        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public async Task<UploadBlobCommandResponse> Handle(UploadBlobCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var blobName = Path.GetFileNameWithoutExtension(request.Name);

            if (!Guid.TryParse(blobName, out _))
                blobName = Guid.NewGuid().ToString();

            blobName += ".png";

            var blobClient = _blobServiceClient.GetBlobContainerClient(request.ContainerName.ToString().ToLower()).GetBlobClient(blobName);

            using var image = Image.Load(request.File.OpenReadStream());
            image.Mutate(x => x.Resize(720, 720, KnownResamplers.Lanczos3));

            using var uploadFileStream = new MemoryStream();
            await image.SaveAsync(uploadFileStream, new PngEncoder());

            uploadFileStream.Position = 0;

            await blobClient.UploadAsync(uploadFileStream, overwrite: true);
            uploadFileStream.Close();

            return new UploadBlobCommandResponse(blobClient.Name);
        }
        catch (Exception ex)
        {
            throw new Exception(FileErrorMessages.ErrorUploadingFile, ex);
        }
    }

}
