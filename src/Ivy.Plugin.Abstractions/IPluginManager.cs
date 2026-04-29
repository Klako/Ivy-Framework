namespace Ivy.Plugins;

public interface IPluginManager
{
    IReadOnlyList<string> GetLoadedPluginIds();
    bool UnloadPlugin(string pluginId);
    bool LoadPlugin(string pluginPath);
    bool ReloadPlugin(string pluginId);
}
