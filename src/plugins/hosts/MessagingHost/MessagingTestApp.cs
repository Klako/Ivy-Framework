using Ivy;
using Ivy.Plugins;
using Ivy.Plugins.Messaging;
using static Ivy.Layout;
using static Ivy.Text;

namespace MessagingHost;

[App(icon: Icons.MessageCircle, title: "Messaging Test")]
public class MessagingTestApp : ViewBase
{
    public override object? Build()
    {
        var plugins = this.UseService<IPluginServiceProvider>();
        var channels = plugins.GetServices<IMessagingChannel>().ToList();

        var selectedPlatform = UseState(channels.FirstOrDefault()?.Platform ?? "");
        var channelName = UseState("#ivy-plugin-test");
        var messageText = UseState("Hello from an Ivy messaging plugin!");
        var threadId = UseState("");
        var status = UseState("");
        var sending = UseState(false);

        var activeChannel = channels.FirstOrDefault(c => c.Platform == selectedPlatform.Value);

        return Vertical().Gap(6).Padding(4)
            | H1("Messaging Test")
            | (channels.Count == 0
                ? new Badge("No messaging plugins loaded", BadgeVariant.Warning)
                : new Badge($"{channels.Count} channel(s): {string.Join(", ", channels.Select(c => c.Platform))}", BadgeVariant.Success))
            | new Separator()
            | (channels.Count > 1
                ? new Field(
                    selectedPlatform.ToSelectInput(channels.Select(c => c.Platform))
                  ).Label("Platform")
                : null)
            | new Field(
                channelName.ToTextInput().Placeholder("#channel or ID")
            ).Label("Channel")
            | new Field(
                messageText.ToTextInput().Placeholder("Type a message...")
            ).Label("Message")
            | new Field(
                threadId.ToTextInput().Placeholder("e.g. 1234567890.123456")
            ).Label("Thread ID")
            | Horizontal().Gap(4)
                | new Button("Send Plain Text", onClick: async _ =>
                {
                    if (activeChannel is null || string.IsNullOrWhiteSpace(messageText.Value)) return;
                    sending.Set(true);
                    status.Set("");
                    try
                    {
                        var msg = new MessageBuilder()
                            .Text(messageText.Value)
                            .Build(threadId: NullIfEmpty(threadId.Value));
                        await activeChannel.SendMessageAsync(channelName.Value, msg);
                        status.Set($"Sent via {activeChannel.Platform}!");
                    }
                    catch (Exception ex)
                    {
                        status.Set($"Error: {ex.Message}");
                    }
                    finally
                    {
                        sending.Set(false);
                    }
                }).Disabled(activeChannel is null || sending.Value)
                | new Button("Send Rich Message", onClick: async _ =>
                {
                    if (activeChannel is null) return;
                    sending.Set(true);
                    status.Set("");
                    try
                    {
                        var msg = new MessageBuilder()
                            .Bold("Title:").Text(" Example PR notification").LineBreak()
                            .Bold("Project:").Text(" :hammer_and_wrench: Framework").LineBreak()
                            .Bold("PR:").Text(" ").Link("https://github.com/Ivy-Interactive/Ivy-Framework/pull/1", "Ivy-Interactive/Ivy-Framework#1").LineBreak()
                            .Bold("Status:").Text(" ").Italic("Ready for review")
                            .Build(threadId: NullIfEmpty(threadId.Value));
                        await activeChannel.SendMessageAsync(channelName.Value, msg);
                        status.Set($"Sent via {activeChannel.Platform}!");
                    }
                    catch (Exception ex)
                    {
                        status.Set($"Error: {ex.Message}");
                    }
                    finally
                    {
                        sending.Set(false);
                    }
                }, variant: ButtonVariant.Outline).Disabled(activeChannel is null || sending.Value)
                | new Button("Send with Divider & Code", onClick: async _ =>
                {
                    if (activeChannel is null) return;
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
                            .Build(threadId: NullIfEmpty(threadId.Value));
                        await activeChannel.SendMessageAsync(channelName.Value, msg);
                        status.Set($"Sent via {activeChannel.Platform}!");
                    }
                    catch (Exception ex)
                    {
                        status.Set($"Error: {ex.Message}");
                    }
                    finally
                    {
                        sending.Set(false);
                    }
                }, variant: ButtonVariant.Outline).Disabled(activeChannel is null || sending.Value)
            | (string.IsNullOrEmpty(status.Value) ? null : StatusBadge(status.Value));
    }

    private static string? NullIfEmpty(string value) =>
        string.IsNullOrWhiteSpace(value) ? null : value;

    private static object StatusBadge(string text) =>
        text.StartsWith("Error")
            ? new Badge(text, BadgeVariant.Destructive)
            : new Badge(text, BadgeVariant.Success);
}
