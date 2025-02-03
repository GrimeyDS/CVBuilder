using CVBuilder.Shared.Models;
using CVBuilder.Shared.Queries;
using CVBuilder.Tags.Data;
using CVBuilder.Tags.Mapping;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Tags.Handlers;

internal class GetTagsByIdsQueryHandler : IRequestHandler<GetTagsByIdsQuery, GetTagsByIdsQueryResponse>
{
    private readonly TagDbContext _context;

    public GetTagsByIdsQueryHandler(TagDbContext context)
    {
        _context = context;
    }

    public async Task<GetTagsByIdsQueryResponse> Handle(GetTagsByIdsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _context.Tags.ToListAsync(cancellationToken);
        var result = new Dictionary<int, List<TagModel>>();

        foreach (var key in request.Ids.Keys)
        {
            var keyTags = tags.Where(t => request.Ids[key].Contains(t.Id)).ToList();
            result.Add(key, keyTags.Select(t => t.ToModel()).ToList());
        }

        return new GetTagsByIdsQueryResponse(result);
    }
}