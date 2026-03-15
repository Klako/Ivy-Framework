using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI;

[App(order:1, title:"CLI Overview", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/01_CLIOverview.md", searchHints: ["cli", "command-line", "terminal", "tools", "commands", "ivy-console"])]
public class CLIOverviewApp(bool onlyBody = false) : ViewBase
{
    public CLIOverviewApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("cli-overview", "CLI Overview", 1), new ArticleHeading("quick-start", "Quick Start", 2), new ArticleHeading("key-features", "Key Features", 2), new ArticleHeading("database-support", "Database Support", 3), new ArticleHeading("authentication-providers", "Authentication Providers", 3), new ArticleHeading("deployment-options", "Deployment Options", 3), new ArticleHeading("project-structure", "Project Structure", 2), new ArticleHeading("getting-help", "Getting Help", 2), new ArticleHeading("next-steps", "Next Steps", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# CLI Overview").OnLinkClick(onLinkClick)
            | Lead("Streamline your Ivy development workflow with powerful CLI tools for project initialization, database integration, authentication, and deployment.")
            | new Markdown(
                """"
                Ivy CLI is a powerful tool designed to streamline the development of Ivy projects. It provides:
                
                - **Database Integration**: Connect to multiple database providers (SQL Server, PostgreSQL, MySQL, SQLite, and more). See [Database Overview](app://onboarding/cli/database-integration/database-overview) and [Connections](app://onboarding/concepts/connections).
                - **Authentication**: Add [authentication](app://onboarding/cli/authentication/authentication-overview) providers (Auth0, Supabase, Authelia, Basic Auth)
                - **Deployment**: [Deploy](app://onboarding/cli/deployment/deployment-overview) to cloud platforms (AWS, Azure, GCP)
                - **Project Management**: Initialize and manage Ivy projects with ease
                
                ## Quick Start
                
                Get started with Ivy CLI in just a few commands:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy init")
                .AddCommand("ivy upgrade")
                .AddCommand("ivy db add")
                .AddCommand("ivy auth add")
                .AddCommand("ivy deploy")
                
            | new Callout(
                """"
                If you're using a specific operating system, read the instructions in your terminal after installing Ivy.Console.
                You can always see all available commands by using `ivy --help`.
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Key Features
                
                ### Database Support
                
                - **SQL Server** - Microsoft's enterprise database
                - **PostgreSQL** - Advanced open-source database
                - **MySQL/MariaDB** - Popular open-source databases
                - **SQLite** - Lightweight file-based database
                - **Supabase** - Open-source Firebase alternative
                - **Airtable** - Spreadsheet-database hybrid
                - **Oracle** - Enterprise database system
                - **Google Spanner** - Globally distributed database
                - **ClickHouse** - Column-oriented database
                - **Snowflake** - Cloud data platform
                
                ### Authentication Providers
                
                - **Auth0** - Universal authentication platform
                - **Supabase Auth** - Built-in authentication
                - **Authelia** - Open-source identity provider
                - **Basic Auth** - Simple username/password authentication
                
                ### Deployment Options
                
                - **AWS** - Amazon Web Services
                - **Azure** - Microsoft Azure
                - **GCP** - Google Cloud Platform
                
                ## Project Structure
                
                An Ivy project follows a standardized structure:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                YourProject/
                ├── Program.cs              # Main project entry point
                ├── YourProject.csproj      # .NET project file
                ├── README.md               # Project documentation
                ├── Apps/                   # User interface code
                ├── Connections/            # Database connections
                │   └── [ConnectionName]/   # Individual connection configs
                ├── .ivy/                   # Ivy-specific configuration, only created by Ivy CLI when necessary
                └── .gitignore              # Git ignore file
                """",Languages.Text)
            | new Markdown(
                """"
                See [Program](app://onboarding/concepts/program) for the entry point, [Apps](app://onboarding/concepts/apps) for application UI code, and [Connections](app://onboarding/concepts/connections) for database connections.
                
                ## Getting Help
                
                - Use `ivy --help` for general help
                - Use `ivy [command] --help` for command-specific help
                - Use `ivy docs` to open documentation
                - Use `ivy samples` to see example projects
                
                Most Ivy commands require authentication. Use `ivy login` to authenticate with your Ivy account.
                
                ## Next Steps
                
                1. **Initialize a project**: `ivy init`
                2. **Upgrade your project**: `ivy upgrade`
                3. **Add a database**: `ivy db add`
                4. **Add authentication**: `ivy auth add`
                5. **Deploy your project**: `ivy deploy`
                
                For detailed information on each feature, see the specific documentation files:
                
                - [Project Initialization](app://onboarding/cli/init)
                - [Project Upgrade](app://onboarding/cli/upgrade)
                - [Database Integration](app://onboarding/cli/database-integration/database-overview)
                - [Authentication Setup](app://onboarding/cli/authentication/authentication-overview)
                - [Deployment Guide](app://onboarding/cli/deployment/deployment-overview)
                - [Framework Information](app://onboarding/cli/question)
                - [Documentation Index](app://onboarding/cli/docs)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.Concepts.ConnectionsApp), typeof(Onboarding.CLI.Authentication.AuthenticationOverviewApp), typeof(Onboarding.CLI.Deployment.DeploymentOverviewApp), typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.CLI.InitApp), typeof(Onboarding.CLI.UpgradeApp), typeof(Onboarding.CLI.QuestionApp), typeof(Onboarding.CLI.DocsApp)]; 
        return article;
    }
}

