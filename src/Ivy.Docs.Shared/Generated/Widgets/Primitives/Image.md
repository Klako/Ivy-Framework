# Image

*Display images with automatic loading, responsive sizing, and proper accessibility features for rich visual content.*

The `Image` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) displays images in your [app](../../01_Onboarding/02_Concepts/10_Apps.md).

```csharp
new Image("https://api.images.cat/150/150")
```

### Supported Image Sources

The Image widget works with multiple source types:

- **External URLs**: Images hosted on external servers
- **Local files**: Images stored in your application's file system
- **Data URIs**: Base64-encoded images embedded directly in the code

```csharp
var dataUri = "data:image/png;base64,iVBORw0KGgoAAAANS...";
new Image(dataUri);

new Image("https://example.com/image.jpg");  // External URL
new Image("/ivy/images/logo.png");           // Local file
```


## API

[View Source: Image.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Image.cs)

### Constructors

| Signature |
|-----------|
| `new Image(string src)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `Alt` | `string` | - |
| `AspectRatio` | `float?` | - |
| `Caption` | `string` | - |
| `Density` | `Density?` | - |
| `Height` | `Size` | - |
| `Link` | `string` | `Link` |
| `ObjectFit` | `ImageFit?` | - |
| `Src` | `string` | - |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |


### Events

| Name | Type | Handlers |
|------|------|----------|
| `OnClick` | `EventHandler<Event<Image>>` | `OnClick` |