using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:19, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/19_FileBasedApps.md", searchHints: ["file-based", "single file", "script", "dotnet run", "quick start", "minimal", "no project"])]
public class FileBasedAppsApp(bool onlyBody = false) : ViewBase
{
    public FileBasedAppsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("file-based-apps", "File-Based Apps", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("prerequisites", "Prerequisites", 2), new ArticleHeading("file-level-directive-package", "File-Level Directive: Package", 2), new ArticleHeading("usings", "Usings", 2), new ArticleHeading("running-the-file", "Running the File", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# File-Based Apps").OnLinkClick(onLinkClick)
            | Lead("Run an Ivy app from a single `.cs` file—no project scaffolding, no `ivy init`, no solution or folder structure. Ideal for quick experiments, demos, and learning.")
            | new Markdown(
                """"
                Usually you create Ivy apps with [ivy init](app://onboarding/cli/init) and run them with [ivy run](app://onboarding/cli/run). **File-based apps** let you write one file and run it with `dotnet run YourFile.cs`, without any other project files.
                
                ## Basic Usage
                
                Create a file, for example `HelloApp.cs`:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --script")
                
            | new Markdown("You will receive a file-based app that you can populate with your own context as needed.").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                #: package Ivy@*
                
                using Ivy;
                
                var server = new Server();
                server.AddApp<HelloApp>();
                await server.RunAsync();
                
                [App]
                class HelloApp : ViewBase
                {
                    public override object? Build()
                    {
                        return Layout.Center(
                            new Card(
                                Text.P("Hello")
                            ).Width(Size.Units(60))
                        );
                    }
                }
                """",Languages.Csharp)
            | new Markdown("Run it from the same directory:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet run HelloApp.cs")
                
            | new Markdown(
                """"
                The app starts (by default on port 5010). Open the URL shown in the terminal to see your app.
                
                ## Prerequisites
                
                - [**.NET 10** or later](https://dotnet.microsoft.com/download/dotnet/10.0) (single-file `dotnet run` is supported from .NET 10). See [Enhanced file-based apps](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/sdk#enhanced-file-based-apps-with-publish-support-and-native-aot) in the Microsoft documentation.
                - [Ivy NuGet package](https://www.nuget.org/packages/Ivy) (referenced via a file-level directive in the script).
                
                ## File-Level Directive: Package
                
                At the top of the file, use the **package** directive so the file can use Ivy without a `.csproj`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("#: package Ivy@*",Languages.Csharp)
            | new Markdown(
                """"
                - `#: package` – file-level NuGet package reference (no project file needed).
                - `Ivy@*` – the [Ivy](https://www.nuget.org/packages/Ivy) package; `*` means latest version. You can pin a version, e.g. `Ivy@1.2.0`.
                
                ## Usings
                
                Include the namespaces you use in the file. Common ones:
                
                | Namespace | Use for |
                |-----------|---------|
                | `Ivy` | [`Server`](app://onboarding/concepts/program), server configuration and `RunAsync()`. [`AppDescriptor`](app://onboarding/concepts/apps), `AppIds`, `AppHelpers`, the `[App]` attribute. [`UseService`](app://hooks/core/use-service), service registration and resolution. Helper utilities. [`ViewBase`](app://onboarding/concepts/views), [`Layout`](app://onboarding/concepts/layout), and built-in [widgets](app://onboarding/concepts/widgets) ([`Card`](app://widgets/common/card), [`Text`](app://widgets/primitives/text-block), [`Button`](app://widgets/common/button), etc.). [Chrome](app://onboarding/concepts/chrome), sidebar, and layout configuration. [Client](app://onboarding/concepts/clients) and API usage. [Authentication](app://onboarding/cli/authentication/authentication-overview) providers. [Input widgets](app://widgets/inputs/_index) ([`TextInput`](app://widgets/inputs/text-input), [`SelectInput`](app://widgets/inputs/select-input), etc.). |
                | `Ivy.Core` | Core Ivy types. |
                | `Ivy.Core.Hooks` | [Hooks](app://hooks/hook-introduction) ([UseState](app://hooks/core/use-state), [UseEffect](app://hooks/core/use-effect), [UseMemo](app://hooks/core/use-memo), etc.). |
                
                Example (minimal for a simple app):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("using Ivy;",Languages.Csharp)
            | new Markdown(
                """"
                If you use only certain widgets or types, you might need extra namespaces (for example from other Ivy packages). Add `using` directives as you would in a normal C# project.
                
                ## Running the File
                
                From the directory that contains your `.cs` file:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet run HelloApp.cs")
                
            | new Markdown("If you need a specific port, set it via `ServerArgs`:").OnLinkClick(onLinkClick)
            | new CodeBlock("var server = new Server(new ServerArgs { Port = 5011 });",Languages.Csharp)
            | new Markdown("Or use the `PORT` environment variable before running:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("set PORT=5011")
                .AddCommand("dotnet run HelloApp.cs")
                
            | new Markdown("See [Program](app://onboarding/concepts/program) for all available `ServerArgs` properties and [Ivy Run](app://onboarding/cli/run) for the full list of CLI options.").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.CLI.InitApp), typeof(Onboarding.CLI.RunApp), typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.Concepts.AppsApp), typeof(Hooks.Core.UseServiceApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Common.CardApp), typeof(Widgets.Primitives.TextBlockApp), typeof(Widgets.Common.ButtonApp), typeof(Onboarding.Concepts.ChromeApp), typeof(Onboarding.Concepts.ClientsApp), typeof(Onboarding.CLI.Authentication.AuthenticationOverviewApp), typeof(Widgets.Inputs._IndexApp), typeof(Widgets.Inputs.TextInputApp), typeof(Widgets.Inputs.SelectInputApp), typeof(Hooks.HookIntroductionApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseMemoApp)]; 
        return article;
    }
}

