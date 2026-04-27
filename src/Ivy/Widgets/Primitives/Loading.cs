// ReSharper disable once CheckNamespace
namespace Ivy;

/// <summary>
/// Visual variants supported by the <see cref="Loading"/> widget.
/// </summary>
public enum LoadingType
{
    /// <summary>A simple animated spinner with a "Loading..." label.</summary>
    Spinner,
    /// <summary>A randomized skeleton placeholder layout.</summary>
    Skeleton
}

/// <summary>
/// A loading indicator. Renders as a spinner by default; use
/// <see cref="LoadingExtensions.Skeleton(Loading)"/> to render a skeleton placeholder instead.
/// </summary>
public record Loading : WidgetBase<Loading>
{
    /// <summary>Initializes a new <see cref="Loading"/> with the default <see cref="LoadingType.Spinner"/> variant.</summary>
    public Loading() { }

    /// <summary>The visual variant rendered by this loading indicator.</summary>
    [Prop] public LoadingType Type { get; set; } = LoadingType.Spinner;
}

/// <summary>
/// Fluent extension methods for configuring a <see cref="Loading"/> widget.
/// </summary>
public static class LoadingExtensions
{
    /// <summary>Returns a <see cref="Loading"/> configured to render the spinner variant.</summary>
    public static Loading Spinner(this Loading loading) => loading with { Type = LoadingType.Spinner };

    /// <summary>Returns a <see cref="Loading"/> configured to render the randomized skeleton variant.</summary>
    public static Loading Skeleton(this Loading loading) => loading with { Type = LoadingType.Skeleton };
}
