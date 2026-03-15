# Kbd

*Display keyboard shortcuts and key combinations with proper styling to help users identify commands and improve documentation.*

The `Kbd` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) displays keyboard shortcuts or key combinations with proper styling. It helps users identify key commands and improves documentation clarity.

```csharp
Layout.Horizontal() | 
    new Kbd("Ctrl + C") | 
    new Kbd("Shift + Ctrl + C")
```


## API

[View Source: Kbd.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Kbd.cs)

### Constructors

| Signature |
|-----------|
| `new Kbd(object content)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Density` | `Density?` | - |
| `Height` | `Size` | - |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |