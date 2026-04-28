using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Ivy.Plugins.Messaging;

namespace Ivy.Plugin.Slack;

public sealed class SlackMessagingChannel : IMessagingChannel, IDisposable
{
    private const string PostMessageUrl = "https://slack.com/api/chat.postMessage";

    private readonly HttpClient _http;
    private readonly SlackConfig _config;

    public SlackMessagingChannel(SlackConfig config)
    {
        _config = config;
        _http = new HttpClient();
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", config.BotToken);
    }

    public string Platform => "slack";

    public async Task SendMessageAsync(string channel, Message message, CancellationToken ct = default)
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
        if (message.Username is not null)
            payload["username"] = message.Username;
        if (message.IconEmoji is not null)
            payload["icon_emoji"] = message.IconEmoji;

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
    }

    public void Dispose() => _http.Dispose();
}
