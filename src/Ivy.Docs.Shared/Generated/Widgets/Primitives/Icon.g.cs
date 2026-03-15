using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/02_Icon.md", searchHints: ["lucide", "symbols", "graphics", "glyphs", "svg", "icons"])]
public class IconApp(bool onlyBody = false) : ViewBase
{
    public IconApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("icon", "Icon", 1), new ArticleHeading("lucide-icons", "Lucide Icons", 2), new ArticleHeading("colors", "Colors", 2), new ArticleHeading("sizes", "Sizes", 2), new ArticleHeading("animation", "Animation", 2), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Icon").OnLinkClick(onLinkClick)
            | Lead("Display crisp Lucide vector icons with customizable colors, sizes, and animation-ready styling. Use [Colors](app://api-reference/ivy/colors) and the [Animation](app://widgets/effects/animation) widget with `.ToIcon().Color().WithAnimation()` to create expressive, interactive icons.")
            | new Markdown(
                """"
                The `Icon` [widget](app://onboarding/concepts/widgets) displays vector icons from Ivy's built-in icon set. Icons enhance visual communication and can be customized with different colors, sizes, and animations.
                
                ## Lucide Icons
                
                We use the [Lucide Icons](https://lucide.dev/icons/) set, which is a collection of 1000+ free icons. You can find the full set [here](https://lucide.dev/icons/).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new LucideIconsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class LucideIconsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var searchState = UseState("code");
                            var iconsState = UseState<Icons[]>(Array.Empty<Icons>());
                    
                            UseEffect(() =>
                            {
                                var allIcons = Enum.GetValues<Icons>().Where(e => e != Icons.None);
                                iconsState.Set(string.IsNullOrEmpty(searchState.Value)
                                    ? []
                                    : allIcons.Where(e => e.ToString().Contains(searchState.Value, StringComparison.OrdinalIgnoreCase)).Take(10).ToArray());
                            }, [ EffectTrigger.OnMount(), searchState.Throttle(TimeSpan.FromMilliseconds(500)).ToTrigger() ]);
                    
                            var searchInput = searchState.ToSearchInput().Placeholder("Type an icon name");
                    
                            var icons = iconsState.Value.Select(e => Layout.Horizontal().Gap(2)
                                | new Button(null, @event =>
                                {
                                    var iconCode = "Icons." + e.ToString();
                                    client.CopyToClipboard(iconCode);
                                    client.Toast($"Copied '{iconCode}' to clipboard", "Icon Code Copied");
                                }, ButtonVariant.Ghost, e).Small().WithTooltip($"Click to copy {e.ToString()}")
                                | Text.Label("Icons." + e.ToString())
                            );
                    
                            return Layout.Vertical()
                                   | searchInput
                                   | icons
                                ;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Colors
                
                You can customize the color of the icons using the [`Color`](app://api-reference/ivy/colors) parameter.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Icon(Icons.Clipboard, Colors.Red)
    | new Icon(Icons.Settings, Colors.Green)
    | new Icon(Icons.User, Colors.Blue)
    | new Icon(Icons.Calendar, Colors.Purple)
    | new Icon(Icons.Mail, Colors.Orange))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Icon(Icons.Clipboard, Colors.Red)
                        | new Icon(Icons.Settings, Colors.Green)
                        | new Icon(Icons.User, Colors.Blue)
                        | new Icon(Icons.Calendar, Colors.Purple)
                        | new Icon(Icons.Mail, Colors.Orange)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Sizes
                
                You can also customize the size of the icons using the [`Size`](app://api-reference/ivy/size) parameter.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Icon(Icons.Cloud).Small()
    | new Icon(Icons.Cloud)
    | new Icon(Icons.Cloud).Large())),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Icon(Icons.Cloud).Small()
                        | new Icon(Icons.Cloud)
                        | new Icon(Icons.Cloud).Large()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Animation
                
                You can animate icons using the [`Animation`](app://widgets/effects/animation) widget and the `.WithAnimation()` extension methods.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal().Align(Align.Center)
    | Icons.LoaderCircle
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Rotate)
        .Trigger(AnimationTrigger.Auto)
        .Duration(1)
    | Icons.Heart
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
    | Icons.MousePointer
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Hover)
        .Trigger(AnimationTrigger.Hover)
    | Icons.Target
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Hover)
        .Duration(0.5)
    | Icons.Bell
        .ToIcon()
        .Color(Colors.Orange)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6)
    | Icons.Rocket
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.8)
    | Icons.Gift
        .ToIcon()
        .Color(Colors.Pink)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Hover)
        .Duration(0.7))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal().Align(Align.Center)
                        | Icons.LoaderCircle
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Rotate)
                            .Trigger(AnimationTrigger.Auto)
                            .Duration(1)
                        | Icons.Heart
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                        | Icons.MousePointer
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Hover)
                            .Trigger(AnimationTrigger.Hover)
                        | Icons.Target
                            .ToIcon()
                            .Color(Colors.Green)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Hover)
                            .Duration(0.5)
                        | Icons.Bell
                            .ToIcon()
                            .Color(Colors.Orange)
                            .WithAnimation(AnimationType.Shake)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.6)
                        | Icons.Rocket
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.8)
                        | Icons.Gift
                            .ToIcon()
                            .Color(Colors.Pink)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Hover)
                            .Duration(0.7)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("How do I change the size of an Icon?",
                Vertical().Gap(4)
                | new Markdown("Use the `.Small()` or `.Large()` extension methods:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    new Icon(Icons.Star).Small()   // small icon
                    new Icon(Icons.Star)            // default size
                    new Icon(Icons.Star).Large()   // large icon
                    """",Languages.Csharp)
                | new Markdown("**Important:** There is no `.WithIconSize()` method or `IconSize` enum. Use the simple `.Small()` and `.Large()` fluent modifiers.").OnLinkClick(onLinkClick)
            )
            | new WidgetDocsView("Ivy.Icon", "Ivy.IconExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Icon.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(ApiReference.Ivy.ColorsApp), typeof(Widgets.Effects.AnimationApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.SizeApp)]; 
        return article;
    }
}


public class LucideIconsView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var searchState = UseState("code");
        var iconsState = UseState<Icons[]>(Array.Empty<Icons>());
        
        UseEffect(() =>
        {
            var allIcons = Enum.GetValues<Icons>().Where(e => e != Icons.None);
            iconsState.Set(string.IsNullOrEmpty(searchState.Value)
                ? []
                : allIcons.Where(e => e.ToString().Contains(searchState.Value, StringComparison.OrdinalIgnoreCase)).Take(10).ToArray());
        }, [ EffectTrigger.OnMount(), searchState.Throttle(TimeSpan.FromMilliseconds(500)).ToTrigger() ]);
        
        var searchInput = searchState.ToSearchInput().Placeholder("Type an icon name");
        
        var icons = iconsState.Value.Select(e => Layout.Horizontal().Gap(2)
            | new Button(null, @event =>
            {
                var iconCode = "Icons." + e.ToString();
                client.CopyToClipboard(iconCode);
                client.Toast($"Copied '{iconCode}' to clipboard", "Icon Code Copied");
            }, ButtonVariant.Ghost, e).Small().WithTooltip($"Click to copy {e.ToString()}")
            | Text.Label("Icons." + e.ToString())
        );

        return Layout.Vertical()
               | searchInput
               | icons
            ; 
    }
}
