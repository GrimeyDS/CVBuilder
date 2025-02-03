using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CVBuilder.Files.Tests")]

namespace CVBuilder.Files;
public static class FileAssemblyReference
{
    public static readonly Assembly Assembly = typeof(FileAssemblyReference).Assembly;
}