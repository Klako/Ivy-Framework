# DateRangeInput

*Select date ranges with an intuitive calendar [interface](../../01_Onboarding/02_Concepts/02_Views.md) for start and end dates, perfect for filtering and event scheduling.*

The `DateRangeInput` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) allows users to select a range of dates. It provides a calendar interface for both start and end date selection, making it ideal for filtering data by date ranges or scheduling events.

## Basic Usage

Here's a simple example of a `DateRangeInput` that allows users to select a date range:

```csharp
public class BasicDateRangeDemo : ViewBase
{
    public override object? Build()
    {    
        var dateRangeState = UseState(() => (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
        var start = dateRangeState.Value.Item1;
        var end = dateRangeState.Value.Item2;
        var span = $"That's {(end-start).Days} days";
        return Layout.Vertical()
                | dateRangeState.ToDateRangeInput()
                | Text.P(span).Large();
    }    
}        
```

As can be seen, the starting and ending date of the date range can be extracted using the
`DateTimeRange.Value.Item1` and `DateTimeRange.Value.Item2`

## Supported Types

DateRangeInput supports DateOnly tuple types:

- `(DateOnly, DateOnly)` - Date-only range
- `(DateOnly?, DateOnly?)` - Nullable date-only range

## Variants

The `DateRangeInput` can be customized with various states. The following example demonstrates **Disabled**, **Invalid**, and **Nullable** states:

```csharp
public class DateRangeVariantsDemo : ViewBase
{
    public override object? Build()
    {
        var dateRange = UseState(() => 
            (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
        
        var nullableRange = UseState<(DateOnly?, DateOnly?)>(() => 
            (DateOnly.FromDateTime(DateTime.Today.AddDays(-7)), 
             DateOnly.FromDateTime(DateTime.Today)));

        return Layout.Vertical().Gap(4)
            | Text.P("Disabled State").Small()
            | dateRange.ToDateRangeInput().Disabled()
            | Text.P("Invalid State").Small()
            | dateRange.ToDateRangeInput().Invalid("Invalid date range")
            | Text.P("Nullable State").Small()
            | nullableRange.ToDateRangeInput();
    }
}
```

## Format

To change the format of selected dates the `Format` function needs to be used.

```csharp
public class FormatDateRangeDemo : ViewBase
{
    public override object? Build()
    {   
         var dateRangeState = UseState(() => 
            (from: DateTime.Today.AddDays(-7), to: DateTime.Today));
         return Layout.Vertical()
                 | dateRangeState.ToDateRangeInput()
                                  .Format("yyyy-MM-dd");
    }    
}        
```


## API

[View Source: DateRangeInput.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/DateRangeInput.cs)

### Constructors

| Signature |
|-----------|
| `new DateRangeInput<TDateRange>(IAnyState state, string placeholder = null, bool disabled = false)` |
| `new DateRangeInput<TDateRange>(TDateRange value, Func<Event<IInput<TDateRange>, TDateRange>, ValueTask> onChange, string placeholder = null, bool disabled = false)` |
| `new DateRangeInput<TDateRange>(TDateRange value, Action<Event<IInput<TDateRange>, TDateRange>> onChange, string placeholder = null, bool disabled = false)` |
| `new DateRangeInput<TDateRange>(string placeholder = null, bool disabled = false)` |
| `ToDateRangeInput(IAnyState state, string placeholder = null, bool disabled = false)` |


### Supported Types

| Group | Type | Nullable |
|-------|------|----------|
| Other | `ValueTuple<DateOnly, DateOnly>` | - |
| Other | `ValueTuple<DateOnly?, DateOnly?>` | - |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Density` | `Density?` | - |
| `Disabled` | `bool` | `Disabled` |
| `Format` | `string` | `Format` |
| `Height` | `Size` | - |
| `Invalid` | `string` | `Invalid` |
| `Nullable` | `bool` | `Nullable` |
| `Placeholder` | `string` | `Placeholder` |
| `Value` | `TDateRange` | - |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |


### Events

| Name | Type | Handlers |
|------|------|----------|
| `OnBlur` | `EventHandler<Event<IAnyInput>>` | `OnBlur` |
| `OnChange` | `EventHandler<Event<IInput<TDateRange>, TDateRange>>` | - |




## Examples


### Hotel Booking Example

A realistic example demonstrating a hotel booking form with date range selection, validation, and price calculation:

```csharp
public class HotelBookingDemo : ViewBase
{
    private const decimal PricePerNight = 120m;
    
    public override object? Build()
    {
        var bookingRange = UseState<(DateOnly?, DateOnly?)>(() => (null, null));
        
        var from = bookingRange.Value.Item1;
        var to = bookingRange.Value.Item2;
        
        var nights = from.HasValue && to.HasValue 
            ? (to.Value.ToDateTime(TimeOnly.MinValue) - from.Value.ToDateTime(TimeOnly.MinValue)).Days 
            : 0;
        
        var isValid = nights >= 1;
        var errorMessage = !isValid && from.HasValue ? "Minimum stay is 1 night" : "";
        var totalPrice = nights * PricePerNight;

        return Layout.Vertical().Gap(4)
            | Text.P("Book Your Stay").Large().Bold()
            | bookingRange.ToDateRangeInput()
                .Placeholder("Select check-in and check-out dates")
                .Format("MMM dd, yyyy")
                .Invalid(errorMessage)
            | (nights > 0 
                ? Layout.Horizontal().Gap(2)
                    | Text.P($"{nights} night(s)").Small().Muted()
                    | Text.P($"Total: ${totalPrice}").Small().Bold()
                : null);
    }
}
```