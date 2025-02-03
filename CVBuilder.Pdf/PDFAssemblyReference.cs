using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CVBuilder.Pdf.Tests")]

namespace CVBuilder.Projects;
public static class PDFAssemblyReference
{
    public static readonly Assembly Assembly = typeof(PDFAssemblyReference).Assembly;
}