using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:11, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/11_UseService.md", searchHints: ["dependency-injection", "di", "useservice", "services", "ioc", "container"])]
public class UseServiceApp(bool onlyBody = false) : ViewBase
{
    public UseServiceApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("useservice", "UseService", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("service-registration", "Service Registration", 3), new ArticleHeading("service-descriptions", "Service Descriptions", 3), new ArticleHeading("service-interfaces", "Service Interfaces", 3), new ArticleHeading("service-lifetime", "Service Lifetime", 3), new ArticleHeading("service-middleware", "Service Middleware", 3), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseService").OnLinkClick(onLinkClick)
            | Lead("Services in Ivy provide dependency injection and service management for clean application architecture.")
            | new Markdown(
                """"
                ## Overview
                
                The service system in Ivy supports:
                
                - Dependency injection
                - Service registration and [configuration](app://onboarding/concepts/program)
                - Service [lifecycle management](app://onboarding/concepts/views)
                - Scoped and singleton services
                - Service interfaces and implementations
                - Service [middleware](app://onboarding/concepts/program)
                
                ## Basic Usage
                
                Use the `UseService<T>()` hook to access registered services in your views:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ServiceExampleView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var message = UseState("Hello from service!");
                    
                            return Layout.Vertical()
                                | Text.P(message.Value)
                                | new Button("Show Toast",
                                    onClick: _ => client.Toast(message.Value, "Service Demo"));
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ServiceExampleView())
            )
            | new Markdown(
                """"
                ### Service Registration
                
                Register services in your [application startup](app://onboarding/concepts/program):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class Program
                {
                    public static void Main()
                    {
                        var server = new Server()
                            .UseService<IMyService, MyService>()
                            .UseService<IDataService, DataService>(ServiceLifetime.Singleton)
                            .UseService<IAuthService, AuthService>();
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Service Descriptions
                
                Services can provide custom descriptions by implementing the `IDescribableService` interface. Use `ServerDescriptionReader` to read environment-specific service descriptions from your [application](app://onboarding/concepts/apps).
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Implement IDescribableService for custom service descriptions
                public class MyService : IMyService, IDescribableService
                {
                    public string ToYaml()
                    {
                        return "Custom service description in YAML format";
                    }
                }
                
                // Read service descriptions with environment context
                var description = await ServerDescriptionReader.ReadAsync(
                    projectDirectory,
                    environment: "PRODUCTION"
                );
                """",Languages.Csharp)
            | new Markdown(
                """"
                The `ServiceDescription` class includes an optional `Description` property for better documentation of your services.
                
                ### Service Interfaces
                
                Define service interfaces for better abstraction:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public interface IDataService
                {
                    Task<IEnumerable<Data>> GetDataAsync();
                    Task<Data> GetDataByIdAsync(string id);
                    Task SaveDataAsync(Data data);
                }
                
                public class DataService : IDataService
                {
                    private readonly ILogger<DataService> _logger;
                
                    public DataService(ILogger<DataService> logger)
                    {
                        _logger = logger;
                    }
                
                    public async Task<IEnumerable<Data>> GetDataAsync()
                    {
                        _logger.LogInformation("Fetching data");
                        // Implementation
                    }
                
                    public async Task<Data> GetDataByIdAsync(string id)
                    {
                        _logger.LogInformation("Fetching data for id: {Id}", id);
                        // Implementation
                    }
                
                    public async Task SaveDataAsync(Data data)
                    {
                        _logger.LogInformation("Saving data");
                        // Implementation
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Service Lifetime
                
                Ivy supports different service lifetimes:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Singleton - Created once for the entire application
                .UseService<ICacheService, CacheService>(ServiceLifetime.Singleton)
                
                // Scoped - Created once per request
                .UseService<IDbContext, DbContext>(ServiceLifetime.Scoped)
                
                // Transient - Created each time requested
                .UseService<ILogger, Logger>(ServiceLifetime.Transient)
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Service Middleware
                
                Add middleware to services for cross-cutting concerns:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class LoggingServiceMiddleware : IServiceMiddleware
                {
                    private readonly ILogger _logger;
                
                    public LoggingServiceMiddleware(ILogger logger)
                    {
                        _logger = logger;
                    }
                
                    public async Task<T> ExecuteAsync<T>(Func<Task<T>> next)
                    {
                        _logger.LogInformation("Service method called");
                        try
                        {
                            return await next();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Service method failed");
                            throw;
                        }
                    }
                }
                
                // Register middleware
                .UseServiceMiddleware<LoggingServiceMiddleware>()
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Best Practices
                
                1. **Interface-based Design**: Always define interfaces for your services
                2. **Single Responsibility**: Each service should have a single, well-defined purpose
                3. **Dependency Injection**: Use constructor injection for dependencies
                4. **Service Lifetime**: Choose appropriate lifetimes for your services
                5. **Error Handling**: Implement proper error handling in services
                6. **Logging**: Use logging for important operations and errors
                7. **Testing**: Make services easily testable through interfaces
                
                ## Examples
                """").OnLinkClick(onLinkClick)
            | new Expandable("Simple Service Usage",
                Vertical().Gap(4)
                | new Markdown("Use a service to display notifications or interact with client features:").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public class SimpleServiceView : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                                var count = UseState(0);
                        
                                return Layout.Vertical()
                                    | Text.P($"Button clicked {count.Value} times")
                                    | new Button("Show Toast", onClick: _ =>
                                    {
                                        count.Set(count.Value + 1);
                                        client.Toast($"Notification #{count.Value}", "Service Demo");
                                    });
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new SimpleServiceView())
                )
            )
            | new Expandable("Using Multiple Services",
                Vertical().Gap(4)
                | new Markdown("Access multiple services in a single view to combine their functionality:").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public class MultiServiceView : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                                var message = UseState("Ready");
                                var count = UseState(0);
                        
                                return Layout.Vertical()
                                    | Text.P($"Last action: {message.Value}")
                                    | Text.P($"Total actions: {count.Value}")
                                    | (Layout.Horizontal()
                                        | new Button("Action 1", onClick: _ =>
                                        {
                                            count.Set(count.Value + 1);
                                            client.Toast("Action 1 executed", "Service Demo");
                                            message.Set("Action 1 completed");
                                        })
                                        | new Button("Action 2", onClick: _ =>
                                        {
                                            count.Set(count.Value + 1);
                                            client.Toast("Action 2 executed", "Service Demo");
                                            message.Set("Action 2 completed");
                                        }));
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new MultiServiceView())
                )
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ProgramApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.AppsApp)]; 
        return article;
    }
}


public class ServiceExampleView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var message = UseState("Hello from service!");
        
        return Layout.Vertical()
            | Text.P(message.Value)
            | new Button("Show Toast", 
                onClick: _ => client.Toast(message.Value, "Service Demo"));
    }
}

public class SimpleServiceView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var count = UseState(0);
        
        return Layout.Vertical()
            | Text.P($"Button clicked {count.Value} times")
            | new Button("Show Toast", onClick: _ => 
            {
                count.Set(count.Value + 1);
                client.Toast($"Notification #{count.Value}", "Service Demo");
            });
    }
}

public class MultiServiceView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var message = UseState("Ready");
        var count = UseState(0);
        
        return Layout.Vertical()
            | Text.P($"Last action: {message.Value}")
            | Text.P($"Total actions: {count.Value}")
            | (Layout.Horizontal()
                | new Button("Action 1", onClick: _ => 
                {
                    count.Set(count.Value + 1);
                    client.Toast("Action 1 executed", "Service Demo");
                    message.Set("Action 1 completed");
                })
                | new Button("Action 2", onClick: _ => 
                {
                    count.Set(count.Value + 1);
                    client.Toast("Action 2 executed", "Service Demo");
                    message.Set("Action 2 completed");
                }));
    }
}
