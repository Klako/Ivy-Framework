# RadialBarChart

*Display categorical data as concentric arcs radiating from a center point, perfect for progress tracking, KPI dashboards, and gauge-like visualizations.*

`RadialBarChart` displays data as concentric circular bars, where each category occupies its own ring. Unlike PieChart which shows proportions of a whole, RadialBarChart compares independent values that each have their own track. Build chart [views](../../01_Onboarding/02_Concepts/02_Views.md) inside [layouts](../../01_Onboarding/02_Concepts/04_Layout.md) and use [state](../../03_Hooks/02_Core/03_UseState.md) for dynamic data. See [Charts](../../01_Onboarding/02_Concepts/18_Charts.md) for an overview of Ivy chart widgets.

## Basic Usage

Create a basic radial bar chart by providing data and configuring the radial bars. Each value is rendered as a concentric arc with its own radius.

```csharp
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
```

## Progress Tracking (Apple Watch Style)

Radial bar charts excel at displaying progress toward goals. Use `Background(true)` to show unfilled portions and adjust the angular range to create activity ring-style visualizations.

```csharp
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
```

The `StartAngle` and `EndAngle` properties control the arc span. Setting StartAngle to 90 and EndAngle to 450 creates a 360-degree ring starting from the top (matching Apple Watch's design).

## Gauge-Like Displays

Create gauge-style visualizations for KPIs by adjusting the angular range and using background tracks. This is ideal for dashboard metrics where you want to show current values against maximum capacity.

```csharp
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
```

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

```csharp
.PolarGrid(new PolarGrid()
    .GridType(PolarGridTypes.Circle)
    .RadialLines(true))
```

Available grid types:
- `PolarGridTypes.Polygon`: Polygonal grid (default)
- `PolarGridTypes.Circle`: Circular grid


## API

[View Source: RadialBarChart.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Charts/RadialBarChart.cs)

### Constructors

| Signature |
|-----------|
| `new RadialBarChart(object data)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `BarCategoryGap` | `object` | - |
| `BarGap` | `int` | `BarGap` |
| `BarSize` | `int?` | - |
| `ColorScheme` | `ColorScheme` | `ColorScheme` |
| `Cx` | `object` | - |
| `Cy` | `object` | - |
| `Data` | `object` | - |
| `Density` | `Density?` | - |
| `EndAngle` | `int` | `EndAngle` |
| `Height` | `Size` | - |
| `InnerRadius` | `object` | `InnerRadius` |
| `Legend` | `Legend` | `Legend` |
| `OuterRadius` | `object` | `OuterRadius` |
| `PolarAngleAxis` | `PolarAngleAxis` | `PolarAngleAxis` |
| `PolarGrid` | `PolarGrid` | `PolarGrid` |
| `PolarRadiusAxis` | `PolarRadiusAxis` | `PolarRadiusAxis` |
| `RadialBars` | `RadialBar[]` | - |
| `StartAngle` | `int` | `StartAngle` |
| `Toolbox` | `Toolbox` | `Toolbox` |
| `Tooltip` | `ChartTooltip` | `Tooltip` |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |