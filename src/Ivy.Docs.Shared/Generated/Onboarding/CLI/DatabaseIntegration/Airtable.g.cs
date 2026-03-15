using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.CLI.DatabaseIntegration;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/03_CLI/05_DatabaseIntegration/02_Airtable.md", searchHints: ["airtable", "database", "spreadsheet", "api", "cloud", "nocode"])]
public class AirtableApp(bool onlyBody = false) : ViewBase
{
    public AirtableApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("airtable-database-provider", "Airtable Database Provider", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("adding-a-database-connection", "Adding a Database Connection", 2), new ArticleHeading("creating-an-access-token", "Creating an Access Token", 2), new ArticleHeading("finding-your-base-id", "Finding Your Base ID", 2), new ArticleHeading("configuration", "Configuration", 2), new ArticleHeading("working-with-airtable-tables", "Working With Airtable Tables", 2), new ArticleHeading("airtable-specific-features", "Airtable-Specific Features", 2), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("common-issues", "Common Issues", 3), new ArticleHeading("related-documentation", "Related Documentation", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Airtable Database Provider").OnLinkClick(onLinkClick)
            | Lead("Connect your Ivy application to Airtable with automatic Entity Framework configuration for seamless integration with your Airtable bases.")
            | new Markdown(
                """"
                ## Overview
                
                Airtable is a cloud-based spreadsheet-database hybrid that combines the simplicity of a spreadsheet with the power of a database. Ivy provides integration with Airtable through Entity Framework Core, allowing you to leverage Airtable's flexible data organization in your applications.
                
                ## Adding a Database Connection
                
                To set up Airtable with Ivy, run the following command and choose `Airtable` when asked to select a DB provider:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy db add")
                
            | new Markdown(
                """"
                You will be asked to name your connection, then prompted for two pieces of information:
                
                1. **Access Token**: Your Airtable personal access token (PAT)
                2. **Base ID**: The ID of your Airtable base (starts with 'app')
                
                These values will be combined into a connection string and stored in [.NET user secrets](app://onboarding/concepts/secrets):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("BaseId=appXXXXXXXXXXXXXX;ApiKey=patXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",Languages.Text)
            | new Markdown(
                """"
                See [Database Overview](app://onboarding/cli/database-integration/database-overview) for more information on adding database connections to Ivy.
                
                ## Creating an Access Token
                
                Airtable uses personal access tokens (PATs) for authentication. You can generate a token from the [Personal access tokens](https://airtable.com/create/tokens) page in your Airtable Builder Hub. Be sure to enable the scopes `data.records:read` and `schema.bases:read`, and if writing to Airtable is needed for your project, `data.records:write` as well. No other scopes are currently used by Ivy. Then, give it access to the Airtable base(s) you wish to use or select "Add all resources."
                
                ![Airtable create personal access token page](/ivy/assets/airtable_create_pat.webp "Airtable create personal access token page")
                
                For detailed instructions, see the [Airtable personal access tokens documentation](https://airtable.com/developers/web/guides/personal-access-tokens).
                
                ## Finding Your Base ID
                
                To find your base ID, login to Airtable in your browser and visit the [Airtable API Reference](https://airtable.com/api). At the bottom of that page, you should see a list of bases your account has access to:
                
                ![Airtable API Reference](/ivy/assets/airtable_api_reference.webp "Airtable API Reference")
                
                Select the one you want your Ivy project to connect to. This will lead you to base-specific API documentation. You can find the base ID on the Introduction page. Look for "The ID of this base is..."
                
                ![Airtable Base-Specific API Reference, showing the chosen base's ID](/ivy/assets/airtable_base_id.webp "Airtable Base-Specific API Reference")
                
                ## Configuration
                
                Ivy automatically configures the **Ivy.Airtable.EFCore** package (an Ivy-specific fork of `Airtable.EFCore`) and imports the `Airtable.EFCore` and `AirtableApiClient` namespaces for Airtable connections. This allows you to interact with your Airtable bases using Entity Framework Core.
                
                ## Working With Airtable Tables
                
                Ivy maps Airtable tables to entity classes, handles Airtable's data types, and provides standard Entity Framework CRUD operations.
                
                ## Airtable-Specific Features
                
                Key features Ivy can leverage:
                
                - **Rich field types** (attachments, links, formulas)
                - **Record linking** for relationships
                  - _Disclaimer: links are currently exposed as raw record IDs instead of entity references_
                - **Views** for filtered data presentation
                
                See [Airtable's API Reference](https://airtable.com/developers/web/api/introduction) for more details on Airtable features.
                
                ## Troubleshooting
                
                ### Common Issues
                
                **Authentication Failed**
                
                - Verify token validity and permissions
                
                **Base Access Issues**
                
                - Confirm Base ID and account access
                
                **Rate Limiting**
                
                - For details on API limits, see [Airtable API Rate Limits](https://airtable.com/developers/web/api/rate-limits).
                
                ## Related Documentation
                
                - [Database Overview](app://onboarding/cli/database-integration/database-overview)
                - [SQLite Provider](app://onboarding/cli/database-integration/sq-lite)
                - [PostgreSQL Provider](app://onboarding/cli/database-integration/postgre-sql)
                - [Official Airtable API Documentation](https://airtable.com/developers/web/api/introduction)
                - [Airtable .NET API Client](https://github.com/ngocnicholas/airtable.net)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.DatabaseIntegration.DatabaseOverviewApp), typeof(Onboarding.CLI.DatabaseIntegration.SQLiteApp), typeof(Onboarding.CLI.DatabaseIntegration.PostgreSqlApp)]; 
        return article;
    }
}

