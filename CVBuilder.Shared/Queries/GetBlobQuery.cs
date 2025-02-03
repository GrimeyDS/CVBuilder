using MediatR;

namespace CVBuilder.Shared.Queries;

public sealed record GetBlobQuery(string BlobName, string ContainerName) : IRequest<GetBlobQueryResponse>;

public sealed record GetBlobQueryResponse(byte[] FileContent);