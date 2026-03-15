using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:14, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/14_Secrets.md", searchHints: ["configuration", "environment", "security", "credentials", "api-keys", "secrets"])]
public class SecretsApp(bool onlyBody = false) : ViewBase
{
    public SecretsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("secrets", "Secrets", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("ihavesecrets-interface", "IHaveSecrets Interface", 3), new ArticleHeading("secret-record", "Secret Record", 3), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("hierarchical-secret-names", "Hierarchical Secret Names", 3), new ArticleHeading("database-connections-with-built-in-secrets-declaration", "Database Connections with Built-in Secrets Declaration", 2), new ArticleHeading("generated-connection-classes", "Generated Connection Classes", 3), new ArticleHeading("connection-string-format", "Connection String Format", 3), new ArticleHeading("configuration-validation", "Configuration Validation", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Secrets").OnLinkClick(onLinkClick)
            | Lead("The Ivy Framework provides a comprehensive secrets management foundation that enables compile-time tracking of required application secrets, ensuring all necessary [configuration](app://onboarding/concepts/program) is in place before deployment.")
            | new Markdown(
                """"
                ## Overview
                
                The Ivy Framework now includes a robust foundation for secrets management through the introduction of the `IHaveSecrets` interface and `Secret` record. This infrastructure enables compile-time tracking of required application secrets, making it easier to validate that all necessary configuration is in place before deployment.
                
                ### IHaveSecrets Interface
                
                The `IHaveSecrets` interface is the foundation of Ivy's secrets management system. Any class that requires secrets should implement this interface:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public interface IHaveSecrets
                {
                    Secret[] GetSecrets();
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Secret Record
                
                The `Secret` record represents a required secret configuration:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("public record Secret(string Name);",Languages.Csharp)
            | new Markdown(
                """"
                ## Basic Usage
                
                To declare that your service requires secrets, implement the `IHaveSecrets` interface:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class MyService : IHaveSecrets
                {
                    public Secret[] GetSecrets()
                    {
                        return
                        [
                            new Secret("ApiKey"),
                            new Secret("ConnectionString"),
                            new Secret("OAuth:ClientSecret")
                        ];
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Hierarchical Secret Names
                
                Ivy supports hierarchical secret naming using colon-separated paths, which aligns with .NET configuration standards:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class ConfigurationService : IHaveSecrets
                {
                    public Secret[] GetSecrets()
                    {
                        return
                        [
                            new Secret("Database:ConnectionString"),
                            new Secret("Auth:Jwt:SecretKey"),
                            new Secret("External:PaymentGateway:ApiKey"),
                            new Secret("Monitoring:ApplicationInsights:InstrumentationKey")
                        ];
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Database Connections with Built-in Secrets Declaration
                
                [Database connections](app://onboarding/cli/database-integration/database-overview) automatically declare their required secrets when generated through the [Ivy CLI](app://onboarding/cli/_index). This integration ensures that your database [connection](app://onboarding/concepts/connections) strings are automatically included in secrets validation.
                
                ### Generated Connection Classes
                
                When you generate a [database connection](app://onboarding/cli/database-integration/database-overview) using the Ivy CLI, the generated connection class implements both `IConnection` and `IHaveSecrets`:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class MyDatabaseConnection : IConnection, IHaveSecrets
                {
                    // ... existing connection methods ...
                
                    public Secret[] GetSecrets()
                    {
                        return
                        [
                            new("ConnectionStrings:MyDatabase")
                        ];
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Connection String Format
                
                The connection string secret name follows the colon-separated format (`ConnectionStrings:ConnectionName`) for consistency with .NET configuration standards:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class UserDatabaseConnection : IConnection, IHaveSecrets
                {
                    public Secret[] GetSecrets()
                    {
                        return
                        [
                            new("ConnectionStrings:UserDatabase"),
                            new("ConnectionStrings:AnalyticsDatabase")
                        ];
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Configuration Validation
                
                Before deployment, you can validate that all required secrets are properly configured:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Example validation logic (implementation depends on your deployment pipeline)
                public void ValidateSecrets(IEnumerable<IHaveSecrets> services)
                {
                    var allSecrets = services
                        .SelectMany(s => s.GetSecrets())
                        .Select(s => s.Name)
                        .ToHashSet();
                }
                """",Languages.Csharp)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI._IndexApp), typeof(Onboarding.Concepts.ConnectionsApp)]; 
        return article;
    }
}

