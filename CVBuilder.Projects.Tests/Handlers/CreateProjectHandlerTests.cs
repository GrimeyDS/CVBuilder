using CVBuilder.Projects.Data;
using CVBuilder.Projects.Handlers;
using CVBuilder.Projects.Data.Entities;
using CVBuilder.Projects.Commands;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CVBuilder.Profiles.Tests.Handlers;

public class CreateProjectHandlerTests
{
    private readonly DbContextOptions<ProjectDbContext> _context;
    private readonly CreateProjectCommandHandler _handler;
    private readonly List<ProjectEntity> _projects;

    public CreateProjectHandlerTests()
    {
        _projects = new List<ProjectEntity>
        {
            new ProjectEntity { Id = 1, ProfileId = 1, Title = "Test Title", Customer = "Test Customer", Description = "Test Description", StartDate = DateTime.Now.AddDays(-1000), EndDate = DateTime.Now.AddDays(-500), PictureUrl = "test.jpg" }
        };

        _context = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateProjects")
            .Options;

        _handler = new CreateProjectCommandHandler(new ProjectDbContext(_context));
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
    public async Task CreateProjectCommandHandler_WithCorrectInputs_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new CreateProjectCommand(2, "Test Title", "Test Customer", DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500), "Test Description", "test.jpg");  

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task CreateProjectCommandHandler_WithExistingProject_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new CreateProjectCommand(1, "Test Title", "Test Customer", DateTime.Now.AddDays(-1000), DateTime.Now.AddDays(-500), "Test Description", "test.jpg");  

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

}
