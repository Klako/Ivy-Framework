using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/03_UseState.md", searchHints: ["usestate", "state", "reactive", "data", "hooks", "reactivity", "state-management"])]
public class UseStateApp(bool onlyBody = false) : ViewBase
{
    public UseStateApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("usestate", "UseState", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("state-with-factory-functions", "State with Factory Functions", 3), new ArticleHeading("state-types-and-patterns", "State Types and Patterns", 3), new ArticleHeading("state-in-forms", "State in Forms", 3), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("should-i-use-usestate-to-store-a-prop-or-parameter-value", "Should I use UseState to store a prop or parameter value?", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # UseState
                
                Master reactive state management in Ivy using [hooks](app://hooks/rules-of-hooks) like UseState, [UseSignal](app://hooks/core/use-signal), and [UseEffect](app://hooks/core/use-effect) to build dynamic, responsive [applications](app://onboarding/concepts/apps).
                
                State management is a fundamental concept in Ivy that allows you to handle and update data within your [views](app://onboarding/concepts/views). Ivy provides several mechanisms for managing state, each suited for different use cases.
                
                ## Basic Usage
                
                The `UseState` hook is the primary way to create reactive state in Ivy views:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class CounterApp : ViewBase
                    {
                        public override object? Build()
                        {
                            var count = UseState(0);
                            var name = UseState("World");
                            var client = UseService<IClientProvider>();
                    
                            return new Card(
                                Layout.Vertical(
                                    Text.Literal($"Hello, {name.Value}!"),
                                    Text.Literal($"Count: {count.Value}"),
                                    Layout.Horizontal(
                                        new Button("Increment", _ => count.Set(count.Value + 1)),
                                        new Button("Decrement", _ => count.Set(count.Value - 1)),
                                        new Button("Reset", _ => count.Set(0))
                                    ),
                                    new Separator(),
                                    Layout.Horizontal(
                                        name.ToTextInput().Placeholder("Your Name"),
                                        new Button("Greet", _ => client.Toast($"Hello, {name.Value}!", "Greeting"))
                                    )
                                )
                            ).Title("Counter Demo");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new CounterApp())
            )
            | new Markdown(
                """"
                ### State with Factory Functions
                
                For complex initialization or when you need to defer object creation, use factory functions with UseState. This pattern is useful for expensive computations, [dependency injection](app://hooks/core/use-service), [memoization](app://hooks/core/use-memo), and [lazy loading](app://onboarding/concepts/apps):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FactoryStateDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FactoryStateDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var expensiveData = UseState(() => ComputeExpensiveData());
                    
                            var service = UseState(() => new DataService(GetConfig()));
                    
                            return Layout.Vertical(
                                Text.P("Factory Functions Demo").Large(),
                                Text.Literal($"Data: {expensiveData.Value}"),
                                Text.Literal($"Service: {service.Value.Status}"),
                                new Button("Refresh", _ => expensiveData.Set(ComputeExpensiveData()))
                            );
                        }
                    
                        private string ComputeExpensiveData() => $"Computed at {DateTime.Now:HH:mm:ss}";
                        private string GetConfig() => "production";
                    }
                    
                    public class DataService
                    {
                        public string Status { get; } = "Connected";
                        public DataService(string config) { }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### State Types and Patterns").OnLinkClick(onLinkClick)
            | new Callout("Always use immutable types (e.g. records) with `UseState`. If you mutate an object in-place and call `.Set()` with the same reference, the UI will **not** re-render because the reference hasn't changed. Instead, create a new instance (e.g. using `with` expressions on records) before calling `.Set()`.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown("Ivy supports various state types including primitives, collections, complex objects, and nullable types. Each type has specific update patterns and considerations for optimal performance and maintainability:").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StatePatternsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StatePatternsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            // Primitive types
                            var count = UseState(0);
                            var name = UseState("Guest");
                    
                            // Collections
                            var items = UseState(() => new List<string> { "Item 1", "Item 2" });
                    
                            // Complex objects
                            var user = UseState(() => new User { Name = "John", Age = 25 });
                    
                            // Nullable types
                            var selectedItem = UseState(() => (string?)null);
                    
                            return Layout.Vertical(
                                Text.P("State Types & Patterns").Large(),
                    
                                // Primitive state
                                Layout.Horizontal(
                                    new Button($"Count: {count.Value}", _ => count.Set(count.Value + 1)),
                                    new Button(name.Value, _ => name.Set("User " + Random.Shared.Next(100)))
                                ),
                    
                                // Collection state
                                Layout.Horizontal(
                                    new Button("Add Item", _ => {
                                        var newList = new List<string>(items.Value) { $"Item {items.Value.Count + 1}" };
                                        items.Set(newList);
                                    }),
                                    new Button("Clear", _ => {
                                        var emptyList = new List<string>();
                                        items.Set(emptyList);
                                    })
                                ),
                    
                                // Object state
                                Layout.Horizontal(
                                    new Button($"User: {user.Value.Name}", _ => {
                                        var newUser = new User { Name = "Jane", Age = 30 };
                                        user.Set(newUser);
                                    })
                                ),
                    
                                // Nullable state
                                Layout.Horizontal(
                                    new Button("Set Item", _ => selectedItem.Set("Selected Item")),
                                    new Button("Clear Item", _ => {
                                        string? nullValue = null;
                                        selectedItem.Set(nullValue);
                                    })
                                ),
                    
                                new Separator(),
                                Text.Literal($"Items: {string.Join(", ", items.Value)}"),
                                Text.Literal($"Selected: {selectedItem.Value ?? "None"}")
                            );
                        }
                    }
                    
                    public class User
                    {
                        public string Name { get; set; } = "";
                        public int Age { get; set; }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("State updates in Ivy are handled through the Set method, which can accept direct values or computed values. Updates trigger automatic re-renders of the affected components, ensuring the UI stays synchronized with the current state:").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StateUpdatesDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StateUpdatesDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var count = UseState(0);
                            var text = UseState("Hello");
                            var items = UseState(() => new List<string> { "Item 1", "Item 2" });
                    
                            return Layout.Vertical(
                                Text.P("State Updates Demo").Large(),
                    
                                // Direct updates
                                Layout.Horizontal(
                                    new Button($"Count: {count.Value}", _ => count.Set(count.Value + 1)),
                                    new Button("Reset", _ => count.Set(0))
                                ),
                    
                                // String updates
                                Layout.Horizontal(
                                    text.ToTextInput("Enter text"),
                                    new Button("Clear", _ => text.Set("")),
                                    new Button("Uppercase", _ => text.Set(text.Value.ToUpper()))
                                ),
                    
                                // Collection updates
                                Layout.Horizontal(
                                    new Button("Add Item", _ => {
                                        var newItems = new List<string>(items.Value) { $"Item {items.Value.Count + 1}" };
                                        items.Set(newItems);
                                    }),
                                    new Button("Clear", _ => items.Set(new List<string>()))
                                ),
                    
                                Text.Literal($"Text: {text.Value}"),
                                Text.Literal($"Items: {string.Join(", ", items.Value)}")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### State in Forms
                
                [Forms](app://onboarding/concepts/forms) in Ivy use state variables bound to input widgets, allowing for real-time validation, live previews, and easy form submission handling. The ToTextInput, ToNumberInput, and ToBoolInput extensions provide seamless state binding:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormStateDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FormStateDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var name = UseState("");
                            var email = UseState("");
                            var age = UseState(18);
                            var isSubscribed = UseState(false);
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Vertical(
                                Text.P("Form State Demo").Large(),
                    
                                name.ToTextInput("Name").Placeholder("Enter your name"),
                                email.ToTextInput("Email").Placeholder("Enter your email"),
                                age.ToNumberInput("Age").Min(0).Max(120),
                                isSubscribed.ToBoolInput("Subscribe to newsletter"),
                    
                                new Separator(),
                    
                                Layout.Horizontal(
                                    new Button("Submit", _ => client.Toast($"Hello, {name.Value}!", "Greeting")),
                                    new Button("Clear", _ => {
                                        name.Set("");
                                        email.Set("");
                                        age.Set(18);
                                        isSubscribed.Set(false);
                                    })
                                ),
                    
                                Text.Literal($"Preview: {name.Value} ({email.Value}) - Age: {age.Value}")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("The [UseEffect](app://hooks/core/use-effect) hook allows you to perform [side effects](app://hooks/core/use-effect) when state changes, such as updating derived state, making API calls, or triggering other actions. Effects run automatically when their [dependencies](app://hooks/core/use-effect) change:").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new UseStateEffectsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class UseStateEffectsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var count = UseState(0);
                            var lastUpdate = UseState(DateTime.Now);
                            var isEven = UseState(false);
                    
                            // Effect that runs when count changes
                            UseEffect(() => {
                                lastUpdate.Set(DateTime.Now);
                                isEven.Set(count.Value % 2 == 0);
                            }, [count]);
                    
                            return Layout.Vertical(
                                Text.P("State with Effects Demo").Large(),
                    
                                Layout.Horizontal(
                                    new Button($"Count: {count.Value}", _ => count.Set(count.Value + 1)),
                                    new Button("Reset", _ => count.Set(0))
                                ),
                    
                                new Separator(),
                    
                                Text.Literal($"Last Update: {lastUpdate.Value:HH:mm:ss}"),
                                Text.Literal($"Is Even: {(isEven.Value ? "Yes" : "No")}")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Faq
                
                ### Should I use UseState to store a prop or parameter value?
                
                No. UseState captures its initial value on the first render and does not update when props change.
                
                **Wrong:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // endpoint is initially null, so UseState captures "{}" forever
                var responseJson = UseState(endpoint?.ResponseJson ?? "{}");
                return responseJson.Value.ToCodeInput();
                """",Languages.Csharp)
            | new Markdown("**Correct — use the prop directly if read-only:**").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                return (endpoint?.ResponseJson ?? "{}").ToCodeInput().Disabled();
                """",Languages.Csharp)
            | new Markdown("**Correct — use UseEffect to sync if you need editable state:**").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var responseJson = UseState(() => endpoint?.ResponseJson ?? "{}");
                UseEffect(() => responseJson.Set(endpoint?.ResponseJson ?? "{}"), endpoint);
                return responseJson.ToCodeInput();
                """",Languages.Csharp)
            | new Markdown("UseState is for user-owned state (form inputs, toggles, selections). If you just need to display a value from a prop, use the prop directly.").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseSignalApp), typeof(Hooks.Core.UseEffectApp), typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseServiceApp), typeof(Hooks.Core.UseMemoApp), typeof(Onboarding.Concepts.FormsApp)]; 
        return article;
    }
}


public class CounterApp : ViewBase
{
    public override object? Build()
    {
        var count = UseState(0);
        var name = UseState("World");
        var client = UseService<IClientProvider>();
        
        return new Card(
            Layout.Vertical(
                Text.Literal($"Hello, {name.Value}!"),
                Text.Literal($"Count: {count.Value}"),
                Layout.Horizontal(
                    new Button("Increment", _ => count.Set(count.Value + 1)),
                    new Button("Decrement", _ => count.Set(count.Value - 1)),
                    new Button("Reset", _ => count.Set(0))
                ),
                new Separator(),
                Layout.Horizontal(
                    name.ToTextInput().Placeholder("Your Name"),
                    new Button("Greet", _ => client.Toast($"Hello, {name.Value}!", "Greeting"))
                )
            )
        ).Title("Counter Demo");
    }
}

public class FactoryStateDemo : ViewBase
{
    public override object? Build()
    {
        var expensiveData = UseState(() => ComputeExpensiveData());
        
        var service = UseState(() => new DataService(GetConfig()));
        
        return Layout.Vertical(
            Text.P("Factory Functions Demo").Large(),
            Text.Literal($"Data: {expensiveData.Value}"),
            Text.Literal($"Service: {service.Value.Status}"),
            new Button("Refresh", _ => expensiveData.Set(ComputeExpensiveData()))
        );
    }
    
