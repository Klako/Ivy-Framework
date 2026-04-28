using Ivy;
using Ivy.Plugins;
using Ivy.Plugins.Messaging;
using static Ivy.Layout;
using static Ivy.Text;

namespace SlackHost;

[App(icon: Icons.MessageCircle, title: "Slack Test")]
public class SlackTestApp : ViewBase
{
    public override object? Build()
    {
        var plugins = this.UseService<IPluginServiceProvider>();
        var slackChannel = plugins.GetServices<IMessagingChannel>().FirstOrDefault(c => c.Platform == "slack");

        var channelName = UseState("#general");
        var messageText = UseState("");
        var status = UseState("");
        var sending = UseState(false);

        return Vertical().Gap(6).Padding(4)
            | H1("Slack Plugin Test")
            | (slackChannel is null
                ? new Badge("No Slack plugin loaded — check Plugins:Slack:BotToken in config", BadgeVariant.Warning)
                : new Badge("Slack connected", BadgeVariant.Success))
            | new Separator()
            | new Field(
                channelName.ToTextInput().Placeholder("#channel or ID")
            ).Label("Channel")
            | new Field(
                messageText.ToTextInput().Placeholder("Type a message...")
            ).Label("Message")
            | Horizontal().Gap(4)
                | new Button("Send Plain Text", onClick: async _ =>
                {
                    if (slackChannel is null || string.IsNullOrWhiteSpace(messageText.Value)) return;
                    sending.Set(true);
                    status.Set("");
                    try
                    {
                        var msg = new MessageBuilder()
                            .Text(messageText.Value)
                            .Build();
                        await slackChannel.SendMessageAsync(channelName.Value, msg);
                        status.Set("Sent!");
                    }
                    catch (Exception ex)
                    {
                        status.Set($"Error: {ex.Message}");
                    }
                    finally
                    {
                        sending.Set(false);
                    }
                }).Disabled(slackChannel is null || sending.Value)
                | new Button("Send Rich Message", onClick: async _ =>
                {
                    if (slackChannel is null) return;
                    sending.Set(true);
                    status.Set("");
                    try
                    {
                        var msg = new MessageBuilder()
                            .Bold("Title:").Text(" Example PR notification").LineBreak()
                            .Bold("Project:").Text(" :hammer_and_wrench: Framework").LineBreak()
                            .Bold("PR:").Text(" ").Link("https://github.com/Ivy-Interactive/Ivy-Framework/pull/1", "Ivy-Interactive/Ivy-Framework#1").LineBreak()
                            .Bold("Status:").Text(" ").Italic("Ready for review")
                            .Build();
                        await slackChannel.SendMessageAsync(channelName.Value, msg);
                        status.Set("Sent!");
                    }
                    catch (Exception ex)
                    {
                        status.Set($"Error: {ex.Message}");
                    }
                    finally
                    {
                        sending.Set(false);
                    }
                }, variant: ButtonVariant.Outline).Disabled(slackChannel is null || sending.Value)
                | new Button("Send with Divider & Code", onClick: async _ =>
                {
                    if (slackChannel is null) return;
                    sending.Set(true);
                    status.Set("");
                    try
                    {
                        var msg = new MessageBuilder()
                            .Bold("Build Report")
                            .Divider()
                            .Text(":white_check_mark: All tests passed").LineBreak()
                            .Text(":package: Artifacts ready").LineBreak()
                            .Divider()
                            .CodeBlock("dotnet test --filter \"FullyQualifiedName~Ivy\"\n\nPassed! - 42 tests, 0 failed")
                            .Build();
                        await slackChannel.SendMessageAsync(channelName.Value, msg);
                        status.Set("Sent!");
                    }
                    catch (Exception ex)
                    {
                        status.Set($"Error: {ex.Message}");
                    }
                    finally
                    {
                        sending.Set(false);
                    }
                }, variant: ButtonVariant.Outline).Disabled(slackChannel is null || sending.Value)
            | (string.IsNullOrEmpty(status.Value) ? null : StatusBadge(status.Value));
    }

    private static object StatusBadge(string text) =>
        text.StartsWith("Error")
            ? new Badge(text, BadgeVariant.Destructive)
            : new Badge(text, BadgeVariant.Success);
}
