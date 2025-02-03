using CVBuilder.Experiences.Commands;
using CVBuilder.Experiences.Constants;
using CVBuilder.Experiences.Validators;

namespace CVBuilder.Experiences.Tests.Validators;

public class CreateExperienceCommandValidatorTests
{
    private readonly CreateExperienceCommandValidator _validator;
    private CreateExperienceCommand _command;
    private const string longString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris faucibus ante id sapien accumsan rutrum. Duis eget orci rhoncus, dictum libero at, tincidunt turpis. Nam maximus, metus nec gravida blandit, erat metus pharetra risus, id pulvinar orci erat at quam. Phasellus eget libero id massa suscipit laoreet. Morbi a laoreet nulla. Praesent id efficitur ligula, id egestas dui. Ut iaculis eget erat nec imperdiet. Praesent eu massa eget mauris malesuada consequat. Quisque quis aliquam orci. Cras risus nulla, vestibulum convallis ornare dapibus, pulvinar ut tellus. Sed sed purus quam. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec luctus eget velit eu tincidunt. Morbi enim nisi, posuere at porta eu, maximus non est. Ut nibh libero, hendrerit sit amet orci sed, placerat consequat turpis. Donec fringilla odio turpis, quis gravida nulla egestas id.";

    public CreateExperienceCommandValidatorTests()
    {
        _validator = new CreateExperienceCommandValidator();
        _command = new CreateExperienceCommand(1, "Title", "Description", ExperienceTypes.Job, DateTime.Now.AddMonths(-10), DateTime.Now.AddMonths(-5));
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithInvalidProfileId_ReturnsFalse()
    {   
        // Arrange
        _command.ProfileId = 0;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithValidProfileId_ReturnsTrue()
    {
        // Arrange
        _command.ProfileId = 1;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(longString)]
    public void CreateExperienceCommandValidator_WithEmptyTitle_ReturnsFalse(string title)
    {
        // Arrange
        _command.Title = title;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithValidTitle_ReturnsTrue()
    {
        // Arrange
        _command.Title = "Test";

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithInvalidDescription_ReturnsFalse()
    {
        // Arrange
        _command.Description = longString;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithValidDescription_ReturnsTrue()
    {
        // Arrange
        _command.Description = "Test";

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithInvalidType_ReturnsFalse()
    {
        // Arrange
        _command.Type = (ExperienceTypes)10;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithValidType_ReturnsTrue()
    {
        // Arrange
        _command.Type = ExperienceTypes.Job;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(2)]
    public void CreateProjectCommandValidator_WithInvalidStartDate_ReturnsFalse(int months)
    {
        // Arrange
        _command.EndDate = DateTime.Now.AddMonths(-5);
        _command.StartDate = DateTime.Now.AddMonths(months);

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithValidStartDate_ReturnsTrue()
    {
        // Arrange
        _command.StartDate = DateTime.Now.AddMonths(-6);

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(-6)]
    [InlineData(2)]
    public void CreateExperienceCommandValidator_WithInvalidEndDate_ReturnsFalse(int months)
    {
        // Arrange
        _command.StartDate = DateTime.Now.AddMonths(-5);
        _command.EndDate = DateTime.Now.AddMonths(months);

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateExperienceCommandValidator_WithValidEndDate_ReturnsTrue()
    {
        // Arrange
        _command.EndDate = DateTime.Now.AddMonths(-5);

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }
}