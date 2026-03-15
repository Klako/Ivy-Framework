using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Charts;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/06_Charts/05_RadialBarChart.md", searchHints: ["visualization", "graph", "analytics", "data", "radial", "circular", "progress", "gauge", "statistics"])]
public class RadialBarChartApp(bool onlyBody = false) : ViewBase
{
    public RadialBarChartApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("radialbarchart", "RadialBarChart", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("progress-tracking-apple-watch-style", "Progress Tracking (Apple Watch Style)", 2), new ArticleHeading("gauge-like-displays", "Gauge-Like Displays", 2), new ArticleHeading("configuration-options", "Configuration Options", 2), new ArticleHeading("radius-control", "Radius Control", 3), new ArticleHeading("angular-range", "Angular Range", 3), new ArticleHeading("background-tracks", "Background Tracks", 3), new ArticleHeading("polar-grid", "Polar Grid", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# RadialBarChart").OnLinkClick(onLinkClick)
            | Lead("Display categorical data as concentric arcs radiating from a center point, perfect for progress tracking, KPI dashboards, and gauge-like visualizations.")
            | new Markdown(
                """"
                `RadialBarChart` displays data as concentric circular bars, where each category occupies its own ring. Unlike PieChart which shows proportions of a whole, RadialBarChart compares independent values that each have their own track. Build chart [views](app://onboarding/concepts/views) inside [layouts](app://onboarding/concepts/layout) and use [state](app://hooks/core/use-state) for dynamic data. See [Charts](app://onboarding/concepts/charts) for an overview of Ivy chart widgets.
                
                ## Basic Usage
                
                Create a basic radial bar chart by providing data and configuring the radial bars. Each value is rendered as a concentric arc with its own radius.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicRadialBarChartView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicRadialBarChartView : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Category = "Completed", Value = 85 },
                                new { Category = "In Progress", Value = 65 },
                                new { Category = "Pending", Value = 45 },
                                new { Category = "Blocked", Value = 25 }
                            };
                    
                            return new Card().Title("Task Status")
                                | new RadialBarChart(data)
                                    .ColorScheme(ColorScheme.Default)
                                    .RadialBar("Value")
                                    .Tooltip()
                                    .Legend()
                            ;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Progress Tracking (Apple Watch Style)
                
                Radial bar charts excel at displaying progress toward goals. Use `Background(true)` to show unfilled portions and adjust the angular range to create activity ring-style visualizations.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FitnessGoalsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FitnessGoalsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Goal = "Move", Progress = 78 },
                                new { Goal = "Exercise", Progress = 62 },
                                new { Goal = "Stand", Progress = 90 }
                            };
                    
                            return new Card().Title("Daily Fitness Goals")
                                | new RadialBarChart(data)
                                    .ColorScheme(ColorScheme.Default)
                                    .RadialBar(new RadialBar("Progress").Background(true))
                                    .InnerRadius("20%")
                                    .OuterRadius("90%")
                                    .StartAngle(90)
                                    .EndAngle(450)
                                    .Tooltip()
                            ;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                The `StartAngle` and `EndAngle` properties control the arc span. Setting StartAngle to 90 and EndAngle to 450 creates a 360-degree ring starting from the top (matching Apple Watch's design).
                
                ## Gauge-Like Displays
                
                Create gauge-style visualizations for KPIs by adjusting the angular range and using background tracks. This is ideal for dashboard metrics where you want to show current values against maximum capacity.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new KPIDashboardView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class KPIDashboardView : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Metric = "Performance", Score = 88 },
                                new { Metric = "Quality", Score = 92 },
                                new { Metric = "Efficiency", Score = 75 },
                                new { Metric = "Satisfaction", Score = 85 }
                            };
                    
                            return new Card().Title("KPI Dashboard")
                                | new RadialBarChart(data)
                                    .ColorScheme(ColorScheme.Default)
                                    .RadialBar(new RadialBar("Score").Background(true).Animated(true))
                                    .StartAngle(-90)
                                    .EndAngle(270)
                                    .InnerRadius("30%")
                                    .OuterRadius("80%")
                                    .Tooltip()
                                    .Legend()
                                    .Toolbox()
                            ;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Configuration Options
                
                ### Radius Control
                
                Control the size and spacing of the concentric rings:
                
                - `InnerRadius`: Sets the inner radius (percentage or pixels)
                - `OuterRadius`: Sets the outer radius (percentage or pixels)
                - `BarGap`: Space between bars in pixels (default: 4)
                
                ### Angular Range
                
                Control the arc span with `StartAngle` and `EndAngle`:
                
                - Full circle: `StartAngle(0).EndAngle(360)`
                - Three-quarter gauge: `StartAngle(-90).EndAngle(270)`
                - Activity rings: `StartAngle(90).EndAngle(450)`
                
                ### Background Tracks
                
                Enable background tracks with `.Background(true)` on individual RadialBar configurations to show the unfilled portion of each ring. This is useful for progress indicators and goal tracking.
                
                ### Polar Grid
                
                Customize the polar grid appearance:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                .PolarGrid(new PolarGrid()
                    .GridType(PolarGridTypes.Circle)
                    .RadialLines(true))
                """",Languages.Csharp)
            | new Markdown(
                """"
                Available grid types:
                
                - `PolarGridTypes.Polygon`: Polygonal grid (default)
                - `PolarGridTypes.Circle`: Circular grid
                """").OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.RadialBarChart", "Ivy.RadialBarChartExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Charts/RadialBarChart.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.ChartsApp)]; 
        return article;
    }
}


public class BasicRadialBarChartView : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Category = "Completed", Value = 85 },
            new { Category = "In Progress", Value = 65 },
            new { Category = "Pending", Value = 45 },
            new { Category = "Blocked", Value = 25 }
        };

        return new Card().Title("Task Status")
            | new RadialBarChart(data)
                .ColorScheme(ColorScheme.Default)
                .RadialBar("Value")
                .Tooltip()
                .Legend()
        ;
    }
}

public class FitnessGoalsView : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Goal = "Move", Progress = 78 },
            new { Goal = "Exercise", Progress = 62 },
            new { Goal = "Stand", Progress = 90 }
        };

        return new Card().Title("Daily Fitness Goals")
            | new RadialBarChart(data)
                .ColorScheme(ColorScheme.Default)
                .RadialBar(new RadialBar("Progress").Background(true))
                .InnerRadius("20%")
                .OuterRadius("90%")
                .StartAngle(90)
                .EndAngle(450)
                .Tooltip()
        ;
    }
}

public class KPIDashboardView : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Metric = "Performance", Score = 88 },
            new { Metric = "Quality", Score = 92 },
            new { Metric = "Efficiency", Score = 75 },
            new { Metric = "Satisfaction", Score = 85 }
        };

        return new Card().Title("KPI Dashboard")
            | new RadialBarChart(data)
                .ColorScheme(ColorScheme.Default)
                .RadialBar(new RadialBar("Score").Background(true).Animated(true))
                .StartAngle(-90)
                .EndAngle(270)
                .InnerRadius("30%")
                .OuterRadius("80%")
                .Tooltip()
                .Legend()
                .Toolbox()
        ;
    }
}
