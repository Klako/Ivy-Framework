using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:9, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/09_Navigation.md", searchHints: ["navigation", "routing", "usenavigation", "navigate", "apps", "deeplink", "urls", "chrome", "navigation-args", "route"])]
public class NavigationApp(bool onlyBody = false) : ViewBase
{
    public NavigationApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("navigation", "Navigation", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("how-usenavigation-works", "How UseNavigation Works", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("getting-the-navigator", "Getting the Navigator", 3), new ArticleHeading("navigation-methods", "Navigation Methods", 3), new ArticleHeading("navigation-patterns", "Navigation Patterns", 2), new ArticleHeading("type-safe-navigation", "Type-Safe Navigation", 3), new ArticleHeading("navigation-with-arguments", "Navigation with Arguments", 3), new ArticleHeading("uri-based-navigation", "URI-Based Navigation", 3), new ArticleHeading("external-url-navigation", "External URL Navigation", 3), new ArticleHeading("navigation-helpers", "Navigation Helpers", 2), new ArticleHeading("integration-with-chrome-settings", "Integration with Chrome Settings", 3), new ArticleHeading("navigation-modes", "Navigation Modes", 3), new ArticleHeading("best-practices-and-common-patterns", "Best Practices and Common Patterns", 2), new ArticleHeading("type-safe-navigation", "Type-Safe Navigation", 3), new ArticleHeading("master-detail-navigation", "Master-Detail Navigation", 3), new ArticleHeading("conditional-navigation", "Conditional Navigation", 3), new ArticleHeading("memoized-navigation-callbacks", "Memoized Navigation Callbacks", 3), new ArticleHeading("troubleshooting", "Troubleshooting", 2), new ArticleHeading("app-not-found-error", "App Not Found Error", 3), new ArticleHeading("navigation-arguments-not-received", "Navigation Arguments Not Received", 3), new ArticleHeading("external-urls-not-opening", "External URLs Not Opening", 3), new ArticleHeading("performance-considerations", "Performance Considerations", 2), new ArticleHeading("usenavigation", "UseNavigation", 2), new ArticleHeading("basic-usage", "Basic Usage", 3), new ArticleHeading("how-navigation-works", "How Navigation Works", 3), new ArticleHeading("common-patterns", "Common Patterns", 3), new ArticleHeading("navigation-with-arguments", "Navigation with Arguments", 4), new ArticleHeading("external-url-navigation", "External URL Navigation", 4), new ArticleHeading("troubleshooting", "Troubleshooting", 3), new ArticleHeading("best-practices", "Best Practices", 3), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Navigation").OnLinkClick(onLinkClick)
            | Lead("The UseNavigation hook provides a powerful way to navigate between [apps](app://onboarding/concepts/apps) and [external URLs](app://onboarding/concepts/clients) in Ivy [applications](app://onboarding/concepts/apps), enabling seamless user experiences and [deep linking](#navigation-with-arguments) capabilities.")
            | new Markdown(
                """"
                ## Overview
                
                Navigation in Ivy is handled through the `UseNavigation()` hook, which returns an `INavigator` interface. This hook enables:
                
                - **App-to-App Navigation** - Navigate between different Ivy [apps](app://onboarding/concepts/apps) within your application
                - **External URL Navigation** - Open external URLs and resources
                - **Deep Linking** - Navigate to specific apps with deep linking parameters and [arguments](app://hooks/core/use-args)
                - **Type-Safe Navigation** - Navigate using strongly-typed app classes
                
                The navigation system is built on top of Ivy's [signal system](app://hooks/core/use-signal) and integrates seamlessly with the [Chrome](app://onboarding/concepts/chrome) framework for managing app lifecycle and routing.
                
                ## How UseNavigation Works
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TD
                    A[View Component] --> B[UseNavigation Hook]
                    B --> C[INavigator Interface]
                    C --> D{Navigation Type?}
                    D -->|Type-Safe| E[Navigate by Type]
                    D -->|URI-Based| F[Navigate by URI]
                    D -->|External| G[Open External URL]
                    E --> H[Chrome System]
                    F --> H
                    G --> I[Browser/External Handler]
                    H --> J[Target App]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Basic Usage
                
                ### Getting the Navigator
                
                Get the navigator in any [view](app://onboarding/concepts/views) and use it with [Button](app://widgets/common/button) or other widgets:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Navigation)]
                public class MyNavigationApp : ViewBase
                {
                    public override object? Build()
                    {
                        // Get the navigator instance
                        var navigator = UseNavigation();
                
                        return new Button("Navigate to Another App")
                            .OnClick(() => navigator.Navigate(typeof(AnotherApp)));
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Navigation Methods
                
                The `INavigator` interface provides two main navigation methods:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public interface INavigator
                {
                    // Navigate using app type (type-safe)
                    void Navigate(Type type, object? appArgs = null);
                
                    // Navigate using URI string (flexible)
                    void Navigate(string uri, object? appArgs = null);
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Navigation Patterns
                
                ### Type-Safe Navigation
                
                Navigate to [apps](app://onboarding/concepts/apps) using their class types for compile-time safety:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class DashboardApp : ViewBase
                {
                    public override object? Build()
                    {
                        var navigator = UseNavigation();
                
                        return Layout.Vertical(
                            new Button("Go to User Profile")
                                .OnClick(() => navigator.Navigate(typeof(UserProfileApp))),
                
                            new Button("Open Settings")
                                .OnClick(() => navigator.Navigate(typeof(SettingsApp))),
                
                            new Button("View Reports")
                                .OnClick(() => navigator.Navigate(typeof(ReportsApp)))
                        );
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Navigation with Arguments
                
                Pass data to target apps using strongly-typed arguments. Receive them in the target app with [UseArgs](app://hooks/core/use-args):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public record UserProfileArgs(int UserId, string Tab = "overview");
                
                // Navigate with arguments
                navigator.Navigate(typeof(UserProfileApp), new UserProfileArgs(123, "details"));
                
                // Receive arguments in target app
                public class UserProfileApp : ViewBase
                {
                    public override object? Build()
                    {
                        var args = UseArgs<UserProfileArgs>();
                        return Text.Heading($"User Profile: {args?.UserId}");
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### URI-Based Navigation
                
                Use URI strings for dynamic navigation scenarios:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Navigate using URI strings
                navigator.Navigate("app://dashboard");
                navigator.Navigate("app://users");
                navigator.Navigate("app://settings");
                
                // Dynamic navigation
                var appUri = $"app://{selectedAppName}";
                navigator.Navigate(appUri);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### External URL Navigation
                
                Open external websites and resources:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Open external URLs
                navigator.Navigate("https://docs.ivy-framework.com");
                navigator.Navigate("https://github.com/ivy-framework/ivy");
                navigator.Navigate("mailto:support@example.com");
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Navigation Helpers
                
                Create reusable navigation patterns:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public static class NavigationHelpers
                {
                    public static Action<string> UseLinks(this IView view)
                    {
                        var navigator = view.UseNavigation();
                        return uri => navigator.Navigate(uri);
                    }
                
                    public static Action UseBackNavigation(this IView view, string defaultApp = "app://dashboard")
                    {
                        var navigator = view.UseNavigation();
                        return () => navigator.Navigate(defaultApp);
                    }
                }
                
                // Usage
                var navigateToLink = UseLinks();
                var goBack = UseBackNavigation();
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Integration with Chrome Settings
                
                Navigation behavior can be configured through [Chrome](app://onboarding/concepts/chrome) settings in your [Program](app://onboarding/concepts/program):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class Program
                {
                    public static void Main(string[] args)
                    {
                        IvyApp.Run(args, app =>
                        {
                            app.UseChrome(ChromeSettings.Default()
                                .UseTabs(preventDuplicates: true) // Prevent duplicate tabs
                                .DefaultApp<DashboardApp>()       // Set default app
                            );
                        });
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Navigation Modes
                
                - **Tabs Mode**: Each navigation creates a new tab (default)
                - **Pages Mode**: Navigation replaces the current view
                - **Prevent Duplicates**: Avoid opening multiple tabs for the same app
                
                ## Best Practices and Common Patterns
                
                ### Type-Safe Navigation
                
                Prefer type-safe navigation over URI strings when possible:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Preferred: Type-safe navigation
                navigator.Navigate(typeof(UserProfileApp), new UserProfileArgs(userId));
                
                // Avoid: String-based navigation when type is known
                navigator.Navigate($"app://user-profile?userId={userId}");
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Master-Detail Navigation
                
                Navigate from list views to detail views using a [Table](app://widgets/common/table):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                return new Table<Item>(items)
                    .Column("Name", i => i.Name)
                    .OnRowClick(item =>
                        navigator.Navigate(typeof(ItemDetailApp), new ItemDetailArgs(item.Id))
                    );
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Conditional Navigation
                
                Navigate based on user permissions or [state](app://hooks/core/use-state):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var handleNavigation = UseCallback(() =>
                {
                    if (user.HasRole("Admin"))
                        navigator.Navigate(typeof(AdminPanelApp));
                    else
                        navigator.Navigate(typeof(UnauthorizedApp));
                }, user);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Memoized Navigation Callbacks
                
                Use [UseCallback](app://hooks/core/use-callback) to prevent unnecessary re-renders:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var navigateToUser = UseCallback((int userId) =>
                {
                    navigator.Navigate(typeof(UserProfileApp), new UserProfileArgs(userId));
                }, navigator);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Troubleshooting
                
                ### App Not Found Error
                
                Ensure your app has the [App](app://onboarding/concepts/apps) attribute:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.LayoutDashboard)]
                public class MyApp : ViewBase { }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Navigation Arguments Not Received
                
                Ensure argument types match exactly between source and target apps:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Source: navigator.Navigate(typeof(TargetApp), new MyArgs("value"));
                // Target: var args = UseArgs<MyArgs>(); // Same type
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### External URLs Not Opening
                
                Include the protocol in external URLs:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                navigator.Navigate("https://example.com"); // Correct
                navigator.Navigate("example.com"); // Incorrect - treated as app URI
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Performance Considerations
                
                - **Memoize Navigation Callbacks**: Use [UseCallback](app://hooks/core/use-callback) for navigation handlers to prevent unnecessary re-renders
                - **Lazy App Loading**: Apps are loaded on-demand when navigated to
                - **State Cleanup**: Navigation automatically handles cleanup of previous app [state](app://hooks/core/use-state)
                - **Memory Management**: The [Chrome](app://onboarding/concepts/chrome) system manages app lifecycle and memory usage
                
                ## UseNavigation
                
                The `UseNavigation` hook enables programmatic navigation:
                
                - **Type-Safe Navigation** - Navigate to [apps](app://onboarding/concepts/apps) using strongly-typed app classes
                - **URI-Based Navigation** - Navigate using URI strings for dynamic scenarios
                - **Navigation Arguments** - Pass data to target apps during navigation
                - **External URL Navigation** - Open external websites and resources
                
                ### Basic Usage
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var navigator = UseNavigation();
                
                // Navigate by URI
                navigator.Navigate("app://hooks/core/usestate");
                
                // Navigate by type
                navigator.Navigate(typeof(MyApp));
                
                // Navigate with arguments
                navigator.Navigate(typeof(MyApp), new MyArgs(123));
                """",Languages.Csharp)
            | new Markdown("### How Navigation Works").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart LR
                    A[UseNavigation] --> B[INavigator]
                    B --> C{Type?}
                    C -->|Type-Safe| D[Navigate by Type]
                    C -->|URI| E[Navigate by URI]
                    C -->|External| F[Open URL]
                    D --> G[Target App]
                    E --> G
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Common Patterns
                
                #### Navigation with Arguments
                
                Pass data to target apps using strongly-typed arguments. Receive them with [UseArgs](app://hooks/core/use-args) in the target app:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public record UserArgs(int UserId, string Tab = "overview");
                
                // Navigate with arguments
                var navigator = UseNavigation();
                navigator.Navigate(typeof(TargetApp), new UserArgs(123, "settings"));
                
                // Receive in target app
                var args = UseArgs<UserArgs>();
                """",Languages.Csharp)
            | new Markdown(
                """"
                #### External URL Navigation
                
                Open external websites and resources:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var navigator = UseNavigation();
                
                navigator.Navigate("https://docs.ivy.app");
                navigator.Navigate("mailto:support@example.com");
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Troubleshooting
                
                **App Not Found**: Ensure your app has the [App](app://onboarding/concepts/apps) attribute:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.LayoutDashboard)]
                public class MyApp : ViewBase { }
                """",Languages.Csharp)
            | new Markdown("**Arguments Not Received**: Ensure argument types match exactly between source and target:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Source: navigator.Navigate(typeof(TargetApp), new MyArgs("value"));
                // Target: var args = UseArgs<MyArgs>(); // Same type
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Best Practices
                
                - **Prefer type-safe navigation** - Use `Navigate(typeof(MyApp))` when target is known at compile time
                - **Use records for arguments** - Pass data with strongly-typed argument objects
                - **Include protocol for external URLs** - Always use `https://` or `mailto:` for external links
                - **Ensure apps have [App](app://onboarding/concepts/apps) attribute** - Target apps must be decorated with `[App]`
                
                ## See Also
                
                - [Chrome](app://onboarding/concepts/chrome)
                - [Apps](app://onboarding/concepts/apps)
                - [UseArgs](app://hooks/core/use-args)
                - [Views](app://onboarding/concepts/views)
                - [Signals](app://hooks/core/use-signal)
                - [State Management](app://hooks/core/use-state)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.ClientsApp), typeof(Hooks.Core.UseArgsApp), typeof(Hooks.Core.UseSignalApp), typeof(Onboarding.Concepts.ChromeApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Widgets.Common.ButtonApp), typeof(Onboarding.Concepts.ProgramApp), typeof(Widgets.Common.TableApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseCallbackApp)]; 
        return article;
    }
}

