using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Constants;
using CVBuilder.Tags.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Tags.Handlers;

internal class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, DeleteTagCommandResponse>
{
    private readonly TagDbContext _context;

    public DeleteTagCommandHandler(TagDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteTagCommandResponse> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tags.Where(e => e.Id == request.TagId)
                                        .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            throw new Exception(ErrorMessages.TagNotFound);

        _context.Tags.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTagCommandResponse(entity.Id);   
    }
}