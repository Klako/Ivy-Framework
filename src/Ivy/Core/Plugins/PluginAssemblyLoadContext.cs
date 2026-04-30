using System.Reflection;
using System.Runtime.Loader;

namespace Ivy.Core.Plugins;

internal class PluginAssemblyLoadContext(string pluginPath, IReadOnlySet<string> sharedAssemblyNames)
    : AssemblyLoadContext(isCollectible: true)
{
    private readonly AssemblyDependencyResolver _resolver = new(pluginPath);

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        // Shared assemblies are loaded from the host so types match across contexts
        if (sharedAssemblyNames.Contains(assemblyName.Name!))
            return null; // fall back to Default context

        var path = _resolver.ResolveAssemblyToPath(assemblyName);
        return path != null ? LoadFromAssemblyPath(path) : null;
    }
}
