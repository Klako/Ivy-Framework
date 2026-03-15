using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:6, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/06_Spacer.md", searchHints: ["spacing", "gap", "margin", "padding", "layout", "whitespace"])]
public class SpacerApp(bool onlyBody = false) : ViewBase
{
    public SpacerApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("spacer", "Spacer", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("flexible-spacing", "Flexible Spacing", 3), new ArticleHeading("header-layout-with-spacing", "Header Layout with Spacing", 3), new ArticleHeading("height-based-spacing", "Height-Based Spacing", 3), new ArticleHeading("form-layout-with-spacing", "Form Layout with Spacing", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Spacer").OnLinkClick(onLinkClick)
            | Lead("Add precise spacing between layout elements for fine-tuned control over alignment and visual balance in your [interfaces](app://onboarding/concepts/views).")
            | new Markdown(
                """"
                The `Spacer` [widget](app://onboarding/concepts/widgets) creates empty space between elements in your layout. By default, it grows to fill available space in the parent layout's direction, making it easy to push elements apart. It's useful for fine-tuning spacing and alignment.
                
                ## Basic Usage
                
                Create simple spacing between elements:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicSpacerView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicSpacerView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical()
                                | new Card("Spacer after First element")
                                | new Spacer()
                                | new Card("Second Element with no Spacer")
                                | new Card("Third Element");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Flexible Spacing
                
                A bare `Spacer` grows to fill available space by default, automatically pushing elements apart:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FlexibleSpacerView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FlexibleSpacerView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Horizontal().Gap(4)
                                | new Button("Left Button").Variant(ButtonVariant.Outline)
                                | new Spacer()
                                | new Button("Right Button").Variant(ButtonVariant.Primary);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("The `Spacer` defaults to grow behavior (`flex-grow: 1`), making it take up all available space in the parent layout's direction. This effectively pushes sibling elements to opposite sides. See [Size](app://api-reference/ivy/size) for other sizing options like explicit widths or heights.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Header Layout with Spacing
                
                Create navigation headers with proper spacing:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new HeaderSpacerView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class HeaderSpacerView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                    
                            var header = new Card(
                                Layout.Horizontal().Gap(3)
                                    | new Button("Home").Variant(ButtonVariant.Ghost)
                                    | new Button("About").Variant(ButtonVariant.Ghost)
                                    | new Button("Contact").Variant(ButtonVariant.Ghost)
                                    | new Spacer().Width(Size.Units(60))
                                    | new Button("Login").Variant(ButtonVariant.Outline)
                                    | new Button("Sign Up").Variant(ButtonVariant.Primary)
                            );
                    
                            var content = Layout.Vertical().Gap(4)
                                | new Card("Welcome to our application")
                                | new Card("This demonstrates how Spacer creates balanced layouts")
                                | new Card("Elements are properly distributed across the available space");
                    
                            return Layout.Vertical().Gap(4)
                                | header
                                | content;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Height-Based Spacing
                
                Add vertical spacing with specific heights:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new HeightSpacerView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class HeightSpacerView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(2)
                                | new Card("Top Section")
                                | new Spacer().Height(Size.Units(2))
                                | new Card("Middle Section")
                                | new Spacer().Height(Size.Units(10))
                                | new Card("Bottom Section");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("When using `Spacer().Height()` or `Spacer().Width()`, the values represent units in the Ivy Framework's spacing system, not pixels. The framework automatically converts these units to appropriate spacing based on the current [theme](app://onboarding/concepts/theming) and design system.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Form Layout with Spacing
                
                Organize form elements with consistent spacing:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormSpacerView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FormSpacerView : ViewBase
                    {
                        public override object? Build()
                        {
                            var name = UseState("");
                            var email = UseState("");
                            var message = UseState("");
                    
                            return Layout.Vertical().Gap(3)
                                | new Card(
                                    Layout.Vertical().Gap(3)
                                        | Text.Label("Contact Form")
                                        | new Separator()
                                        | Text.Label("Name:")
                                        | name.ToTextInput().Placeholder("Enter your name")
                                        | new Spacer().Height(Size.Units(4))
                                        | Text.Label("Email:")
                                        | email.ToTextInput().Placeholder("Enter your email")
                                        | new Spacer().Height(Size.Units(4))
                                        | Text.Label("Message:")
                                        | message.ToTextareaInput().Placeholder("Enter your message")
                                        | new Spacer().Height(Size.Units(10))
                                        | (Layout.Horizontal().Gap(3)
                                            | new Button("Cancel").Variant(ButtonVariant.Outline)
                                            | new Button("Submit").Variant(ButtonVariant.Primary))
                                );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Spacer", "Ivy.SpacerExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Spacer.cs")
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("Dashboard Grid with Spacing",
                Vertical().Gap(4)
                | new Markdown("Create responsive dashboard layouts with proper spacing:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new DashboardSpacerView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class DashboardSpacerView : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                        
                                var statsRow = Layout.Horizontal().Gap(4)
                                    | new Card(
                                        Layout.Vertical().Gap(2)
                                            | Text.P("Total Users").Small()
                                            | Text.Label("12.3K").Color(Colors.Primary)
                                    )
                                    | new Spacer()
                                    | new Card(
                                        Layout.Vertical().Gap(2)
                                            | Text.P("Revenue").Small()
                                            | Text.Label("$54K").Color(Colors.Green)
                                    )
                                    | new Spacer()
                                    | new Card(
                                        Layout.Vertical().Gap(2)
                                            | Text.P("Growth").Small()
                                            | Text.Label("+23%").Color(Colors.Amber)
                                    );
                        
                                var actionBar = Layout.Horizontal().Gap(3)
                                    | new Button("Export Data").Icon(Icons.Download).Variant(ButtonVariant.Outline)
                                    | new Spacer()
                                    | new Button("Refresh").Icon(Icons.RefreshCw).Variant(ButtonVariant.Ghost)
                                    | new Button("Settings").Icon(Icons.Settings).Variant(ButtonVariant.Ghost);
                        
                                return Layout.Vertical().Gap(4)
                                    | statsRow
                                    | new Spacer().Height(Size.Units(2))
                                    | actionBar
                                    | new Card("Main Content Area").Height(Size.Units(50));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("How do I create a horizontal layout with items spaced between?",
                Vertical().Gap(4)
                | new Markdown("Instead, use a `Spacer` with `Size.Grow()` to push items apart:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    Layout.Horizontal().Align(Align.Center)
                        | Text.H1("Title")
                        | new Spacer().Width(Size.Grow())
                        | new Button("Action", handler)
                    """",Languages.Csharp)
                | new Markdown("The `Spacer` takes up all remaining space, pushing elements before it to the left and elements after it to the right. You can also use `.Right()` on the layout to align all children to the right:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    Layout.Horizontal().Right()
                        | new Button("Right-aligned", handler)
                    """",Languages.Csharp)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.SizeApp), typeof(Onboarding.Concepts.ThemingApp)]; 
        return article;
    }
}


public class BasicSpacerView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical()
            | new Card("Spacer after First element")
            | new Spacer()
            | new Card("Second Element with no Spacer")
            | new Card("Third Element");
    }
}

public class FlexibleSpacerView : ViewBase
{
    public override object? Build()
    {
        return Layout.Horizontal().Gap(4)
            | new Button("Left Button").Variant(ButtonVariant.Outline)
            | new Spacer()
            | new Button("Right Button").Variant(ButtonVariant.Primary);
    }
}

public class HeaderSpacerView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var header = new Card(
            Layout.Horizontal().Gap(3)
                | new Button("Home").Variant(ButtonVariant.Ghost)
                | new Button("About").Variant(ButtonVariant.Ghost)
                | new Button("Contact").Variant(ButtonVariant.Ghost)
                | new Spacer().Width(Size.Units(60))
                | new Button("Login").Variant(ButtonVariant.Outline)
                | new Button("Sign Up").Variant(ButtonVariant.Primary)
        );
        
        var content = Layout.Vertical().Gap(4)
            | new Card("Welcome to our application")
            | new Card("This demonstrates how Spacer creates balanced layouts")
            | new Card("Elements are properly distributed across the available space");
            
        return Layout.Vertical().Gap(4)
            | header
            | content;
    }
}

public class HeightSpacerView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(2)
            | new Card("Top Section")
            | new Spacer().Height(Size.Units(2))
            | new Card("Middle Section")
            | new Spacer().Height(Size.Units(10))
            | new Card("Bottom Section");
    }
}

public class FormSpacerView : ViewBase
{
    public override object? Build()
    {
        var name = UseState("");
        var email = UseState("");
        var message = UseState("");
        
        return Layout.Vertical().Gap(3)
            | new Card(
                Layout.Vertical().Gap(3)
                    | Text.Label("Contact Form")
                    | new Separator()
                    | Text.Label("Name:")
                    | name.ToTextInput().Placeholder("Enter your name")
                    | new Spacer().Height(Size.Units(4))
                    | Text.Label("Email:")
                    | email.ToTextInput().Placeholder("Enter your email")
                    | new Spacer().Height(Size.Units(4))
                    | Text.Label("Message:")
                    | message.ToTextareaInput().Placeholder("Enter your message")
                    | new Spacer().Height(Size.Units(10))
                    | (Layout.Horizontal().Gap(3)
                        | new Button("Cancel").Variant(ButtonVariant.Outline)
                        | new Button("Submit").Variant(ButtonVariant.Primary))
            );
    }
}

public class DashboardSpacerView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var statsRow = Layout.Horizontal().Gap(4)
            | new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Total Users").Small()
                    | Text.Label("12.3K").Color(Colors.Primary)
            )
            | new Spacer()
            | new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Revenue").Small()
                    | Text.Label("$54K").Color(Colors.Green)
            )
            | new Spacer()
            | new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Growth").Small()
                    | Text.Label("+23%").Color(Colors.Amber)
            );
            
        var actionBar = Layout.Horizontal().Gap(3)
            | new Button("Export Data").Icon(Icons.Download).Variant(ButtonVariant.Outline)
            | new Spacer()
            | new Button("Refresh").Icon(Icons.RefreshCw).Variant(ButtonVariant.Ghost)
            | new Button("Settings").Icon(Icons.Settings).Variant(ButtonVariant.Ghost);
            
        return Layout.Vertical().Gap(4)
            | statsRow
            | new Spacer().Height(Size.Units(2))
            | actionBar
            | new Card("Main Content Area").Height(Size.Units(50));
    }
}
