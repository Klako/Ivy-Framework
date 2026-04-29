namespace Ivy.Plugins;

public record PluginCandidate(string Id, string Directory);

public interface IPluginManager
{
    IReadOnlyList<string> GetLoadedPluginIds();
    IReadOnlyList<PluginCandidate> GetUnloadedPlugins();
    bool UnloadPlugin(string pluginId);
    bool LoadPlugin(string pluginPath);
    bool ReloadPlugin(string pluginId);
}
