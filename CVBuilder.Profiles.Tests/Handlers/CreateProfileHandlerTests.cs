using CVBuilder.Profiles.Data;
using CVBuilder.Profiles.Handlers;
using CVBuilder.Profiles.Data.Entities;
using CVBuilder.Profiles.Commands;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Tests.Handlers;

public class CreateProfileHandlerTests
{
    private readonly DbContextOptions<ProfileDbContext> _context;
    private readonly CreateProfileCommandHandler _handler;
    private readonly List<ProfileEntity> _profiles;

    public CreateProfileHandlerTests()
    {
        _profiles = new List<ProfileEntity>
        {
            new ProfileEntity { Id = 1, UserId = 1, FirstName = "Test", LastName = "Von Test", BirthDate = DateTime.Now.AddDays(-1000), Description = "Test Description", PictureUrl = "test.jpg" }
        };

        _context = new DbContextOptionsBuilder<ProfileDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateProfiles")
            .Options;

        _handler = new CreateProfileCommandHandler(new ProfileDbContext(_context));
    }

    private void SeedDatabase()
    {
        using (var context = new ProfileDbContext(_context))
        {
            context.Profiles.RemoveRange(context.Profiles); // Clear existing data
            context.SaveChanges();


            context.Profiles.AddRange(_profiles);
            context.SaveChanges();
        }
    }

    [Fact]
    public async Task CreateProfileCommandHandler_WithCorrectInputs_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();
        var command = new CreateProfileCommand(2, "Test", "Von Test", DateTime.Now.AddDays(-1000), "Test Description", "test.jpg", "test role");  

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }


    // Uncomment when authentication is implemented
    //[Fact]
    //public async Task CreateProfileCommandHandler_WithExistingUserId_ThrowsException()
    //{
    //    // Arrange
    //    SeedDatabase();
    //    var command = new CreateProfileCommand(1, "Test", "Von Test", DateTime.Now.AddDays(-1000), "Test Description", "test.jpg");

    //    // Act & Assert
    //    var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    //}
}
