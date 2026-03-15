using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/04_Layout.md", searchHints: ["layout", "horizontal", "vertical", "center", "wrap", "grid", "tabs", "fluent", "composition", "row", "column"])]
public class LayoutApp(bool onlyBody = false) : ViewBase
{
    public LayoutApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("layout", "Layout", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("pipe-operator-syntax", "Pipe Operator Syntax", 2), new ArticleHeading("configuration-methods", "Configuration Methods", 2), new ArticleHeading("gap", "Gap", 3), new ArticleHeading("independent-row--column-gap", "Independent Row & Column Gap", 3), new ArticleHeading("padding-and-margin", "Padding and Margin", 3), new ArticleHeading("width-and-height", "Width and Height", 3), new ArticleHeading("alignment", "Alignment", 3), new ArticleHeading("space-distribution", "Space Distribution", 3), new ArticleHeading("wrap", "Wrap", 3), new ArticleHeading("alignself", "AlignSelf", 3), new ArticleHeading("scroll", "Scroll", 3), new ArticleHeading("combining-with-other-layouts", "Combining with Other Layouts", 2), new ArticleHeading("extension-methods", "Extension Methods", 2), new ArticleHeading("available-methods", "Available Methods", 2), new ArticleHeading("available-layouts", "Available Layouts", 2), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("does-ivy-have-row-and-column-widgets", "Does Ivy have Row and Column widgets?", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Layout").OnLinkClick(onLinkClick)
            | Lead("The Layout static class provides a fluent API for creating common layout compositions with minimal code, serving as the primary entry point for building [UI structures](app://onboarding/concepts/views) in Ivy.")
            | new Markdown(
                """"
                The Layout class offers convenient factory methods that return pre-configured layout [views](app://onboarding/concepts/views). Instead of manually instantiating layout [widgets](app://onboarding/concepts/widgets), you can use Layout.Vertical(), Layout.Horizontal(), and other methods to quickly compose your UI with a clean, fluent syntax.
                
                ## Basic Usage
                
                Create simple layouts using the static helper methods.
                
                Vertical layout arranges elements from top to bottom:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    Layout.Vertical()
                        | new Badge("Top")
                        | new Badge("Middle")
                        | new Badge("Bottom")
                    """",Languages.Csharp)
                | new Box().Content(Layout.Vertical()
    | new Badge("Top")
    | new Badge("Middle")
    | new Badge("Bottom"))
            )
            | new Markdown("Horizontal layout arranges elements from left to right:").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Badge("Left")
                        | new Badge("Center")
                        | new Badge("Right")
                    """",Languages.Csharp)
                | new Box().Content(Layout.Horizontal()
    | new Badge("Left")
    | new Badge("Center")
    | new Badge("Right"))
            )
            | new Markdown(
                """"
                ## Pipe Operator Syntax
                
                The Layout class supports the pipe operator `|` for adding children, enabling a clean and readable composition syntax:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.Label("User Profile")
    | (Layout.Horizontal().Gap(2)
        | new Badge("Active").Primary()
        | new Badge("Premium").Secondary())
    | Text.P("Choose your plan").Small())),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.Label("User Profile")
                        | (Layout.Horizontal().Gap(2)
                            | new Badge("Active").Primary()
                            | new Badge("Premium").Secondary())
                        | Text.P("Choose your plan").Small()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Configuration Methods
                
                All layout methods return a LayoutView that can be further configured:
                
                ### Gap
                
