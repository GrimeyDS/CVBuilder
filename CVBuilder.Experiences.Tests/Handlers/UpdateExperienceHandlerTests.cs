using CVBuilder.Experiences.Data;
using CVBuilder.Experiences.Handlers;
using CVBuilder.Experiences.Data.Entities;
using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Constants;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Tests.Handlers;

public class UpdateExperienceHandlerTests
{
    private readonly DbContextOptions<ExperienceDbContext> _context;
    private readonly UpdateExperienceCommandHandler _handler;
    private readonly List<ExperienceEntity> _experiences;

    public UpdateExperienceHandlerTests()
    {
        _experiences = new List<ExperienceEntity>
        {
            new ExperienceEntity { Id = 1, ProfileId = 1, Title = "test", Description = "test description", Type = ExperienceTypes.Education.ToString(), StartDate = DateTime.Now.AddDays(-1000), EndDate = DateTime.Now.AddDays(-500) }
        };

        _context = new DbContextOptionsBuilder<ExperienceDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateExperience")
            .Options;

        _handler = new UpdateExperienceCommandHandler(new ExperienceDbContext(_context));
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
    public async Task UpdateExperienceHandler_WithCorrectInputs_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new UpdateExperienceCommand(1, 1, "test2", "test description2", DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500));

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task UpdateExperienceHandler_WithNonExistingExperience_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new UpdateExperienceCommand(2, 1, "test2", "test description2", DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateExperienceHandler_WithDifferentProfileId_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new UpdateExperienceCommand(1, 2, "test", "test description", DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }
}