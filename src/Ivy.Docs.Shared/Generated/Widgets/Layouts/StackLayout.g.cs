using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Layouts;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/02_Layouts/01_StackLayout.md", searchHints: ["layout", "vertical", "horizontal", "stack", "arrangement", "flexbox"])]
public class StackLayoutApp(bool onlyBody = false) : ViewBase
{
    public StackLayoutApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("stacklayout", "StackLayout", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("alignment", "Alignment", 2), new ArticleHeading("space-distribution", "Space Distribution", 2), new ArticleHeading("row-gap--column-gap", "Row Gap & Column Gap", 2), new ArticleHeading("wrap", "Wrap", 2), new ArticleHeading("alignself", "AlignSelf", 2), new ArticleHeading("scroll", "Scroll", 2), new ArticleHeading("advanced-features", "Advanced Features", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# StackLayout").OnLinkClick(onLinkClick)
            | Lead("StackLayout arranges child elements in either a vertical or horizontal stack with configurable spacing, alignment, and styling options. It's the foundation for creating linear [layouts](app://onboarding/concepts/layout) where elements are arranged sequentially in a single direction.")
            | new Markdown(
                """"
                The `StackLayout` [widget](app://onboarding/concepts/widgets) is the core building block for most layout compositions, offering flexible configuration for orientation, gaps between elements, padding, margins, [background colors](app://api-reference/ivy/colors), and content [alignment](app://api-reference/ivy/align). It can be used to create simple stacks or as the foundation for more complex layout systems.
                
                ## Basic Usage
                
                Create simple stack using the helper methods:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicStackExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicStackExample : ViewBase
                    {
                        public override object? Build()
                        {
                            return new StackLayout([
                                Text.H2("Stack"),
                                Text.Label("Creation of a simple Stack Layout")]);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Alignment
                
                The `StackLayout` widget arranges child elements in a linear sequence with configurable orientation, spacing, alignment, and padding. This example demonstrates the core features:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StackLayoutExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class StackLayoutExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var box1 = new Box().Background(Colors.Primary).Width(Size.Units(2)).Height(Size.Units(2));
                            var box2 = new Box().Background(Colors.Primary).Width(Size.Units(2)).Height(Size.Units(2));
                            var box3 = new Box().Background(Colors.Primary).Width(Size.Units(2)).Height(Size.Units(2));
                    
                            return new StackLayout([
                                Text.H2("StackLayout Features"),
                                Text.Label("Orientation.Horizontal, gap(2), padding(1)"),
                                new StackLayout([box1, box2, box3], Orientation.Horizontal, gap: 2, padding: new Thickness(1)),
                                Text.Label("Orientation.Vertical, gap(1), Align.Center, padding(2)"),
                                new StackLayout([box1, box2, box3], Orientation.Vertical, gap: 1, align: Align.Center, padding: new Thickness(2))
                            ], gap: 4);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Space Distribution
                
                Use `SpaceBetween`, `SpaceAround`, or `SpaceEvenly` to distribute space between elements:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StackLayout([
    Text.Label("SpaceBetween:"),
    new StackLayout([new Badge("A"), new Badge("B"), new Badge("C")], Orientation.Horizontal, align: Align.SpaceBetween),
    Text.Label("SpaceAround:"),
    new StackLayout([new Badge("A"), new Badge("B"), new Badge("C")], Orientation.Horizontal, align: Align.SpaceAround),
    Text.Label("SpaceEvenly:"),
    new StackLayout([new Badge("A"), new Badge("B"), new Badge("C")], Orientation.Horizontal, align: Align.SpaceEvenly)
], gap: 4).Width(Size.Full()))),
                new Tab("Code", new CodeBlock(
                    """"
                    new StackLayout([
                        Text.Label("SpaceBetween:"),
                        new StackLayout([new Badge("A"), new Badge("B"), new Badge("C")], Orientation.Horizontal, align: Align.SpaceBetween),
                        Text.Label("SpaceAround:"),
                        new StackLayout([new Badge("A"), new Badge("B"), new Badge("C")], Orientation.Horizontal, align: Align.SpaceAround),
                        Text.Label("SpaceEvenly:"),
                        new StackLayout([new Badge("A"), new Badge("B"), new Badge("C")], Orientation.Horizontal, align: Align.SpaceEvenly)
                    ], gap: 4).Width(Size.Full())
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Row Gap & Column Gap
                
                Control vertical and horizontal spacing independently using `RowGap` and `ColumnGap` properties:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StackLayout([
    Text.Label("RowGap=8, ColumnGap=2:"),
    new StackLayout([
        new Badge("A"), new Badge("B"), new Badge("C"),
        new Badge("D"), new Badge("E"), new Badge("F"),
        new Badge("G"), new Badge("H"), new Badge("I"),
        new Badge("J"), new Badge("K"), new Badge("L"),
        new Badge("M"), new Badge("N")
    ], Orientation.Horizontal, wrap: true) { RowGap = 8, ColumnGap = 2 }
        .Width(Size.Units(60)),
    Text.Label("RowGap=2, ColumnGap=8:"),
    new StackLayout([
        new Badge("A"), new Badge("B"), new Badge("C"),
        new Badge("D"), new Badge("E"), new Badge("F"),
        new Badge("G"), new Badge("H")
    ], Orientation.Horizontal, wrap: true) { RowGap = 2, ColumnGap = 8 }
        .Width(Size.Units(60))
], gap: 4))),
                new Tab("Code", new CodeBlock(
                    """"
                    new StackLayout([
                        Text.Label("RowGap=8, ColumnGap=2:"),
                        new StackLayout([
                            new Badge("A"), new Badge("B"), new Badge("C"),
                            new Badge("D"), new Badge("E"), new Badge("F"),
                            new Badge("G"), new Badge("H"), new Badge("I"),
                            new Badge("J"), new Badge("K"), new Badge("L"),
                            new Badge("M"), new Badge("N")
                        ], Orientation.Horizontal, wrap: true) { RowGap = 8, ColumnGap = 2 }
                            .Width(Size.Units(60)),
                        Text.Label("RowGap=2, ColumnGap=8:"),
                        new StackLayout([
                            new Badge("A"), new Badge("B"), new Badge("C"),
                            new Badge("D"), new Badge("E"), new Badge("F"),
                            new Badge("G"), new Badge("H")
                        ], Orientation.Horizontal, wrap: true) { RowGap = 2, ColumnGap = 8 }
                            .Width(Size.Units(60))
                    ], gap: 4)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Wrap
                
                Use the `wrap` parameter to allow items to flow to the next line when they run out of space. Try resizing the window to see the wrapping behavior:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StackLayout([
    new Badge("Tag 1").Primary(),
    new Badge("Tag 2").Secondary(),
    new Badge("Tag 3"),
    new Badge("Tag 4").Primary(),
    new Badge("Tag 5").Secondary(),
    new Badge("Tag 6"),
    new Badge("Tag 7").Primary(),
    new Badge("Tag 8").Secondary(),
    new Badge("Tag 9"),
    new Badge("Tag 10").Primary(),
    new Badge("Tag 11").Secondary()
], Orientation.Horizontal, gap: 2, wrap: true))),
                new Tab("Code", new CodeBlock(
                    """"
                    new StackLayout([
                        new Badge("Tag 1").Primary(),
                        new Badge("Tag 2").Secondary(),
                        new Badge("Tag 3"),
                        new Badge("Tag 4").Primary(),
                        new Badge("Tag 5").Secondary(),
                        new Badge("Tag 6"),
                        new Badge("Tag 7").Primary(),
                        new Badge("Tag 8").Secondary(),
                        new Badge("Tag 9"),
                        new Badge("Tag 10").Primary(),
                        new Badge("Tag 11").Secondary()
                    ], Orientation.Horizontal, gap: 2, wrap: true)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## AlignSelf
                
                Override alignment for individual children using `.AlignSelf()`:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StackLayout([
    new Badge("Top").Primary().AlignSelf(Align.TopLeft),
    new Badge("Center").Primary().AlignSelf(Align.Center),
    new Badge("Bottom").Primary().AlignSelf(Align.BottomRight)
], gap: 4).Width(Size.Full()))),
                new Tab("Code", new CodeBlock(
                    """"
                    new StackLayout([
                        new Badge("Top").Primary().AlignSelf(Align.TopLeft),
                        new Badge("Center").Primary().AlignSelf(Align.Center),
                        new Badge("Bottom").Primary().AlignSelf(Align.BottomRight)
                    ], gap: 4).Width(Size.Full())
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Scroll
                
                Add scrollable behavior using the `Scroll` property on a height-constrained layout:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new StackLayout([
    new Badge("Item 1"), new Badge("Item 2"), new Badge("Item 3"),
    new Badge("Item 4"), new Badge("Item 5"), new Badge("Item 6"),
    new Badge("Item 7"), new Badge("Item 8"), new Badge("Item 9"),
    new Badge("Item 10"), new Badge("Item 11"), new Badge("Item 12")
], gap: 2) { Scroll = Scroll.Vertical }.Height(Size.Units(30)).Width(Size.Full()))),
                new Tab("Code", new CodeBlock(
                    """"
                    new StackLayout([
                        new Badge("Item 1"), new Badge("Item 2"), new Badge("Item 3"),
                        new Badge("Item 4"), new Badge("Item 5"), new Badge("Item 6"),
                        new Badge("Item 7"), new Badge("Item 8"), new Badge("Item 9"),
                        new Badge("Item 10"), new Badge("Item 11"), new Badge("Item 12")
                    ], gap: 2) { Scroll = Scroll.Vertical }.Height(Size.Units(30)).Width(Size.Full())
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Advanced Features
                
                Complete example showing padding, margins, background colors, and parent padding control. Use [Thickness](app://api-reference/ivy/thickness) for padding and margin, and [Colors](app://api-reference/ivy/colors) for background. Alignment options are in [Align](app://api-reference/ivy/align):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AdvancedStackLayoutExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AdvancedStackLayoutExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var box = new Box().Background(Colors.Primary).Width(Size.Units(2)).Height(Size.Units(2));
                    
                            return new StackLayout([
                                Text.H2("Advanced StackLayout Features"),
                                Text.Label("With Margin (external spacing)"),
                                new StackLayout([box, box], Orientation.Horizontal, margin: new Thickness(4)),
                                Text.Label("Remove Parent Padding, Background color"),
                                new StackLayout([box, box], Orientation.Horizontal, removeParentPadding: true, background: Colors.Gray)
                            ], gap: 2, padding: new Thickness(8));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("StackLayout is the foundation for most other layout widgets. Understanding its properties will help you master more complex layout systems.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.StackLayout", null, "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Layouts/StackLayout.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Navigation Bar",
                Vertical().Gap(4)
                | new Markdown("Create a horizontal navigation bar with proper alignment:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new NavigationExample())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class NavigationExample : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                        
                                return new StackLayout([
                                    // Navigation buttons
                                    new StackLayout([
                                        new Button("Home", _ => client.Toast("Home")),
                                        new Button("About", _ => client.Toast("About")),
                                        new Button("Contact", _ => client.Toast("Contact")),
                                        new Button("Settings", _ => client.Toast("Settings"))
                                    ], Orientation.Horizontal, gap: 8, align: Align.Center),
                        
                                     // App title and user info
                                    new StackLayout([
                                        Text.H3("MyApp"),
                                        Text.P("Welcome back!").Small()
                                    ], Orientation.Vertical, align: Align.Left),
                        
                                    // User actions
                                    new StackLayout([
                                        new Button("Profile", _ => client.Toast("Profile")),
                                        new Button("Logout", _ => client.Toast("Logout"))
                                    ], Orientation.Horizontal, gap: 4, align: Align.Right)
                        
                                ], Orientation.Vertical, gap: 16, padding: new Thickness(12), align: Align.Center);
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.LayoutApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.ColorsApp), typeof(ApiReference.Ivy.AlignApp), typeof(ApiReference.Ivy.ThicknessApp)]; 
        return article;
    }
}


public class BasicStackExample : ViewBase
{
    public override object? Build()
    {   
        return new StackLayout([
            Text.H2("Stack"), 
            Text.Label("Creation of a simple Stack Layout")]);
    }
}

public class StackLayoutExample : ViewBase
{
    public override object? Build()
    {
        var box1 = new Box().Background(Colors.Primary).Width(Size.Units(2)).Height(Size.Units(2));
        var box2 = new Box().Background(Colors.Primary).Width(Size.Units(2)).Height(Size.Units(2));
        var box3 = new Box().Background(Colors.Primary).Width(Size.Units(2)).Height(Size.Units(2));
        
        return new StackLayout([
            Text.H2("StackLayout Features"),
            Text.Label("Orientation.Horizontal, gap(2), padding(1)"),
            new StackLayout([box1, box2, box3], Orientation.Horizontal, gap: 2, padding: new Thickness(1)),
            Text.Label("Orientation.Vertical, gap(1), Align.Center, padding(2)"),
            new StackLayout([box1, box2, box3], Orientation.Vertical, gap: 1, align: Align.Center, padding: new Thickness(2))
        ], gap: 4);
    }
}

public class AdvancedStackLayoutExample : ViewBase
{
    public override object? Build()
    {
        var box = new Box().Background(Colors.Primary).Width(Size.Units(2)).Height(Size.Units(2));
        
        return new StackLayout([
            Text.H2("Advanced StackLayout Features"),
            Text.Label("With Margin (external spacing)"),
            new StackLayout([box, box], Orientation.Horizontal, margin: new Thickness(4)),
            Text.Label("Remove Parent Padding, Background color"),
            new StackLayout([box, box], Orientation.Horizontal, removeParentPadding: true, background: Colors.Gray)
        ], gap: 2, padding: new Thickness(8));
    }
}

public class NavigationExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        return new StackLayout([
            // Navigation buttons
            new StackLayout([
                new Button("Home", _ => client.Toast("Home")),
                new Button("About", _ => client.Toast("About")),
                new Button("Contact", _ => client.Toast("Contact")),
                new Button("Settings", _ => client.Toast("Settings"))
            ], Orientation.Horizontal, gap: 8, align: Align.Center),

             // App title and user info
            new StackLayout([
                Text.H3("MyApp"),
                Text.P("Welcome back!").Small()
            ], Orientation.Vertical, align: Align.Left),
            
            // User actions
            new StackLayout([
                new Button("Profile", _ => client.Toast("Profile")),
                new Button("Logout", _ => client.Toast("Logout"))
            ], Orientation.Horizontal, gap: 4, align: Align.Right)
            
        ], Orientation.Vertical, gap: 16, padding: new Thickness(12), align: Align.Center);
    }
}
