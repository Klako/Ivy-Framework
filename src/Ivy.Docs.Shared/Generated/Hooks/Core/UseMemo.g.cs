using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/05_UseMemo.md", searchHints: ["performance", "optimization", "caching", "usememo", "usecallback", "rendering", "memo"])]
public class UseMemoApp(bool onlyBody = false) : ViewBase
{
    public UseMemoApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("usememo", "UseMemo", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("choosing-the-right-memoization-approach", "Choosing the Right Memoization Approach", 2), new ArticleHeading("how-usememo-works", "How UseMemo Works", 3), new ArticleHeading("when-to-use-memoization", "When to Use Memoization", 3), new ArticleHeading("component-memoization-with-imemoized", "Component Memoization with IMemoized", 2), new ArticleHeading("how-imemoized-works", "How IMemoized Works", 3), new ArticleHeading("imemoized-basic-usage", "IMemoized Basic Usage", 3), new ArticleHeading("imemoized-component-lifecycle", "IMemoized Component Lifecycle", 3), new ArticleHeading("best-practices-for-imemoized", "Best Practices for IMemoized", 3), new ArticleHeading("common-pitfalls-and-solutions", "Common Pitfalls and Solutions", 2), new ArticleHeading("unstable-dependencies", "Unstable Dependencies", 3), new ArticleHeading("callback-dependencies-issues", "Callback Dependencies Issues", 3), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("see-also", "See Also", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseMemo").OnLinkClick(onLinkClick)
            | Lead("Memoization helps Ivy [applications](app://onboarding/concepts/apps) run faster by caching results of expensive computations and preventing unnecessary re-renders in your [views](app://onboarding/concepts/views).")
            | new Markdown(
                """"
                ## Overview
                
                Memoization in Ivy provides several powerful tools to optimize performance:
                
                - **[`UseMemo`](#usememo-hook)** - Caches the result of expensive computations
                - **[`UseCallback`](app://hooks/core/use-callback)** - Memoizes callback functions to prevent unnecessary re-renders.
                - **`IMemoized`** - Interface for component-level memoization
                
                These [hooks](app://hooks/rules-of-hooks) work similarly to their React counterparts (`useMemo`, `useCallback`) but are designed specifically for Ivy's architecture.
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ExpensiveCalculationView : ViewBase
                    {
                        public override object? Build()
                        {
                            var input = UseState(0);
                    
                            // Memoize the result of an expensive calculation
                            var result = UseMemo(() =>
                            {
                                return input.Value * input.Value;
                            }, input.Value); // Only recompute when input changes
                    
                            return Layout.Vertical()
                                | input.ToNumberInput().Placeholder("Number")
                                | Text.P($"Result: {result}");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ExpensiveCalculationView())
            )
            | new Markdown("## Choosing the Right Memoization Approach").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TD
                    A["Need to optimize performance?"] --> B{What are you optimizing?}
                
                    B --> C["Expensive computation<br/>or data transformation"]
                    B --> D["Callback function<br/>passed to child components"]
                    B --> E["Entire component<br/>with expensive rendering"]
                
                    C --> F["Use UseMemo"]
                    D --> G["Use UseCallback"]
                    E --> H["Implement IMemoized"]
                
                    F --> I["Cache computed values<br/>• Data filtering<br/>• Complex calculations<br/>• Derived state"]
                    G --> J["Prevent child re-renders<br/>• Event handlers<br/>• Stable function references<br/>• Effect dependencies"]
                    H --> K["Component-level optimization<br/>• List items<br/>• Heavy UI components<br/>• Custom widgets"]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown("The `UseMemo` [hook](app://hooks/rules-of-hooks) caches the result of a computation and only recomputes it when its [state](app://hooks/core/use-state) dependencies change.").OnLinkClick(onLinkClick)
            | new Callout("`UseMemo` hook stores only the most recent dependency values for comparison; older values are discarded.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("### How UseMemo Works").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant C as Component
                    participant M as UseMemo Hook
                    participant S as UseState Storage
                
                    Note over C,S: First Render
                    C->>M: UseMemo(() => expensiveCalc(), [dep1, dep2])
                    M->>S: UseState(() => new MemoRef(result, deps))
                    S-->>M: Create new MemoRef with initial value
                    M->>M: Execute factory function
                    M->>S: Store MemoRef(result, [dep1, dep2])
                    M-->>C: Return computed value
                
                    Note over C,S: Subsequent Render (deps unchanged)
                    C->>M: UseMemo(() => expensiveCalc(), [dep1, dep2])
                    M->>S: Get stored MemoRef
                    S-->>M: Return MemoRef(cachedResult, [dep1, dep2])
                    M->>M: AreDependenciesEqual([dep1, dep2], [dep1, dep2])
                    Note right of M: Dependencies equal!<br/>Skip computation
                    M-->>C: Return cached value (no computation)
                
                    Note over C,S: Subsequent Render (deps changed)
                    C->>M: UseMemo(() => expensiveCalc(), [dep1_new, dep2])
                    M->>S: Get stored MemoRef
                    S-->>M: Return MemoRef(oldResult, [dep1, dep2])
                    M->>M: AreDependenciesEqual([dep1, dep2], [dep1_new, dep2])
                    Note right of M: Dependencies changed!<br/>Need recomputation
                    M->>M: Execute factory function
                    M->>S: Update MemoRef(newResult, [dep1_new, dep2])
                    M-->>C: Return new computed value
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### When to Use Memoization
                
                Use memoization when:
                
                - You have expensive computations that don't need to be redone on every render
                - You want to prevent unnecessary re-renders of [child components](app://onboarding/concepts/widgets)
                - You're dealing with complex data transformations that depend on [state](app://hooks/core/use-state) changes
                - You need stable function references for [`UseEffect`](app://hooks/core/use-effect) dependencies
                
                ## Component Memoization with IMemoized
                
                The `IMemoized` interface allows entire [components](app://onboarding/concepts/views) to be memoized, preventing re-renders when their props haven't changed. This is useful for optimizing [views](app://onboarding/concepts/views) with expensive rendering logic.
                
                ### How IMemoized Works
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant P as Parent Component
                    participant WT as WidgetTree
                    participant C as IMemoized Component
                    participant H as Hash Calculator
                
                    Note over P,H: First Render
                    P->>WT: Render child component
                    WT->>C: Check if implements IMemoized
                    C-->>WT: Yes, implements IMemoized
                    WT->>C: Call GetMemoValues()
                    C-->>WT: Return [prop1, prop2, prop3]
                    WT->>H: CalculateMemoizedHashCode(viewId, memoValues)
                    H->>H: Hash viewId + each prop (string/valuetype/json)
                    H-->>WT: Return computed hash
                    WT->>C: Call Build() - component renders
                    WT->>WT: Store TreeNode with memoizedHashCode
                
                    Note over P,H: Subsequent Render (props unchanged)
                    P->>WT: Render child component
                    WT->>C: Call GetMemoValues()
                    C-->>WT: Return [prop1, prop2, prop3] (same values)
                    WT->>H: CalculateMemoizedHashCode(viewId, memoValues)
                    H-->>WT: Return same hash
                    WT->>WT: Compare: previousHash == currentHash
                    Note right of WT: Hash matches!<br/>Skip Build() call
                    WT-->>P: Reuse previous TreeNode (no re-render)
                
                    Note over P,H: Subsequent Render (props changed)
                    P->>WT: Render child component
                    WT->>C: Call GetMemoValues()
                    C-->>WT: Return [prop1_new, prop2, prop3] (changed values)
                    WT->>H: CalculateMemoizedHashCode(viewId, memoValues)
                    H-->>WT: Return different hash
                    WT->>WT: Compare: previousHash != currentHash
                    Note right of WT: Hash changed!<br/>Need to re-render
                    WT->>C: Call Build() - component re-renders
                    WT->>WT: Update TreeNode with new hash
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown("### IMemoized Basic Usage").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class MemoizedDemoView : ViewBase
                    {
                        public override object? Build()
                        {
                            var count = UseState(0);
                            return Layout.Vertical()
                                | new ExpensiveMemoComponent("Demo", count.Value)
                                | new Button("Increment", onClick: _ => count.Set(count.Value + 1));
                        }
                    }
                    
                    public class ExpensiveMemoComponent : ViewBase, IMemoized
                    {
                        private readonly string _title;
                        private readonly int _value;
                        private readonly DateTime _timestamp;
                    
                        public ExpensiveMemoComponent(string title, int value, DateTime? timestamp = null)
                        {
                            _title = title;
                            _value = value;
                            _timestamp = timestamp ?? DateTime.Now;
                        }
                    
                        public object[] GetMemoValues() => [_title, _value];
                    
                        public override object? Build() =>
                            Layout.Vertical()
                                | Text.H2(_title)
                                | Text.Block($"Value: {_value}")
                                | Text.P($"Rendered at: {_timestamp:HH:mm:ss}");
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MemoizedDemoView())
            )
            | new Markdown("### IMemoized Component Lifecycle").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                stateDiagram-v2
                    [*] --> Created: Component instantiated
                    Created --> CheckMemo: Parent renders
                
                    CheckMemo --> GetValues: Call GetMemoValues()
                    GetValues --> Compare: Compare with cached values
                
                    Compare --> CacheHit: Values unchanged
                    Compare --> CacheMiss: Values changed
                
                    CacheHit --> SkipRender: Use cached result
                    CacheMiss --> ExecuteBuild: Call Build() method
                
                    ExecuteBuild --> UpdateCache: Store new result
                    UpdateCache --> Rendered: Component updated
                
                    SkipRender --> [*]: No re-render needed
                    Rendered --> [*]: Component rendered
                
                    Rendered --> CheckMemo: Next parent render
                    SkipRender --> CheckMemo: Next parent render
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Best Practices for IMemoized
                
                - **Include all relevant props** - Any value that affects rendering should be in `GetMemoValues()`
                - **Exclude volatile values** - Don't include timestamps or random values unless they affect the UI
                - **Use with .Key()** - Always provide a stable key when rendering memoized components in lists
                - **Keep it simple** - Only memoize components with expensive rendering logic
                
                ## Common Pitfalls and Solutions
                
                ### Unstable Dependencies
                
                **Problem**: Creating new objects or arrays in the dependency array
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Bad: New array created on every render
                var result = UseMemo(() => ProcessData(data.Value), data.Value, new[] { "option1", "option2" });
                """",Languages.Csharp)
            | new Markdown("**Solution**: Use stable references with [UseRef](app://hooks/core/use-ref)").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Good: Stable dependency
                var options = UseRef(new[] { "option1", "option2" });
                var result = UseMemo(() => ProcessData(data.Value), data.Value, options);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Callback Dependencies Issues
                
                **Problem**: [UseCallback](app://hooks/core/use-callback) callbacks that capture too many state variables
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Bad: Callback recreated whenever any state changes
                var handleClick = UseCallback(() =>
                {
                    DoSomething(data.Value, filter.Value, sortOrder.Value);
                }, data, filter, sortOrder); // Too many dependencies
                """",Languages.Csharp)
            | new Markdown("**Solution**: Split into smaller, focused callbacks using [UseCallback](app://hooks/core/use-callback)").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Good: Separate callbacks with minimal dependencies
                var handleDataAction = UseCallback(() => DoSomethingWithData(data.Value), data);
                var handleFilterAction = UseCallback(() => ApplyFilter(filter.Value), filter);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Best Practices
                
                - **Dependency Array**: Always specify the [state](app://hooks/core/use-state) dependencies that should trigger a recomputation
                - **Expensive Operations**: Only memoize truly expensive operations
                - **Clean Dependencies**: Keep the dependency array minimal and focused on state values
                - **Avoid Side Effects**: Memoized functions should be pure and not have side effects (use [UseEffect](app://hooks/core/use-effect) for side effects)
                
                ## See Also
                
                - [State Management](app://hooks/core/use-state) - Managing component state
                - [UseCallback](app://hooks/core/use-callback) - Memoizing callback functions
                - [Effects](app://hooks/core/use-effect) - Performing side effects with dependencies
                - [Rules of Hooks](app://hooks/rules-of-hooks) - Understanding hook rules and best practices
                - [UseRef](app://hooks/core/use-ref) - Storing stable references
                - [Signals](app://hooks/core/use-signal) - Reactive state management
                - [Views](app://onboarding/concepts/views) - Understanding Ivy views and components
                
                ## Examples
                """").OnLinkClick(onLinkClick)
            | new Expandable("Complex Data Filtering",
                Vertical().Gap(4)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public record FilterItem(int Id, string Name);
                        
                        public class DataFilterDemoView : ViewBase
                        {
                            public override object? Build()
                            {
                                var data = UseState(new List<FilterItem>
                                {
                                    new(1, "Laptop"),
                                    new(2, "Mouse"),
                                    new(3, "Keyboard"),
                                    new(4, "Monitor"),
                                    new(5, "Headphones")
                                });
                                var filter = UseState("");
                                var filteredData = UseMemo(() =>
                                    data.Value
                                        .Where(item => item.Name.Contains(filter.Value, StringComparison.OrdinalIgnoreCase))
                                        .ToList(),
                                    data.Value, filter.Value);
                        
                                var items = filteredData.Count == 0
                                    ? new object[] { Text.P("No matches.").Muted() }
                                    : filteredData.Select(i => Text.Block(i.Name)).ToArray();
                        
                                return Layout.Vertical()
                                    | filter.ToTextInput().Placeholder("Filter by name")
                                    | Layout.Vertical(items);
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new DataFilterDemoView())
                )
            )
            | new Expandable("Computed Properties",
                Vertical().Gap(4)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public record DemoSale(decimal Amount);
                        
                        public class StatsDemoView : ViewBase
                        {
                            public override object? Build()
                            {
                                var sales = UseState(new List<DemoSale> { new(100m), new(250m), new(75m) });
                                var stats = UseMemo(() => new
                                {
                                    Total = sales.Value.Sum(s => s.Amount),
                                    Average = sales.Value.Count > 0 ? sales.Value.Average(s => s.Amount) : 0m,
                                    Count = sales.Value.Count
                                }, sales.Value);
                        
                                return Layout.Vertical()
                                    | Text.P($"Total: ${stats.Total:N2}")
                                    | Text.P($"Average: ${stats.Average:N2}")
                                    | Text.P($"Count: {stats.Count}")
                                    | new Button("Add sale", onClick: _ => sales.Set(sales.Value.Append(new DemoSale((Random.Shared.Next(1, 50) * 10))).ToList()));
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new StatsDemoView())
                )
            )
            | new Expandable("IMemoized In List Items",
                Vertical().Gap(4)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public record ListProduct(int Id, string Name, decimal Price);
                        
                        public class ProductListDemoView : ViewBase
                        {
                            public override object? Build()
                            {
                                var products = UseState(new List<ListProduct>
                                {
                                    new(1, "Laptop", 999m),
                                    new(2, "Mouse", 29.99m),
                                    new(3, "Keyboard", 79m),
                                    new(4, "Monitor", 299m),
                                    new(5, "Headphones", 149m)
                                });
                                var sortBy = UseState("name");
                                var sortOptions = new IAnyOption[]
                                {
                                    new Option<string>("Name", "name"),
                                    new Option<string>("Price", "price"),
                                    new Option<string>("Id", "id")
                                };
                                var sortedProducts = UseMemo(() =>
                                    (sortBy.Value switch
                                    {
                                        "name" => products.Value.OrderBy(p => p.Name),
                                        "price" => products.Value.OrderBy(p => p.Price),
                                        _ => products.Value.OrderBy(p => p.Id)
                                    }).ToList(),
                                    products.Value, sortBy.Value);
                        
                                var items = sortedProducts.Select((p, i) => new ProductListCard(p, i).Key(p.Id)).ToArray();
                                return Layout.Vertical()
                                    | sortBy.ToSelectInput(sortOptions).Placeholder("Sort by")
                                    | Layout.Vertical(items);
                            }
                        }
                        
                        public class ProductListCard : ViewBase, IMemoized
                        {
                            private readonly ListProduct _product;
                            private readonly int _index;
                        
                            public ProductListCard(ListProduct product, int index)
                            {
                                _product = product;
                                _index = index;
                            }
                        
                            public object[] GetMemoValues() => [_product.Id, _product.Name, _product.Price, _index];
                        
                            public override object? Build() =>
                                new Card(
                                    Layout.Horizontal()
                                        | new Avatar(_product.Name.Length > 0 ? _product.Name[0].ToString() : "?", null)
                                        | (Layout.Vertical()
                                            | Text.H2(_product.Name)
                                            | Text.Block($"${_product.Price:N2}")
                                            | Text.P($"Position: {_index + 1}").Small()));
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new ProductListDemoView())
                )
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseCallbackApp), typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseRefApp), typeof(Hooks.Core.UseSignalApp)]; 
        return article;
    }
}


