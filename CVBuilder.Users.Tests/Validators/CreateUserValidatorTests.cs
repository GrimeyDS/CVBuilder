using CVBuilder.Users.Commands;
using CVBuilder.Users.Validators;

namespace CVBuilder.Users.Tests.Validators;

public class CreateUserValidatorTests
{
    private readonly CreateUserCommandValidator _validator;
    private readonly CreateUserCommand _command;

    public CreateUserValidatorTests()
    {
        _validator = new CreateUserCommandValidator();
        _command = new CreateUserCommand("12345", "email@email.com");
    }

    [Fact]
    public void CreateUserValidator_WithValidEmail_ReturnsTrue()
    {
        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("email")]
    [InlineData("email@")]
    [InlineData("email.com")]
    public void CreateUserValidator_WithInvalidEmail_ReturnsFalse(string email)
    {
        // Arrange
        var newCommand = new CreateUserCommand("12345", email);

        // Act
        var result = _validator.Validate(newCommand);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    public void CreateUserValidator_WithInvalidEntraId_ReturnsFalse(string entraId)
    {
        // Arrange
        var newCommand = new CreateUserCommand(entraId, "email@email.com");

        // Act
        var result = _validator.Validate(newCommand);

        // Assert
        Assert.False(result.IsValid);
    }
}

