using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/04_UseEffect.md", searchHints: ["useeffect", "lifecycle", "hooks", "side-effects", "async", "cleanup"])]
public class UseEffectApp(bool onlyBody = false) : ViewBase
{
    public UseEffectApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("useeffect", "UseEffect", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("effect-overloads", "Effect Overloads", 2), new ArticleHeading("action-handler", "Action Handler", 3), new ArticleHeading("async-task-handler", "Async Task Handler", 3), new ArticleHeading("disposable-handler", "Disposable Handler", 3), new ArticleHeading("async-disposable-handler", "Async Disposable Handler", 3), new ArticleHeading("effect-triggers", "Effect Triggers", 2), new ArticleHeading("state-dependencies", "State Dependencies", 3), new ArticleHeading("multiple-dependencies", "Multiple Dependencies", 3), new ArticleHeading("common-patterns", "Common Patterns", 2), new ArticleHeading("data-fetching", "Data Fetching", 3), new ArticleHeading("cleanup-operations", "Cleanup Operations", 3), new ArticleHeading("conditional-effects", "Conditional Effects", 3), new ArticleHeading("see-also", "See Also", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseEffect").OnLinkClick(onLinkClick)
            | Lead("Perform side effects in your Ivy [views](app://onboarding/concepts/views) with the UseEffect [hook](app://hooks/rules-of-hooks), similar to React's useEffect but optimized for server-side architecture.")
            | new Markdown(
                """"
                The `UseEffect` [hook](app://hooks/rules-of-hooks) is a powerful feature in Ivy that allows you to perform side effects in your [views](app://onboarding/concepts/views). It's similar to React's useEffect hook but adapted for Ivy's architecture and patterns.
                
                Effects are essential for handling operations that don't directly relate to rendering, such as working with [state](app://hooks/core/use-state) updates, [async operations](app://onboarding/concepts/tasks-and-observables), and external services:
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph TD
                    A[UseEffect] --> B[API calls & Data fetching]
                    A --> C[Timers & Intervals]
                    A --> D[Event subscriptions]
                    A --> E[Cleanup operations]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Basic Usage
                
                The simplest form of `UseEffect` runs after the component initializes:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicEffectView : ViewBase
                    {
                        public override object? Build()
                        {
                            var message = UseState("Click the button to load data");
                            var loadTrigger = UseState(0);
                    
                            // Effect runs when loadTrigger state changes
                            UseEffect(async () =>
                            {
                                if (loadTrigger.Value == 0) return; // Skip initial render
                    
                                message.Set("Loading...");
                                await Task.Delay(2000); // Simulate API call
                                message.Set("Data loaded!");
                            }, loadTrigger);
                    
                            return Layout.Vertical()
                                | new Button("Load Data", () => loadTrigger.Set(loadTrigger.Value + 1))
                                | Text.P(message.Value);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicEffectView())
            )
            | new Markdown(
                """"
                ## Effect Overloads
                
                Ivy provides four different overloads of `UseEffect` to handle various scenarios:
                
                ### Action Handler
                
                For simple synchronous operations:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                UseEffect(() =>
                {
                    Console.WriteLine("Component initialized");
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Async Task Handler
                
                For asynchronous operations:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                UseEffect(async () =>
                {
                    var data = await ApiService.GetData();
                    // Handle data...
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Disposable Handler
                
                For operations that need cleanup:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                UseEffect(() =>
                {
                    var timer = new Timer(callback, null, 0, 1000);
                    return timer; // Timer will be disposed when component unmounts
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Async Disposable Handler
                
                For async operations with cleanup:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                UseEffect(async () =>
                {
                    var connection = await ConnectToService();
                    return connection; // Connection will be disposed automatically
                });
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Effect Triggers
                
                Effects can be triggered by different events using trigger parameters:
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph LR
                    A[UseEffect] --> B[OnMount - Default]
                    A --> C[OnBuild]
                    A --> D[OnStateChange]
                
                    B --> B1["Runs once during initialization"]
                    C --> C1["Runs after virtual DOM updates"]
                    D --> D1["Runs when state changes"]
                ```
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // OnMount (default) - runs once during initialization
                UseEffect(() => { /* ... */ });
                UseEffect(() => { /* ... */ }, EffectTrigger.OnMount());
                
                // OnBuild - runs after virtual DOM updates
                UseEffect(() => { /* ... */ }, EffectTrigger.OnBuild());
                
                // OnStateChange - runs when state changes
                UseEffect(() => { /* ... */ }, EffectTrigger.OnStateChange(myState));
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### State Dependencies
                
                Effects can depend on [state](app://hooks/core/use-state) changes:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DependentEffectView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DependentEffectView : ViewBase
                    {
                        public override object? Build()
                        {
                            var count = UseState(0);
                            var message = UseState("Count: 0");
                    
                            UseEffect(() =>
                            {
                                message.Set($"Count changed to: {count.Value}");
                            }, count);
                    
                            return Layout.Vertical()
                                | new Button($"Count: {count.Value}",
                                    () => count.Set(count.Value + 1))
                                | Text.P(message.Value);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Multiple Dependencies
                
                Effects can depend on multiple state variables:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MultipleDepsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class MultipleDepsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var firstName = UseState("John");
                            var lastName = UseState("Doe");
                            var fullName = UseState("");
                    
                            UseEffect(() =>
                            {
                                fullName.Set($"{firstName.Value} {lastName.Value}");
                            }, firstName, lastName);
                    
                            return Layout.Vertical()
                                | (Layout.Horizontal()
                                    | new Button($"First: {firstName.Value}",
                                        () => firstName.Set(firstName.Value == "John" ? "Jane" : "John"))
                                    | new Button($"Last: {lastName.Value}",
                                        () => lastName.Set(lastName.Value == "Doe" ? "Smith" : "Doe")))
                                | Text.P($"Full name: {fullName.Value}");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Common Patterns
                
                ### Data Fetching
                
                Use `UseEffect` to fetch data from APIs or external services. The effect can be triggered by user interactions, state changes, or component initialization. Manage loading states to provide feedback during async operations.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DataFetchView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DataFetchView : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = UseState<List<Item>?>();
                            var loading = UseState(false);
                            var loadTrigger = UseState(0);
                    
                            UseEffect(async () =>
                            {
                                if (loadTrigger.Value == 0) return; // Skip initial render
                    
                                loading.Set(true);
                    
                                // Simulate API call - exceptions automatically handled by Ivy
                                await Task.Delay(1500);
                                var items = new List<Item>
                                {
                                    new("Item 1", "Description 1"),
                                    new("Item 2", "Description 2"),
                                    new("Item 3", "Description 3")
                                };
                    
                                data.Set(items);
                                loading.Set(false);
                            }, loadTrigger);
                    
                            return Layout.Vertical()
                                | new Button("Fetch Data", () => loadTrigger.Set(loadTrigger.Value + 1))
                                | (loading.Value
                                    ? Text.P("Loading data...")
                                    : Layout.Horizontal(
                                        data.Value?.Select(item =>
                                            new Button($"{item.Name}: {item.Description}")
                                        ) ?? Enumerable.Empty<Button>()
                                    ));
                        }
                    }
                    
                    public record Item(string Name, string Description);
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("You do not need to manually catch exceptions in UseEffect. Ivy has a built-in exception handling pipeline that automatically catches exceptions from effects and displays them to users via error notifications and console logging. The system wraps effect exceptions in `EffectException` and routes them through registered exception handlers.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Cleanup Operations
                
                Return an `IDisposable` from `UseEffect` to perform cleanup when dependencies change or the component unmounts. This is essential for releasing resources like timers, subscriptions, or connections to prevent memory leaks. Store disposables in [UseRef](app://hooks/core/use-ref) when you need to reference them across renders.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SubscriptionView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SubscriptionView : ViewBase
                    {
                        public override object? Build()
                        {
                            var message = UseState("Stopped");
                            var isActive = UseState(false);
                            var previousResource = UseRef<IDisposable?>(() => null);
                    
                            UseEffect(() =>
                            {
                                if (!isActive.Value)
                                {
                                    var hadResource = previousResource.Value != null;
                                    previousResource.Value?.Dispose();
                                    previousResource.Value = null;
                                    if (!hadResource) message.Set("Stopped");
                                    return System.Reactive.Disposables.Disposable.Empty;
                                }
                    
                                previousResource.Value?.Dispose();
                                message.Set("Running");
                    
                                var resource = new SafeDisposable(() => message.Set("Cleaned up"));
                                previousResource.Value = resource;
                                return resource;
                            }, isActive);
                    
                            return Layout.Vertical()
                                | new Button(isActive.Value ? "Stop" : "Start",
                                    () => isActive.Set(!isActive.Value))
                                | Text.P($"Status: {message.Value}");
                        }
                    
                        private class SafeDisposable : IDisposable
                        {
                            private readonly Action _onDispose;
                            private bool _isDisposed;
                    
                            public SafeDisposable(Action onDispose) => _onDispose = onDispose;
                    
                            public void Dispose()
                            {
                                if (_isDisposed) return;
                                _isDisposed = true;
                                _onDispose();
                            }
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Conditional Effects
                
                Effects can be conditionally executed based on state values. Check conditions inside the effect and return early or conditionally create resources.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ConditionalEffectView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ConditionalEffectView : ViewBase
                    {
                        public override object? Build()
                        {
                            var isEnabled = UseState(false);
                            var data = UseState<string?>();
                    
                            UseEffect(async () =>
                            {
                                if (!isEnabled.Value)
                                {
                                    data.Set((string)null);
                                    return;
                                }
                    
                                // Only fetch when enabled
                                var result = await FetchData();
                                data.Set(result);
                            }, isEnabled);
                    
                            return Layout.Vertical()
                                | new Button($"Fetching: {(isEnabled.Value ? "ON" : "OFF")}",
                                    onClick: _ => isEnabled.Set(!isEnabled.Value))
                                | (data.Value != null ? Text.P(data.Value) : Text.Muted("No data"));
                        }
                    
                        private async Task<string> FetchData()
                        {
                            await Task.Delay(1000);
                            return $"Data fetched at {DateTime.Now:HH:mm:ss}";
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## See Also
                
                - [State Management](app://hooks/core/use-state) - Managing component state
                - [Rules of Hooks](app://hooks/rules-of-hooks) - Understanding hook rules and best practices
                - [Memoization](app://hooks/core/use-memo) - Optimizing performance with memoization
                - [UseCallback](app://hooks/core/use-callback) - Memoizing callback functions
                - [Signals](app://hooks/core/use-signal) - Reactive state management
                - [Views](app://onboarding/concepts/views) - Understanding Ivy views and components
                
                ## Faq
                """").OnLinkClick(onLinkClick)
            | new Expandable("Why does my System.Timers.Timer keep firing after I dispose it in UseEffect cleanup?",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    `System.Timers.Timer.Dispose()` does not cancel callbacks that are already queued on the thread pool. This means 1–2 additional `Elapsed` events can fire *after* disposal, causing state updates on an unmounted or paused component.
                    
                    **Recommended: Use `UseInterval`** for most timer needs. It handles the lifecycle correctly:
                    """").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var seconds = UseState(0);
                    var isRunning = UseState(false);
                    
                    // Timer starts when isRunning is true, stops when false
                    UseInterval(() =>
                    {
                        seconds.Set(seconds.Value + 1);
                    }, isRunning.Value ? TimeSpan.FromSeconds(1) : null);
                    """",Languages.Csharp)
                | new Markdown(
                    """"
                    Pass `null` to pause the timer, or a `TimeSpan` to start/resume it. The timer is automatically disposed on component unmount.
                    
                    **For advanced cases:** If you need a raw `System.Timers.Timer` in UseEffect, use `TimerDisposable` with a `CancellationTokenSource` guard:
                    """").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    using Ivy.Core.Helpers;
                    
                    UseEffect(() =>
                    {
                        var cts = new CancellationTokenSource();
                        var timer = new System.Timers.Timer(1000);
                        timer.Elapsed += (s, e) =>
                        {
                            if (cts.Token.IsCancellationRequested) return;
                            counter.Set(counter.Value + 1);
                        };
                        timer.AutoReset = true;
                        timer.Start();
                        return new TimerDisposable(timer, cts);
                    }, isRunning);
                    """",Languages.Csharp)
                | new Markdown("`TimerDisposable` ensures `Cancel()` → `Stop()` → `Dispose()` ordering, providing a hard barrier against post-disposal callbacks.").OnLinkClick(onLinkClick)
            )
            | new Expandable("How do I clean up resources (timers, subscriptions) in UseEffect?",
                Vertical().Gap(4)
                | new Markdown("Return an `IDisposable` from the UseEffect callback. For simple cases, return the resource directly. For custom cleanup logic, use `Disposable.Create()` from `System.Reactive.Disposables`:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    // Simple: return the disposable resource directly
                    UseEffect(() =>
                    {
                        var timer = new System.Threading.Timer(_ =>
                        {
                            counter.Set(counter.Value + 1);
                        }, null, 0, 1000);
                    
                        return timer; // Timer implements IDisposable — returned for cleanup
                    }, dependencies);
                    """",Languages.Csharp)
                | new CodeBlock(
                    """"
                    // Custom cleanup: use Disposable.Create() from System.Reactive
                    using System.Reactive.Disposables;
                    
                    UseEffect(() =>
                    {
                        var timer = new System.Threading.Timer(_ =>
                        {
                            counter.Set(counter.Value + 1);
                        }, null, 0, 1000);
                    
                        return Disposable.Create(() =>
                        {
                            timer?.Dispose();
                            // additional cleanup logic here
                        });
                    }, dependencies);
                    """",Languages.Csharp)
                | new Markdown(
                    """"
                    **Important:** `Disposable.Create()` requires `using System.Reactive.Disposables;`. System.Reactive is included as a transitive dependency of Ivy Framework — you do NOT need to add a NuGet package, just the using statement.
                    
                    For cancellation-based cleanup, use a `CancellationTokenSource`:
                    """").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    UseEffect(() =>
                    {
                        var cts = new CancellationTokenSource();
                        StartBackgroundWork(cts.Token);
                        return cts; // CancellationTokenSource implements IDisposable
                    }, dependencies);
                    """",Languages.Csharp)
            )
            | new Expandable("Why does my UseEffect fire multiple times?",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    `UseEffect` with `AfterChange` triggers (state dependencies) fires once per `Set()` call on the watched state. If the state is updated multiple times in quick succession (e.g., file upload status transitions), the effect runs for each update.
                    
                    Use a guard pattern to prevent duplicate processing:
                    """").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var processedFile = UseRef<string?>(null);
                    var uploadedFile = UseState<FileUpload?>(null);
                    
                    UseEffect(() =>
                    {
                        var file = uploadedFile.Value;
                        if (file == null) return;
                        if (processedFile.Value == file.FileName) return; // Guard: already processed
                        processedFile.Value = file.FileName;
                    
                        // Process file and show toast
                        alert.Toast($"Loaded {file.FileName}");
                    }, uploadedFile);
                    """",Languages.Csharp)
                | new Markdown(
                    """"
                    Key points:
                    
                    - Use `UseRef` to track processed state without triggering re-renders
                    - Always check if the meaningful value actually changed before taking action
                    - For file uploads, guard on the file name or a unique identifier
                    """").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.RulesOfHooksApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.TasksAndObservablesApp), typeof(Hooks.Core.UseRefApp), typeof(Hooks.Core.UseMemoApp), typeof(Hooks.Core.UseCallbackApp), typeof(Hooks.Core.UseSignalApp)]; 
        return article;
    }
}


public class BasicEffectView : ViewBase
{
    public override object? Build()
    {
        var message = UseState("Click the button to load data");
        var loadTrigger = UseState(0);
        
        // Effect runs when loadTrigger state changes
        UseEffect(async () =>
        {
            if (loadTrigger.Value == 0) return; // Skip initial render
            
            message.Set("Loading...");
            await Task.Delay(2000); // Simulate API call
            message.Set("Data loaded!");
        }, loadTrigger);
        
        return Layout.Vertical()
            | new Button("Load Data", () => loadTrigger.Set(loadTrigger.Value + 1))
            | Text.P(message.Value);
    }
}

public class DependentEffectView : ViewBase
{
    public override object? Build()
    {
        var count = UseState(0);
        var message = UseState("Count: 0");
        
        UseEffect(() =>
        {
            message.Set($"Count changed to: {count.Value}");
        }, count);
        
        return Layout.Vertical()
            | new Button($"Count: {count.Value}", 
                () => count.Set(count.Value + 1))
            | Text.P(message.Value);
    }
}

public class MultipleDepsView : ViewBase
{
    public override object? Build()
    {
        var firstName = UseState("John");
        var lastName = UseState("Doe");
        var fullName = UseState("");
        
        UseEffect(() =>
        {
            fullName.Set($"{firstName.Value} {lastName.Value}");
        }, firstName, lastName);
        
        return Layout.Vertical()
            | (Layout.Horizontal()
                | new Button($"First: {firstName.Value}", 
                    () => firstName.Set(firstName.Value == "John" ? "Jane" : "John"))
                | new Button($"Last: {lastName.Value}", 
                    () => lastName.Set(lastName.Value == "Doe" ? "Smith" : "Doe")))
            | Text.P($"Full name: {fullName.Value}");
    }
}

public class DataFetchView : ViewBase
{
    public override object? Build()
    {
        var data = UseState<List<Item>?>();
        var loading = UseState(false);
        var loadTrigger = UseState(0);
        
        UseEffect(async () =>
        {
            if (loadTrigger.Value == 0) return; // Skip initial render
            
            loading.Set(true);
            
            // Simulate API call - exceptions automatically handled by Ivy
            await Task.Delay(1500);
            var items = new List<Item>
            {
                new("Item 1", "Description 1"),
                new("Item 2", "Description 2"),
                new("Item 3", "Description 3")
            };
            
            data.Set(items);
            loading.Set(false);
        }, loadTrigger);
        
        return Layout.Vertical()
            | new Button("Fetch Data", () => loadTrigger.Set(loadTrigger.Value + 1))
            | (loading.Value 
                ? Text.P("Loading data...") 
                : Layout.Horizontal(
                    data.Value?.Select(item => 
                        new Button($"{item.Name}: {item.Description}")
                    ) ?? Enumerable.Empty<Button>()
                ));
    }
}

public record Item(string Name, string Description);

public class SubscriptionView : ViewBase
{
    public override object? Build()
    {
        var message = UseState("Stopped");
        var isActive = UseState(false);
        var previousResource = UseRef<IDisposable?>(() => null);
        
        UseEffect(() =>
        {
            if (!isActive.Value)
            {
                var hadResource = previousResource.Value != null;
                previousResource.Value?.Dispose();
                previousResource.Value = null;
                if (!hadResource) message.Set("Stopped");
                return System.Reactive.Disposables.Disposable.Empty;
            }
            
            previousResource.Value?.Dispose();
            message.Set("Running");
            
            var resource = new SafeDisposable(() => message.Set("Cleaned up"));
            previousResource.Value = resource;
            return resource;
        }, isActive);
        
        return Layout.Vertical()
            | new Button(isActive.Value ? "Stop" : "Start", 
                () => isActive.Set(!isActive.Value))
            | Text.P($"Status: {message.Value}");
    }
    
    private class SafeDisposable : IDisposable
    {
        private readonly Action _onDispose;
        private bool _isDisposed;
        
        public SafeDisposable(Action onDispose) => _onDispose = onDispose;
        
        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;
            _onDispose();
        }
    }
}

public class ConditionalEffectView : ViewBase
{
    public override object? Build()
    {
        var isEnabled = UseState(false);
        var data = UseState<string?>();
        
        UseEffect(async () =>
        {
            if (!isEnabled.Value)
            {
                data.Set((string)null);
                return;
            }
            
            // Only fetch when enabled
            var result = await FetchData();
            data.Set(result);
        }, isEnabled);
        
        return Layout.Vertical()
            | new Button($"Fetching: {(isEnabled.Value ? "ON" : "OFF")}", 
                onClick: _ => isEnabled.Set(!isEnabled.Value))
            | (data.Value != null ? Text.P(data.Value) : Text.Muted("No data"));
    }
    
    private async Task<string> FetchData()
    {
        await Task.Delay(1000);
        return $"Data fetched at {DateTime.Now:HH:mm:ss}";
    }
}
