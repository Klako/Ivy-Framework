using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/02_Badge.md", searchHints: ["tag", "label", "chip", "status", "indicator", "pill"])]
public class BadgeApp(bool onlyBody = false) : ViewBase
{
    public BadgeApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("badge", "Badge", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("using-extension-methods", "Using Extension Methods", 3), new ArticleHeading("icons", "Icons", 3), new ArticleHeading("click-listener", "Click Listener", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Badge").OnLinkClick(onLinkClick)
            | Lead("Display small pieces of information like counts, statuses, or labels in compact, styled [badges](app://onboarding/concepts/widgets) with various colors and variants.")
            | new Markdown(
                """"
                The `Badge` [widget](app://onboarding/concepts/widgets) is a versatile component used to display small pieces of information, such as counts or statuses, in a compact form. It is commonly used within [Views](app://onboarding/concepts/views).
                
                ## Basic Usage
                
                Here's a simple example of a badge:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Badge("Primary")
                    """",Languages.Csharp)
                | new Box().Content(new Badge("Primary"))
            )
            | new Markdown(
                """"
                ## Variants
                
                Badges come in several variants to suit different use cases and [visual hierarchies](app://onboarding/concepts/theming).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Badge("Primary")
    | new Badge("Destructive", variant:BadgeVariant.Destructive)
    | new Badge("Outline", variant:BadgeVariant.Outline)
    | new Badge("Secondary", variant:BadgeVariant.Secondary)
    | new Badge("Success", variant:BadgeVariant.Success)
    | new Badge("Warning", variant:BadgeVariant.Warning)
    | new Badge("Info", variant:BadgeVariant.Info))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Badge("Primary")
                        | new Badge("Destructive", variant:BadgeVariant.Destructive)
                        | new Badge("Outline", variant:BadgeVariant.Outline)
                        | new Badge("Secondary", variant:BadgeVariant.Secondary)
                        | new Badge("Success", variant:BadgeVariant.Success)
                        | new Badge("Warning", variant:BadgeVariant.Warning)
                        | new Badge("Info", variant:BadgeVariant.Info)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Using Extension Methods
                
                You can also use extension methods for cleaner code:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Badge("Primary")
    | new Badge("Destructive").Destructive()
    | new Badge("Outline").Outline()
    | new Badge("Secondary").Secondary())),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Badge("Primary")
                        | new Badge("Destructive").Destructive()
                        | new Badge("Outline").Outline()
                        | new Badge("Secondary").Secondary()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Icons
                
                `Badge`s can include icons to enhance their visual appearance and meaning. See [Icon](app://widgets/primitives/icon) for more details. Use [Align](app://api-reference/ivy/align) for icon position (e.g. `Align.Right`).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.P("Icons on the Left").Large()
    | (Layout.Horizontal().Gap(4)
        | new Badge("Notification", icon:Icons.Bell)
        | new Badge("Success", icon:Icons.Check).Secondary()
        | new Badge("Error", icon:Icons.X).Destructive())
    | Text.P("Icons on the Right").Large()
    | (Layout.Horizontal().Gap(4)
        | new Badge("Download").Icon(Icons.Download, Align.Right)
        | new Badge("Next").Icon(Icons.ChevronRight, Align.Right).Secondary())
    | Text.P("Icon-Only").Large()
    | (Layout.Horizontal().Gap(4)
        | new Badge(null, icon:Icons.Bell)
        | new Badge(null, icon:Icons.X, variant:BadgeVariant.Destructive)))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.P("Icons on the Left").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Badge("Notification", icon:Icons.Bell)
                            | new Badge("Success", icon:Icons.Check).Secondary()
                            | new Badge("Error", icon:Icons.X).Destructive())
                        | Text.P("Icons on the Right").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Badge("Download").Icon(Icons.Download, Align.Right)
                            | new Badge("Next").Icon(Icons.ChevronRight, Align.Right).Secondary())
                        | Text.P("Icon-Only").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Badge(null, icon:Icons.Bell)
                            | new Badge(null, icon:Icons.X, variant:BadgeVariant.Destructive))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Click Listener
                
                Badges can be made clickable using the `OnClick` extension method. This is useful for filter chips, tag management, and toggle states.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Badge("Click Me", icon:Icons.MousePointer)
                        .OnClick(_ => client.Toast("Badge clicked!"))
                    """",Languages.Csharp)
                | new Box().Content(new Badge("Click Me", icon:Icons.MousePointer)
    .OnClick(_ => client.Toast("Badge clicked!")))
            )
            | new WidgetDocsView("Ivy.Badge", "Ivy.BadgeExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Badge.cs")
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.P("Status").Large()
    | (Layout.Horizontal().Gap(4)
        | new Badge("Online", icon:Icons.Circle, variant:BadgeVariant.Secondary)
        | new Badge("Offline", icon:Icons.Circle, variant:BadgeVariant.Destructive))
    | Text.P("Counters").Large()
    | (Layout.Horizontal().Gap(4)
        | new Badge("4").Large()
        | new Badge("12", icon:Icons.Mail).Large())
    | Text.P("Tags").Large()
    | (Layout.Horizontal().Gap(4)
        | new Badge("Design", icon:Icons.Palette)
        | new Badge("Development", icon:Icons.Code)))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.P("Status").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Badge("Online", icon:Icons.Circle, variant:BadgeVariant.Secondary)
                            | new Badge("Offline", icon:Icons.Circle, variant:BadgeVariant.Destructive))
                        | Text.P("Counters").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Badge("4").Large()
                            | new Badge("12", icon:Icons.Mail).Large())
                        | Text.P("Tags").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Badge("Design", icon:Icons.Palette)
                            | new Badge("Development", icon:Icons.Code))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Expandable("What are the available BadgeVariant values in Ivy?",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    The `BadgeVariant` enum has these values: `Primary`, `Destructive`, `Outline`, `Secondary`, `Success`, `Warning`, `Info`.
                    
                    Usage:
                    """").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    // Via constructor:
                    new Badge("Status", BadgeVariant.Success)
                    
                    // Via fluent Variant() method:
                    new Badge("Status").Variant(BadgeVariant.Warning)
                    
                    // Via shortcut extension methods:
                    new Badge("Status").Success()
                    new Badge("Status").Destructive()
                    new Badge("Status").Info()
                    """",Languages.Csharp)
                | new Markdown("There is no `BadgeVariant.Default`. Use `BadgeVariant.Primary` or omit the variant for the default appearance.").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.ThemingApp), typeof(Widgets.Primitives.IconApp), typeof(ApiReference.Ivy.AlignApp)]; 
        return article;
    }
}

