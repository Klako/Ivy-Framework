using Ivy.Plugins;

namespace Ivy.Core.Plugins;

internal class PluginStateService : IPluginStateService
{
    private readonly IPluginManager _pluginManager;

    public event Action? PluginStateChanged;

    public PluginStateService(IPluginManager pluginManager)
    {
        _pluginManager = pluginManager;

        // Subscribe to PluginLoader events
        if (_pluginManager is PluginLoader loader)
        {
            loader.PluginLoaded += OnPluginChanged;
            loader.PluginUnloaded += OnPluginChanged;
            loader.PluginReloaded += OnPluginChanged;
        }
    }

    private void OnPluginChanged(string pluginId) => PluginStateChanged?.Invoke();

    public IReadOnlyList<string> GetLoadedPluginIds() =>
        _pluginManager.GetLoadedPluginIds();
}
