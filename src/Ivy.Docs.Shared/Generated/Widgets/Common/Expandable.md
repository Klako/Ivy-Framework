# Expandable

*Create collapsible content sections that users can expand and collapse to maintain clean, organized [layouts](../../01_Onboarding/02_Concepts/04_Layout.md).*

The `Expandable` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) allows you to hide and show content interactively, providing a clean and organized way to present information. It's perfect for organizing content into collapsible sections, FAQs, or any scenario where you want to reduce visual clutter.

## Basic Usage

Here's a simple example of an expandable widget.

```csharp
new Expandable("Click to expand", 
    "This is the hidden content that appears when you expand the widget.")
```

### Nested Expandables

Create hierarchical structures by nesting expandable widgets.

```csharp
new Expandable("Main Section", 
    Layout.Vertical().Gap(2)
        | Text.H4("Overview")
        | Text.Muted("This is the main content")
        | new Expandable("Subsection 1", "Details about subsection 1")
        | new Expandable("Subsection 2", "Details about subsection 2")
        | new Expandable("Subsection 3", "Details about subsection 3")
)
```

### Disabled, Open and Closed

> **info:** You can also disable an expandable, or set it to be open by default.

```csharp
Layout.Vertical().Gap(2)
    | new Expandable("Normal", "This expandable works normally")
    | new Expandable("Disabled", "This expandable is disabled").Disabled()
    | new Expandable("Open", "This expandable is open").Open()
```

### With Icon

Add an icon to the expandable header to provide additional visual context.

```csharp
Layout.Vertical().Gap(2)
    | new Expandable("Settings", "Configure your application preferences here.").Icon(Icons.Settings)
    | new Expandable("User Profile", "View and edit your profile information.").Icon(Icons.User)
    | new Expandable("Notifications", "Manage your notification preferences.").Icon(Icons.Bell)
```


## API

[View Source: Expandable.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Expandable.cs)

### Constructors

| Signature |
|-----------|
| `new Expandable(object header, object content)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Density` | `Density?` | - |
| `Disabled` | `bool` | `Disabled` |
| `Height` | `Size` | - |
| `Icon` | `Icons?` | `Icon` |
| `Open` | `bool` | `Open` |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |




## Examples


### Form Sections

Organize [forms](../../01_Onboarding/02_Concepts/08_Forms.md) into logical, collapsible sections.

```csharp
public class SimpleFormExample : ViewBase
{
    public record PersonalInfo(string FirstName, string LastName, string Email, string Phone);
    public record AddressInfo(string Street, string City, string State, string ZipCode);
    public record UserPreferences(bool EmailNotifications, bool SmsNotifications, string Language, string Theme);

    public override object? Build()
    {
        var personalInfo = UseState(() => new PersonalInfo("", "", "", ""));
        var addressInfo = UseState(() => new AddressInfo("", "", "", ""));
        var preferences = UseState(() => new UserPreferences(false, false, "en", "light"));

        return Layout.Vertical().Gap(2)
            | new Expandable("Personal Information", personalInfo.ToForm())
            | new Expandable("Address", addressInfo.ToForm())
            | new Expandable("Preferences", preferences.ToForm());
    }
}
```