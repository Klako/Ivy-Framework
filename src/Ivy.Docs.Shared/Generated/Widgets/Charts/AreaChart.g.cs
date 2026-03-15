using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Charts;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/06_Charts/03_AreaChart.md", searchHints: ["visualization", "graph", "analytics", "data", "trends", "statistics"])]
public class AreaChartApp(bool onlyBody = false) : ViewBase
{
    public AreaChartApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("areachart", "AreaChart", 1), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# AreaChart").OnLinkClick(onLinkClick)
            | Lead("Display quantitative data over time with filled areas that can be stacked with different colors.")
            | new Markdown(
                """"
                `AreaChart`s display quantitative data over time. Build chart [views](app://onboarding/concepts/views) inside [layouts](app://onboarding/concepts/layout) and use [state](app://hooks/core/use-state) for dynamic data. See [Charts](app://onboarding/concepts/charts) for an overview of Ivy chart widgets. Multiple series can be stacked
                with different colors.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicAreaChart : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Month = "Jan", Desktop = 186, Mobile = 80 },
                                new { Month = "Feb", Desktop = 305, Mobile = 200 },
                                new { Month = "Mar", Desktop = 237, Mobile = 120 },
                            };
                    
                            return Layout.Vertical()
                                | Text.P("Sales figures").Large()
                                | new AreaChart(data)
                                        .ColorScheme(ColorScheme.Default)
                                        .Area(new Area("Mobile", 1).Fill(Colors.Red).LegendType(LegendTypes.Square))
                                        .Area(new Area("Desktop", 1).Fill(Colors.Blue).LegendType(LegendTypes.Square))
                                        .CartesianGrid(new CartesianGrid().Horizontal())
                                        .Tooltip()
                                        .XAxis(new XAxis("Month").TickLine(false).AxisLine(false))
                                        .Legend()
                                        .Toolbox();
                       }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicAreaChart())
            )
            | new WidgetDocsView("Ivy.AreaChart", "Ivy.AreaChartExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Charts/AreaChart.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("COVID-19 Cases and Deaths in numbers",
                Vertical().Gap(4)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        
                        public class Covid19Demo : ViewBase
                        {
                            public override object? Build()
                            {
                                var data = new[] {
                                        new { Month = "Jan", Cases = 45680, Deaths = 1250 },
                                        new { Month = "Feb", Cases = 38420, Deaths = 980 },
                                        new { Month = "Mar", Cases = 42150, Deaths = 1100 },
                                        new { Month = "Apr", Cases = 28900, Deaths = 750 },
                                        new { Month = "May", Cases = 31200, Deaths = 820 },
                                        new { Month = "Jun", Cases = 35800, Deaths = 890 },
                                        new { Month = "Jul", Cases = 58200, Deaths = 1420 },
                                        new { Month = "Aug", Cases = 71500, Deaths = 1680 },
                                        new { Month = "Sep", Cases = 52300, Deaths = 1350 },
                                        new { Month = "Oct", Cases = 39600, Deaths = 1020 },
                                        new { Month = "Nov", Cases = 48700, Deaths = 1180 },
                                        new { Month = "Dec", Cases = 62800, Deaths = 1450 }
                                };
                        
                                return new Card().Title("COVID-19 cases")
                                    | data.ToAreaChart()
                                        .Dimension("Month", e => e.Month)
                                        .Measure("Cases", e => e.Sum(f => f.Cases))
                                        .Measure("Deaths", e => e.Sum(f => f.Deaths))
                                        .Toolbox()
                                ;
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new Covid19Demo())
                )
            )
            | new Expandable("Migrations to Europe",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    In the previous example, solid [Colors](app://api-reference/ivy/colors) have been used. However, the colors can be transparent
                    and the opacity can be controlled using the function `FillOpacity`. In the following example
                    `Fill` (used to fill an area with a [Colors](app://api-reference/ivy/colors) value) and `FillOpacity` are used to show area charts
                    that obviously fall behind other ones to show that they are indeed present.
                    """").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        public class ImmigrationToEurope : ViewBase
                        {
                            public override object? Build()
                            {
                                var data = new[] {
                                    new { Month = "Jan", Ukraine = 32000, MiddleEast = 18500, Africa = 28000, Asia = 22000, Americas = 8500 },
                                    new { Month = "Feb", Ukraine = 28000, MiddleEast = 16200, Africa = 24500, Asia = 19800, Americas = 7200 },
                                    new { Month = "Mar", Ukraine = 35000, MiddleEast = 21000, Africa = 31000, Asia = 26500, Americas = 9800 },
                                    new { Month = "Apr", Ukraine = 29000, MiddleEast = 19800, Africa = 33500, Asia = 24200, Americas = 8900 },
                                    new { Month = "May", Ukraine = 31500, MiddleEast = 22500, Africa = 38000, Asia = 28000, Americas = 10200 },
                                    new { Month = "Jun", Ukraine = 26000, MiddleEast = 25000, Africa = 42000, Asia = 31500, Americas = 11800 },
                                    new { Month = "Jul", Ukraine = 23000, MiddleEast = 28500, Africa = 48000, Asia = 35000, Americas = 13200 },
                                    new { Month = "Aug", Ukraine = 21000, MiddleEast = 31000, Africa = 52000, Asia = 38500, Americas = 14500 },
                                    new { Month = "Sep", Ukraine = 25000, MiddleEast = 27500, Africa = 45000, Asia = 33000, Americas = 12800 },
                                    new { Month = "Oct", Ukraine = 30000, MiddleEast = 24000, Africa = 38500, Asia = 29000, Americas = 11200 },
                                    new { Month = "Nov", Ukraine = 28500, MiddleEast = 20500, Africa = 32000, Asia = 25500, Americas = 9800 },
                                    new { Month = "Dec", Ukraine = 26000, MiddleEast = 18000, Africa = 28500, Asia = 23000, Americas = 8900 }
                                };
                                return new Card().Title("Immigration to Europe")
                                    | new AreaChart(data, new Area("Ukraine")
                                                .LegendType(LegendTypes.Square)
                                                .Fill(Colors.Sky)
                                                .FillOpacity(0.55)
                                                .Animated())
                                            .Area(new Area("MiddleEast")
                                                  .Name("Middle East")
                                                 .LegendType(LegendTypes.Square)
                                                 .Fill(Colors.Amber)
                                                 .FillOpacity(0.55))
                                            .Area(new Area("Africa")
                                                .LegendType(LegendTypes.Square)
                                                .Fill(Colors.Orange)
                                                .FillOpacity(0.55))
                                            .Area(new Area("Asia")
                                                .LegendType(LegendTypes.Square)
                                                .Fill(Colors.Teal)
                                                .FillOpacity(0.55))
                                            .Area(new Area("Americas")
                                                .LegendType(LegendTypes.Square)
                                                .Fill(Colors.Rose)
                                                .FillOpacity(0.55))
                                            .XAxis(new XAxis("Month").TickLine(false).AxisLine(false))
                                            .Tooltip()
                                            .Legend()
                                            .Toolbox();
                            }
                        }
                        """",Languages.Csharp)
                    | new Box().Content(new ImmigrationToEurope())
                )
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.LayoutApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.ChartsApp)]; 
        return article;
    }
}


public class BasicAreaChart : ViewBase 
{   
    public override object? Build()
    {    
        var data = new[]
        {
            new { Month = "Jan", Desktop = 186, Mobile = 80 },
            new { Month = "Feb", Desktop = 305, Mobile = 200 },
            new { Month = "Mar", Desktop = 237, Mobile = 120 },
        };
    
        return Layout.Vertical()
            | Text.P("Sales figures").Large()
            | new AreaChart(data)
                    .ColorScheme(ColorScheme.Default)
                    .Area(new Area("Mobile", 1).Fill(Colors.Red).LegendType(LegendTypes.Square))
                    .Area(new Area("Desktop", 1).Fill(Colors.Blue).LegendType(LegendTypes.Square))
                    .CartesianGrid(new CartesianGrid().Horizontal())
                    .Tooltip()
                    .XAxis(new XAxis("Month").TickLine(false).AxisLine(false))
                    .Legend()
                    .Toolbox();
   }
}

    
public class Covid19Demo : ViewBase
{
    public override object? Build()
    {
        var data = new[] {
                new { Month = "Jan", Cases = 45680, Deaths = 1250 },
                new { Month = "Feb", Cases = 38420, Deaths = 980 },
                new { Month = "Mar", Cases = 42150, Deaths = 1100 },
                new { Month = "Apr", Cases = 28900, Deaths = 750 },
                new { Month = "May", Cases = 31200, Deaths = 820 },
                new { Month = "Jun", Cases = 35800, Deaths = 890 },
                new { Month = "Jul", Cases = 58200, Deaths = 1420 },
                new { Month = "Aug", Cases = 71500, Deaths = 1680 },
                new { Month = "Sep", Cases = 52300, Deaths = 1350 },
                new { Month = "Oct", Cases = 39600, Deaths = 1020 },
                new { Month = "Nov", Cases = 48700, Deaths = 1180 },
                new { Month = "Dec", Cases = 62800, Deaths = 1450 }
        };

        return new Card().Title("COVID-19 cases")
            | data.ToAreaChart()
                .Dimension("Month", e => e.Month)
                .Measure("Cases", e => e.Sum(f => f.Cases))
                .Measure("Deaths", e => e.Sum(f => f.Deaths))
                .Toolbox()
        ;
    }
}

public class ImmigrationToEurope : ViewBase
{
    public override object? Build()
    {
        var data = new[] {
            new { Month = "Jan", Ukraine = 32000, MiddleEast = 18500, Africa = 28000, Asia = 22000, Americas = 8500 },
            new { Month = "Feb", Ukraine = 28000, MiddleEast = 16200, Africa = 24500, Asia = 19800, Americas = 7200 },
            new { Month = "Mar", Ukraine = 35000, MiddleEast = 21000, Africa = 31000, Asia = 26500, Americas = 9800 },
            new { Month = "Apr", Ukraine = 29000, MiddleEast = 19800, Africa = 33500, Asia = 24200, Americas = 8900 },
            new { Month = "May", Ukraine = 31500, MiddleEast = 22500, Africa = 38000, Asia = 28000, Americas = 10200 },
            new { Month = "Jun", Ukraine = 26000, MiddleEast = 25000, Africa = 42000, Asia = 31500, Americas = 11800 },
            new { Month = "Jul", Ukraine = 23000, MiddleEast = 28500, Africa = 48000, Asia = 35000, Americas = 13200 },
            new { Month = "Aug", Ukraine = 21000, MiddleEast = 31000, Africa = 52000, Asia = 38500, Americas = 14500 },
            new { Month = "Sep", Ukraine = 25000, MiddleEast = 27500, Africa = 45000, Asia = 33000, Americas = 12800 },
            new { Month = "Oct", Ukraine = 30000, MiddleEast = 24000, Africa = 38500, Asia = 29000, Americas = 11200 },
            new { Month = "Nov", Ukraine = 28500, MiddleEast = 20500, Africa = 32000, Asia = 25500, Americas = 9800 },
            new { Month = "Dec", Ukraine = 26000, MiddleEast = 18000, Africa = 28500, Asia = 23000, Americas = 8900 }
        };
        return new Card().Title("Immigration to Europe")
            | new AreaChart(data, new Area("Ukraine")
                        .LegendType(LegendTypes.Square)
                        .Fill(Colors.Sky)
                        .FillOpacity(0.55)
                        .Animated())
                    .Area(new Area("MiddleEast")
                          .Name("Middle East")
                         .LegendType(LegendTypes.Square)
                         .Fill(Colors.Amber)
                         .FillOpacity(0.55))
                    .Area(new Area("Africa")
                        .LegendType(LegendTypes.Square)
                        .Fill(Colors.Orange)
                        .FillOpacity(0.55))
                    .Area(new Area("Asia")
                        .LegendType(LegendTypes.Square)
                        .Fill(Colors.Teal)
                        .FillOpacity(0.55))
                    .Area(new Area("Americas")
                        .LegendType(LegendTypes.Square)
                        .Fill(Colors.Rose)
                        .FillOpacity(0.55))   
                    .XAxis(new XAxis("Month").TickLine(false).AxisLine(false))
                    .Tooltip()
                    .Legend()
                    .Toolbox();        
    }
}
