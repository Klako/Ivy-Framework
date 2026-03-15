using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted.IvyMCPServer;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/07_IvyMCPServer/02_Claude.md")]
public class ClaudeApp(bool onlyBody = false) : ViewBase
{
    public ClaudeApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("getting-started-claude", "Getting Started: Claude", 1), new ArticleHeading("setup", "Setup", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Getting Started: Claude").OnLinkClick(onLinkClick)
            | Lead("The Ivy MCP Server enables AI assistants to directly interact with the Ivy Framework, providing them with the capability to read documentation, query widget properties, and build complex Ivy applications. By connecting your AI tools to the Ivy MCP Server, you can unlock powerful agentic coding workflows tailored for the Ivy ecosystem.")
            | new Markdown(
                """"
                For more information on configuring MCP servers in Claude, please refer to their [official documentation](https://code.claude.com/docs/en/mcp).
                
                To use the Ivy MCP Server, you first need to install Ivy. Refer to the [installation guide](app://onboarding/getting-started/installation) to learn how.
                
                ## Setup
                
                The fastest way to get started is scaffolding the sample `--hello` project, which configures the IDE-specific MCP settings in one command.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("ivy init --hello --claude")
                
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.GettingStarted.InstallationApp)]; 
        return article;
    }
}

