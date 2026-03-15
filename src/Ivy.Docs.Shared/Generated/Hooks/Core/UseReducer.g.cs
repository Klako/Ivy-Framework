using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;
using Ivy.Core.Hooks;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:7, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/07_UseReducer.md", searchHints: ["usereducer", "reducer", "state-management", "complex-state", "actions", "hooks", "state-updates", "predictable-state"])]
public class UseReducerApp(bool onlyBody = false) : ViewBase
{
    public UseReducerApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("usereducer", "UseReducer", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("when-to-use-usereducer", "When to Use UseReducer", 2), new ArticleHeading("how-usereducer-works", "How UseReducer Works", 3), new ArticleHeading("use-cases", "Use Cases", 3), new ArticleHeading("usereducer-vs-usestate", "UseReducer vs UseState", 3), new ArticleHeading("best-practices", "Best Practices", 3), new ArticleHeading("see-also", "See Also", 2), new ArticleHeading("examples", "Examples", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseReducer").OnLinkClick(onLinkClick)
            | Lead("Manage complex [state](app://hooks/core/use-state) logic with reducers, providing a predictable state management pattern for [components](app://onboarding/concepts/views) with multiple sub-values or interdependent state updates.")
            | new Markdown(
                """"
                ## Overview
                
                The `UseReducer` [hook](app://hooks/rules-of-hooks) is an alternative to [`UseState`](app://hooks/core/use-state) that is better suited for managing complex [state](app://hooks/core/use-state) logic. It follows the reducer pattern where state updates are handled by a pure function.
                
                Key benefits of `UseReducer`:
                
                - **Predictable [State](app://hooks/core/use-state) Updates** - All state changes go through a single reducer function
                - **Complex [State](app://hooks/core/use-state) Logic** - Better suited for state with multiple sub-values or interdependent updates
                - **Action-Based Updates** - State changes are explicit and traceable through actions
                - **Testability** - Pure reducer functions are easy to test in isolation
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicReducerDemo : ViewBase
                    {
                        // Reducer function
                        private int CounterReducer(int state, string action) => action switch
                        {
                            "increment" => state + 1,
                            "decrement" => state - 1,
                            "reset" => 0,
                            _ => state
                        };
                    
                        public override object? Build()
                        {
                            var (count, dispatch) = UseReducer(CounterReducer, 0);
                    
                            return Layout.Vertical(
                                Text.H3($"Count: {count}"),
                                Layout.Horizontal(
                                    new Button("-", _ => dispatch("decrement")),
                                    new Button("Reset", _ => dispatch("reset")),
                                    new Button("+", _ => dispatch("increment"))
                                )
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicReducerDemo())
            )
            | new Callout("`UseReducer` is ideal when you have complex [state](app://hooks/core/use-state) logic involving multiple sub-values, when the next state depends on the previous one, or when you want to centralize state update logic in one place.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("## When to Use UseReducer").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TD
                    A["Need to manage state?"] --> B{What's the complexity?}
                
                    B --> C["Simple state<br/>1-3 independent values"]
                    B --> D["Complex state<br/>4+ values or interdependent"]
                    B --> E["State updates depend<br/>on previous state"]
                    B --> F["Need centralized<br/>state logic"]
                
                    C --> G["Use UseState<br/>Simple and direct"]
                    D --> H["Use UseReducer<br/>Better organization"]
                    E --> I["Use UseReducer<br/>Predictable updates"]
                    F --> J["Use UseReducer<br/>Single source of truth"]
                
                    G --> K["Direct state setting<br/>Less boilerplate<br/>Good for simple cases"]
                    H --> L["Action-based updates<br/>More structured<br/>Better for complex logic"]
                    I --> M["Pure reducer function<br/>Easier to reason about<br/>Better testability"]
                    J --> N["Centralized logic<br/>Easier to maintain<br/>Better debugging"]
                ```
                """").OnLinkClick(onLinkClick)
            | new Callout("Reducers should be pure functions - they should not have side effects and should return a new [state](app://hooks/core/use-state) object rather than mutating the existing one. Use [`UseEffect`](app://hooks/core/use-effect) for side effects.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("### How UseReducer Works").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                sequenceDiagram
                    participant C as Component
                    participant UR as UseReducer Hook
                    participant R as Reducer Function
                    participant S as State Storage
                
                    Note over C,S: Initialization
                    C->>UR: UseReducer(reducer, initialState)
                    UR->>S: Store initialState
                    UR->>UR: Create dispatch function
                    UR-->>C: Return (state, dispatch)
                
                    Note over C,S: State Update via Dispatch
                    C->>UR: dispatch(action)
                    UR->>S: Get current state
                    S-->>UR: Return current state
                    UR->>R: Call reducer(currentState, action)
                    R->>R: Process action and compute new state
                    R-->>UR: Return new state
                    UR->>S: Update stored state
                    UR->>C: Trigger re-render with new state
                
                    Note over C,S: Component Re-render
                    C->>UR: UseReducer(reducer, initialState)
                    UR->>S: Get current state
                    S-->>UR: Return updated state
                    UR-->>C: Return (newState, dispatch)
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Use Cases
                
                Use `UseReducer` when:
                
                - **Complex [State](app://hooks/core/use-state) Logic** - Managing state with multiple sub-values or interdependent properties
                - **State Updates Depend on Previous State** - When the next state depends on the previous state value
                - **Action-Based Updates** - When you want explicit, traceable state changes through actions
                - **Centralized State Logic** - When you want to centralize all state update logic in one place
                - **Better Testability** - When you need to test state logic in isolation
                
                ### UseReducer vs UseState
                
                Choose between `UseReducer` and [`UseState`](app://hooks/core/use-state) based on complexity:
                
                | UseState | UseReducer |
                |----------|------------|
                | Simple state updates | Complex state logic |
                | Independent values | Interdependent values |
                | Direct state setting | Action-based updates |
                | Less boilerplate | More structured |
                | Good for 1-3 values | Good for 4+ values |
                
                ### Best Practices
                
                - **Keep Reducers Pure** - Reducers should not have side effects and should return new [state](app://hooks/core/use-state) objects. Use [`UseEffect`](app://hooks/core/use-effect) for side effects.
                - **Use Immutable Updates** - Always return new state objects rather than mutating existing ones
                - **Handle All Action Types** - Include a default case to handle unknown actions gracefully
                - **Type Safety** - Use strongly-typed actions and [state](app://hooks/core/use-state) for better compile-time safety
                - **Extract Complex Logic** - Move complex reducer logic into separate functions for clarity and use [`UseMemo`](app://hooks/core/use-memo) for expensive computations
                
                ## See Also
                
                - [State Management](app://hooks/core/use-state) - Simple state management with UseState
                - [Rules of Hooks](app://hooks/rules-of-hooks) - Understanding hook rules and best practices
                - [Effects](app://hooks/core/use-effect) - Side effects and async operations
                - [Memoization](app://hooks/core/use-memo) - Performance optimization with UseMemo
                - [Callbacks](app://hooks/core/use-callback) - Memoized callback functions with UseCallback
                - [Views](app://onboarding/concepts/views) - Understanding Ivy views and components
                
                ### Examples
                """").OnLinkClick(onLinkClick)
            | new Expandable("Shopping Cart with Interdependent State",
                Vertical().Gap(4)
                | new Markdown("This example demonstrates why reducers are powerful - multiple interdependent values (items, subtotal, tax, discount, total) that must stay in sync. With `UseState`, you'd need to update each value separately and risk inconsistencies.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new ShoppingCartDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class ShoppingCartDemo : ViewBase
                        {
                            record CartItem(string Name, decimal Price, int Quantity);
                            record CartState(List<CartItem> Items, decimal DiscountPercent, decimal Subtotal, decimal Tax, decimal Total);
                        
                            private CartState CartReducer(CartState state, string action)
                            {
                                var parts = action.Split('|', 2);
                                var actionType = parts[0];
                                var actionValue = parts.Length > 1 ? parts[1] : "";
                        
                                return actionType switch
                                {
                                    "addItem" => CalculateTotals(state with {
                                        Items = state.Items.Append(new CartItem(actionValue, 10.00m, 1)).ToList()
                                    }),
                                    "removeItem" => CalculateTotals(state with {
                                        Items = state.Items.Where((item, idx) => idx.ToString() != actionValue).ToList()
                                    }),
                                    "setDiscount" => CalculateTotals(state with {
                                        DiscountPercent = decimal.TryParse(actionValue, out var discount) ? discount : 0m
                                    }),
                                    _ => state
                                };
                            }
                        
                            private CartState CalculateTotals(CartState state)
                            {
                                var subtotal = state.Items.Sum(item => item.Price * item.Quantity);
                                var discountAmount = subtotal * (state.DiscountPercent / 100m);
                                var afterDiscount = subtotal - discountAmount;
                                var tax = afterDiscount * 0.08m; // 8% tax
                                var total = afterDiscount + tax;
                        
                                return state with
                                {
                                    Subtotal = subtotal,
                                    Tax = tax,
                                    Total = total
                                };
                            }
                        
                            public override object? Build()
                            {
                                var (cart, dispatch) = UseReducer(CartReducer, new CartState(new List<CartItem>(), 0m, 0m, 0m, 0m));
                                var newItemName = UseState("");
                        
                                return Layout.Vertical(
                                    Text.H3("Shopping Cart"),
                                    Layout.Horizontal(
                                        newItemName.ToTextInput().Placeholder("Item name"),
                                        new Button("Add Item", _ => {
                                            if (!string.IsNullOrWhiteSpace(newItemName.Value))
                                            {
                                                dispatch($"addItem|{newItemName.Value}");
                                                newItemName.Set("");
                                            }
                                        })
                                    ),
                                    new Separator(),
                                    cart.Items.Count > 0
                                        ? Layout.Vertical(
                                            new {
                                                Items = string.Join(", ", cart.Items.Select(item => $"{item.Name} (${item.Price:F2} x {item.Quantity})"))
                                            }.ToDetails(),
                                            Layout.Horizontal(
                                                cart.Items.Select((item, idx) =>
                                                    new Button($"Remove {item.Name}", _ => dispatch($"removeItem|{idx}"))
                                                ).ToArray()
                                            )
                                        )
                                        : Text.P("No items in cart").Small(),
                                    new Separator(),
                                    Layout.Vertical(
                                        Text.P($"Subtotal: ${cart.Subtotal:F2}"),
                                        Text.P($"Discount ({cart.DiscountPercent}%): ${cart.Subtotal * (cart.DiscountPercent / 100m):F2}"),
                                        Text.P($"Tax (8%): ${cart.Tax:F2}"),
                                        Text.H4($"Total: ${cart.Total:F2}")
                                    ),
                                    Layout.Horizontal(
                                        new Button("10% Off", _ => dispatch("setDiscount|10")),
                                        new Button("20% Off", _ => dispatch("setDiscount|20")),
                                        new Button("Clear Discount", _ => dispatch("setDiscount|0"))
                                    )
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Game State with Multiple Interdependent Values",
                Vertical().Gap(4)
                | new Markdown("This example shows a game state where actions affect multiple values simultaneously - perfect for demonstrating reducer's centralized state management.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new GameStateDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class GameStateDemo : ViewBase
                        {
                            record GameState(int Score, int Level, int Lives, int Multiplier, bool IsGameOver);
                        
                            private GameState GameReducer(GameState state, string action) => action switch
                            {
                                "score" => state.IsGameOver ? state : state with {
                                    Score = state.Score + (10 * state.Multiplier),
                                    Level = (state.Score + (10 * state.Multiplier)) / 100 + 1,
                                    Multiplier = Math.Min(state.Multiplier + 1, 5)
                                },
                                "miss" => state.IsGameOver ? state : state with {
                                    Lives = state.Lives - 1,
                                    Multiplier = 1,
                                    IsGameOver = state.Lives <= 1
                                },
                                "reset" => new GameState(0, 1, 3, 1, false),
                                _ => state
                            };
                        
                            public override object? Build()
                            {
                                var (game, dispatch) = UseReducer(GameReducer, new GameState(0, 1, 3, 1, false));
                        
                                return Layout.Vertical(
                                    Text.H3("Game State"),
                                    Layout.Vertical(
                                        Text.P($"Score: {game.Score}"),
                                        Text.P($"Level: {game.Level}"),
                                        Text.P($"Lives: {game.Lives}"),
                                        Text.P($"Multiplier: {game.Multiplier}x"),
                                        game.IsGameOver ? Text.Danger("Game Over!") : Text.Success("Playing...")
                                    ),
                                    new Separator(),
                                    Layout.Horizontal(
                                        new Button("Score!", _ => dispatch("score")).Disabled(game.IsGameOver),
                                        new Button("Miss", _ => dispatch("miss")).Disabled(game.IsGameOver),
                                        new Button("Reset", _ => dispatch("reset"))
                                    ),
                                    Text.P("Notice how scoring updates level and multiplier, while missing resets multiplier and decreases lives - all in one reducer!").Small()
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseMemoApp), typeof(Hooks.Core.UseCallbackApp)]; 
        return article;
    }
}


public class BasicReducerDemo : ViewBase
{
    // Reducer function
    private int CounterReducer(int state, string action) => action switch
    {
        "increment" => state + 1,
        "decrement" => state - 1,
        "reset" => 0,
        _ => state
    };
    
    public override object? Build()
    {
        var (count, dispatch) = UseReducer(CounterReducer, 0);
        
        return Layout.Vertical(
            Text.H3($"Count: {count}"),
            Layout.Horizontal(
                new Button("-", _ => dispatch("decrement")),
                new Button("Reset", _ => dispatch("reset")),
                new Button("+", _ => dispatch("increment"))
            )
        );
    }
}

public class ShoppingCartDemo : ViewBase
{
    record CartItem(string Name, decimal Price, int Quantity);
    record CartState(List<CartItem> Items, decimal DiscountPercent, decimal Subtotal, decimal Tax, decimal Total);
    
    private CartState CartReducer(CartState state, string action)
    {
        var parts = action.Split('|', 2);
        var actionType = parts[0];
        var actionValue = parts.Length > 1 ? parts[1] : "";
        
        return actionType switch
        {
            "addItem" => CalculateTotals(state with { 
                Items = state.Items.Append(new CartItem(actionValue, 10.00m, 1)).ToList() 
            }),
            "removeItem" => CalculateTotals(state with { 
                Items = state.Items.Where((item, idx) => idx.ToString() != actionValue).ToList() 
            }),
            "setDiscount" => CalculateTotals(state with { 
                DiscountPercent = decimal.TryParse(actionValue, out var discount) ? discount : 0m 
            }),
            _ => state
        };
    }
    
    private CartState CalculateTotals(CartState state)
    {
        var subtotal = state.Items.Sum(item => item.Price * item.Quantity);
        var discountAmount = subtotal * (state.DiscountPercent / 100m);
        var afterDiscount = subtotal - discountAmount;
        var tax = afterDiscount * 0.08m; // 8% tax
        var total = afterDiscount + tax;
        
        return state with 
        { 
            Subtotal = subtotal,
            Tax = tax,
            Total = total
        };
    }
    
    public override object? Build()
    {
        var (cart, dispatch) = UseReducer(CartReducer, new CartState(new List<CartItem>(), 0m, 0m, 0m, 0m));
        var newItemName = UseState("");
        
        return Layout.Vertical(
            Text.H3("Shopping Cart"),
            Layout.Horizontal(
                newItemName.ToTextInput().Placeholder("Item name"),
                new Button("Add Item", _ => {
                    if (!string.IsNullOrWhiteSpace(newItemName.Value))
                    {
                        dispatch($"addItem|{newItemName.Value}");
                        newItemName.Set("");
                    }
                })
            ),
            new Separator(),
            cart.Items.Count > 0 
                ? Layout.Vertical(
                    new { 
                        Items = string.Join(", ", cart.Items.Select(item => $"{item.Name} (${item.Price:F2} x {item.Quantity})"))
                    }.ToDetails(),
                    Layout.Horizontal(
                        cart.Items.Select((item, idx) => 
                            new Button($"Remove {item.Name}", _ => dispatch($"removeItem|{idx}"))
                        ).ToArray()
                    )
                )
                : Text.P("No items in cart").Small(),
            new Separator(),
            Layout.Vertical(
                Text.P($"Subtotal: ${cart.Subtotal:F2}"),
                Text.P($"Discount ({cart.DiscountPercent}%): ${cart.Subtotal * (cart.DiscountPercent / 100m):F2}"),
                Text.P($"Tax (8%): ${cart.Tax:F2}"),
                Text.H4($"Total: ${cart.Total:F2}")
            ),
            Layout.Horizontal(
                new Button("10% Off", _ => dispatch("setDiscount|10")),
                new Button("20% Off", _ => dispatch("setDiscount|20")),
                new Button("Clear Discount", _ => dispatch("setDiscount|0"))
            )
        );
    }
}

public class GameStateDemo : ViewBase
{
    record GameState(int Score, int Level, int Lives, int Multiplier, bool IsGameOver);
    
    private GameState GameReducer(GameState state, string action) => action switch
    {
        "score" => state.IsGameOver ? state : state with { 
            Score = state.Score + (10 * state.Multiplier),
            Level = (state.Score + (10 * state.Multiplier)) / 100 + 1,
            Multiplier = Math.Min(state.Multiplier + 1, 5)
        },
        "miss" => state.IsGameOver ? state : state with { 
            Lives = state.Lives - 1,
            Multiplier = 1,
            IsGameOver = state.Lives <= 1
        },
        "reset" => new GameState(0, 1, 3, 1, false),
        _ => state
    };
    
    public override object? Build()
    {
        var (game, dispatch) = UseReducer(GameReducer, new GameState(0, 1, 3, 1, false));
        
        return Layout.Vertical(
            Text.H3("Game State"),
            Layout.Vertical(
                Text.P($"Score: {game.Score}"),
                Text.P($"Level: {game.Level}"),
                Text.P($"Lives: {game.Lives}"),
                Text.P($"Multiplier: {game.Multiplier}x"),
                game.IsGameOver ? Text.Danger("Game Over!") : Text.Success("Playing...")
            ),
            new Separator(),
            Layout.Horizontal(
                new Button("Score!", _ => dispatch("score")).Disabled(game.IsGameOver),
                new Button("Miss", _ => dispatch("miss")).Disabled(game.IsGameOver),
                new Button("Reset", _ => dispatch("reset"))
            ),
            Text.P("Notice how scoring updates level and multiplier, while missing resets multiplier and decreases lives - all in one reducer!").Small()
        );
    }
}
