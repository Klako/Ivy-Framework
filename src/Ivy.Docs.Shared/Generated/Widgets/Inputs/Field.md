# Field

*Group any input with a label, description, help text, and required indicator for a consistent, accessible [form](../../01_Onboarding/02_Concepts/08_Forms.md) design.*

The `Field` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) acts as a **wrapper** around any input (such as `TextInput`, `Select`, `DateTime`, etc.).  
It provides a standardized way to display a label, optional description, help text [tooltips](../03_Common/03_Tooltip.md), and visual cues like a required asterisk.  

This makes [forms](../../01_Onboarding/02_Concepts/08_Forms.md) easier to build and ensures inputs remain consistent in layout and accessibility.

## Basic Usage

Here's how to wrap a `TextInput` in a `Field`:

```csharp
public class BasicFieldDemo : ViewBase
{
    public override object? Build()
    {
        var name = UseState("");
        return new Field(
            new TextInput(name)
                .Placeholder("Enter your name")
        )
        .Label("Name")
        .Description("Your full name as it appears on official documents")
        .Required();
    }
}
```

> **info:** `Field` does not provide inputs by itself - it always wraps an input widget like `TextInput`, `Select`, or `Checkbox`.

## Properties

A `Field` supports the following common properties:

* **Label(string)** - The display label above the input.
* **Description(string)** - An optional helper text shown below the input.
* **Help(string)** - An optional help text displayed as a tooltip on an info icon next to the label.
* **Required(bool)** - Marks the input as required (adds an asterisk or style cue).

### Layout Configuration

* **LabelPosition(LabelPosition)** - Controls whether the label is positioned above the input (`Top`) or beside it (`Left`).

```csharp
public class LabelPositionExample : ViewBase
{
    public override object? Build()
    {
        var email = UseState("");
        return email.ToTextInput()
            .Placeholder("admin@company.com")
            .WithField()
            .Label("Work Email")
            .LabelPosition(LabelPosition.Left)
            .Description("We'll use this for account recovery");
    }
}
```

### Properties Usage

```csharp
public class FieldExample : ViewBase
{
    public override object? Build()
    {
        var username = UseState("");
        return username.ToTextInput()
            .Placeholder("Enter username")
            .WithField()
            .Label("Username")
            .Description("Must be at least 8 characters long")
            .Help("Your username must be unique and contain only letters, numbers, and underscores")
            .Required();
    }
}
```

### Wrapping Other Inputs

Since `Field` works generically, it can wrap **any widget**:

```csharp
public class MixedInputsDemo : ViewBase
{
    public override object? Build()
    {
        var dateState = UseState(DateTime.Today);

        var accepted = UseState(false);
        var options = new List<string>() { "I read the terms and conditions and I agree"};
        var selectedNotice = UseState(new string[]{});
        return Layout.Vertical()
            | dateState.ToDateTimeInput()
                .Variant(DateTimeInputVariant.Date)
                .WithField()
                .Label("Date of birth")
            | selectedNotice.ToSelectInput(options.ToOptions())
                .Variant(SelectInputVariant.List)
                .WithField()
                .Label("Terms & Conditions")
                .Description("You must agree before continuing")
                .Required();
    }
}
```

> **Note:** Use `Field` whenever you want **consistent form layout** across your application with labels, description and required asterisk.


## API

[View Source: Field.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/Field.cs)

### Constructors

| Signature |
|-----------|
| `new Field(IAnyInput input, string label = null, string description = null, bool required = false, string help = null, Density density = Density.Medium)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Density` | `Density?` | - |
| `Description` | `string` | - |
| `Height` | `Size` | - |
| `Help` | `string` | - |
| `Label` | `string` | - |
| `LabelPosition` | `LabelPosition` | - |
| `Required` | `bool` | - |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |