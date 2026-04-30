using Ivy.Core.Apps;
using Ivy.Plugin.PluginManager;
using Ivy.Plugins;
using Ivy.Plugins.Messaging;
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
        services.AddSingleton(appRepository);
        var config = new ConfigurationBuilder().Build();
        var context = new TestPluginContext(services, config);
        var plugin = new PluginManagerPlugin();

        // Act
        plugin.Configure(context);

        // Assert
        var app = appRepository.GetApp("plugin-manager");
        Assert.NotNull(app);
        Assert.Equal("Plugin Manager", app.Title);
        Assert.Equal(typeof(PluginManagerApp), app.Type);
    }

    private class TestPluginContext : IPluginContext
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public TestPluginContext(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public IServiceCollection Services => _services;
        public IConfiguration Configuration => _configuration;
        public void RegisterMessagingChannel(IMessagingChannel channel) { }
    }
}
