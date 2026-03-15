# Pagination

*Display page [navigation](../../01_Onboarding/02_Concepts/09_Navigation.md) controls for traversing large sets of data, with customizable appearance and dynamic updates.*

The `Pagination` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) is used to provide navigation between multiple pages of content, such as [lists](07_List.md) or [tables](08_Table.md). It displays a set of page links, previous/next buttons, and optional ellipsis for skipped ranges. The widget can be customized for appearance and supports binding to [state](../../03_Hooks/02_Core/03_UseState.md) for dynamic updates.

## Basic Usage

Here's a simple example of a pagination control initialized at page 5 of 10.

```csharp
public class BasicPaginationApp : ViewBase
{
    public override object? Build() {
        var page = UseState(5);

        return new Pagination(page.Value, 10, newPage => page.Set(newPage.Value));
    }
}
```

## Configuration

You can configure the number of visible pages adjacent to the current page (siblings) and at the start/end of the range (boundaries).

```csharp
public class PaginationConfigurationApp : ViewBase
{
    public override object? Build() {
        var page = UseState(5);

        return Layout.Vertical().Gap(4)
            | Text.P("Siblings").Large()
            | (Layout.Vertical()
                | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Siblings(1)
                | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Siblings(2))
            | Text.P("Boundaries").Large()
            | (Layout.Vertical()
                | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Boundaries(1)
                | new Pagination(page.Value, 20, newPage => page.Set(newPage.Value)).Boundaries(2));
    }
}
```


## API

[View Source: Pagination.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Pagination.cs)

### Constructors

| Signature |
|-----------|
| `new Pagination(int? page, int? numPages, Func<Event<Pagination, int>, ValueTask> onChange, bool disabled = false)` |
| `new Pagination(int? page, int? numPages, Action<Event<Pagination, int>> onChange, bool disabled = false)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Boundaries` | `int?` | `Boundaries` |
| `Density` | `Density?` | - |
| `Disabled` | `bool` | `Disabled` |
| `Height` | `Size` | - |
| `NumPages` | `int?` | - |
| `Page` | `int?` | - |
| `Siblings` | `int?` | `Siblings` |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |


### Events

| Name | Type | Handlers |
|------|------|----------|
| `OnChange` | `EventHandler<Event<Pagination, int>>` | - |