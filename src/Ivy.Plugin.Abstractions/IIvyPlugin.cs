using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Plugins;

public interface IIvyPlugin
{
    PluginManifest Manifest { get; }
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    void Configure(IPluginContext context);
}
