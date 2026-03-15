using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Charts;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/06_Charts/01_LineChart.md", searchHints: ["visualization", "graph", "analytics", "data", "trends", "statistics"])]
public class LineChartApp(bool onlyBody = false) : ViewBase
{
    public LineChartApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("linechart", "LineChart", 1), new ArticleHeading("styles", "Styles", 2), new ArticleHeading("changing-line-widths", "Changing Line Widths", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("example", "Example", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# LineChart").OnLinkClick(onLinkClick)
            | Lead("Visualize trends and changes over time with interactive line charts that support multiple data series and customizable styling.")
            | new Markdown(
                """"
                Line charts show trends over a period of time. Build chart [views](app://onboarding/concepts/views) inside [layouts](app://onboarding/concepts/layout) and use [state](app://hooks/core/use-state) for dynamic data. See [Charts](app://onboarding/concepts/charts) for an overview of Ivy chart widgets. The example below renders desktop
                and mobile sales figures.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicLineChartDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    
                    public class BasicLineChartDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Month = "January", Desktop = 186, Mobile = 100 },
                                new { Month = "February", Desktop = 305, Mobile = 200 },
                                new { Month = "March", Desktop = 237, Mobile = 300 },
                                new { Month = "April", Desktop = 186, Mobile = 100 },
                                new { Month = "May", Desktop = 325, Mobile = 200 }
                            };
                            return Layout.Vertical()
                                     | data.ToLineChart(
                                            style: LineChartStyles.Default)
                                            .Dimension("Month", e => e.Month)
                                            .Measure("Desktop", e => e.Sum(f => f.Desktop))
                                            .Measure("Mobile", e => e.Sum(f => f.Mobile))
                                            .Toolbox();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Styles
                
                There are three different styles that can be used to determine how the points on the line charts
                are connected. If smooth spline like curves is needed, use `LineChartStyles.Default` or
                `LineChartStyles.Dashboard`. If, however, straight line jumps are needed, then `LineChartStyles.Custom`
                should be used. The following example shows these three different styles.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new LineStylesDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class LineStylesDemo: ViewBase
                    {
                        Dictionary<string,LineChartStyles>
                              styleMap = new
                                  Dictionary<string,LineChartStyles>();
                        public override object? Build()
                        {
                            styleMap.TryAdd("Default",LineChartStyles.Default);
                            styleMap.TryAdd("Dashboard",LineChartStyles.Dashboard);
                            styleMap.TryAdd("Custom",LineChartStyles.Custom);
                    
                            var styles = new string[]{"Default","Dashboard","Custom"};
                            var selectedStyle = UseState(styles[0]);
                            var style = styleMap[selectedStyle.Value];
                            var styleInput = selectedStyle.ToSelectInput(styles.ToOptions())
                                                       .Width(Size.Units(20));
                    
                            var data = new[]
                            {
                                new { Month = "January", Desktop = 186, Mobile = 100 },
                                new { Month = "February", Desktop = 305, Mobile = 200 },
                                new { Month = "March", Desktop = 237, Mobile = 300 },
                                new { Month = "April", Desktop = 186, Mobile = 100 },
                                new { Month = "May", Desktop = 325, Mobile = 200 },
                            };
                            return Layout.Vertical()
                                     | styleInput
                                     | data.ToLineChart(
                                            style: style)
                                            .Dimension("Month", e => e.Month)
                                            .Measure("Desktop", e => e.Sum(f => f.Desktop))
                                            .Measure("Mobile", e => e.Sum(f => f.Mobile))
                                            .Toolbox();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Changing Line Widths
                
                To change the width of individual lines, use the `StrokeWidth` function:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new LineWidthDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class LineWidthDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Month = "January", Desktop = 186, Mobile = 100},
                                new { Month = "February", Desktop = 305, Mobile = 200},
                                new { Month = "March", Desktop = 237, Mobile = 300},
                                new { Month = "April", Desktop = 186, Mobile = 100},
                                new { Month = "May", Desktop = 325, Mobile = 200}
                            };
                            return Layout.Vertical()
                                     | new LineChart(data, "Desktop", "Month")
                                            .Line(new Line("Mobile").StrokeWidth(5))
                                            .Legend();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.LineChart", "Ivy.LineChartExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Charts/LineChart.cs")
            | new Markdown("## Example").OnLinkClick(onLinkClick)
            | new Expandable("Bitcoin data",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    LineChart can comfortably handle large number of data points. The following example shows
                    how it can be used to render bitcoin prices for the last 100 days.
                    """").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new BitcoinChart())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class BitcoinChart : ViewBase
                        {
                            public override object? Build()
                            {
                                var random = new Random(42); // Fixed seed for consistent data
                                double min = 80000;
                                double max = 120000;
                        
                        
                                var bitcoinData = Enumerable.Range(1, 100)
                                    .Select(daysBefore => new {
                                        Date = DateTime.Today.AddDays(-daysBefore),
                                        Price = random.NextDouble() * (max - min) + min
                                    })
                                    .OrderBy(x => x.Date)
                                    .ToArray();
                        
                                return Layout.Vertical()
                                         | Text.P("Bitcoin Price - Last 100 Days").Large()
                                         | Text.P($"Showing {bitcoinData.Length} days of data").Small()
                                         | Text.Html($"<i>From {bitcoinData.First().Date:yyyy-MM-dd} to {bitcoinData.Last().Date:yyyy-MM-dd}</i>")
                                         | bitcoinData.ToLineChart(
                                                style: LineChartStyles.Dashboard)
                                            .Dimension("Date", e => e.Date)
                                            .Measure("Price", e => e.Sum(f => f.Price))
                                            .Toolbox();
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.ChartsApp)]; 
        return article;
    }
}



public class BasicLineChartDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Month = "January", Desktop = 186, Mobile = 100 },
            new { Month = "February", Desktop = 305, Mobile = 200 },
            new { Month = "March", Desktop = 237, Mobile = 300 },
            new { Month = "April", Desktop = 186, Mobile = 100 },
            new { Month = "May", Desktop = 325, Mobile = 200 }
        };
        return Layout.Vertical()
                 | data.ToLineChart(
                        style: LineChartStyles.Default)
                        .Dimension("Month", e => e.Month)
                        .Measure("Desktop", e => e.Sum(f => f.Desktop))
                        .Measure("Mobile", e => e.Sum(f => f.Mobile))
                        .Toolbox();
    }
}    

