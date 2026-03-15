using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_RulesOfHooks.md", searchHints: ["rules", "hooks", "best-practices", "ivy-analyzer", "hook-rules", "conditional-hooks", "hook-order", "compile-time-validation"])]
public class RulesOfHooksApp(bool onlyBody = false) : ViewBase
{
    public RulesOfHooksApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("rules-of-hooks", "Rules of Hooks", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("the-rules", "The Rules", 2), new ArticleHeading("1-only-call-hooks-at-the-top-level", "1. Only Call Hooks at the Top Level", 3), new ArticleHeading("2-only-call-hooks-from-ivy-views", "2. Only Call Hooks from Ivy Views", 3), new ArticleHeading("why-these-rules-matter", "Why These Rules Matter", 2), new ArticleHeading("how-hook-order-works", "How Hook Order Works", 3), new ArticleHeading("common-violations-and-solutions", "Common Violations and Solutions", 2), new ArticleHeading("ivyhook001-hook-used-outside-valid-context", "IVYHOOK001: Hook used outside valid context", 3), new ArticleHeading("ivyhook002-conditional-hook-usage", "IVYHOOK002: Conditional Hook Usage", 3), new ArticleHeading("ivyhook003-hook-inside-a-loop", "IVYHOOK003: Hook inside a loop", 3), new ArticleHeading("ivyhook005-hook-not-at-top-of-method", "IVYHOOK005: Hook not at top of method", 3), new ArticleHeading("hook-detection", "Hook Detection", 2), new ArticleHeading("troubleshooting-guide", "Troubleshooting Guide", 2), new ArticleHeading("quick-reference", "Quick Reference", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Rules of Hooks").OnLinkClick(onLinkClick)
            | Lead("Ivy hooks (functions starting with `Use...`) are a powerful feature that lets you use [state](app://hooks/core/use-state) and other Ivy features. However, hooks rely on a strict call order to function correctly. Follow these rules to ensure your hooks work as expected.")
            | new Markdown(
                """"
                ## Overview
                
                Ivy hooks provide a way to add stateful logic and side effects to your [views](app://onboarding/concepts/views). To work correctly, hooks must be called in a consistent order on every render.
                
                Key principles:
                
                - **Consistent Call Order** - Hooks must be called in the same order on every render
                - **Top-Level Only** - Hooks can only be called at the top level of your component
                - **Valid Context** - Hooks can only be called from Views or other hooks
                - **Compile-Time Validation** - The Ivy.Analyser package enforces these rules automatically
                """").OnLinkClick(onLinkClick)
            | new Callout("The Ivy.Analyser package automatically validates hook usage at compile time, catching violations before your code runs. This helps prevent runtime errors and ensures your hooks work correctly.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## The Rules
                
                Ivy hooks follow two fundamental rules that ensure they work correctly:
                
                ### 1. Only Call Hooks at the Top Level
                
                **Don't call hooks inside loops, conditions, or nested functions.** Always use hooks at the top level of your component's `Build` method (or custom hook).
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TD
                    A["Component Render"] --> B{Where are hooks called?}
                
                    B --> C["Top level of Build()"]
                    B --> D["Inside if/else"]
                    B --> E["Inside loop"]
                    B --> F["Inside nested function"]
                
                    C --> G["Valid<br/>Same order every render<br/>Hooks work correctly"]
                    D --> H["Invalid<br/>Order may change<br/>IVYHOOK002 error"]
                    E --> I["Invalid<br/>Order may change<br/>IVYHOOK003 error"]
                    F --> J["Invalid<br/>May not execute<br/>IVYHOOK001 error"]
                
                    G --> K["Hooks preserve state<br/>Component works correctly"]
                    H --> L["Compile error<br/>Fix hook placement"]
                    I --> L
                    J --> L
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### 2. Only Call Hooks from Ivy Views
                
                **Don't call hooks from regular C# functions.** Hooks can only be called from:
                
                - Ivy [views](app://onboarding/concepts/views) (inside `Build` method)
                - Custom hooks (functions starting with `Use...`)
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TD
                    A["Want to use a hook?"] --> B{Where are you calling it?}
                
                    B --> C["Inside ViewBase.Build()"]
                    B --> D["Inside custom UseX hook"]
                    B --> E["Inside regular method"]
                    B --> F["Inside service class"]
                
                    C --> G["Valid<br/>Hooks work correctly"]
                    D --> G
                    E --> H["Invalid<br/>IVYHOOK001 error"]
                    F --> H
                
                    G --> I["Hook executes<br/>State preserved"]
                    H --> J["Compile error<br/>Move to View or custom hook"]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Why These Rules Matter
                
                Hooks rely on call order to preserve [state](app://hooks/core/use-state) between renders. When hooks are called in the same order every time, Ivy can correctly match each hook call with its stored state.
                
                ### How Hook Order Works
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant C as Component
                    participant H as Hook System
                    participant S as State Storage
                
                    Note over C,S: First Render
                    C->>H: UseState(0) - Hook #1
                    H->>S: Store state at index 0
                    C->>H: UseState("") - Hook #2
                    H->>S: Store state at index 1
                    C->>H: UseEffect(...) - Hook #3
                    H->>S: Store effect at index 2
                
                    Note over C,S: Second Render (same order)
                    C->>H: UseState(0) - Hook #1
                    H->>S: Retrieve state from index 0
                    C->>H: UseState("") - Hook #2
                    H->>S: Retrieve state from index 1
                    C->>H: UseEffect(...) - Hook #3
                    H->>S: Retrieve effect from index 2
                
                    Note over C,S: Third Render (different order - ERROR!)
                    C->>H: UseState("") - Hook #1 (wrong!)
                    H->>S: Retrieve from index 0 (wrong state!)
                    C->>H: UseState(0) - Hook #2 (wrong!)
                    H->>S: Retrieve from index 1 (wrong state!)
                    Note right of H: State mismatch!<br/>Component breaks
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Common Violations and Solutions
                
                ### IVYHOOK001: Hook used outside valid context
                
                This error occurs when you try to use a hook outside of a View's `Build` method or another hook.
                
                **Bad**: Hook in Regular Method
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public class MyService
                {
                    public void DoSomething()
                    {
                        var state = UseState(0); // Error! Not inside a View.
                    }
                }
                """",Languages.Csharp)
            | new Markdown("**Good**: Hook in View").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class MyView : ViewBase
                    {
                        public override object Build()
                        {
                            var state = UseState(0); // OK - inside ViewBase.Build()
                            return Layout.Vertical(
                                Text.P($"Value: {state.Value}"),
                                new Button("Increment", _ => state.Set(state.Value + 1))
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MyView())
            )
            | new Markdown(
                """"
                ### IVYHOOK002: Conditional Hook Usage
                
                This error occurs if a hook call is wrapped in an `if` statement. Hook calls must be unconditional.
                
                **Bad**: Conditional Hook
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public override object Build()
                {
                    if (condition) {
                        var state = UseState(0); // Error! Hook might not run.
                    }
                    return Layout.Vertical();
                }
                """",Languages.Csharp)
            | new Markdown("**Good**: Unconditional Hook").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ConditionalStateDemo : ViewBase
                    {
                        public override object Build()
                        {
                            // Always call the hook, handle logic afterwards
                            var state = UseState(0);
                            var condition = UseState(true);
                    
                            return Layout.Vertical(
                                Layout.Horizontal(
                                    new Button("Toggle Condition", _ => condition.Set(!condition.Value)),
                                    new Button("Increment", _ => state.Set(state.Value + 1))
                                ),
                                condition.Value
                                    ? Text.P($"State value: {state.Value}")
                                    : Text.P("Condition is false")
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ConditionalStateDemo())
            )
            | new Markdown(
                """"
                ### IVYHOOK003: Hook inside a loop
                
                Hooks cannot be called inside `for`, `foreach`, `while` loops. Each item in a loop needs its own component instance to use hooks correctly.
                
                **Bad**: Hook in Loop
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public override object Build()
                {
                    var items = UseState(new List<string> { "Item 1", "Item 2" });
                
                    // Error! Hook called multiple times with unpredictable order
                    foreach (var item in items.Value) {
                        var count = UseState(0); // IVYHOOK003: Hook inside a loop
                    }
                    return Layout.Vertical();
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Good**: Component per Item
                
                Each item gets its own component, allowing each to safely use hooks:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ItemListDemo : ViewBase
                    {
                        public override object Build()
                        {
                            var items = UseState(new List<string> { "Apple", "Banana", "Cherry" });
                            var newItem = UseState("");
                    
                            return Layout.Vertical(
                                Text.H3("Shopping List"),
                                Layout.Horizontal(
                                    newItem.ToTextInput().Placeholder("Add item..."),
                                    new Button("Add", _ => {
                                        if (!string.IsNullOrWhiteSpace(newItem.Value))
                                        {
                                            items.Set(items.Value.Append(newItem.Value).ToList());
                                            newItem.Set("");
                                        }
                                    })
                                ),
                                new Separator(),
                                Layout.Vertical(items.Value.Select((item, index) => new ShoppingItemView(item).Key($"{item}-{index}")).ToArray())
                            );
                        }
                    }
                    
                    public class ShoppingItemView : ViewBase
                    {
                        private readonly string _itemName;
                    
                        public ShoppingItemView(string itemName)
                        {
                            _itemName = itemName;
                        }
                    
                        public override object Build()
                        {
                            // Hook is called at top level - OK!
                            // Each ShoppingItemView instance has its own count state
                            var count = UseState(1);
                    
                            return Layout.Horizontal(
                                Text.P($"{_itemName} x {count.Value}"),
                                new Button("-", _ => count.Set(Math.Max(1, count.Value - 1))),
                                new Button("+", _ => count.Set(count.Value + 1))
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ItemListDemo())
            )
            | new Markdown(
                """"
                ### IVYHOOK005: Hook not at top of method
                
                Hooks must be called before any other statements (like `return`, `throw`, etc) to ensure they always run.
                
                **Bad**: Early Return Before Hook
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                public override object Build()
                {
                    if (User == null) return Text("Login required");
                
                    var state = UseState(0); // Error! This hook might not run.
                    return Layout.Vertical();
                }
                """",Languages.Csharp)
            | new Markdown("**Good**: Hooks First").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class EarlyReturnDemo : ViewBase
                    {
                        public override object Build()
                        {
                            // Always call hooks first
                            var state = UseState(0);
                            var user = UseState(() => (string?)null);
                    
                            // Then handle early returns
                            if (user.Value == null)
                                return Text.P("Login required");
                    
                            return Layout.Vertical(
                                Text.P($"Welcome, {user.Value}!"),
                                Text.P($"Count: {state.Value}"),
                                new Button("Increment", _ => state.Set(state.Value + 1))
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new EarlyReturnDemo())
            )
            | new Markdown(
                """"
                ## Hook Detection
                
                The analyzer automatically detects hooks by their naming convention:
                
                | Pattern | Valid | Examples |
                |---------|-------|----------|
                | Starts with `Use` + uppercase letter | Yes | `UseState`, `UseEffect`, `UseCustomHook`, `UseMyFeature` |
                | Starts with `Use` but lowercase | No | `useState`, `useEffect` |
                | Doesn't start with `Use` | No | `GetState`, `CreateEffect` |
                | `Use` but no uppercase after | No | `Use`, `Useless` |
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart LR
                    A[Hook Name] --> B{Pattern: Use + Uppercase?}
                    B -->|Yes| C[Valid Hook]
                    B -->|No| D[Invalid Hook]
                
                    C --> C1[UseState<br/>UseEffect<br/>UseReducer<br/>UseMemo]
                    D --> D1[useState<br/>Use<br/>GetState]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                **Valid Examples:**
                
                - [`UseState`](app://hooks/core/use-state), [`UseEffect`](app://hooks/core/use-effect), `UseCustomHook`, `UseMyFeature`, [`UseReducer`](app://hooks/core/use-reducer), [`UseMemo`](app://hooks/core/use-memo)
                
                **Invalid Examples:**
                
                - `useState` (lowercase 's')
                - `Use` (no uppercase after 'Use')
                - `Useless` (no uppercase after 'Use')
                - `GetState` (doesn't start with 'Use')
                
                This means any custom hooks you create following the `UseX` pattern will be automatically validated by the analyzer.
                
                ## Troubleshooting Guide
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TD
                    A["Hook error or unexpected behavior?"] --> B{What's the error?}
                
                    B --> C["IVYHOOK001<br/>Outside valid context"]
                    B --> D["IVYHOOK002<br/>Conditional hook"]
                    B --> E["IVYHOOK003<br/>Hook in loop"]
                    B --> F["IVYHOOK005<br/>Not at top"]
                    B --> G["State not updating"]
                
                    C --> C1["Move hook to ViewBase.Build()<br/>Or create custom hook<br/>Check method context"]
                    D --> D1["Remove if/else around hook<br/>Call hook unconditionally<br/>Move condition after hook"]
                    E --> E1["Extract to component<br/>Create ItemView class<br/>Call hook in component"]
                    F --> F1["Move hooks to top<br/>Before any returns<br/>Before any logic"]
                    G --> G1["Check hook order<br/>Verify same order every render<br/>Check for conditional calls<br/>Review state updates"]
                
                    C1 --> H["Problem solved?"]
                    D1 --> H
                    E1 --> H
                    F1 --> H
                    G1 --> H
                
                    H -->|Yes| I["Great! Your hooks are working correctly"]
                    H -->|No| J["Review hook rules<br/>Check analyzer errors<br/>Seek help in community"]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown("## Quick Reference").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart LR
                    A[Call Hook?] --> B{In View Build<br/>or custom hook?}
                    B -->|No| C[Don't Call]
                    B -->|Yes| D{At top level,<br/>same order,<br/>before conditionals?}
                    D -->|No| C
                    D -->|Yes| E[Call Hook]
                
                    C --> C1[Not in loop<br/>Not in if/else<br/>Not in nested function<br/>Not after return]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                **Rules Checklist:**
                
                - In [view](app://onboarding/concepts/views) Build method or custom hook
                - At top level of Build method
                - Same order every render
                - Before all conditional logic
                - Not in loops, conditionals, or nested functions
                
                ## See Also
                
                - [State Management](app://hooks/core/use-state) - Using UseState hook
                - [Effects](app://hooks/core/use-effect) - Using UseEffect hook
                - [Memoization](app://hooks/core/use-memo) - Using UseMemo and UseCallback
                - [Views](app://onboarding/concepts/views) - Understanding Ivy Views
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseReducerApp), typeof(Hooks.Core.UseMemoApp)]; 
        return article;
    }
}


public class MyView : ViewBase
{
    public override object Build()
    {
        var state = UseState(0); // OK - inside ViewBase.Build()
        return Layout.Vertical(
            Text.P($"Value: {state.Value}"),
            new Button("Increment", _ => state.Set(state.Value + 1))
        );
    }
}

public class ConditionalStateDemo : ViewBase
{
    public override object Build()
    {
        // Always call the hook, handle logic afterwards
        var state = UseState(0);
        var condition = UseState(true);
        
        return Layout.Vertical(
            Layout.Horizontal(
                new Button("Toggle Condition", _ => condition.Set(!condition.Value)),
                new Button("Increment", _ => state.Set(state.Value + 1))
            ),
            condition.Value 
                ? Text.P($"State value: {state.Value}")
                : Text.P("Condition is false")
        );
    }
}

public class ItemListDemo : ViewBase
{
    public override object Build()
    {
        var items = UseState(new List<string> { "Apple", "Banana", "Cherry" });
        var newItem = UseState("");
        
        return Layout.Vertical(
            Text.H3("Shopping List"),
            Layout.Horizontal(
                newItem.ToTextInput().Placeholder("Add item..."),
                new Button("Add", _ => {
                    if (!string.IsNullOrWhiteSpace(newItem.Value))
                    {
                        items.Set(items.Value.Append(newItem.Value).ToList());
                        newItem.Set("");
                    }
                })
            ),
            new Separator(),
            Layout.Vertical(items.Value.Select((item, index) => new ShoppingItemView(item).Key($"{item}-{index}")).ToArray())
        );
    }
}

public class ShoppingItemView : ViewBase
{
    private readonly string _itemName;
    
    public ShoppingItemView(string itemName)
    {
        _itemName = itemName;
    }
    
    public override object Build()
    {
        // Hook is called at top level - OK!
        // Each ShoppingItemView instance has its own count state
        var count = UseState(1);
        
        return Layout.Horizontal(
            Text.P($"{_itemName} x {count.Value}"),
            new Button("-", _ => count.Set(Math.Max(1, count.Value - 1))),
            new Button("+", _ => count.Set(count.Value + 1))
        );
    }
}

public class EarlyReturnDemo : ViewBase
{
    public override object Build()
    {
        // Always call hooks first
        var state = UseState(0);
        var user = UseState(() => (string?)null);
        
        // Then handle early returns
        if (user.Value == null) 
            return Text.P("Login required");
        
        return Layout.Vertical(
            Text.P($"Welcome, {user.Value}!"),
            Text.P($"Count: {state.Value}"),
            new Button("Increment", _ => state.Set(state.Value + 1))
        );
    }
}
