using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Deployment;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/06_Deployment/04_GCP.md", searchHints: ["deployment", "cloud", "production", "gcp", "docker"])]
public class GCPApp(bool onlyBody = false) : ViewBase
{
    public GCPApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("gcp-deployment", "GCP Deployment", 1), new ArticleHeading("basic-gcp-deployment", "Basic GCP Deployment", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# GCP Deployment").OnLinkClick(onLinkClick)
            | Lead("**GCP (Google Cloud Platform)** - Cloud computing services for building, testing, and deploying applications.")
            | new Markdown("##Setup Process").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy")
                .AddOutput("# Select GCP when prompted")
                
            | new Markdown(
                """"
                **Required Configuration** - GCP Project, Container Registry (GCR), Cloud Run Service, and Region.
                
                **GCP Services Used** - Google Container Registry, Cloud Run (serverless container platform), Cloud Build, and IAM.
                
                **GCP Setup Prerequisites** - Create a Google Cloud account, install Google Cloud CLI, login to GCP: `gcloud auth login`, and set your project: `gcloud config set project <project-id>`.
                
                ## Basic GCP Deployment
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy")
                .AddOutput("# Select GCP")
                .AddOutput("# Configure project and region")
                
            | new Markdown(
                """"
                For detailed information about GCP cloud provider:
                
                - **GCP**: [Google Cloud Documentation](https://cloud.google.com/docs/)
                """").OnLinkClick(onLinkClick)
            ;
        return article;
    }
}

