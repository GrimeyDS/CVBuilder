using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Validators;

namespace CVBuilder.Profiles.Tests.Validators;

public class DeleteProfileCommandValidatorTests
{
    private readonly DeleteProfileCommandValidator _validator;
    private DeleteProfileCommand _command;

    public DeleteProfileCommandValidatorTests()
    {
        _validator = new DeleteProfileCommandValidator();
        _command = new DeleteProfileCommand(1);
    }

    [Fact]
    public void DeleteProfileCommandValidator_WithValidProfileId_ReturnsTrue()
    {
        // Arrange
        _command.ProfileId = 1;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void DeleteProfileCommandValidator_WithInvalidProfileId_ReturnsFalse()
    {
        // Arrange
        _command.ProfileId = 0;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }
}
