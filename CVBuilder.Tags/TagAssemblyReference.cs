using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CVBuilder.Tags.Tests")]

namespace CVBuilder.Tags;
public static class TagAssemblyReference
{
    public static readonly Assembly Assembly = typeof(TagAssemblyReference).Assembly;
}