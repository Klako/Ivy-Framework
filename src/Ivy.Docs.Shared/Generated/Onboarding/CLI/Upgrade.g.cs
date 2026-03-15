using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI;

[App(order:8, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/08_Upgrade.md", searchHints: ["upgrade", "update", "version", "migrate", "latest"])]
public class UpgradeApp(bool onlyBody = false) : ViewBase
{
    public UpgradeApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivy-upgrade", "Ivy Upgrade", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("command-options", "Command Options", 2), new ArticleHeading("what-to-expect", "What to Expect", 2), new ArticleHeading("prerequisites", "Prerequisites", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("related-commands", "Related Commands", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Ivy Upgrade").OnLinkClick(onLinkClick)
            | Lead("Upgrade your existing Ivy project to the latest version of the Ivy framework with a single command.")
            | new Markdown(
                """"
                The `ivy upgrade` command updates your Ivy project to the latest available version. It modifies your project file (`.csproj`) to reference the newest Ivy packages, ensuring you have access to the latest features, bug fixes, and improvements.
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy upgrade")
                
            | new Markdown(
                """"
                This command will:
                
                - Detect the current Ivy version in your project
                - Fetch the latest available Ivy version
                - Update all Ivy package references in your `.csproj` file
                - Optionally commit the changes to Git
                
                ## Command Options
                
                `--verbose` or `-v` - Enable verbose logging for detailed output during the upgrade process.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy upgrade --verbose")
                
            | new Markdown("`--ignore-git` - Skip Git checks and commit. By default, Ivy verifies your Git status and commits the upgrade changes automatically.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy upgrade --ignore-git")
                
            | new Markdown(
                """"
                ## What to Expect
                
                When you run the command, Ivy will update the package references in your project file. A successful upgrade looks like this:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy upgrade")
                .AddOutput("Upgrading Ivy from 1.2.14 to 1.2.15...")
                .AddOutput("Updated package references in YourProject.csproj")
                .AddOutput("Ivy upgrade complete!")
                
            | new Callout("After upgrading, run `ivy run` to verify that your project builds and runs correctly with the new version.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Prerequisites
                
                Before running `ivy upgrade`, ensure you have:
                
                1. **Ivy Project** - Must be run in a valid Ivy project directory (created with `ivy init`)
                2. **Git** - A clean Git working tree (unless using `--ignore-git`)
                
                ## Troubleshooting
                
                **Build Errors After Upgrade** - If you encounter build errors after upgrading, check the [release notes](https://github.com/Ivy-Interactive/Ivy-Framework/releases) for any breaking changes. You can also use the `ivy fix` command to automatically resolve common issues.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy fix")
                
            | new Markdown("**Git Errors** - If Git checks fail, ensure you have committed or stashed all changes before upgrading. Alternatively, use `--ignore-git` to skip Git checks.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy upgrade --ignore-git")
                
            | new Markdown(
                """"
                ## Related Commands
                
                | Command | Description |
                | :--- | :--- |
                | `ivy init` | [Create a new Ivy project](app://onboarding/cli/init) |
                | `ivy run` | [Run your application locally](app://onboarding/cli/run) |
                | `ivy fix` | Fix build and runtime issues |
                | `ivy deploy` | [Deploy your application](app://onboarding/cli/deployment/deployment-overview) |
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.CLI.InitApp), typeof(Onboarding.CLI.RunApp), typeof(Onboarding.CLI.Deployment.DeploymentOverviewApp)]; 
        return article;
    }
}

