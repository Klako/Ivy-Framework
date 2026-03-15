using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted.IvyMCPServer;

[App(order:7, title:"Ivy MCP Server", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/07_IvyMCPServer/_Index.md", searchHints: ["mcp", "server", "ai-agent", "claude", "cursor", "antigravity", "vscode", "windsurf"])]
public class _IndexApp(bool onlyBody = false) : ViewBase
{
    public _IndexApp() : this(false)
    {
    }
    public override object? Build()
    {
        return null;
    }
}