public class LineStylesDemo: ViewBase
{
    Dictionary<string,LineChartStyles> 
          styleMap = new 
              Dictionary<string,LineChartStyles>();
    public override object? Build()
    {
        styleMap.TryAdd("Default",LineChartStyles.Default);
        styleMap.TryAdd("Dashboard",LineChartStyles.Dashboard);
        styleMap.TryAdd("Custom",LineChartStyles.Custom);
   
        var styles = new string[]{"Default","Dashboard","Custom"};
        var selectedStyle = UseState(styles[0]);
        var style = styleMap[selectedStyle.Value];
        var styleInput = selectedStyle.ToSelectInput(styles.ToOptions())
                                   .Width(Size.Units(20));
        
        var data = new[]
        {
            new { Month = "January", Desktop = 186, Mobile = 100 },
            new { Month = "February", Desktop = 305, Mobile = 200 },
            new { Month = "March", Desktop = 237, Mobile = 300 },
            new { Month = "April", Desktop = 186, Mobile = 100 },
            new { Month = "May", Desktop = 325, Mobile = 200 },
        };
        return Layout.Vertical()
                 | styleInput
                 | data.ToLineChart(
                        style: style)
                        .Dimension("Month", e => e.Month)
                        .Measure("Desktop", e => e.Sum(f => f.Desktop))
                        .Measure("Mobile", e => e.Sum(f => f.Mobile))
                        .Toolbox();
    }
}

public class LineWidthDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Month = "January", Desktop = 186, Mobile = 100},
            new { Month = "February", Desktop = 305, Mobile = 200},
            new { Month = "March", Desktop = 237, Mobile = 300},
            new { Month = "April", Desktop = 186, Mobile = 100},
            new { Month = "May", Desktop = 325, Mobile = 200}
        };
        return Layout.Vertical()
                 | new LineChart(data, "Desktop", "Month")
                        .Line(new Line("Mobile").StrokeWidth(5))
                        .Legend();
    }
}

public class BitcoinChart : ViewBase
{
    public override object? Build()
    {
        var random = new Random(42); // Fixed seed for consistent data
        double min = 80000;
        double max = 120000;
        
     
        var bitcoinData = Enumerable.Range(1, 100)
            .Select(daysBefore => new { 
                Date = DateTime.Today.AddDays(-daysBefore), 
                Price = random.NextDouble() * (max - min) + min 
            })
            .OrderBy(x => x.Date) 
            .ToArray();
        
        return Layout.Vertical()
                 | Text.P("Bitcoin Price - Last 100 Days").Large()
                 | Text.P($"Showing {bitcoinData.Length} days of data").Small()
                 | Text.Html($"<i>From {bitcoinData.First().Date:yyyy-MM-dd} to {bitcoinData.Last().Date:yyyy-MM-dd}</i>")
                 | bitcoinData.ToLineChart(
                        style: LineChartStyles.Dashboard)
                    .Dimension("Date", e => e.Date)
                    .Measure("Price", e => e.Sum(f => f.Price))
                    .Toolbox();
    }
}
