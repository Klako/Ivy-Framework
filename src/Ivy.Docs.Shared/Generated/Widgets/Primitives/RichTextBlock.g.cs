using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:24, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/24_RichTextBlock.md", searchHints: ["rich text", "inline formatting", "text run", "mixed styling", "bold italic", "highlight", "link", "streaming text"])]
public class RichTextBlockApp(bool onlyBody = false) : ViewBase
{
    public RichTextBlockApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("richtextblock", "RichTextBlock", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("word-auto-spacing", "Word Auto-Spacing", 2), new ArticleHeading("combined-styles", "Combined Styles", 2), new ArticleHeading("links", "Links", 2), new ArticleHeading("streaming", "Streaming", 2), new ArticleHeading("basic-example", "Basic Example", 3), new ArticleHeading("simulated-llm-response", "Simulated LLM Response", 3), new ArticleHeading("buffering", "Buffering", 3), new ArticleHeading("textrun-properties", "TextRun Properties", 2), new ArticleHeading("richtextblock-properties", "RichTextBlock Properties", 2), new ArticleHeading("builder-api", "Builder API", 2), new ArticleHeading("run-methods", "Run Methods", 3), new ArticleHeading("block-level-methods", "Block-Level Methods", 3), new ArticleHeading("related-components", "Related Components", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # RichTextBlock
                
                The `RichTextBlock` [widget](app://onboarding/concepts/widgets) displays rich text composed of individually styled `TextRun` segments. Use it when you need mixed formatting within a single line or paragraph — for example, bold + normal + colored text together.
                
                For uniform text styling, use [TextBlock](app://widgets/primitives/text-block) instead.
                
                ## Basic Usage
                
                Use `Text.Rich()` to create a `RichTextBuilder`, then chain run methods to add styled segments:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class RichTextBasicDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Text.Rich()
                                .Run("Hello ")
                                .Bold("world")
                                .Run("! This is ")
                                .Italic("rich text", color: Colors.Blue)
                                .Run(".");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new RichTextBasicDemo())
            )
            | new Markdown(
                """"
                ## Word Auto-Spacing
                
                When `Word` is true, a space is automatically prepended before the content (except for the first run). This is useful for composing sentences word-by-word:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class RichTextWordDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Text.Rich()
                                .Word("This")
                                .Word("is")
                                .Word("automatically")
                                .Bold("spaced", word: true)
                                .Word("text.");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new RichTextWordDemo())
            )
            | new Markdown(
                """"
                ## Combined Styles
                
                Each run method accepts optional parameters for combining multiple styles in a single call:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class RichTextCombinedDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.Rich()
                                    .Run("Status: ")
                                    .Bold("Active", color: Colors.Green)
                                    .Run(" | ")
                                    .Run("Inactive", color: Colors.Gray, strikeThrough: true)
                                | Text.Rich()
                                    .Run("This has a ")
                                    .Bold("highlighted", highlightColor: Colors.Yellow)
                                    .Run(" word.");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new RichTextCombinedDemo())
            )
            | new Markdown(
                """"
                ## Links
                
                Use `Link()` to add clickable text runs. Links are words by default (auto-spaced). Use `OnLinkClick` to handle clicks:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class RichTextLinkDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Text.Rich()
                                .Run("Visit")
                                .Link("Ivy Docs", "https://example.com")
                                .Run("or")
                                .Link("open in new tab", "https://example.com", linkTarget: LinkTarget.Blank)
                                .OnLinkClick((Action<string>)(url => { /* handle navigation */ }));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new RichTextLinkDemo())
            )
            | new Markdown(
                """"
                ## Streaming
                
                Use [UseStream](app://hooks/core/use-stream) to dynamically append `TextRun` segments in real time — ideal for LLM responses, live logs, or any incremental text output.
                
                Call `Context.UseStream<TextRun>()` to create a stream, attach it to a `RichTextBuilder` with `.UseStream(stream)`, then call `stream.Write(...)` to push runs to the frontend as they arrive. Initial `Runs` are displayed immediately; streamed runs are appended after them.
                
                ### Basic Example
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new RichTextStreamDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class RichTextStreamDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var stream = Context.UseStream<TextRun>();
                    
                            return Layout.Vertical()
                                | Text.Rich()
                                    .Run("Thinking")
                                    .UseStream(stream)
                                | new Button("Add word", onClick: () =>
                                {
                                    stream.Write(new TextRun("...") { Word = true });
                                });
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Simulated LLM Response
                
                A more realistic example that streams words with a delay, similar to an LLM response. Use `Word = true` on each run to auto-insert spaces between words:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new RichTextLLMStreamDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class RichTextLLMStreamDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var stream = Context.UseStream<TextRun>();
                            var cts = new CancellationTokenSource();
                    
                            return Layout.Vertical()
                                | Text.Rich()
                                    .Bold("🤖 ")
                                    .UseStream(stream)
                                | new Button("Generate response").OnClick(async () =>
                                {
                                    await cts.CancelAsync();
                                    cts = new CancellationTokenSource();
                                    var token = cts.Token;
                    
                                    var words = "The meaning of life is to build great software.".Split(' ');
                                    try
                                    {
                                        foreach (var word in words)
                                        {
                                            await Task.Delay(100, token);
                                            stream.Write(new TextRun(word) { Word = true });
                                        }
                                    }
                                    catch (OperationCanceledException) { }
                                });
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Buffering
                
                By default, `UseStream<T>()` buffers data until the frontend subscribes. This means you can start writing immediately — any data written before the frontend is ready is automatically flushed once the connection is established. To disable buffering, pass `buffer: false`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("var stream = Context.UseStream<TextRun>(buffer: false);",Languages.Csharp)
            | new Markdown(
                """"
                ## TextRun Properties
                
                | Property | Type | Default | Description |
                |---|---|---|---|
                | `Content` | `string` | `""` | The text content of the run |
                | `Bold` | `bool` | `false` | Renders the text in bold |
                | `Italic` | `bool` | `false` | Renders the text in italic |
                | `StrikeThrough` | `bool` | `false` | Renders the text with a strikethrough line |
                | `Color` | `Colors?` | `null` | Sets the text color |
                | `HighlightColor` | `Colors?` | `null` | Sets the background highlight color |
                | `Word` | `bool` | `false` | Auto-prepends a space before the content (except the first run) |
                | `Link` | `string?` | `null` | When set, renders the run as a clickable link |
                | `LinkTarget` | `LinkTarget` | `Self` | Controls whether the link opens in the same tab or a new tab |
                
                ## RichTextBlock Properties
                
                | Property | Type | Default | Description |
                |---|---|---|---|
                | `Runs` | `TextRun[]` | `[]` | The text runs to display |
                | `Stream` | `IWriteStream<TextRun>?` | `null` | Stream for dynamically appending runs |
                | `TextAlignment` | `TextAlignment?` | `null` | Text alignment (Left, Center, Right) |
                | `NoWrap` | `bool` | `false` | Prevents text from wrapping |
                | `Overflow` | `Overflow?` | `null` | Overflow behavior (Auto, Clip, Ellipsis) |
                
                ## Builder API
                
                Entry point: `Text.Rich()` returns a `RichTextBuilder`.
                
                ### Run Methods
                
                | Method | Description |
                |---|---|
                | `Run(content, ...)` | Add a text run with optional styling parameters |
                | `Bold(content, ...)` | Add a bold run (shorthand for `Run` with `bold: true`) |
                | `Italic(content, ...)` | Add an italic run |
                | `StrikeThrough(content, ...)` | Add a strikethrough run |
                | `Word(content, ...)` | Add a run with `Word=true` for auto-spacing |
                | `Link(content, url, ...)` | Add a link run (word by default) |
                
                ### Block-Level Methods
                
                | Method | Description |
                |---|---|
                | `NoWrap()` | Prevent text wrapping |
                | `Overflow(overflow)` | Set overflow behavior |
                | `Density(density)` / `Small()` / `Medium()` / `Large()` | Set text scale |
                | `Align(alignment)` / `Left()` / `Center()` / `Right()` | Set text alignment |
                | `UseStream(stream)` | Attach a stream for dynamic run appending |
                | `OnLinkClick(handler)` | Set a callback for link clicks (accepts `Action<string>`, `Action<Event<...>>`, or `Func<Event<...>, ValueTask>`) |
                
                ## Related Components
                
                - [TextBlock](app://widgets/primitives/text-block) — for uniform text styling
                - [Markdown](app://widgets/primitives/markdown) — for rendering markdown content
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Primitives.TextBlockApp), typeof(Hooks.Core.UseStreamApp), typeof(Widgets.Primitives.MarkdownApp)]; 
        return article;
    }
}


public class RichTextBasicDemo : ViewBase
{
    public override object? Build()
    {
        return Text.Rich()
            .Run("Hello ")
            .Bold("world")
            .Run("! This is ")
            .Italic("rich text", color: Colors.Blue)
            .Run(".");
    }
}

public class RichTextWordDemo : ViewBase
{
    public override object? Build()
    {
        return Text.Rich()
            .Word("This")
            .Word("is")
            .Word("automatically")
            .Bold("spaced", word: true)
            .Word("text.");
    }
}

public class RichTextCombinedDemo : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.Rich()
                .Run("Status: ")
                .Bold("Active", color: Colors.Green)
                .Run(" | ")
                .Run("Inactive", color: Colors.Gray, strikeThrough: true)
            | Text.Rich()
                .Run("This has a ")
                .Bold("highlighted", highlightColor: Colors.Yellow)
                .Run(" word.");
    }
}

public class RichTextLinkDemo : ViewBase
{
    public override object? Build()
    {
        return Text.Rich()
            .Run("Visit")
            .Link("Ivy Docs", "https://example.com")
            .Run("or")
            .Link("open in new tab", "https://example.com", linkTarget: LinkTarget.Blank)
            .OnLinkClick((Action<string>)(url => { /* handle navigation */ }));
    }
}

public class RichTextStreamDemo : ViewBase
{
    public override object? Build()
    {
        var stream = Context.UseStream<TextRun>();

        return Layout.Vertical()
            | Text.Rich()
                .Run("Thinking")
                .UseStream(stream)
            | new Button("Add word", onClick: () =>
            {
                stream.Write(new TextRun("...") { Word = true });
            });
    }
}

public class RichTextLLMStreamDemo : ViewBase
{
    public override object? Build()
    {
        var stream = Context.UseStream<TextRun>();
        var cts = new CancellationTokenSource();

        return Layout.Vertical()
            | Text.Rich()
                .Bold("🤖 ")
                .UseStream(stream)
            | new Button("Generate response").OnClick(async () =>
            {
                await cts.CancelAsync();
                cts = new CancellationTokenSource();
                var token = cts.Token;

                var words = "The meaning of life is to build great software.".Split(' ');
                try
                {
                    foreach (var word in words)
                    {
                        await Task.Delay(100, token);
                        stream.Write(new TextRun(word) { Word = true });
                    }
                }
                catch (OperationCanceledException) { }
            });
    }
}
