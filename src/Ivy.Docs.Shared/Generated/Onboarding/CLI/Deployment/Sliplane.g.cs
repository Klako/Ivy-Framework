using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Deployment;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/06_Deployment/05_Sliplane.md", searchHints: ["deployment", "cloud", "production", "sliplane", "docker"])]
public class SliplaneApp(bool onlyBody = false) : ViewBase
{
    public SliplaneApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("sliplane-deployment", "Sliplane Deployment", 1), new ArticleHeading("setup-process", "Setup Process", 2), new ArticleHeading("basic-sliplane-deployment", "Basic Sliplane Deployment", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Sliplane Deployment").OnLinkClick(onLinkClick)
            | Lead("**Sliplane** - Modern container deployment platform with automated infrastructure and simplified deployment workflow.")
            | new Markdown("## Setup Process").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy")
                .AddOutput("# Select Sliplane when prompted")
                
            | new Markdown(
                """"
                **Required Configuration** - Sliplane API Key, Server (optional, will be created if not specified), and Port Configuration (defaults to port 80).
                
                **Sliplane Services Used** - Container hosting and deployment, automated SSL/TLS certificates, load balancing and traffic routing, and automated health checks and monitoring.
                
                **Sliplane Setup Prerequisites** - Create a Sliplane account, generate an API key from your Sliplane dashboard, and optionally create a server in your Sliplane dashboard (or let Ivy create one automatically).
                
                ## Basic Sliplane Deployment
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy")
                .AddOutput("# Select Sliplane")
                .AddOutput("# Configure server and deployment settings")
                
            | new Markdown(
                """"
                For detailed information about Sliplane cloud provider:
                
                - **Sliplane**: [Sliplane Documentation](https://docs.sliplane.io/)
                """").OnLinkClick(onLinkClick)
            ;
        return article;
    }
}

