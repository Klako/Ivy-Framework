using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:6, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/06_UseCallback.md", searchHints: ["usecallback", "performance", "optimization", "callbacks", "usecallback", "memoization", "rendering", "event handlers"])]
public class UseCallbackApp(bool onlyBody = false) : ViewBase
{
    public UseCallbackApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("usecallback", "UseCallback", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("when-to-use-usecallback", "When to Use UseCallback", 2), new ArticleHeading("how-usecallback-works", "How UseCallback Works", 3), new ArticleHeading("memory-vs-speed-trade-offs", "Memory vs Speed Trade-offs", 3), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("see-also", "See Also", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseCallback").OnLinkClick(onLinkClick)
            | Lead("The `UseCallback` [hook](app://hooks/rules-of-hooks) memoizes callback functions, preventing unnecessary re-renders when callbacks are passed as props to [child components](app://onboarding/concepts/widgets) or used as dependencies in other [hooks](app://hooks/rules-of-hooks).")
            | new Markdown(
                """"
                ## Overview
                
                The `UseCallback` [hook](app://hooks/rules-of-hooks) provides a way to optimize callback functions in Ivy [applications](app://onboarding/concepts/apps):
                
                - **Stable Function References** - Returns the same function reference when [state](app://hooks/core/use-state) dependencies haven't changed
                - **Prevents Re-renders** - [Child components](app://onboarding/concepts/widgets) won't re-render unnecessarily when receiving memoized callbacks
                - **Stable Dependencies** - Ensures callbacks used in [`UseEffect`](app://hooks/core/use-effect) and other hooks have stable references
                """").OnLinkClick(onLinkClick)
            | new Callout("`UseCallback` memoizes the function reference itself, while [`UseMemo`](app://hooks/core/use-memo) memoizes the result of calling a function. The memoized callback is only executed when you invoke it.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("## Basic Usage").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class ParentView : ViewBase
                {
                    public override object? Build()
                    {
                        var count = UseState(0);
                        var multiplier = UseState(2);
                
                        // Memoize the callback to prevent child re-renders
                        var handleIncrement = UseCallback(() =>
                        {
                            count.Set(count.Value + 1);
                        }, count); // Only recreate when count changes
                
                        var handleReset = UseCallback(() =>
                        {
                            count.Set(0);
                        }); // No dependencies - callback never changes
                
                        return Layout.Vertical(
                            Text.Inline($"Count: {count.Value}"),
                            new ChildComponent(handleIncrement, handleReset),
                            new NumberInput("Multiplier", multiplier.Value, v => multiplier.Set(v))
                        );
                    }
                }
                """",Languages.Csharp)
            | new Markdown("## When to Use UseCallback").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TD
                    A["Need to optimize callbacks?"] --> B{What's the use case?}
                
                    B --> C["Callback passed to<br/>child component"]
                    B --> D["Callback used as<br/>dependency in UseEffect"]
                    B --> E["Event handler with<br/>expensive setup"]
                    B --> F["Callback in list items<br/>or repeated components"]
                
                    C --> G["Use UseCallback<br/>Prevents child re-renders"]
                    D --> H["Use UseCallback<br/>Prevents infinite loops"]
                    E --> I["Use UseCallback<br/>Avoids recreation overhead"]
                    F --> J["Use UseCallback<br/>Optimizes list performance"]
                
                    G --> K["Stable function reference<br/> Child won't re-render<br/> Better performance"]
                    H --> L["Stable dependency<br/> Effect runs correctly<br/> No infinite loops"]
                    I --> M[" Handler created once<br/> Less memory churn<br/> Faster renders"]
                    J --> N[" List items optimized<br/> Better scrolling<br/> Smoother UI"]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown("The `UseCallback` [hook](app://hooks/rules-of-hooks) memoizes callback functions and only recreates them when their [state](app://hooks/core/use-state) dependencies change.").OnLinkClick(onLinkClick)
            | new Callout("`UseCallback` hook stores only the most recent dependency values for comparison; older values are discarded.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("### How UseCallback Works").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant C as Component
                    participant CB as UseCallback Hook
                    participant S as UseState Storage
                
                    Note over C,S: First Render
                    C->>CB: UseCallback(() => handleClick(), [dep1, dep2])
                    CB->>S: UseState(() => new CallbackRef(callback, deps))
                    S-->>CB: Create new CallbackRef with callback
                    CB->>S: Store CallbackRef(callback, [dep1, dep2])
                    CB-->>C: Return callback function
                
                    Note over C,S: Subsequent Render (deps unchanged)
                    C->>CB: UseCallback(() => handleClick(), [dep1, dep2])
                    CB->>S: Get stored CallbackRef
                    S-->>CB: Return CallbackRef(cachedCallback, [dep1, dep2])
                    CB->>CB: AreDependenciesEqual([dep1, dep2], [dep1, dep2])
                    Note right of CB: Dependencies equal!<br/>Return same function reference
                    CB-->>C: Return cached callback (same reference)
                
                    Note over C,S: Subsequent Render (deps changed)
                    C->>CB: UseCallback(() => handleClick(), [dep1_new, dep2])
                    CB->>S: Get stored CallbackRef
                    S-->>CB: Return CallbackRef(oldCallback, [dep1, dep2])
                    CB->>CB: AreDependenciesEqual([dep1, dep2], [dep1_new, dep2])
                    Note right of CB: Dependencies changed!<br/>Create new function reference
                    CB->>S: Update CallbackRef(newCallback, [dep1_new, dep2])
                    CB-->>C: Return new callback function
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Memory vs Speed Trade-offs
                
                - **Function References**: `UseCallback` stores function references in memory. Consider the number of memoized callbacks:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Good: Small number of memoized callbacks
                var handleClick = UseCallback(() => DoSomething(), []);
                var handleSubmit = UseCallback(() => SubmitForm(), formData);
                
                // Caution: Many memoized callbacks might consume memory
                // Consider if all are necessary
                """",Languages.Csharp)
            | new Markdown("- **[State](app://hooks/core/use-state) Dependency Stability**: If state dependencies change frequently, callbacks will be recreated often, reducing the effectiveness of memoization:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Bad: Dependency changes on every render
                var config = new { threshold: 100 };
                var handleAction = UseCallback(() => DoAction(config), config);
                
                // Good: Stable dependency
                var threshold = UseState(100);
                var handleAction = UseCallback(() => DoAction(threshold.Value), threshold);
                """",Languages.Csharp)
            | new Markdown("- **Callback Complexity**: Simple callbacks may not benefit from memoization:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Unnecessary memoization for simple callback
                var handleClick = UseCallback(() => count.Set(count.Value + 1), count);
                
                // Consider direct inline for simple cases
                // Or memoize only if passed to many child components
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Best Practices
                
                - **Dependency Array**: Always specify the [state](app://hooks/core/use-state) dependencies that should trigger callback recreation
                - **Stable References**: Only include state values that actually affect the callback's behavior
                - **Avoid Over-Memoization**: Don't memoize simple callbacks that don't cause performance issues
                - **Combine with IMemoized**: Use `UseCallback` together with `IMemoized` [components](app://onboarding/concepts/views) for maximum optimization
                
                ## See Also
                
                - [Memoization](app://hooks/core/use-memo) - Caching computed values with UseMemo
                - [UseMemo](app://hooks/core/use-memo) - Memoizing function resultss
                - [Effects](app://hooks/core/use-effect) - Performing side effects with stable dependencies
                - [State Management](app://hooks/core/use-state) - Managing component state
                - [Rules of Hooks](app://hooks/rules-of-hooks) - Understanding hook rules and best practices
                - [UseRef](app://hooks/core/use-ref) - Storing stable references
                - [Views](app://onboarding/concepts/views) - Understanding Ivy views and components
                - [Widgets](app://onboarding/concepts/widgets) - Building UI components
                
                ## Examples
                """").OnLinkClick(onLinkClick)
            | new Expandable("Preventing Child Re-renders",
                new CodeBlock(
                    """"
                    public class TodoListView : ViewBase
                    {
                        public override object? Build()
                        {
                            var todos = UseState(new List<Todo>());
                            var filter = UseState("");
                    
                            // Memoize callbacks to prevent TodoItem re-renders
                            var handleToggle = UseCallback((int id) =>
                            {
                                todos.Set(todos.Value.Select(t =>
                                    t.Id == id ? t with { Completed = !t.Completed } : t
                                ).ToList());
                            }, todos);
                    
                            var handleDelete = UseCallback((int id) =>
                            {
                                todos.Set(todos.Value.Where(t => t.Id != id).ToList());
                            }, todos);
                    
                            var filteredTodos = UseMemo(() =>
                                todos.Value.Where(t =>
                                    t.Title.Contains(filter.Value, StringComparison.OrdinalIgnoreCase)
                                ).ToList(),
                                todos, filter
                            );
                    
                            return Layout.Vertical(
                                new TextInput("Filter", filter.Value, v => filter.Set(v)),
                                Layout.Vertical(
                                    filteredTodos.Select(todo =>
                                        new TodoItem(todo, handleToggle, handleDelete).Key(todo.Id)
                                    )
                                )
                            );
                        }
                    }
                    """",Languages.Csharp)
            )
            | new Expandable("Stable Dependencies for Effects",
                new CodeBlock(
                    """"
                    public class DataFetcherView : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = UseState<List<Item>?>(null);
                            var loading = UseState(false);
                            var searchTerm = UseState("");
                    
                            // Memoize the fetch function
                            var fetchData = UseCallback(async () =>
                            {
                                loading.Set(true);
                                try
                                {
                                    var result = await ApiService.SearchItems(searchTerm.Value);
                                    data.Set(result);
                                }
                                finally
                                {
                                    loading.Set(false);
                                }
                            }, searchTerm);
                    
                            // Use the memoized callback in an effect
                            UseEffect(async () =>
                            {
                                await fetchData();
                            }, fetchData); // Stable dependency prevents infinite loops
                    
                            return Layout.Vertical(
                                new TextInput("Search", searchTerm.Value, v => searchTerm.Set(v)),
                                loading.Value ? new Loading() : new ItemList(data.Value ?? new List<Item>())
                            );
                        }
                    }
                    """",Languages.Csharp)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.RulesOfHooksApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseMemoApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseRefApp)]; 
        return article;
    }
}

