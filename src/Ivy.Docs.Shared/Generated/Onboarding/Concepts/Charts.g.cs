using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:18, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/18_Charts.md", searchHints: ["visualization", "graph", "analytics", "data", "trends", "statistics", "charts", "sorting"])]
public class ChartsApp(bool onlyBody = false) : ViewBase
{
    public ChartsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("charts", "Charts", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("sorting", "Sorting", 2), new ArticleHeading("simple-sorting", "Simple Sorting", 3), new ArticleHeading("custom-key-sorting", "Custom Key Sorting", 3), new ArticleHeading("styling", "Styling", 2), new ArticleHeading("color-schemes", "Color Schemes", 3), new ArticleHeading("common-methods", "Common Methods", 3), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("how-do-i-pass-data-to-a-chart", "How do I pass data to a chart?", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Charts").OnLinkClick(onLinkClick)
            | Lead("Create interactive charts for data visualization using the Chart Builders API.")
            | new Markdown(
                """"
                ## Basic Usage
                
                The simplest way to create a chart is to call a builder method like `.ToLineChart()` on your data. Use `.Dimension()` to define the X-axis grouping and `.Measure()` for Y-axis values with aggregation.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicChartExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var salesData = new[]
                            {
                                new { Month = "Jan", Sales = 186 },
                                new { Month = "Feb", Sales = 305 },
                                new { Month = "Mar", Sales = 237 },
                                new { Month = "Apr", Sales = 289 }
                            };
                    
                            return salesData.ToLineChart()
                                .Dimension("Month", e => e.Month)
                                .Measure("Sales", e => e.Sum(f => f.Sales))
                                .Toolbox();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicChartExample())
            )
            | new Markdown(
                """"
                Ivy supports four chart types — each optimized for different visualization needs:
                
                | Chart | Best For | Builder Method |
                |-------|----------|----------------|
                | [LineChart](app://widgets/charts/line-chart) | Trends over time | `.ToLineChart()` |
                | [BarChart](app://widgets/charts/bar-chart) | Comparing categories | `.ToBarChart()` |
                | [AreaChart](app://widgets/charts/area-chart) | Cumulative data | `.ToAreaChart()` |
                | [PieChart](app://widgets/charts/pie-chart) | Parts of a whole | `.ToPieChart()` |
                
                ## Sorting
                
                By default, chart data appears in the order it exists in your data source. Use `SortBy` to control X-axis ordering.
                
                ### Simple Sorting
                
                Sort alphabetically or lexicographically:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SimpleSortDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SimpleSortDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Label = "Cherry", Value = 30 },
                                new { Label = "Apple", Value = 10 },
                                new { Label = "Banana", Value = 20 }
                            };
                    
                            return Layout.Horizontal().Gap(4)
                                | (Layout.Vertical()
                                    | Text.P("No Sorting").Small()
                                    | data.ToBarChart()
                                        .Dimension("Label", e => e.Label)
                                        .Measure("Value", e => e.Sum(f => f.Value)))
                                | (Layout.Vertical()
                                    | Text.P("Ascending").Small()
                                    | data.ToBarChart()
                                        .Dimension("Label", e => e.Label)
                                        .Measure("Value", e => e.Sum(f => f.Value))
                                        .SortBy(SortOrder.Ascending));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Custom Key Sorting
                
                For numeric strings or dates, specify how values should be interpreted:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CustomSortDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CustomSortDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Label = "1", Value = 10 },
                                new { Label = "10", Value = 100 },
                                new { Label = "2", Value = 20 }
                            };
                    
                            return Layout.Horizontal().Gap(4)
                                | (Layout.Vertical()
                                    | Text.P("Lexicographic: 1, 10, 2").Small()
                                    | data.ToLineChart()
                                        .Dimension("Label", e => e.Label)
                                        .Measure("Value", e => e.Sum(f => f.Value))
                                        .SortBy(SortOrder.Ascending))
                                | (Layout.Vertical()
                                    | Text.P("Numeric: 1, 2, 10").Small()
                                    | data.ToLineChart()
                                        .Dimension("Label", e => e.Label)
                                        .Measure("Value", e => e.Sum(f => f.Value))
                                        .SortBy(e => int.Parse(e.Label), SortOrder.Ascending));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                **SortOrder options:**
                
                - `SortOrder.None` — no sorting (default)
                - `SortOrder.Ascending` — A→Z, smallest→largest
                - `SortOrder.Descending` — Z→A, largest→smallest
                """").OnLinkClick(onLinkClick)
            | new Callout("`SortBy` is available for LineChart, BarChart, and AreaChart. PieChart doesn't use X-axis sorting.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Styling
                
                ### Color Schemes
                
                Control the color palette for chart series:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ColorSchemeDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ColorSchemeDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Month = "Jan", A = 100, B = 80, C = 60 },
                                new { Month = "Feb", A = 120, B = 90, C = 70 },
                                new { Month = "Mar", A = 140, B = 100, C = 80 }
                            };
                    
                            return Layout.Horizontal().Gap(4)
                                | (Layout.Vertical()
                                    | Text.P("Default").Small()
                                    | new BarChart(data)
                                        .Bar("A").Bar("B").Bar("C")
                                        .ColorScheme(ColorScheme.Default)
                                        .Legend())
                                | (Layout.Vertical()
                                    | Text.P("Rainbow").Small()
                                    | new BarChart(data)
                                        .Bar("A").Bar("B").Bar("C")
                                        .ColorScheme(ColorScheme.Rainbow)
                                        .Legend());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Common Methods
                
                All Cartesian charts (Line, Bar, Area) share these methods:
                
                | Method | Description |
                |--------|-------------|
                | `.CartesianGrid()` | Add grid lines (`.Horizontal()`, `.Vertical()`) |
                | `.XAxis()` | Configure X-axis (`.Label<XAxis>("text")`) |
                | `.YAxis()` | Configure Y-axis (`.Label<YAxis>("text")`) |
                | `.Legend()` | Show legend (`.Layout()`, `.VerticalAlign()`) |
                | `.Tooltip()` | Enable hover tooltips |
                | `.Toolbox()` | Add interactive toolbox |
                | `.Height()` / `.Width()` | Set chart dimensions |
                
                ## Faq
                
                ### How do I pass data to a chart?
                
                Always use the builder pattern extension methods (`.ToLineChart()`, `.ToBarChart()`, `.ToAreaChart()`, `.ToPieChart()`) on your data collection. Do NOT construct charts manually with `List<dynamic>`. Anonymous types work correctly with the builder pattern:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var data = new[] { new { Month = "Jan", Sales = 100 } };
                return data.ToLineChart()
                    .Dimension("Month", e => e.Month)
                    .Measure("Sales", e => e.Sum(f => f.Sales));
                """",Languages.Csharp)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Charts.LineChartApp), typeof(Widgets.Charts.BarChartApp), typeof(Widgets.Charts.AreaChartApp), typeof(Widgets.Charts.PieChartApp)]; 
        return article;
    }
}


public class BasicChartExample : ViewBase
{
    public override object? Build()
    {
        var salesData = new[]
        {
            new { Month = "Jan", Sales = 186 },
            new { Month = "Feb", Sales = 305 },
            new { Month = "Mar", Sales = 237 },
            new { Month = "Apr", Sales = 289 }
        };

        return salesData.ToLineChart()
            .Dimension("Month", e => e.Month)
            .Measure("Sales", e => e.Sum(f => f.Sales))
            .Toolbox();
    }
}

public class SimpleSortDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Label = "Cherry", Value = 30 },
            new { Label = "Apple", Value = 10 },
            new { Label = "Banana", Value = 20 }
        };

        return Layout.Horizontal().Gap(4)
            | (Layout.Vertical()
                | Text.P("No Sorting").Small()
                | data.ToBarChart()
                    .Dimension("Label", e => e.Label)
                    .Measure("Value", e => e.Sum(f => f.Value)))
            | (Layout.Vertical()
                | Text.P("Ascending").Small()
                | data.ToBarChart()
                    .Dimension("Label", e => e.Label)
                    .Measure("Value", e => e.Sum(f => f.Value))
                    .SortBy(SortOrder.Ascending));
    }
}

