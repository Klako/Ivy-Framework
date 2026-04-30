using Ivy.Core.Apps;
using Ivy.Plugin.PluginManager;
using Ivy.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ivy.Test.Plugins;

public class PluginManagerPluginTests
{
    [Fact]
    public void PluginManagerPlugin_InjectsAppIntoRepository()
    {
        // Arrange
        var services = new ServiceCollection();
        var appRepository = new AppRepository();
        services.AddSingleton<AppRepository>(appRepository);
        var config = new ConfigurationBuilder().Build();
        var context = new PluginContext(services, config);
        var plugin = new PluginManagerPlugin();

        // Act
        plugin.Configure(context);

        // Assert
        var app = appRepository.GetApp("plugin-manager");
        Assert.NotNull(app);
        Assert.Equal("Plugin Manager", app.Title);
        Assert.Equal(typeof(PluginManagerApp), app.Type);
    }
}
