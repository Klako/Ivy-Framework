using System.Reflection;
using System.Runtime.Loader;

namespace Ivy.Core.Plugins;

internal class PluginAssemblyLoadContext(string pluginPath) : AssemblyLoadContext(isCollectible: false)
{
    private readonly AssemblyDependencyResolver _resolver = new(pluginPath);

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        // Don't load Ivy.Plugin.Abstractions here — use the default context's copy
        if (assemblyName.Name == "Ivy.Plugin.Abstractions")
            return null;

        var path = _resolver.ResolveAssemblyToPath(assemblyName);
        return path != null ? LoadFromAssemblyPath(path) : null;
    }
}
