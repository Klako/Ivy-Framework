using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:14, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/14_Markdown.md", searchHints: ["formatting", "markup", "markdown", "md", "text", "content"])]
public class MarkdownApp(bool onlyBody = false) : ViewBase
{
    public MarkdownApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("markdown", "Markdown", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("tables", "Tables", 3), new ArticleHeading("code-blocks", "Code Blocks", 3), new ArticleHeading("math", "Math", 3), new ArticleHeading("mermaid-diagrams", "Mermaid Diagrams", 3), new ArticleHeading("emojis", "Emojis", 3), new ArticleHeading("text-alignment", "Text Alignment", 3), new ArticleHeading("html-support-and-link-handling", "HTML Support and Link Handling", 3), new ArticleHeading("complete-example", "Complete Example", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Markdown").OnLinkClick(onLinkClick)
            | Lead("Render rich Markdown content with syntax highlighting, math support, tables, and interactive features in your Ivy applications..")
            | new Markdown(
                """"
                The `Markdown` [widget](app://onboarding/concepts/widgets) renders Markdown content as formatted [HTML](app://widgets/primitives/html) with syntax highlighting, math support, tables, images, and interactive link handling.
                
                ## Basic Usage
                
                The Markdown widget supports standard markdown syntax including text formatting, lists, links, and blockquotes. This example demonstrates the most commonly used features for basic content creation.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicMarkdownView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicMarkdownView : ViewBase
                    {
                        public override object? Build()
                        {
                            var markdownContent =
                                """
                                Hello World
                    
                                This is **bold** and *italic* text with `inline code`.
                    
                                - Unordered list item
                                - [x] Task list item
                    
                                [Link to Google](https://www.google.com)
                    
                                > This is a blockquote with **bold** text.
                                """;
                    
                            return new Markdown(markdownContent);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Tables
                
                Tables in Markdown provide a structured way to display data in rows and columns. They support alignment, headers, and can be easily formatted for better readability.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TablesMarkdownView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TablesMarkdownView : ViewBase
                    {
                        public override object? Build()
                        {
                            var markdownContent =
                                """
                                | Feature        | Basic | Premium | Enterprise |
                                |----------------|-------|---------|------------|
                                | Users          | 1     | 10      | Unlimited  |
                                | Storage        | 1GB   | 100GB   | 1TB        |
                                | Support        | Email | Phone   | 24/7       |
                                """;
                    
                            return new Markdown(markdownContent);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Code Blocks
                
                Code blocks support syntax highlighting for various programming languages and can be used for displaying code examples, configuration files, or any formatted text. The language is automatically detected based on the code fence specification.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CodeBlocksMarkdownView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CodeBlocksMarkdownView : ViewBase
                    {
                        public override object? Build()
                        {
                            var markdownContent =
                                """
                                ```csharp
                                public class Example
                                {
                                    public void Demo() => Console.WriteLine("Hello, World!");
                                }
                                ```
                                """;
                    
                            return new Markdown(markdownContent);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Math
                
                Mathematical expressions can be rendered using KaTeX, supporting both inline math with single dollar signs and block math with double dollar signs. This feature is perfect for technical documentation and educational content.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MathMarkdownView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class MathMarkdownView : ViewBase
                    {
                        public override object? Build()
                        {
                            var markdownContent =
                                """
                                Inline: $E = mc^2$
                    
                                $$
                                \int_a^b f(x) dx = F(b) - F(a)
                                $$
                                """;
                    
                            return new Markdown(markdownContent);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Mermaid Diagrams
                
                Mermaid diagrams allow you to create various types of visual diagrams directly in markdown content. Supported diagram types include flowcharts, sequence diagrams, class diagrams, and more for visualizing complex processes and relationships.
                
                For more information on how to use Mermaid, [follow this link](https://mermaid.js.org/):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MermaidView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class MermaidView : ViewBase
                    {
                        public override object? Build()
                        {
                            var markdownContent =
                                """
                                ```mermaid
                                sequenceDiagram
                                    participant U as User
                                    participant F as Frontend
                                    participant B as Backend
                    
                                    U->>F: Navigate to page
                                    F->>B: GET /api/data
                                    B-->>F: JSON response
                                    F-->>U: Render UI
                                ```
                                """;
                    
                            return new Markdown(markdownContent);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Emojis
                
                Emoji support enhances content with visual elements and expressions. You can use standard emoji shortcodes to add personality and visual appeal to your markdown content.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new EmojiView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class EmojiView : ViewBase
                    {
                        public override object? Build()
                        {
                            var markdownContent =
                                """
                                Express yourself! :smile: :heart: :star: :rocket:
                    
                                **People:** :smile: :wink: :heart_eyes: :thumbsup:
                                **Nature:** :sunny: :cloud: :zap: :snowflake:
                                **Objects:** :computer: :phone: :bulb: :gear:
                                """;
                    
                            return new Markdown(markdownContent);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Text Alignment
                
                Align the whole markdown block within its container using `.Align(TextAlignment)` with `TextAlignment.Left`, `TextAlignment.Center`, `TextAlignment.Right`, or `TextAlignment.Justify`, or use the shorthand methods `.Left()`, `.Center()`, `.Right()`, `.Justify()`. Default is left.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TextAlignmentMarkdownView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TextAlignmentMarkdownView : ViewBase
                    {
                        public override object? Build()
                        {
                            var content = "Aligned text";
                            var longContent = "This paragraph is long enough to wrap across several lines, so justify alignment spreads the text to fill the full width of the container.";
                            return new StackLayout([
                                new Markdown(content),
                                new Markdown(content).Center(),
                                new Markdown(content).Right(),
                                new Markdown(longContent).Justify()
                            ]);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### HTML Support and Link Handling
                
                The Markdown widget supports HTML tags for advanced formatting and provides interactive link handling through the OnLinkClick event. This allows for custom navigation logic and enhanced user interactions.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new HtmlAndLinksView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class HtmlAndLinksView : ViewBase
                    {
                        public override object? Build()
                        {
                            var markdownContent =
                                """
                                <details>
                                    <summary>Click to expand</summary>
                                    Hidden content with **markdown** support.
                                </details>
                    
                                - [Navigate to Home](/home)
                                - [External Link](https://example.com)
                                """;
                    
                            Action<string> handleLink = url =>
                            {
                                if (url.StartsWith("/"))
                                {
                                    Console.WriteLine($"Navigating to: {url}");
                                }
                                else
                                {
                                    Console.WriteLine($"Opening external link: {url}");
                                }
                            };
                    
                            return new Markdown(markdownContent)
                                .OnLinkClick(handleLink);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Complete Example
                
                This comprehensive example showcases multiple Markdown features working together in a single widget. It demonstrates how different elements can be combined to create rich, interactive content with proper link handling.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ComprehensiveMarkdownView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ComprehensiveMarkdownView : ViewBase
                    {
                        public override object? Build()
                        {
                            var markdownContent =
                                """
                                **Bold text** and *italic text* with `inline code`
                    
                                - Item 1
                                - [x] Completed task
                                - [ ] Pending task
                    
                                ```csharp
                                public class Example
                                {
                                    public void ShowFeatures() => Console.WriteLine("Markdown!");
                                }
                                ```
                    
                                Inline equation: $f(x) = x^2 + 2x + 1$
                    
                                | Language | Type       | Performance |
                                |----------|------------|-------------|
                                | C#       | Compiled   | High        |
                                | Python   | Interpreted| Moderate    |
                    
                                ```mermaid
                                graph LR
                                    A[Start] --> B[Process] --> C[End]
                                ```
                    
                                Made with :heart: using Ivy Framework :rocket:
                                """;
                    
                            Action<string> handleLink = url => Console.WriteLine($"Navigate to: {url}");
                    
                            return new Markdown(markdownContent)
                                .OnLinkClick(handleLink);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Markdown", "Ivy.MarkdownExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Markdown.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Primitives.HtmlApp)]; 
        return article;
    }
}


public class BasicMarkdownView : ViewBase
{
    public override object? Build()
    {
        var markdownContent = 
            """
            Hello World
            
            This is **bold** and *italic* text with `inline code`.
            
            - Unordered list item
            - [x] Task list item
            
            [Link to Google](https://www.google.com)
            
            > This is a blockquote with **bold** text.
            """;
            
        return new Markdown(markdownContent);
    }
}

public class TablesMarkdownView : ViewBase
{
    public override object? Build()
    {
        var markdownContent = 
            """
            | Feature        | Basic | Premium | Enterprise |
            |----------------|-------|---------|------------|
            | Users          | 1     | 10      | Unlimited  |
            | Storage        | 1GB   | 100GB   | 1TB        |
            | Support        | Email | Phone   | 24/7       |
            """;
            
        return new Markdown(markdownContent);
    }
}

public class CodeBlocksMarkdownView : ViewBase
{
    public override object? Build()
    {
        var markdownContent = 
            """
            ```csharp
            public class Example
            {
                public void Demo() => Console.WriteLine("Hello, World!");
            }
            ```
            """;
            
        return new Markdown(markdownContent);
    }
}

public class MathMarkdownView : ViewBase
{
    public override object? Build()
    {
        var markdownContent = 
            """
            Inline: $E = mc^2$
            
            $$
            \int_a^b f(x) dx = F(b) - F(a)
            $$
            """;
            
        return new Markdown(markdownContent);
    }
}

public class MermaidView : ViewBase
{
    public override object? Build()
    {
        var markdownContent = 
            """
            ```mermaid
            sequenceDiagram
                participant U as User
                participant F as Frontend
                participant B as Backend
                
                U->>F: Navigate to page
                F->>B: GET /api/data
                B-->>F: JSON response
                F-->>U: Render UI
            ```
            """;
            
        return new Markdown(markdownContent);
    }
}

public class EmojiView : ViewBase
{
    public override object? Build()
    {
        var markdownContent = 
            """
            Express yourself! :smile: :heart: :star: :rocket:
            
            **People:** :smile: :wink: :heart_eyes: :thumbsup:
            **Nature:** :sunny: :cloud: :zap: :snowflake:
            **Objects:** :computer: :phone: :bulb: :gear:
            """;
            
        return new Markdown(markdownContent);
    }
}

public class TextAlignmentMarkdownView : ViewBase
{
    public override object? Build()
    {
        var content = "Aligned text";
        var longContent = "This paragraph is long enough to wrap across several lines, so justify alignment spreads the text to fill the full width of the container.";
        return new StackLayout([
            new Markdown(content),
            new Markdown(content).Center(),
            new Markdown(content).Right(),
            new Markdown(longContent).Justify()
        ]);
    }
}

public class HtmlAndLinksView : ViewBase
{
    public override object? Build()
    {
        var markdownContent = 
            """
            <details>
                <summary>Click to expand</summary>
                Hidden content with **markdown** support.
            </details>
            
            - [Navigate to Home](/home)
            - [External Link](https://example.com)
            """;
            
        Action<string> handleLink = url =>
        {
            if (url.StartsWith("/"))
            {
                Console.WriteLine($"Navigating to: {url}");
            }
            else
            {
                Console.WriteLine($"Opening external link: {url}");
            }
        };
            
        return new Markdown(markdownContent)
            .OnLinkClick(handleLink);
    }
}

public class ComprehensiveMarkdownView : ViewBase
{
    public override object? Build()
    {
        var markdownContent = 
            """
            **Bold text** and *italic text* with `inline code`
            
            - Item 1
            - [x] Completed task
            - [ ] Pending task
            
            ```csharp
            public class Example
            {
                public void ShowFeatures() => Console.WriteLine("Markdown!");
            }
            ```
            
            Inline equation: $f(x) = x^2 + 2x + 1$
            
            | Language | Type       | Performance |
            |----------|------------|-------------|
            | C#       | Compiled   | High        |
            | Python   | Interpreted| Moderate    |
            
            ```mermaid
            graph LR
                A[Start] --> B[Process] --> C[End]
            ```
            
            Made with :heart: using Ivy Framework :rocket:
            """;
            
        Action<string> handleLink = url => Console.WriteLine($"Navigate to: {url}");
            
        return new Markdown(markdownContent)
            .OnLinkClick(handleLink);
    }
}
