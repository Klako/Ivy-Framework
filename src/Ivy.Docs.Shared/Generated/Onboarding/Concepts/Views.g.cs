using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/02_Views.md", searchHints: ["components", "viewbase", "build", "render", "lifecycle", "composition"])]
public class ViewsApp(bool onlyBody = false) : ViewBase
{
    public ViewsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("views", "Views", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("the-viewbase-class", "The ViewBase Class", 3), new ArticleHeading("build-method", "Build Method", 3), new ArticleHeading("state-management-with-hooks", "State Management with Hooks", 3), new ArticleHeading("rules-of-hooks", "Rules of Hooks", 3), new ArticleHeading("state-initialization", "State Initialization", 3), new ArticleHeading("service-injection", "Service Injection", 2), new ArticleHeading("effects-and-side-effects", "Effects and Side Effects", 2), new ArticleHeading("view-composition", "View Composition", 3), new ArticleHeading("app-attribute", "App Attribute", 3), new ArticleHeading("sizing-views", "Sizing Views", 3), new ArticleHeading("advanced-patterns", "Advanced Patterns", 2), new ArticleHeading("conditional-rendering", "Conditional Rendering", 3), new ArticleHeading("dynamic-lists", "Dynamic Lists", 3), new ArticleHeading("simple-user-profile-example", "Simple User Profile Example", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Views
                
                Understand how Views work as the core building blocks of Ivy [apps](app://onboarding/concepts/apps), similar to React components but written entirely in C#.
                
                Views are the fundamental building blocks of Ivy apps. They are similar to React components, providing a way to encapsulate UI logic and [state management](app://hooks/core/use-state) in a reusable way. Every view inherits from `ViewBase` and implements a `Build()` method that returns the UI structure.
                
                ## Basic Usage
                
                Here's a simple view that displays a greeting:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    Text.P("Hello, World!")
                    """",Languages.Csharp)
                | new Box().Content(Text.P("Hello, World!"))
            )
            | new Markdown(
                """"
                ### The ViewBase Class
                
                All views inherit from the abstract `ViewBase` class, which provides:
                
                - **Build() method**: The core method that returns the UI structure
                - **Lifecycle management**: Automatic disposal and cleanup
                - **Hook access**: Built-in state management and effect [hooks](app://hooks/rules-of-hooks)
                - **Service injection**: Access to [application services](app://hooks/core/use-service)
                - **Context management**: Shared data between parent and child views
                
                ### Build Method
                
                The `Build()` method is the heart of every view. It can return:
                
                - [Widgets](app://onboarding/concepts/widgets) ([Button](app://widgets/common/button), [Card](app://widgets/common/card), Text, etc.)
                - Other Views (for composition)
                - [Layouts](app://onboarding/concepts/layout) (to arrange multiple elements)
                - Primitive types (strings, numbers)
                - Collections (arrays, lists)
                - `null` (to render nothing)
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FlexibleContentView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FlexibleContentView : ViewBase
                    {
                        public override object? Build()
                        {
                            var showContent = UseState(true);
                    
                            return Layout.Vertical()
                                | new Button($"{(showContent.Value ? "Hide" : "Show")} Content",
                                    onClick: _ => showContent.Set(!showContent.Value))
                                | (showContent.Value ? "This content can be toggled!" : null);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### State Management with Hooks
                
                Views use React-like hooks for state management. The most common hook is [UseState()](app://hooks/core/use-state):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CounterView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CounterView : ViewBase
                    {
                        public override object? Build()
                        {
                            var count = UseState(0);
                    
                            return new Card(
                                Layout.Vertical().Align(Align.Center).Gap(4)
                                    | Text.P($"{count.Value}")
                                    | (Layout.Horizontal().Gap(2).Align(Align.Center)
                                        | new Button("-", onClick: _ => count.Set(count.Value - 1))
                                        | new Button("Reset", onClick: _ => count.Set(0))
                                        | new Button("+", onClick: _ => count.Set(count.Value + 1)))
                            ).Title("Counter");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Rules of Hooks
                
                To ensure correct state management, Ivy hooks must follow specific rules.
                Read the full guide on **[Rules of Hooks](app://hooks/rules-of-hooks)** to learn more and troubleshoot common errors.
                
                ### State Initialization
                
                You can initialize state in multiple ways:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Direct value
                var count = UseState(0);
                
                // Lazy initialization (called only once)
                var expensiveData = UseState(() => ComputeExpensiveData());
                
                // State that doesn't trigger rebuilds
                var cache = UseState(new Dictionary<string, object>(), buildOnChange: false);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Service Injection
                
                Views can access application services using the [UseService<T>()](app://hooks/core/use-service) hook:
                """").OnLinkClick(onLinkClick)
            | new Box().Content(new Button("Show Toast",
    onClick: _ => client.Toast("Hello from service!", "Service Demo")))
            | new Markdown(
                """"
                ## Effects and Side Effects
                
                Use `UseEffect()` for [side effects](app://hooks/core/use-effect) like API calls, timers, or [subscriptions](app://hooks/core/use-signal):
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class TimerView : ViewBase
                    {
                        public override object? Build()
                        {
                            var time = UseState(DateTime.Now);
                    
                            // Update time every second
                            UseEffect(async () =>
                            {
                                while (true)
                                {
                                    await Task.Delay(1000);
                                    time.Set(DateTime.Now);
                                }
                            });
                    
                            return Text.P($"Current time: {time.Value:HH:mm:ss}");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new TimerView())
            )
            | new Markdown(
                """"
                ### View Composition
                
                Views can be composed together to create complex UIs:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    Layout.Vertical()
                        | Text.H2("Team Members")
                        | new Card(
                            Layout.Vertical()
                                | Text.H4("Alice Smith")
                                | Text.P("alice@example.com").Small().Color(Colors.Gray)
                                | new Badge("Admin").Secondary()
                        )
                        | new Card(
                            Layout.Vertical()
                                | Text.H4("Bob Johnson")
                                | Text.P("bob@example.com").Small().Color(Colors.Gray)
                                | new Badge("User").Secondary()
                        )
                        | new Card(
                            Layout.Vertical()
                                | Text.H4("Carol Brown")
                                | Text.P("carol@example.com").Small().Color(Colors.Gray)
                                | new Badge("Manager").Secondary()
                        )
                    """",Languages.Csharp)
                | new Box().Content(Layout.Vertical()
    | Text.H2("Team Members")
    | new Card(
        Layout.Vertical()
            | Text.H4("Alice Smith")
            | Text.P("alice@example.com").Small().Color(Colors.Gray)
            | new Badge("Admin").Secondary()
    )
    | new Card(
        Layout.Vertical()
            | Text.H4("Bob Johnson")
            | Text.P("bob@example.com").Small().Color(Colors.Gray)
            | new Badge("User").Secondary()
    )
    | new Card(
        Layout.Vertical()
            | Text.H4("Carol Brown")
            | Text.P("carol@example.com").Small().Color(Colors.Gray)
            | new Badge("Manager").Secondary()
    ))
            )
            | new Markdown(
                """"
                ### App Attribute
                
                To make a view available as an app, use the `[App]` attribute:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Home, title: "My App")]
                public class MyApp : ViewBase
                {
                    public override object? Build()
                    {
                        return Text.H1("Welcome to My App!");
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                The `[App]` attribute supports several properties:
                
                - `icon`: [Icon](app://widgets/primitives/icon) to display in [navigation](app://onboarding/concepts/navigation)
                - `title`: Display name (defaults to class name)
                - `group`: [Navigation](app://onboarding/concepts/navigation) path array for hierarchical organization
                - `isVisible`: Whether to show in navigation
                - `searchHints`: Alternative keywords for search discoverability
                - `order`: Sort order within group
                - `description`: Brief description of the app
                
                For enhanced search discoverability, use `searchHints` to provide alternative keywords:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.TextCursorInput,
                     group: ["Widgets", "Inputs"],
                     searchHints: ["password", "textarea", "search", "email"])]
                public class TextInputApp : ViewBase
                {
                    public override object? Build()
                    {
                        return Text.H1("Text Input Examples");
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Sizing Views
                
                Views (`ViewBase` subclasses) have `.Width()` and `.Height()` extension methods that automatically wrap the view in a layout:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Apply width directly — wraps in a layout under the hood
                new MyView().Width(Size.Fraction(0.5f))
                
                // You can also use WithLayout() explicitly
                new MyView().WithLayout().Width(Size.Fraction(0.5f))
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Note:** These methods return a `LayoutView`, not the original view type. This means you cannot chain view-specific methods after `.Width()` or `.Height()` — apply sizing as the last step, or use `WithLayout()` explicitly if you need further layout configuration.
                
                ## Advanced Patterns
                
                ### Conditional Rendering
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ConditionalView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ConditionalView : ViewBase
                    {
                        public override object? Build()
                        {
                            var isLoggedIn = UseState(false);
                    
                            return Layout.Vertical()
                                | new Button(isLoggedIn.Value ? "Logout" : "Login",
                                    onClick: _ => isLoggedIn.Set(!isLoggedIn.Value))
                                | (isLoggedIn.Value
                                    ? Text.Success("Welcome back!")
                                    : Text.Muted("Please log in to continue"));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Dynamic Lists").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TodoApp())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TodoApp : ViewBase
                    {
                        public override object? Build()
                        {
                            var todos = UseState(new List<string>());
                            var newTodo = UseState("");
                    
                            return Layout.Vertical()
                                | new Card(
                                    Layout.Vertical()
                                        | (Layout.Horizontal()
                                            | newTodo.ToTextInput(placeholder: "Add a todo...").Width(Size.Grow())
                                            | new Button("Add", onClick: _ => {
                                                if (!string.IsNullOrWhiteSpace(newTodo.Value))
                                                {
                                                    todos.Set([..todos.Value, newTodo.Value]);
                                                    newTodo.Set("");
                                                }
                                            }).Icon(Icons.Plus))
                                        | todos.Value.Select((todo, index) =>
                                            Layout.Horizontal()
                                                | Text.Literal(todo).Width(Size.Grow())
                                                | new Button("Remove", onClick: _ => {
                                                    var list = todos.Value.ToList();
                                                    list.RemoveAt(index);
                                                    todos.Set(list);
                                                }).Icon(Icons.Trash).Variant(ButtonVariant.Outline).Small()
                                        )
                                ).Title("Todo List");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Simple User Profile Example").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Card(
    Layout.Vertical()
            | new Avatar("John Doe", "JD")
        | (Layout.Horizontal()
            | Text.P("John Doe")
            | Text.P("42 posts"))
        | new Button("Follow")
            .Variant(ButtonVariant.Primary)
            .Width(Size.Full())
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Card(
                        Layout.Vertical()
                                | new Avatar("John Doe", "JD")
                            | (Layout.Horizontal()
                                | Text.P("John Doe")
                                | Text.P("42 posts"))
                            | new Button("Follow")
                                .Variant(ButtonVariant.Primary)
                                .Width(Size.Full())
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseServiceApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Common.ButtonApp), typeof(Widgets.Common.CardApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseSignalApp), typeof(Widgets.Primitives.IconApp), typeof(Onboarding.Concepts.NavigationApp)]; 
        return article;
    }
}


public class FlexibleContentView : ViewBase
{
    public override object? Build()
    {
        var showContent = UseState(true);
        
        return Layout.Vertical()
            | new Button($"{(showContent.Value ? "Hide" : "Show")} Content", 
                onClick: _ => showContent.Set(!showContent.Value))
            | (showContent.Value ? "This content can be toggled!" : null);
    }
}

public class CounterView : ViewBase
{
    public override object? Build()
    {
        var count = UseState(0);
        
        return new Card(
            Layout.Vertical().Align(Align.Center).Gap(4)
                | Text.P($"{count.Value}")
                | (Layout.Horizontal().Gap(2).Align(Align.Center)
                    | new Button("-", onClick: _ => count.Set(count.Value - 1))
                    | new Button("Reset", onClick: _ => count.Set(0))
                    | new Button("+", onClick: _ => count.Set(count.Value + 1)))
        ).Title("Counter");
    }
}

public class TimerView : ViewBase
{
    public override object? Build()
    {
        var time = UseState(DateTime.Now);
        
        // Update time every second
        UseEffect(async () =>
        {
            while (true)
            {
                await Task.Delay(1000);
                time.Set(DateTime.Now);
            }
        });
        
        return Text.P($"Current time: {time.Value:HH:mm:ss}");
    }
}

public class ConditionalView : ViewBase
{
    public override object? Build()
    {
        var isLoggedIn = UseState(false);
        
        return Layout.Vertical()
            | new Button(isLoggedIn.Value ? "Logout" : "Login", 
                onClick: _ => isLoggedIn.Set(!isLoggedIn.Value))
            | (isLoggedIn.Value 
                ? Text.Success("Welcome back!")
                : Text.Muted("Please log in to continue"));
    }
}

public class TodoApp : ViewBase
{
    public override object? Build()
    {
        var todos = UseState(new List<string>());
        var newTodo = UseState("");
        
        return Layout.Vertical()
            | new Card(
                Layout.Vertical()
                    | (Layout.Horizontal()
                        | newTodo.ToTextInput(placeholder: "Add a todo...").Width(Size.Grow())
                        | new Button("Add", onClick: _ => {
                            if (!string.IsNullOrWhiteSpace(newTodo.Value))
                            {
                                todos.Set([..todos.Value, newTodo.Value]);
                                newTodo.Set("");
                            }
                        }).Icon(Icons.Plus))
                    | todos.Value.Select((todo, index) => 
                        Layout.Horizontal()
                            | Text.Literal(todo).Width(Size.Grow())
                            | new Button("Remove", onClick: _ => {
                                var list = todos.Value.ToList();
                                list.RemoveAt(index);
                                todos.Set(list);
                            }).Icon(Icons.Trash).Variant(ButtonVariant.Outline).Small()
                    )
            ).Title("Todo List");
    }
}