public class ExpensiveCalculationView : ViewBase
{
    public override object? Build()
    {
        var input = UseState(0);
        
        // Memoize the result of an expensive calculation
        var result = UseMemo(() => 
        {
            return input.Value * input.Value;
        }, input.Value); // Only recompute when input changes
        
        return Layout.Vertical()
            | input.ToNumberInput().Placeholder("Number")
            | Text.P($"Result: {result}");
    }
}

public class MemoizedDemoView : ViewBase
{
    public override object? Build()
    {
        var count = UseState(0);
        return Layout.Vertical()
            | new ExpensiveMemoComponent("Demo", count.Value)
            | new Button("Increment", onClick: _ => count.Set(count.Value + 1));
    }
}

public class ExpensiveMemoComponent : ViewBase, IMemoized
{
    private readonly string _title;
    private readonly int _value;
    private readonly DateTime _timestamp;

    public ExpensiveMemoComponent(string title, int value, DateTime? timestamp = null)
    {
        _title = title;
        _value = value;
        _timestamp = timestamp ?? DateTime.Now;
    }

    public object[] GetMemoValues() => [_title, _value];

    public override object? Build() =>
        Layout.Vertical()
            | Text.H2(_title)
            | Text.Block($"Value: {_value}")
            | Text.P($"Rendered at: {_timestamp:HH:mm:ss}");
}

