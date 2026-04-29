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
    private readonly ReaderWriterLockSlim _lock = new();
    private PluginContextBase? _pluginContext;
    private IConfiguration? _configuration;
    private IServiceProvider? _bootstrapProvider;
    private Version? _hostVersion;

    internal PluginLoader(string pluginsDirectory, ILogger<PluginLoader> logger, IEnumerable<string>? sharedAssemblyNames = null)
    {
        _pluginsDirectory = pluginsDirectory;
        _logger = logger;
        _sharedAssemblyNames = new HashSet<string>(sharedAssemblyNames ?? [])
        {
            "Ivy.Plugin.Abstractions"
        };
    }

    public IReadOnlyList<LoadedPlugin> Plugins
    {
        get
        {
            _lock.EnterReadLock();
            try { return _plugins.ToList(); }
            finally { _lock.ExitReadLock(); }
        }
    }

    public void DiscoverAndLoad(Version hostVersion, IServiceProvider serviceProvider)
    {
        _hostVersion = hostVersion;
        _bootstrapProvider = serviceProvider;

        if (!Directory.Exists(_pluginsDirectory))
        {
            _logger.LogDebug("Plugins directory not found: {Directory}", _pluginsDirectory);
            return;
        }

        var candidates = new List<(IIvyPlugin Instance, Assembly Assembly, AssemblyLoadContext Context, string Directory)>();

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
        _lock.EnterWriteLock();
        try
        {
            _plugins.AddRange(sorted.Select(c => new LoadedPlugin(c.Instance, c.Assembly, c.Context, c.Directory)));
        }
        finally
        {
            _lock.ExitWriteLock();
        }

        foreach (var plugin in _plugins)
            _logger.LogInformation("Loaded plugin: {Id} v{Version}", plugin.Instance.Manifest.Id, plugin.Instance.Manifest.Version);
    }

    private (IIvyPlugin Instance, Assembly Assembly, AssemblyLoadContext Context, string Directory)? LoadPluginFromDirectory(
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
        return (instance, pluginAssembly, context, directory);
    }

    private List<(IIvyPlugin Instance, Assembly Assembly, AssemblyLoadContext Context, string Directory)> TopologicalSort(
        List<(IIvyPlugin Instance, Assembly Assembly, AssemblyLoadContext Context, string Directory)> candidates)
    {
        var byId = candidates.ToDictionary(c => c.Instance.Manifest.Id);
        var visited = new HashSet<string>();
        var visiting = new HashSet<string>(); // cycle detection
        var sorted = new List<(IIvyPlugin, Assembly, AssemblyLoadContext, string)>();

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

    public void ConfigureServices(IServiceCollection hostServices, IConfiguration configuration)
    {
        _configuration = configuration;
        _lock.EnterReadLock();
        try
        {
            foreach (var plugin in _plugins)
                plugin.Instance.ConfigureServices(plugin.Services, configuration);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void Configure(PluginContextBase context)
    {
        _pluginContext = context;
        _lock.EnterReadLock();
        try
        {
            foreach (var plugin in _plugins)
            {
                context.SetCurrentPlugin(plugin.Instance.Manifest.Id, plugin.Directory);
                plugin.Instance.Configure(context);
                context.ClearCurrentPlugin();
            }
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public bool UnloadPlugin(string pluginId)
    {
        _lock.EnterWriteLock();
        try
        {
            var plugin = _plugins.FirstOrDefault(p => p.Instance.Manifest.Id == pluginId);
            if (plugin is null)
            {
                _logger.LogWarning("Plugin '{Id}' not found for unload.", pluginId);
                return false;
            }

            // Check if other loaded plugins depend on this one
            var dependents = _plugins
                .Where(p => p.Instance.Manifest.Id != pluginId &&
                            p.Instance.Manifest.Dependencies.Contains(pluginId))
                .Select(p => p.Instance.Manifest.Id)
                .ToList();

            if (dependents.Count > 0)
            {
                _logger.LogError(
                    "Cannot unload plugin '{Id}': plugins [{Dependents}] depend on it.",
                    pluginId, string.Join(", ", dependents));
                return false;
            }

            // Remove contributions from context
            _pluginContext?.RemovePluginContributions(pluginId);

            // Dispose the plugin's service provider
            (plugin.ServiceProvider as IDisposable)?.Dispose();

            // Unload the assembly context
            plugin.LoadContext.Unload();

            _plugins.Remove(plugin);
            _logger.LogInformation("Unloaded plugin: {Id}", pluginId);
            return true;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool LoadPlugin(string pluginPath)
    {
        if (_bootstrapProvider is null || _hostVersion is null)
        {
            _logger.LogError("Cannot load plugin: PluginLoader has not been initialized via DiscoverAndLoad.");
            return false;
        }

        _lock.EnterWriteLock();
        try
        {
            var loaded = LoadPluginFromDirectory(pluginPath, _bootstrapProvider);
            if (loaded is null)
            {
                _logger.LogError("Failed to load plugin from {Path}.", pluginPath);
                return false;
            }

            var manifest = loaded.Value.Instance.Manifest;

            if (_plugins.Any(p => p.Instance.Manifest.Id == manifest.Id))
            {
                _logger.LogError("Plugin '{Id}' is already loaded.", manifest.Id);
                return false;
            }

            if (manifest.MinimumHostVersion is { } minVersion && _hostVersion < minVersion)
            {
                _logger.LogError(
                    "Plugin '{Id}' requires host version {Required} but current is {Current}.",
                    manifest.Id, minVersion, _hostVersion);
                return false;
            }

            // Check dependencies are loaded
            foreach (var dep in manifest.Dependencies)
            {
                if (_plugins.All(p => p.Instance.Manifest.Id != dep))
                {
                    _logger.LogError("Plugin '{Id}' depends on '{Dep}' which is not loaded.", manifest.Id, dep);
                    return false;
                }
            }

            var plugin = new LoadedPlugin(loaded.Value.Instance, loaded.Value.Assembly, loaded.Value.Context, loaded.Value.Directory);

            // Configure services
            if (_configuration is not null)
                plugin.Instance.ConfigureServices(plugin.Services, _configuration);

            // Configure context
            if (_pluginContext is not null)
            {
                _pluginContext.SetCurrentPlugin(manifest.Id, pluginPath);
                plugin.Instance.Configure(_pluginContext);
                _pluginContext.ClearCurrentPlugin();
                _pluginContext.BuildPluginServiceProvider(manifest.Id, plugin.Services);
                plugin.ServiceProvider = _pluginContext.GetPluginServiceProvider(manifest.Id);
            }

            _plugins.Add(plugin);
            _logger.LogInformation("Loaded plugin: {Id} v{Version}", manifest.Id, manifest.Version);
            return true;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool ReloadPlugin(string pluginId)
    {
        _lock.EnterReadLock();
        string? directory;
        try
        {
            var plugin = _plugins.FirstOrDefault(p => p.Instance.Manifest.Id == pluginId);
            if (plugin is null)
            {
                _logger.LogWarning("Plugin '{Id}' not found for reload.", pluginId);
                return false;
            }
            directory = plugin.Directory;
        }
        finally
        {
            _lock.ExitReadLock();
        }

        if (!UnloadPlugin(pluginId)) return false;
        return LoadPlugin(directory);
    }

    public IReadOnlyList<string> GetLoadedPluginIds()
    {
        _lock.EnterReadLock();
        try
        {
            return _plugins.Select(p => p.Instance.Manifest.Id).ToList();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
}

public record LoadedPlugin(
    IIvyPlugin Instance,
    Assembly Assembly,
    AssemblyLoadContext LoadContext,
    string Directory)
{
    public ServiceCollection Services { get; } = new();
    public IServiceProvider? ServiceProvider { get; internal set; }
}
