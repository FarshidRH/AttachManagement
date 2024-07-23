using System.Reflection;

namespace AttachManagement.Core;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
