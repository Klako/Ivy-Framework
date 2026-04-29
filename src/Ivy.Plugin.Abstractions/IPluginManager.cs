namespace Ivy.Plugins;

public record PluginCandidate(string Id, string Directory);
public record FailedPlugin(string Directory, string Reason, DateTime FailedAt);

public interface IPluginManager
{
    IReadOnlyList<string> GetLoadedPluginIds();
    IReadOnlyList<PluginCandidate> GetUnloadedPlugins();
    IReadOnlyList<FailedPlugin> GetFailedPlugins();
    bool UnloadPlugin(string pluginId);
    bool LoadPlugin(string pluginPath);
    bool ReloadPlugin(string pluginId);
}