                Control spacing between elements with `.Gap()`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | Text.Label("No Gap:")
    | (Layout.Horizontal().Gap(0)
        | new Badge("A") | new Badge("B") | new Badge("C"))
    | Text.Label("With Gap:")
    | (Layout.Horizontal().Gap(8)
        | new Badge("A") | new Badge("B") | new Badge("C")))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | Text.Label("No Gap:")
                        | (Layout.Horizontal().Gap(0)
                            | new Badge("A") | new Badge("B") | new Badge("C"))
                        | Text.Label("With Gap:")
                        | (Layout.Horizontal().Gap(8)
                            | new Badge("A") | new Badge("B") | new Badge("C"))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Independent Row & Column Gap
                
                Use `.Gap(rowGap, columnGap)` to control vertical and horizontal spacing independently:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.Label("RowGap=8, ColumnGap=2:")
    | (Layout.Wrap().Gap(8, 2).Width(Size.Units(60))
        | new Badge("A") | new Badge("B") | new Badge("C")
        | new Badge("D") | new Badge("E") | new Badge("F")
        | new Badge("G") | new Badge("H") | new Badge("I")
        | new Badge("J") | new Badge("K") | new Badge("L")
        | new Badge("M") | new Badge("N"))
    | Text.Label("RowGap=2, ColumnGap=8:")
    | (Layout.Wrap().Gap(2, 8).Width(Size.Units(60))
        | new Badge("A") | new Badge("B") | new Badge("C")
        | new Badge("D") | new Badge("E") | new Badge("F")
        | new Badge("G") | new Badge("H")))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.Label("RowGap=8, ColumnGap=2:")
                        | (Layout.Wrap().Gap(8, 2).Width(Size.Units(60))
                            | new Badge("A") | new Badge("B") | new Badge("C")
                            | new Badge("D") | new Badge("E") | new Badge("F")
                            | new Badge("G") | new Badge("H") | new Badge("I")
                            | new Badge("J") | new Badge("K") | new Badge("L")
                            | new Badge("M") | new Badge("N"))
                        | Text.Label("RowGap=2, ColumnGap=8:")
                        | (Layout.Wrap().Gap(2, 8).Width(Size.Units(60))
                            | new Badge("A") | new Badge("B") | new Badge("C")
                            | new Badge("D") | new Badge("E") | new Badge("F")
                            | new Badge("G") | new Badge("H"))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Padding and Margin
                
                Add internal and external spacing:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Padding(4).Background(Colors.Muted)
    | Text.Label("This layout has padding and background")
    | new Badge("Example"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Padding(4).Background(Colors.Muted)
                        | Text.Label("This layout has padding and background")
                        | new Badge("Example")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Width and Height
                
                Control layout dimensions:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal().Gap(4)
    | (Layout.Vertical().Width(Size.Units(50)).Height(Size.Units(20)).Background(Colors.Muted).Center()
        | Text.Label("50 units wide"))
    | (Layout.Vertical().Width(Size.Units(30)).Height(Size.Units(20)).Background(Colors.Muted).Center()
        | Text.Label("30 units")))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal().Gap(4)
                        | (Layout.Vertical().Width(Size.Units(50)).Height(Size.Units(20)).Background(Colors.Muted).Center()
                            | Text.Label("50 units wide"))
                        | (Layout.Vertical().Width(Size.Units(30)).Height(Size.Units(20)).Background(Colors.Muted).Center()
                            | Text.Label("30 units"))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Alignment
                
                Align content within the layout:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | (Layout.Horizontal().Left()
        | new Badge("Left aligned"))
    | (Layout.Horizontal().Center()
        | new Badge("Center aligned"))
    | (Layout.Horizontal().Right()
        | new Badge("Right aligned")))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | (Layout.Horizontal().Left()
                            | new Badge("Left aligned"))
                        | (Layout.Horizontal().Center()
                            | new Badge("Center aligned"))
                        | (Layout.Horizontal().Right()
                            | new Badge("Right aligned"))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Space Distribution
                
                Distribute space between elements using `SpaceBetween`, `SpaceAround`, or `SpaceEvenly`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.Label("SpaceBetween — items pushed to edges:")
    | (Layout.Horizontal().Align(Align.SpaceBetween).Width(Size.Full())
        | new Badge("A") | new Badge("B") | new Badge("C"))
    | Text.Label("SpaceAround — equal space around each item:")
    | (Layout.Horizontal().Align(Align.SpaceAround).Width(Size.Full())
        | new Badge("A") | new Badge("B") | new Badge("C"))
    | Text.Label("SpaceEvenly — equal space between all items:")
    | (Layout.Horizontal().Align(Align.SpaceEvenly).Width(Size.Full())
        | new Badge("A") | new Badge("B") | new Badge("C")))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.Label("SpaceBetween — items pushed to edges:")
                        | (Layout.Horizontal().Align(Align.SpaceBetween).Width(Size.Full())
                            | new Badge("A") | new Badge("B") | new Badge("C"))
                        | Text.Label("SpaceAround — equal space around each item:")
                        | (Layout.Horizontal().Align(Align.SpaceAround).Width(Size.Full())
                            | new Badge("A") | new Badge("B") | new Badge("C"))
                        | Text.Label("SpaceEvenly — equal space between all items:")
                        | (Layout.Horizontal().Align(Align.SpaceEvenly).Width(Size.Full())
                            | new Badge("A") | new Badge("B") | new Badge("C"))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Wrap
                
                Use `Layout.Wrap()` to create a layout where items flow and wrap to the next line when they run out of space. Try resizing the window to see the wrapping behavior:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Wrap().Gap(2)
    | new Badge("Tag 1").Primary()
    | new Badge("Tag 2").Secondary()
    | new Badge("Tag 3")
    | new Badge("Tag 4").Primary()
    | new Badge("Tag 5").Secondary()
    | new Badge("Tag 6")
    | new Badge("Tag 7").Primary()
    | new Badge("Tag 8").Secondary()
    | new Badge("Tag 9")
    | new Badge("Tag 10").Primary()
    | new Badge("Tag 11").Secondary())),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Wrap().Gap(2)
                        | new Badge("Tag 1").Primary()
                        | new Badge("Tag 2").Secondary()
                        | new Badge("Tag 3")
                        | new Badge("Tag 4").Primary()
                        | new Badge("Tag 5").Secondary()
                        | new Badge("Tag 6")
                        | new Badge("Tag 7").Primary()
                        | new Badge("Tag 8").Secondary()
                        | new Badge("Tag 9")
                        | new Badge("Tag 10").Primary()
                        | new Badge("Tag 11").Secondary()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### AlignSelf
                
                Override alignment for individual children using `.AlignSelf()`. In a horizontal layout, this controls vertical positioning of each child independently:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | new Badge("Top").Primary().AlignSelf(Align.TopLeft)
    | new Badge("Center").Primary().AlignSelf(Align.Center)
    | new Badge("Bottom").Primary().AlignSelf(Align.BottomRight))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | new Badge("Top").Primary().AlignSelf(Align.TopLeft)
                        | new Badge("Center").Primary().AlignSelf(Align.Center)
                        | new Badge("Bottom").Primary().AlignSelf(Align.BottomRight)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Scroll
                
                Add scrollable behavior to layouts with constrained height using `.Scroll()`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Height(Size.Units(30)).Scroll(Scroll.Vertical).Gap(2)
    | new Badge("Item 1") | new Badge("Item 2") | new Badge("Item 3")
    | new Badge("Item 4") | new Badge("Item 5") | new Badge("Item 6")
    | new Badge("Item 7") | new Badge("Item 8") | new Badge("Item 9")
    | new Badge("Item 10") | new Badge("Item 11") | new Badge("Item 12"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Height(Size.Units(30)).Scroll(Scroll.Vertical).Gap(2)
                        | new Badge("Item 1") | new Badge("Item 2") | new Badge("Item 3")
                        | new Badge("Item 4") | new Badge("Item 5") | new Badge("Item 6")
                        | new Badge("Item 7") | new Badge("Item 8") | new Badge("Item 9")
                        | new Badge("Item 10") | new Badge("Item 11") | new Badge("Item 12")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Combining with Other Layouts
                
                The Layout methods integrate seamlessly with specialized layout [widgets](app://widgets/layouts/_index) and [Card](app://widgets/common/card):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.Label("Dashboard")
    | (Layout.Grid().Columns(2).Gap(4)
        | new Card("Sales").Title("$12,450")
        | new Card("Users").Title("1,234")
        | new Card("Orders").Title("89")
        | new Card("Revenue").Title("$45,000")))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.Label("Dashboard")
                        | (Layout.Grid().Columns(2).Gap(4)
                            | new Card("Sales").Title("$12,450")
                            | new Card("Users").Title("1,234")
                            | new Card("Orders").Title("89")
                            | new Card("Revenue").Title("$45,000"))
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Extension Methods
                
                The LayoutExtensions class provides additional helper methods:
                
                | Extension | Description |
                |-----------|-------------|
                | .WithMargin(int) | Wraps any object in a layout with margin |
                | .WithMargin(int, int) | Wraps with horizontal and vertical margin |
                | .WithMargin(int, int, int, int) | Wraps with left, top, right, bottom margin |
                | .WithLayout() | Wraps any object in a vertical layout |
                
                ## Available Methods
                
                The Layout class provides the following factory methods:
                
                | Method | Description |
                |--------|-------------|
                | Layout.Vertical() | Creates a vertical stack layout |
                | Layout.Horizontal() | Creates a horizontal stack layout |
                | Layout.Center() | Creates a centered layout with removed parent padding |
                | Layout.TopCenter() | Creates a top-center aligned layout |
                | Layout.Wrap() | Creates a wrapping stack layout for flowing content |
                | Layout.Grid() | Creates a grid layout for two-dimensional arrangements |
                | Layout.Tabs() | Creates a tabbed layout |
                
                ## Available Layouts
                
                | Layout | Description |
                |--------|-------------|
                | [StackLayout](app://widgets/layouts/stack-layout) | Arranges elements vertically or horizontally in a linear sequence (supports wrapping) |
                | [GridLayout](app://widgets/layouts/grid-layout) | Two-dimensional grid system with precise control over positioning and spanning |
                | [TabsLayout](app://widgets/layouts/tabs-layout) | Organizes content into tabbed sections for easy navigation |
                | [SidebarLayout](app://widgets/layouts/sidebar-layout) | Main content area with a collapsible sidebar for navigation |
                | [HeaderLayout](app://widgets/layouts/header-layout) | Page layout with a fixed header section |
                | [FooterLayout](app://widgets/layouts/footer-layout) | Page layout with a fixed footer section |
                | [FloatingPanel](app://widgets/layouts/floating-panel) | Overlay panels that float above the main content |
                | [ResizablePanelGroup](app://widgets/layouts/resizable-panel-group) | Split panels that users can resize by dragging |
                
                ## Faq
                
                ### Does Ivy have Row and Column widgets?
                
                No. Ivy uses `Layout.Horizontal()` for horizontal layouts (similar to Row) and `Layout.Vertical()` for vertical layouts (similar to Column). You can also use `new StackLayout([...], Orientation.Horizontal)` for explicit orientation control.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Horizontal layout (like "Row")
                Layout.Horizontal([widget1, widget2, widget3]);
                
                // Vertical layout (like "Column")
                Layout.Vertical([widget1, widget2, widget3]);
                """",Languages.Csharp)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Layouts._IndexApp), typeof(Widgets.Common.CardApp), typeof(Widgets.Layouts.StackLayoutApp), typeof(Widgets.Layouts.GridLayoutApp), typeof(Widgets.Layouts.TabsLayoutApp), typeof(Widgets.Layouts.SidebarLayoutApp), typeof(Widgets.Layouts.HeaderLayoutApp), typeof(Widgets.Layouts.FooterLayoutApp), typeof(Widgets.Layouts.FloatingPanelApp), typeof(Widgets.Layouts.ResizablePanelGroupApp)]; 
        return article;
    }
}