    private string ComputeExpensiveData() => $"Computed at {DateTime.Now:HH:mm:ss}";
    private string GetConfig() => "production";
}

public class DataService
{
    public string Status { get; } = "Connected";
    public DataService(string config) { }
}

public class StatePatternsDemo : ViewBase
{
    public override object? Build()
    {
        // Primitive types
        var count = UseState(0);
        var name = UseState("Guest");
        
        // Collections
        var items = UseState(() => new List<string> { "Item 1", "Item 2" });
        
        // Complex objects
        var user = UseState(() => new User { Name = "John", Age = 25 });
        
        // Nullable types
        var selectedItem = UseState(() => (string?)null);
        
        return Layout.Vertical(
            Text.P("State Types & Patterns").Large(),
            
            // Primitive state
            Layout.Horizontal(
                new Button($"Count: {count.Value}", _ => count.Set(count.Value + 1)),
                new Button(name.Value, _ => name.Set("User " + Random.Shared.Next(100)))
            ),
            
            // Collection state
            Layout.Horizontal(
                new Button("Add Item", _ => {
                    var newList = new List<string>(items.Value) { $"Item {items.Value.Count + 1}" };
                    items.Set(newList);
                }),
                new Button("Clear", _ => {
                    var emptyList = new List<string>();
                    items.Set(emptyList);
                })
            ),
            
            // Object state
            Layout.Horizontal(
                new Button($"User: {user.Value.Name}", _ => {
                    var newUser = new User { Name = "Jane", Age = 30 };
                    user.Set(newUser);
                })
            ),
            
            // Nullable state
            Layout.Horizontal(
                new Button("Set Item", _ => selectedItem.Set("Selected Item")),
                new Button("Clear Item", _ => {
                    string? nullValue = null;
                    selectedItem.Set(nullValue);
                })
            ),
            
            new Separator(),
            Text.Literal($"Items: {string.Join(", ", items.Value)}"),
            Text.Literal($"Selected: {selectedItem.Value ?? "None"}")
        );
    }
}

public class User
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
}