public record FilterItem(int Id, string Name);

public class DataFilterDemoView : ViewBase
{
    public override object? Build()
    {
        var data = UseState(new List<FilterItem>
        {
            new(1, "Laptop"),
            new(2, "Mouse"),
            new(3, "Keyboard"),
            new(4, "Monitor"),
            new(5, "Headphones")
        });
        var filter = UseState("");
        var filteredData = UseMemo(() =>
            data.Value
                .Where(item => item.Name.Contains(filter.Value, StringComparison.OrdinalIgnoreCase))
                .ToList(),
            data.Value, filter.Value);

        var items = filteredData.Count == 0
            ? new object[] { Text.P("No matches.").Muted() }
            : filteredData.Select(i => Text.Block(i.Name)).ToArray();

        return Layout.Vertical()
            | filter.ToTextInput().Placeholder("Filter by name")
            | Layout.Vertical(items);
    }
}

public record DemoSale(decimal Amount);

public class StatsDemoView : ViewBase
{
    public override object? Build()
    {
        var sales = UseState(new List<DemoSale> { new(100m), new(250m), new(75m) });
        var stats = UseMemo(() => new
        {
            Total = sales.Value.Sum(s => s.Amount),
            Average = sales.Value.Count > 0 ? sales.Value.Average(s => s.Amount) : 0m,
            Count = sales.Value.Count
        }, sales.Value);

        return Layout.Vertical()
            | Text.P($"Total: ${stats.Total:N2}")
            | Text.P($"Average: ${stats.Average:N2}")
            | Text.P($"Count: {stats.Count}")
            | new Button("Add sale", onClick: _ => sales.Set(sales.Value.Append(new DemoSale((Random.Shared.Next(1, 50) * 10))).ToList()));
    }
}

