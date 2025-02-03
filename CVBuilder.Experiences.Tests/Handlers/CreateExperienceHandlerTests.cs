using CVBuilder.Experiences.Data;
using CVBuilder.Experiences.Handlers;
using CVBuilder.Experiences.Data.Entities;
using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Constants;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Experiences.Tests.Handlers;

public class CreateExperienceHandlerTests
{
    private readonly DbContextOptions<ExperienceDbContext> _context;
    private readonly CreateExperienceCommandHandler _handler;
    private readonly List<ExperienceEntity> _experiences;

    public CreateExperienceHandlerTests()
    {
        _experiences = new List<ExperienceEntity>
        {
            new ExperienceEntity { ProfileId = 1, Title = "test", Description = "test description", Type = ExperienceTypes.Education.ToString() }
        };

        _context = new DbContextOptionsBuilder<ExperienceDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateExperience")
            .Options;

        _handler = new CreateExperienceCommandHandler(new ExperienceDbContext(_context));
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
    public async Task CreateExperienceHandler_WithCorrectInputs_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new CreateExperienceCommand(1, "test2", "test description2", ExperienceTypes.Job, DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500));

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task CreateExperienceHandler_WithExistingExperienceForProfile_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new CreateExperienceCommand(1, "test", "test description", ExperienceTypes.Education, DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task CreateExperienceHandler_WithExistingTitleInNewProfile_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new CreateExperienceCommand(2, "test", "test description", ExperienceTypes.Education, DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500));

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }
}