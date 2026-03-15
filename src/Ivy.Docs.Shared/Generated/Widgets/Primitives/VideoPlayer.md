# Video Player

*Play video content with browser controls. Supports common video formats (e.g., MP4, WebM) and provides customizable playback options.*

The `VideoPlayer` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) displays a video player with browser-native controls in your [app](../../01_Onboarding/02_Concepts/15_Apps.md). This widget is for playing video files.

## Basic Usage

Create a simple video player:

```csharp
new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
```

## Configuration Options

### Autoplay, Muted, and Looping

Configure automatic playback and looping (muted autoplay is more likely to be allowed by browsers):

```csharp
Layout.Vertical().Gap(4)
| Text.H4("Muted Autoplay Video")
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Autoplay(true)
    .Muted(true)
| Text.H4("Looping Video")
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Loop(true)
```

### Controls Toggle

Enable or disable browser playback controls:

```csharp
Layout.Vertical().Gap(4)
| Text.P("With Controls (default)").Small()
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Controls(true)
| Text.P("Without Controls (programmatic control only)").Small()
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Controls(false)
```

### Custom Sizing

Control width and height of the video player:

```csharp
Layout.Vertical().Gap(4)
| Text.P("50% width, fixed height").Small()
| new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Width(Size.Fraction(0.5f))
    .Height(Size.Units(50))
```

### Poster Image (Preview Frame)

Display a placeholder image before playback:

```csharp
new VideoPlayer("https://www.w3schools.com/html/mov_bbb.mp4")
    .Poster("https://www.w3schools.com/html/pic_trulli.jpg")
```

### Large Video Files

The VideoPlayer also supports streaming of large video files.

```csharp
new VideoPlayer("https://archive.org/download/BigBuckBunny_124/Content/big_buck_bunny_720p_surround.mp4")
    .Height(Size.Units(100))
```

### YouTube Video Embed

Embed a YouTube video directly by providing its URL:

```csharp
new VideoPlayer("https://www.youtube.com/watch?v=dQw4w9WgXcQ")
    .Height(Size.Units(100))
    .Controls(false)
```


## API

[View Source: VideoPlayer.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/VideoPlayer.cs)

### Constructors

| Signature |
|-----------|
| `new VideoPlayer(string source = null, bool autoplay = false, bool controls = true, bool muted = false, bool loop = false, string poster = null)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Autoplay` | `bool` | `Autoplay` |
| `Controls` | `bool` | `Controls` |
| `Density` | `Density?` | - |
| `Height` | `Size` | - |
| `Loop` | `bool` | `Loop` |
| `Muted` | `bool` | `Muted` |
| `Poster` | `string` | `Poster` |
| `Source` | `string` | `Source` |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |