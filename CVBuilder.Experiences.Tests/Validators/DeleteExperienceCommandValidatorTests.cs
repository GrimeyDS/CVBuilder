using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Validators;

namespace CVBuilder.Experiences.Tests.Validators;

public class DeleteExperienceCommandValidatorTests
{
    private readonly DeleteExperienceCommandValidator _validator;
    private DeleteExperienceCommand _command;
    private const string longString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris faucibus ante id sapien accumsan rutrum. Duis eget orci rhoncus, dictum libero at, tincidunt turpis. Nam maximus, metus nec gravida blandit, erat metus pharetra risus, id pulvinar orci erat at quam. Phasellus eget libero id massa suscipit laoreet. Morbi a laoreet nulla. Praesent id efficitur ligula, id egestas dui. Ut iaculis eget erat nec imperdiet. Praesent eu massa eget mauris malesuada consequat. Quisque quis aliquam orci. Cras risus nulla, vestibulum convallis ornare dapibus, pulvinar ut tellus. Sed sed purus quam. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec luctus eget velit eu tincidunt. Morbi enim nisi, posuere at porta eu, maximus non est. Ut nibh libero, hendrerit sit amet orci sed, placerat consequat turpis. Donec fringilla odio turpis, quis gravida nulla egestas id.";

    public DeleteExperienceCommandValidatorTests()
    {
        _validator = new DeleteExperienceCommandValidator();
        _command = new DeleteExperienceCommand(1);
    }

    [Fact]
    public void DeleteExperienceCommandValidator_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        _command.Id = 0;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void DeleteExperienceCommandValidator_WithValidId_ReturnsTrue()
    {
        // Arrange
        _command.Id = 1;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }
}
