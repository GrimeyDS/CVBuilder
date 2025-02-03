namespace CVBuilder.Tags.Requests;

public sealed class UpdateTagRequest(string Title)
{
    public string Title { get; set; } = Title;
}