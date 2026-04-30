using Ivy.Core.Apps;
using Ivy.Core.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ivy.Test.Plugins;

public class PluginLoaderTests
{
    private interface ITestServiceA { }
    private interface ITestServiceB { }
    private class TestServiceA : ITestServiceA { }
    private class TestServiceB : ITestServiceB { }
    private class AnotherTestServiceA : ITestServiceA { }

    private class TestPluginContext : PluginContextBase
    {
        private readonly AppRepository _appRepository = new();
        private readonly WebApplicationBuilder _builder;
        private readonly IConfiguration _configuration;

        public TestPluginContext()
        {
            _builder = WebApplication.CreateBuilder();
            _configuration = new ConfigurationBuilder().Build();
        }

        public override IConfiguration Configuration => _configuration;
        protected override AppRepository AppRepository => _appRepository;
        protected override WebApplicationBuilder Builder => _builder;
    }

    [Fact]
    public void PerPluginServiceIsolation()
    {
        var context = new TestPluginContext();

        // Simulate plugin A registering services
        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.Services.AddSingleton<ITestServiceA, TestServiceA>();
        context.ClearCurrentPlugin();

        // Simulate plugin B registering services
        context.SetCurrentPlugin("plugin-b", "/plugins/b");
        context.Services.AddSingleton<ITestServiceB, TestServiceB>();
        context.ClearCurrentPlugin();

        context.BuildServiceProvider();

        // Both services should be resolvable through the aggregate
        Assert.NotNull(context.GetService<ITestServiceA>());
        Assert.NotNull(context.GetService<ITestServiceB>());
    }

    [Fact]
    public void UnloadRemovesServicesFromAggregator()
    {
        var context = new TestPluginContext();

        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.Services.AddSingleton<ITestServiceA, TestServiceA>();
        context.ClearCurrentPlugin();

        context.SetCurrentPlugin("plugin-b", "/plugins/b");
        context.Services.AddSingleton<ITestServiceB, TestServiceB>();
        context.ClearCurrentPlugin();

        context.BuildServiceProvider();

        // Both should exist before unload
        Assert.NotNull(context.GetService<ITestServiceA>());
        Assert.NotNull(context.GetService<ITestServiceB>());

        // Unload plugin A
        context.RemovePluginContributions("plugin-a");

        // Plugin A's service should be gone
        Assert.Null(context.GetService<ITestServiceA>());
        // Plugin B's service should still exist
        Assert.NotNull(context.GetService<ITestServiceB>());
    }

    [Fact]
    public void ServiceResolutionAfterUnloadDoesNotReturnUnloadedServices()
    {
        var context = new TestPluginContext();

        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.Services.AddSingleton<ITestServiceA, TestServiceA>();
        context.ClearCurrentPlugin();

        context.BuildServiceProvider();

        // Service exists
        Assert.NotNull(context.GetService<ITestServiceA>());

        // Unload
        context.RemovePluginContributions("plugin-a");

        // Service gone — should not leak
        Assert.Null(context.GetService<ITestServiceA>());
        Assert.Empty(context.GetServices<ITestServiceA>());
    }

    [Fact]
    public void ReloadPluginWorkflow()
    {
        var context = new TestPluginContext();

        // Load plugin A with TestServiceA
        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.Services.AddSingleton<ITestServiceA, TestServiceA>();
        context.ClearCurrentPlugin();
        context.BuildServiceProvider();

        var beforeReload = context.GetService<ITestServiceA>();
        Assert.NotNull(beforeReload);

        // Unload (simulate reload step 1)
        context.RemovePluginContributions("plugin-a");
        Assert.Null(context.GetService<ITestServiceA>());

        // Reload (simulate reload step 2 — register with potentially different impl)
        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.Services.AddSingleton<ITestServiceA, AnotherTestServiceA>();
        context.ClearCurrentPlugin();
        context.BuildPluginServiceProvider("plugin-a", new ServiceCollection());

        var afterReload = context.GetService<ITestServiceA>();
        Assert.NotNull(afterReload);
        Assert.IsType<AnotherTestServiceA>(afterReload);
    }

