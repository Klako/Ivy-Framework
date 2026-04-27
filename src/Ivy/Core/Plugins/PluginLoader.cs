using System.Reflection;
using System.Runtime.Loader;
using Ivy.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ivy.Core.Plugins;

public class PluginLoader
{
    private readonly string _pluginsDirectory;
    private readonly ILogger<PluginLoader> _logger;
    private readonly IReadOnlySet<string> _sharedAssemblyNames;
    private readonly List<LoadedPlugin> _plugins = [];

    internal PluginLoader(string pluginsDirectory, ILogger<PluginLoader> logger, IEnumerable<string>? sharedAssemblyNames = null)
    {
        _pluginsDirectory = pluginsDirectory;
        _logger = logger;
        _sharedAssemblyNames = new HashSet<string>(sharedAssemblyNames ?? [])
        {
            "Ivy.Plugin.Abstractions"
        };
    }

    public IReadOnlyList<LoadedPlugin> Plugins => _plugins;

    public void DiscoverAndLoad(Version hostVersion, IServiceProvider serviceProvider)
    {
        if (!Directory.Exists(_pluginsDirectory))
        {
            _logger.LogDebug("Plugins directory not found: {Directory}", _pluginsDirectory);
            return;
        }

        var candidates = new List<(IIvyPlugin Instance, Assembly Assembly, AssemblyLoadContext Context)>();

        foreach (var directory in Directory.GetDirectories(_pluginsDirectory))
        {
            try
            {
                var loaded = LoadPluginFromDirectory(directory, serviceProvider);
                if (loaded is null) continue;

                var manifest = loaded.Value.Instance.Manifest;

                if (manifest.MinimumHostVersion is { } minVersion && hostVersion < minVersion)
                {
                    _logger.LogError(
                        "Plugin '{Id}' requires host version {Required} but current is {Current}. Skipping.",
                        manifest.Id, minVersion, hostVersion);
                    continue;
                }

                candidates.Add(loaded.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load plugin from {Directory}. Skipping.", directory);
            }
        }

        var sorted = TopologicalSort(candidates);
        _plugins.AddRange(sorted.Select(c => new LoadedPlugin(c.Instance, c.Assembly, c.Context)));

        foreach (var plugin in _plugins)
            _logger.LogInformation("Loaded plugin: {Id} v{Version}", plugin.Instance.Manifest.Id, plugin.Instance.Manifest.Version);
    }

    private (IIvyPlugin Instance, Assembly Assembly, AssemblyLoadContext Context)? LoadPluginFromDirectory(
        string directory, IServiceProvider serviceProvider)
    {
        var dllFiles = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories)
            .Where(f => !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}"))
            .ToArray();
        if (dllFiles.Length == 0)
        {
            _logger.LogWarning("No DLL files found in plugin directory: {Directory}", directory);
            return null;
        }

        var found = new List<(Type PluginType, Assembly Assembly, AssemblyLoadContext Context)>();

        foreach (var dllPath in dllFiles)
        {
            var loadContext = new PluginAssemblyLoadContext(dllPath, _sharedAssemblyNames);
            Assembly assembly;
            try
            {
                assembly = loadContext.LoadFromAssemblyPath(dllPath);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Could not load {Dll}, skipping.", dllPath);
                continue;
            }

            var attr = assembly.GetCustomAttribute<IvyPluginAttribute>();
            if (attr is null) continue;

            if (!typeof(IIvyPlugin).IsAssignableFrom(attr.PluginType))
            {
                _logger.LogError(
                    "Assembly {Dll} has [IvyPlugin({Type})] but that type does not implement IIvyPlugin. Skipping directory.",
                    dllPath, attr.PluginType.FullName);
                return null;
            }

            found.Add((attr.PluginType, assembly, loadContext));
        }

        if (found.Count == 0)
        {
            _logger.LogDebug("No [assembly: IvyPlugin] attribute found in {Directory}. Skipping.", directory);
            return null;
        }

        if (found.Count > 1)
        {
            _logger.LogError(
                "Multiple [assembly: IvyPlugin] attributes found in {Directory} ({Assemblies}). Skipping.",
                directory, string.Join(", ", found.Select(f => Path.GetFileName(f.Assembly.Location))));
            return null;
        }

        var (pluginType, pluginAssembly, context) = found[0];
        var instance = (IIvyPlugin)ActivatorUtilities.CreateInstance(serviceProvider, pluginType);
        return (instance, pluginAssembly, context);
    }

    private List<(IIvyPlugin Instance, Assembly Assembly, AssemblyLoadContext Context)> TopologicalSort(
        List<(IIvyPlugin Instance, Assembly Assembly, AssemblyLoadContext Context)> candidates)
    {
        var byId = candidates.ToDictionary(c => c.Instance.Manifest.Id);
        var visited = new HashSet<string>();
        var visiting = new HashSet<string>(); // cycle detection
        var sorted = new List<(IIvyPlugin, Assembly, AssemblyLoadContext)>();

        foreach (var candidate in candidates)
        {
            Visit(candidate.Instance.Manifest.Id);
        }

        return sorted;

        void Visit(string id)
        {
            if (visited.Contains(id)) return;

            if (!visiting.Add(id))
            {
                _logger.LogError("Circular dependency detected involving plugin '{Id}'. Skipping.", id);
                return;
            }

            if (byId.TryGetValue(id, out var candidate))
            {
                foreach (var dep in candidate.Instance.Manifest.Dependencies)
                {
                    if (!byId.ContainsKey(dep))
                    {
                        _logger.LogWarning("Plugin '{Id}' depends on '{Dep}' which is not loaded.", id, dep);
                        continue;
                    }
                    Visit(dep);
                }

                sorted.Add(candidate);
            }

            visiting.Remove(id);
            visited.Add(id);
        }
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        foreach (var plugin in _plugins)
            plugin.Instance.ConfigureServices(services, configuration);
    }

    public void Configure(IPluginContext context)
    {
        foreach (var plugin in _plugins)
            plugin.Instance.Configure(context);
    }
}

public record LoadedPlugin(
    IIvyPlugin Instance,
    Assembly Assembly,
    AssemblyLoadContext LoadContext
);
