using System.Diagnostics;
using Ivy;
using Ivy.Widgets.ClaudeJsonRenderer;

var server = new Server();
server.AddApp<ClaudeJsonRendererDemo>();
await server.RunAsync();

[App]
class ClaudeJsonRendererDemo : ViewBase
{
    public override object Build()
    {
        var prompt = Context.UseState("Explain what Ivy framework is in 2 sentences");
        var stream = Context.UseStream<string>();
        var running = Context.UseState(false);

        return Layout.Vertical().Gap(4)
            | Layout.Horizontal().Gap(2).AlignY(Align.Center)
                | new TextBox().Value(prompt).OnChange(prompt.Set).Width(Size.Full())
                | new Button(running.Value ? "Running..." : "Run Claude")
                    .OnClick(async () =>
                    {
                        if (running.Value) return;
                        running.Set(true);
                        await Task.Run(() => RunClaude(prompt.Value, stream));
                        running.Set(false);
                    })
                    .Enabled(!running.Value)
            | new ClaudeJsonRenderer()
                .Stream(stream)
                .ShowThinking(true)
                .ShowSystemEvents(true)
                .Height(Size.Px(600));
    }

    static void RunClaude(string prompt, Ivy.Hooks.IWriteStream<string> stream)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "claude",
            Arguments = $"-p \"{prompt.Replace("\"", "\\\"")}\" --output-format stream-json --verbose --max-turns 3",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using var process = Process.Start(psi);
        if (process == null) return;

        while (!process.StandardOutput.EndOfStream)
        {
            var line = process.StandardOutput.ReadLine();
            if (!string.IsNullOrWhiteSpace(line))
            {
                stream.Write(line);
            }
        }

        process.WaitForExit();
    }
}