public record ListProduct(int Id, string Name, decimal Price);

public class ProductListDemoView : ViewBase
{
    public override object? Build()
    {
        var products = UseState(new List<ListProduct>
        {
            new(1, "Laptop", 999m),
            new(2, "Mouse", 29.99m),
            new(3, "Keyboard", 79m),
            new(4, "Monitor", 299m),
            new(5, "Headphones", 149m)
        });
        var sortBy = UseState("name");
        var sortOptions = new IAnyOption[]
        {
            new Option<string>("Name", "name"),
            new Option<string>("Price", "price"),
            new Option<string>("Id", "id")
        };
        var sortedProducts = UseMemo(() =>
            (sortBy.Value switch
            {
                "name" => products.Value.OrderBy(p => p.Name),
                "price" => products.Value.OrderBy(p => p.Price),
                _ => products.Value.OrderBy(p => p.Id)
            }).ToList(),
            products.Value, sortBy.Value);

        var items = sortedProducts.Select((p, i) => new ProductListCard(p, i).Key(p.Id)).ToArray();
        return Layout.Vertical()
            | sortBy.ToSelectInput(sortOptions).Placeholder("Sort by")
            | Layout.Vertical(items);
    }
}

public class ProductListCard : ViewBase, IMemoized
{
    private readonly ListProduct _product;
    private readonly int _index;

    public ProductListCard(ListProduct product, int index)
    {
        _product = product;
        _index = index;
    }

    public object[] GetMemoValues() => [_product.Id, _product.Name, _product.Price, _index];

    public override object? Build() =>
        new Card(
            Layout.Horizontal()
                | new Avatar(_product.Name.Length > 0 ? _product.Name[0].ToString() : "?", null)
                | (Layout.Vertical()
                    | Text.H2(_product.Name)
                    | Text.Block($"${_product.Price:N2}")
                    | Text.P($"Position: {_index + 1}").Small()));
}
