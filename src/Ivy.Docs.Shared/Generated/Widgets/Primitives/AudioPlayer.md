# Audio Player

*Play audio content with browser controls. Supports common audio formats and provides customizable playback options.*

The `AudioPlayer` [widget](../../01_Onboarding/02_Concepts/03_Widgets.md) displays an audio player with browser controls in your [app](../../01_Onboarding/02_Concepts/15_Apps.md). This widget is for playing audio files, not recording them.

## Basic Usage

Create a simple audio player:

```csharp
new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
```

## Configuration Options

### Autoplay and Looping

Configure automatic playback and looping:

```csharp
Layout.Vertical().Gap(4)
| Text.H4("Muted Autoplay (browsers allow this)")
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Autoplay(true)
    .Muted(true)
| Text.H4("Looping Audio")
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Loop(true)
```

### Preload Strategy

Control how much audio data is loaded:

```csharp
Layout.Vertical().Gap(4)
| Text.P("Preload: None (no data loaded)").Small()
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Preload(AudioPreload.None)
| Text.P("Preload: Metadata (duration and basic info)").Small()
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Preload(AudioPreload.Metadata)
| Text.P("Preload: Auto (entire file)").Small()
| new AudioPlayer("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3")
    .Preload(AudioPreload.Auto)
```


## API

[View Source: AudioPlayer.cs](https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/AudioPlayer.cs)

### Constructors

| Signature |
|-----------|
| `new AudioPlayer(string src)` |


### Properties

| Name | Type | Setters |
|------|------|---------|
| `AspectRatio` | `float?` | - |
| `Autoplay` | `bool` | - |
| `Controls` | `bool` | - |
| `Density` | `Density?` | - |
| `Height` | `Size` | - |
| `Loop` | `bool` | - |
| `Muted` | `bool` | - |
| `Preload` | `AudioPreload` | - |
| `Src` | `string` | - |
| `Visible` | `bool` | - |
| `Width` | `Size` | - |