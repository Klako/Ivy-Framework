using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_Supabase.md", searchHints: ["supabase", "database", "postgres", "backend", "cloud", "db"])]
public class SupabaseApp(bool onlyBody = false) : ViewBase
{
    public SupabaseApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("supabase-database-provider", "Supabase Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("supabase-specific-features", "Supabase-Specific Features", 2), new ArticleHeading("security-best-practices", "Security Best Practices", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Supabase Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to Supabase with automatic Entity Framework configuration for PostgreSQL.")
            | new Markdown(
                """"
                ## Overview
                
                Supabase is an open-source Firebase alternative that provides a PostgreSQL database with real-time capabilities, authentication, and storage. Ivy integrates with Supabase using its PostgreSQL backend through the Npgsql provider. Learn more at the [Supabase website](https://supabase.com/).
                
                ## Adding a Database Connection
                
                To set up a Supabase database with Ivy, run the following command and choose `Supabase` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown(
                """"
                You will be asked to name your connection, then prompted for a connection string. To get your connection string:
                
                1. Go to your Supabase project dashboard
                2. Click the "Connect" button at the top of the page to access all available connection strings
                3. Choose the connection string type that's best for you:
                   - **Direct connection**: Best for persistent servers (VMs, containers)
                   - **Transaction pooler**: For applications with many short-lived connections
                   - **Session pooler**: Should only be used for applications that must use IPv4
                
                For more detailed information, see the [official Supabase documentation on connecting to Postgres](https://supabase.com/docs/guides/database/connecting-to-postgres).
                
                ![Supabase connect screen, showing all three types of connection string](/ivy/assets/supabase_connect_screen.webp "Supabase connect screen")
                
                Your connection string will look something like this:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("postgresql://postgres:[YOUR-PASSWORD]@db.[YOUR-PROJECT-REF].supabase.co:5432/postgres",Languages.Text)
            | new Markdown("In addition to the URI-style format, Ivy also supports the standard Entity Framework key-value format:").OnLinkClick(onLinkClick)
            | new CodeBlock("Host=db.[YOUR-PROJECT-REF].supabase.co;Database=postgres;Username=postgres;Password=[YOUR-PASSWORD]",Languages.Text)
            | new Markdown(
                """"
                For either format, remember to replace `[YOUR-PASSWORD]` with your database password, which is the password you used when creating the project (not your Supabase account password).
                
                Ivy CLI will automatically detect and convert URI-style connection strings to the key-value format. After conversion, your connection string will be stored in [.NET user secrets](app://onboarding/concepts/secrets).
                
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Configuration
                
                Ivy treats Supabase as a specialized PostgreSQL provider, using the **Npgsql.EntityFrameworkCore.PostgreSQL** package with specific configuration for Supabase compatibility. This includes handling connection string conversions and ensuring proper connection pooling settings.
                
                ## Supabase-Specific Features
                
                Supabase offers additional features like real-time subscriptions and Row Level Security (RLS). See the [Supabase Features documentation](https://supabase.com/docs/guides/database/overview).
                
                ## Security Best Practices
                
                - **Use SSL connections** (enabled by default with Ivy and Supabase)
                - **Implement Row Level Security** policies for user data isolation
                - **Use service role key** only for administrative operations
                - **Monitor database activity** through Supabase dashboard
                
                See [Supabase Row Level Security documentation](https://supabase.com/docs/guides/database/postgres/row-level-security) for implementation details.
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Connection Problems**
                
                - Verify your Supabase project is active
                - Use correct host URL and credentials
                
                **Other Issues**
                See [Supabase Connection Troubleshooting](https://supabase.com/docs/guides/database/connecting-to-postgres#troubleshooting-and-postgres-connection-string-faqs) for more troubleshooting steps.
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [Supabase Authentication](app://onboarding/cli/authentication/supabase)
                - [PostgreSQL Provider](app://onboarding/cli/database-integration/postgre-sql)
                - [Official Supabase Documentation](https://supabase.com/docs)
                - [Npgsql Entity Framework Core Provider](https://www.npgsql.org/efcore/)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.Authentication.SupabaseApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp)]; 
        return article;
    }
}

