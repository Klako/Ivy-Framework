# LineChart

*Visualize trends and changes over time with interactive line charts that support multiple data series and customizable styling.*

Line charts show trends over a period of time. Build chart [views](../../01_Onboarding/02_Concepts/02_Views.md) inside [layouts](../../01_Onboarding/02_Concepts/04_Layout.md) and use [state](../../03_Hooks/02_Core/03_UseState.md) for dynamic data. See [Charts](../../01_Onboarding/02_Concepts/18_Charts.md) for an overview of Ivy chart widgets. The example below renders desktop
and mobile sales figures.

```csharp

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
```

## Styles

There are three different styles that can be used to determine how the points on the line charts
are connected. If smooth spline like curves is needed, use `LineChartStyles.Default` or
`LineChartStyles.Dashboard`. If, however, straight line jumps are needed, then `LineChartStyles.Custom`
should be used. The following example shows these three different styles.  

```csharp
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
```

## Changing Line Widths

To change the width of individual lines, use the `StrokeWidth` function:

```csharp
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
```


## API

[View Source: LineChart.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Charts/LineChart.cs)

### Constructors

| Signature |
|-----------|
| `new LineChart(object data, Line[] lines)` |
| `new LineChart(object data, string dataKey, string nameKey)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `CartesianGrid` | `CartesianGrid` | `CartesianGrid` |
| `ColorScheme` | `ColorScheme` | `ColorScheme` |
| `Data` | `object` | - |
| `Density` | `Density?` | - |
| `Height` | `Size` | - |
| `Layout` | `Layouts` | `Layout` |
| `Legend` | `Legend` | `Legend` |
| `Lines` | `Line[]` | - |
| `ReferenceAreas` | `ReferenceArea[]` | - |
| `ReferenceDots` | `ReferenceDot[]` | - |
| `ReferenceLines` | `ReferenceLine[]` | - |
| `Toolbox` | `Toolbox` | `Toolbox` |
| `Tooltip` | `ChartTooltip` | `Tooltip` |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |
| `XAxis` | `XAxis[]` | `XAxis` |
| `YAxis` | `YAxis[]` | `YAxis` |




## Example


### Bitcoin data

LineChart can comfortably handle large number of data points. The following example shows
how it can be used to render bitcoin prices for the last 100 days.

```csharp
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
```