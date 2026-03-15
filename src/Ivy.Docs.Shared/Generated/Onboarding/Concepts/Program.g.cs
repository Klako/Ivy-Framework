using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/01_Program.md", searchHints: ["startup", "configuration", "bootstrap", "server", "main", "entry-point"])]
public class ProgramApp(bool onlyBody = false) : ViewBase
{
    public ProgramApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("program", "Program", 1), new ArticleHeading("basic-structure", "Basic Structure", 2), new ArticleHeading("server-configuration", "Server Configuration", 2), new ArticleHeading("serverargs-properties", "ServerArgs Properties", 3), new ArticleHeading("adding-applications", "Adding Applications", 2), new ArticleHeading("from-assembly", "From Assembly", 3), new ArticleHeading("individual-apps", "Individual Apps", 3), new ArticleHeading("hot-reload", "Hot Reload", 2), new ArticleHeading("authentication", "Authentication", 2), new ArticleHeading("services-and-dependency-injection", "Services and Dependency Injection", 2), new ArticleHeading("environment-configuration", "Environment Configuration", 2), new ArticleHeading("environment-variables", "Environment Variables", 3), new ArticleHeading("configuration-sources", "Configuration Sources", 3), new ArticleHeading("production-configuration", "Production Configuration", 2), new ArticleHeading("https-redirection", "HTTPS Redirection", 3), new ArticleHeading("metadata", "Metadata", 3), new ArticleHeading("complete-examples", "Complete Examples", 2), new ArticleHeading("simple-application", "Simple Application", 3), new ArticleHeading("documentation-server", "Documentation Server", 3), new ArticleHeading("authentication-enabled-application", "Authentication-Enabled Application", 3), new ArticleHeading("production-ready-configuration", "Production-Ready Configuration", 3), new ArticleHeading("advanced-configuration", "Advanced Configuration", 2), new ArticleHeading("custom-content-builder", "Custom Content Builder", 3), new ArticleHeading("webapplication-builder-modifications", "WebApplication Builder Modifications", 3), new ArticleHeading("connection-management", "Connection Management", 3), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Program").OnLinkClick(onLinkClick)
            | Lead("Configure and bootstrap your Ivy [application](app://onboarding/concepts/apps) with dependency injection, [services](app://hooks/core/use-service), and middleware for production-ready deployment.")
            | new Markdown("The `Program.cs` file is the entry point for your Ivy application. It configures and starts the Ivy server using the `Server` class, which provides a fluent API for setting up apps, authentication, middleware, and other services.").OnLinkClick(onLinkClick)
            | new Callout("Want to try Ivy without a full project? You can run a [file-based app](app://onboarding/concepts/file-based-apps) from a single `.cs` file using `dotnet run YourFile.cs`.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Basic Structure
                
                Every Ivy application follows a similar startup pattern:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.UseCulture("en-US");
                server.UseHotReload();
                server.AddAppsFromAssembly();
                server.UseChrome();
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Server Configuration
                
                The `Server` class accepts optional `ServerArgs` for configuration:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Default configuration
                var server = new Server();
                
                // Custom configuration
                var server = new Server(new ServerArgs
                {
                    Port = 8080,
                    Verbose = true,
                    Browse = true,
                    Silent = false
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### ServerArgs Properties
                
                | Property | Type | Default | Description |
                |----------|------|---------|-------------|
                | `Port` | `int` | `5010` | Port number for the server |
                | `Verbose` | `bool` | `false` | Enable verbose logging |
                | `Browse` | `bool` | `false` | Automatically open browser on startup |
                | `Silent` | `bool` | `false` | Suppress startup messages |
                | `DefaultAppId` | `string?` | `null` | Set the default app to load |
                | `MetaTitle` | `string?` | `null` | HTML meta title |
                | `MetaDescription` | `string?` | `null` | HTML meta description |
                
                ## Adding Applications
                
                ### From Assembly
                
                The most common approach is to automatically discover apps from an assembly:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Discover apps from the calling assembly
                server.AddAppsFromAssembly();
                
                // Discover apps from a specific assembly
                server.AddAppsFromAssembly(typeof(MyApp).Assembly);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Individual Apps
                
                You can also add apps individually:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Add by type
                server.AddApp(typeof(MyApp));
                
                // Add by type and set as default
                server.AddApp(typeof(MyApp), isDefault: true);
                
                // Add using AppDescriptor
                server.AddApp(new AppDescriptor
                {
                    Id = "my-app",
                    Title = "My Application",
                    ViewFunc = (context) => new MyView(),
                    Path = ["Apps", "MyApp"],
                    IsVisible = true
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Hot Reload
                
                Enable hot reload for development:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("server.UseHotReload();",Languages.Csharp)
            | new Markdown(
                """"
                This automatically refreshes the browser when C# code changes during development.
                
                For more information about configuring the application chrome (sidebar, header, footer), see [Chrome](app://onboarding/concepts/chrome).
                
                ## Authentication
                """").OnLinkClick(onLinkClick)
            | new Callout("Use the `ivy auth add` command to automatically configure authentication providers in your project. This [CLI](app://onboarding/cli/_index) command will update your `Program.cs` and manage [secrets](app://onboarding/concepts/secrets) for you. See the [Authentication CLI documentation](app://onboarding/cli/authentication/authentication-overview) for details.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("Ivy supports various authentication providers:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Supabase authentication
                server.UseAuth<SupabaseAuthProvider>(c =>
                    c.UseEmailPassword().UseGoogle());
                
                // Auth0 authentication
                server.UseAuth<Auth0AuthProvider>(c =>
                {
                    c.Domain = "your-domain.auth0.com";
                    c.ClientId = "your-client-id";
                });
                
                // Microsoft Entra authentication
                server.UseAuth<MicrosoftEntraAuthProvider>();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Services and Dependency Injection
                
                Register services for dependency injection:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Register services
                server.Services.AddSingleton<IMyService, MyService>();
                server.Services.AddScoped<IRepository, Repository>();
                
                // Configure Entity Framework
                server.UseBuilder(builder =>
                {
                    builder.Services.AddDbContext<MyDbContext>(options =>
                        options.UseSqlServer(connectionString));
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Environment Configuration
                
                ### Environment Variables
                
                The server automatically reads configuration from environment variables:
                
                - `PORT` - Override the default port
                - `VERBOSE` - Enable verbose logging
                
                ### Configuration Sources
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                server.UseBuilder(builder =>
                {
                    builder.Configuration.AddJsonFile("appsettings.json");
                    builder.Configuration.AddEnvironmentVariables();
                    builder.Configuration.AddUserSecrets<Program>();
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Production Configuration
                
                ### HTTPS Redirection
                
                Enable HTTPS redirection for production:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                #if !DEBUG
                server.UseHttpRedirection();
                #endif
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Metadata
                
                Set HTML metadata for SEO:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                server.SetMetaTitle("My Ivy Application");
                server.SetMetaDescription("A powerful web application built with Ivy");
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Complete Examples
                
                ### Simple Application
                
                A minimal setup for development with hot reload enabled and basic chrome configuration.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.UseCulture("en-US");
                server.UseHotReload();
                server.AddAppsFromAssembly();
                server.UseChrome();
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Documentation Server
                
                A specialized configuration for documentation sites with custom chrome, version display, and page-based navigation.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.UseCulture("en-US");
                server.AddAppsFromAssembly(typeof(DocsServer).Assembly);
                server.UseHotReload();
                
                var version = typeof(Server).Assembly.GetName().Version!.ToString().EatRight(".0");
                server.SetMetaTitle($"Ivy Docs {version}");
                
                var chromeSettings = new ChromeSettings()
                    .Header(
                        Layout.Vertical().Padding(2)
                        | new IvyLogo()
                        | Text.Muted($"Version {version}")
                    )
                    .DefaultApp<IntroductionApp>()
                    .UsePages();
                
                server.UseChrome(() => new DefaultSidebarChrome(chromeSettings));
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Authentication-Enabled Application
                
                A basic setup with Supabase authentication configured for email/password and Google OAuth login.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.UseCulture("en-US");
                server.UseHotReload();
                server.AddAppsFromAssembly();
                server.UseChrome();
                server.UseAuth<SupabaseAuthProvider>(c =>
                    c.UseEmailPassword().UseGoogle());
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Production-Ready Configuration
                
                A comprehensive setup with conditional compilation, HTTPS redirection, metadata configuration, and dependency injection services for production deployment.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var server = new Server();
                server.UseCulture("en-US");
                
                #if !DEBUG
                server.UseHttpRedirection();
                #endif
                
                #if DEBUG
                server.UseHotReload();
                #endif
                
                server.AddAppsFromAssembly();
                server.UseChrome();
                
                server.SetMetaTitle("My Production App");
                server.SetMetaDescription("Enterprise application built with Ivy");
                
                // Configure services
                server.UseBuilder(builder =>
                {
                    builder.Services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
                
                    builder.Services.AddSingleton<IEmailService, EmailService>();
                });
                
                await server.RunAsync();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Advanced Configuration
                
                ### Custom Content Builder
                
                Configure a custom content builder to handle specialized content rendering and processing.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("server.UseContentBuilder(new CustomContentBuilder());",Languages.Csharp)
            | new Markdown(
                """"
                ### WebApplication Builder Modifications
                
                Extend the underlying WebApplication builder with custom middleware, services, and logging configuration.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                server.UseBuilder(builder =>
                {
                    // Add custom middleware
                    builder.Services.AddAuthentication();
                    builder.Services.AddAuthorization();
                
                    // Configure logging
                    builder.Logging.AddApplicationInsights();
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Connection Management
                
                Automatically discover and register SignalR connection classes for real-time communication features.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("server.AddConnectionsFromAssembly();",Languages.Csharp)
            | new Markdown(
                """"
                This automatically discovers and registers SignalR connection classes for real-time communication.
                
                ## Faq
                """").OnLinkClick(onLinkClick)
            | new Expandable("What namespace are Ivy types in?",
                Vertical().Gap(4)
                | new Markdown("All Ivy types are in the root `Ivy` namespace. There are no sub-namespaces. You only need:").OnLinkClick(onLinkClick)
                | new CodeBlock("using Ivy;",Languages.Csharp)
                | new Markdown(
                    """"
                    This single using statement gives you access to everything: `ViewBase`, `IState<T>`, `MetricView`, `MetricRecord`, chart views (`LineChartView`, `PieChartView`, `BarChartView`, `AreaChartView`), `DataTable`, `DataTableBuilder<T>`, layout helpers (`Layout.Vertical()`, `Layout.Horizontal()`), `Button`, `TextInput`, all input types, `Card`, `Dialog`, `Sheet`, `Tab`, `Icons`, `RefreshToken`, `IClientProvider`, `IBladeService`, `IConnection`, `IHaveSecrets`, and all other framework types.
                    
                    **Do NOT use sub-namespaces** like `Ivy.Views.Dashboards`, `Ivy.Widgets.DataTables`, `Ivy.Client`, `Ivy.Hooks`, `Ivy.Services`, or `Ivy.Apps`. These do not exist — the framework source code organizes files in subdirectories but all types use `namespace Ivy;`.
                    
                    Ivy projects include `<ImplicitUsings>enable</ImplicitUsings>` plus a global using for the project's own namespace, so typically the only explicit using you need is `using Ivy;` and `using Microsoft.EntityFrameworkCore;` (for database connections).
                    """").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Hooks.Core.UseServiceApp), typeof(Onboarding.Concepts.FileBasedAppsApp), typeof(Onboarding.Concepts.ChromeApp), typeof(Onboarding.CLI._IndexApp), typeof(Onboarding.Concepts.SecretsApp), typeof(Onboarding.CLI.Authentication.AuthenticationOverviewApp)]; 
        return article;
    }
}

