# Confetti

*Add celebratory confetti effects to any [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) with customizable triggers for automatic, click, or hover activation.*

The `Confetti` animation can be triggered automatically, on click, or when the mouse hovers over the widget. Perfect for celebrating user achievements, [form](../../01_Onboarding/02_Concepts/08_Forms.md) submissions, or adding delightful interactions to your [interface](../../01_Onboarding/02_Concepts/02_Views.md).

## Basic Usage

Wrap any widget with confetti using the `WithConfetti()` extension method:

```csharp ivy-bg
new Button("Confetti on click!")
    .WithConfetti(AnimationTrigger.Click)
```

### Auto Trigger

Confetti fires automatically when the widget is first rendered, perfect for welcoming users or celebrating initial page loads.

```csharp ivy-bg
Text.Block("Welcome!")
    .WithConfetti(AnimationTrigger.Auto)
```

### Hover Trigger

Confetti activates when the mouse hovers over the widget, providing immediate visual feedback for interactive elements.

```csharp ivy-bg
new Card("Hover over me")
    .WithConfetti(AnimationTrigger.Hover)
```

### List Usage

Demonstrates how to add confetti to list items, making each selection feel special and celebratory.

```csharp ivy-bg
Layout.Vertical().Gap(10)
    | new List(new[] { "First option on click", "Second option on click" }
        .Select(level => new ListItem(level, onClick: _ => {}, icon: Icons.Circle)
            .WithConfetti(AnimationTrigger.Click)))
```


## API

[View Source: Confetti.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Effects/Confetti.cs)

### Constructors

| Signature |
|-----------|
| `new Confetti(object child = null)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Density` | `Density?` | - |
| `Height` | `Size` | - |
| `Trigger` | `AnimationTrigger` | `Trigger` |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |




## Examples


### Integration with Other Widgets

Confetti works seamlessly with all Ivy [widgets](../../01_Onboarding/02_Concepts/03_Widgets.md), allowing you to add celebratory effects to any interface element (for example Button, Card, ListItem, Badge, or text).

```csharp ivy-bg
Layout.Vertical().Gap(10)
    | new Button("Action").WithConfetti(AnimationTrigger.Click)
    | new Card("Content").WithConfetti(AnimationTrigger.Hover)
    | new ListItem("Item").WithConfetti(AnimationTrigger.Click)
    | Text.Block("Message").WithConfetti(AnimationTrigger.Hover)
    | new Badge("Success").WithConfetti(AnimationTrigger.Hover)
```