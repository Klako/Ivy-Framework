using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted.IvyMCPServer;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/07_IvyMCPServer/04_VSCode.md")]
public class VSCodeApp(bool onlyBody = false) : ViewBase
{
    public VSCodeApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("getting-started-vs-code", "Getting Started: VS Code", 1), new ArticleHeading("setup", "Setup", 2), new ArticleHeading("manual-configuration", "Manual Configuration", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Getting Started: VS Code").OnLinkClick(onLinkClick)
            | Lead("The Ivy MCP Server enables AI assistants to directly interact with the Ivy Framework, providing them with the capability to read documentation, query widget properties, and build complex Ivy applications. By connecting your AI tools to the Ivy MCP Server, you can unlock powerful agentic coding workflows tailored for the Ivy ecosystem.")
            | new Markdown(
                """"
                For more information on configuring MCP servers in VS Code, please refer to their [official documentation](https://code.visualstudio.com/docs/copilot/customization/mcp-servers).
                
                To use the Ivy MCP Server, you first need to install Ivy. Refer to the [installation guide](app://onboarding/getting-started/installation) to learn how.
                
                ## Setup
                
                1. Open your project directory in VS Code.
                2. Initialise the Ivy project:
                   ```terminal
                   ivy init
                   ```
                3. Generate the MCP configuration:
                
                   ```terminal
                   ivy mcp config
                   ```
                
                4. In the chat, prompt `@AGENTS.md` to sync the project context and agent instructions.
                
                ### Manual Configuration
                
                If you prefer to configure the MCP server manually, use the following configuration:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                {
                  "mcpServers": {
                    "ivy-release": {
                      "command": "<path-to-dotnet-tools>/ivy",
                      "args": [
                        "mcp",
                        "--path",
                        "<path-to-your-project>"
                      ],
                      "env": {
                        "Ivy__Mcp__ApiUrl": "https://mcp.ivy.app"
                      }
                    }
                  }
                }
                """",Languages.Json)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.GettingStarted.InstallationApp)]; 
        return article;
    }
}

