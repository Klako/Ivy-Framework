using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/01_DatabaseOverview.md", searchHints: ["database", "entityframework", "sql", "connection", "integration", "db"])]
public class DatabaseOverviewApp(bool onlyBody = false) : ViewBase
{
    public DatabaseOverviewApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("database-overview", "Database Overview", 1), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("command-options", "Command Options", 3), new ArticleHeading("supported-database-providers", "Supported Database Providers", 2), new ArticleHeading("relational-databases", "Relational Databases", 3), new ArticleHeading("cloud-databases", "Cloud Databases", 3), new ArticleHeading("specialized-databases", "Specialized Databases", 3), new ArticleHeading("connection-configuration-details", "Connection Configuration Details", 2), new ArticleHeading("security-and-secrets-management", "Security and Secrets Management", 3), new ArticleHeading("environment-variables", "Environment Variables", 4), new ArticleHeading("connection-structure", "Connection Structure", 3), new ArticleHeading("entity-framework-integration", "Entity Framework Integration", 4), new ArticleHeading("multiple-database-connections", "Multiple Database Connections", 3), new ArticleHeading("troubleshooting", "Troubleshooting", 3), new ArticleHeading("examples", "Examples", 2), new ArticleHeading("default-schema", "Default schema", 2), new ArticleHeading("related-commands", "Related Commands", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Database Overview").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to various databases with automatic Entity Framework configuration for SQL Server, PostgreSQL, MySQL, SQLite, and more.")
            | new Markdown(
                """"
                The `ivy db` commands allow you to add and manage [database connections](app://onboarding/concepts/connections) in your Ivy project. Ivy supports a wide range of database providers and automatically generates the necessary Entity Framework configurations.
                
                ## Adding a Database Connection
                
                To add a database connection to your Ivy project, run:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown(
                """"
                When you run this command without specifying options, Ivy will guide you through an interactive setup:
                
                1. **Select Database Provider**: Choose from the available providers
                2. **Connection Name**: Enter a name for your connection (PascalCase recommended)
                3. **Connection String**: Provide the connection string or other information to let Ivy build it for you, depending on the provider.
                
                ### Command Options
                
                `--provider <PROVIDER>` or `-p <PROVIDER>` - Specify the database provider directly:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Postgres")
                
            | new Markdown(
                """"
                Available providers: `SqlServer`, `Postgres`, `MySql`, `MariaDb`, `Sqlite`, `Supabase`, `Airtable`, `Oracle`, `Spanner`, `ClickHouse`, `Snowflake`
                
                `--name <CONNECTION_NAME>` or `-n <CONNECTION_NAME>` - Specify the connection name in PascalCase:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --name MyDatabase")
                
            | new Markdown("`--connection-string <CONNECTION_STRING>` - Provide the connection string directly:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Postgres --connection-string YourConnectionString")
                
            | new Markdown("`--schema <SCHEMA>` - Specify the database schema (for providers that support it):").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Postgres --schema public")
                
            | new Markdown("`--verbose` or `-v` - Enable verbose output for detailed logging:").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --verbose")
                
            | new Markdown(
                """"
                ## Supported Database Providers
                
                Ivy supports the following database providers. Click on any provider for detailed setup instructions:
                
                ### Relational Databases
                
                - **[SQL Server](app://onboarding/cli/database-integration/sql-server)** - Microsoft's enterprise database
                - **[PostgreSQL](app://onboarding/cli/database-integration/postgre-sql)** - Advanced open-source database
                - **[MySQL](app://onboarding/cli/database-integration/my-sql)** - Popular open-source database
                - **[MariaDB](app://onboarding/cli/database-integration/maria-db)** - MySQL fork with enhanced features
                - **[SQLite](app://onboarding/cli/database-integration/sq-lite)** - Lightweight file-based database
                - **[Oracle](app://onboarding/cli/database-integration/oracle)** - Enterprise database system
                
                ### Cloud Databases
                
                - **[Supabase](app://onboarding/cli/database-integration/supabase)** - Open-source Firebase alternative with PostgreSQL
                - **[Google Spanner](app://onboarding/cli/database-integration/google-spanner)** - Globally distributed database
                - **[Snowflake](app://onboarding/cli/database-integration/snowflake)** - Cloud data platform
                
                ### Specialized Databases
                
                - **[ClickHouse](app://onboarding/cli/database-integration/click-house)** - Column-oriented database for analytics
                - **[Airtable](app://onboarding/cli/database-integration/airtable)** - Spreadsheet-database hybrid
                
                ## Connection Configuration Details
                
                Ivy automatically configures:
                
                - **Connection strings** stored securely using [.NET User Secrets](app://onboarding/concepts/secrets)
                - **Entity Framework Core** with the appropriate provider, and generated context and entity classes
                - **Ivy [connection](app://onboarding/concepts/connections)** to facilitate communication between Ivy apps and the database provider
                
                ### Security and Secrets Management
                
                Ivy automatically configures [.NET User Secrets](app://onboarding/concepts/secrets) for secure connection string storage:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("dotnet user-secrets list")
                
            | new Markdown(
                """"
                #### Environment Variables
                
                You can also use environment variables for connection strings (with the exception of SQLite connection strings):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                export ConnectionStrings__MY_DATABASE_CONNECTION_STRING="Host=localhost;Database=mydb;Username=user;Password=pass"
                """",Languages.Text)
            | new Markdown(
                """"
                ### Connection Structure
                
                Each database connection has its own a folder structure:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Connections/
                └── [ConnectionName]/
                    ├── [ConnectionName]Context.cs             # Entity Framework DbContext
                    ├── [ConnectionName]ContextFactory.cs      # DbContext factory
                    ├── [ConnectionName]Connection.cs          # Connection configuration
                    └── [EntityName].cs...                     # One or more generated entity classes
                """",Languages.Text)
            | new Markdown(
                """"
                #### Entity Framework Integration
                
                **Connection** - Ivy generates a class for each connection to facilitate communication with the database provider:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class MyDatabaseConnection : IConnection
                {
                    public string GetContext(string connectionPath)
                    {
                        var connectionFile = nameof(MyDatabaseConnection) + ".cs";
                        var contextFactoryFile = nameof(MyDatabaseContextFactory) + ".cs";
                        var files = Directory.GetFiles(connectionPath, "*.*", SearchOption.TopDirectoryOnly)
                            .Where(f => !f.EndsWith(connectionFile) && !f.EndsWith(contextFactoryFile))
                            .Select(File.ReadAllText)
                            .ToArray();
                        return string.Join(Environment.NewLine, files);
                    }
                
                    public string GetName() => nameof(MyDatabase);
                
                    public string GetNamespace() => typeof(MyDatabaseConnection).Namespace;
                
                    public ConnectionEntity[] GetEntities()
                    {
                        return typeof(MyDatabaseContext)
                            .GetProperties()
                            .Where(e => e.PropertyType.IsGenericType && e.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                            .Select(e => new ConnectionEntity(e.PropertyType.GenericTypeArguments[0].Name, e.Name))
                            .ToArray();
                    }
                
                    public void RegisterServices(IServiceCollection services)
                    {
                        services.AddSingleton<MyDatabaseContextFactory>();
                    }
                }
                """",Languages.Csharp)
            | new Markdown("**DbContext** - A DbContext class is also generated for each connection:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public partial class MyDatabaseContext : DbContext
                {
                    public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
                        : base(options)
                    {
                    }
                
                    public virtual DbSet<Employee> Employees { get; set; }
                
                    public virtual DbSet<Order> Orders { get; set; }
                
                    protected override void OnModelCreating(ModelBuilder modelBuilder)
                    {
                        modelBuilder.Entity<Employee>(entity =>
                        {
                            entity.Property(e => e.Id).ValueGeneratedNever();
                        });
                
                        modelBuilder.Entity<Order>(entity =>
                        {
                            entity.Property(e => e.Id).ValueGeneratedNever();
                        });
                
                        OnModelCreatingPartial(modelBuilder);
                    }
                
                    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
                }
                """",Languages.Csharp)
            | new Markdown("**DbContext Factory** - Finally, a context factory class is generated for dependency injection:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class MyDatabaseContextFactory(ServerArgs args) : IDbContextFactory<MyDatabaseContext>
                {
                    public MyDatabaseContext CreateDbContext()
                    {
                        var configuration = new ConfigurationBuilder()
                           .AddEnvironmentVariables()
                           .AddUserSecrets(Assembly.GetExecutingAssembly())
                           .Build();
                
                        var optionsBuilder = new DbContextOptionsBuilder<MyDatabaseContext>();
                
                        var connectionString = configuration.GetConnectionString("MY_DATABASE_CONNECTION_STRING");
                
                        if (string.IsNullOrWhiteSpace(connectionString))
                        {
                            throw new InvalidOperationException("Database connection string 'MY_DATABASE_CONNECTION_STRING' is not set.");
                        }
                
                        // Ivy will use the appropriate method for your chosen DB provider here
                        optionsBuilder.UseDbProvider(connectionString);
                
                        if (args.Verbose)
                        {
                            optionsBuilder
                                .EnableSensitiveDataLogging()
                                .LogTo(Console.WriteLine, LogLevel.Information);
                        }
                
                        return new MyDatabaseContext(optionsBuilder.Options);
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Multiple Database Connections
                
                You can add multiple database connections to a single project:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Postgres --name PrimaryDb")
                .AddCommand("ivy db add --provider Sqlite --name LogDb")
                .AddCommand("ivy db add --provider ClickHouse --name AnalyticsDb")
                
            | new Markdown(
                """"
                ### Troubleshooting
                
                **Connection String Issues** - Ensure the connection string format is correct for your provider, verify database server is running and accessible (if applicable), and check firewall settings and network connectivity.
                
                **Entity Framework Issues** - Ensure required NuGet packages are installed, verify .NET EF tools are installed: `dotnet tool install -g dotnet-ef`, and check for conflicting Entity Framework versions.
                
                **Authentication Issues** - Ensure you're logged in: `ivy login` and verify your Ivy account has the necessary permissions. See [Connections](app://onboarding/concepts/connections) for how Ivy uses connection classes in your app.
                
                ## Examples
                
                **Basic PostgreSQL Setup**
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Postgres --name MyProjectDb")
                
            | new Markdown("**SQL Server with Custom Schema**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider SqlServer --name InventoryDb --schema dbo")
                
            | new Markdown("**Supabase Integration**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Supabase --name UserDb")
                
            | new Markdown("**Multiple Databases**").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Postgres --name PrimaryDb")
                .AddCommand("ivy db add --provider Sqlite --name LogDb")
                .AddCommand("ivy db add --provider ClickHouse --name AnalyticsDb")
                
            | new Markdown(
                """"
                ## Default schema
                
                The ivy db add command includes a `--use-default-schema` parameter that automatically uses the database's default schema without prompting.
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider postgres --connection-string \"...\" --name MyDb --use-default-schema")
                .AddOutput("")
                
            | new Markdown(
                """"
                The default schemas for each database provider are:
                
                - `PostgreSQL/Supabase` – public
                - `SQL Server` – dbo
                - `Oracle` – Uses the connected username as the default schema
                - `ClickHouse` – default
                - `Snowflake` – PUBLIC
                """").OnLinkClick(onLinkClick)
            | new Callout("You cannot use both --schema and --use-default-schema parameters together. Choose one based on whether you want to specify a custom schema or use the database default.", icon:Icons.CircleAlert).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Related Commands
                
                - `ivy init` - Initialize a new Ivy project
                - `ivy auth add` - Add authentication providers
                - `ivy app create` - Create apps
                - `ivy deploy` - Deploy your project
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ConnectionsApp), typeof(Onboarding.CLI.DatabaseIntegration.SqlServerApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp), typeof(Onboarding.CLI.DatabaseIntegration.MySqlApp), typeof(Onboarding.CLI.DatabaseIntegration.MariaDbApp), typeof(Onboarding.CLI.DatabaseIntegration.SQLiteApp), typeof(Onboarding.CLI.DatabaseIntegration.OracleApp), typeof(Onboarding.CLI.DatabaseIntegration.SupabaseApp), typeof(Onboarding.CLI.DatabaseIntegration.GoogleSpannerApp), typeof(Onboarding.CLI.DatabaseIntegration.SnowflakeApp), typeof(Onboarding.CLI.DatabaseIntegration.ClickHouseApp), typeof(Onboarding.CLI.DatabaseIntegration.AirtableApp), typeof(Onboarding.Concepts.SecretsApp)]; 
        return article;
    }
}

