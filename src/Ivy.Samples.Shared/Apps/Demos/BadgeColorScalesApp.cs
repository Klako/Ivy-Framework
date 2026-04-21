namespace Ivy.Samples.Shared.Apps.Demos;

[App(icon: Icons.Palette, title: "Badge Color Scales", searchHints: ["badge", "color", "tint", "scale", "palette", "dark", "pr", "4227"])]
public class BadgeColorScalesApp : SampleBase
{
    private static readonly BadgeVariant[] Variants = [
        BadgeVariant.Primary,
        BadgeVariant.Secondary,
        BadgeVariant.Destructive,
        BadgeVariant.Success,
        BadgeVariant.Warning,
        BadgeVariant.Info,
        BadgeVariant.Outline,
    ];

    private static readonly Colors[] Chromatic = [
        Colors.Red, Colors.Orange, Colors.Amber, Colors.Yellow, Colors.Lime,
        Colors.Green, Colors.Emerald, Colors.Teal, Colors.Cyan, Colors.Sky,
        Colors.Blue, Colors.Indigo, Colors.Violet, Colors.Purple, Colors.Fuchsia,
        Colors.Pink, Colors.Rose,
    ];

    private static readonly Colors[] Neutrals = [
        Colors.Slate, Colors.Gray, Colors.Zinc, Colors.Neutral, Colors.Stone,
    ];

    private static readonly Colors[] Semantic = [
        Colors.Primary, Colors.Secondary, Colors.Destructive,
        Colors.Success, Colors.Warning, Colors.Info, Colors.IvyGreen,
    ];

    protected override object? BuildSample()
    {
        return Layout.Vertical()
               | Text.H1("Badge Color Scales")
               | Text.Markdown(
                   "Visual test for [PR #4227](https://github.com/Ivy-Interactive/Ivy-Framework/pull/4227). "
                   + "Badges render as soft-tinted chips — bg `400` + text `900` in light mode, "
                   + "bg `800` + text `100` in dark mode. Toggle the theme to verify the swap.")

               | Text.H2("Variants")
               | Layout.Wrap(Variants.Select(v => new Badge(v.ToString(), variant: v)))

               | Text.H2("Chromatic colors (via Colors enum)")
               | Layout.Wrap(Chromatic.Select(c => new Badge(c.ToString()).Color(c)))

               | Text.H2("Neutrals")
               | Layout.Wrap(Neutrals.Select(c => new Badge(c.ToString()).Color(c)))

               | Text.H2("Semantic colors (via Colors enum)")
               | Layout.Wrap(Semantic.Select(c => new Badge(c.ToString()).Color(c)))

               | Text.H2("Sizes (all tinted)")
               | (Layout.Horizontal()
                  | new Badge("Small", variant: BadgeVariant.Destructive).Small()
                  | new Badge("Medium", variant: BadgeVariant.Destructive)
                  | new Badge("Large", variant: BadgeVariant.Destructive).Large())

               | Text.H2("With icons")
               | (Layout.Horizontal()
                  | new Badge("Success", variant: BadgeVariant.Success, icon: Icons.CircleCheck)
                  | new Badge("Warning", variant: BadgeVariant.Warning, icon: Icons.TriangleAlert)
                  | new Badge("Info", variant: BadgeVariant.Info, icon: Icons.Info)
                  | new Badge("Destructive", variant: BadgeVariant.Destructive, icon: Icons.CircleX)
                  | new Badge("Teal", icon: Icons.Leaf).Color(Colors.Teal)
                  | new Badge("Rose", icon: Icons.Heart).Color(Colors.Rose));
    }
}
