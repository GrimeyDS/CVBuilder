using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CVBuilder.Projects.Tests")]

namespace CVBuilder.Projects;
public static class ProjectsAssemblyReference
{
    public static readonly Assembly Assembly = typeof(ProjectsAssemblyReference).Assembly;
}