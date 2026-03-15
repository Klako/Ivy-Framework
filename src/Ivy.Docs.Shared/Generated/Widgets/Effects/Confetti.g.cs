using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Effects;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/05_Effects/Confetti.md", searchHints: ["celebration", "particles", "animation", "effects", "visual", "party"])]
public class ConfettiApp(bool onlyBody = false) : ViewBase
{
    public ConfettiApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("confetti", "Confetti", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("auto-trigger", "Auto Trigger", 3), new ArticleHeading("hover-trigger", "Hover Trigger", 3), new ArticleHeading("list-usage", "List Usage", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Confetti").OnLinkClick(onLinkClick)
            | Lead("Add celebratory confetti effects to any [widget](app://onboarding/concepts/widgets) with customizable triggers for automatic, click, or hover activation.")
            | new Markdown(
                """"
                The `Confetti` animation can be triggered automatically, on click, or when the mouse hovers over the widget. Perfect for celebrating user achievements, [form](app://onboarding/concepts/forms) submissions, or adding delightful interactions to your [interface](app://onboarding/concepts/views).
                
                ## Basic Usage
                
                Wrap any widget with confetti using the `WithConfetti()` extension method:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Button("Confetti on click!")
    .WithConfetti(AnimationTrigger.Click))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Button("Confetti on click!")
                        .WithConfetti(AnimationTrigger.Click)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Auto Trigger
                
                Confetti fires automatically when the widget is first rendered, perfect for welcoming users or celebrating initial page loads.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Text.Block("Welcome!")
    .WithConfetti(AnimationTrigger.Auto))),
                new Tab("Code", new CodeBlock(
                    """"
                    Text.Block("Welcome!")
                        .WithConfetti(AnimationTrigger.Auto)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Hover Trigger
                
                Confetti activates when the mouse hovers over the widget, providing immediate visual feedback for interactive elements.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Card("Hover over me")
    .WithConfetti(AnimationTrigger.Hover))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Card("Hover over me")
                        .WithConfetti(AnimationTrigger.Hover)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### List Usage
                
                Demonstrates how to add confetti to list items, making each selection feel special and celebratory.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(10)
    | new List(new[] { "First option on click", "Second option on click" }
        .Select(level => new ListItem(level, onClick: _ => {}, icon: Icons.Circle)
            .WithConfetti(AnimationTrigger.Click))))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(10)
                        | new List(new[] { "First option on click", "Second option on click" }
                            .Select(level => new ListItem(level, onClick: _ => {}, icon: Icons.Circle)
                                .WithConfetti(AnimationTrigger.Click)))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Confetti", "Ivy.ConfettiExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Effects/Confetti.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Integration with Other Widgets",
                Vertical().Gap(4)
                | new Markdown("Confetti works seamlessly with all Ivy [widgets](app://onboarding/concepts/widgets), allowing you to add celebratory effects to any interface element (for example Button, Card, ListItem, Badge, or text).").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(Layout.Vertical().Gap(10)
    | new Button("Action").WithConfetti(AnimationTrigger.Click)
    | new Card("Content").WithConfetti(AnimationTrigger.Hover)
    | new ListItem("Item").WithConfetti(AnimationTrigger.Click)
    | Text.Block("Message").WithConfetti(AnimationTrigger.Hover)
    | new Badge("Success").WithConfetti(AnimationTrigger.Hover))),
                    new Tab("Code", new CodeBlock(
                        """"
                        Layout.Vertical().Gap(10)
                            | new Button("Action").WithConfetti(AnimationTrigger.Click)
                            | new Card("Content").WithConfetti(AnimationTrigger.Hover)
                            | new ListItem("Item").WithConfetti(AnimationTrigger.Click)
                            | Text.Block("Message").WithConfetti(AnimationTrigger.Hover)
                            | new Badge("Success").WithConfetti(AnimationTrigger.Hover)
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.ViewsApp)]; 
        return article;
    }
}

