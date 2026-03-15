using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_Oracle.md", searchHints: ["oracle", "database", "sql", "enterprise", "relational", "db"])]
public class OracleApp(bool onlyBody = false) : ViewBase
{
    public OracleApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("oracle-database-provider", "Oracle Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("oracle-specific-features", "Oracle-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Oracle Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to Oracle Database with automatic Entity Framework configuration.")
            | new Markdown(
                """"
                ## Overview
                
                Oracle Database is a multi-model database management system developed by Oracle Corporation. It's widely used in enterprise environments and offers advanced features for mission-critical applications.
                
                ## Adding a Database Connection
                
                To set up Oracle Database with Ivy, run the following command and choose `Oracle` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown("You will be asked to name your connection, then prompted for a connection string. The connection string you provide should follow this format:").OnLinkClick(onLinkClick)
            | new CodeBlock("Data Source=localhost:1521/FREEPDB1; User Id=user; Password=password;",Languages.Text)
            | new Markdown(
                """"
                Specifically, your connection string should contain the following information, in the form of semicolon-separated key-value pairs:
                
                - **Data Source**: Database to connect to, specified by either an easy connect name (as shown above), a connect descriptor, or an Oracle net services name.
                - **User Id** and **Password**: The credentials used to authenticate to the server.
                
                For all connection options, see the [Oracle documentation](https://docs.oracle.com/en/database/oracle/oracle-database/19/odpnt/ConnectionConnectionString.html#GUID-DF4ED9A3-1AAF-445D-AEEF-016E6CD5A0C0__BABBAGJJ).
                
                Your connection string will be stored in [.NET user secrets](app://onboarding/concepts/secrets).
                
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Configuration
                
                Ivy automatically configures the **Oracle.EntityFrameworkCore** package for Oracle connections.
                
                ## Oracle-Specific Features
                
                Key enterprise features Ivy can leverage:
                
                - **Advanced Security** - encryption and access controls
                - **Performance** - partitioning and indexing
                - **PL/SQL** - stored procedures and functions
                
                For complete features, see the [Oracle Database documentation](https://docs.oracle.com/en/database/oracle/oracle-database/index.html).
                
                ## Security Best Practices
                
                - **Use encrypted connections** with SSL/TLS
                - **Create dedicated database users** with minimal required privileges
                - **Enable auditing** for sensitive operations
                - **Use Oracle Advanced Security** features in production
                - **Implement connection pooling** to optimize resource usage
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Authentication Failed**
                
                - Check username/password and account status
                
                For detailed troubleshooting, refer to [Oracle Database Error Messages](https://docs.oracle.com/en/database/oracle/oracle-database/19/errmg/index.html).
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [SQL Server Provider](app://onboarding/cli/database-integration/sql-server)
                - [PostgreSQL Provider](app://onboarding/cli/database-integration/postgre-sql)
                - [Enterprise Features](app://hooks/core/use-service)
                - [Official Oracle Database Documentation](https://docs.oracle.com/en/database/oracle/oracle-database/index.html)
                - [Oracle.EntityFrameworkCore Package](https://docs.oracle.com/en/database/oracle/oracle-data-access-components/19.3/odpnt/ODPEFCore.html)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.SqlServerApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp), typeof(Hooks.Core.UseServiceApp)]; 
        return article;
    }
}

