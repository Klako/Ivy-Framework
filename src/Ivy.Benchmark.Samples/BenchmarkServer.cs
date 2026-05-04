namespace Ivy.Benchmark.Samples;

public static class BenchmarkServer
{
    public static async Task RunAsync(ServerArgs? args = null)
    {
        var server = new Ivy.Server(args);
        server.UseCulture("en-US");
        server.AddAppsFromAssembly(typeof(BenchmarkServer).Assembly);

        var appShellSettings = new AppShellSettings()
            .UseTabs(preventDuplicates: true);
        server.UseAppShell(() => new DefaultSidebarAppShell(appShellSettings));

        await server.RunAsync();
    }
}
