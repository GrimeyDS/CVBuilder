namespace CVBuilder.Pdf
{
    public interface IPDFService
    {
        Task<byte[]> CreatePdf();
    }
}