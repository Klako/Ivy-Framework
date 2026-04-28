namespace Ivy.Plugins.Messaging;

public interface IMessagingChannel
{
    string Platform { get; }

    Task<MessageResult> SendMessageAsync(
        string channel,
        Message message,
        CancellationToken ct = default);

    Task DeleteMessageAsync(
        string channel,
        string messageId,
        CancellationToken ct = default);
}
