using Ivy;
using Ivy.Core.Plugins;
using Ivy.Plugins.Messaging;
using Microsoft.Extensions.DependencyInjection;

var server = new Server();
server.UseAppShell(new AppShellSettings());
server.AddAppsFromAssembly(typeof(Program).Assembly);

var pluginsDir = Path.GetFullPath(
    Path.Combine(System.AppContext.BaseDirectory, "..", "..", "..", "..", "..", "plugins"));

PluginContextBase? pluginContext = null;

server.UsePlugins(pluginsDir,
    contextFactory: (s, b) =>
    {
        pluginContext = new SlackHost.SlackHostPluginContext(s, b);
        return pluginContext;
    });

server.Services.AddSingleton<IReadOnlyList<IMessagingChannel>>(
    _ => pluginContext?.MessagingChannels ?? []);

await server.RunAsync();
