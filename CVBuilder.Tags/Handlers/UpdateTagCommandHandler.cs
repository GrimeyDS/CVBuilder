using CVBuilder.Tags.Constants;
using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Handlers;

internal class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, UpdateTagCommandResponse>
{
    private readonly TagDbContext _context;

    public UpdateTagCommandHandler(TagDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateTagCommandResponse> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var oldTag = await _context.Tags.FirstOrDefaultAsync(u => u.Id == request.TagId, cancellationToken);

        if (oldTag is null)
            throw new Exception(ErrorMessages.TagNotFound);

        oldTag.Title = request.Title;

        _context.Tags.Update(oldTag);
        await _context.SaveChangesAsync(cancellationToken);
        return new UpdateTagCommandResponse(oldTag.Id);
    }

}