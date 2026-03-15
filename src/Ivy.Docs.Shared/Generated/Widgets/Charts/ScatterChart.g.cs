using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Charts;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/06_Charts/05_ScatterChart.md", searchHints: ["visualization", "scatter", "bubble", "correlation", "analytics", "data", "points", "distribution"])]
public class ScatterChartApp(bool onlyBody = false) : ViewBase
{
    public ScatterChartApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("scatterchart", "ScatterChart", 1), new ArticleHeading("bubble-charts", "Bubble Charts", 2), new ArticleHeading("point-shapes", "Point Shapes", 2), new ArticleHeading("connected-scatter", "Connected Scatter", 2), new ArticleHeading("zaxis-configuration", "ZAxis Configuration", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("example", "Example", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# ScatterChart").OnLinkClick(onLinkClick)
            | Lead("Visualize correlations and distributions with interactive scatter plots that support bubble charts, multiple series, and customizable point shapes.")
            | new Markdown("Scatter charts display data points on a two-dimensional coordinate system, making them ideal for showing correlations between two numeric variables. Build chart [views](app://onboarding/concepts/views) inside [layouts](app://onboarding/concepts/layout) and use [state](app://hooks/core/use-state) for dynamic data. See [Charts](app://onboarding/concepts/charts) for an overview of Ivy chart widgets. The example below renders a basic scatter plot showing the relationship between height and weight.").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicScatterChartDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    
                    public class BasicScatterChartDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Height = 165, Weight = 65 },
                                new { Height = 170, Weight = 72 },
                                new { Height = 158, Weight = 58 },
                                new { Height = 175, Weight = 78 },
                                new { Height = 162, Weight = 60 },
                                new { Height = 180, Weight = 85 },
                                new { Height = 155, Weight = 52 },
                                new { Height = 168, Weight = 68 },
                            };
                    
                            return Layout.Vertical()
                                | new ScatterChart(data)
                                    .Scatter(new Scatter("Value").Name("Height vs Weight"))
                                    .XAxis(new XAxis("Height").Type(AxisTypes.Number))
                                    .YAxis(new YAxis("Weight").Type(AxisTypes.Number))
                                    .Tooltip(new ChartTooltip().Animated(true))
                                    .Legend()
                                    .CartesianGrid();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Bubble Charts
                
