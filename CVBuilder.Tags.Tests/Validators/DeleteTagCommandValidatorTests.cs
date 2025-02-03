using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Validators;

namespace CVBuilder.Tags.Tests.Validators;

public class DeleteTagCommandValidatorTests
{
    private readonly DeleteTagCommandValidator _validator;
    private DeleteTagCommand _command;

    public DeleteTagCommandValidatorTests()
    {
        _validator = new DeleteTagCommandValidator();
        _command = new DeleteTagCommand(1, 1);
    }

    [Fact]
    public void DeleteTagCommandValidator_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        _command.TagId = 0;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void DeleteTagCommandValidator_WithValidId_ReturnsTrue()
    {
        // Arrange
        _command.TagId = 1;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void DeleteTagCommandValidator_WithInvalidOriginId_ReturnsFalse()
    {
        // Arrange
        _command.OriginId = 0;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void DeleteTagCommandValidator_WithValidOriginId_ReturnsTrue()
    {
        // Arrange
        _command.OriginId = 1;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }
}