public class StateUpdatesDemo : ViewBase
{
    public override object? Build()
    {
        var count = UseState(0);
        var text = UseState("Hello");
        var items = UseState(() => new List<string> { "Item 1", "Item 2" });
        
        return Layout.Vertical(
            Text.P("State Updates Demo").Large(),
            
            // Direct updates
            Layout.Horizontal(
                new Button($"Count: {count.Value}", _ => count.Set(count.Value + 1)),
                new Button("Reset", _ => count.Set(0))
            ),
            
            // String updates
            Layout.Horizontal(
                text.ToTextInput("Enter text"),
                new Button("Clear", _ => text.Set("")),
                new Button("Uppercase", _ => text.Set(text.Value.ToUpper()))
            ),
            
            // Collection updates
            Layout.Horizontal(
                new Button("Add Item", _ => {
                    var newItems = new List<string>(items.Value) { $"Item {items.Value.Count + 1}" };
                    items.Set(newItems);
                }),
                new Button("Clear", _ => items.Set(new List<string>()))
            ),
            
            Text.Literal($"Text: {text.Value}"),
            Text.Literal($"Items: {string.Join(", ", items.Value)}")
        );
    }
}

public class FormStateDemo : ViewBase
{
    public override object? Build()
    {
        var name = UseState("");
        var email = UseState("");
        var age = UseState(18);
        var isSubscribed = UseState(false);
        var client = UseService<IClientProvider>();

        return Layout.Vertical(
            Text.P("Form State Demo").Large(),
            
            name.ToTextInput("Name").Placeholder("Enter your name"),
            email.ToTextInput("Email").Placeholder("Enter your email"),
            age.ToNumberInput("Age").Min(0).Max(120),
            isSubscribed.ToBoolInput("Subscribe to newsletter"),
            
            new Separator(),
            
            Layout.Horizontal(
                new Button("Submit", _ => client.Toast($"Hello, {name.Value}!", "Greeting")),
                new Button("Clear", _ => {
                    name.Set("");
                    email.Set("");
                    age.Set(18);
                    isSubscribed.Set(false);
                })
            ),
            
            Text.Literal($"Preview: {name.Value} ({email.Value}) - Age: {age.Value}")
        );
    }
}

public class UseStateEffectsDemo : ViewBase
{
    public override object? Build()
    {
        var count = UseState(0);
        var lastUpdate = UseState(DateTime.Now);
        var isEven = UseState(false);
        
        // Effect that runs when count changes
        UseEffect(() => {
            lastUpdate.Set(DateTime.Now);
            isEven.Set(count.Value % 2 == 0);
        }, [count]);
        
        return Layout.Vertical(
            Text.P("State with Effects Demo").Large(),
            
            Layout.Horizontal(
                new Button($"Count: {count.Value}", _ => count.Set(count.Value + 1)),
                new Button("Reset", _ => count.Set(0))
            ),
            
            new Separator(),
            
            Text.Literal($"Last Update: {lastUpdate.Value:HH:mm:ss}"),
            Text.Literal($"Is Even: {(isEven.Value ? "Yes" : "No")}")
        );
    }
}
