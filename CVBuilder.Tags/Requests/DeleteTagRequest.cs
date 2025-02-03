namespace CVBuilder.Tags.Requests;

public sealed class DeleteTagRequest(int OriginId)
{
    public int OriginId { get; set; } = OriginId;
}