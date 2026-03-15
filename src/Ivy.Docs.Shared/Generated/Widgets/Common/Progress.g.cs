using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:10, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/10_Progress.md", searchHints: ["loading", "percentage", "bar", "indicator", "status", "completion"])]
public class ProgressApp(bool onlyBody = false) : ViewBase
{
    public ProgressApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("progress", "Progress", 1), new ArticleHeading("example", "Example", 2), new ArticleHeading("indeterminate-mode", "Indeterminate Mode", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Progress").OnLinkClick(onLinkClick)
            | Lead("Show task completion status with customizable progress bars that support dynamic updates and multiple [color variants](app://onboarding/concepts/theming).")
            | new Markdown(
                """"
                The `Progress` [widget](app://onboarding/concepts/widgets) is used to visually represent the completion status of a task or process. It displays a visual progress bar that can be customized with different color variants and can be bound to [state](app://hooks/core/use-state) for dynamic updates.
                
                ## Example
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ProgressDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ProgressDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var progress = UseState(25);
                    
                            return Layout.Vertical(
                                new Progress(progress.Value).Goal($"{progress.Value}% Complete"),
                                Layout.Horizontal(
                                    new Button("0%", _ => progress.Set(0)),
                                    new Button("25%", _ => progress.Set(25)),
                                    new Button("50%", _ => progress.Set(50)),
                                    new Button("75%", _ => progress.Set(75)),
                                    new Button("100%", _ => progress.Set(100))
                                )
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Indeterminate Mode
                
                Use the `Indeterminate` property to display an animated progress bar when the completion percentage is unknown. This is useful for tasks like file uploads, API calls, or any operation where you can't determine the exact progress.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new IndeterminateProgressDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class IndeterminateProgressDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var isLoading = UseState(true);
                            var progress = UseState(0);
                    
                            return Layout.Vertical(
                                // Basic indeterminate progress
                                new Progress().Indeterminate().Goal("Loading..."),
                    
                                // Toggle between indeterminate and determinate
                                new Progress(progress.Value)
                                    .Indeterminate(isLoading.Value)
                                    .Goal(isLoading.Value ? "Syncing..." : $"{progress.Value}% Complete"),
                    
                                Layout.Horizontal(
                                    new Button("Toggle Loading", _ => isLoading.Set(!isLoading.Value)),
                                    new Button("Set 50%", _ => progress.Set(50))
                                )
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("The indeterminate animation respects the user's `prefers-reduced-motion` setting — when active, a static appearance is shown instead of the sliding animation.").OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.Progress", "Ivy.ProgressExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Progress.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ThemingApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}


public class ProgressDemo : ViewBase
{
    public override object? Build()
    {
        var progress = UseState(25);

        return Layout.Vertical(
            new Progress(progress.Value).Goal($"{progress.Value}% Complete"),
            Layout.Horizontal(
                new Button("0%", _ => progress.Set(0)),
                new Button("25%", _ => progress.Set(25)),
                new Button("50%", _ => progress.Set(50)),
                new Button("75%", _ => progress.Set(75)),
                new Button("100%", _ => progress.Set(100))
            )
        );
    }
}

public class IndeterminateProgressDemo : ViewBase
{
    public override object? Build()
    {
        var isLoading = UseState(true);
        var progress = UseState(0);

        return Layout.Vertical(
            // Basic indeterminate progress
            new Progress().Indeterminate().Goal("Loading..."),

            // Toggle between indeterminate and determinate
            new Progress(progress.Value)
                .Indeterminate(isLoading.Value)
                .Goal(isLoading.Value ? "Syncing..." : $"{progress.Value}% Complete"),

            Layout.Horizontal(
                new Button("Toggle Loading", _ => isLoading.Set(!isLoading.Value)),
                new Button("Set 50%", _ => progress.Set(50))
            )
        );
    }
}
