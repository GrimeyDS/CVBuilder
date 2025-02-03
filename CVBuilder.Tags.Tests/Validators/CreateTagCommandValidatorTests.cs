using CVBuilder.Tags.Commands;
using CVBuilder.Tags.Constants;
using CVBuilder.Tags.Validators;

namespace CVBuilder.Tags.Tests.Validators;

public class CreateTagCommandValidatorTests
{
    private readonly CreateTagCommandValidator _validator;
    private CreateTagCommand _command;
    private const string longString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris faucibus ante id sapien accumsan rutrum. Duis eget orci rhoncus, dictum libero at, tincidunt turpis. Nam maximus, metus nec gravida blandit, erat metus pharetra risus, id pulvinar orci erat at quam. Phasellus eget libero id massa suscipit laoreet. Morbi a laoreet nulla. Praesent id efficitur ligula, id egestas dui. Ut iaculis eget erat nec imperdiet. Praesent eu massa eget mauris malesuada consequat. Quisque quis aliquam orci. Cras risus nulla, vestibulum convallis ornare dapibus, pulvinar ut tellus. Sed sed purus quam. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec luctus eget velit eu tincidunt. Morbi enim nisi, posuere at porta eu, maximus non est. Ut nibh libero, hendrerit sit amet orci sed, placerat consequat turpis. Donec fringilla odio turpis, quis gravida nulla egestas id.";

    public CreateTagCommandValidatorTests()
    {
        _validator = new CreateTagCommandValidator();
        _command = new CreateTagCommand("Title");
    }

    [Theory]
    [InlineData("")]
    [InlineData(longString)]
    public void CreateTagCommandValidator_WithEmptyTitle_ReturnsFalse(string title)
    {
        // Arrange
        _command.Title = title;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateTagCommandValidator_WithValidTitle_ReturnsTrue()
    {
        // Arrange
        _command.Title = "Test";

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }
}