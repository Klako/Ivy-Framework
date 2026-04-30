using Ivy;
using Ivy.Core.Apps;
using Ivy.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: IvyPlugin(typeof(Ivy.Plugin.PluginManager.PluginManagerPlugin))]

namespace Ivy.Plugin.PluginManager;

public class PluginManagerPlugin : IIvyPlugin
{
    public PluginManifest Manifest { get; } = new()
    {
        Id = "Ivy.Plugin.PluginManager",
        Name = "Plugin Manager",
        ConfigSectionName = "PluginManager",
        Version = new Version(1, 0, 0),
    };

    public PluginConfigurationSchema? ConfigurationSchema => null;

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // No additional services needed
    }

    public void Configure(IPluginContext context)
    {
        // Get the AppRepository from the service provider
        var appRepository = context.Services
            .BuildServiceProvider()
            .GetService<AppRepository>();

        if (appRepository is null)
            return;

        // Register a factory that returns our Plugin Manager app
        appRepository.AddFactory(() =>
        [
            new AppDescriptor
            {
                Id = "plugin-manager",
                Title = "Plugin Manager",
                Icon = Icons.Plug,
                Description = "Manage loaded and unloaded plugins",
                Type = typeof(PluginManagerApp),
                Group = ["Tools"],
                IsVisible = true,
                Order = 100
            }
        ]);

        // Trigger a reload so the app appears immediately
        appRepository.Reload(new HashSet<string>());
    }
}