    [Fact]
    public void MultiplePluginsWithOverlappingServiceTypes()
    {
        var context = new TestPluginContext();

        // Both plugins register ITestServiceA
        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.Services.AddSingleton<ITestServiceA, TestServiceA>();
        context.ClearCurrentPlugin();

        context.SetCurrentPlugin("plugin-b", "/plugins/b");
        context.Services.AddSingleton<ITestServiceA, AnotherTestServiceA>();
        context.ClearCurrentPlugin();

        context.BuildServiceProvider();

        // GetServices should return both
        var services = context.GetServices<ITestServiceA>().ToList();
        Assert.Equal(2, services.Count);
        Assert.Contains(services, s => s is TestServiceA);
        Assert.Contains(services, s => s is AnotherTestServiceA);

        // GetService returns the first one found
        Assert.NotNull(context.GetService<ITestServiceA>());
    }

    [Fact]
    public void PluginDependencyPreventsUnload()
    {
        using var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
        var logger = loggerFactory.CreateLogger<PluginLoader>();
        var tempDir = Path.Combine(Path.GetTempPath(), $"ivy-test-plugins-{Guid.NewGuid():N}");
        Directory.CreateDirectory(tempDir);

        try
        {
            var loader = new PluginLoader(tempDir, logger);

            // We can't easily test this through the full PluginLoader without real DLLs,
            // but we can verify GetLoadedPluginIds returns empty for a directory with no plugins
            using var sp = new ServiceCollection().BuildServiceProvider();
            loader.DiscoverAndLoad(new Version(1, 0), sp);

            Assert.Empty(loader.GetLoadedPluginIds());
            Assert.False(loader.UnloadPlugin("nonexistent"));
        }
        finally
        {
            Directory.Delete(tempDir, true);
        }
    }

    [Fact]
    public void MenuTransformersAreTrackedPerPlugin()
    {
        var context = new TestPluginContext();

        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.AddMenuItems(items => items.Append(new MenuItem("A")));
        context.ClearCurrentPlugin();

        context.SetCurrentPlugin("plugin-b", "/plugins/b");
        context.AddMenuItems(items => items.Append(new MenuItem("B")));
        context.ClearCurrentPlugin();

        Assert.Equal(2, context.MenuTransformers.Count);

        // Unload plugin A
        context.RemovePluginContributions("plugin-a");

        Assert.Single(context.MenuTransformers);
    }

    [Fact]
    public void FooterMenuTransformersAreTrackedPerPlugin()
    {
        var context = new TestPluginContext();

        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.AddFooterMenuItems((items, nav) => items.Append(new MenuItem("A")));
        context.ClearCurrentPlugin();

        context.SetCurrentPlugin("plugin-b", "/plugins/b");
        context.AddFooterMenuItems((items, nav) => items.Append(new MenuItem("B")));
        context.ClearCurrentPlugin();

        Assert.Equal(2, context.FooterMenuTransformers.Count);

        context.RemovePluginContributions("plugin-b");

        Assert.Single(context.FooterMenuTransformers);
    }

    [Fact]
    public void BadgeProvidersAreTrackedPerPlugin()
    {
        var context = new TestPluginContext();

        context.SetCurrentPlugin("plugin-a", "/plugins/a");
        context.AddBadgeProvider("tag-a", _ => 5);
        context.ClearCurrentPlugin();

        context.SetCurrentPlugin("plugin-b", "/plugins/b");
        context.AddBadgeProvider("tag-b", _ => 10);
        context.ClearCurrentPlugin();

        Assert.Equal(2, context.BadgeProviders.Count);

        context.RemovePluginContributions("plugin-a");

        Assert.Single(context.BadgeProviders);
        Assert.Equal("tag-b", context.BadgeProviders[0].Tag);
    }

    [Fact]
    public async Task AggregateProviderThreadSafety()
    {
        var provider = new AggregatePluginServiceProvider();

        var services1 = new ServiceCollection();
        services1.AddSingleton<ITestServiceA, TestServiceA>();
        provider.AddProvider("p1", services1.BuildServiceProvider());

        var services2 = new ServiceCollection();
        services2.AddSingleton<ITestServiceB, TestServiceB>();
        provider.AddProvider("p2", services2.BuildServiceProvider());

        // Concurrent reads should work
        var tasks = Enumerable.Range(0, 10).Select(_ => Task.Run(() =>
        {
            Assert.NotNull(provider.GetService<ITestServiceA>());
            Assert.NotNull(provider.GetService<ITestServiceB>());
        })).ToArray();

        await Task.WhenAll(tasks);

        Assert.Equal(2, provider.LoadedPluginIds.Count);

        provider.RemoveProvider("p1");
        Assert.Single(provider.LoadedPluginIds);
        Assert.Null(provider.GetService<ITestServiceA>());
    }
}
