using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:10, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/10_Apps.md", searchHints: ["app", "attribute", "routing", "icon", "search", "deeplink", "metadata", "title"])]
public class AppsApp(bool onlyBody = false) : ViewBase
{
    public AppsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("apps--the-attribute", "Apps & The  Attribute", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("the-attribute", "The  Attribute", 2), new ArticleHeading("attribute-parameters", "Attribute Parameters", 3), new ArticleHeading("route-generation", "Route Generation", 2), new ArticleHeading("examples", "Examples", 3), new ArticleHeading("customizing-routes", "Customizing Routes", 3), new ArticleHeading("page-title", "Page Title", 2), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Apps & The  Attribute").OnLinkClick(onLinkClick)
            | Lead("The `[App]` attribute is the cornerstone of defining applications within the Ivy Framework. It transforms a standard C# class into a discoverable, routable, and feature-rich application component.")
            | new Markdown(
                """"
                ## Overview
                
                In Ivy, an "App" is a self-contained unit of functionality, typically represented by a class inheriting from [ViewBase](app://onboarding/concepts/views). To make this class recognized by the framework as an App, you must decorate it with the `[App]` attribute.
                
                This attribute provides essential metadata that the framework uses to:
                
                1. **Generate Routes**: Automatically creates URL routes for navigation.
                2. **Generate UI**: Populates [navigation](app://onboarding/concepts/navigation) menus, search results, and window titles.
                3. **Configure Behavior**: Controls visibility, ordering, and searchability.
                
                ## The  Attribute
                
                The `[App]` attribute is found in the `Ivy` namespace.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                
                [App(
                    title: "Product Catalog",
                    icon: Icons.ShoppingBag,
                    searchHints: ["store", "items", "inventory"]
                )]
                public class ProductsApp : ViewBase
                {
                    // ...
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Attribute Parameters
                
                | Parameter | Type | Default | Description |
                | :--- | :--- | :--- | :--- |
                | **`id`** | `string?` | `null` | A unique identifier for the app. If omitted, it is generated from the class name and namespace (see Route Generation below). |
                | **`title`** | `string?` | `null` | The human-readable title of the app. Used in window titles and navigation menus. Defaults to a readable version of the class name. |
                | **`icon`** | `Icons` | `Icons.None` | The [icon](app://widgets/primitives/icon) representing the app in the navigation bar and search results. Uses the `Icons` enum. |
                | **`description`** | `string?` | `null` | A brief description of the app's purpose. May be shown in tooltips or app listings. |
                | **`group`** | `string[]?` | `null` | Explicitly defines the navigation path. Overrides automatic generation from the namespace. |
                | **`isVisible`** | `bool` | `true` | Controls whether the app appears in automatically generated navigation menus. Set to `false` for hidden apps or internal tools. |
                | **`order`** | `int` | `0` | Controls the sorting order of the app within its group in the navigation menu. Lower numbers appear first. |
                | **`groupExpanded`** | `bool` | `false` | If `true`, the navigation group containing this app will be expanded by default. |
                | **`documentSource`** | `string?` | `null` | Specifies a source for documentation content, if the app is a documentation viewer. |
                | **`searchHints`** | `string[]?` | `null` | An array of keywords to improve discoverability in the global search. |
                
                ## Route Generation
                
                One of the most powerful features of the `[App]` attribute is automatic route generation. Ivy uses a convention-based approach to turn your C# class structure into clean, friendly URLs.
                
                The logic works as follows:
                
                1. **Namespace Parsing**: The framework looks for the segment `Apps` in your namespace.
                2. **Path Extension**: Anything *after* `Apps` in the namespace is treated as a folder path.
                3. **Kebab-Case Conversion**: CamelCase names are converted to kebab-case (e.g., `MyApp` -> `my-app`).
                
                ### Examples
                
                | Class Name | Full Namespace | Generated Route ID | URL |
                | :--- | :--- | :--- | :--- |
                | `DashboardApp` | `MyProject.Apps` | `dashboard-app` | `/dashboard-app` |
                | `UserProfile` | `MyProject.Apps.Settings` | `settings/user-profile` | `/settings/user-profile` |
                | `AuditLog` | `MyProject.Apps.Admin.Logs` | `admin/logs/audit-log` | `/admin/logs/audit-log` |
                
                ### Customizing Routes
                
                You can override the automatic generation using the `id` or `group` parameters, though sticking to conventions is recommended for consistency.
                
                ## Page Title
                
                The framework automatically updates the browser page title to reflect your current application route.
                
                When you define an app using the `[App]` attribute, the framework uses its `title` property to set the browser page title:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(title: "Dashboard")]
                public class DashboardApp : ViewBase { /* ... */ }
                """",Languages.Csharp)
            | new Markdown(
                """"
                If no title is specified, the framework generates one from the class name (e.g., `DashboardApp` -> "Dashboard").
                
                ## Best Practices
                
                * **Suffix with `App`**: It's common convention to name your app classes ending with `App` (e.g., `ProductsApp`), though the framework will automatically make the title readable (e.g., "Products").
                * **Use `searchHints`**: Add synonyms for your app's functionality to make it easier for users to find via the [Command Palette](app://onboarding/concepts/navigation) (Cmd/Ctrl+K).
                * **Organize with Namespaces**: Use namespaces to group related apps. This automatically creates a structured hierarchy in your navigation menu.
                
                ## Faq
                """").OnLinkClick(onLinkClick)
            | new Expandable("How do I create an Ivy App with the [App] attribute?",
                Vertical().Gap(4)
                | new Markdown("The `[App]` attribute marks a class as an Ivy application. Key parameters:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    [App(
                        title: "My App",           // Display name (optional, defaults to class name)
                        icon: Icons.Layout,        // Icon from the Icons enum (optional)
                        group: ["Category"],        // Navigation path/group (optional, array of strings)
                        description: "My app desc" // Description text (optional)
                    )]
                    public class MyApp : ViewBase
                    {
                        public override object? Build()
                        {
                            return Text.H1("Hello World");
                        }
                    }
                    """",Languages.Csharp)
                | new Markdown(
                    """"
                    **Key points:**
                    
                    - The class must inherit from `ViewBase` and override `Build()`
                    - `group` controls navigation grouping (e.g., `["Settings", "Advanced"]` creates nested groups)
                    - `icon` uses the `Icons` enum (e.g., `Icons.Settings`, `Icons.Users`, `Icons.Database`)
                    - All parameters are optional — `[App]` with no arguments is valid
                    - Other parameters: `id`, `isVisible`, `order`, `groupExpanded`, `documentSource`, `searchHints`
                    """").OnLinkClick(onLinkClick)
            )
            | new Expandable("What is the base class for Ivy apps?",
                Vertical().Gap(4)
                | new Markdown("All Ivy apps inherit from `ViewBase` and override `Build()`. Mark them with the `[App]` attribute. Both `ViewBase` and `[App]` are in the `Ivy` namespace (`using Ivy;`). There is no `AppBase` or `IClient` class — use `ViewBase` and `IClientProvider`.").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    using Ivy;
                    
                    [App]
                    public class MyApp : ViewBase
                    {
                        public override ViewSpec Build()
                        {
                            return Text.H1("Hello");
                        }
                    }
                    """",Languages.Csharp)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.NavigationApp), typeof(Widgets.Primitives.IconApp)]; 
        return article;
    }
}

