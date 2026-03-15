using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, title:"PostgreSQL", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_PostgreSql.md", searchHints: ["postgres", "postgresql", "database", "sql", "relational", "db"])]
public class PostgreSqlApp(bool onlyBody = false) : ViewBase
{
    public PostgreSqlApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("postgresql-database-provider", "PostgreSQL Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("advanced-configuration", "Advanced Configuration", 2), new ArticleHeading("custom-schema", "Custom Schema", 3), new ArticleHeading("postgresql-specific-features", "PostgreSQL-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# PostgreSQL Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to PostgreSQL with automatic Entity Framework configuration.")
            | new Markdown(
                """"
                ## Overview
                
                PostgreSQL is an advanced open-source relational database known for its reliability, feature robustness, and performance. Ivy provides seamless integration with PostgreSQL through Entity Framework Core.
                
                ## Adding a Database Connection
                
                To set up PostgreSQL with Ivy, run the following command and choose `Postgres` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown("You will be asked to name your connection, then prompted for a connection string. The connection string you provide should follow this format:").OnLinkClick(onLinkClick)
            | new CodeBlock("Host=localhost;Database=mydb;Username=user;Password=pass",Languages.Text)
            | new Markdown(
                """"
                Specifically, your connection string should contain the following information, in the form of semicolon-separated key-value pairs:
                
                - **Host**: The hostname of your Postgres server instance.
                - **Database**: The name of the database you wish to connect to.
                - **Username** and **Password**: The credentials used to authenticate to the server.
                
                > **Note**: Ivy also supports URI-style connection strings (e.g., `postgresql://user:password@host:port/dbname`) and will automatically convert them to the key-value format.
                
                For all connection options, see [Npgsql Connection String Parameters](https://www.npgsql.org/doc/connection-string-parameters.html).
                
                Your connection string will be stored in [.NET user secrets](app://onboarding/concepts/secrets).
                
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Configuration
                
                Ivy automatically configures the **Npgsql.EntityFrameworkCore.PostgreSQL** package for PostgreSQL connections.
                
                ## Advanced Configuration
                
                ### Custom Schema
                
                PostgreSQL supports multiple schemas. When configuring your PostgreSQL database with Ivy, you'll be prompted to select a schema from your database, or you can specify one directly using the `--schema` parameter:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Postgres --name MyPostgres --schema MyCustomSchema")
                
            | new Markdown(
                """"
                ## PostgreSQL-Specific Features
                
                Key features Ivy can leverage:
                
                - **JSONB columns** for document storage
                - **Array types** for collections
                - **Custom data types** and enums
                
                See [About PostgreSQL](https://www.postgresql.org/about/) for more information on PostgreSQL features.
                
                ## Security Best Practices
                
                - **Create dedicated database users** with minimal required permissions
                - **Enable row-level security** when appropriate
                - **Use connection pooling** to optimize performance
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Connection Issues**
                
                - Verify server is running and listening on expected port
                - Check credentials and firewall settings
                
                **Authentication Problems**
                
                - Check `pg_hba.conf` configuration
                
                For detailed help, see the [PostgreSQL Documentation](https://www.postgresql.org/docs/current/) and search for common issues in the [PostgreSQL Wiki](https://wiki.postgresql.org/wiki/Main_Page).
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [SQL Server Provider](app://onboarding/cli/database-integration/sql-server)
                - [MySQL Provider](app://onboarding/cli/database-integration/my-sql)
                - [Supabase Provider](app://onboarding/cli/database-integration/supabase)
                - [Official PostgreSQL Documentation](https://www.postgresql.org/docs/current/)
                - [Npgsql Entity Framework Core Provider](https://www.npgsql.org/efcore/)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.SqlServerApp), typeof(Onboarding.CLI.DatabaseIntegration.MySqlApp), typeof(Onboarding.CLI.DatabaseIntegration.SupabaseApp)]; 
        return article;
    }
}

