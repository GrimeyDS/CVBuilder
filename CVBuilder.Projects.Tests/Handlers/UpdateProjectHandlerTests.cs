using CVBuilder.Projects.Data;
using CVBuilder.Projects.Handlers;
using CVBuilder.Projects.Data.Entities;
using CVBuilder.Projects.Commands;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CVBuilder.Profiles.Tests.Handlers;

public class UpdateProjectHandlerTests
{
    private readonly DbContextOptions<ProjectDbContext> _context;
    private readonly UpdateProjectCommandHandler _handler;
    private readonly List<ProjectEntity> _projects;

    public UpdateProjectHandlerTests()
    {
        _projects = new List<ProjectEntity>
        {
            new ProjectEntity { Id = 1, ProfileId = 1, Title = "Test Title", Customer = "Test Customer", Description = "Test Description", StartDate = DateTime.Now.AddDays(-1000), EndDate = DateTime.Now.AddDays(-500), PictureUrl = "test.jpg" }
        };

        _context = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateProjects")
            .Options;

        _handler = new UpdateProjectCommandHandler(new ProjectDbContext(_context));
    }

    private void SeedDatabase()
    {
        using (var context = new ProjectDbContext(_context))
        {
            context.Projects.RemoveRange(context.Projects); // Clear existing data
            context.SaveChanges();


            context.Projects.AddRange(_projects);
            context.SaveChanges();
        }
    }

    [Fact]
    public async Task UpdateProjectCommandHandler_WithInvalidProjectId_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new UpdateProjectCommand(2, "Test Title", "Test Customer", DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500), "Test Description", "test.jpg");

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateProjectCommandHandler_WithValidProjectId_ReturnsProjectInteger()
    {
        // Arrange
        SeedDatabase();
        int profileId = 1;
        var command = new UpdateProjectCommand(profileId, "Test Title", "Test Customer", DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500), "Test Description", "test.jpg");

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(profileId, response.Id);
    }
}
