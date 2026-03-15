using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.ApiReference.Ivy;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/04_ApiReference/Ivy/Thickness.md", searchHints: ["thickness", "padding", "margin", "spacing", "borders", "layout"])]
public class ThicknessApp(bool onlyBody = false) : ViewBase
{
    public ThicknessApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("thickness", "Thickness", 1), new ArticleHeading("base-usage", "Base usage", 2), new ArticleHeading("horizontalvertical-thickness", "Horizontal/Vertical Thickness", 3), new ArticleHeading("individual-side-thickness", "Individual Side Thickness", 3), new ArticleHeading("zero-thickness", "Zero Thickness", 3), new ArticleHeading("widget-padding", "Widget Padding", 3), new ArticleHeading("layout-margins", "Layout Margins", 2), new ArticleHeading("horizontal-layout-margins", "Horizontal Layout Margins", 3), new ArticleHeading("border-thickness", "Border Thickness", 3), new ArticleHeading("layout-views", "Layout Views", 3), new ArticleHeading("box-widget", "Box Widget", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Thickness
                
                `Thickness` is a value type that represents spacing values for the four sides of a rectangular area. It's commonly used for padding, margins, borders, and offsets in Ivy [widgets](app://onboarding/concepts/widgets) and [layouts](app://onboarding/concepts/layout). See [Size](app://api-reference/ivy/size) for width/height dimensions.
                
                ## Base usage
                
                Suggested approach for configuring thickness:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Box("Content")
    .Padding(new Thickness(10)))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Box("Content")
                        .Padding(new Thickness(10))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                You can create elements with thickness. In the basic example, there is only one parameter to assign uniform thickness.
                
                ### Horizontal/Vertical Thickness
                
                You can define thickness for horizontal (left and right) and vertical (top and bottom) sides separately using two parameters:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Box("Content")
    .Width(Size.Units(10))
    .Height(Size.Units(30))
    .Padding(new Thickness(50, 15)))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Box("Content")
                        .Width(Size.Units(10))
                        .Height(Size.Units(30))
                        .Padding(new Thickness(50, 15))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Individual Side Thickness
                
                To specify different thickness values for each side (left, top, right, bottom), use four parameters.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Box("Content")
    .Width(Size.Units(30))
    .Height(Size.Units(30))
    .Padding(new Thickness(2, 10, 6, 4)))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Box("Content")
                        .Width(Size.Units(30))
                        .Height(Size.Units(30))
                        .Padding(new Thickness(2, 10, 6, 4))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Zero Thickness
                
                Use Thickness.Zero to completely remove padding or borders by setting all sides to zero.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Box("Content")
    .Padding(Thickness.Zero))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Box("Content")
                        .Padding(Thickness.Zero)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Widget Padding
                
                This example demonstrates different padding approaches on three cards.
                
                The first card has uniform padding, the second has horizontal/vertical padding, and the third has individual side padding.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | new Box("Card 1")
        .Width(Size.Units(170))
        .Height(Size.Units(20))
        .Padding(new Thickness(10))
    | new Box("Card 2")
        .Width(Size.Units(170))
        .Height(Size.Units(20))
        .Padding(new Thickness(20, 15))
    | new Box("Card 3")
        .Width(Size.Units(170))
        .Height(Size.Units(20))
        .Padding(new Thickness(5, 5, 100, 20)))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | new Box("Card 1")
                            .Width(Size.Units(170))
                            .Height(Size.Units(20))
                            .Padding(new Thickness(10))
                        | new Box("Card 2")
                            .Width(Size.Units(170))
                            .Height(Size.Units(20))
                            .Padding(new Thickness(20, 15))
                        | new Box("Card 3")
                            .Width(Size.Units(170))
                            .Height(Size.Units(20))
                            .Padding(new Thickness(5, 5, 100, 20))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Layout Margins
                
                Margins create space around elements.
                
                They can be omitted or defined separately for horizontal and vertical spacing.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | new Box("box without margins")
        .Width(Size.Units(170))
        .Height(Size.Units(20))
| Layout.Vertical()
    .Margin(15, 5)  // Larger margins for comparison
    | new Box("Large margins (15,5)")
        .Width(Size.Units(170))
        .Height(Size.Units(20))
    | new Box("Another box without margins")
        .Width(Size.Units(170))
        .Height(Size.Units(20)))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | new Box("box without margins")
                            .Width(Size.Units(170))
                            .Height(Size.Units(20))
                    | Layout.Vertical()
                        .Margin(15, 5)  // Larger margins for comparison
                        | new Box("Large margins (15,5)")
                            .Width(Size.Units(170))
                            .Height(Size.Units(20))
                        | new Box("Another box without margins")
                            .Width(Size.Units(170))
                            .Height(Size.Units(20))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Horizontal Layout Margins
                
                In horizontal layouts, margin values can be adjusted to control spacing between elements.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    .Margin(50, 5)  // Creates Thickness(50, 5) internally
    | new Box("With margins (50, 5)").Width(Size.Units(30)).Height(Size.Units(20))
| Layout.Horizontal()
    .Margin(10, 5)
    | new Box("With margins (10, 5)").Width(Size.Units(30)).Height(Size.Units(20)))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        .Margin(50, 5)  // Creates Thickness(50, 5) internally
                        | new Box("With margins (50, 5)").Width(Size.Units(30)).Height(Size.Units(20))
                    | Layout.Horizontal()
                        .Margin(10, 5)
                        | new Box("With margins (10, 5)").Width(Size.Units(30)).Height(Size.Units(20))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Border Thickness
                
                Border thickness defines the width of the border around an element.
                
                It can be thin or thick, depending on the design needs:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Box("Thin Border")
        .Width(Size.Units(170))
        .Height(Size.Units(40))
        .BorderThickness(new Thickness(1))
    | new Box("Thick Border")
        .Width(Size.Units(170))
        .Height(Size.Units(40))
        .BorderThickness(new Thickness(10)))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Box("Thin Border")
                            .Width(Size.Units(170))
                            .Height(Size.Units(40))
                            .BorderThickness(new Thickness(1))
                        | new Box("Thick Border")
                            .Width(Size.Units(170))
                            .Height(Size.Units(40))
                            .BorderThickness(new Thickness(10))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Layout Views
                
                You can apply both padding and margin to control the internal space of a component and the space around it.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    .Padding(8)
    .Margin(4)  
    | new Box("Content"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        .Padding(8)
                        .Margin(4)
                        | new Box("Content")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Box Widget
                
                A single [Box](app://widgets/primitives/box) element can use padding, margin, and border thickness at the same time to precisely control layout and appearance.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Box("Content")
    .Padding(new Thickness(8))
    .Margin(new Thickness(4))
    .BorderThickness(new Thickness(8)))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Box("Content")
                        .Padding(new Thickness(8))
                        .Margin(new Thickness(4))
                        .BorderThickness(new Thickness(8))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.LayoutApp), typeof(ApiReference.Ivy.SizeApp), typeof(Widgets.Primitives.BoxApp)]; 
        return article;
    }
}

