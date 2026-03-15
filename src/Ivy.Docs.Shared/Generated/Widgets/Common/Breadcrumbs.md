# Breadcrumbs

*A secondary navigation component that shows the user's location within a hierarchy. Perfect for multi-level navigation and hierarchical applications.*

The `Breadcrumbs` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) renders a navigation trail showing the user's location within a hierarchy. Each item is clickable (except the current/last item), enabling quick navigation to ancestor pages.

## Basic Usage

Create a simple breadcrumb trail:

```csharp
new Breadcrumbs(
    new BreadcrumbItem("Home", () => { }),
    new BreadcrumbItem("Products", () => { }),
    new BreadcrumbItem("Details")
)
```

The last item in the list is rendered as non-clickable, representing the current page.

## With Navigation

Integrate breadcrumbs with state management for navigation:

```csharp
public class BreadcrumbsNavigationDemo : ViewBase
{
    public override object? Build()
    {
        var currentPage = UseState("Details");

        return new Breadcrumbs(
            new BreadcrumbItem("Home", () => currentPage.Set("Home")),
            new BreadcrumbItem("Products", () => currentPage.Set("Products")),
            new BreadcrumbItem("Product Details")
        );
    }
}
```

## Configuration Options

### Custom Separator

Change the separator character between items:

```csharp
new Breadcrumbs(
    new BreadcrumbItem("Home", () => { }),
    new BreadcrumbItem("Products", () => { }),
    new BreadcrumbItem("Details")
).Separator(">")
```

### With Icons

Add icons to breadcrumb items:

```csharp
new Breadcrumbs(
    new BreadcrumbItem("Home", () => { }, Icons.House),
    new BreadcrumbItem("Products", () => { }, Icons.ShoppingCart),
    new BreadcrumbItem("Details")
)
```

### Tooltips

Add helpful tooltips to items:

```csharp
public class BreadcrumbsTooltipDemo : ViewBase
{
    public override object? Build()
    {
        return new Breadcrumbs(
            new BreadcrumbItem("Home", () => { }, Tooltip: "Go to homepage"),
            new BreadcrumbItem("Products", () => { }, Tooltip: "View all products"),
            new BreadcrumbItem("Details")
        );
    }
}
```

### Disabled State

Disable the entire breadcrumb trail or individual items:

```csharp
new Breadcrumbs(
    new BreadcrumbItem("Home", () => { }),
    new BreadcrumbItem("Products", Disabled: true),
    new BreadcrumbItem("Details")
).Disabled()
```


## API

[View Source: Breadcrumbs.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Breadcrumbs.cs)

### Constructors

| Signature |
|-----------|
| `new Breadcrumbs(IEnumerable<BreadcrumbItem> items)` |
| `new Breadcrumbs(BreadcrumbItem[] items)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Density` | `Density?` | - |
| `Disabled` | `bool` | `Disabled` |
| `Height` | `Size` | - |
| `Items` | `BreadcrumbItem[]` | - |
| `Separator` | `string` | `Separator` |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |


### Events

| Name | Type | Handlers |
|------|------|----------|
| `OnItemClick` | `EventHandler<Event<Breadcrumbs, int>>` | - |