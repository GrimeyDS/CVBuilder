using CVBuilder.Projects.Commands;
using CVBuilder.Projects.Validators;
using Xunit;

namespace CVBuilder.Projects.Tests.Validators;

public class DeleteProjectCommandValidatorTests
{
    private readonly DeleteProjectCommandValidator _validator;
    private DeleteProjectCommand _command;

    public DeleteProjectCommandValidatorTests()
    {
        _validator = new DeleteProjectCommandValidator();
        _command = new DeleteProjectCommand(1);
    }

    [Fact]
    public void DeleteProjectCommandValidator_WithValidProjectId_ReturnsTrue()
    {
        // Arrange
        _command.ProjectId = 1;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void DeleteProjectCommandValidator_WithInvalidProjectId_ReturnsFalse()
    {
        // Arrange
        _command.ProjectId = 0;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }
}
