using CVBuilder.Tags.Data;
using CVBuilder.Tags.Data.Entities;
using CVBuilder.Tags.Mapping;
using CVBuilder.Tags.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Tags.Handlers;

internal class GetTagQueryHandler : IRequestHandler<GetTagQuery, GetTagQueryResponse>
{
    private readonly TagDbContext _context;

    public GetTagQueryHandler(TagDbContext context)
    {
        _context = context;
    }

    public async Task<GetTagQueryResponse> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var result = new List<TagEntity>();

        if (request.Id.HasValue)
        {
            var tag = await _context.Tags.Where(e => e.Id == request.Id).FirstOrDefaultAsync();
            if (tag != null) result.Add(tag);
        }
        else
        {
            result = await _context.Tags.ToListAsync(cancellationToken: cancellationToken);

        }

        var response = new GetTagQueryResponse(result.Select(u => u.ToModel()));
        return response;
    }
}