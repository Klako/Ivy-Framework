using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, title:"MySQL", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_MySql.md", searchHints: ["mysql", "database", "sql", "relational", "oracle", "db"])]
public class MySqlApp(bool onlyBody = false) : ViewBase
{
    public MySqlApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("mysql-database-provider", "MySQL Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("mysql-specific-features", "MySQL-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# MySQL Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to MySQL with automatic Entity Framework configuration.")
            | new Markdown(
                """"
                ## Overview
                
                MySQL is one of the world's most popular open-source relational databases, known for its speed, reliability, and ease of use. Ivy provides seamless integration with MySQL through Entity Framework Core.
                
                ## Adding a Database Connection
                
                To set up MySQL with Ivy, run the following command and choose `MySql` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown("You will be asked to name your connection, then prompted for a connection string. The connection string you provide should follow this format:").OnLinkClick(onLinkClick)
            | new CodeBlock("Server=localhost; Database=my_db; User=user; Password=password;",Languages.Text)
            | new Markdown(
                """"
                Specifically, your connection string should contain the following information, in the form of semicolon-separated key-value pairs:
                
                - **Server**: The hostname of your MySQL server instance.
                - **Database**: The name of the database you wish to connect to.
                - **User** and **Password**: The credentials used to authenticate to the server.
                
                For all connection options, see the [MySqlConnector documentation](https://mysqlconnector.net/connection-options/).
                
                > **Note**: `MySqlConnector` is an ADO.NET driver, used by Ivy to connect with MySQL and MariaDB.
                
                Your connection string will be stored in [.NET user secrets](app://onboarding/concepts/secrets).
                
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Configuration
                
                Ivy automatically configures the **Pomelo.EntityFrameworkCore.MySql** package for MySQL connections.
                
                > **Note**: When you provide a connection string, Ivy will verify that you're connecting to an actual MySQL server (not MariaDB). If it detects MariaDB instead, you'll be prompted to use the MariaDB provider.
                
                ## MySQL-Specific Features
                
                Key features Ivy can leverage:
                
                - **JSON columns** for document storage (MySQL 5.7+)
                - **Full-text indexes** for search functionality
                - **Multiple storage engines** (InnoDB, MyISAM)
                
                See [MySQL Feature Reference](https://dev.mysql.com/doc/refman/8.4/en/features.html) for details.
                
                ## Security Best Practices
                
                - **Use SSL connections** in production environments
                - **Create dedicated database users** with minimal required permissions
                - **Enable binary logging** for point-in-time recovery
                - **Use connection pooling** to optimize performance
                
                For more security recommendations, see [MySQL Security Guidelines](https://dev.mysql.com/doc/refman/8.4/en/security-guidelines.html).
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Connection Issues**
                
                - Verify server is running and listening on the expected port
                - Check firewall settings
                
                **Authentication Problems**
                
                - Verify credentials and user privileges
                
                See [MySQL Problems and Common Errors](https://dev.mysql.com/doc/refman/8.4/en/problems.html) for more help.
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [MariaDB Provider](app://onboarding/cli/database-integration/maria-db)
                - [PostgreSQL Provider](app://onboarding/cli/database-integration/postgre-sql)
                - [SQL Server Provider](app://onboarding/cli/database-integration/sql-server)
                - [Official MySQL Documentation](https://dev.mysql.com/doc/)
                - [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.MariaDbApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp), typeof(Onboarding.CLI.DatabaseIntegration.SqlServerApp)]; 
        return article;
    }
}

