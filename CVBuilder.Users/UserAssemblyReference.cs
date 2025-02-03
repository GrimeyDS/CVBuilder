using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CVBuilder.Users.Tests")]

namespace CVBuilder.Users;

public static class UserAssemblyReference
{
    public static readonly Assembly Assembly = typeof(UserAssemblyReference).Assembly;
}