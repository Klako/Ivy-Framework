using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, title:"MariaDB", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_MariaDb.md", searchHints: ["mariadb", "database", "sql", "mysql", "relational", "db"])]
public class MariaDbApp(bool onlyBody = false) : ViewBase
{
    public MariaDbApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("mariadb-database-provider", "MariaDB Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("mariadb-specific-features", "MariaDB-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# MariaDB Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to MariaDB with automatic Entity Framework configuration.")
            | new Markdown(
                """"
                ## Overview
                
                MariaDB is a popular open-source relational database that started as a fork of MySQL. It offers enhanced features, improved performance, and better storage engines while maintaining MySQL compatibility. For more information, visit the [MariaDB documentation](https://mariadb.com/docs/general-resources/about/about-mariadb).
                
                ## Adding a Database Connection
                
                To set up MariaDB with Ivy, run the following command and choose `MariaDb` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown("You will be asked to name your connection, then prompted for a connection string. The connection string you provide should follow this format:").OnLinkClick(onLinkClick)
            | new CodeBlock("Server=localhost; Database=my_db; User=user; Password=password;",Languages.Text)
            | new Markdown(
                """"
                Specifically, your connection string should contain the following information, in the form of semicolon-separated key-value pairs:
                
                - **Server**: The hostname of your MariaDB server instance.
                - **Database**: The name of the database you wish to connect to.
                - **User** and **Password**: The credentials used to authenticate to the server.
                
                For all connection options, see the [MySqlConnector documentation](https://mysqlconnector.net/connection-options/).
                
                > **Note**: `MySqlConnector` is an ADO.NET driver, used by Ivy to connect with MariaDB and MySQL.
                
                Your connection string will be stored in [.NET user secrets](app://onboarding/concepts/secrets).
                
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Configuration
                
                Ivy automatically configures the **Pomelo.EntityFrameworkCore.MySql** package for MariaDB connections.
                
                > **Note**: When you provide a connection string, Ivy will verify that you're connecting to a MariaDB server (not MySQL). If it detects MySQL instead, you'll be prompted to use the MySQL provider.
                
                ## MariaDB-Specific Features
                
                Key advantages over MySQL:
                
                - **Advanced JSON support** with better performance
                - **Temporal tables** for data versioning
                - **Multiple storage engines** including Aria and ColumnStore
                
                See the [MariaDB Documentation](https://mariadb.com/kb/en/library/documentation/) for details.
                
                ## Security Best Practices
                
                - **Use SSL connections** in production environments
                - **Create dedicated database users** with minimal required permissions
                - **Enable binary logging** for point-in-time recovery
                - **Implement proper backup strategies** with MariaDB backup tools
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Connection Issues**
                
                - Verify server is running and listening on the expected port
                - Check firewall settings
                
                **Authentication Problems**
                
                - Verify credentials and user privileges
                
                See [MariaDB Troubleshooting](https://mariadb.com/kb/en/troubleshooting-connection-issues/) for more help.
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [MySQL Provider](app://onboarding/cli/database-integration/my-sql)
                - [PostgreSQL Provider](app://onboarding/cli/database-integration/postgre-sql)
                - [SQL Server Provider](app://onboarding/cli/database-integration/sql-server)
                - [Official MariaDB Documentation](https://mariadb.com/kb/en/documentation/)
                - [Pomelo.EntityFrameworkCore.MySql for MariaDB](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.MySqlApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp), typeof(Onboarding.CLI.DatabaseIntegration.SqlServerApp)]; 
        return article;
    }
}