public class CustomSortDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Label = "1", Value = 10 },
            new { Label = "10", Value = 100 },
            new { Label = "2", Value = 20 }
        };

        return Layout.Horizontal().Gap(4)
            | (Layout.Vertical()
                | Text.P("Lexicographic: 1, 10, 2").Small()
                | data.ToLineChart()
                    .Dimension("Label", e => e.Label)
                    .Measure("Value", e => e.Sum(f => f.Value))
                    .SortBy(SortOrder.Ascending))
            | (Layout.Vertical()
                | Text.P("Numeric: 1, 2, 10").Small()
                | data.ToLineChart()
                    .Dimension("Label", e => e.Label)
                    .Measure("Value", e => e.Sum(f => f.Value))
                    .SortBy(e => int.Parse(e.Label), SortOrder.Ascending));
    }
}

public class ColorSchemeDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Month = "Jan", A = 100, B = 80, C = 60 },
            new { Month = "Feb", A = 120, B = 90, C = 70 },
            new { Month = "Mar", A = 140, B = 100, C = 80 }
        };

        return Layout.Horizontal().Gap(4)
            | (Layout.Vertical()
                | Text.P("Default").Small()
                | new BarChart(data)
                    .Bar("A").Bar("B").Bar("C")
                    .ColorScheme(ColorScheme.Default)
                    .Legend())
            | (Layout.Vertical()
                | Text.P("Rainbow").Small()
                | new BarChart(data)
                    .Bar("A").Bar("B").Bar("C")
                    .ColorScheme(ColorScheme.Rainbow)
                    .Legend());
    }
}