                Scatter charts can encode a third dimension using bubble size through the `ZAxis` property. This creates bubble charts where point size represents an additional numeric value:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BubbleChartDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BubbleChartDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Height = 165, Weight = 65, Age = 25 },
                                new { Height = 170, Weight = 72, Age = 45 },
                                new { Height = 158, Weight = 58, Age = 22 },
                                new { Height = 175, Weight = 78, Age = 50 },
                                new { Height = 162, Weight = 60, Age = 20 },
                                new { Height = 180, Weight = 85, Age = 55 },
                                new { Height = 155, Weight = 52, Age = 18 },
                                new { Height = 168, Weight = 68, Age = 35 },
                            };
                    
                            return Layout.Vertical()
                                | new ScatterChart(data)
                                    .Scatter(new Scatter("Value").Name("People"))
                                    .XAxis(new XAxis("Height").Type(AxisTypes.Number))
                                    .YAxis(new YAxis("Weight").Type(AxisTypes.Number))
                                    .ZAxis(new ZAxis("Age").Range(40, 200))
                                    .Tooltip(new ChartTooltip().Animated(true))
                                    .Legend()
                                    .CartesianGrid(new CartesianGrid().Horizontal().Vertical());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Point Shapes
                
                Customize point appearance with different shapes. Available shapes include `Circle`, `Square`, `Cross`, `Diamond`, `Star`, `Triangle`, and `Wye`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ScatterShapesDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ScatterShapesDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { X = 10, Y = 20 },
                                new { X = 15, Y = 25 },
                                new { X = 20, Y = 30 },
                                new { X = 25, Y = 35 },
                                new { X = 30, Y = 40 },
                            };
                    
                            return Layout.Vertical()
                                | new ScatterChart(data)
                                    .Scatter(new Scatter("Value")
                                        .Name("Diamond Points")
                                        .Shape(ScatterShape.Diamond)
                                        .Fill(Colors.Blue))
                                    .XAxis(new XAxis("X").Type(AxisTypes.Number))
                                    .YAxis(new YAxis("Y").Type(AxisTypes.Number))
                                    .Tooltip(new ChartTooltip().Animated(true))
                                    .Legend()
                                    .CartesianGrid();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Connected Scatter
                
                Connect scatter points with lines to show sequential relationships. Use `Joint` for straight connections or `Fitting` for smooth interpolation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ConnectedScatterDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ConnectedScatterDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Time = 1, Temperature = 20, Humidity = 65 },
                                new { Time = 2, Temperature = 22, Humidity = 62 },
                                new { Time = 3, Temperature = 24, Humidity = 58 },
                                new { Time = 4, Temperature = 26, Humidity = 55 },
                                new { Time = 5, Temperature = 28, Humidity = 52 },
                                new { Time = 6, Temperature = 30, Humidity = 48 },
                            };
                    
                            return Layout.Vertical()
                                | new ScatterChart(data)
                                    .Scatter(new Scatter("Value")
                                        .Name("Temperature vs Humidity")
                                        .Line(true)
                                        .LineType(ScatterLineType.Fitting))
                                    .XAxis(new XAxis("Temperature").Type(AxisTypes.Number))
                                    .YAxis(new YAxis("Humidity").Type(AxisTypes.Number))
                                    .Tooltip(new ChartTooltip().Animated(true))
                                    .Legend()
                                    .CartesianGrid(new CartesianGrid().Horizontal().Vertical());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## ZAxis Configuration
                
                Control bubble size range with the `Range` method on `ZAxis`. The range determines the minimum and maximum pixel sizes for bubbles:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ZAxisRangeDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ZAxisRangeDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Income = 30000, Savings = 5000, Expenses = 25000 },
                                new { Income = 45000, Savings = 10000, Expenses = 35000 },
                                new { Income = 60000, Savings = 18000, Expenses = 42000 },
                                new { Income = 75000, Savings = 25000, Expenses = 50000 },
                                new { Income = 90000, Savings = 35000, Expenses = 55000 },
                                new { Income = 105000, Savings = 45000, Expenses = 60000 },
                            };
                    
                            return Layout.Vertical()
                                | new ScatterChart(data)
                                    .Scatter(new Scatter("Value").Name("Financial Data"))
                                    .XAxis(new XAxis("Income").Type(AxisTypes.Number))
                                    .YAxis(new YAxis("Savings").Type(AxisTypes.Number))
                                    .ZAxis(new ZAxis("Expenses").Range(50, 300))
                                    .Tooltip(new ChartTooltip().Animated(true))
                                    .Legend()
                                    .CartesianGrid(new CartesianGrid().Horizontal().Vertical());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.ScatterChart", "Ivy.ScatterChartExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Charts/ScatterChart.cs")
            | new Markdown("## Example").OnLinkClick(onLinkClick)
            | new Expandable("Correlation Analysis",
                Vertical().Gap(4)
                | new Markdown("ScatterChart can be used to analyze correlations between variables. The following example demonstrates a multi-dimensional analysis with size encoding:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new CorrelationAnalysisDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class CorrelationAnalysisDemo : ViewBase
                        {
                            public override object? Build()
                            {
                                var random = new Random(42); // Fixed seed for consistent data
                        
                                var sampleData = Enumerable.Range(1, 50)
                                    .Select(i => new {
                                        StudyHours = random.Next(5, 50),
                                        TestScore = random.Next(40, 100),
                                        AttendancePercent = random.Next(60, 100)
                                    })
                                    .ToArray();
                        
                                return Layout.Vertical()
                                    | Text.P("Student Performance Analysis").Large()
                                    | Text.P($"Analyzing {sampleData.Length} students").Small()
                                    | Text.Html("<i>Study Hours vs Test Score (bubble size = attendance %)</i>")
                                    | new ScatterChart(sampleData)
                                        .Scatter(new Scatter("Value").Name("Students"))
                                        .XAxis(new XAxis("StudyHours").Type(AxisTypes.Number))
                                        .YAxis(new YAxis("TestScore").Type(AxisTypes.Number))
                                        .ZAxis(new ZAxis("AttendancePercent").Range(60, 250))
                                        .Tooltip(new ChartTooltip().Animated(true))
                                        .Legend()
                                        .Toolbox(new Toolbox())
                                        .CartesianGrid(new CartesianGrid().Horizontal().Vertical());
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



public class BasicScatterChartDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Height = 165, Weight = 65 },
            new { Height = 170, Weight = 72 },
            new { Height = 158, Weight = 58 },
            new { Height = 175, Weight = 78 },
            new { Height = 162, Weight = 60 },
            new { Height = 180, Weight = 85 },
            new { Height = 155, Weight = 52 },
            new { Height = 168, Weight = 68 },
        };

        return Layout.Vertical()
            | new ScatterChart(data)
                .Scatter(new Scatter("Value").Name("Height vs Weight"))
                .XAxis(new XAxis("Height").Type(AxisTypes.Number))
                .YAxis(new YAxis("Weight").Type(AxisTypes.Number))
                .Tooltip(new ChartTooltip().Animated(true))
                .Legend()
                .CartesianGrid();
    }
}

