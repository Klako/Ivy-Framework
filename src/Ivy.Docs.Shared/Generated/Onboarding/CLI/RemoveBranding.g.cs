using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI;

[App(order:7, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/07_RemoveBranding.md", searchHints: ["branding", "license", "subscription", "white-label", "pro", "customization"])]
public class RemoveBrandingApp(bool onlyBody = false) : ViewBase
{
    public RemoveBrandingApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("remove-branding", "Remove Branding", 1), new ArticleHeading("prerequisites", "Prerequisites", 2), new ArticleHeading("usage", "Usage", 2), new ArticleHeading("command-options", "Command Options", 3), new ArticleHeading("deployment-configuration", "Deployment Configuration", 2), new ArticleHeading("cloud-deployment", "Cloud Deployment", 3), new ArticleHeading("manual-deployment", "Manual Deployment", 3), new ArticleHeading("verification", "Verification", 2), new ArticleHeading("check-user-secrets", "Check User Secrets", 3), new ArticleHeading("local-testing", "Local Testing", 3), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("license-token-not-working", "License Token Not Working", 3), new ArticleHeading("configuration-not-found", "Configuration Not Found", 3), new ArticleHeading("deployment-issues", "Deployment Issues", 3), new ArticleHeading("related-commands", "Related Commands", 2), new ArticleHeading("subscription-management", "Subscription Management", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Remove Branding").OnLinkClick(onLinkClick)
            | Lead("Remove Ivy branding from your deployed projects with a valid Ivy Pro or Team subscription.")
            | new Markdown(
                """"
                The `ivy remove-branding` command allows users with a paid subscription to remove Ivy branding from their projects. This command configures the necessary license token in your project's [.NET user secrets](app://onboarding/concepts/secrets), which must then be included in your deployment environment.
                
                ## Prerequisites
                
                Before using the remove-branding command, ensure you have:
                
                1. **Paid Subscription** - A valid Ivy Pro or Team subscription
                2. **Authentication** - Must be logged in with `ivy login`
                3. **Ivy Project** - Must be run in a valid Ivy project directory
                
                ## Usage
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy remove-branding")
                
            | new Markdown(
                """"
                This command will:
                
                - Verify you have a valid Ivy Pro or Team subscription
                - Retrieve your license token from the Ivy billing service
                - Set the `Ivy:License` configuration value in .NET user secrets
                - Add the license configuration to your project's secrets manager
                
                ### Command Options
                
                `--project-path <PATH>` or `-p <PATH>` - Specify the path to your project directory. Defaults to the current working directory.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy remove-branding --project-path /path/to/your/project")
                
            | new Markdown("`--verbose` - Enable verbose output for detailed logging during the process.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy remove-branding --verbose")
                
            | new Markdown("## Deployment Configuration").OnLinkClick(onLinkClick)
            | new Callout("The `Ivy:License` configuration value must be included in your deployment environment for branding to be removed. The command only sets up local development secrets.", icon:Icons.CircleAlert).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Cloud Deployment
                
                When using [ivy deploy](app://onboarding/cli/deployment/deployment-overview) to simplify deployment, the license configuration will be automatically included in your deployment if it has been configured in your [.NET user secrets](app://onboarding/concepts/secrets).
                
                ### Manual Deployment
                
                When deploying an Ivy project without using [ivy deploy](app://onboarding/cli/deployment/deployment-overview), your local [.NET user secrets](app://onboarding/concepts/secrets) are not automatically transferred. In that case, you can configure your Ivy license by setting the environment variable or .NET user secret below.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                # Environment variable
                Ivy__License=your-unique-license-token
                
                # User secret
                Ivy:License=your-unique-license-token
                """",Languages.Text)
            | new Markdown(
                """"
                > **Note:** If configuration is present in both .NET user secrets and environment variables, Ivy will use the values in **[.NET user secrets](app://onboarding/concepts/secrets) over environment variables**.
                
                To retrieve your license token, run `ivy remove-branding` locally, then look for `Ivy:License` in your user secrets. See [Check User Secrets](#check-user-secrets) below for more information.
                
                ## Verification
                
                After running the command successfully, verify the configuration:
                
                ### Check User Secrets
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet user-secrets list")
                
            | new Markdown("You should see:").OnLinkClick(onLinkClick)
            | new CodeBlock("Ivy:License = your-unique-license-token",Languages.Text)
            | new Markdown(
                """"
                ### Local Testing
                
                Run your project locally to verify branding has been removed:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run")
                
            | new Markdown(
                """"
                ## Troubleshooting
                
                ### License Token Not Working
                
                - Ensure the `Ivy:License` configuration is properly set in your deployment environment
                - Verify your subscription is still active
                - Re-run `ivy remove-branding` to refresh the license token
                
                ### Configuration Not Found
                
                - Check that .NET user secrets are properly initialized
                - Use `--verbose` flag for detailed logging
                
                ### Deployment Issues
                
                - Confirm license environment variable or secret is correctly set in your deployment platform
                - Verify the license token is not being truncated or modified
                
                ## Related Commands
                
                - `ivy login` - Authenticate with your Ivy account
                - `ivy deploy` - Deploy your project (includes license configuration)
                - `ivy init` - Initialize a new Ivy project
                
                ## Subscription Management
                
                To manage your Ivy subscription and check available features:
                
                - Visit [https://ivy.app/pricing](https://ivy.app/pricing) to view plans
                - Check your current subscription status in your Ivy account
                - Contact support for subscription-related questions
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.Deployment.DeploymentOverviewApp)]; 
        return article;
    }
}

