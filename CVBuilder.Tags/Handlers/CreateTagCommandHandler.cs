using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Constants;
using CVBuilder.Tags.Data;
using CVBuilder.Tags.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Tags.Handlers;

internal class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, CreateTagCommandResponse>
{
    private readonly TagDbContext _context;

    public CreateTagCommandHandler(TagDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTagCommandResponse> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Title == request.Title.Trim(), cancellationToken);

        if (existingTag != null)
            throw new Exception(ErrorMessages.TagAlreadyExists);

        var entity = new TagEntity
        {
            Title = request.Title.Trim(),
        };

        _context.Tags.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTagCommandResponse(entity.Id);
    }

}