using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/01_TextBlock.md", searchHints: ["typography", "heading", "paragraph", "label", "text", "content", "alignment", "justify", "center", "left", "right"])]
public class TextBlockApp(bool onlyBody = false) : ViewBase
{
    public TextBlockApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("textblock", "TextBlock", 1), new ArticleHeading("basic-text-variants", "Basic Text Variants", 2), new ArticleHeading("code-and-markup-variants", "Code and Markup Variants", 2), new ArticleHeading("text-modifiers", "Text Modifiers", 2), new ArticleHeading("text-alignment", "Text Alignment", 2), new ArticleHeading("practical-examples", "Practical Examples", 2), new ArticleHeading("article-layout", "Article Layout", 3), new ArticleHeading("form-labels-and-messages", "Form Labels and Messages", 3), new ArticleHeading("code-documentation", "Code Documentation", 3), new ArticleHeading("status-messages", "Status Messages", 3), new ArticleHeading("textbuilder-modifiers", "TextBuilder Modifiers", 2), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("related-components", "Related Components", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # TextBlock
                
                The `TextBlock` [widget](app://onboarding/concepts/widgets) displays text content with customizable styling. It's a fundamental building block for creating [user interfaces](app://onboarding/concepts/views) with text, supporting various formatting options and layout properties.
                
                This widget is rarely used directly. Instead, we use the helper class `Text` which provides a more user-friendly API for creating text elements.
                
                ## Basic Text Variants
                
                The Text helper provides various methods for different text styles and purposes:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class TextVariantsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.Literal("Literal text")
                                | Text.H1("Heading 1")
                                | Text.H2("Heading 2")
                                | Text.H3("Heading 3")
                                | Text.H4("Heading 4")
                                | Text.P("Paragraph text")
                                | Text.Block("Block text")
                                | Text.Blockquote("Blockquote text")
                                | Text.Monospaced("Monospaced")
                                | Text.Lead("Lead text for prominent display")
                                | Text.P("Large text").Large()
                                | Text.P("Small text").Small()
                                | Text.Label("Label text")
                                | Text.Strong("Strong/bold text")
                                | Text.Muted("Muted text")
                                | Text.Danger("Danger text")
                                | Text.Warning("Warning text")
                                | Text.Success("Success text");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new TextVariantsDemo())
            )
            | new Markdown(
                """"
                ## Code and Markup Variants
                
                For displaying code and markup content:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CodeVariantsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CodeVariantsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.Code("public class Example { }", Languages.Csharp)
                                | Text.Json("{ \"name\": \"value\", \"number\": 42 }")
                                | Text.Xml("<root><item>value</item></root>")
                                | Text.Html("<div class='example'>HTML content</div>")
                                | Text.Markdown("# Markdown\n**Bold** and *italic* text")
                                | Text.Latex("\\frac{a}{b} = \\frac{c}{d}");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Text Modifiers
                
                Text elements can be customized with various modifiers. Use [Colors](app://api-reference/ivy/colors) for the `Color()` modifier and [Size](app://api-reference/ivy/size) for `Width()`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TextModifiersDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TextModifiersDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.P("Normal paragraph text")
                                | Text.P("Colored text").Color(Colors.Primary)
                                | Text.P("Amber colored text").Color(Colors.Amber)
                                | Text.P("Bold text").Bold()
                                | Text.P("Italic text").Italic()
                                | Text.P("Muted text").Muted()
                                | Text.P("Bold and italic text").Bold().Italic()
                                | Text.P("Strikethrough text").StrikeThrough()
                                | Text.P("No wrap text that should not wrap to multiple lines").NoWrap()
                                | Text.P("Text with custom width").Width(Size.Units(200))
                                | Text.P("Text with overflow clip").Overflow(Overflow.Clip).Width(Size.Units(100))
                                | Text.P("Text with overflow ellipsis").Overflow(Overflow.Ellipsis).Width(Size.Units(100));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Text Alignment
                
                Both the Text and [Markdown](app://widgets/primitives/markdown) widgets support text alignment with fluent methods that control how content is aligned within its container. You can align text left (default), center, right, or justify.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TextAlignmentDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TextAlignmentDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.P("Left-aligned paragraph (default).").Left()
                                | Text.P("Centered title or callout").Center()
                                | Text.P("Right-aligned numbers or dates").Right()
                                | Text.P("Justified text that stretches to fill the full width of its container.").Justify();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Practical Examples
                
                ### Article Layout
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ArticleDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ArticleDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.H1("Getting Started with Ivy")
                                | Text.Lead("Ivy is a powerful framework for building interactive web applications with C#.")
                                | Text.P("This guide will walk you through the basics of creating your first Ivy project. You'll learn about widgets, layouts, and how to structure your code effectively.")
                                | Text.H2("Prerequisites")
                                | Text.P("Before you begin, make sure you have:")
                                | Text.Block("• .NET 8.0 SDK installed")
                                | Text.Block("• A code editor (Visual Studio, VS Code, or Rider)")
                                | Text.Block("• Basic knowledge of C#")
                                | Text.H2("Installation")
                                | Text.P("Install Ivy using the .NET CLI:")
                                | Text.Monospaced("dotnet tool install -g Ivy.Console")
                                | Text.P("Create a new project:")
                                | Text.Monospaced("ivy init --namespace MyFirstProject");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Form Labels and Messages").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FormDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.Label("Email Address")
                                | Text.P("Enter your email address below")
                                | Text.P("We'll never share your email with anyone else.").Small()
                                | Layout.Horizontal()
                                    | Text.Success("✓ Email sent successfully!")
                                    | Text.Warning("⚠ Please check your spam folder")
                                    | Text.Danger("✗ Invalid email format");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Code Documentation").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CodeDocDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CodeDocDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.H3("TextHelper Class")
                                | Text.P("The TextHelper class provides convenient methods for creating text elements:")
                                | Text.Code("""
                                    public static TextBuilder H1(string content)
                                    {
                                        return new TextBuilder(content, TextVariant.H1);
                                    }
                                    """, Languages.Csharp)
                                | Text.Blockquote("Note: All Text helper methods return a TextBuilder that supports method chaining for modifiers.")
                                | Text.P("Common modifiers include:")
                                | Text.Block("• Color() - Set text color")
                                | Text.Block("• Width() - Set text width")
                                | Text.Block("• StrikeThrough() - Add strikethrough styling")
                                | Text.Block("• NoWrap() - Prevent text wrapping");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Status Messages").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StatusDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StatusDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | Text.H3("System Status")
                                | Layout.Horizontal()
                                    | Text.Success("Database: Connected")
                                    | Text.Success("API: Online")
                                    | Text.Warning("Cache: Warming up...")
                                    | Text.Danger("Backup: Failed")
                                | Text.P("Last updated: 2 minutes ago").Small()
                                | Text.Muted("System monitoring is active");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## TextBuilder Modifiers
                
                The TextBuilder class provides several modifiers for customizing text appearance:
                
                | Modifier | Description | Example |
                |----------|-------------|---------|
                | `Bold()` | Apply bold styling | `Text.P("Bold text").Bold()` |
                | `Italic()` | Apply italic styling | `Text.P("Italic text").Italic()` |
                | `Muted()` | Apply muted/disabled styling | `Text.P("Muted text").Muted()` |
                | `Color()` | Set text [color](app://api-reference/ivy/colors) | `Text.P("Red text").Color(Colors.Destructive)` |
                | `Width()` | Set text width with [Size](app://api-reference/ivy/size) | `Text.P("Fixed width").Width(Size.Units(200))` |
                | `StrikeThrough()` | Add strikethrough | `Text.P("Crossed out").StrikeThrough()` |
                | `NoWrap()` | Prevent wrapping | `Text.P("Single line").NoWrap()` |
                | `Overflow()` | Handle overflow | `Text.P("Long text").Overflow(Overflow.Clip)` |
                | `Small()` | Apply small text size | `Text.P("Small text").Small()` |
                | `Medium()` | Apply medium text size (default) | `Text.P("Normal text").Medium()` |
                | `Large()` | Apply large text size | `Text.P("Large text").Large()` |
                | `Left()` | Align text left (default) | `Text.P("Left-aligned").Left()` |
                | `Center()` | Center text | `Text.P("Centered").Center()` |
                | `Right()` | Align text right | `Text.P("Right-aligned").Right()` |
                | `Justify()` | Justify text to fill width | `Text.P("Justified").Justify()` |
                
                ## Best Practices
                
                - **Use semantic variants**: Choose the appropriate text variant for your content (H1-H4 for headings, P for paragraphs, etc.)
                - **Consistent styling**: Use the same text variants throughout your project for consistency
                - **Accessibility**: Use proper heading hierarchy (H1 → H2 → H3) for screen readers
                - **Color usage**: Use color modifiers sparingly and ensure sufficient contrast
                - **Responsive design**: Use width modifiers carefully to maintain responsive layouts
                
                ## Related Components
                
                - **[Markdown](app://widgets/primitives/markdown)** - For rendering markdown content
                - **[CodeBlock](app://widgets/primitives/code-block)** - For syntax-highlighted code blocks
                - **[Json](app://widgets/primitives/json)** - For JSON data display
                - **[Html](app://widgets/primitives/html)** - For HTML content rendering
                
                ## Faq
                """").OnLinkClick(onLinkClick)
            | new Expandable("How do I change the font size of text?",
                Vertical().Gap(4)
                | new Markdown("Use the `.Large()`, `.Medium()`, or `.Small()` modifiers on any `TextBuilder`:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    Text.P("Large text").Large()
                    Text.P("Normal text").Medium()
                    Text.P("Small text").Small()
                    Text.P("Small muted text").Small().Muted()
                    """",Languages.Csharp)
                | new Markdown("These modifiers work with all text factory methods (`Text.P()`, `Text.H1()`, `Text.Block()`, `Text.Label()`, etc.). **Important:** There is no `.WithFontSize()` method or `FontSize` enum. **There is no `.Style()` method for arbitrary CSS styling.**").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(ApiReference.Ivy.ColorsApp), typeof(ApiReference.Ivy.SizeApp), typeof(Widgets.Primitives.MarkdownApp), typeof(Widgets.Primitives.CodeBlockApp), typeof(Widgets.Primitives.JsonApp), typeof(Widgets.Primitives.HtmlApp)]; 
        return article;
    }
}


public class TextVariantsDemo : ViewBase
{   
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.Literal("Literal text")
            | Text.H1("Heading 1")
            | Text.H2("Heading 2") 
            | Text.H3("Heading 3")
            | Text.H4("Heading 4")
            | Text.P("Paragraph text")
            | Text.Block("Block text")
            | Text.Blockquote("Blockquote text")
            | Text.Monospaced("Monospaced")
            | Text.Lead("Lead text for prominent display")
            | Text.P("Large text").Large()
            | Text.P("Small text").Small()
            | Text.Label("Label text")
            | Text.Strong("Strong/bold text")
            | Text.Muted("Muted text")
            | Text.Danger("Danger text")
            | Text.Warning("Warning text")
            | Text.Success("Success text");
    }
}

public class CodeVariantsDemo : ViewBase
{   
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.Code("public class Example { }", Languages.Csharp)
            | Text.Json("{ \"name\": \"value\", \"number\": 42 }")
            | Text.Xml("<root><item>value</item></root>")
            | Text.Html("<div class='example'>HTML content</div>")
            | Text.Markdown("# Markdown\n**Bold** and *italic* text")
            | Text.Latex("\\frac{a}{b} = \\frac{c}{d}");
    }
}

public class TextModifiersDemo : ViewBase
{   
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.P("Normal paragraph text")
            | Text.P("Colored text").Color(Colors.Primary)
            | Text.P("Amber colored text").Color(Colors.Amber)
            | Text.P("Bold text").Bold()
            | Text.P("Italic text").Italic()
            | Text.P("Muted text").Muted()
            | Text.P("Bold and italic text").Bold().Italic()
            | Text.P("Strikethrough text").StrikeThrough()
            | Text.P("No wrap text that should not wrap to multiple lines").NoWrap()
            | Text.P("Text with custom width").Width(Size.Units(200))
            | Text.P("Text with overflow clip").Overflow(Overflow.Clip).Width(Size.Units(100))
            | Text.P("Text with overflow ellipsis").Overflow(Overflow.Ellipsis).Width(Size.Units(100));
    }
}

public class TextAlignmentDemo : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.P("Left-aligned paragraph (default).").Left()
            | Text.P("Centered title or callout").Center()
            | Text.P("Right-aligned numbers or dates").Right()
            | Text.P("Justified text that stretches to fill the full width of its container.").Justify();
    }
}

public class ArticleDemo : ViewBase
{   
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.H1("Getting Started with Ivy")
            | Text.Lead("Ivy is a powerful framework for building interactive web applications with C#.")
            | Text.P("This guide will walk you through the basics of creating your first Ivy project. You'll learn about widgets, layouts, and how to structure your code effectively.")
            | Text.H2("Prerequisites")
            | Text.P("Before you begin, make sure you have:")
            | Text.Block("• .NET 8.0 SDK installed")
            | Text.Block("• A code editor (Visual Studio, VS Code, or Rider)")
            | Text.Block("• Basic knowledge of C#")
            | Text.H2("Installation")
            | Text.P("Install Ivy using the .NET CLI:")
            | Text.Monospaced("dotnet tool install -g Ivy.Console")
            | Text.P("Create a new project:")
            | Text.Monospaced("ivy init --namespace MyFirstProject");
    }
}

public class FormDemo : ViewBase
{   
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.Label("Email Address")
            | Text.P("Enter your email address below")
            | Text.P("We'll never share your email with anyone else.").Small()
            | Layout.Horizontal()
                | Text.Success("✓ Email sent successfully!")
                | Text.Warning("⚠ Please check your spam folder")
                | Text.Danger("✗ Invalid email format");
    }
}

public class CodeDocDemo : ViewBase
{   
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.H3("TextHelper Class")
            | Text.P("The TextHelper class provides convenient methods for creating text elements:")
            | Text.Code("""
                public static TextBuilder H1(string content)
                {
                    return new TextBuilder(content, TextVariant.H1);
                }
                """, Languages.Csharp)
            | Text.Blockquote("Note: All Text helper methods return a TextBuilder that supports method chaining for modifiers.")
            | Text.P("Common modifiers include:")
            | Text.Block("• Color() - Set text color")
            | Text.Block("• Width() - Set text width")
            | Text.Block("• StrikeThrough() - Add strikethrough styling")
            | Text.Block("• NoWrap() - Prevent text wrapping");
    }
}

public class StatusDemo : ViewBase
{   
    public override object? Build()
    {
        return Layout.Vertical()
            | Text.H3("System Status")
            | Layout.Horizontal()
                | Text.Success("Database: Connected")
                | Text.Success("API: Online")
                | Text.Warning("Cache: Warming up...")
                | Text.Danger("Backup: Failed")
            | Text.P("Last updated: 2 minutes ago").Small()
            | Text.Muted("System monitoring is active");
    }
}
