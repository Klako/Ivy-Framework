using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/01_Introduction.md", searchHints: ["overview", "what-is", "framework", "fullstack"])]
public class IntroductionApp(bool onlyBody = false) : ViewBase
{
    public IntroductionApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("welcome-to-ivy", "Welcome to Ivy", 1), new ArticleHeading("what-you-can-do", "What You Can Do", 2), new ArticleHeading("getting-started", "Getting Started", 2), new ArticleHeading("easiest-way-file-based-apps-in-under-1-minute", "Easiest Way: File-based Apps in under 1 minute", 3), new ArticleHeading("understanding-the-code", "Understanding the Code", 4), new ArticleHeading("package-reference", "Package Reference", 5), new ArticleHeading("server-initialization", "Server Initialization", 5), new ArticleHeading("the-view", "The View", 5), new ArticleHeading("the-build-method", "The Build Method", 5), new ArticleHeading("ui-composition", "UI Composition", 5), new ArticleHeading("advanced-projects-regular-projects-in-under-3-minutes", "Advanced Projects: Regular Projects in under 3 minutes", 3), new ArticleHeading("installing-the-cli", "Installing the CLI", 4), new ArticleHeading("initializing-a-project", "Initializing a Project", 4), new ArticleHeading("running-your-project", "Running Your Project", 4), new ArticleHeading("community--resources", "Community & Resources", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Welcome to Ivy").OnLinkClick(onLinkClick)
            | Lead("Ivy is the ultimate framework for building internal tools and dashboards using Pure C#, unifying frontend and backend into a single cohesive codebase. It can also be used to build general-purpose full-stack applications. Ivy's main focus is on developer's experience, UI and UX.")
            | new Markdown("Ivy eliminates the traditional frontend/backend split by bringing React-like declarative patterns directly to C#. You build your entire project—UI, logic, and data access—in one place.").OnLinkClick(onLinkClick)
            | new Embed("https://www.youtube.com/watch?v=pQKSQR9BfD8")
            | new Markdown(
                """"
                ## What You Can Do
                
                - **[Database Integration](https://docs.ivy.app/onboarding/cli/database-integration/database-overview)**: Connect to SQL Server, PostgreSQL, Supabase, and more with `ivy db add`.
                - **[Authentication](https://docs.ivy.app/onboarding/cli/authentication/authentication-overview)**: Add Auth0, Clerk, or Microsoft Entra with `ivy auth add`.
                - **[Deployment](https://docs.ivy.app/onboarding/cli/deploy)**: Deploy to AWS, Azure, GCP, or Sliplane with `ivy deploy`.
                - **AI Agentic Features** : Generate entire back-office applications from your database schema using `ivy app create`.
                
                ## Getting Started
                
                ### Easiest Way: File-based Apps in under 1 minute
                
                The fastest way to try Ivy is using [.NET 10's file-based apps feature](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview). This allows you to create a self-contained full-stack application in a single script-like file without any project files.
                
                Make sure to have latest .NET 10 installed, [download it here](https://dotnet.microsoft.com/en-us/download)
                
                1. Create a new file called `HelloWorldApp.cs`.
                2. Paste the following code:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                #:package Ivy@*
                
                using Ivy;
                
                var server = new Server();
                server.AddApp<HelloApp>();
                await server.RunAsync();
                
                [App]
                class HelloApp : ViewBase
                {
                    public override object? Build()
                    {
                        var nameState = UseState<string>();
                
                        return Layout.Center()
                               | new Card(
                                   Layout.Vertical().Gap(6).Padding(2)
                                   | new Confetti(new IvyLogo())
                                   | Text.H2("Hello " + (string.IsNullOrEmpty(nameState.Value) ? "there" : nameState.Value) + "!")
                                   | Text.Block("Welcome to the fantastic world of Ivy. Let's build something amazing together!")
                                   | nameState.ToInput(placeholder: "What is your name?")
                                   | new Separator()
                                   | Text.Markdown("You'd be a hero to us if you could ⭐ us on [Github](https://github.com/Ivy-Interactive/Ivy-Framework)")
                                 )
                                 .Width(Size.Units(120).Max(500));
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                #### Understanding the Code
                
                Let's break down each part of the file-based application.
                
                ##### Package Reference
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("#:package Ivy@*",Languages.Csharp)
            | new Markdown(
                """"
                This is a direct [Ivy nuget package](https://www.nuget.org/packages/Ivy) reference. It tells the .NET runtime to download and use the latest version of the framework, allowing you to run this file as a standalone script without a project file.
                
                ##### Server Initialization
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.AddApp<HelloWorldApp>();
                
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                This part initializes the Ivy server, registers your `HelloWorldApp` view, and starts the server. The server handles all state management and real-time communication. [Learn more about the Ivy program here](https://docs.ivy.app/onboarding/concepts/program).
                
                ##### The View
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App]
                class HelloWorldApp : ViewBase
                {
                  // ...
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                In Ivy, your UI is organized into views. By inheriting from `ViewBase`, you get access to all the hooks and lifecycle methods. The [[App] attribute](https://docs.ivy.app/onboarding/concepts/apps) tells Ivy to show this view in the main navigation.
                
                ##### The Build Method
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public override object? Build()
                {
                  return Layout.Center()
                      | new Card(
                          // ...
                      );
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                The `Build()` method is where you define your UI. It returns a tree of components that Ivy renders on the client. Just like in React, whenever state changes, this method is called again to determine the new UI structure.
                
                ##### UI Composition
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Layout.Center()
                    | new Card(...)
                """",Languages.Csharp)
            | new Markdown(
                """"
                Ivy uses a fluent API and the pipe operator (`|`) to compose layouts and widgets. This makes it easy to read and build complex hierarchical UIs. Learn more about the [Card widget here](https://docs.ivy.app/widgets/common/card).
                
                3. Run it immediately using the Ivy CLI:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet run HelloWorldApp.cs")
                
            | new Markdown(
                """"
                Ivy will start a local server at `http://localhost:5010`. Open it in your browser to see your interactive "Hello" app!
                
                ---
                
                ### Advanced Projects: Regular Projects in under 3 minutes
                
                For larger applications that require multiple files, services, and deep integrations, we recommend creating a regular Ivy project using the CLI.
                
                #### Installing the CLI
                
                To use the `ivy` command, you'll need the **.NET 10 SDK** installed. Then, install the Ivy CLI globally:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet tool install -g Ivy.Console")
                
            | new Markdown("Verify the installation:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy --version")
                
            | new Markdown(
                """"
                #### Initializing a Project
                
                Create a new directory for your project and initialize it. We recommend using the `--hello` flag to include an example hello app:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("mkdir MyProject")
                .AddCommand("cd MyProject")
                .AddCommand("ivy init --hello")
                
            | new Markdown(
                """"
                #### Running Your Project
                
                Run the project with hot reloading enabled:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run")
                
            | new Markdown(
                """"
                ## Community & Resources
                
                - **[Ivy Samples](https://samples.ivy.app)**: Real-time demo of all Ivy widgets and layouts.
                - **[App Gallery](https://ivy.app/gallery)**: See real-world applications and integrations built with Ivy.
                - **[Ivy Framework GitHub](https://github.com/Ivy-Interactive/Ivy-Framework)**: The core framework source code. Open-source and free to use.
                - **[Ivy Examples GitHub](https://github.com/Ivy-Interactive/Ivy-Examples)**: A collection of example projects to kickstart your development.
                """").OnLinkClick(onLinkClick)
            ;
        return article;
    }
}

