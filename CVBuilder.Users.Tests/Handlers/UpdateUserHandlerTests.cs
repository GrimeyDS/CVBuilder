using CVBuilder.Users.Data;
using CVBuilder.Users.Handlers;
using CVBuilder.Users.Data.Entities;
using Microsoft.EntityFrameworkCore;
using CVBuilder.Users.Commands;
using CVBuilder.Users.Models;

namespace CVBuilder.Users.Tests.Handlers;

public class UpdateUserHandlerTest
{
    private readonly DbContextOptions<UserDbContext> _context;
    private readonly UpdateUserCommandHandler _handler;
    private readonly List<UserEntity> _users;
    private readonly UpdateUserCommand _command;

    public UpdateUserHandlerTest()
    {
        _users = new List<UserEntity>
        {
            new UserEntity { Id = 1, Email = "email@email.com", EntraId = "12345" }
        };

        _context = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase(databaseName: "UpdateUsers")
            .Options;

        _command = new UpdateUserCommand(1, "email@email2.com", "12345");

        _handler = new UpdateUserCommandHandler(new UserDbContext(_context));
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
    public async Task UpdateUserHandler_WithoutUserRequest_ThrowsException()
    {
        // Arrange
        var newCommand = new UpdateUserCommand(0, "", "");
        
        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(newCommand, new CancellationToken()));
    }

    [Fact]
    public async Task UpdateUserHandler_WithoutUserRequest_ReturnsId()
    {
        // Arrange
        SeedDatabase();
        var id = 1;
        var newCommand = new UpdateUserCommand(id, _command.Email, _command.EntraId);

        // Act
        var result = await _handler.Handle(newCommand, new CancellationToken());

        // Assert
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task UpdateUserHandler_WithInvalidId_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var id = 2;
        var newCommand = new UpdateUserCommand(id, _command.Email, _command.EntraId);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(newCommand, new CancellationToken()));
    }

    [Fact]
    public async Task UpdateUserHandler_WithInvalidEntraId_ThrowsException()
    {
        // Arrange
        SeedDatabase();
        var id = 1;
        var newCommand = new UpdateUserCommand(id, _command.Email, "54321");

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(newCommand, new CancellationToken()));
    }

    [Fact]
    public async Task UpdateUserHandler_WithValidEntraId_UpdatesUser()
    {
        // Arrange
        SeedDatabase();
        var id = 1;
        var newEmail = "testers@testers.com";
        var newCommand = new UpdateUserCommand(id, newEmail, _command.EntraId);

        // Act
        await _handler.Handle(newCommand, new CancellationToken());

        // Assert
        using (var context = new UserDbContext(_context))
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));
            Assert.NotNull(user);
            Assert.Equal(user.Email, newEmail);
        }
    }
}
