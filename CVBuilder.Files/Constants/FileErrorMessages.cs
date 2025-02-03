namespace CVBuilder.Files.Constants;

internal static class FileErrorMessages
{
    internal const string FileSizeError = "Image must be less or equal to 5mb";
    internal const string FileTypeError = "Image must be a type of .jpg, .jpeg or .png";

    internal const string AzureStorageConnectionStringError = "Azure Storage Connection String not found";
    internal const string ErrorUploadingFile = "Error uploading file";

    internal const string ErrorGettingFile = "Error getting file";
    internal const string FileNotFound = "File not found";

    internal const string InvalidContainerName = "Invalid container name";
}
