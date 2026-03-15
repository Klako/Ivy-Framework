using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Hooks.Core;

[App(order:18, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/03_Hooks/02_Core/18_UseInterval.md", searchHints: ["useinterval", "timer", "interval", "polling", "countdown", "setinterval"])]
public class UseIntervalApp(bool onlyBody = false) : ViewBase
{
    public UseIntervalApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("useinterval", "UseInterval", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("pausing-and-resuming", "Pausing and Resuming", 2), new ArticleHeading("polling", "Polling", 2), new ArticleHeading("variable-rate-interval", "Variable-Rate Interval", 2), new ArticleHeading("how-it-works", "How It Works", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# UseInterval").OnLinkClick(onLinkClick)
            | Lead("Run a callback on a repeating interval with safe timer lifecycle management. Pass `null` to pause, or a `TimeSpan` to start.")
            | new Markdown(
                """"
                `UseInterval` is a built-in hook that handles `System.Timers.Timer` lifecycle correctly, eliminating the common race condition where timer callbacks fire after disposal.
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var seconds = UseState(0);
                var isRunning = UseState(true);
                
                // Timer ticks every second while isRunning is true
                UseInterval(() =>
                {
                    seconds.Set(seconds.Value + 1);
                }, isRunning.Value ? TimeSpan.FromSeconds(1) : null);
                """",Languages.Csharp)
            | new Markdown(
                """"
                When `interval` is `null`, the timer is stopped and disposed. When a non-null `TimeSpan` is provided, the timer is started (or restarted if the interval changed).
                
                ## Pausing and Resuming
                
                Control the timer by toggling the interval between a value and `null`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StopwatchView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StopwatchView : ViewBase
                    {
                        public override object? Build()
                        {
                            var seconds = UseState(0);
                            var isRunning = UseState(false);
                    
                            UseInterval(() =>
                            {
                                seconds.Set(seconds.Value + 1);
                            }, isRunning.Value ? TimeSpan.FromSeconds(1) : null);
                    
                            return Layout.Vertical()
                                | Text.H3($"{seconds.Value}s")
                                | (Layout.Horizontal()
                                    | new Button(isRunning.Value ? "Pause" : "Start",
                                        () => isRunning.Set(!isRunning.Value))
                                    | new Button("Reset", () => seconds.Set(0)));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Polling
                
                Use `UseInterval` to periodically fetch data:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var data = UseState<string?>(null);
                
                UseInterval(() =>
                {
                    data.Set(FetchLatestData());
                }, TimeSpan.FromSeconds(30));
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Variable-Rate Interval
                
                Change the interval dynamically based on state:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var remaining = UseState(60);
                var speed = UseState(1.0);
                
                UseInterval(() =>
                {
                    remaining.Set(Math.Max(0, remaining.Value - 1));
                }, remaining.Value > 0 ? TimeSpan.FromSeconds(speed.Value) : null);
                """",Languages.Csharp)
            | new Markdown(
                """"
                When `remaining` reaches zero, passing `null` stops the timer automatically.
                
                ## How It Works
                
                Internally, `UseInterval` uses `System.Timers.Timer` with a `CancellationTokenSource` barrier. When the interval changes or the component unmounts:
                
                1. The cancellation token is cancelled (post-disposal callbacks become no-ops)
                2. The timer is stopped (no new callbacks are queued)
                3. The timer is disposed (resources are released)
                
                This three-step cleanup prevents the race condition where `System.Timers.Timer.Dispose()` alone allows already-queued thread pool callbacks to execute.
                
                ## See Also
                
                - [UseEffect](app://hooks/core/use-effect) - For general side effects and custom cleanup
                - [State Management](app://hooks/core/use-state) - Managing component state
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseEffectApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}


public class StopwatchView : ViewBase
{
    public override object? Build()
    {
        var seconds = UseState(0);
        var isRunning = UseState(false);

        UseInterval(() =>
        {
            seconds.Set(seconds.Value + 1);
        }, isRunning.Value ? TimeSpan.FromSeconds(1) : null);

        return Layout.Vertical()
            | Text.H3($"{seconds.Value}s")
            | (Layout.Horizontal()
                | new Button(isRunning.Value ? "Pause" : "Start",
                    () => isRunning.Set(!isRunning.Value))
                | new Button("Reset", () => seconds.Set(0)));
    }
}
