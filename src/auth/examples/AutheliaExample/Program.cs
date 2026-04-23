using Ivy;
using Ivy.Desktop;

var server = new Server();

server.UseHotReload();

server.AddConnectionsFromAssembly();
server.AddAppsFromAssembly();

var settings = new AppShellSettings()
    .UseTabs(preventDuplicates: true)
    .DefaultApp<AutheliaExample.MainApp>();

server.UseAppShell(settings);

server.SetMetaTitle("Authelia Example");

Server.AuthCookiePrefix = "authelia";

// Check for desktop launch flag
var launchDesktop = args.Contains("--desktop") || args.Contains("-d");

if (launchDesktop)
{
    var window = new DesktopWindow(server)
        .Title("Authelia Example")
        .AppId("AutheliaExample")
        .Size(1200, 800)
        .MinSize(800, 600)
        .UseDevTools();

    return window.Run();
}
else
{
    await server.RunAsync();
    return 0;
}
