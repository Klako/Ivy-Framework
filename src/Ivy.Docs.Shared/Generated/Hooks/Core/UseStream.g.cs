using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:20, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/20_UseStream.md", searchHints: ["usestream", "stream", "real-time", "streaming", "llm", "text run", "push", "websocket"])]
public class UseStreamApp(bool onlyBody = false) : ViewBase
{
    public UseStreamApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("usestream", "UseStream", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("how-it-works", "How It Works", 2), new ArticleHeading("buffering", "Buffering", 2), new ArticleHeading("when-to-use", "When to Use", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseStream").OnLinkClick(onLinkClick)
            | Lead("Create a server-to-client stream that pushes data to the frontend in real time without triggering a full [view](app://onboarding/concepts/views) re-render for each chunk, using the UseStream [hook](app://hooks/rules-of-hooks).")
            | new Markdown(
                """"
                The `UseStream<T>` [hook](app://hooks/rules-of-hooks) returns an `IWriteStream<T>` that you attach to a widget property. You then call `Write(T data)` to push chunks to the client over the existing connection. Typical uses include LLM text streaming, progress updates, and live logs.
                
                ## Overview
                
                - **Real-time push** — Data is sent to the client as you call `Write()`; no polling.
                - **No re-render per chunk** — The stream is consumed by the widget on the client; the view tree is not rebuilt for every write.
                - **Typical `T`** — Often `TextRun` for streaming rich text; `byte[]` is also supported (serialized as base64).
                
                ## Basic Usage
                
                1. Create a stream with `Context.UseStream<T>()`.
                2. Attach it to a widget that supports streaming (e.g. [RichTextBlock](app://widgets/primitives/rich-text-block) via `Text.Rich().UseStream(stream)`).
                3. Call `stream.Write(data)` from event handlers or async code to push data in real time.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class StreamingApp : ViewBase
                    {
                        public override object? Build()
                        {
                            var stream = Context.UseStream<TextRun>();
                    
                            return Layout.Vertical()
                                | Text.Rich()
                                    .Bold("🤖 ")
                                    .UseStream(stream)
                                | new Button("Generate").OnClick(async () =>
                                {
                                    var words = new[] { "Hello", "world", "from", "the", "stream!" };
                                    foreach (var word in words)
                                    {
                                        await Task.Delay(200);
                                        stream.Write(new TextRun(word) { Word = true });
                                    }
                                });
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new StreamingApp())
            )
            | new Markdown(
                """"
                ## How It Works
                
                **Hook:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("IWriteStream<T> UseStream<T>(bool buffer = true)",Languages.Csharp)
            | new Markdown(
                """"
                | Parameter | Type | Default | Description |
                |-----------|------|---------|-------------|
                | `buffer` | `bool` | `true` | When `true`, data written before the client subscribes is buffered and flushed when the subscription is established. When `false`, that data is discarded. |
                
                **Returned interface `IWriteStream<T>`:**
                
                | Member | Description |
                |--------|-------------|
                | `Id` | Unique stream identifier (used internally for client subscription). |
                | `Write(T data)` | Pushes one item to the client. Serialization: JSON for most types, base64 for `byte[]`. |
                
                ## Buffering
                
                With the default `buffer: true`, any data you write before the frontend has rendered and subscribed is buffered on the server. When the client subscribes, all buffered data is sent immediately. Use this when you want to guarantee that no chunks are lost (e.g. LLM streaming).
                
                To avoid buffering (e.g. to reduce memory or when early data is irrelevant), pass `buffer: false`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("var stream = Context.UseStream<TextRun>(buffer: false);",Languages.Csharp)
            | new Markdown(
                """"
                ## When to Use
                
                | Use UseStream for | Prefer something else when |
                |-------------------|----------------------------|
                | LLM or other token-by-token text output | One-shot text - normal [state](app://hooks/core/use-state) or [UseQuery](app://hooks/core/use-query) |
                | Progress or log tail that updates in place | Full list updates - [UseState](app://hooks/core/use-state) or [UseQuery](app://hooks/core/use-query) |
                | Pushing binary or structured chunks to a single widget | Cross-component events - [UseSignal](app://hooks/core/use-signal) |
                
                ## See Also
                
                - [RichTextBlock — Streaming](app://widgets/primitives/rich-text-block) — Main use case: streaming `TextRun` with `Text.Rich().UseStream(stream)`
                - [Rules of Hooks](app://hooks/rules-of-hooks) — Hook rules and best practices
                - [UseState](app://hooks/core/use-state) — For reactive state that triggers re-renders
                - [UseSignal](app://hooks/core/use-signal) — For cross-component messaging
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.RulesOfHooksApp), typeof(Widgets.Primitives.RichTextBlockApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseQueryApp), typeof(Hooks.Core.UseSignalApp)]; 
        return article;
    }
}


public class StreamingApp : ViewBase
{
    public override object? Build()
    {
        var stream = Context.UseStream<TextRun>();

        return Layout.Vertical()
            | Text.Rich()
                .Bold("🤖 ")
                .UseStream(stream)
            | new Button("Generate").OnClick(async () =>
            {
                var words = new[] { "Hello", "world", "from", "the", "stream!" };
                foreach (var word in words)
                {
                    await Task.Delay(200);
                    stream.Write(new TextRun(word) { Word = true });
                }
            });
    }
}
