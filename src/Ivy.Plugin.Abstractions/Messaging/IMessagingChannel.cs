namespace Ivy.Plugins.Messaging;

public interface IMessagingChannel
{
    string Platform { get; }

    Task SendMessageAsync(
        string channel,
        Message message,
        CancellationToken ct = default);
}
