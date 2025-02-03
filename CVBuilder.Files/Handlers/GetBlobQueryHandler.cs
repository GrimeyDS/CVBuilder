using Azure.Storage.Blobs;
using CVBuilder.Files.Constants;
using MediatR;
using CVBuilder.Shared.Queries;
using Microsoft.Extensions.Configuration;

namespace CVBuilder.Files.Handlers;

internal class GetBlobQueryHandler : IRequestHandler<GetBlobQuery, GetBlobQueryResponse>
{
    private readonly BlobServiceClient _blobServiceClient;

    public GetBlobQueryHandler(IConfiguration configuration)
    {
        string? connectionString = configuration["StorageAccount:ConnectionString"];

        if (string.IsNullOrEmpty(connectionString))
            throw new Exception(FileErrorMessages.AzureStorageConnectionStringError);
        _blobServiceClient = new BlobServiceClient(connectionString);
    }

    public async Task<GetBlobQueryResponse> Handle(GetBlobQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var blobClient = _blobServiceClient.GetBlobContainerClient(request.ContainerName).GetBlobClient(request.BlobName);
            if (await blobClient.ExistsAsync(cancellationToken))
            {
                var response = await blobClient.DownloadContentAsync(cancellationToken);
                var content = response.Value.Content.ToArray();
                return new GetBlobQueryResponse(content);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(FileErrorMessages.ErrorGettingFile, ex);
        }
        throw new Exception(FileErrorMessages.FileNotFound);
    }

}