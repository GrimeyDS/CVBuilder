using CVBuilder.Experiences.Data;
using CVBuilder.Experiences.Handlers;
using CVBuilder.Experiences.Data.Entities;
using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Constants;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Tests.Handlers;

public class DeleteExperienceHandlerTests
{
    private readonly DbContextOptions<ExperienceDbContext> _context;
    private readonly DeleteExperienceCommandHandler _handler;
    private readonly List<ExperienceEntity> _experiences;

    public DeleteExperienceHandlerTests()
    {
        _experiences = new List<ExperienceEntity>
        {
            new ExperienceEntity { Id = 1, ProfileId = 1, Title = "test", Description = "test description", Type = ExperienceTypes.Education.ToString() }
        };

        _context = new DbContextOptionsBuilder<ExperienceDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteExperience")
            .Options;

        _handler = new DeleteExperienceCommandHandler(new ExperienceDbContext(_context));
    }

    private void SeedDatabase()
    {
        using (var context = new ExperienceDbContext(_context))
        {
            context.Experiences.RemoveRange(context.Experiences); // Clear existing data
            context.SaveChanges();


            context.Experiences.AddRange(_experiences);
            context.SaveChanges();
        }
    }

    [Fact]
    public async Task DeleteExperienceHandler_WithCorrectInputs_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new DeleteExperienceCommand(1);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task DeleteExperienceHandler_WithNonExistingExperience_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new DeleteExperienceCommand(2);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}
