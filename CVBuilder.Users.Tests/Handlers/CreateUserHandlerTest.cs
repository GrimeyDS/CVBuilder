using CVBuilder.Users.Data;
using CVBuilder.Users.Handlers;
using CVBuilder.Users.Data.Entities;
using CVBuilder.Users.Commands;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Users.Tests.Handlers;

public class CreateUserHandlerTest
{
    private readonly DbContextOptions<UserDbContext> _context;
    private readonly CreateUserCommandHandler _handler;
    private readonly List<UserEntity> _users;
    private readonly CreateUserCommand _command;
    private readonly string _sameEmail;
    private readonly string _sameEntraId;

    public CreateUserHandlerTest()
    {
        _sameEmail = "email@email.com";
        _sameEntraId = "12345";
        _users = new List<UserEntity>
        {
            new UserEntity { Id = 1, Email = _sameEmail, EntraId = _sameEntraId}
        };

        _command = new CreateUserCommand("54321", "test@test.com");

        _context = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateUsers")
            .Options;

        _handler = new CreateUserCommandHandler(new UserDbContext(_context));
    }

    private void SeedDatabase()
    {
        using (var context = new UserDbContext(_context))
        {
            context.Users.RemoveRange(context.Users); // Clear existing data
            context.SaveChanges();


            context.Users.AddRange(_users);
            context.SaveChanges();
        }
    }

    [Fact]
    public async Task CreateUserHandler_WithNewEmail_ReturnsNewInteger()
    {
        // Arrange
        SeedDatabase();

        // Act
        var response = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async Task CreateUserHandler_WithExistingEmail_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var newCommand = new CreateUserCommand(_command.EntraId, _sameEmail);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(newCommand, new CancellationToken()));
    }

    [Fact]
    public async Task CreateUserHandler_WithExistingEntraId_ThrowsException()
    {
        SeedDatabase();
        var newCommand = new CreateUserCommand(_sameEntraId, _command.Email);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(newCommand, new CancellationToken()));
    }

    [Fact]
    public async Task CreateUserHandler_WithNewEmailAndNewEntraId_CreatesOneUser()
    {
        // Arrange
        SeedDatabase();
        var usersCount = 2;

        // Act
        var response = await _handler.Handle(_command, CancellationToken.None);

        // Assert
        var users = await new UserDbContext(_context).Users.ToListAsync();
        Assert.Equal(usersCount, users.Count);
    }
}
