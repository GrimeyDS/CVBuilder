using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CVBuilder.Profiles.Tests")]

namespace CVBuilder.Projects;
public static class ProfileAssemblyReference
{
    public static readonly Assembly Assembly = typeof(ProfileAssemblyReference).Assembly;
}