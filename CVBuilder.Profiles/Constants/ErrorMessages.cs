namespace CVBuilder.Profiles.Constants;

internal static class ErrorMessages
{
    public const string UserIdRequired = "User ID is required";
    public const string UserIdInvalid = "User ID is invalid";
    public const string ProfileIdRequired = "Profile ID is required";
    public const string ProfileIdInvalid = "Profile ID is invalid";
    public const string FirstNameRequired = "First name is required";
    public const string FirstNameMaxLength = "First name must have a maximum of 50 characters";
    public const string LastNameRequired = "Last name is required";
    public const string LastNameMaxLength = "Last name must have a maximum of 50 characters";
    public const string BirthDateRequired = "Birth date is required";
    public const string BirthDateInvalid = "Birth date must be less than the current date";
    public const string DescriptionMaxLength = "Description must have a maximum of 250 characters";
    public const string PictureUrlMaxLength = "Picture URL must have a maximum of 500 characters";

    public const string ProfileNotFound = "Profile not found";
    public const string ProfileAlreadyExists = "Profile already exists for this user";

    public const string CurrentRoleRequired = "Current role is required";
    public const string CurrentRoleMaxLength = "Current role must have a maximum of 250 characters";
}
