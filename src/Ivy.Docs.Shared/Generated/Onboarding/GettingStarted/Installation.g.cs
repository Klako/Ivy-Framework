using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/02_Installation.md", searchHints: ["setup", "install", "cli", "getting-started", "download", "prerequisites", "packages", "dependencies", "project-structure"])]
public class InstallationApp(bool onlyBody = false) : ViewBase
{
    public InstallationApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("installation-and-project-setup", "Installation and Project Setup", 1), new ArticleHeading("quick-start-using-the-cli", "Quick Start: Using the CLI", 2), new ArticleHeading("install-ivy-globally", "Install Ivy Globally", 3), new ArticleHeading("create-a-new-ivy-project", "Create a New Ivy Project", 3), new ArticleHeading("prerequisites", "Prerequisites", 2), new ArticleHeading("manual-setup-creating-your-first-project", "Manual Setup: Creating Your First Project", 2), new ArticleHeading("create-console-application", "Create Console Application", 3), new ArticleHeading("add-ivy-package", "Add Ivy Package", 3), new ArticleHeading("basic-server-configuration", "Basic Server Configuration", 3), new ArticleHeading("project-file-configuration", "Project File Configuration", 3), new ArticleHeading("core-package-installation", "Core Package Installation", 2), new ArticleHeading("project-structure-overview", "Project Structure Overview", 2), new ArticleHeading("multi-project-solutions", "Multi-Project Solutions", 3), new ArticleHeading("server-configuration", "Server Configuration", 2), new ArticleHeading("development-vs-production", "Development vs Production", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Installation and Project Setup").OnLinkClick(onLinkClick)
            | Lead("This page covers the installation of Ivy Framework components, setting up a new Ivy project, and understanding the basic project structure. It provides the foundational steps needed before building applications with Ivy.")
            | new Markdown(
                """"
                For information about core Ivy concepts like [Views](app://onboarding/concepts/views) and [state management](app://hooks/core/use-state), see [Core Concepts](app://onboarding/concepts/views). For guidance on building your first application, see [Basics](app://onboarding/getting-started/basics). For development tools and CLI commands, see [CLI Tools](app://onboarding/cli/cli-overview).
                
                ## Quick Start: Using the CLI
                
                The easiest way to set up an Ivy project is using the Ivy [CLI](app://onboarding/cli/cli-overview). This will automatically create the project structure, configuration files, and necessary setup.
                
                ### Install Ivy Globally
                
                Run the following command in your terminal to install Ivy as a global tool:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet tool install -g Ivy.Console")
                
            | new Callout(
                """"
                If you're using a specific operating system, read the instructions in your terminal after installing Ivy.Console.
                You can always see all available commands by using `ivy --help`.
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Create a New Ivy Project
                
                Use the Ivy CLI to scaffold a new project:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init --namespace Acme.InternalProject")
                .AddCommand("ivy run")
                
            | new Markdown(
                """"
                This will create a new Ivy project with the necessary structure and configuration files. For more details about the generated project structure, see [Ivy Init](app://onboarding/cli/init).
                
                ## Prerequisites
                
                Ivy Framework requires .NET 10.0 as the target framework. All Ivy projects and packages are built against this version.
                
                ## Manual Setup: Creating Your First Project
                
                If you prefer to set up a project manually, follow these steps:
                
                ### Create Console Application
                
                Create a new .NET console application:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet new console -n MyIvyApp")
                .AddCommand("cd MyIvyApp")
                
            | new Markdown("### Add Ivy Package").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet add package Ivy")
                
            | new Markdown(
                """"
                ### Basic Server Configuration
                
                Replace the contents of `Program.cs` with minimal server setup:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                using Ivy;
                
                var server = new Server();
                server.UseHotReload();
                server.AddAppsFromAssembly();
                server.UseChrome();
                
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                This configuration creates a Server instance, enables hot reload for development, automatically discovers apps in the current assembly, uses default chrome (sidebar navigation), and starts the server.
                
                ### Project File Configuration
                
                Ensure your `.csproj` targets .NET 10.0:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                <Project Sdk="Microsoft.NET.Sdk">
                  <PropertyGroup>
                    <OutputType>Exe</OutputType>
                    <TargetFramework>net10.0</TargetFramework>
                    <ImplicitUsings>enable</ImplicitUsings>
                    <Nullable>enable</Nullable>
                  </PropertyGroup>
                </Project>
                """",Languages.Xml)
            | new Markdown(
                """"
                ## Core Package Installation
                
                The primary Ivy Framework package is installed via NuGet and provides the foundation for your application.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet add package Ivy")
                
            | new Markdown(
                """"
                | Component | Description |
                | :--- | :--- |
                | **Core Framework** | High-performance server-side engine and application system |
                | **Widget System** | Library of strongly-typed UI components (Shadcn/Tailwind) |
                | **SignalR Hub** | Real-time state synchronization between C# and React |
                | **Embedded Assets** | Pre-built frontend bundle embedded in the DLL |
                | **Auth Interfaces** | extensible framework for security and identity |
                
                Extend Ivy's functionality with official extension packages for authentication and data management.
                
                | Package | Purpose |
                | :--- | :--- |
                | `Ivy.Auth.Supabase` | Identity management via [Supabase](app://onboarding/cli/authentication/supabase) |
                | `Ivy.Auth.Authelia` | Single Sign-On and 2FA via [Authelia](app://onboarding/cli/authentication/authelia) |
                | `Ivy.Auth.Entra` | Microsoft [Entra](app://onboarding/cli/authentication/microsoft-entra) ID (Azure AD) integration |
                | `Ivy.Database.Generator.Toolkit` | Utilities for AI-powered schema and code generation |
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet add package Ivy.Auth.Supabase")
                .AddCommand("dotnet add package Ivy.Database.Generator.Toolkit")
                
            | new Markdown(
                """"
                The Ivy package abstracts away several modern technologies to provide its seamless developer experience:
                
                - **ASP.NET Core**: Secure and scalable web hosting
                - **SignalR**: Low-latency, real-time communication
                - **JWT & Auth**: Industrial-grade security protocols
                - **System.Reactive**: Event-driven UI updates
                - **JSON Patch**: Efficient state synchronization
                
                ## Project Structure Overview
                
                A standard Ivy project follows a clean, flattened structure designed for clarity.
                
                | File/Folder | Description |
                | :--- | :--- |
                | **`Project.csproj`** | Matches the .NET 10.0 target and contains Ivy references |
                | **`Program.cs`** | The entry point where you configure and run the Ivy server |
                | **`Apps/`** | Where your Views and business logic reside |
                | **`Assets/`** | Optional static files (images, custom CSS) |
                
                ### Multi-Project Solutions
                
                For enterprise-scale applications, we recommend a multi-project structure to separate concerns:
                
                - **Web Project**: Contains `Program.cs` and server startup configuration.
                - **Shared/Core Project**: Contains the majority of your `ViewBase` classes and domain logic.
                - **Test Project**: Contains unit and integration tests for your UI components.
                
                ## Server Configuration
                
                The server configuration follows a builder pattern where each method configures different aspects of the Ivy application before calling `RunAsync()` to start the web server.
                
                For detailed server configuration options, including `ServerArgs` properties and advanced settings, see [Program](app://onboarding/concepts/program).
                
                ### Development vs Production
                
                The server automatically optimizes its behavior based on the current environment.
                
                | Feature | Development | Production |
                | :--- | :--- | :--- |
                | **Hot Reload** | Enabled (instant UI updates) | Disabled (optimized performance) |
                | **Error Handling** | Detailed stack traces | Secure, logged exceptions |
                | **Caching** | Disabled for immediate changes | Aggressive ETag & compression |
                | **Logging** | Debug & Information | Warning & Error only |
                | **Port Management** | Conflict detection & auto-shift | Strict port binding |
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.GettingStarted.BasicsApp), typeof(Onboarding.CLI.CLIOverviewApp), typeof(Onboarding.CLI.InitApp), typeof(Onboarding.CLI.Authentication.SupabaseApp), typeof(Onboarding.CLI.Authentication.AutheliaApp), typeof(Onboarding.CLI.Authentication.MicrosoftEntraApp), typeof(Onboarding.Concepts.ProgramApp)]; 
        return article;
    }
}

