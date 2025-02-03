namespace CVBuilder.Projects.Constants;

internal static class ErrorMessages
{
    public const string ProjectAlreadyExists = "The project already exists.";
    public const string ProjectNotFound = "The project was not found.";
    public const string ProjectIdRequired = "The project ID is required.";
    public const string ProjectIdInvalid = "The project ID is invalid.";

    public const string ProfileIdRequired = "The profile ID is required.";
    public const string ProfileIdInvalid = "The profile ID is invalid.";

    public const string TitleRequired = "The title is required.";
    public const string TitleMaxLength = "The title must have a maximum of 50 characters.";

    public const string CustomerRequired = "The customer is required.";
    public const string CustomerMaxLength = "The customer must have a maximum of 50 characters.";

    public const string StartDateRequired = "The start date is required.";
    public const string StartDateInvalid = "The start date must be less than the current date.";

    public const string EndDateInvalid = "The end date must be less than the current date.";

    public const string DescriptionMaxLength = "The description must have a maximum of 500 characters.";
}
