using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CVBuilder.Experiences.Tests")]

namespace CVBuilder.Experiences;
public static class ExperienceAssemblyReference
{
    public static readonly Assembly Assembly = typeof(ExperienceAssemblyReference).Assembly;
}