using CVBuilder.Users.Commands;
using CVBuilder.Users.Validators;

namespace CVBuilder.Users.Tests.Validators;

public class UpdateUserValidatorTests
{
    private readonly UpdateUserCommandValidator _validator;
    private readonly UpdateUserCommand _command;

    public UpdateUserValidatorTests()
    {
        _validator = new UpdateUserCommandValidator();

        _command = new UpdateUserCommand(1, "email@email.com", "12345");
    }

    [Theory]
    [InlineData("")]
    [InlineData("email")]
    [InlineData("email@")]
    [InlineData("email.com")]
    public void UpdateUserValidator_WithInvalidEmail_ReturnsFalse(string email)
    {
        // Arrange
        var newCommand = new UpdateUserCommand(1, email, _command.EntraId);

        // Act
        var result = _validator.Validate(newCommand);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void UpdateUserValidator_WithValidEmail_ReturnsTrue()
    {
        // Act
        var result = _validator.Validate(_command);
        
        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void UpdateUserValidator_WithInvalidId_ReturnsFalse(int id)
    {
        // Arrange
        var newCommand = new UpdateUserCommand(id, _command.Email, _command.EntraId);

        // Act
        var result = _validator.Validate(newCommand);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(int.MaxValue)]
    public void UpdateUserValidator_WithValidId_ReturnsTrue(int id)
    {
        // Arrange
        var newCommand = new UpdateUserCommand(id, _command.Email, _command.EntraId);

        // Act
        var result = _validator.Validate(newCommand);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void UpdateUserValidator_WithEmptyEntraId_ReturnsFalse()
    {
        // Arrange
        var newCommand = new UpdateUserCommand(_command.Id, _command.Email, "");

        // Act
        var result = _validator.Validate(newCommand);

        // Assert
        Assert.False(result.IsValid);
    }
}
