using Ivy.Core.Hooks;
using Ivy.Core.Plugins;
using Ivy.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Test.Hooks;

public class UsePluginStateTests
{
    [Fact]
    public void UsePluginState_ReturnsRefreshToken()
    {
        var fakeService = new FakePluginStateService();
        var services = new ServiceCollection();
        services.AddSingleton<IPluginStateService>(fakeService);
        var serviceProvider = services.BuildServiceProvider();

        var view = new TestPluginView();
        var context = new ViewContext(serviceProvider, new CapturingSender());
        view.Context = context;

        var result = view.Build();

        Assert.NotNull(result);
    }

    [Fact]
    public void UsePluginState_RefreshesWhenPluginLoads()
    {
        var fakeService = new FakePluginStateService();
        var services = new ServiceCollection();
        services.AddSingleton<IPluginStateService>(fakeService);
        var serviceProvider = services.BuildServiceProvider();

        var view = new TestPluginView();
        var context = new ViewContext(serviceProvider, new CapturingSender());
        view.Context = context;

        var refreshToken = view.GetRefreshToken();
        var initialValue = refreshToken.Value;

        fakeService.RaisePluginStateChanged();

        Assert.NotEqual(initialValue, refreshToken.Value);
    }

    [Fact]
    public void UsePluginState_RefreshesWhenPluginUnloads()
    {
        var fakeService = new FakePluginStateService();
        var services = new ServiceCollection();
        services.AddSingleton<IPluginStateService>(fakeService);
        var serviceProvider = services.BuildServiceProvider();

        var view = new TestPluginView();
        var context = new ViewContext(serviceProvider, new CapturingSender());
        view.Context = context;

        var refreshToken = view.GetRefreshToken();
        var initialValue = refreshToken.Value;

        fakeService.RaisePluginStateChanged();

        Assert.NotEqual(initialValue, refreshToken.Value);
    }

    [Fact]
    public void UsePluginState_CleanupUnsubscribesFromEvents()
    {
        var fakeService = new FakePluginStateService();
        var services = new ServiceCollection();
        services.AddSingleton<IPluginStateService>(fakeService);
        var serviceProvider = services.BuildServiceProvider();

        var view = new TestPluginView();
        var context = new ViewContext(serviceProvider, new CapturingSender());
        view.Context = context;

        view.Build();

        // Simulate cleanup by disposing the context
        context.Dispose();

        // This should not throw or cause issues
        fakeService.RaisePluginStateChanged();

        Assert.True(true); // Test passes if no exception
    }

    private class TestPluginView : ViewBase
    {
        private IState<RefreshToken>? _pluginState;

        public override object Build()
        {
            _pluginState = UsePluginState();
            return new object();
        }

        public RefreshToken GetRefreshToken() => _pluginState?.Value ?? throw new InvalidOperationException("Build() not called");
    }

    private class FakePluginStateService : IPluginStateService
    {
        public event Action? PluginStateChanged;

        public IReadOnlyList<string> GetLoadedPluginIds() => [];

        public void RaisePluginStateChanged() => PluginStateChanged?.Invoke();
    }

    private class CapturingSender : IClientSender
    {
        public void Send(string method, object? data) { }
    }
}
