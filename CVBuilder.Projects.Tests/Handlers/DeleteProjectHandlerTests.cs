using CVBuilder.Projects.Data;
using CVBuilder.Projects.Handlers;
using CVBuilder.Projects.Data.Entities;
using CVBuilder.Projects.Commands;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CVBuilder.Profiles.Tests.Handlers;

public class DeleteProjectHandlerTests
{
    private readonly DbContextOptions<ProjectDbContext> _context;
    private readonly DeleteProjectCommandHandler _handler;
    private readonly List<ProjectEntity> _projects;

    public DeleteProjectHandlerTests()
    {
        _projects = new List<ProjectEntity>
        {
            new ProjectEntity { Id = 1, ProfileId = 1, Title = "Test Title", Customer = "Test Customer", Description = "Test Description", StartDate = DateTime.Now.AddDays(-1000), EndDate = DateTime.Now.AddDays(-500), PictureUrl = "test.jpg" }
        };

        _context = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteProjects")
            .Options;

        _handler = new DeleteProjectCommandHandler(new ProjectDbContext(_context));
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
    public async Task DeleteProjectCommandHandler_WithInvalidProjectId_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new DeleteProjectCommand(2);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteProjectCommandHandler_WithValidProjectId_ReturnsProjectInteger()
    {
        // Arrange
        SeedDatabase();
        int profileId = 1;
        var command = new DeleteProjectCommand(profileId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(profileId, result.Id);
    }
}
