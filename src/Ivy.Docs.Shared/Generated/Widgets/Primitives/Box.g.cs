using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/04_Box.md", searchHints: ["container", "div", "wrapper", "rectangle", "styling", "layout"])]
public class BoxApp(bool onlyBody = false) : ViewBase
{
    public BoxApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("box", "Box", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("border-styling", "Border Styling", 3), new ArticleHeading("border-thickness", "Border Thickness", 3), new ArticleHeading("border-radius", "Border Radius", 3), new ArticleHeading("basic-spacing", "Basic Spacing", 3), new ArticleHeading("advanced-features--interactions", "Advanced Features & Interactions", 3), new ArticleHeading("interactive-states", "Interactive States", 3), new ArticleHeading("colors", "Colors", 3), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Box").OnLinkClick(onLinkClick)
            | Lead("Create versatile container elements with customizable borders, colors, and padding for grouping content and structuring [layouts](app://onboarding/concepts/layout).")
            | new Markdown(
                """"
                The `Box` [widget](app://onboarding/concepts/widgets) is a versatile container element that provides customizable borders, colors, padding, margins, and content alignment. It's perfect for visually grouping related content, creating distinct sections in your [UI](app://onboarding/concepts/views), and building [card](app://widgets/common/card)-based layouts.
                
                ## Basic Usage
                
                The simplest way to create a Box is by passing content directly to the constructor.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicBoxExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicBoxExample : ViewBase
                    {
                        public override object? Build()
                        {
                            return new Box("Simple content");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Box widgets come with sensible defaults: Primary color, 2-unit borders with rounded corners, 2-unit padding, centered content, and no margin.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Border Styling
                
                Boxes support various border styles, thicknesses, and radius options.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BorderStyleExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BorderStyleExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Box("Solid Border").BorderStyle(BorderStyle.Solid).Padding(8)
                                | new Box("Dashed Border").BorderStyle(BorderStyle.Dashed).Padding(8)
                                | new Box("Dotted Border").BorderStyle(BorderStyle.Dotted).Padding(8)
                                | new Box("No Border").BorderStyle(BorderStyle.None).Padding(8);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Border Thickness
                
                Control the width of borders using the `BorderThickness` property. You can specify a single value for uniform thickness or use the [Thickness](app://api-reference/ivy/thickness) class for more precise control.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BorderThicknessExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BorderThicknessExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Horizontal().Gap(4)
                                | new Box("Thin Border")
                                    .BorderThickness(1)
                                    .Padding(8)
                                    .Width(Size.Fraction(1/3f))
                                | new Box("Medium Border")
                                    .BorderThickness(2)
                                    .Padding(8)
                                    .Width(Size.Fraction(1/3f))
                                | new Box("Thick Border")
                                    .BorderThickness(4)
                                    .Padding(8)
                                    .Width(Size.Fraction(1/3f));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Border Radius
                
                Choose from different border radius options to create rounded corners. This affects the visual style and can range from sharp edges to fully rounded corners.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BorderRadiusExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BorderRadiusExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Horizontal().Gap(4)
                                | new Box("No Radius")
                                    .BorderRadius(BorderRadius.None)
                                    .Padding(8)
                                    .Width(Size.Fraction(1/3f))
                                | new Box("Rounded")
                                    .BorderRadius(BorderRadius.Rounded)
                                    .Padding(8)
                                    .Width(Size.Fraction(1/3f))
                                | new Box("Full Radius")
                                    .BorderRadius(BorderRadius.Full)
                                    .Padding(8)
                                    .Width(Size.Fraction(1/3f));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Basic Spacing
                
                Control internal and external spacing using padding and margins.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SpacingExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SpacingExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Box("No Padding").Padding(0)
                                | new Box("Small Padding").Padding(4)
                                | new Box("Large Padding").Padding(10)
                                | new Box("With Margin").Margin(8).Padding(8);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Advanced Features & Interactions
                
                Use the [Thickness](app://api-reference/ivy/thickness) class for more precise control over padding on different sides. This allows you to specify different spacing values for left, top, right, and bottom edges.
                
                Boxes also support a wide range of [Colors](app://api-reference/ivy/colors), and interactive states using `OnClick` and `Hover()`.
                
                The following example demonstrates combining these features to create status indicators, interactive selections, and professional card layouts.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BoxFeaturesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BoxFeaturesView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var selected = UseState("Option A");
                    
                            return Layout.Vertical().Gap(8)
                                | Layout.Grid().Columns(2).Gap(8)
                                    // Advanced Spacing & Colors
                                    | new Box("Asymmetric Padding & Primary Color")
                                        .Padding(new Thickness(24, 12, 6, 18))
                                        .Width(Size.Fraction(1/2f));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Interactive States
                
                Boxes support hover effects and click events.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new InteractiveBoxView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class InteractiveBoxView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var selected = UseState("Option A");
                    
                            return Layout.Vertical().Gap(8)
                                | new Box("Pointer + Translate Hover")
                                    .Hover(CardHoverVariant.PointerAndTranslate)
                                    .OnClick(_ => client.Toast("Box clicked!"))
                                    .Padding(8)
                                | Layout.Horizontal().Gap(4)
                                    | CreateOption("Option A", selected, client)
                                    | CreateOption("Option B", selected, client);
                        }
                    
                        private Box CreateOption(string label, IState<string> selected, IClientProvider client)
                        {
                            var isSelected = selected.Value == label;
                            return new Box(label)
                                .Background(isSelected ? Colors.Primary : Colors.Muted)
                                .BorderThickness(isSelected ? 2 : 1)
                                .Hover(CardHoverVariant.Pointer)
                                .OnClick(_ => {
                                    selected.Set(label);
                                    client.Toast($"Selected: {label}");
                                })
                                .Padding(8)
                                .Width(Size.Fraction(1/2f));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Colors
                
                Boxes support a wide range of predefined colors that automatically adapt to light/dark themes.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ColorExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ColorExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Box("Primary Color").Background(Colors.Primary).Padding(8);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                For more colors, see the [Colors](app://api-reference/ivy/colors) reference.
                
                ## Faq
                """").OnLinkClick(onLinkClick)
            | new Expandable("Status Dashboard",
                Vertical().Gap(4)
                | new Markdown("Create a dashboard with status indicators using different colors and styles. This example demonstrates how to use boxes for displaying system status information with appropriate visual cues.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new StatusDashboardView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class StatusDashboardView : ViewBase
                        {
                            public override object? Build()
                            {
                                return Layout.Horizontal().Gap(4)
                                    | new Box("System Online")
                                        .Background(Colors.Green)
                                        .BorderRadius(BorderRadius.Rounded)
                                        .BorderThickness(2)
                                        .Padding(8)
                                        .Width(Size.Fraction(1/3f))
                                    | new Box("Warning: High CPU Usage")
                                        .Background(Colors.Yellow)
                                        .BorderStyle(BorderStyle.Dashed)
                                        .BorderThickness(2)
                                        .Padding(8)
                                        .Width(Size.Fraction(1/3f))
                                    | new Box("Database Error")
                                        .Background(Colors.Red)
                                        .BorderThickness(2)
                                        .Padding(8)
                                        .Width(Size.Fraction(1/3f));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Card Layout",
                Vertical().Gap(4)
                | new Markdown("Build card-based layouts with consistent styling for displaying structured information. This example shows how to create professional-looking cards with proper spacing, borders, and content organization.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new CardLayoutView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class CardLayoutView : ViewBase
                        {
                            public override object? Build()
                            {
                                return Layout.Grid().Columns(2).Gap(8)
                                    | new Box()
                                        .Background(Colors.White)
                                        .BorderRadius(BorderRadius.Rounded)
                                        .BorderThickness(1)
                                        .Padding(12)
                                        .Content(
                                            Text.Label("User Profile"),
                                            Text.P("John Doe"),
                                            Text.P("Software Developer")
                                        );
                            }
                        
                            private Box CreateOption(string label, IState<string> selected, IClientProvider client)
                            {
                                var isSelected = selected.Value == label;
                                return new Box(label)
                                    .Background(isSelected ? Colors.Primary : Colors.Muted)
                                    .BorderThickness(isSelected ? 2 : 1)
                                    .Hover(CardHoverVariant.Pointer)
                                    .OnClick(_ => {
                                        selected.Set(label);
                                        client.Toast($"Selected: {label}");
                                    })
                                    .Padding(8)
                                    .Width(Size.Fraction(1/2f));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("How do I create a circular shape or circle in Ivy?",
                Vertical().Gap(4)
                | new Markdown("There is no dedicated Shape or Circle widget. Use a `Box` with `BorderRadius.Full` and equal width and height to create a circle:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    // A colored circle
                    new Box()
                        .Background(Colors.Primary)
                        .Width(Size.Px(36))
                        .Height(Size.Px(36))
                        .BorderRadius(BorderRadius.Full)
                    
                    // A circle with content centered inside
                    new Box(Text.P("A"))
                        .Color(Colors.Slate)
                        .Width(Size.Px(48))
                        .Height(Size.Px(48))
                        .ContentAlign(Align.Center)
                        .BorderRadius(BorderRadius.Full)
                    """",Languages.Csharp)
                | new Markdown("`BorderRadius.Full` makes the box fully rounded. When width and height are equal, this produces a perfect circle. Use `BorderRadius.Rounded` for rounded corners instead.").OnLinkClick(onLinkClick)
            )
            | new Expandable("How do I apply styling (width, height, color, padding) to Ivy components?",
                Vertical().Gap(4)
                | new Markdown("Ivy uses a fluent API for styling — there is no `.Style()` method for arbitrary CSS. Use the built-in extension methods:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    new Box(content)
                        .Width(Size.Px(200))
                        .Height(Size.Px(100))
                        .Background(Colors.Blue)
                        .Padding(16)
                        .Margin(8)
                        .BorderRadius(BorderRadius.Rounded)
                        .BorderStyle(BorderStyle.Solid)
                    """",Languages.Csharp)
                | new Markdown("For CSS transforms, rotations, or complex visual effects that can't be expressed with Ivy's styling API, use the `Html` widget with inline styles:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    new Html($"<div style='transform: rotate({degrees}deg); width: 100px; height: 2px; background: #000;'></div>")
                        .DangerouslyAllowScripts()
                    """",Languages.Csharp)
                | new Markdown("Note: The `Html` widget renders in an iframe. CSS variables like `var(--primary)` do not resolve — use hardcoded color values.").OnLinkClick(onLinkClick)
            )
            | new WidgetDocsView("Ivy.Box", "Ivy.BoxExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Box.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.LayoutApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Widgets.Common.CardApp), typeof(ApiReference.Ivy.ThicknessApp), typeof(ApiReference.Ivy.ColorsApp)]; 
        return article;
    }
}


public class BasicBoxExample : ViewBase
{
    public override object? Build()
    {
        return new Box("Simple content");
    }
}

public class BorderStyleExamplesView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Box("Solid Border").BorderStyle(BorderStyle.Solid).Padding(8)
            | new Box("Dashed Border").BorderStyle(BorderStyle.Dashed).Padding(8)
            | new Box("Dotted Border").BorderStyle(BorderStyle.Dotted).Padding(8)
            | new Box("No Border").BorderStyle(BorderStyle.None).Padding(8);
    }
}

public class BorderThicknessExamplesView : ViewBase
{
    public override object? Build()
    {
        return Layout.Horizontal().Gap(4)
            | new Box("Thin Border")
                .BorderThickness(1)
                .Padding(8)
                .Width(Size.Fraction(1/3f))
            | new Box("Medium Border")
                .BorderThickness(2)
                .Padding(8)
                .Width(Size.Fraction(1/3f))
            | new Box("Thick Border")
                .BorderThickness(4)
                .Padding(8)
                .Width(Size.Fraction(1/3f));
    }
}

public class BorderRadiusExamplesView : ViewBase
{
    public override object? Build()
    {
        return Layout.Horizontal().Gap(4)
            | new Box("No Radius")
                .BorderRadius(BorderRadius.None)
                .Padding(8)
                .Width(Size.Fraction(1/3f))
            | new Box("Rounded")
                .BorderRadius(BorderRadius.Rounded)
                .Padding(8)
                .Width(Size.Fraction(1/3f))
            | new Box("Full Radius")
                .BorderRadius(BorderRadius.Full)
                .Padding(8)
                .Width(Size.Fraction(1/3f));
    }
}

public class SpacingExamplesView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Box("No Padding").Padding(0)
            | new Box("Small Padding").Padding(4)
            | new Box("Large Padding").Padding(10)
            | new Box("With Margin").Margin(8).Padding(8);
    }
}

public class BoxFeaturesView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var selected = UseState("Option A");

        return Layout.Vertical().Gap(8)
            | Layout.Grid().Columns(2).Gap(8)
                // Advanced Spacing & Colors
                | new Box("Asymmetric Padding & Primary Color")
                    .Padding(new Thickness(24, 12, 6, 18))
                    .Width(Size.Fraction(1/2f));
    }
}

public class InteractiveBoxView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var selected = UseState("Option A");

        return Layout.Vertical().Gap(8)
            | new Box("Pointer + Translate Hover")
                .Hover(CardHoverVariant.PointerAndTranslate)
                .OnClick(_ => client.Toast("Box clicked!"))
                .Padding(8)
            | Layout.Horizontal().Gap(4)
                | CreateOption("Option A", selected, client)
                | CreateOption("Option B", selected, client);
    }

    private Box CreateOption(string label, IState<string> selected, IClientProvider client)
    {
        var isSelected = selected.Value == label;
        return new Box(label)
            .Background(isSelected ? Colors.Primary : Colors.Muted)
            .BorderThickness(isSelected ? 2 : 1)
            .Hover(CardHoverVariant.Pointer)
            .OnClick(_ => {
                selected.Set(label);
                client.Toast($"Selected: {label}");
            })
            .Padding(8)
            .Width(Size.Fraction(1/2f));
    }
}

public class ColorExamplesView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Box("Primary Color").Background(Colors.Primary).Padding(8);
    }
}

public class StatusDashboardView : ViewBase
{
    public override object? Build()
    {
        return Layout.Horizontal().Gap(4)
            | new Box("System Online")
                .Background(Colors.Green)
                .BorderRadius(BorderRadius.Rounded)
                .BorderThickness(2)
                .Padding(8)
                .Width(Size.Fraction(1/3f))
            | new Box("Warning: High CPU Usage")
                .Background(Colors.Yellow)
                .BorderStyle(BorderStyle.Dashed)
                .BorderThickness(2)
                .Padding(8)
                .Width(Size.Fraction(1/3f))
            | new Box("Database Error")
                .Background(Colors.Red)
                .BorderThickness(2)
                .Padding(8)
                .Width(Size.Fraction(1/3f));
    }
}

public class CardLayoutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Grid().Columns(2).Gap(8)
            | new Box()
                .Background(Colors.White)
                .BorderRadius(BorderRadius.Rounded)
                .BorderThickness(1)
                .Padding(12)
                .Content(
                    Text.Label("User Profile"),
                    Text.P("John Doe"),
                    Text.P("Software Developer")
                );
    }

    private Box CreateOption(string label, IState<string> selected, IClientProvider client)
    {
        var isSelected = selected.Value == label;
        return new Box(label)
            .Background(isSelected ? Colors.Primary : Colors.Muted)
            .BorderThickness(isSelected ? 2 : 1)
            .Hover(CardHoverVariant.Pointer)
            .OnClick(_ => {
                selected.Set(label);
                client.Toast($"Selected: {label}");
            })
            .Padding(8)
            .Width(Size.Fraction(1/2f));
    }
}
