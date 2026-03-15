# Terminal

*Display terminal-style output with commands and responses in a visually distinct console format with copy functionality.*

The `Terminal` widget renders a terminal-like interface ideal for displaying CLI commands, code snippets, or command outputs. It includes a header with title and a copy button for easy command copying.

## Basic Usage

Here's a simple example of a terminal displaying installation commands:

```csharp
Layout.Vertical()
    | new Terminal()
        .Title("Getting Started")
        .AddCommand("dotnet new install Ivy.Templates")
        .AddOutput("Template 'Ivy Application' installed successfully.")
```

## Styling

You can customize the terminal appearance and behavior:

```csharp
Layout.Vertical().Gap(4)
    | Text.P("With Title").Large()
    | new Terminal()
        .Title("My Terminal")
        .AddCommand("echo Hello World")
        .AddOutput("Hello World")
    | Text.P("Without Header").Large()
    | new Terminal() { ShowHeader = false }
        .AddCommand("npm install")
        .AddOutput("added 125 packages")
    | Text.P("Without Copy Button").Large()
    | new Terminal()
        .Title("Read Only")
        .ShowCopyButton(false)
        .AddCommand("git status")
        .AddOutput("nothing to commit, working tree clean")
```


## API

[View Source: Terminal.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Terminal.cs)

### Constructors

| Signature |
|-----------|
| `new Terminal()` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Density` | `Density?` | - |
| `Height` | `Size` | - |
| `Lines` | `TerminalLine[]` | - |
| `ShowCopyButton` | `bool` | `ShowCopyButton` |
| `ShowHeader` | `bool` | - |
| `Title` | `string` | `Title` |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |




## Examples


### Installation Guide

Display step-by-step installation instructions for your users:

```csharp
Layout.Vertical()
    | new Terminal()
        .Title("Install MyApp")
        .AddCommand("npm install myapp")
        .AddOutput("added 42 packages in 3.2s")
        .AddCommand("myapp init")
        .AddOutput("Configuration created at ./myapp.config.json")
        .AddCommand("myapp start")
        .AddOutput("Server running at http://localhost:3000")
```