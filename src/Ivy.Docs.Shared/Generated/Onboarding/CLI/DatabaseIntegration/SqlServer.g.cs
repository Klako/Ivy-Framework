using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, title:"SQL Server", documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_SqlServer.md", searchHints: ["sqlserver", "mssql", "database", "microsoft", "sql", "db"])]
public class SqlServerApp(bool onlyBody = false) : ViewBase
{
    public SqlServerApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("sql-server-database-provider", "SQL Server Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("advanced-configuration", "Advanced Configuration", 2), new ArticleHeading("custom-schema", "Custom Schema", 3), new ArticleHeading("schema-support", "Schema Support", 3), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# SQL Server Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to Microsoft SQL Server with automatic Entity Framework configuration.")
            | new Markdown(
                """"
                ## Overview
                
                SQL Server is Microsoft's enterprise-grade relational database management system. Ivy provides seamless integration with SQL Server through Entity Framework Core.
                
                ## Adding a Database Connection
                
                To set up SQL Server with Ivy, run the following command and choose `SqlServer` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown(
                """"
                You will be asked to name your connection, then prompted for a connection string. The connection string you provide should follow one of the following formats, depending on your authentication mode:
                
                > For details on authentication modes, see Microsoft's [Choose an authentication mode](https://learn.microsoft.com/en-us/sql/relational-databases/security/choose-an-authentication-mode).
                
                **Windows Authentication (Recommended)**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Server=localhost; Database=my_db; Trusted_Connection=True;",Languages.Text)
            | new Markdown("**SQL Server Authentication**").OnLinkClick(onLinkClick)
            | new CodeBlock("Server=localhost; Database=my_db; User Id=user; Password=password;",Languages.Text)
            | new Markdown(
                """"
                Specifically, your connection string should contain the following information, in the form of semicolon-separated key-value pairs:
                
                - **Server**: The hostname of your SQL Server instance.
                - **Database**: The name of the database you wish to connect to.
                - One of the following sets of options:
                  - **Trusted_Connection**: Set to `True` to use Windows authentication.
                  - **User ID** and **Password**: The credentials used to authenticate with SQL Server authentication.
                
                For all connection options, see the [SqlConnection.ConnectionString documentation](https://learn.microsoft.com/en-us/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring).
                
                Your connection string will be stored in [.NET user secrets](app://onboarding/concepts/secrets).
                
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Configuration
                
                Ivy automatically configures the **Microsoft.EntityFrameworkCore.SqlServer** package for SQL Server connections.
                
                ## Advanced Configuration
                
                ### Custom Schema
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider SqlServer --name MySqlServer --schema MyCustomSchema")
                
            | new Markdown(
                """"
                ### Schema Support
                
                SQL Server supports multiple schemas. When connecting with Ivy, you'll be prompted to select a schema from your database, or you can specify one directly using the `--schema` parameter:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider SqlServer --name MySqlServer --schema MyCustomSchema")
                
            | new Markdown(
                """"
                See Microsoft's [Create a database schema](https://learn.microsoft.com/en-us/sql/relational-databases/security/authentication-access/create-a-database-schema) for more details.
                
                ## Security Best Practices
                
                - **Use Windows Authentication** when possible for local development
                - **Use Azure AD authentication** for Azure SQL Database
                - **Enable encryption** in connection strings for production
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Connection Problems**
                
                - Verify server is running and network connectivity
                - Check credentials and permissions
                - Ensure firewall allows port 1433
                
                For detailed troubleshooting, see [SQL Server Troubleshooting](https://learn.microsoft.com/en-us/troubleshoot/sql/welcome-sql-server).
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [PostgreSQL Provider](app://onboarding/cli/database-integration/postgre-sql)
                - [MySQL Provider](app://onboarding/cli/database-integration/my-sql)
                - [SQLite Provider](app://onboarding/cli/database-integration/sq-lite)
                - [SQL Server Technical Documentation](https://learn.microsoft.com/en-us/sql/sql-server/)
                - [SQL Server EF Core Database Provider](https://learn.microsoft.com/en-us/ef/core/providers/sql-server/)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp), typeof(Onboarding.CLI.DatabaseIntegration.MySqlApp), typeof(Onboarding.CLI.DatabaseIntegration.SQLiteApp)]; 
        return article;
    }
}

