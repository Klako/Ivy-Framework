using Ivy.Core.Apps;
using Ivy.Core.Plugins;
using Ivy.Plugin.HelloWorld;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorldHost;

public class HelloWorldPluginContext(Ivy.Server server, WebApplicationBuilder builder) : PluginContextBase, IHelloWorldPluginContext
{
    private readonly List<IGreeter> _greeters = [];

    public override IServiceCollection Services => server.Services;
    public override IConfiguration Configuration => server.Configuration;
    protected override AppRepository AppRepository => server.AppRepository;
    protected override WebApplicationBuilder Builder => builder;

    public IReadOnlyList<IGreeter> Greeters => _greeters;

    public void RegisterGreeter(IGreeter greeter)
    {
        _greeters.Add(greeter);
    }
}
