namespace CVBuilder.Experiences.Constants;

internal static class ErrorMessages
{
    public const string ExperienceAlreadyExists = "Experience already exists";
    public const string ExperienceNotFound = "Experience not found";
    public const string ProfileExperienceNotFound = "Experience is not a part of Profile";

    public const string IdRequired = "Id is required";
    public const string IdInvalid = "Id is invalid";
    public const string ProfileIdRequired = "ProfileId is required";
    public const string ProfileIdInvalid = "ProfileId is invalid";
    public const string TitleRequired = "Title is required";
    public const string DescriptionRequired = "Description is required";
    public const string TitleMaxLength = "Title must be less than 100 characters";
    public const string DescriptionMaxLength = "Description must be less than 500 characters";
    public const string TypeRequired = "Type is required";

    internal const string InvalidType = "Invalid container name";
    public const string StartDateRequired = "The start date is required.";
    public const string StartDateInvalid = "The start date must be less than the current date.";

    public const string EndDateInvalid = "The end date must be less than the current date.";
}