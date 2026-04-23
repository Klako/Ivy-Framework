using Ivy;
using Ivy.Desktop;

var server = new Server();

server.UseHotReload();

server.AddConnectionsFromAssembly();
server.AddAppsFromAssembly();

var settings = new AppShellSettings()
    .UseTabs(preventDuplicates: true)
    .DefaultApp<GitHubExample.MainApp>();

server.UseAppShell(settings);

server.SetMetaTitle("GitHub Example");

Server.AuthCookiePrefix = "github";

// Check for desktop launch flag
var launchDesktop = args.Contains("--desktop") || args.Contains("-d");

if (launchDesktop)
{
    var window = new DesktopWindow(server)
        .Title("GitHub Example")
        .AppId("GitHubExample")
        .Size(1200, 800)
        .MinSize(800, 600)
        .UseDevTools();

    return window.Run();
}
else
{
    await server.RunAsync();
}
