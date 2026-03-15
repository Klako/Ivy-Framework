using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;
using Ivy.Core.Hooks;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:8, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/08_UseRef.md", searchHints: ["useref", "ref", "static", "mutable", "persistence", "hooks", "non-reactive", "timers", "subscriptions"])]
public class UseRefApp(bool onlyBody = false) : ViewBase
{
    public UseRefApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("useref", "UseRef", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("how-useref-works", "How UseRef Works", 2), new ArticleHeading("when-to-use-useref", "When to Use UseRef", 2), new ArticleHeading("useref-vs-usestate-vs-usememo", "UseRef vs UseState vs UseMemo", 3), new ArticleHeading("performance-considerations", "Performance Considerations", 2), new ArticleHeading("see-also", "See Also", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseRef").OnLinkClick(onLinkClick)
            | Lead("Store values that persist across re-renders without triggering updates, similar to React's useRef for holding mutable values that don't affect the [view](app://onboarding/concepts/views) lifecycle.")
            | new Markdown(
                """"
                ## Overview
                
                Key characteristics of `UseRef`:
                
                - **Non-Reactive Storage** - Values persist but don't trigger re-renders when changed
                - **Mutable References** - Perfect for storing timers, subscriptions, and other mutable objects
                - **Performance** - No dependency tracking or re-render overhead
                - **Persistence** - Values survive across [component](app://onboarding/concepts/views) re-renders
                """").OnLinkClick(onLinkClick)
            | new Callout("`UseRef` is ideal for storing mutable references that don't affect rendering, such as timers, subscriptions, DOM references, or previous [state](app://hooks/core/use-state) values for comparison.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("## Basic Usage").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicRefDemo : ViewBase
                    {
                        class Counter { public int Value = 0; }
                    
                        public override object? Build()
                        {
                            var renderCount = UseRef(() => new Counter());
                            var forceUpdate = UseState(0);
                    
                            // Increment without triggering re-render
                            renderCount.Value.Value++;
                    
                            return Layout.Vertical()
                                | new Button("Force Re-render", _ => forceUpdate.Set(forceUpdate.Value + 1))
                                | Text.P($"This component has rendered {renderCount.Value.Value} times")
                                | Text.P("(Note: The count increments on each render, but doesn't trigger re-renders)").Small();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicRefDemo())
            )
            | new Markdown("## How UseRef Works").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant C as Component
                    participant UR as UseRef Hook
                    participant S as Storage
                
                    Note over C,S: First Render
                    C->>UR: UseRef(initialValue)
                    UR->>S: Check if value exists
                    S-->>UR: No value found
                    UR->>S: Store initialValue
                    S-->>UR: Return stored value container
                    UR-->>C: Return IState<T> container
                
                    Note over C,S: Subsequent Render
                    C->>UR: UseRef(initialValue)
                    UR->>S: Get stored value
                    S-->>UR: Return cached container
                    UR-->>C: Return same container
                
                    Note over C,S: Value Mutation (External)
                    C->>UR: Direct mutation (ref.Value = newValue)
                    Note right of C: Value changed but<br/>NO re-render triggered
                    UR->>S: Value updated in storage
                    Note right of UR: Component continues<br/>with same render
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## When to Use UseRef
                
                | Use UseRef For | Use Other Hooks Instead |
                |----------------|-------------------------|
                | Mutable references (timers, subscriptions) | Value affects UI → UseState |
                | Tracking previous state values | Computed from other values → UseMemo |
                | Caching expensive initializations | Needs to trigger side effects → UseState + UseEffect |
                | Storing callback references | Simple constant → regular variable |
                
                ### UseRef vs UseState vs UseMemo
                
                | Hook | Triggers Re-render | Mutable | Use Case |
                |------|-------------------|---------|----------|
                | [`UseState`](app://hooks/core/use-state) | Yes | No | UI state that affects rendering |
                | [`UseMemo`](app://hooks/core/use-memo) | No | No | Expensive calculations |
                | `UseRef` | No | Yes | Mutable refs, timers, subscriptions |
                
                ## Performance Considerations
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Good: Timer reference (no re-render needed)
                var timerId = UseRef<Timer?>(null);
                
                // Good: Previous value tracking
                var previousCount = UseRef(0);
                
                // Bad: Value affects UI - use UseState instead
                var count = UseRef(0); // Won't trigger re-render!
                
                // Bad: Computed value - use UseMemo instead
                var total = UseRef(items.Sum()); // Won't update when items change!
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [State Management](app://hooks/core/use-state) - Reactive state with UseState
                - [Rules of Hooks](app://hooks/rules-of-hooks) - Understanding hook rules and best practices
                - [Effects](app://hooks/core/use-effect) - Side effects and cleanup
                - [Memoization](app://hooks/core/use-memo) - Performance optimization with UseMemo
                - [Callbacks](app://hooks/core/use-callback) - Memoized callback functions with UseCallback
                - [Views](app://onboarding/concepts/views) - Understanding Ivy views and components
                
                ## Examples
                """").OnLinkClick(onLinkClick)
            | new Expandable("Tracking Previous Values",
                Tabs( 
                    new Tab("Demo", new Box().Content(new PreviousValueDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class PreviousValueDemo : ViewBase
                        {
                            class PreviousValue { public int? Value = null; }
                            class Counter { public int Value = 0; }
                        
                            public override object? Build()
                            {
                                var count = UseState(0);
                                var previousValue = UseRef(() => new PreviousValue());
                                var renderCount = UseRef(() => new Counter());
                        
                                renderCount.Value.Value++;
                        
                                // Get the previous value before updating it
                                var previous = previousValue.Value.Value;
                                var delta = previous.HasValue
                                    ? count.Value - previous.Value
                                    : 0;
                        
                                // Update previous value for next render (in real app, use UseEffect)
                                previousValue.Value.Value = count.Value;
                        
                                return Layout.Vertical(
                                    Text.P($"Current: {count.Value}"),
                                    Text.P($"Previous: {previous?.ToString() ?? "None"}"),
                                    Text.P($"Delta: {delta}"),
                                    Text.P($"Renders: {renderCount.Value.Value}").Small(),
                                    Layout.Horizontal(
                                        new Button("+1", _ => count.Set(count.Value + 1)),
                                        new Button("+5", _ => count.Set(count.Value + 5)),
                                        new Button("Reset", _ => {
                                            count.Set(0);
                                            previousValue.Value.Value = null;
                                        })
                                    )
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Storing Mutable References",
                Tabs( 
                    new Tab("Demo", new Box().Content(new MutableReferenceDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class MutableReferenceDemo : ViewBase
                        {
                            class RenderTracker { public int Count = 0; public DateTime LastRender = DateTime.Now; }
                        
                            public override object? Build()
                            {
                                var count = UseState(0);
                                var tracker = UseRef(() => new RenderTracker());
                        
                                // Mutate ref value without triggering re-render
                                tracker.Value.Count++;
                                tracker.Value.LastRender = DateTime.Now;
                        
                                return Layout.Vertical(
                                    Text.H3($"Count: {count.Value}"),
                                    new {
                                        RenderCount = tracker.Value.Count.ToString(),
                                        LastRender = tracker.Value.LastRender.ToString("HH:mm:ss")
                                    }.ToDetails(),
                                    Text.P("Render tracker is stored in UseRef - it persists across re-renders but doesn't trigger them").Small(),
                                    new Button("Increment", _ => count.Set(count.Value + 1))
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseMemoApp), typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseCallbackApp)]; 
        return article;
    }
}


public class BasicRefDemo : ViewBase
{
    class Counter { public int Value = 0; }
    
    public override object? Build()
    {
        var renderCount = UseRef(() => new Counter());
        var forceUpdate = UseState(0);
        
        // Increment without triggering re-render
        renderCount.Value.Value++;
        
        return Layout.Vertical()
            | new Button("Force Re-render", _ => forceUpdate.Set(forceUpdate.Value + 1))
            | Text.P($"This component has rendered {renderCount.Value.Value} times")
            | Text.P("(Note: The count increments on each render, but doesn't trigger re-renders)").Small();
    }
}

public class PreviousValueDemo : ViewBase
{
    class PreviousValue { public int? Value = null; }
    class Counter { public int Value = 0; }
    
    public override object? Build()
    {
        var count = UseState(0);
        var previousValue = UseRef(() => new PreviousValue());
        var renderCount = UseRef(() => new Counter());
        
        renderCount.Value.Value++;
        
        // Get the previous value before updating it
        var previous = previousValue.Value.Value;
        var delta = previous.HasValue 
            ? count.Value - previous.Value 
            : 0;
        
        // Update previous value for next render (in real app, use UseEffect)
        previousValue.Value.Value = count.Value;
        
        return Layout.Vertical(
            Text.P($"Current: {count.Value}"),
            Text.P($"Previous: {previous?.ToString() ?? "None"}"),
            Text.P($"Delta: {delta}"),
            Text.P($"Renders: {renderCount.Value.Value}").Small(),
            Layout.Horizontal(
                new Button("+1", _ => count.Set(count.Value + 1)),
                new Button("+5", _ => count.Set(count.Value + 5)),
                new Button("Reset", _ => {
                    count.Set(0);
                    previousValue.Value.Value = null;
                })
            )
        );
    }
}

public class MutableReferenceDemo : ViewBase
{
    class RenderTracker { public int Count = 0; public DateTime LastRender = DateTime.Now; }
    
    public override object? Build()
    {
        var count = UseState(0);
        var tracker = UseRef(() => new RenderTracker());
        
        // Mutate ref value without triggering re-render
        tracker.Value.Count++;
        tracker.Value.LastRender = DateTime.Now;
        
        return Layout.Vertical(
            Text.H3($"Count: {count.Value}"),
            new { 
                RenderCount = tracker.Value.Count.ToString(),
                LastRender = tracker.Value.LastRender.ToString("HH:mm:ss")
            }.ToDetails(),
            Text.P("Render tracker is stored in UseRef - it persists across re-renders but doesn't trigger them").Small(),
            new Button("Increment", _ => count.Set(count.Value + 1))
        );
    }
}
