using CVBuilder.Tags.Constants;

namespace CVBuilder.Tags.Requests;

public sealed class CreateTagRequest(string Title)
{
    public string Title { get; set; } = Title;
}