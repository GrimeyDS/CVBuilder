using CVBuilder.Tags.Data;
using CVBuilder.Tags.Handlers;
using CVBuilder.Tags.Data.Entities;
using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Constants;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Tags.Tests.Handlers;

public class CreateTagHandlerTests
{
    private readonly DbContextOptions<TagDbContext> _context;
    private readonly CreateTagCommandHandler _handler;
    private readonly List<TagEntity> _tags;

    public CreateTagHandlerTests()
    {
        _tags = new List<TagEntity>
        {
            new TagEntity { Title = "test" }
        };

        _context = new DbContextOptionsBuilder<TagDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateTag")
            .Options;

        _handler = new CreateTagCommandHandler(new TagDbContext(_context));
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
    public async Task CreateTagHandler_WithCorrectInputs_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new CreateTagCommand("test2");

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task CreateTagHandler_WithExistingTag_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new CreateTagCommand("test");

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}