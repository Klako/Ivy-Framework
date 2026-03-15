using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, title:"ClickHouse", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_ClickHouse.md", searchHints: ["clickhouse", "database", "analytics", "olap", "columnar", "db"])]
public class ClickHouseApp(bool onlyBody = false) : ViewBase
{
    public ClickHouseApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("clickhouse-database-provider", "ClickHouse Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("clickhouse-specific-features", "ClickHouse-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# ClickHouse Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to ClickHouse with automatic Entity Framework configuration for high-performance analytics.")
            | new Markdown(
                """"
                ## Overview
                
                ClickHouse is an open-source column-oriented database management system designed for online analytical processing (OLAP) that allows generating analytical reports using SQL queries in real-time. Ivy provides seamless integration with ClickHouse through Entity Framework Core, enabling you to leverage its high-performance capabilities in your applications.
                
                ## Adding a Database Connection
                
                To set up ClickHouse with Ivy, run the following command and choose `ClickHouse` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown("You will be asked to name your connection, then prompted for a connection string. The connection string you provide should follow this format:").OnLinkClick(onLinkClick)
            | new CodeBlock("Host=localhost; Port=8123; Protocol=http; Database=my_db; Username=default; Password=mypassword;",Languages.Text)
            | new Markdown(
                """"
                Specifically, your connection string should contain the following information, in the form of semicolon-separated key-value pairs:
                
                - **Host**: The hostname of your running ClickHouse server instance.
                - **Protocol**: The protocol used to connect to the server. This should be either `http` or `https`.
                - **Port**: The port used to connect to the server. By default this is 8123 for HTTP connections and 8443 for HTTPS connections.
                - **Database**: The name of the database you wish to connect to.
                - **Username** and **Password**: The credentials used to authenticate to the server. These may be omitted in simple cases, e.g., running a local unsecured instance during development.
                
                Your connection string will be stored in [.NET user secrets](app://onboarding/concepts/secrets).
                
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Configuration
                
                Ivy automatically configures the **EntityFrameworkCore.ClickHouse** package and imports the `ClickHouse.EntityFrameworkCore.Extensions` namespace for ClickHouse connections.
                
                ## ClickHouse-Specific Features
                
                Key features for analytics applications:
                
                - **Columnar storage** for efficient query processing
                - **Materialized views** for pre-computed results
                - **Vectorized query execution** for high performance
                
                See the [ClickHouse Features Overview](https://clickhouse.com/docs/en/about-us/distinctive-features) for details.
                
                ## Security Best Practices
                
                - **Use HTTPS** for secure connections
                - **Implement IP-based access restrictions**
                - **Use a separate user** with limited permissions for application access
                - **Enable secure communication** between ClickHouse nodes in a cluster
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Connection Issues**
                
                - Verify server is running and listening on the specified port
                - Check credentials and permissions
                
                **Performance Issues**
                
                - Review table engine selection and indexing strategy
                - Consider materialized views for common queries
                
                See the [ClickHouse Troubleshooting Guide](https://clickhouse.com/docs/guides/troubleshooting) for more help.
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [PostgreSQL Provider](app://onboarding/cli/database-integration/postgre-sql)
                - [Snowflake Provider](app://onboarding/cli/database-integration/snowflake)
                - [Official ClickHouse Documentation](https://clickhouse.com/docs/)
                - [EntityFrameworkCore.ClickHouse Package](https://www.nuget.org/packages/EntityFrameworkCore.ClickHouse/)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp), typeof(Onboarding.CLI.DatabaseIntegration.SnowflakeApp)]; 
        return article;
    }
}

