using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Effects;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/05_Effects/Animation.md", searchHints: ["motion", "transition", "effects", "animated", "movement", "visual"])]
public class AnimationApp(bool onlyBody = false) : ViewBase
{
    public AnimationApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("animation", "Animation", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("animation-types", "Animation Types", 3), new ArticleHeading("animation-triggers", "Animation Triggers", 3), new ArticleHeading("customization-options", "Customization Options", 3), new ArticleHeading("interactive-feedback-examples", "Interactive Feedback Examples", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Animation").OnLinkClick(onLinkClick)
            | Lead("The Animation [widget](app://onboarding/concepts/widgets) provides a comprehensive set of animation effects that can transform any widget with engaging visual feedback. From subtle hover effects to attention-grabbing loading spinners, animations enhance user experience by providing visual cues, feedback, and polish to your [interface](app://onboarding/concepts/views). Use [Colors](app://api-reference/ivy/colors) and [Icon](app://widgets/primitives/icon) with `.ToIcon().Color()` for styled icons.")
            | new Markdown(
                """"
                The `Animation` widget wraps any child widget and animates it using the specified `AnimationType`. Animations can be triggered automatically, on click, or on hover, with extensive customization options for duration, easing, direction, and more.
                
                ## Basic Usage
                
                The simplest way to add animation to any widget is using the `WithAnimation` extension method. Click any icon to see its animation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Wrap().Gap(4).Align(Align.Center)
    | Icons.LoaderCircle
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Rotate)
        .Trigger(AnimationTrigger.Click)
        .Duration(1)
    | Icons.Heart
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6)
    | Icons.Bell
        .ToIcon()
        .Color(Colors.Orange)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.5)
    | Icons.Star
        .ToIcon()
        .Color(Colors.Yellow)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.8)
    | Icons.Check
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
        .Intensity(1.3)
    | Icons.ArrowRight
        .ToIcon()
        .Color(Colors.Purple)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Wrap().Gap(4).Align(Align.Center)
                        | Icons.LoaderCircle
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Rotate)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(1)
                        | Icons.Heart
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.6)
                        | Icons.Bell
                            .ToIcon()
                            .Color(Colors.Orange)
                            .WithAnimation(AnimationType.Shake)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.5)
                        | Icons.Star
                            .ToIcon()
                            .Color(Colors.Yellow)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.8)
                        | Icons.Check
                            .ToIcon()
                            .Color(Colors.Green)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.4)
                            .Intensity(1.3)
                        | Icons.ArrowRight
                            .ToIcon()
                            .Color(Colors.Purple)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.6)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Animation Types
                
                Ivy provides a rich set of animation types for different use cases. Click any icon to see its animation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Wrap().Gap(4).Align(Align.Center)
    | Icons.ArrowRight
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.5)
    | Icons.ArrowLeft
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.5)
    | Icons.Search
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
        .Intensity(1.2)
    | Icons.X
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
    | Icons.Heart
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(1)
    | Icons.Zap
        .ToIcon()
        .Color(Colors.Yellow)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(1)
    | Icons.Bell
        .ToIcon()
        .Color(Colors.Orange)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6)
    | Icons.LoaderCircle
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Rotate)
        .Trigger(AnimationTrigger.Click)
        .Duration(2)
        .Repeat(3)
    | Icons.Activity
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(1.2)
    | Icons.Sparkles
        .ToIcon()
        .Color(Colors.Purple)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.8))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Wrap().Gap(4).Align(Align.Center)
                        | Icons.ArrowRight
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.5)
                        | Icons.ArrowLeft
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.5)
                        | Icons.Search
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.4)
                            .Intensity(1.2)
                        | Icons.X
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Shake)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.4)
                        | Icons.Heart
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(1)
                        | Icons.Zap
                            .ToIcon()
                            .Color(Colors.Yellow)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(1)
                        | Icons.Bell
                            .ToIcon()
                            .Color(Colors.Orange)
                            .WithAnimation(AnimationType.Shake)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.6)
                        | Icons.LoaderCircle
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Rotate)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(2)
                            .Repeat(3)
                        | Icons.Activity
                            .ToIcon()
                            .Color(Colors.Green)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(1.2)
                        | Icons.Sparkles
                            .ToIcon()
                            .Color(Colors.Purple)
                            .WithAnimation(AnimationType.Shake)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.8)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Animation Triggers
                
                Control when animations start using different trigger types:
                
                **Auto Trigger** - Animations play automatically when the page renders:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Wrap().Gap(4).Align(Align.Center)
    | Icons.LoaderCircle
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Rotate)
        .Trigger(AnimationTrigger.Auto)
        .Duration(2)
    | Icons.Heart
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Auto)
        .Duration(1.5)
    | Icons.Activity
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Auto)
        .Duration(1.2))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Wrap().Gap(4).Align(Align.Center)
                        | Icons.LoaderCircle
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Rotate)
                            .Trigger(AnimationTrigger.Auto)
                            .Duration(2)
                        | Icons.Heart
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Auto)
                            .Duration(1.5)
                        | Icons.Activity
                            .ToIcon()
                            .Color(Colors.Green)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Auto)
                            .Duration(1.2)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("**Click and Hover Triggers** - Try clicking and hovering over these icons:").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Wrap().Gap(4).Align(Align.Center)
    | Icons.Heart
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(1)
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
                    Layout.Wrap().Gap(4).Align(Align.Center)
                        | Icons.Heart
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(1)
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
            | new Markdown(
                """"
                ### Customization Options
                
                Explore different animation customization options. Click any icon to see the effect:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Wrap().Gap(4).Align(Align.Center)
    | Icons.Flame
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.3)
    | Icons.Wind
        .ToIcon()
        .Color(Colors.Cyan)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(1.2)
    | Icons.Moon
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(2)
    | Icons.Heart
        .ToIcon()
        .Color(Colors.Pink)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Delay(0.5)
    | Icons.Star
        .ToIcon()
        .Color(Colors.Yellow)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Intensity(1.5)
    | Icons.Circle
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Intensity(0.7)
    | Icons.Zap
        .ToIcon()
        .Color(Colors.Orange)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Easing(AnimationEasing.BackOut)
    | Icons.Target
        .ToIcon()
        .Color(Colors.Purple)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Easing(AnimationEasing.EaseInOut)
    | Icons.Loader
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Rotate)
        .Trigger(AnimationTrigger.Click)
        .Repeat(3)
    | Icons.Activity
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Repeat(2)
        .RepeatDelay(0.3))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Wrap().Gap(4).Align(Align.Center)
                        | Icons.Flame
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.3)
                        | Icons.Wind
                            .ToIcon()
                            .Color(Colors.Cyan)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(1.2)
                        | Icons.Moon
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(2)
                        | Icons.Heart
                            .ToIcon()
                            .Color(Colors.Pink)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Delay(0.5)
                        | Icons.Star
                            .ToIcon()
                            .Color(Colors.Yellow)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Intensity(1.5)
                        | Icons.Circle
                            .ToIcon()
                            .Color(Colors.Green)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Intensity(0.7)
                        | Icons.Zap
                            .ToIcon()
                            .Color(Colors.Orange)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Easing(AnimationEasing.BackOut)
                        | Icons.Target
                            .ToIcon()
                            .Color(Colors.Purple)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Easing(AnimationEasing.EaseInOut)
                        | Icons.Loader
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Rotate)
                            .Trigger(AnimationTrigger.Click)
                            .Repeat(3)
                        | Icons.Activity
                            .ToIcon()
                            .Color(Colors.Green)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Repeat(2)
                            .RepeatDelay(0.3)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Interactive Feedback Examples
                
                Click any icon to see user interaction feedback animations:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Wrap().Gap(4).Align(Align.Center)
    | Icons.Check
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
        .Intensity(1.4)
    | Icons.Download
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.8)
    | Icons.Upload
        .ToIcon()
        .Color(Colors.Purple)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.8)
    | Icons.X
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6)
        .Intensity(1.5)
    | Icons.Heart
        .ToIcon()
        .Color(Colors.Pink)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.5)
        .Repeat(2)
    | Icons.Star
        .ToIcon()
        .Color(Colors.Amber)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.7)
    | Icons.Share
        .ToIcon()
        .Color(Colors.Cyan)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
        .Intensity(1.3)
    | Icons.CircleAlert
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.5)
        .Repeat(3)
    | Icons.Check
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
        .Easing(AnimationEasing.BackOut)
        .Intensity(1.4)
    | Icons.Info
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Wrap().Gap(4).Align(Align.Center)
                        | Icons.Check
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.4)
                            .Intensity(1.4)
                        | Icons.Download
                            .ToIcon()
                            .Color(Colors.Green)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.8)
                        | Icons.Upload
                            .ToIcon()
                            .Color(Colors.Purple)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.8)
                        | Icons.X
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Shake)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.6)
                            .Intensity(1.5)
                        | Icons.Heart
                            .ToIcon()
                            .Color(Colors.Pink)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.5)
                            .Repeat(2)
                        | Icons.Star
                            .ToIcon()
                            .Color(Colors.Amber)
                            .WithAnimation(AnimationType.Bounce)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.7)
                        | Icons.Share
                            .ToIcon()
                            .Color(Colors.Cyan)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.4)
                            .Intensity(1.3)
                        | Icons.CircleAlert
                            .ToIcon()
                            .Color(Colors.Red)
                            .WithAnimation(AnimationType.Shake)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.5)
                            .Repeat(3)
                        | Icons.Check
                            .ToIcon()
                            .Color(Colors.Green)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.4)
                            .Easing(AnimationEasing.BackOut)
                            .Intensity(1.4)
                        | Icons.Info
                            .ToIcon()
                            .Color(Colors.Blue)
                            .WithAnimation(AnimationType.Pulse)
                            .Trigger(AnimationTrigger.Click)
                            .Duration(0.6)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Animation", "Ivy.AnimationExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Effects/Animation.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Interactive buttons with various animation effects.",
                Vertical().Gap(4)
                | new Markdown("Click any button to see the animation:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(Layout.Wrap().Gap(3).Align(Align.Center)
    | new Button("Bounce", onClick: _ => {})
        .Icon(Icons.Zap)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
    | new Button("Save", onClick: _ => {})
        .Icon(Icons.Check)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.3)
        .Intensity(1.2)
    | new Button("Delete", onClick: _ => {})
        .Icon(Icons.X)
        .Variant(ButtonVariant.Destructive)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
    | new Button("Submit", onClick: _ => {})
        .Icon(Icons.ArrowRight)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
        .Intensity(1.3)
    | new Button("Back", onClick: _ => {})
        .Icon(Icons.ArrowLeft)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.5)
    | new Button("Next", onClick: _ => {})
        .Icon(Icons.ArrowRight)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.5)
    | new Button("Home", onClick: _ => {})
        .Icon(Icons.House)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
        .Intensity(1.2)
    | new Button("Like", onClick: _ => {})
        .Icon(Icons.Heart)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.4)
        .Repeat(2)
        .Intensity(1.3)
    | new Button("Share", onClick: _ => {})
        .Icon(Icons.Share)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6)
    | new Button("Comment", onClick: _ => {})
        .Icon(Icons.Circle)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.3)
        .Intensity(0.8))),
                    new Tab("Code", new CodeBlock(
                        """"
                        Layout.Wrap().Gap(3).Align(Align.Center)
                            | new Button("Bounce", onClick: _ => {})
                                .Icon(Icons.Zap)
                                .WithAnimation(AnimationType.Bounce)
                                .Trigger(AnimationTrigger.Click)
                            | new Button("Save", onClick: _ => {})
                                .Icon(Icons.Check)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.3)
                                .Intensity(1.2)
                            | new Button("Delete", onClick: _ => {})
                                .Icon(Icons.X)
                                .Variant(ButtonVariant.Destructive)
                                .WithAnimation(AnimationType.Shake)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.4)
                            | new Button("Submit", onClick: _ => {})
                                .Icon(Icons.ArrowRight)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.4)
                                .Intensity(1.3)
                            | new Button("Back", onClick: _ => {})
                                .Icon(Icons.ArrowLeft)
                                .WithAnimation(AnimationType.Bounce)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.5)
                            | new Button("Next", onClick: _ => {})
                                .Icon(Icons.ArrowRight)
                                .WithAnimation(AnimationType.Bounce)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.5)
                            | new Button("Home", onClick: _ => {})
                                .Icon(Icons.House)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.4)
                                .Intensity(1.2)
                            | new Button("Like", onClick: _ => {})
                                .Icon(Icons.Heart)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.4)
                                .Repeat(2)
                                .Intensity(1.3)
                            | new Button("Share", onClick: _ => {})
                                .Icon(Icons.Share)
                                .WithAnimation(AnimationType.Bounce)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.6)
                            | new Button("Comment", onClick: _ => {})
                                .Icon(Icons.Circle)
                                .WithAnimation(AnimationType.Shake)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.3)
                                .Intensity(0.8)
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Media & Directional Examples",
                Vertical().Gap(4)
                | new Markdown("Click any icon to see media controls and directional animations:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(Layout.Wrap().Gap(4).Align(Align.Center)
    | Icons.Play
        .ToIcon()
        .Color(Colors.Green)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(1.5)
    | Icons.Pause
        .ToIcon()
        .Color(Colors.Orange)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(1)
    | Icons.SkipBack
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6)
    | Icons.SkipForward
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.6)
    | Icons.Volume
        .ToIcon()
        .Color(Colors.Purple)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(1.2)
    | Icons.VolumeX
        .ToIcon()
        .Color(Colors.Red)
        .WithAnimation(AnimationType.Shake)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.8)
    | Icons.Repeat
        .ToIcon()
        .Color(Colors.Blue)
        .WithAnimation(AnimationType.Rotate)
        .Trigger(AnimationTrigger.Click)
        .Duration(1.5)
    | Icons.Music
        .ToIcon()
        .Color(Colors.Pink)
        .WithAnimation(AnimationType.Pulse)
        .Trigger(AnimationTrigger.Click)
        .Duration(1.8)
    | Icons.ArrowRight
        .ToIcon()
        .Color(Colors.Cyan)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.7)
    | Icons.ArrowLeft
        .ToIcon()
        .Color(Colors.Cyan)
        .WithAnimation(AnimationType.Bounce)
        .Trigger(AnimationTrigger.Click)
        .Duration(0.7))),
                    new Tab("Code", new CodeBlock(
                        """"
                        Layout.Wrap().Gap(4).Align(Align.Center)
                            | Icons.Play
                                .ToIcon()
                                .Color(Colors.Green)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(1.5)
                            | Icons.Pause
                                .ToIcon()
                                .Color(Colors.Orange)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(1)
                            | Icons.SkipBack
                                .ToIcon()
                                .Color(Colors.Blue)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.6)
                            | Icons.SkipForward
                                .ToIcon()
                                .Color(Colors.Blue)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.6)
                            | Icons.Volume
                                .ToIcon()
                                .Color(Colors.Purple)
                                .WithAnimation(AnimationType.Bounce)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(1.2)
                            | Icons.VolumeX
                                .ToIcon()
                                .Color(Colors.Red)
                                .WithAnimation(AnimationType.Shake)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.8)
                            | Icons.Repeat
                                .ToIcon()
                                .Color(Colors.Blue)
                                .WithAnimation(AnimationType.Rotate)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(1.5)
                            | Icons.Music
                                .ToIcon()
                                .Color(Colors.Pink)
                                .WithAnimation(AnimationType.Pulse)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(1.8)
                            | Icons.ArrowRight
                                .ToIcon()
                                .Color(Colors.Cyan)
                                .WithAnimation(AnimationType.Bounce)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.7)
                            | Icons.ArrowLeft
                                .ToIcon()
                                .Color(Colors.Cyan)
                                .WithAnimation(AnimationType.Bounce)
                                .Trigger(AnimationTrigger.Click)
                                .Duration(0.7)
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(ApiReference.Ivy.ColorsApp), typeof(Widgets.Primitives.IconApp)]; 
        return article;
    }
}

