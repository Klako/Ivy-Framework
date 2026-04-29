using Ivy;
using Ivy.Plugin.HelloWorld;
using Ivy.Plugins;
using static Ivy.Layout;
using static Ivy.Text;

namespace HelloWorldHost;

[App(icon: Icons.Hand, title: "Hello World")]
public class HelloWorldApp : ViewBase
{
    public override object? Build()
    {
        var plugins = this.UseService<IPluginServiceProvider>();
        var pluginManager = this.UseService<IPluginManager>();
        var greeters = plugins.GetServices<IGreeter>().ToList();
        var loadedPlugins = pluginManager.GetLoadedPluginIds();
        var unloadedPlugins = pluginManager.GetUnloadedPlugins();
        var failedPlugins = pluginManager.GetFailedPlugins();
        var nameState = UseState("World");
        var pluginStatus = UseState("");
        var refreshToken = UseRefreshToken();

        var greeting = greeters.Count > 0
            ? greeters[0].Greet(string.IsNullOrWhiteSpace(nameState.Value) ? "World" : nameState.Value)
            : "No greeter plugin loaded.";

        return Vertical().Gap(6).Padding(4)
            | H1(greeting)
            | new Field(
                nameState.ToTextInput().Placeholder("Enter a name")
            ).Label("Who to greet?")
            | Muted($"Using {greeters.Count} greeter plugin(s)")
            | new Separator()
            | H2("Plugin Management")
            | loadedPlugins.Select(id => (object)(Horizontal().Gap(4)
                | new Badge(id, BadgeVariant.Secondary)
                | new Button("Reload", onClick: _ =>
                {
                    pluginStatus.Set(pluginManager.ReloadPlugin(id)
                        ? $"Reloaded '{id}'"
                        : $"Failed to reload '{id}'");
                    refreshToken.Refresh();
                    return ValueTask.CompletedTask;
                }, variant: ButtonVariant.Outline, icon: Icons.RefreshCw)
                | new Button("Unload", onClick: _ =>
                {
                    pluginStatus.Set(pluginManager.UnloadPlugin(id)
                        ? $"Unloaded '{id}'"
                        : $"Failed to unload '{id}'");
                    refreshToken.Refresh();
                    return ValueTask.CompletedTask;
                }, variant: ButtonVariant.Outline, icon: Icons.Power)
            )).ToArray()
            | unloadedPlugins.Select(p => (object)(Horizontal().Gap(4)
                | new Badge(p.Id, BadgeVariant.Outline)
                | Muted("unloaded")
                | new Button("Load", onClick: _ =>
                {
                    pluginStatus.Set(pluginManager.LoadPlugin(p.Directory)
                        ? $"Loaded '{p.Id}'"
                        : $"Failed to load '{p.Id}'");
                    refreshToken.Refresh();
                    return ValueTask.CompletedTask;
                }, variant: ButtonVariant.Outline, icon: Icons.Plus)
            )).ToArray()
            | failedPlugins.Select(f => (object)(Horizontal().Gap(4)
                | new Badge(Path.GetFileName(f.Directory), BadgeVariant.Destructive)
                | Muted(f.Reason)
                | new Button("Retry", onClick: _ =>
                {
                    pluginStatus.Set(pluginManager.LoadPlugin(f.Directory)
                        ? $"Loaded from '{Path.GetFileName(f.Directory)}'"
                        : $"Failed to load '{Path.GetFileName(f.Directory)}'");
                    refreshToken.Refresh();
                    return ValueTask.CompletedTask;
                }, variant: ButtonVariant.Outline, icon: Icons.RefreshCw)
            )).ToArray()
            | (string.IsNullOrEmpty(pluginStatus.Value) ? null
                : pluginStatus.Value.StartsWith("Failed")
                    ? new Badge(pluginStatus.Value, BadgeVariant.Destructive)
                    : new Badge(pluginStatus.Value, BadgeVariant.Success));
    }
}
