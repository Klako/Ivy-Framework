using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Deployment;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/06_Deployment/03_Azure.md", searchHints: ["deployment", "cloud", "production", "azure", "docker"])]
public class AzureApp(bool onlyBody = false) : ViewBase
{
    public AzureApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("azure-deployment", "Azure Deployment", 1), new ArticleHeading("setup-process", "Setup Process", 2), new ArticleHeading("basic-azure-deployment", "Basic Azure Deployment", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Azure Deployment").OnLinkClick(onLinkClick)
            | Lead("**Azure (Microsoft Azure)** - Cloud services for building, deploying, and managing applications.")
            | new Markdown("## Setup Process").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy")
                .AddOutput("# Select Azure when prompted")
                
            | new Markdown(
                """"
                **Required Configuration** - Azure Subscription, Resource Group, Container Registry (ACR), and Container Apps Environment.
                
                **Azure Services Used** - Azure Container Registry, Azure Container Apps (serverless container platform), Azure Resource Manager, and Azure Active Directory.
                
                **Azure Setup Prerequisites** - Create an Azure account, install Azure CLI, login to Azure: `az login`, and set your subscription: `az account set --subscription <subscription-id>`.
                
                ## Basic Azure Deployment
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy --verbose")
                .AddOutput("# Select Azure")
                .AddOutput("# Configure custom resource group and region")
                
            | new Markdown(
                """"
                For detailed information about Azure cloud provider:
                
                - **Azure**: [Azure Documentation](https://docs.microsoft.com/azure/)
                """").OnLinkClick(onLinkClick)
            ;
        return article;
    }
}

