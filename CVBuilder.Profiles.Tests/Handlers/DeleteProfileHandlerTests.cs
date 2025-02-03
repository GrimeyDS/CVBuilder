using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Data;
using CVBuilder.Profiles.Data.Entities;
using CVBuilder.Profiles.Handlers;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Profiles.Tests.Handlers;

public class DeleteProfileHandlerTests
{
    private readonly DbContextOptions<ProfileDbContext> _context;
    private readonly DeleteProfileCommandHandler _handler;
    private readonly List<ProfileEntity> _profiles;

    public DeleteProfileHandlerTests()
    {
        _profiles = new List<ProfileEntity>
        {
            new ProfileEntity { Id = 1, UserId = 1, FirstName = "Test", LastName = "Von Test", BirthDate = DateTime.Now.AddDays(-1000), Description = "Test Description", PictureUrl = "test.jpg" }
        };

        _context = new DbContextOptionsBuilder<ProfileDbContext>()
            .UseInMemoryDatabase(databaseName: "DeleteProfiles")
            .Options;

        _handler = new DeleteProfileCommandHandler(new ProfileDbContext(_context));
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
    public async Task DeleteProfileCommandHandler_WithInvalidProfileId_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var command = new DeleteProfileCommand(2);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task DeleteProfileCommandHandler_WithValidProfileId_ReturnsProfileInteger()
    {
        // Arrange
        SeedDatabase();
        int profileId = 1;
        var command = new DeleteProfileCommand(profileId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(profileId, result.Id);
    }
}
