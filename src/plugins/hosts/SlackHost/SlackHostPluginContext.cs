using Ivy.Core.Apps;
using Ivy.Core.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SlackHost;

public class SlackHostPluginContext(Ivy.Server server, WebApplicationBuilder builder) : PluginContextBase
{
    public override IServiceCollection Services => server.Services;
    public override IConfiguration Configuration => server.Configuration;
    protected override AppRepository AppRepository => server.AppRepository;
    protected override WebApplicationBuilder Builder => builder;
}
