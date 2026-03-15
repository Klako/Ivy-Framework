using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:14, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/14_Terminal.md", searchHints: ["terminal", "command", "console", "cli", "output", "shell"])]
public class TerminalApp(bool onlyBody = false) : ViewBase
{
    public TerminalApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("terminal", "Terminal", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("styling", "Styling", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Terminal").OnLinkClick(onLinkClick)
            | Lead("Display terminal-style output with commands and responses in a visually distinct console format with copy functionality.")
            | new Markdown(
                """"
                The `Terminal` widget renders a terminal-like interface ideal for displaying CLI commands, code snippets, or command outputs. It includes a header with title and a copy button for easy command copying.
                
                ## Basic Usage
                
                Here's a simple example of a terminal displaying installation commands:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    Layout.Vertical()
                        | new Terminal()
                            .Title("Getting Started")
                            .AddCommand("dotnet new install Ivy.Templates")
                            .AddOutput("Template 'Ivy Application' installed successfully.")
                    """",Languages.Csharp)
                | new Box().Content(Layout.Vertical()
    | new Terminal()
        .Title("Getting Started")
        .AddCommand("dotnet new install Ivy.Templates")
        .AddOutput("Template 'Ivy Application' installed successfully."))
            )
            | new Markdown(
                """"
                ## Styling
                
                You can customize the terminal appearance and behavior:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.P("With Title").Large()
    | new Terminal()
        .Title("My Terminal")
        .AddCommand("echo Hello World")
        .AddOutput("Hello World")
    | Text.P("Without Header").Large()
    | new Terminal() { ShowHeader = false }
        .AddCommand("npm install")
        .AddOutput("added 125 packages")
    | Text.P("Without Copy Button").Large()
    | new Terminal()
        .Title("Read Only")
        .ShowCopyButton(false)
        .AddCommand("git status")
        .AddOutput("nothing to commit, working tree clean"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.P("With Title").Large()
                        | new Terminal()
                            .Title("My Terminal")
                            .AddCommand("echo Hello World")
                            .AddOutput("Hello World")
                        | Text.P("Without Header").Large()
                        | new Terminal() { ShowHeader = false }
                            .AddCommand("npm install")
                            .AddOutput("added 125 packages")
                        | Text.P("Without Copy Button").Large()
                        | new Terminal()
                            .Title("Read Only")
                            .ShowCopyButton(false)
                            .AddCommand("git status")
                            .AddOutput("nothing to commit, working tree clean")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Terminal", "Ivy.TerminalExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Terminal.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Installation Guide",
                Vertical().Gap(4)
                | new Markdown("Display step-by-step installation instructions for your users:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(Layout.Vertical()
    | new Terminal()
        .Title("Install MyApp")
        .AddCommand("npm install myapp")
        .AddOutput("added 42 packages in 3.2s")
        .AddCommand("myapp init")
        .AddOutput("Configuration created at ./myapp.config.json")
        .AddCommand("myapp start")
        .AddOutput("Server running at http://localhost:3000"))),
                    new Tab("Code", new CodeBlock(
                        """"
                        Layout.Vertical()
                            | new Terminal()
                                .Title("Install MyApp")
                                .AddCommand("npm install myapp")
                                .AddOutput("added 42 packages in 3.2s")
                                .AddCommand("myapp init")
                                .AddOutput("Configuration created at ./myapp.config.json")
                                .AddCommand("myapp start")
                                .AddOutput("Server running at http://localhost:3000")
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        return article;
    }
}

