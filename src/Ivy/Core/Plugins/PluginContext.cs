using System.Reflection;
using Ivy.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Core.Plugins;

internal class PluginContext(Ivy.Server server, WebApplicationBuilder builder) : IIvyPluginContext
{
    private readonly List<Func<IEnumerable<MenuItem>, IEnumerable<MenuItem>>> _menuTransformers = [];
    private readonly List<Func<IEnumerable<MenuItem>, INavigator, IEnumerable<MenuItem>>> _footerMenuTransformers = [];
    private readonly List<(string Tag, Func<IServiceProvider, int> CountProvider)> _badgeProviders = [];
    private readonly List<Action<WebApplication>> _appActions = [];

    public IServiceCollection Services => server.Services;
    public IConfiguration Configuration => server.Configuration;

    public IReadOnlyList<Func<IEnumerable<MenuItem>, IEnumerable<MenuItem>>> MenuTransformers => _menuTransformers;
    public IReadOnlyList<Func<IEnumerable<MenuItem>, INavigator, IEnumerable<MenuItem>>> FooterMenuTransformers => _footerMenuTransformers;
    public IReadOnlyList<(string Tag, Func<IServiceProvider, int> CountProvider)> BadgeProviders => _badgeProviders;

    public void AddApp(AppDescriptor descriptor)
    {
        server.AddApp(descriptor);
    }

    public void AddAppsFromAssembly(Assembly assembly)
    {
        server.AddAppsFromAssembly(assembly);
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

    public void UseWebApplication(Action<WebApplication> configure)
    {
        _appActions.Add(configure);
    }

    public void UseWebApplicationBuilder(Action<WebApplicationBuilder> configure)
    {
        configure(builder);
    }

    internal void Apply(WebApplication app)
    {
        foreach (var action in _appActions)
            action(app);
    }
}
