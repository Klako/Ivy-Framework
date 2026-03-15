using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.GettingStarted;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/01_GettingStarted/04_HowIvyWorks.md", searchHints: ["architecture", "concepts", "websocket"])]
public class HowIvyWorksApp(bool onlyBody = false) : ViewBase
{
    public HowIvyWorksApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("how-ivy-works", "How Ivy Works", 1), new ArticleHeading("core-philosophy", "Core Philosophy", 2), new ArticleHeading("architecture-overview", "Architecture Overview", 2), new ArticleHeading("views--components", "Views & Components", 3), new ArticleHeading("reactive-state-management", "Reactive State Management", 3), new ArticleHeading("hook-guidelines", "Hook Guidelines", 3), new ArticleHeading("widget-library", "Widget Library", 3), new ArticleHeading("real-time-communication", "Real-time Communication", 3), new ArticleHeading("detailed-architecture", "Detailed Architecture", 2), new ArticleHeading("development-experience", "Development Experience", 2), new ArticleHeading("hot-reloading", "Hot Reloading", 3), new ArticleHeading("strongly-typed-everything", "Strongly Typed Everything", 3), new ArticleHeading("seamless-net-integration", "Seamless .NET Integration", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# How Ivy Works").OnLinkClick(onLinkClick)
            | Lead("Ivy lets you build modern web UIs using pure C# on the server, combining the familiarity of React patterns with the power of C# and .NET.")
            | new Markdown(
                """"
                ## Core Philosophy
                
                Ivy is a **server-side web framework** that brings React-like patterns to C#. Instead of wrestling with JavaScript, HTML, and CSS, you write everything in C# using a reactive, component-based architecture.
                
                **In production, you only work with the backend** - the React frontend is pre-built and embedded in the Ivy framework, so you don't need to manage frontend code, build processes, or deployment configurations. You write C# code, and Ivy handles the rest.
                
                ## Architecture Overview
                
                ### Views & Components
                
                Every Ivy app is built from **[Views](app://onboarding/concepts/views)** - C# classes that inherit from [ViewBase](app://onboarding/concepts/views). Each view implements a single `Build()` method that returns widgets or other views:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App(icon: Icons.Calendar)]
                public class TodoApp : ViewBase
                {
                    public override object? Build()
                    {
                        var newTitle = UseState("");
                        var todos = UseState(ImmutableArray.Create<Todo>());
                
                        return new Card()
                            .Title("My Todos")
                            .Description("What needs to be done?")
                            | Layout.Vertical(
                                // Input and Add button
                                Layout.Horizontal(
                                    newTitle.ToTextInput(placeholder: "New task..."),
                                    new Button("Add", onClick: _ => {
                                        todos.Set(todos.Value.Add(new Todo(newTitle.Value, false)));
                                        newTitle.Set("");
                                    })
                                ),
                                // Todo list
                                todos.Value.Select(todo => new TodoItem(todo))
                            );
                    }
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Reactive State Management
                
                Ivy provides React-inspired hooks for [state management](app://hooks/core/use-state):
                
                **Available Hooks:**
                
                - [UseState<T>()](app://hooks/core/use-state) - Local component state that triggers re-renders
                - [UseEffect()](app://hooks/core/use-effect) - Side effects with dependency tracking
                - [UseService<T>()](app://hooks/core/use-service) - Dependency injection integration
                - [UseSignal()](app://hooks/core/use-signal), [UseDownload()](app://hooks/core/use-download), `UseWebhook()` - And many more...
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public override object? Build()
                {
                    // State hook - triggers re-render when changed
                    var count = UseState(0);
                
                    // Effect hook - runs when count changes
                    UseEffect(() => {
                        Console.WriteLine($"Count changed to: {count.Value}");
                    }, count);
                
                    return new Button($"Count: {count.Value}",
                        onClick: _ => count.Set(count.Value + 1));
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Hook Guidelines
                
                Hooks rely on a strict call order to function correctly. Following these rules ensures that Ivy can properly track state between renders:
                
                1. **Call hooks at the top level** - Don't call hooks inside loops, conditions, or nested functions
                2. **Call hooks from Views only** - Hooks must be used inside the `Build()` method
                
                The **Ivy.Analyser** package automatically enforces these rules at compile time, catching violations before your code runs.
                
                For detailed examples and troubleshooting, see [Rules of Hooks](app://hooks/rules-of-hooks).
                
                ### Widget Library
                
                Ivy ships with a comprehensive set of strongly-typed [widgets](app://onboarding/concepts/widgets):
                
                | Category   | Examples                                                                   |
                | ---------- | -------------------------------------------------------------------------- |
                | Common     | `Button`, `Badge`, `Progress`, `Table`, `Card`, `Tooltip`, `Expandable`... |
                | Inputs     | `TextInput`, `NumberInput`, `BoolInput`, `DateTimeInput`, `FileInput`...   |
                | Primitives | `Text`, `Icon`, `Image`, `Markdown`, `Json`, `Code`, `Avatar`...           |
                | Layouts    | `Layout.Vertical()`, `GridLayout`, `TabsLayout`, `SidebarLayout`...        |
                | Effects    | `Animation`, `Confetti`...                                                 |
                | Charts     | `LineChart`, `BarChart`, `PieChart`, `AreaChart`...                        |
                | Advanced   | `Sheet`, `Chat`...                                                         |
                
                ### Real-time Communication
                
                The magic happens through WebSocket communication:
                
                **Key Steps:**
                
                1. **Initial Render**: Ivy builds your view tree and serializes it to JSON
                2. **WebSocket Transfer**: The widget tree is sent to the browser
                3. **React Frontend**: A pre-built React client renders the widgets as HTML
                4. **Event Handling**: User interactions trigger events sent back to C#
                5. **State Updates**: Ivy detects changes and re-renders only affected parts
                """").OnLinkClick(onLinkClick)
            | new Callout("When working with search results in the sidebar (both in Ivy Samples and Docs), you can **Ctrl + right click** on any item to open it as a separate app in a new window. This is handy for multitasking or developing multiple features simultaneously.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("## Detailed Architecture").OnLinkClick(onLinkClick)
            | new Callout("The following technical documentation is intended primarily for internal developers of the Ivy-Framework.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                For a comprehensive technical overview of Ivy's architecture, see:
                
                - **[Framework Design](https://github.com/Ivy-Interactive/Ivy-Framework/wiki/Framework-Design)** - Design system, theming, and UI framework choices
                - **[Backend Architecture](https://github.com/Ivy-Interactive/Ivy-Framework/wiki/Backend-Architecture)** - C# server configuration, application system, and deployment
                - **[Communication](https://github.com/Ivy-Interactive/Ivy-Framework/wiki/BE%E2%80%90FE-communication)** - SignalR protocol, message types, and state synchronization
                
                ## Development Experience
                
                ### Hot Reloading
                
                The development workflow is incredibly smooth:
                """").OnLinkClick(onLinkClick)
            | new Terminal()
                .AddCommand("ivy run")
                
            | new Markdown(
                """"
                That's it! Changes to your C# code instantly refresh the browser.
                
                ### Strongly Typed Everything
                
                No more runtime errors from typos in HTML/CSS:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Compile-time safety
                new Button("Click me")
                    .Variant(ButtonVariant.Primary)
                    .Icon(Icons.Plus)
                    .Size(Size.Large);
                
                // This won't compile
                new Button().Variant("invalid-variant"); // Compiler error!
                """",Languages.Csharp)
            | new Markdown("### Seamless .NET Integration").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public override object? Build()
                {
                    // Use any .NET library
                    var db = UseService<MyDbContext>();
                    var logger = UseService<ILogger<MyApp>>();
                
                    // Async operations work naturally
                    var data = await db.Users.ToListAsync();
                
                    return new Table(data)
                        .Columns(
                            col => col.Name,
                            col => col.Email,
                            col => col.CreatedAt.ToString("yyyy-MM-dd")
                        );
                }
                """",Languages.Csharp)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseServiceApp), typeof(Hooks.Core.UseSignalApp), typeof(Hooks.Core.UseDownloadApp), typeof(Hooks.RulesOfHooksApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}

