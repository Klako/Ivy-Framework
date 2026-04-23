using Ivy;
using Ivy.Desktop;
using Microsoft.Extensions.Configuration;

var server = new Server();

server.UseHotReload();

server.AddConnectionsFromAssembly();
server.AddAppsFromAssembly();

var settings = new AppShellSettings()
    .UseTabs(preventDuplicates: true)
    .DefaultApp<ClerkExample.MainApp>();

server.UseAppShell(settings);

server.SetMetaTitle("Clerk Example");

Server.AuthCookiePrefix = "clerk";

server.UseConfiguration(config =>
{
    if (ProcessHelper.IsProduction())
    {
        var secretsPath = Environment.GetEnvironmentVariable("IVY_CLERK_SECRETS_PATH");
        if (!string.IsNullOrEmpty(secretsPath))
        {
            if (!File.Exists(secretsPath))
            {
                throw new FileNotFoundException(
                    $"Clerk secrets file not found at path specified by IVY_CLERK_SECRETS_PATH: '{secretsPath}'");
            }
            config.AddJsonFile(secretsPath, optional: false);
        }
    }
});

// Check for desktop launch flag
var launchDesktop = args.Contains("--desktop") || args.Contains("-d");

if (launchDesktop)
{
    var window = new DesktopWindow(server)
        .Title("Clerk Example")
        .AppId("ClerkExample")
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
