using System.Reflection;
using Ivy.Core.Apps;
using Ivy.Plugins;
using Ivy.Plugins.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Core.Plugins;

public abstract class PluginContextBase : IIvyPluginContext, IPluginServiceProvider
{
    private readonly List<Func<IEnumerable<MenuItem>, IEnumerable<MenuItem>>> _menuTransformers = [];
    private readonly List<Func<IEnumerable<MenuItem>, INavigator, IEnumerable<MenuItem>>> _footerMenuTransformers = [];
    private readonly List<(string Tag, Func<IServiceProvider, int> CountProvider)> _badgeProviders = [];
    private readonly List<Action<WebApplication>> _appActions = [];
    private readonly ServiceCollection _pluginServices = new();
    private IServiceProvider? _pluginServiceProvider;

    public IServiceCollection Services => _pluginServices;
    public abstract IConfiguration Configuration { get; }

    protected abstract AppRepository AppRepository { get; }
    protected abstract WebApplicationBuilder Builder { get; }

    public IReadOnlyList<Func<IEnumerable<MenuItem>, IEnumerable<MenuItem>>> MenuTransformers => _menuTransformers;
    public IReadOnlyList<Func<IEnumerable<MenuItem>, INavigator, IEnumerable<MenuItem>>> FooterMenuTransformers => _footerMenuTransformers;
    public IReadOnlyList<(string Tag, Func<IServiceProvider, int> CountProvider)> BadgeProviders => _badgeProviders;

    public void AddApp(AppDescriptor descriptor)
    {
        AppRepository.AddFactory(() => [descriptor]);
    }

    public void AddAppsFromAssembly(Assembly assembly)
    {
        AppRepository.AddFactory(() => AppHelpers.GetApps(assembly));
    }

    public void AddMenuItems(Func<IEnumerable<MenuItem>, IEnumerable<MenuItem>> transformer)
    {
        _menuTransformers.Add(transformer);
    }

    public void AddFooterMenuItems(Func<IEnumerable<MenuItem>, INavigator, IEnumerable<MenuItem>> transformer)
    {
        _footerMenuTransformers.Add(transformer);
    }

    public void AddBadgeProvider(string menuTag, Func<IServiceProvider, int> countProvider)
    {
        _badgeProviders.Add((menuTag, countProvider));
    }

    public void RegisterMessagingChannel(IMessagingChannel channel)
    {
        Services.AddSingleton<IMessagingChannel>(channel);
    }

    public void UseWebApplication(Action<WebApplication> configure)
    {
        _appActions.Add(configure);
    }

    public void UseWebApplicationBuilder(Action<WebApplicationBuilder> configure)
    {
        configure(Builder);
    }

    public void BuildServiceProvider()
    {
        _pluginServiceProvider = _pluginServices.BuildServiceProvider();
    }

    public T? GetService<T>() where T : class
    {
        return _pluginServiceProvider?.GetService<T>();
    }

    public IEnumerable<T> GetServices<T>() where T : class
    {
        return _pluginServiceProvider?.GetServices<T>() ?? [];
    }

    public void Apply(WebApplication app)
    {
        foreach (var action in _appActions)
            action(app);
    }
}

internal class PluginContext(Ivy.Server server, WebApplicationBuilder builder) : PluginContextBase
{
    public override IConfiguration Configuration => server.Configuration;
    protected override AppRepository AppRepository => server.AppRepository;
    protected override WebApplicationBuilder Builder => builder;
}
