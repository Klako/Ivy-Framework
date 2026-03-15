using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Layouts;

[App(order:8, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/02_Layouts/08_ResizablePanelGroup.md", searchHints: ["split", "resizable", "panels", "divider", "adjustable", "layout"])]
public class ResizablePanelGroupApp(bool onlyBody = false) : ViewBase
{
    public ResizablePanelGroupApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("resizablepanelgroup", "ResizablePanelGroup", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("orientation", "Orientation", 2), new ArticleHeading("horizontal-layout-default", "Horizontal Layout (Default)", 3), new ArticleHeading("vertical-layout", "Vertical Layout", 3), new ArticleHeading("panel-sizing", "Panel Sizing", 2), new ArticleHeading("default-sizes", "Default Sizes", 3), new ArticleHeading("auto-sizing", "Auto-Sizing", 3), new ArticleHeading("minmax-constraints", "Min/Max Constraints", 3), new ArticleHeading("handle-visibility", "Handle Visibility", 2), new ArticleHeading("showhide-resize-handles", "Show/Hide Resize Handles", 3), new ArticleHeading("nested-layouts", "Nested Layouts", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# ResizablePanelGroup").OnLinkClick(onLinkClick)
            | Lead("Create flexible, resizable [layouts](app://onboarding/concepts/layout) with draggable handles that allow users to dynamically adjust panel sizes in your applications.")
            | new Markdown(
                """"
                The `ResizablePanelGroup` [widget](app://onboarding/concepts/widgets) creates layouts with multiple panels separated by draggable handles, allowing users to resize sections interactively. Panels can be arranged horizontally or vertically and support nesting for complex layouts.
                
                ## Basic Usage
                
                The simplest resizable panel group consists of two or more panels arranged horizontally:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicResizablePanelView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new ResizablePanelGroup(
                                new ResizablePanel(Size.Fraction(0.3f),
                                    new Card("Left Panel")),
                                new ResizablePanel(Size.Fraction(0.7f),
                                    new Card("Right Panel"))
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicResizablePanelView())
            )
            | new Markdown(
                """"
                ## Orientation
                
                ### Horizontal Layout (Default)
                
                Panels are arranged side by side with vertical drag handles:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new HorizontalResizableView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class HorizontalResizableView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new ResizablePanelGroup(
                                new ResizablePanel(Size.Fraction(0.25f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Sidebar")
                                            | Text.Block("Navigation")
                                            | Text.Block("• Home")
                                            | Text.Block("• Settings")
                                    )),
                                new ResizablePanel(Size.Fraction(0.5f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Main Content")
                                            | Text.Block("This is the primary content area")
                                            | Text.Block("where the main application content")
                                            | Text.Block("would be displayed.")
                                    )),
                                new ResizablePanel(Size.Fraction(0.25f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Info Panel")
                                            | Text.Block("Additional info")
                                            | Text.Block("• Stats")
                                            | Text.Block("• Notifications")
                                    ))
                            ).Horizontal();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Vertical Layout
                
                Panels are stacked vertically with horizontal drag handles:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new VerticalResizableView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class VerticalResizableView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new ResizablePanelGroup(
                                new ResizablePanel(Size.Fraction(0.3f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Header Section")
                                            | Text.Block("Navigation and branding")
                                    )),
                                new ResizablePanel(Size.Fraction(0.4f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Main Content")
                                            | Text.Block("This is the main content area where")
                                            | Text.Block("your primary content would be displayed.")
                                            | Text.Block("It takes up the majority of the space.")
                                    )),
                                new ResizablePanel(Size.Fraction(0.3f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Footer Section")
                                            | Text.Block("Copyright and links")
                                    ))
                            ).Vertical().Height(Size.Units(150));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Panel Sizing
                
                ### Default Sizes
                
                Each panel can have a default size specified as a percentage of the total space:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DefaultSizesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DefaultSizesView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new ResizablePanelGroup(
                                new ResizablePanel(Size.Fraction(0.2f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("20% Panel")
                                            | Text.Block("Small panel")
                                    )),
                                new ResizablePanel(Size.Fraction(0.6f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("60% Panel")
                                            | Text.Block("Large main panel")
                                            | Text.Block("with more content space")
                                    )),
                                new ResizablePanel(Size.Fraction(0.2f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("20% Panel")
                                            | Text.Block("Small panel")
                                    ))
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Auto-Sizing
                
                Panels without specified sizes will automatically distribute the remaining space:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AutoSizingView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AutoSizingView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new ResizablePanelGroup(
                                new ResizablePanel(Size.Fraction(0.25f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Fixed 25%")
                                            | Text.Block("This panel has")
                                            | Text.Block("a fixed size")
                                    )),
                                new ResizablePanel(null,
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Auto Size")
                                            | Text.Block("This panel automatically")
                                            | Text.Block("sizes to available space")
                                    )),
                                new ResizablePanel(null,
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Auto Size")
                                            | Text.Block("This panel also")
                                            | Text.Block("sizes automatically")
                                    ))
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Min/Max Constraints
                
                Panels can have minimum and maximum size constraints to limit how small or large they can be resized:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MinMaxSizingView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class MinMaxSizingView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new ResizablePanelGroup(
                                new ResizablePanel(
                                    Size.Fraction(0.3f).Min(0.15f).Max(0.5f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Constrained Panel")
                                            | Text.Block("Default: 30%")
                                            | Text.Block("Min: 15%, Max: 50%")
                                            | Text.Block("Try resizing!")
                                    )),
                                new ResizablePanel(
                                    Size.Fraction(0.7f).Min(0.5f).Max(0.85f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Main Content")
                                            | Text.Block("Default: 70%")
                                            | Text.Block("Min: 50%, Max: 85%")
                                    ))
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Use `.Min()` and `.Max()` extension methods to set size constraints. Only `Size.Fraction()` is supported for resizeable panels.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Handle Visibility
                
                ### Show/Hide Resize Handles
                
                Create a workspace with multiple resizable sections for different content areas:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new HandleVisibilityView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class HandleVisibilityView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Box(Text.Block("With Handles (Default)")).Padding(2)
                                | new ResizablePanelGroup(
                                    new ResizablePanel(Size.Fraction(0.5f),
                                        new Card(
                                            Layout.Vertical()
                                                | Text.Label("Panel A")
                                                | Text.Block("Resizable panel")
                                        )),
                                    new ResizablePanel(Size.Fraction(0.5f),
                                        new Card(
                                            Layout.Vertical()
                                                | Text.Label("Panel B")
                                                | Text.Block("Resizable panel")
                                        ))
                                ).ShowHandle(true).Height(Size.Units(50))
                                | new Box(Text.Block("Without Handles")).Padding(2)
                                | new ResizablePanelGroup(
                                    new ResizablePanel(Size.Fraction(0.5f),
                                        new Card(
                                            Layout.Vertical()
                                                | Text.Label("Panel A")
                                                | Text.Block("Fixed panel")
                                        )),
                                    new ResizablePanel(Size.Fraction(0.5f),
                                        new Card(
                                            Layout.Vertical()
                                                | Text.Label("Panel B")
                                                | Text.Block("Fixed panel")
                                        ))
                                ).ShowHandle(false).Height(Size.Units(50));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Nested Layouts
                
                Create complex layouts by nesting ResizablePanelGroups:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new NestedLayoutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class NestedLayoutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new ResizablePanelGroup(
                                new ResizablePanel(Size.Fraction(0.25f),
                                    new Card(
                                        Layout.Vertical()
                                            | Text.Label("Sidebar")
                                            | Text.Block("Navigation menu")
                                            | Text.Block("• Dashboard")
                                            | Text.Block("• Reports")
                                            | Text.Block("• Settings")
                                    )),
                                new ResizablePanel(Size.Fraction(0.75f),
                                    new ResizablePanelGroup(
                                        new ResizablePanel(Size.Fraction(0.6f),
                                            new Card(
                                                Layout.Vertical()
                                                    | Text.Label("Main Content")
                                                    | Text.Block("Primary workspace area")
                                                    | Text.Block("This is where the main")
                                                    | Text.Block("application content is displayed.")
                                            )),
                                        new ResizablePanel(Size.Fraction(0.4f),
                                            new ResizablePanelGroup(
                                                new ResizablePanel(Size.Fraction(0.5f),
                                                    new Card(
                                                        Layout.Vertical()
                                                            | Text.Label("Top Right")
                                                            | Text.Block("Quick stats")
                                                            | Text.Block("or tools")
                                                    )),
                                                new ResizablePanel(Size.Fraction(0.5f),
                                                    new Card(
                                                        Layout.Vertical()
                                                            | Text.Label("Bottom Right")
                                                            | Text.Block("Additional info")
                                                            | Text.Block("or controls")
                                                    ))
                                            ).Vertical())
                                    ).Horizontal())
                            ).Horizontal();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.ResizablePanelGroup", "Ivy.ResizablePanelsExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Layouts/ResizablePanelGroup.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Multi-Directional Resizing",
                Vertical().Gap(4)
                | new Markdown("Demonstrate both horizontal and vertical resizing in a complex nested layout:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new MultiDirectionalResizingView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class MultiDirectionalResizingView : ViewBase
                        {
                            public override object? Build()
                            {
                                return new ResizablePanelGroup(
                                    // Left panel - File browser
                                    new ResizablePanel(Size.Fraction(0.25f),
                                        new Card(
                                            Layout.Vertical()
                                                | Text.Label("File Browser")
                                                | Text.Block("App.cs")
                                                | Text.Block("Layout.cs")
                                                | Text.Block("Components.cs")
                                                | Text.Block("Utils.cs")
                                                | Text.Block("Assets/")
                                                | Text.Block("Tests/")
                                        ).Title("Files")
                                    ),
                                    // Center area - Split editor and console
                                    new ResizablePanel(Size.Fraction(0.5f),
                                        new ResizablePanelGroup(
                                            // Top - Code editor
                                            new ResizablePanel(Size.Fraction(0.7f),
                                                new Card(
                                                    Layout.Vertical()
                                                        | Text.Label("Code Editor")
                                                        | Text.Code("public class ResizableExample\n{\n    public void DemoResize()\n    {\n        // Drag handles to resize!\n        Console.WriteLine(\"Resizing works!\");\n    }\n}", Languages.Csharp)
                                                ).Title("main.cs")
                                            ),
                                            // Bottom - Console/Output
                                            new ResizablePanel(Size.Fraction(0.7f),
                                                new Card(
                                                    Layout.Vertical()
                                                        | Text.Label("Console Output")
                                                        | Text.Block("> dotnet run")
                                                        | Text.Block("Building...")
                                                        | Text.Block("Build succeeded")
                                                        | Text.Block("Application started")
                                                        | Text.Block("Resizing works!")
                                                ).Title("Output")
                                            )
                                        ).Vertical().Height(Size.Units(190))),
                                    // Right panel - Properties and tools
                                    new ResizablePanel(Size.Fraction(0.25f),
                                        new Card(
                                            Layout.Vertical()
                                                | Text.Label("Properties")
                                                | Text.Block("Selected: ResizablePanel")
                                                | Text.Block("Default Size: 25%")
                                                | Text.Block("Direction: Horizontal")
                                                | Text.Block("Show Handle: true")
                                                | Text.Block("")
                                                | Text.Label("Actions")
                                                | Text.Block("• Resize horizontally ↔")
                                                | Text.Block("• Resize vertically ↕")
                                                | Text.Block("• Nested resizing ⤡")
                                        ).Title("Inspector")
                                    )
                                ).Height(Size.Units(200));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.LayoutApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class BasicResizablePanelView : ViewBase
{
    public override object? Build()
    {
        return new ResizablePanelGroup(
            new ResizablePanel(Size.Fraction(0.3f), 
                new Card("Left Panel")),
            new ResizablePanel(Size.Fraction(0.7f), 
                new Card("Right Panel"))
        );
    }
}

public class HorizontalResizableView : ViewBase
{
    public override object? Build()
    {
        return new ResizablePanelGroup(
            new ResizablePanel(Size.Fraction(0.25f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Sidebar")
                        | Text.Block("Navigation")
                        | Text.Block("• Home")
                        | Text.Block("• Settings")
                )),
            new ResizablePanel(Size.Fraction(0.5f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Main Content")
                        | Text.Block("This is the primary content area")
                        | Text.Block("where the main application content")
                        | Text.Block("would be displayed.")
                )),
            new ResizablePanel(Size.Fraction(0.25f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Info Panel")
                        | Text.Block("Additional info")
                        | Text.Block("• Stats")
                        | Text.Block("• Notifications")
                ))
        ).Horizontal();
    }
}

public class VerticalResizableView : ViewBase
{
    public override object? Build()
    {
        return new ResizablePanelGroup(
            new ResizablePanel(Size.Fraction(0.3f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Header Section")
                        | Text.Block("Navigation and branding")
                )),
            new ResizablePanel(Size.Fraction(0.4f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Main Content")
                        | Text.Block("This is the main content area where")
                        | Text.Block("your primary content would be displayed.")
                        | Text.Block("It takes up the majority of the space.")
                )),
            new ResizablePanel(Size.Fraction(0.3f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Footer Section")
                        | Text.Block("Copyright and links")
                ))
        ).Vertical().Height(Size.Units(150));
    }
}

public class DefaultSizesView : ViewBase
{
    public override object? Build()
    {
        return new ResizablePanelGroup(
            new ResizablePanel(Size.Fraction(0.2f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("20% Panel")
                        | Text.Block("Small panel")
                )),
            new ResizablePanel(Size.Fraction(0.6f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("60% Panel")
                        | Text.Block("Large main panel")
                        | Text.Block("with more content space")
                )),
            new ResizablePanel(Size.Fraction(0.2f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("20% Panel")
                        | Text.Block("Small panel")
                ))
        );
    }
}

public class AutoSizingView : ViewBase
{
    public override object? Build()
    {
        return new ResizablePanelGroup(
            new ResizablePanel(Size.Fraction(0.25f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Fixed 25%")
                        | Text.Block("This panel has")
                        | Text.Block("a fixed size")
                )),
            new ResizablePanel(null, 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Auto Size")
                        | Text.Block("This panel automatically")
                        | Text.Block("sizes to available space")
                )),
            new ResizablePanel(null, 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Auto Size")
                        | Text.Block("This panel also")
                        | Text.Block("sizes automatically")
                ))
        );
    }
}

public class MinMaxSizingView : ViewBase
{
    public override object? Build()
    {
        return new ResizablePanelGroup(
            new ResizablePanel(
                Size.Fraction(0.3f).Min(0.15f).Max(0.5f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Constrained Panel")
                        | Text.Block("Default: 30%")
                        | Text.Block("Min: 15%, Max: 50%")
                        | Text.Block("Try resizing!")
                )),
            new ResizablePanel(
                Size.Fraction(0.7f).Min(0.5f).Max(0.85f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Main Content")
                        | Text.Block("Default: 70%")
                        | Text.Block("Min: 50%, Max: 85%")
                ))
        );
    }
}

public class HandleVisibilityView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Box(Text.Block("With Handles (Default)")).Padding(2)
            | new ResizablePanelGroup(
                new ResizablePanel(Size.Fraction(0.5f), 
                    new Card(
                        Layout.Vertical()
                            | Text.Label("Panel A")
                            | Text.Block("Resizable panel")
                    )),
                new ResizablePanel(Size.Fraction(0.5f), 
                    new Card(
                        Layout.Vertical()
                            | Text.Label("Panel B")
                            | Text.Block("Resizable panel")
                    ))
            ).ShowHandle(true).Height(Size.Units(50))
            | new Box(Text.Block("Without Handles")).Padding(2)
            | new ResizablePanelGroup(
                new ResizablePanel(Size.Fraction(0.5f), 
                    new Card(
                        Layout.Vertical()
                            | Text.Label("Panel A")
                            | Text.Block("Fixed panel")
                    )),
                new ResizablePanel(Size.Fraction(0.5f), 
                    new Card(
                        Layout.Vertical()
                            | Text.Label("Panel B")
                            | Text.Block("Fixed panel")
                    ))
            ).ShowHandle(false).Height(Size.Units(50));
    }
}

public class NestedLayoutView : ViewBase
{
    public override object? Build()
    {
        return new ResizablePanelGroup(
            new ResizablePanel(Size.Fraction(0.25f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("Sidebar")
                        | Text.Block("Navigation menu")
                        | Text.Block("• Dashboard")
                        | Text.Block("• Reports")
                        | Text.Block("• Settings")
                )),
            new ResizablePanel(Size.Fraction(0.75f),
                new ResizablePanelGroup(
                    new ResizablePanel(Size.Fraction(0.6f), 
                        new Card(
                            Layout.Vertical()
                                | Text.Label("Main Content")
                                | Text.Block("Primary workspace area")
                                | Text.Block("This is where the main")
                                | Text.Block("application content is displayed.")
                        )),
                    new ResizablePanel(Size.Fraction(0.4f),
                        new ResizablePanelGroup(
                            new ResizablePanel(Size.Fraction(0.5f), 
                                new Card(
                                    Layout.Vertical()
                                        | Text.Label("Top Right")
                                        | Text.Block("Quick stats")
                                        | Text.Block("or tools")
                                )),
                            new ResizablePanel(Size.Fraction(0.5f), 
                                new Card(
                                    Layout.Vertical()
                                        | Text.Label("Bottom Right")
                                        | Text.Block("Additional info")
                                        | Text.Block("or controls")
                                ))
                        ).Vertical())
                ).Horizontal())
        ).Horizontal();
    }
}

public class MultiDirectionalResizingView : ViewBase
{
    public override object? Build()
    {
        return new ResizablePanelGroup(
            // Left panel - File browser
            new ResizablePanel(Size.Fraction(0.25f), 
                new Card(
                    Layout.Vertical()
                        | Text.Label("File Browser")
                        | Text.Block("App.cs")
                        | Text.Block("Layout.cs")
                        | Text.Block("Components.cs")
                        | Text.Block("Utils.cs")
                        | Text.Block("Assets/")
                        | Text.Block("Tests/")
                ).Title("Files")
            ),
            // Center area - Split editor and console
            new ResizablePanel(Size.Fraction(0.5f),
                new ResizablePanelGroup(
                    // Top - Code editor
                    new ResizablePanel(Size.Fraction(0.7f), 
                        new Card(
                            Layout.Vertical()
                                | Text.Label("Code Editor")
                                | Text.Code("public class ResizableExample\n{\n    public void DemoResize()\n    {\n        // Drag handles to resize!\n        Console.WriteLine(\"Resizing works!\");\n    }\n}", Languages.Csharp)
                        ).Title("main.cs")
                    ),
                    // Bottom - Console/Output
                    new ResizablePanel(Size.Fraction(0.7f),
                        new Card(
                            Layout.Vertical()
                                | Text.Label("Console Output")
                                | Text.Block("> dotnet run")
                                | Text.Block("Building...")
                                | Text.Block("Build succeeded")
                                | Text.Block("Application started")
                                | Text.Block("Resizing works!")
                        ).Title("Output")
                    )
                ).Vertical().Height(Size.Units(190))),
            // Right panel - Properties and tools
            new ResizablePanel(Size.Fraction(0.25f),
                new Card(
                    Layout.Vertical()
                        | Text.Label("Properties")
                        | Text.Block("Selected: ResizablePanel")
                        | Text.Block("Default Size: 25%")
                        | Text.Block("Direction: Horizontal")
                        | Text.Block("Show Handle: true")
                        | Text.Block("")
                        | Text.Label("Actions")
                        | Text.Block("• Resize horizontally ↔")
                        | Text.Block("• Resize vertically ↕")
                        | Text.Block("• Nested resizing ⤡")
                ).Title("Inspector")
            )
        ).Height(Size.Units(200));
    }
}
