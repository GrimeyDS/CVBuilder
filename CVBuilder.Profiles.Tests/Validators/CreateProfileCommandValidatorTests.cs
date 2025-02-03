using CVBuilder.Profiles.Commands;
using CVBuilder.Profiles.Validators;

namespace CVBuilder.Profiles.Tests.Validators;

public class CreateProfileCommandValidatorTests
{
    private readonly CreateProfileCommandValidator _validator;
    private CreateProfileCommand _command;
    private const string longString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris faucibus ante id sapien accumsan rutrum. Duis eget orci rhoncus, dictum libero at, tincidunt turpis. Nam maximus, metus nec gravida blandit, erat metus pharetra risus, id pulvinar orci erat at quam. Phasellus eget libero id massa suscipit laoreet. Morbi a laoreet nulla. Praesent id efficitur ligula, id egestas dui. Ut iaculis eget erat nec imperdiet. Praesent eu massa eget mauris malesuada consequat. Quisque quis aliquam orci. Cras risus nulla, vestibulum convallis ornare dapibus, pulvinar ut tellus. Sed sed purus quam. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec luctus eget velit eu tincidunt. Morbi enim nisi, posuere at porta eu, maximus non est. Ut nibh libero, hendrerit sit amet orci sed, placerat consequat turpis. Donec fringilla odio turpis, quis gravida nulla egestas id.";

    public CreateProfileCommandValidatorTests()
    {
        _validator = new CreateProfileCommandValidator();
        _command = new CreateProfileCommand(1, "John", "Doe", DateTime.Now.AddMonths(-10), "Description", "PictureUrl", "test role");
    }

    [Fact]
    public void CreateProfileCommandValidator_WithInvalidUserId_ReturnsFalse()
    {
        // Arrange
        _command.UserId = 0;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateProfileCommandValidator_WithValidUserId_ReturnsTrue()
    {
        // Arrange
        _command.UserId = 1;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(longString)]
    public void CreateProfileCommandValidator_WithInvalidFirstName_ReturnsFalse(string firstName)
    {
        // Arrange
        _command.FirstName = firstName;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateProfileCommandValidator_WithValidFirstName_ReturnsTrue()
    {
        // Arrange
        _command.FirstName = "John";

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(longString)]
    public void CreateProfileCommandValidator_WithInvalidLastName_ReturnsFalse(string lastName)
    {
        // Arrange
        _command.LastName = lastName;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateProfileCommandValidator_WithValidLastName_ReturnsTrue()
    {
        // Arrange
        _command.LastName = "Doe";

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void CreateProfileCommandValidator_WithInvalidBirthDate_ReturnsFalse()
    {
        // Arrange
        _command.BirthDate = DateTime.Now.AddMonths(10);

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateProfileCommandValidator_WithValidBirthDate_ReturnsTrue()
    {
        // Arrange
        _command.BirthDate = DateTime.Now.AddMonths(-10);

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void CreateProfileCommandValidator_WithInvalidDescription_ReturnsFalse()
    {
        // Arrange
        _command.Description = longString;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("Description")]
    public void CreateProfileCommandValidator_WithValidDescription_ReturnsTrue(string description)
    {
        // Arrange
        _command.Description = description;

        // Act
        var result = _validator.Validate(_command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(longString)]
    public void CreateProfileCommandValidator_WithInvalidCurrentRole_ReturnsFalse(string currentRole)
    {
        // Arrange
        _command.CurrentRole = currentRole;
        // Act
        var result = _validator.Validate(_command);
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void CreateProfileCommandValidator_WithValidCurrentRole_ReturnsTrue()
    {
        // Arrange
        _command.CurrentRole = "test role";
        // Act
        var result = _validator.Validate(_command);
        // Assert
        Assert.True(result.IsValid);
    }
}
