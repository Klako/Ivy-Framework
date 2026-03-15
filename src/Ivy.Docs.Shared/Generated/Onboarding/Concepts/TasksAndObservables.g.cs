using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:6, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/06_TasksAndObservables.md", searchHints: ["async", "observables", "streams", "reactive", "tasks", "rx"])]
public class TasksAndObservablesApp(bool onlyBody = false) : ViewBase
{
    public TasksAndObservablesApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("tasks-and-observables", "Tasks and Observables", 1), new ArticleHeading("basic-task-usage", "Basic Task Usage", 2), new ArticleHeading("basic-observable-usage", "Basic Observable Usage", 2), new ArticleHeading("observable-with-state-management", "Observable with State Management", 3), new ArticleHeading("observable-with-throttling", "Observable with Throttling", 3), new ArticleHeading("observable-transformations", "Observable Transformations", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Tasks and Observables").OnLinkClick(onLinkClick)
            | Lead("Handle asynchronous operations and reactive data streams with Tasks and Observables for responsive [application](app://onboarding/concepts/apps) behavior.")
            | new Markdown(
                """"
                Ivy provides powerful abstractions for working with asynchronous operations and reactive data streams. **Tasks** handle one-time asynchronous operations, while **Observables** manage continuous data streams that automatically update the UI when data changes.
                
                ## Basic Task Usage
                
                Tasks represent asynchronous operations that complete once and return a result. Ivy provides `TaskView<T>` to automatically handle loading states and display results. See [UseState](app://hooks/core/use-state) and [UseEffect](app://hooks/core/use-effect) for reactive state patterns.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class TaskExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var task = Task.Run(async () =>
                            {
                                await Task.Delay(2000);
                                return "Task completed successfully!";
                            });
                    
                            return new TaskView<string>(task);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new TaskExample())
            )
            | new Markdown(
                """"
                ## Basic Observable Usage
                
                Ivy's `ObservableView<T>` automatically subscribes and updates the UI as new values arrive. This example uses [UseRef](app://hooks/core/use-ref) to hold the observable and [UseState](app://hooks/core/use-state) to control it.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class TimeBasedObservableExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var isActive = UseState<bool>(false);
                    
                            var timeObservable = UseRef(() =>
                                Observable.Interval(TimeSpan.FromMilliseconds(500))
                                    .Where(_ => isActive.Value)
                                    .Select(_ => DateTime.Now.ToString("HH:mm:ss.fff"))
                            ).Value;
                    
                            return Layout.Vertical(
                                Layout.Horizontal(
                                    new Button("Start", _ => isActive.Value = true),
                                    new Button("Stop", _ => isActive.Value = false)
                                ),
                                Text.Block("Time Updates:"),
                                new ObservableView<string>(timeObservable)
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new TimeBasedObservableExample())
            )
            | new Markdown(
                """"
                ### Observable with State Management
                
                This example demonstrates how to properly manage [state](app://hooks/core/use-state) with observables by controlling when subscriptions are active. It shows a timer-based counter that only increments when a state flag is active, with proper UI updates and subscription cleanup.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StateManagementExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StateManagementExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var counter = UseState<int>(0);
                            var isRunning = UseState<bool>(false);
                    
                            var timerObservable = UseRef(() =>
                                Observable.Interval(TimeSpan.FromSeconds(1))
                            ).Value;
                    
                            UseEffect(() =>
                            {
                                var subscription = timerObservable.Subscribe(_ =>
                                {
                                    if (isRunning.Value)
                                    {
                                        counter.Set(prev => prev + 1);
                                    }
                                });
                                return subscription;
                            });
                    
                            return Layout.Vertical(
                                Layout.Horizontal(
                                    new Button("Start", _ => isRunning.Value = true),
                                    new Button("Stop", _ => isRunning.Value = false),
                                    new Button("Reset", _ => counter.Value = 0)
                                ),
                                Text.Block($"Counter: {counter.Value}"),
                                Text.Block($"Status: {(isRunning.Value ? "Running" : "Stopped")}")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Observable with Throttling
                
                This example demonstrates how to use observables for search functionality with [performance optimizations](app://hooks/core/use-memo). It shows throttled updates to prevent excessive filtering while typing, and proper [state management](app://hooks/core/use-state) to avoid duplicate data.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ObservableSearchExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ObservableSearchExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var inputText = UseState<string>("");
                            var originalItems = UseState<string[]>(new[] { "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape", "Honeydew" });
                            var filteredItems = UseState<string[]>(Array.Empty<string>());
                    
                            UseEffect(() => {
                                filteredItems.Set(originalItems.Value);
                            }, []);
                    
                            var searchObservable = UseRef(() =>
                                Observable.Interval(TimeSpan.FromMilliseconds(300))
                                    .Select(_ => inputText.Value)
                                    .DistinctUntilChanged()
                            ).Value;
                    
                            UseEffect(() =>
                            {
                                return searchObservable.Subscribe(searchTerm =>
                                {
                                    if (string.IsNullOrWhiteSpace(searchTerm))
                                    {
                                        filteredItems.Set(originalItems.Value);
                                    }
                                    else
                                    {
                                        var filtered = originalItems.Value
                                            .Where(item => item.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                                            .ToArray();
                                        filteredItems.Set(filtered);
                                    }
                                });
                            });
                    
                            return Layout.Vertical(
                                Text.Block("Observable Search: "),
                                Layout.Horizontal(
                                    new TextInput(inputText, placeholder: "Type to filter (throttled)..."),
                                    new Button("Clear", _ => inputText.Set(""))
                                ),
                                Text.Block($"Found {filteredItems.Value.Length} of {originalItems.Value.Length} items"),
                                Layout.Vertical(
                                    filteredItems.Value.Select(item =>
                                        Text.Block(item)
                                    )
                                )
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Observable Transformations
                
                This example demonstrates interactive data transformation with immediate feedback. It demonstrates filtering, projection, and limiting operations to create processed transformed results.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TransformationExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TransformationExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var generatedData = UseState<int[]>(Array.Empty<int>());
                            var lastTransformed = UseState<int[]>(Array.Empty<int>());
                    
                            void GenerateNewData(Event<Button> _)
                            {
                                var random = new Random();
                                var newData = Enumerable.Range(1, 10)
                                    .Select(_ => random.Next(1, 21))
                                    .ToArray();
                    
                                generatedData.Set(newData);
                    
                                var immediateResult = newData
                                    .Where(num => num % 2 == 0)
                                    .Select(num => num * 2)
                                    .Take(5)
                                    .ToArray();
                                lastTransformed.Set(immediateResult);
                            }
                    
                            return Layout.Vertical(
                                new Button("Generate New Data", GenerateNewData),
                                Text.Block("Generated data: " + string.Join(", ", generatedData.Value)),
                                Text.Block("Last Generated Result: " + string.Join(", ", lastTransformed.Value))
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseRefApp), typeof(Hooks.Core.UseMemoApp)]; 
        return article;
    }
}


public class TaskExample : ViewBase
{
    public override object? Build()
    {
        var task = Task.Run(async () =>
        {
            await Task.Delay(2000); 
            return "Task completed successfully!";
        });

        return new TaskView<string>(task);
    }
}

public class TimeBasedObservableExample : ViewBase
{
    public override object? Build()
    {
        var isActive = UseState<bool>(false);

        var timeObservable = UseRef(() =>
            Observable.Interval(TimeSpan.FromMilliseconds(500))
                .Where(_ => isActive.Value)
                .Select(_ => DateTime.Now.ToString("HH:mm:ss.fff"))
        ).Value;

        return Layout.Vertical(
            Layout.Horizontal(
                new Button("Start", _ => isActive.Value = true),
                new Button("Stop", _ => isActive.Value = false)
            ),
            Text.Block("Time Updates:"),
            new ObservableView<string>(timeObservable)
        );
    }
}

public class StateManagementExample : ViewBase
{
    public override object? Build()
    {
        var counter = UseState<int>(0);
        var isRunning = UseState<bool>(false);
        
        var timerObservable = UseRef(() =>
            Observable.Interval(TimeSpan.FromSeconds(1))
        ).Value;

        UseEffect(() =>
        {
            var subscription = timerObservable.Subscribe(_ =>
            {
                if (isRunning.Value)
                {
                    counter.Set(prev => prev + 1);
                }
            });
            return subscription;
        }); 

        return Layout.Vertical(
            Layout.Horizontal(
                new Button("Start", _ => isRunning.Value = true),
                new Button("Stop", _ => isRunning.Value = false),
                new Button("Reset", _ => counter.Value = 0)
            ),
            Text.Block($"Counter: {counter.Value}"),
            Text.Block($"Status: {(isRunning.Value ? "Running" : "Stopped")}")
        );
    }
}

public class ObservableSearchExample : ViewBase
{
    public override object? Build()
    {
        var inputText = UseState<string>("");
        var originalItems = UseState<string[]>(new[] { "Apple", "Banana", "Cherry", "Date", "Elderberry", "Fig", "Grape", "Honeydew" });
        var filteredItems = UseState<string[]>(Array.Empty<string>());
        
        UseEffect(() => {
            filteredItems.Set(originalItems.Value);
        }, []); 
        
        var searchObservable = UseRef(() =>
            Observable.Interval(TimeSpan.FromMilliseconds(300))
                .Select(_ => inputText.Value)
                .DistinctUntilChanged()
        ).Value;

        UseEffect(() =>
        {
            return searchObservable.Subscribe(searchTerm =>
            {   
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    filteredItems.Set(originalItems.Value);
                }
                else
                {
                    var filtered = originalItems.Value
                        .Where(item => item.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToArray();
                    filteredItems.Set(filtered);
                }
            });
        });

        return Layout.Vertical(
            Text.Block("Observable Search: "),
            Layout.Horizontal(
                new TextInput(inputText, placeholder: "Type to filter (throttled)..."),
                new Button("Clear", _ => inputText.Set(""))
            ),
            Text.Block($"Found {filteredItems.Value.Length} of {originalItems.Value.Length} items"),
            Layout.Vertical(
                filteredItems.Value.Select(item => 
                    Text.Block(item)
                )
            )
        );
    }
}

public class TransformationExample : ViewBase
{
    public override object? Build()
    {
        var generatedData = UseState<int[]>(Array.Empty<int>());
        var lastTransformed = UseState<int[]>(Array.Empty<int>());

        void GenerateNewData(Event<Button> _)
        {
            var random = new Random();
            var newData = Enumerable.Range(1, 10)
                .Select(_ => random.Next(1, 21))
                .ToArray();
            
            generatedData.Set(newData);
            
            var immediateResult = newData
                .Where(num => num % 2 == 0)
                .Select(num => num * 2)
                .Take(5)
                .ToArray();
            lastTransformed.Set(immediateResult);
        }

        return Layout.Vertical(
            new Button("Generate New Data", GenerateNewData),
            Text.Block("Generated data: " + string.Join(", ", generatedData.Value)),
            Text.Block("Last Generated Result: " + string.Join(", ", lastTransformed.Value))
        );
    }
}
