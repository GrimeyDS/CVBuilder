using CVBuilder.Shared.Models;
using MediatR;

namespace CVBuilder.Shared.Queries;

public sealed record GetTagsByIdsQuery(Dictionary<int, List<int>> Ids) : IRequest<GetTagsByIdsQueryResponse>;

public sealed record GetTagsByIdsQueryResponse(Dictionary<int,  List<TagModel>> Tags);