public class BubbleChartDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Height = 165, Weight = 65, Age = 25 },
            new { Height = 170, Weight = 72, Age = 45 },
            new { Height = 158, Weight = 58, Age = 22 },
            new { Height = 175, Weight = 78, Age = 50 },
            new { Height = 162, Weight = 60, Age = 20 },
            new { Height = 180, Weight = 85, Age = 55 },
            new { Height = 155, Weight = 52, Age = 18 },
            new { Height = 168, Weight = 68, Age = 35 },
        };

        return Layout.Vertical()
            | new ScatterChart(data)
                .Scatter(new Scatter("Value").Name("People"))
                .XAxis(new XAxis("Height").Type(AxisTypes.Number))
                .YAxis(new YAxis("Weight").Type(AxisTypes.Number))
                .ZAxis(new ZAxis("Age").Range(40, 200))
                .Tooltip(new ChartTooltip().Animated(true))
                .Legend()
                .CartesianGrid(new CartesianGrid().Horizontal().Vertical());
    }
}

public class ScatterShapesDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { X = 10, Y = 20 },
            new { X = 15, Y = 25 },
            new { X = 20, Y = 30 },
            new { X = 25, Y = 35 },
            new { X = 30, Y = 40 },
        };

        return Layout.Vertical()
            | new ScatterChart(data)
                .Scatter(new Scatter("Value")
                    .Name("Diamond Points")
                    .Shape(ScatterShape.Diamond)
                    .Fill(Colors.Blue))
                .XAxis(new XAxis("X").Type(AxisTypes.Number))
                .YAxis(new YAxis("Y").Type(AxisTypes.Number))
                .Tooltip(new ChartTooltip().Animated(true))
                .Legend()
                .CartesianGrid();
    }
}

public class ConnectedScatterDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Time = 1, Temperature = 20, Humidity = 65 },
            new { Time = 2, Temperature = 22, Humidity = 62 },
            new { Time = 3, Temperature = 24, Humidity = 58 },
            new { Time = 4, Temperature = 26, Humidity = 55 },
            new { Time = 5, Temperature = 28, Humidity = 52 },
            new { Time = 6, Temperature = 30, Humidity = 48 },
        };

        return Layout.Vertical()
            | new ScatterChart(data)
                .Scatter(new Scatter("Value")
                    .Name("Temperature vs Humidity")
                    .Line(true)
                    .LineType(ScatterLineType.Fitting))
                .XAxis(new XAxis("Temperature").Type(AxisTypes.Number))
                .YAxis(new YAxis("Humidity").Type(AxisTypes.Number))
                .Tooltip(new ChartTooltip().Animated(true))
                .Legend()
                .CartesianGrid(new CartesianGrid().Horizontal().Vertical());
    }
}

public class ZAxisRangeDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Income = 30000, Savings = 5000, Expenses = 25000 },
            new { Income = 45000, Savings = 10000, Expenses = 35000 },
            new { Income = 60000, Savings = 18000, Expenses = 42000 },
            new { Income = 75000, Savings = 25000, Expenses = 50000 },
            new { Income = 90000, Savings = 35000, Expenses = 55000 },
            new { Income = 105000, Savings = 45000, Expenses = 60000 },
        };

        return Layout.Vertical()
            | new ScatterChart(data)
                .Scatter(new Scatter("Value").Name("Financial Data"))
                .XAxis(new XAxis("Income").Type(AxisTypes.Number))
                .YAxis(new YAxis("Savings").Type(AxisTypes.Number))
                .ZAxis(new ZAxis("Expenses").Range(50, 300))
                .Tooltip(new ChartTooltip().Animated(true))
                .Legend()
                .CartesianGrid(new CartesianGrid().Horizontal().Vertical());
    }
}

public class CorrelationAnalysisDemo : ViewBase
{
    public override object? Build()
    {
        var random = new Random(42); // Fixed seed for consistent data

        var sampleData = Enumerable.Range(1, 50)
            .Select(i => new {
                StudyHours = random.Next(5, 50),
                TestScore = random.Next(40, 100),
                AttendancePercent = random.Next(60, 100)
            })
            .ToArray();

        return Layout.Vertical()
            | Text.P("Student Performance Analysis").Large()
            | Text.P($"Analyzing {sampleData.Length} students").Small()
            | Text.Html("<i>Study Hours vs Test Score (bubble size = attendance %)</i>")
            | new ScatterChart(sampleData)
                .Scatter(new Scatter("Value").Name("Students"))
                .XAxis(new XAxis("StudyHours").Type(AxisTypes.Number))
                .YAxis(new YAxis("TestScore").Type(AxisTypes.Number))
                .ZAxis(new ZAxis("AttendancePercent").Range(60, 250))
                .Tooltip(new ChartTooltip().Animated(true))
                .Legend()
                .Toolbox(new Toolbox())
                .CartesianGrid(new CartesianGrid().Horizontal().Vertical());
    }
}
