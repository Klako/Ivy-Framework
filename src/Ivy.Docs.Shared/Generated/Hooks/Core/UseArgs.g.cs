using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:13, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/13_UseArgs.md", searchHints: ["args", "useargs", "parameters", "route-parameters", "navigation-args", "component-args"])]
public class UseArgsApp(bool onlyBody = false) : ViewBase
{
    public UseArgsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("useargs", "UseArgs", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("how-args-work", "How Args Work", 2), new ArticleHeading("argument-serialization", "Argument Serialization", 3), new ArticleHeading("serialization-errors-handling", "Serialization Errors Handling", 3), new ArticleHeading("when-to-use-args", "When to Use Args", 2), new ArticleHeading("default-arguments", "Default Arguments", 3), new ArticleHeading("argument-based-routing", "Argument-Based Routing", 3), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseArgs").OnLinkClick(onLinkClick)
            | Lead("The `UseArgs` [hook](app://hooks/rules-of-hooks) provides access to arguments passed to a [component](app://onboarding/concepts/views), such as route parameters or navigation arguments.")
            | new Markdown(
                """"
                ## Overview
                
                The `UseArgs` [hook](app://hooks/rules-of-hooks) allows you to access component arguments:
                
                - **Navigation Arguments** - Retrieve arguments passed during [navigation](app://onboarding/concepts/navigation)
                - **Type Safety** - Strongly typed argument access with compile-time checking
                - **JSON Serialization** - Arguments are automatically serialized and deserialized
                - **Optional Arguments** - Returns null if arguments are not available
                """").OnLinkClick(onLinkClick)
            | new Callout("`UseArgs` is the primary way to pass data between [components](app://onboarding/concepts/views) during navigation. Arguments are serialized as JSON, making them perfect for passing simple data structures like records or DTOs.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Basic Usage
                
                Use `UseArgs<T>` to retrieve arguments passed during navigation:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public record UserArgs(int UserId, string Tab = "overview");
                var args = UseArgs<UserArgs>();
                """",Languages.Csharp)
            | new Markdown("## How Args Work").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant S as Source Component
                    participant N as Navigator
                    participant AC as AppContext
                    participant T as Target Component
                
                    Note over S,T: Navigation with Arguments
                    S->>N: Navigate(typeof(App), args)
                    N->>AC: Serialize args to JSON
                    AC->>AC: Store JSON in AppContext
                    AC->>T: Component loads
                    T->>AC: UseArgs<T>()
                    AC->>AC: Deserialize JSON to T
                    AC-->>T: Return args or null
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Argument Serialization
                
                Arguments are automatically serialized to JSON when passed and deserialized when accessed:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Arguments are serialized to JSON
                var args = new UserProfileArgs(123, "details");
                // Becomes: {"UserId":123,"Tab":"details"}
                
                // When UseArgs is called, JSON is deserialized back
                var receivedArgs = UseArgs<UserProfileArgs>();
                // Returns: UserProfileArgs { UserId = 123, Tab = "details" }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Serialization Errors Handling
                
                If arguments fail to serialize, ensure all properties are serializable and avoid circular references:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Good: All properties serialize
                public record GoodArgs(string Name, int Count);
                
                // Bad: Non-serializable property
                public record BadArgs(string Name, Action Callback);
                
                // Bad: Circular reference
                public class Parent { public Child Child { get; set; } }
                public class Child { public Parent Parent { get; set; } }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## When to Use Args
                
                | Use Args For | Use State/Context Instead For |
                |--------------|-------------------------------|
                | Navigation Data | Component State |
                | Deep Linking (URL parameters) | Shared Component Data |
                | Component Initialization | Complex Objects (circular refs) |
                | Simple Data Transfer | Real-time Updates |
                
                ### Default Arguments
                
                Provide default behavior when args are null:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public record ProductListArgs(string? Category = null, string? SortBy = null, int Page = 1);
                    
                    public class ArgsDefaultDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var args = UseArgs<ProductListArgs>();
                    
                            // Use defaults if args are null
                            var category = args?.Category ?? "all";
                            var sortBy = args?.SortBy ?? "name";
                            var page = args?.Page ?? 1;
                    
                            return Layout.Vertical()
                                | Text.H3("Product List")
                                | Text.Block($"Category: {category}")
                                | Text.Block($"Sort By: {sortBy}")
                                | Text.Block($"Page: {page}");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ArgsDefaultDemo())
            )
            | new Markdown(
                """"
                ### Argument-Based Routing
                
                Use arguments to determine which view to render:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public record AppArgs(string? View = null, int? UserId = null);
                    
                    public class MainView : ViewBase
                    {
                        public override object? Build()
                        {
                            var args = UseArgs<AppArgs>();
                    
                            return args?.View switch
                            {
                                "dashboard" => Text.H3("Dashboard"),
                                "settings" => Text.H3("Settings"),
                                "profile" => Text.H3($"Profile: User {args.UserId}"),
                                _ => Text.H3("Home")
                            };
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MainView())
            )
            | new Markdown(
                """"
                ## Best Practices
                
                - **Use records for arguments** - Immutable, value equality, and serialize well
                - **Provide default values** - Use `public record Args(string Query, int Page = 1)` for optional params
                - **Always handle null** - `UseArgs` returns null if no args were passed
                - **Keep arguments simple** - Only serializable types (no delegates, streams, or resources)
                - **Use descriptive names** - `UserProfileArgs` not `Args` or `Data`
                
                ## See Also
                
                - [Navigation](app://onboarding/concepts/navigation) - Programmatic navigation between components
                - [State](app://hooks/core/use-state) - Component state management
                - [Context](app://hooks/core/use-context) - Component-scoped data sharing
                - [Rules of Hooks](app://hooks/rules-of-hooks) - Understanding hook rules and best practices
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.RulesOfHooksApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.NavigationApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseContextApp)]; 
        return article;
    }
}


public record ProductListArgs(string? Category = null, string? SortBy = null, int Page = 1);

public class ArgsDefaultDemo : ViewBase
{
    public override object? Build()
    {
        var args = UseArgs<ProductListArgs>();
        
        // Use defaults if args are null
        var category = args?.Category ?? "all";
        var sortBy = args?.SortBy ?? "name";
        var page = args?.Page ?? 1;
        
        return Layout.Vertical()
            | Text.H3("Product List")
            | Text.Block($"Category: {category}")
            | Text.Block($"Sort By: {sortBy}")
            | Text.Block($"Page: {page}");
    }
}

public record AppArgs(string? View = null, int? UserId = null);

public class MainView : ViewBase
{
    public override object? Build()
    {
        var args = UseArgs<AppArgs>();
        
        return args?.View switch
        {
            "dashboard" => Text.H3("Dashboard"),
            "settings" => Text.H3("Settings"),
            "profile" => Text.H3($"Profile: User {args.UserId}"),
            _ => Text.H3("Home")
        };
    }
}
