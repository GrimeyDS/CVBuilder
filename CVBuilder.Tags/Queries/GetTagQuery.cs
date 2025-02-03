using CVBuilder.Shared.Models;
using MediatR;

namespace CVBuilder.Tags.Queries;

public sealed record GetTagQuery(int? Id = null) : IRequest<GetTagQueryResponse>;

public sealed record GetTagQueryResponse(IEnumerable<TagModel> Tags);