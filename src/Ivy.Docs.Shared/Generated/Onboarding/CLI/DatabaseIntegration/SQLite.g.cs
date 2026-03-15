using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, title:"SQLite", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_SQLite.md", searchHints: ["sqlite", "database", "file-based", "embedded", "local", "db"])]
public class SQLiteApp(bool onlyBody = false) : ViewBase
{
    public SQLiteApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("sqlite-database-provider", "SQLite Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("database-initialization", "Database Initialization", 2), new ArticleHeading("sqlite-specific-features", "SQLite-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# SQLite Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to SQLite with automatic Entity Framework configuration.")
            | new Markdown(
                """"
                ## Overview
                
                SQLite is a lightweight, file-based relational database that's perfect for development, testing, and applications that need a simple, self-contained database solution. No server setup required! Learn more about SQLite at the [official SQLite website](https://www.sqlite.org/).
                
                ## Adding a Database Connection
                
                To set up SQLite with Ivy, run the following command and choose `Sqlite` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown("You will be asked to name your connection, then prompted for the path to your database file:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddOutput("Path to database file: (existing_file.db):")
                
            | new Markdown(
                """"
                Ivy will attempt to suggest existing SQLite files in your project, but you can specify any path. Ivy automatically adds the database file to your project with `<CopyToOutputDirectory>Always</CopyToOutputDirectory>` so it's included when building your project.
                
                A connection string will be automatically generated as:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Data Source=data.db",Languages.Text)
            | new Markdown(
                """"
                Unlike other providers, SQLite doesn't store this connection string in [.NET user secrets](app://onboarding/concepts/secrets). Instead, it is included directly in the generated DbContextFactory source file.
                
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Configuration
                
                Ivy automatically configures the **Microsoft.EntityFrameworkCore.Sqlite** package for SQLite connections.
                
                ## Database Initialization
                
                The Database Generator for SQLite projects includes automatic database initialization and improved logging capabilities:
                
                - **Automatic Database Creation**: The generated DbContextFactory automatically creates the SQLite database file on first use
                - **Thread-Safe Initialization**: Uses semaphore locking for safe concurrent access
                - **Flexible Storage**: Supports custom storage volumes with a default location in the user's local application data folder
                - **Better Logging**: Entity Framework logs are properly routed through the application's logging infrastructure
                
                The generated factory accepts optional volume and logger parameters for enhanced configuration:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public MyDbContextFactory(
                    ServerArgs args,
                    IVolume? volume = null,
                    ILogger? logger = null
                )
                """",Languages.Csharp)
            | new Callout("The `IVolume` parameter allows custom storage locations for your SQLite database, defaulting to the user's local application data folder. Inject your custom `IVolume` implementation through dependency injection for production deployments, containerized applications, or multi-tenant scenarios.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                For more details on volume configuration, see the [Volume documentation](app://onboarding/concepts/volume).
                
                ## SQLite-Specific Features
                
                Key advantages:
                
                - **Zero-configuration** - no server setup
                - **Cross-platform** database files
                - **Full-text search** and **JSON support**
                
                See [SQLite Features](https://www.sqlite.org/features.html) for details.
                
                ## Security Best Practices
                
                - **Secure file permissions**
                - **Use WAL mode**: `PRAGMA journal_mode=WAL`
                - **Enable foreign keys**: `PRAGMA foreign_keys=ON`
                
                See [SQLite Security Considerations](https://www.sqlite.org/security.html) for more.
                
                ## Troubleshooting
                
                ### Common Issues
                
                **File Access Issues**
                
                - Check read/write permissions and directory existence
                - Ensure file isn't locked by another process
                
                **Database Locked Errors**
                
                - Close connections properly and use WAL mode
                
                See the [SQLite FAQ](https://www.sqlite.org/faq.html) for more help.
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [PostgreSQL Provider](app://onboarding/cli/database-integration/postgre-sql)
                - [SQL Server Provider](app://onboarding/cli/database-integration/sql-server)
                - [MySQL Provider](app://onboarding/cli/database-integration/my-sql)
                - [Official SQLite Documentation](https://www.sqlite.org/docs.html)
                - [EF Core SQLite Provider](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.Concepts.VolumeApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp), typeof(Onboarding.CLI.DatabaseIntegration.SqlServerApp), typeof(Onboarding.CLI.DatabaseIntegration.MySqlApp)]; 
        return article;
    }
}

