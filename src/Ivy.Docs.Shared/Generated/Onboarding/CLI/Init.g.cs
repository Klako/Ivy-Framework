using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/02_Init.md", searchHints: ["init", "create", "scaffold", "new-project", "setup", "initialize"])]
public class InitApp(bool onlyBody = false) : ViewBase
{
    public InitApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivy-init---getting-started", "Ivy Init - Getting Started", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("command-options", "Command Options", 3), new ArticleHeading("interactive-mode", "Interactive Mode", 3), new ArticleHeading("project-structure", "Project Structure", 3), new ArticleHeading("generated-files", "Generated Files", 3), new ArticleHeading("programcs", "Program.cs", 3), new ArticleHeading("prerequisites", "Prerequisites", 3), new ArticleHeading("validation", "Validation", 3), new ArticleHeading("error-handling", "Error Handling", 3), new ArticleHeading("next-steps", "Next Steps", 3), new ArticleHeading("examples", "Examples", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 3), new ArticleHeading("creating-apps", "Creating Apps", 3), new ArticleHeading("removing-apps", "Removing Apps", 3), new ArticleHeading("related-commands", "Related Commands", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Ivy Init - Getting Started").OnLinkClick(onLinkClick)
            | Lead("Quickly scaffold new Ivy projects with the necessary structure, configuration files, and boilerplate code using the init command.")
            | new Markdown(
                """"
                The `ivy init` command creates a new Ivy project with the necessary structure and configuration files to get you started quickly. See [Program](app://onboarding/concepts/program) for how the generated entry point runs your app.
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init")
                
            | new Markdown(
                """"
                This command will:
                
                - Create a new Ivy project in the current directory
                - Set up the basic project structure
                - Generate necessary configuration files
                
                ### Command Options
                
                `--namespace <NAMESPACE>` or `-n <NAMESPACE>` - Specify the namespace for your Ivy project. If not provided, Ivy will suggest a namespace based on the folder name.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --namespace MyCompany.MyProject")
                
            | new Markdown("`--dangerous-clear` - Clear the current folder before creating the new project. **Use with caution!**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --dangerous-clear")
                
            | new Markdown("`--dangerous-overwrite` - Overwrite existing files in the current folder. **Use with caution!**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --dangerous-overwrite")
                
            | new Markdown("`--verbose` - Enable verbose output for detailed logging during initialization.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --verbose")
                
            | new Markdown("`--hello` - Include a simple demo app in the new project to help you get started.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --hello")
                
            | new Markdown("`--script` - Create a simple Ivy script file instead of a full project. Perfect for quick prototyping or single-file applications.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --script")
                
            | new Markdown("`--template <TEMPLATE>` or `-t <TEMPLATE>` - Use a specific template for the new project.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --template my-template")
                
            | new Markdown("`--select-template` - Interactively select a template from available options.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --select-template")
                
            | new Markdown("`--cursor` - Install Cursor MCP integration after project creation.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --cursor")
                
            | new Markdown("`--claude` - Install Claude Code MCP integration after project creation.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --claude")
                
            | new Markdown("`--ignore-git` - Skip Git checks and commit during initialization.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --ignore-git")
                
            | new Markdown("`--prerelease` - Include prerelease versions when fetching the latest Ivy version.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --prerelease")
                
            | new Markdown("`--yes-to-all` - Skip all prompts and use default values. Useful for automated scripts.").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --yes-to-all")
                
            | new Markdown(
                """"
                ### Interactive Mode
                
                When you run `ivy init` without specifying a namespace, Ivy will prompt you to enter one:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("Namespace for the new Ivy project: [suggested-namespace]")
                
            | new Markdown(
                """"
                Ivy will suggest a namespace based on your current folder name. You can accept the suggestion or enter a custom namespace.
                
                ### Project Structure
                
                After running `ivy init`, your project will have the following structure. The generated [Program.cs](app://onboarding/concepts/program) is the application entry point.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                YourProject/
                ├── Program.cs              # Main project entry point
                ├── YourProject.csproj      # .NET project file
                ├── README.md               # Project documentation
                └── .gitignore              # Git ignore file
                """",Languages.Text)
            | new Markdown(
                """"
                ### Generated Files
                
                ### Program.cs
                
                The main [entry point](app://onboarding/concepts/program) for your Ivy project:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.UseCulture("en-US");
                #if !DEBUG
                server.UseHttpRedirection();
                #endif
                #if DEBUG
                server.UseHotReload();
                #endif
                server.AddAppsFromAssembly();
                server.AddConnectionsFromAssembly();
                server.UseHotReload();
                var chromeSettings = new ChromeSettings().UseTabs(preventDuplicates: true);
                server.UseChrome(chromeSettings);
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                The template configures [Chrome](app://onboarding/concepts/chrome) for the browser UI.
                
                ### Prerequisites
                
                Before running `ivy init`, ensure you have:
                
                1. **.NET SDK** installed (version 8.0 or later)
                2. **Git** installed (optional, but recommended)
                3. **Empty directory** or use `--dangerous-clear`/`--dangerous-overwrite`
                
                ### Validation
                
                Ivy performs several validations during initialization:
                
                - **Directory Check**: Ensures the target directory is empty (unless using overwrite options)
                - **Namespace Validation**: Validates the provided namespace format
                - **Git Status**: Checks for uncommitted changes if Git is initialized
                - **.NET Tools**: Ensures required .NET tools are installed
                
                ### Error Handling
                
                **Empty Directory Required** - If the current directory is not empty, Ivy will show an error:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("The current folder is not empty. Please clear the folder or use the --dangerous-clear option or --dangerous-overwrite")
                
            | new Markdown("**Invalid Namespace** - If you provide an invalid namespace, Ivy will prompt you to enter a valid one:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("Invalid 'invalid-namespace' namespace. Please enter a valid namespace.")
                
            | new Markdown(
                """"
                ### Next Steps
                
                After initializing your project:
                
                1. **Add a database connection**: `ivy db add` — see [Database Overview](app://onboarding/cli/database-integration/database-overview)
                2. **Add authentication**: `ivy auth add` — see [Authentication Overview](app://onboarding/cli/authentication/authentication-overview)
                3. **Create an app**: `ivy app create` — see [Apps](app://onboarding/concepts/apps)
                4. **Deploy your project**: `ivy deploy` — see [Deployment Overview](app://onboarding/cli/deployment/deployment-overview)
                
                ## Examples
                
                **Basic Project Initialization**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("mkdir MyIvyProject")
                .AddCommand("cd MyIvyProject")
                .AddCommand("ivy init")
                
            | new Markdown("**Project with Custom Namespace**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --namespace AcmeCorp.InventorySystem")
                
            | new Markdown("**Project with Demo App**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --helloworld --namespace MyDemoProject")
                
            | new Markdown("**Verbose Initialization**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --verbose --namespace MyProject")
                
            | new Callout("The CLI plays a success sound when operations complete. Use `--silent` to disable audio notifications.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Troubleshooting
                
                **Permission Issues** - If you encounter permission issues, ensure you have write access to the current directory.
                
                **NET Tools Not Found** - If required .NET tools are missing, Ivy will attempt to install them automatically. You may need to run:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet tool install -g dotnet-ef")
                .AddCommand("dotnet tool install -g dotnet-user-secrets")
                
            | new Markdown(
                """"
                **Git Issues** - If Git is not installed or configured, Ivy will still create the project but may skip some Git-related operations.
                
                **Build Errors** - If you encounter build errors, you can use the `ivy fix` command to automatically resolve common issues. See also [Program](app://onboarding/concepts/program) for startup and configuration. The default timeout is 360 seconds (6 minutes).
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy fix")
                
            | new Markdown("Use the `--timeout` option to specify a custom timeout in seconds:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy fix --timeout 600")
                
            | new Markdown("Use **Claude Code** for debugging:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy fix --use-claude-code")
                
            | new Markdown("**Set environment** variable for Claude Code").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("export IVY_FIX_USE_CLAUDE_CODE=true")
                .AddCommand("ivy fix")
                
            | new Markdown("**Enable telemetry upload**").OnLinkClick(onLinkClick)
            | new Callout("Telemetry Upload Details: When telemetry upload is enabled, the `ivy fix` command will upload an anonymized snapshot of your project (excluding .git, bin, and obj folders) for analysis. This helps the Ivy team understand common build issues and improve the fix command's effectiveness. The telemetry upload only includes source code files, has a 50MB size limit, and is completely optional and disabled by default.", icon:Icons.CircleAlert).OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("export IVY_FIX_UPLOAD_TELEMETRY=true")
                .AddCommand("ivy fix")
                
            | new Markdown("**Debug commands** to manage settings").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy debug enable-ivy-fix-upload-telemetry")
                .AddCommand("ivy debug disable-ivy-fix-upload-telemetry")
                
            | new Markdown(
                """"
                ### Creating Apps
                
                Create new [apps](app://onboarding/concepts/apps) using AI assistance. The default timeout is 360 seconds (6 minutes).
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy app create MyApp")
                
            | new Markdown("Use the `--timeout` option to specify a custom timeout in seconds:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy app create MyApp --timeout 600")
                
            | new Markdown(
                """"
                ### Removing Apps
                
                **Remove a specific app by name**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy app remove --name MyApp")
                
            | new Markdown("**Interactive mode** - select from a list of existing apps").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy app remove")
                
            | new Markdown("**Remove all** apps at once").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy app remove --all")
                
            | new Markdown(
                """"
                ### Related Commands
                
                - `ivy db add` - Add database connections
                - `ivy auth add` - Add authentication providers
                - `ivy app create` - Create apps
                - `ivy app remove` - Remove apps
                - `ivy deploy` - Deploy your project
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.Concepts.ChromeApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.Authentication.AuthenticationOverviewApp), typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.CLI.Deployment.DeploymentOverviewApp)]; 
        return article;
    }
}

