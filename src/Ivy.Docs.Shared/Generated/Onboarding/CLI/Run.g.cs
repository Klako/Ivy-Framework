using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/03_Run.md", searchHints: ["run", "start", "serve", "dev", "watch", "hot-reload"])]
public class RunApp(bool onlyBody = false) : ViewBase
{
    public RunApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivy-run", "Ivy Run", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("options", "Options", 2), new ArticleHeading("configuring-the-port", "Configuring the Port", 2), new ArticleHeading("cli-flag", "CLI Flag", 3), new ArticleHeading("server-configuration-in-code", "Server Configuration in Code", 3), new ArticleHeading("environment-variable", "Environment Variable", 3), new ArticleHeading("conflict-resolution--debugging", "Conflict Resolution & Debugging", 2), new ArticleHeading("development-features", "Development Features", 2), new ArticleHeading("hot-reload--auto-recovery", "Hot Reload & Auto-Recovery", 3), new ArticleHeading("interactive-controls", "Interactive Controls", 3), new ArticleHeading("what-to-expect", "What to Expect", 2), new ArticleHeading("common-scenarios", "Common Scenarios", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("related-commands", "Related Commands", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Ivy Run").OnLinkClick(onLinkClick)
            | Lead("Run your Ivy application locally with hot reload and automatic rebuilds.")
            | new Markdown(
                """"
                The `ivy run` command is your primary development tool. It starts your project in a live environment that monitors your source code, automatically applying changes or rebuilding as needed. Under the hood, it leverages `dotnet watch` to ensure your development loop is fast and uninterrupted. See [Program](app://onboarding/concepts/program) for server and startup configuration.
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run")
                
            | new Markdown(
                """"
                By default, this starts your application on port **5010**.
                
                ## Options
                
                You can also run the command with various options to customize its behavior:
                
                | Option | Description | Example |
                | :--- | :--- | :--- |
                | `--port <PORT>` | Specify a custom port (default: 5010). | `ivy run --port 8080` |
                | `--browse` | Open default browser on start. | `ivy run --browse` |
                | `--app <NAME>` | Run a specific [app](app://onboarding/concepts/apps) in a multi-app project. | `ivy run --app Admin` |
                | `--describe` | Show application metadata without starting. | `ivy run --describe` |
                | `--verbose` | Enable detailed logging for debugging. | `ivy run --verbose` |
                | `--silent` | Start without the welcome banner. | `ivy run --silent` |
                
                ## Configuring the Port
                
                By default, Ivy starts on port **5010**. There are several ways to change it:
                
                ### CLI Flag
                
                The simplest approach—pass the `--port` flag:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run --port 5011")
                
            | new Markdown(
                """"
                ### Server Configuration in Code
                
                Set the port directly in `Program.cs` (or in a [file-based app](app://onboarding/concepts/file-based-apps)):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("var server = new Server(new ServerArgs { Port = 5011 });",Languages.Csharp)
            | new Markdown(
                """"
                This is the recommended approach when running with `dotnet run` or `dotnet watch` directly, since those commands do not support the `--port` flag.
                
                ### Environment Variable
                
                Set the `PORT` environment variable before starting the app:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("set PORT=5011")
                .AddCommand("dotnet run")
                
            | new Markdown(
                """"
                This works with any launch method (`ivy run`, `dotnet run`, file-based apps).
                
                See [Program](app://onboarding/concepts/program) for more details on server configuration.
                
                ## Conflict Resolution & Debugging
                
                | Flag | Description |
                | :--- | :--- |
                | `--i-kill-for-this-port` | Forcefully kills any process currently using the target port. |
                | `--find-available-port` | Automatically searches for the next free port if the target is taken. |
                | `--watch-verbose` | Enables verbose output specifically for the file watcher. |
                
                ## Development Features
                
                ### Hot Reload & Auto-Recovery
                
                Ivy enables **Hot Reload** by default. When you modify method logic, UI layouts, or properties, changes are injected instantly without losing application state (`🔥 Hot reload succeeded`).
                
                For structural changes—such as modifying constructors, changing inheritance, or adding NuGet packages—Ivy usually requires a full restart. It handles this automatically: detecting the change, rebuilding, and restarting the process.
                
                If you save code with build errors, `ivy run` pauses and waits. Simply fix the error and save again to resume; there is no need to stop and restart the command manually.
                
                ### Interactive Controls
                
                Control the running application directly from your terminal:
                
                - **Ctrl+R**: Force a manual restart.
                - **Ctrl+C**: Gracefully shut down.
                
                ## What to Expect
                
                When you run the command, you'll see status messages from the watcher. A successful startup looks like this:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("dotnet watch 🔥 Hot reload enabled.")
                .AddOutput("dotnet watch 💡 Press Ctrl+R to restart.")
                .AddOutput("dotnet watch 🔨 Building /path/to/Project.csproj ...")
                .AddOutput("dotnet watch 🔨 Build succeeded")
                .AddOutput("Ivy is running on http://localhost:5010")
                
            | new Markdown(
                """"
                ## Common Scenarios
                
                **Run on a different port and open browser:**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run --port 3000 --browse")
                
            | new Markdown("**Handle a port conflict by killing the old process:**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run --port 5010 --i-kill-for-this-port")
                
            | new Markdown("**Run a specific [app](app://onboarding/concepts/apps) (for multi-app solutions):**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run --app Dashboard")
                
            | new Markdown(
                """"
                ## Troubleshooting
                
                - **Port in use:** Use `--find-available-port` to let Ivy pick a free port, or `--i-kill-for-this-port` to claim the specific one you want.
                - **Hot Reload fails:** Some complex edits can't be hot-reloaded. Press **Ctrl+R** to force a fresh build.
                - **Build errors:** Read the terminal output for compiler errors. The watcher will resume automatically once the file is fixed.
                
                ## Related Commands
                
                | Command | Description |
                | :--- | :--- |
                | `ivy init` | [Create a new Ivy project](app://onboarding/cli/init) |
                | `ivy app create` | Create new apps |
                | `ivy fix` | Fix build and runtime issues |
                | `ivy deploy` | [Deploy your application](app://onboarding/cli/deployment/deployment-overview) |
                | `ivy db add` | [Add database connections](app://onboarding/cli/database-integration/database-overview) |
                | `ivy auth add` | [Add authentication](app://onboarding/cli/authentication/authentication-overview) |
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.FileBasedAppsApp), typeof(Onboarding.CLI.InitApp), typeof(Onboarding.CLI.Deployment.DeploymentOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.Authentication.AuthenticationOverviewApp)]; 
        return article;
    }
}

