namespace CVBuilder.Shared.Requests;

public sealed class UpdateTagsRequest(List<int> TagIds)
{
    public List<int> TagIds { get; set; } = TagIds;
}
