using HelloWorldHost;
using Ivy;
using Ivy.Plugin.HelloWorld;
using Microsoft.Extensions.DependencyInjection;

HelloWorldPluginContext? helloWorldContext = null;

var server = new Server();
server.UseAppShell(new AppShellSettings());
server.AddAppsFromAssembly(typeof(Program).Assembly);
var pluginsDir = Path.GetFullPath(Path.Combine(System.AppContext.BaseDirectory, "..", "..", "..", "..", "..", "plugins"));
server.UsePlugins(pluginsDir,
    contextFactory: (s, b) =>
    {
        helloWorldContext = new HelloWorldPluginContext(s, b);
        return helloWorldContext;
    },
    sharedAssemblyNames: ["Ivy.Plugin.HelloWorld.Abstractions"]);

// Register the greeters collected by the plugin context so apps can resolve them
server.Services.AddSingleton<IReadOnlyList<IGreeter>>(
    _ => helloWorldContext?.Greeters ?? []);

await server.RunAsync();
