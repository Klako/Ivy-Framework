# ReadOnlyInput

*Display [form](../../01_Onboarding/02_Concepts/08_Forms.md) data in a consistent input-like style that maintains visual coherence while preventing user modification.*

The `ReadOnlyInput` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) displays data in an input-like format that cannot be edited by the user. It's useful for showing form values in a consistent style with other [inputs](../../01_Onboarding/02_Concepts/03_Widgets.md), while preventing modification.

## Basic Usage

Here's a simple example of a `ReadOnlyInput` displaying a value:

```csharp
public class ReadOnlyDemo : ViewBase
{    
    public override object? Build()
    {    
        double value = 123.45;
        var readOnlyInput = new ReadOnlyInput<double>(value);
        return readOnlyInput;
    }    
}    
```


## API

[View Source: ReadOnlyInput.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/ReadOnlyInput.cs)

### Constructors

| Signature |
|-----------|
| `new ReadOnlyInput(IAnyState state)` |
| `new ReadOnlyInput(string value, Func<Event<IInput<string>, string>, ValueTask> onChange = null)` |
| `new ReadOnlyInput(string value, Action<Event<IInput<string>, string>> onChange = null)` |
| `new ReadOnlyInput()` |
| `ToReadOnlyInput(IAnyState state)` |


### Supported Types

| Group | Type | Nullable |
|-------|------|----------|
| Other | `object` | - |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Density` | `Density?` | - |
| `Disabled` | `bool` | - |
| `Height` | `Size` | - |
| `Invalid` | `string` | - |
| `Nullable` | `bool` | `Nullable` |
| `Placeholder` | `string` | `Placeholder` |
| `ShowCopyButton` | `bool` | `ShowCopyButton` |
| `Value` | `string` | - |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |


### Events

| Name | Type | Handlers |
|------|------|----------|
| `OnBlur` | `EventHandler<Event<IAnyInput>>` | `OnBlur` |
| `OnChange` | `EventHandler<Event<IInput<string>, string>>` | - |




## Examples


### ReadOnlyInput can be used to display computed or derived values in a form alongside editable inputs.

```csharp
public class ReadOnlyFormDemo : ViewBase
{
    public override object? Build()
    {
        var price = UseState(100.0);
        var quantity = UseState(5);
        var total = price.Value * quantity.Value;
        
        return Layout.Vertical().Gap(2)
            | new NumberInput<double>(price)
                .WithField().Label("Price")
            | new NumberInput<int>(quantity)
                .WithField().Label("Quantity")
            | new ReadOnlyInput<double>(total)
                .WithField().Label("Total");
    }
}
```