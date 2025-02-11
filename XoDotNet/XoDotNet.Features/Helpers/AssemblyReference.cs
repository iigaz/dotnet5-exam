using System.Reflection;

namespace XoDotNet.Features.Helpers;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}