using Ivy.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: IvyPlugin(typeof(Ivy.Plugin.Slack.SlackPlugin))]

namespace Ivy.Plugin.Slack;

public class SlackPlugin : IIvyPlugin
{
    public PluginManifest Manifest { get; } = new()
    {
        Id = "Ivy.Plugin.Slack",
        Name = "Slack",
        Version = new Version(1, 0, 0),
    };

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }

    public void Configure(IPluginContext context)
    {
        var section = context.Configuration.GetSection("Plugins:Slack");
        var botToken = section["BotToken"];

        if (string.IsNullOrEmpty(botToken))
            return;

        var config = new SlackConfig
        {
            BotToken = botToken,
            DefaultChannel = section["DefaultChannel"],
        };

        context.RegisterMessagingChannel(new SlackMessagingChannel(config));
    }
}
