using Ivy.Auth.Sliplane;
using Ivy.Desktop;

var server = new Server();

server.UseHotReload();

server.UseAuth<SliplaneAuthProvider>();

server.AddAppsFromAssembly();

var settings = new AppShellSettings()
    .UseTabs(preventDuplicates: true)
    .DefaultApp<SliplaneExample.MainApp>();

server.UseAppShell(settings);

server.SetMetaTitle("Sliplane Example");

Server.AuthCookiePrefix = "sliplane";

// Check for desktop launch flag
var launchDesktop = args.Contains("--desktop") || args.Contains("-d");

if (launchDesktop)
{
    var window = new DesktopWindow(server)
        .Title("Sliplane Example")
        .AppId("SliplaneExample")
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

