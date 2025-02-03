using CVBuilder.Users.Data;
using CVBuilder.Users.Data.Entities;
using CVBuilder.Users.Mapping;
using CVBuilder.Users.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Users.Handlers;

internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResponse>
{
    private readonly UserDbContext _context;

    public GetUserQueryHandler(UserDbContext context)
    {
        _context = context;
    }
    
    public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var result = new List<UserEntity>();

        if (request.Id.HasValue)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user != null) result.Add(user);
        }
        else
        {
            result = await _context.Users.ToListAsync(cancellationToken: cancellationToken);
        }

        var response = new GetUserQueryResponse(result.Select(u => u.ToModel()));
        return response;
    }
}