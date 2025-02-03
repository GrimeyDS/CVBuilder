using CVBuilder.Tags.Data;
using CVBuilder.Tags.Handlers;
using CVBuilder.Tags.Data.Entities;
using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Constants;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Tags.Tests.Handlers;

public class DeleteTagHandlerTests
{
    private readonly DbContextOptions<TagDbContext> _context;
    private readonly DeleteTagCommandHandler _handler;
    private readonly List<TagEntity> _tags;

    public DeleteTagHandlerTests()
    {
        _tags = new List<TagEntity>
        {
            new TagEntity {Id = 1}
        };

        _context = new DbContextOptionsBuilder<TagDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteTag")
            .Options;

        _handler = new DeleteTagCommandHandler(new TagDbContext(_context));
    }

    private void SeedDatabase()
    {
        using (var context = new TagDbContext(_context))
        {
            context.Tags.RemoveRange(context.Tags); // Clear existing data
            context.SaveChanges();


            context.Tags.AddRange(_tags);
            context.SaveChanges();
        }
    }

    [Fact]
    public async Task DeleteTagHandler_WithCorrectInputs_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new DeleteTagCommand(1, 1);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteTagHandler_WithNonExistingTag_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new DeleteTagCommand(2, 1);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}
