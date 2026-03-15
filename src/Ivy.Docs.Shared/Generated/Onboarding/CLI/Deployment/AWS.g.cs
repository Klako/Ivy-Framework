using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.Deployment;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/06_Deployment/02_AWS.md", searchHints: ["deployment", "cloud", "production", "aws", "docker"])]
public class AWSApp(bool onlyBody = false) : ViewBase
{
    public AWSApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("aws-deployment", "AWS Deployment", 1), new ArticleHeading("setup-process", "Setup Process", 2), new ArticleHeading("basic-aws-deployment", "Basic AWS Deployment", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# AWS Deployment").OnLinkClick(onLinkClick)
            | Lead("**AWS (Amazon Web Services)** - Comprehensive cloud platform with various services for application deployment.")
            | new Markdown("## Setup Process").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy")
                .AddOutput("# Select AWS when prompted")
                
            | new Markdown(
                """"
                **Required Configuration** - AWS Credentials (access key and secret key), Region, ECR Repository, and App Runner Service.
                
                **AWS Services Used** - Amazon ECR (container registry), AWS App Runner (serverless container service), Amazon S3 (storage for build artifacts), and AWS IAM (identity and access management).
                
                **AWS Setup Prerequisites** - Create an AWS account, install and configure AWS CLI, create an IAM user with appropriate permissions, and configure AWS credentials: `aws configure`.
                
                ## Basic AWS Deployment
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy deploy")
                .AddOutput("# Select AWS")
                .AddOutput("# Follow prompts for configuration")
                
            | new Markdown(
                """"
                For detailed information about AWS cloud provider:
                
                - **AWS**: [AWS Documentation](https://docs.aws.amazon.com/)
                """").OnLinkClick(onLinkClick)
            ;
        return article;
    }
}

