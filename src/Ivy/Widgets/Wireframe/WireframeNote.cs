// ReSharper disable once CheckNamespace
namespace Ivy;

public enum WireframeNoteColor
{
    Yellow,
    Blue,
    Green,
    Pink,
    Orange,
    Purple
}

public record WireframeNote : WidgetBase<WireframeNote>
{
    public WireframeNote(string? text = null, WireframeNoteColor color = WireframeNoteColor.Yellow)
    {
        Text = text;
        Color = color;
    }

    internal WireframeNote() { }

    [Prop] public string? Text { get; set; }

    [Prop] public WireframeNoteColor Color { get; set; } = WireframeNoteColor.Yellow;

    public static WireframeNote operator |(WireframeNote widget, object child)
    {
        throw new NotSupportedException("WireframeNote does not support children. Use the Text property.");
    }
}

public static class WireframeNoteExtensions
{
    public static WireframeNote Text(this WireframeNote note, string text)
        => note with { Text = text };

    public static WireframeNote Color(this WireframeNote note, WireframeNoteColor color)
        => note with { Color = color };
}
