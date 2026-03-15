using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI;

[App(order:10, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/10_Docs.md", searchHints: ["docs", "documentation", "reference", "content", "list docs"])]
public class DocsApp(bool onlyBody = false) : ViewBase
{
    public DocsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivy-docs", "ivy docs", 1), new ArticleHeading("commands", "Commands", 2), new ArticleHeading("ivy-docs-list", "ivy docs list", 3), new ArticleHeading("list-usage", "list Usage", 4), new ArticleHeading("ivy-docs-path", "ivy docs [path]", 3), new ArticleHeading("path-usage", "path Usage", 4), new ArticleHeading("arguments", "Arguments", 4), new ArticleHeading("example", "Example", 4), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# ivy docs").OnLinkClick(onLinkClick)
            | Lead("Access and retrieve Ivy Framework documentation directly from your terminal.")
            | new Markdown(
                """"
                The `ivy docs` command set provides built-in tools for exploring the comprehensive framework knowledge base. You can either list all available documentation topics or fetch the raw Markdown content for a specific page.
                
                ## Commands
                
                ### ivy docs list
                
                Lists all available documentation paths natively registered inside the Ivy framework for subsequent manual or automated investigation.
                
                The command outputs a structured YAML representation of all discoverable document titles and relative paths. Use this list to find valid `<path>` arguments for the `ivy docs <path>` sibling command.
                
                #### list Usage
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy docs list")
                
            | new Markdown(
                """"
                ---
                
                ### ivy docs [path]
                
                Retrieves the raw Markdown payload of a specific framework documentation page.
                
                This command resolves and standardizes versioning logically, ensuring you always retrieve documentation relevant to the specific framework instantiation you have targeted.
                
                #### path Usage
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy docs [path]")
                
            | new Markdown(
                """"
                #### Arguments
                
                - `<path>`: The relative path or URL slug corresponding to the desired markdown file. You can discover valid paths via the `ivy docs list` command.
                
                #### Example
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy docs \"docs/ApiReference/IvyShared/Colors.md\"")
                
            ;
        return article;
    }
}

