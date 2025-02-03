using CVBuilder.Tags.Data;
using CVBuilder.Tags.Handlers;
using CVBuilder.Tags.Data.Entities;
using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Constants;
using Microsoft.EntityFrameworkCore;
using Azure;
using CVBuilder.Experiences.Handlers;

namespace CVBuilder.Tags.Tests.Handlers;

public class UpdateTagHandlerTests
{
    private readonly DbContextOptions<TagDbContext> _context;
    private readonly UpdateTagCommandHandler _handler;
    private readonly List<TagEntity> _tags;

    public UpdateTagHandlerTests()
    {
        _tags = new List<TagEntity>
        {
            new TagEntity {Id = 1}
        };

        _context = new DbContextOptionsBuilder<TagDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateTag")
            .Options;

        _handler = new UpdateTagCommandHandler(new TagDbContext(_context));
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
    public async Task UpdateTagHandler_WithCorrectInputs_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new UpdateTagCommand(1, "test2");

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task UpdateTagHandler_WithNonExistingTag_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new UpdateTagCommand(2, "test2");

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}