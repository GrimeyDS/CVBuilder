using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Data;
using CVBuilder.Profiles.Data.Entities;
using CVBuilder.Profiles.Handlers;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Tests.Handlers;

public class UpdateProfileHandlerTests
{
    private readonly DbContextOptions<ProfileDbContext> _context;
    private readonly UpdateProfileCommandHandler _handler;
    private readonly List<ProfileEntity> _profiles;

    public UpdateProfileHandlerTests()
    {
        _profiles = new List<ProfileEntity>
        {
            new ProfileEntity { Id = 1, UserId = 1, FirstName = "Test", LastName = "Von Test", BirthDate = DateTime.Now.AddDays(-1000), Description = "Test Description", PictureUrl = "test.jpg" }
        };

        _context = new DbContextOptionsBuilder<ProfileDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateProfiles")
            .Options;

        _handler = new UpdateProfileCommandHandler(new ProfileDbContext(_context));
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
    public async Task UpdateProfileCommandHandler_WithInvalidProfileId_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new UpdateProfileCommand(2, "Test", "Von Test", DateTime.Now.AddDays(-1000), "Test Description", "test.jpg", "test role");

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateProfileCommandHandler_WithValidProfileId_ReturnsProfileInteger()
    {
        // Arrange
        SeedDatabase();
        int profileId = 1;
        var command = new UpdateProfileCommand(profileId, "Test", "Von Test", DateTime.Now.AddDays(-1000), "Test Description", "test.jpg", "test role");

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(profileId, response.Id);
    }
}
