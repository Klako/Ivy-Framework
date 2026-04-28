using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using Ivy.Plugins.Messaging;

namespace Ivy.Plugin.Slack;

public sealed class SlackMessagingChannel : IMessagingChannel, IDisposable
{
    private const string PostMessageUrl = "https://slack.com/api/chat.postMessage";
    private const string DeleteMessageUrl = "https://slack.com/api/chat.delete";

    private readonly HttpClient _http;

    public SlackMessagingChannel(SlackConfig config)
    {
        _http = new HttpClient();
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", config.BotToken);
    }

    public string Platform => "slack";

    public async Task<MessageResult> SendMessageAsync(string channel, Message message, CancellationToken ct = default)
    {
        var (blocks, fallbackText) = SlackRenderer.Render(message.Content);

        var payload = new JsonObject
        {
            ["channel"] = channel,
            ["text"] = fallbackText,
            ["blocks"] = blocks,
        };

        if (message.ThreadId is not null)
            payload["thread_ts"] = message.ThreadId;

        var content = new StringContent(
            payload.ToJsonString(),
            Encoding.UTF8,
            "application/json");

        var response = await _http.PostAsync(PostMessageUrl, content, ct);
        var body = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Slack API returned {response.StatusCode}: {body}");

        var result = JsonNode.Parse(body);
        if (result?["ok"]?.GetValue<bool>() != true)
        {
            var error = result?["error"]?.GetValue<string>() ?? "unknown error";
            throw new InvalidOperationException($"Slack API error: {error}");
        }

        var ts = result["ts"]?.GetValue<string>() ?? "";
        var responseChannel = result["channel"]?.GetValue<string>() ?? channel;

        return new MessageResult
        {
            MessageId = ts,
            ThreadId = ts,
            Channel = responseChannel,
            Timestamp = ParseSlackTimestamp(ts),
        };
    }

    public async Task DeleteMessageAsync(string channel, string messageId, CancellationToken ct = default)
    {
        var payload = new JsonObject
        {
            ["channel"] = channel,
            ["ts"] = messageId,
        };

        var content = new StringContent(
            payload.ToJsonString(),
            Encoding.UTF8,
            "application/json");

        var response = await _http.PostAsync(DeleteMessageUrl, content, ct);
        var body = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Slack API returned {response.StatusCode}: {body}");

        var result = JsonNode.Parse(body);
        if (result?["ok"]?.GetValue<bool>() != true)
        {
            var error = result?["error"]?.GetValue<string>() ?? "unknown error";
            throw new InvalidOperationException($"Slack API error: {error}");
        }
    }

    private static DateTimeOffset ParseSlackTimestamp(string ts)
    {
        var dotIndex = ts.IndexOf('.');
        var seconds = dotIndex >= 0 ? ts[..dotIndex] : ts;
        return long.TryParse(seconds, CultureInfo.InvariantCulture, out var unix)
            ? DateTimeOffset.FromUnixTimeSeconds(unix)
            : DateTimeOffset.UtcNow;
    }

    public void Dispose() => _http.Dispose();
}
