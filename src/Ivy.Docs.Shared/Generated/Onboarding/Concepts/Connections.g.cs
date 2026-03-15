using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:26, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/26_Connections.md", searchHints: ["connection", "integration", "external-service", "api", "database", "iconnection"])]
public class ConnectionsApp(bool onlyBody = false) : ViewBase
{
    public ConnectionsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("connections", "Connections", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("the-iconnection-interface", "The IConnection Interface", 2), new ArticleHeading("method-reference", "Method Reference", 3), new ArticleHeading("connection-types", "Connection Types", 2), new ArticleHeading("database-connections", "Database Connections", 3), new ArticleHeading("api-connections", "API Connections", 3), new ArticleHeading("connection-secrets", "Connection Secrets", 2), new ArticleHeading("registering-connections", "Registering Connections", 2), new ArticleHeading("automatic-registration", "Automatic Registration", 3), new ArticleHeading("manual-registration", "Manual Registration", 3), new ArticleHeading("using-connection-services", "Using Connection Services", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Connections").OnLinkClick(onLinkClick)
            | Lead("Ivy Connections provide a unified abstraction for integrating external data sources and services into your application — from databases to third-party APIs.")
            | new Markdown(
                """"
                ## Overview
                
                A **Connection** in Ivy represents any external resource your application communicates with:
                
                - **[Databases](app://onboarding/cli/database-integration/database-overview)** — [SQL Server](app://onboarding/cli/database-integration/sql-server), [PostgreSQL](app://onboarding/cli/database-integration/postgre-sql), [MySQL](app://onboarding/cli/database-integration/my-sql), [SQLite](app://onboarding/cli/database-integration/sq-lite), etc.
                - **Third-party APIs** — Payment gateways, messaging services, analytics
                - **[Cloud services](app://onboarding/cli/deployment/deployment-overview)** — [AWS](app://onboarding/cli/deployment/aws), [Azure](app://onboarding/cli/deployment/azure), [Google Cloud](app://onboarding/cli/deployment/gcp)
                - **Custom internal services** — Microservices, legacy systems
                
                Connections implement the `IConnection` interface, which provides a standardized way to:
                
                - Register required services in the [DI container](app://onboarding/concepts/program)
                - Expose metadata about the connection (name, type, entities)
                - Integrate with Ivy's [secrets management](app://onboarding/concepts/secrets)
                
                ## The IConnection Interface
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public interface IConnection
                {
                    string GetContext(string connectionPath);
                    string GetNamespace();
                    string GetName();
                    string GetConnectionType();
                    ConnectionEntity[] GetEntities();
                    void RegisterServices(IServiceCollection services);
                }
                
                public record ConnectionEntity(string Singular, string Plural);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Method Reference
                
                | Method | Description |
                |--------|-------------|
                | `GetContext` | Returns the connection's context for code generation |
                | `GetNamespace` | Returns the namespace where the connection is defined |
                | `GetName` | Returns the connection's display name |
                | `GetConnectionType` | Returns the type of connection (e.g., "Database", "API") |
                | `GetEntities` | Returns available entities (tables, resources) |
                | `RegisterServices` | Registers required services in the [DI container](app://onboarding/concepts/program) |
                
                ## Connection Types
                
                ### Database Connections
                
                Database connections are the most common type. They are automatically generated when you use the [Ivy CLI database commands](app://onboarding/cli/database-integration/database-overview):
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add --provider Postgres --name MyDatabase")
                
            | new Markdown(
                """"
                This generates a connection class that implements [IConnection](#the-iconnection-interface) along with Entity Framework context and entities. See the [Database Integration Guide](app://onboarding/cli/database-integration/database-overview) for details.
                
                ### API Connections
                
                You can create custom connections for any external API. Here's an example of a payment gateway connection:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class StripeConnection : IConnection, IHaveSecrets
                {
                    public string GetContext(string connectionPath) => string.Empty;
                
                    public string GetNamespace() => typeof(StripeConnection).Namespace!;
                
                    public string GetName() => "Stripe";
                
                    public string GetConnectionType() => "PaymentAPI";
                
                    public ConnectionEntity[] GetEntities() =>
                    [
                        new("Customer", "Customers"),
                        new("Payment", "Payments"),
                        new("Subscription", "Subscriptions")
                    ];
                
                    public void RegisterServices(IServiceCollection services)
                    {
                        services.AddSingleton<IStripeClient, StripeClient>();
                        services.AddScoped<IPaymentService, StripePaymentService>();
                    }
                
                    public Secret[] GetSecrets() =>
                    [
                        new("Stripe:SecretKey"),
                        new("Stripe:PublishableKey")
                    ];
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Connection Secrets
                
                Connections often require sensitive configuration like API keys or connection strings. Implement [IHaveSecrets](app://onboarding/concepts/secrets) alongside `IConnection` to declare required secrets:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class MyApiConnection : IConnection, IHaveSecrets
                {
                    // ... IConnection implementation ...
                
                    public Secret[] GetSecrets() =>
                    [
                        new("MyApi:ApiKey"),
                        new("MyApi:ApiSecret")
                    ];
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                This integrates with Ivy's [secrets management](app://onboarding/concepts/secrets) for compile-time validation.
                
                ## Registering Connections
                
                ### Automatic Registration
                
                Register all connections from your assembly automatically on the [Server](app://onboarding/concepts/program):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.AddConnectionsFromAssembly();
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                This scans the entry assembly for all classes implementing `IConnection` and calls their `RegisterServices` method.
                
                ### Manual Registration
                
                For more control, register connections manually with your [Server](app://onboarding/concepts/program):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                
                var stripeConnection = new StripeConnection();
                stripeConnection.RegisterServices(server.Services);
                
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Using Connection Services
                
                After registration, use connection services in your [views](app://onboarding/concepts/views):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class PaymentView : ViewBase
                {
                    public override object? Build()
                    {
                        var paymentService = UseService<IPaymentService>();
                        var payments = paymentService.GetRecentPayments();
                
                        return new DataTable<Payment>(payments);
                    }
                }
                """",Languages.Csharp)
            | new Markdown("The example uses [UseService](app://hooks/core/use-service) to resolve the connection's registered service and a [DataTable](app://widgets/advanced/data-table) to display the data.").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.SqlServerApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp), typeof(Onboarding.CLI.DatabaseIntegration.MySqlApp), typeof(Onboarding.CLI.DatabaseIntegration.SQLiteApp), typeof(Onboarding.CLI.Deployment.DeploymentOverviewApp), typeof(Onboarding.CLI.Deployment.AWSApp), typeof(Onboarding.CLI.Deployment.AzureApp), typeof(Onboarding.CLI.Deployment.GCPApp), typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseServiceApp), typeof(Widgets.Advanced.DataTableApp)]; 
        return article;
    }
}

