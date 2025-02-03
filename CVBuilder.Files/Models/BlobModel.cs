namespace CVBuilder.Files.Models;

public class BlobModel(string BlobName, byte[] Blob)
{
    public string BlobName { get; set; } = BlobName;
    public byte[] Blob { get; set; } = Blob;
}
