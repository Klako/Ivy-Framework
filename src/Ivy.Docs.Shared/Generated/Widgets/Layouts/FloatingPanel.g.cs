using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Layouts;

[App(order:9, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/02_Layouts/09_FloatingPanel.md", searchHints: ["overlay", "modal", "popup", "floating", "dialog", "window"])]
public class FloatingPanelApp(bool onlyBody = false) : ViewBase
{
    public FloatingPanelApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("floatingpanel", "FloatingPanel", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("alignment-options", "Alignment Options", 2), new ArticleHeading("corner-positions", "Corner Positions", 3), new ArticleHeading("edge-center-positions", "Edge Center Positions", 3), new ArticleHeading("screen-center", "Screen Center", 3), new ArticleHeading("offset-positioning", "Offset Positioning", 2), new ArticleHeading("basic-offset", "Basic Offset", 3), new ArticleHeading("convenience-offset-methods", "Convenience Offset Methods", 3), new ArticleHeading("complex-content", "Complex Content", 2), new ArticleHeading("navigation-panel", "Navigation Panel", 3), new ArticleHeading("action-panel", "Action Panel", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# FloatingPanel").OnLinkClick(onLinkClick)
            | Lead("Create fixed-position [UI](app://onboarding/concepts/views) elements that remain visible and accessible regardless of scroll position, perfect for [navigation](app://onboarding/concepts/navigation) buttons, action panels, and floating controls.")
            | new Markdown(
                """"
                The `FloatingPanel` [widget](app://onboarding/concepts/widgets) positions its content at a fixed location on the screen, making it ideal for elements that should remain accessible while users scroll through content. It's commonly used for navigation buttons, action panels, and floating controls that need to stay visible.
                
                ## Basic Usage
                
                The simplest floating panel positions content in the bottom-right corner by default:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicFloatingPanelView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicFloatingPanelView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (panelView, showPanel) = UseTrigger((IState<bool> isOpen) =>
                                isOpen.Value ? new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false))) : null);
                    
                            return Layout.Vertical()
                                | new Button("Show Panel", onClick: _ => showPanel())
                                | panelView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Alignment Options
                
                The `FloatingPanel` supports nine different [Align](app://api-reference/ivy/align) positions to place content exactly where you need it:
                
                ### Corner Positions
                
                Position content in any of the four corners of the screen:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CornerAlignmentView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CornerAlignmentView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
                            {
                                var floatingButton = new Button("Action")
                                    .Icon(Icons.Star)
                                    .Large()
                                    .BorderRadius(BorderRadius.Full);
                                return Layout.Vertical()
                                    | new FloatingPanel(floatingButton, Align.TopLeft)
                                    | new FloatingPanel(floatingButton, Align.TopRight)
                                    | new FloatingPanel(floatingButton, Align.BottomLeft)
                                    | new FloatingPanel(floatingButton, Align.BottomRight)
                                    | new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(), Align.Center);
                            });
                    
                            return Layout.Vertical()
                                | new Button("Show Panels", onClick: _ => showPanels())
                                | panelView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Edge Center Positions
                
                Center content along the edges of the screen:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new EdgeCenterAlignmentView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class EdgeCenterAlignmentView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
                            {
                                var floatingButton = new Button("Center")
                                    .Icon(Icons.Move)
                                    .Large()
                                    .BorderRadius(BorderRadius.Full);
                                return Layout.Vertical()
                                    | new FloatingPanel(floatingButton, Align.TopCenter)
                                    | new FloatingPanel(floatingButton, Align.BottomCenter)
                                    | new FloatingPanel(floatingButton, Align.Left)
                                    | new FloatingPanel(floatingButton, Align.Right)
                                    | new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(), Align.Center);
                            });
                    
                            return Layout.Vertical()
                                | new Button("Show Panels", onClick: _ => showPanels())
                                | panelView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Screen Center
                
                Position content in the exact center of the screen:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CenterAlignmentView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CenterAlignmentView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (panelView, showPanel) = UseTrigger((IState<bool> isOpen) =>
                                isOpen.Value ? new FloatingPanel(
                                    new Card(
                                        Layout.Vertical()
                                            | Text.H3("Centered Panel")
                                            | Text.Block("This panel is positioned")
                                            | Text.Block("in the center of the screen")
                                            | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary()
                                    ),
                                    Align.Center) : null);
                    
                            return Layout.Vertical()
                                | new Button("Show Panel", onClick: _ => showPanel())
                                | panelView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Offset Positioning
                
                Fine-tune the position of floating panels using offset values. The `Offset` method accepts a `Thickness` object to specify precise positioning:
                
                ### Basic Offset
                
                Adjust the position from the default alignment:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicOffsetView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicOffsetView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
                            {
                                return Layout.Vertical()
                                    | new FloatingPanel(
                                        new Button("Default Position")
                                            .Icon(Icons.Circle)
                                            .Large()
                                            .BorderRadius(BorderRadius.Full),
                                        Align.TopRight)
                                    | new FloatingPanel(
                                        new Button("Offset Down & Left")
                                            .Icon(Icons.ArrowDownLeft)
                                            .Large()
                                            .BorderRadius(BorderRadius.Full),
                                        Align.BottomLeft)
                                        .Offset(new Thickness(0, 20, 0, 0))  // 20 units up from bottom edge
                                    | new FloatingPanel(
                                        new Button("Custom Offset")
                                            .Icon(Icons.Move)
                                            .Large()
                                            .BorderRadius(BorderRadius.Full),
                                        Align.BottomRight)
                                        .Offset(new Thickness(10, 0, 0, 10)) // Thickness(left, top, right, bottom): 10 from left edge, 10 from bottom edge
                                    | new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(), Align.Center);
                            });
                    
                            return Layout.Vertical()
                                | new Button("Show Panels", onClick: _ => showPanels())
                                | panelView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Convenience Offset Methods
                
                Use the convenience methods for quick positioning adjustments:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ConvenienceOffsetView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ConvenienceOffsetView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
                            {
                                return Layout.Vertical()
                                    | new FloatingPanel(
                                        new Button("Top Offset")
                                            .Icon(Icons.ArrowUp)
                                            .Large()
                                            .BorderRadius(BorderRadius.Full),
                                        Align.TopRight)
                                        .OffsetTop(30)
                                    | new FloatingPanel(
                                        new Button("Left Offset")
                                            .Icon(Icons.ArrowLeft)
                                            .Large()
                                            .BorderRadius(BorderRadius.Full),
                                        Align.TopRight)
                                        .OffsetLeft(30)
                                    | new FloatingPanel(
                                        new Button("Right Offset")
                                            .Icon(Icons.ArrowRight)
                                            .Large()
                                            .BorderRadius(BorderRadius.Full),
                                        Align.TopLeft)
                                        .OffsetRight(30)
                                    | new FloatingPanel(
                                        new Button("Bottom Offset")
                                            .Icon(Icons.ArrowDown)
                                            .Large()
                                            .BorderRadius(BorderRadius.Full),
                                        Align.BottomLeft)
                                        .OffsetBottom(30)
                                    | new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(), Align.Center);
                            });
                    
                            return Layout.Vertical()
                                | new Button("Show Panels", onClick: _ => showPanels())
                                | panelView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Complex Content
                
                Floating panels can contain complex layouts and multiple [widgets](app://onboarding/concepts/widgets):
                
                ### Navigation Panel
                
                Create a floating navigation panel with multiple buttons:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new NavigationPanelView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class NavigationPanelView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (panelView, showPanel) = UseTrigger((IState<bool> isOpen) =>
                                isOpen.Value ? new FloatingPanel(
                                    Layout.Vertical().Gap(2)
                                        | new Button("Home")
                                            .Icon(Icons.House)
                                            .Secondary()
                                            .Width(Size.Units(12))
                                        | new Button("Settings")
                                            .Icon(Icons.Settings)
                                            .Secondary()
                                            .Width(Size.Units(12))
                                        | new Button("Profile")
                                            .Icon(Icons.User)
                                            .Secondary()
                                            .Width(Size.Units(12))
                                        | new Button("Help")
                                            .Icon(Icons.Info)
                                            .Secondary()
                                            .Width(Size.Units(12))
                                        | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(),
                                    Align.Right)
                                    .Offset(new Thickness(0, 0, 10, 0)) : null);
                    
                            return Layout.Vertical()
                                | new Button("Show Panel", onClick: _ => showPanel())
                                | panelView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Action Panel
                
                A floating action panel with multiple action buttons:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ActionPanelView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ActionPanelView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (panelView, showPanel) = UseTrigger((IState<bool> isOpen) =>
                                isOpen.Value ? new FloatingPanel(
                                    Layout.Horizontal().Gap(2)
                                        | new Button("New")
                                            .Icon(Icons.Plus)
                                            .Primary()
                                            .BorderRadius(BorderRadius.Full)
                                        | new Button("Edit")
                                            .Icon(Icons.Pen)
                                            .Secondary()
                                            .BorderRadius(BorderRadius.Full)
                                        | new Button("Delete")
                                            .Icon(Icons.Trash)
                                            .Destructive()
                                            .BorderRadius(BorderRadius.Full)
                                        | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(),
                                    Align.BottomCenter)
                                    .Offset(new Thickness(0, 0, 0, 20)) : null);
                    
                            return Layout.Vertical()
                                | new Button("Show Panel", onClick: _ => showPanel())
                                | panelView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Ensure floating panels don't interfere with content readability and provide clear visual hierarchy. Use appropriate contrast and sizing for interactive elements.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.FloatingPanel", "Ivy.FloatingLayerExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Layouts/FloatingPanel.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Back to Top Button",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    A common use case for floating panels—a "back to top" button:
                    """").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new BackToTopView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class BackToTopView : ViewBase
                        {
                            public override object? Build()
                            {
                                var (panelView, showButton) = UseTrigger((IState<bool> isOpen) =>
                                    isOpen.Value ? new FloatingPanel(
                                        Layout.Horizontal().Gap(2).Align(Align.Center)
                                            | new Button("Top")
                                                .Icon(Icons.ArrowUp)
                                                .Large()
                                                .BorderRadius(BorderRadius.Full)
                                                .Secondary()
                                            | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(),
                                        Align.BottomRight)
                                        .Offset(new Thickness(0, 0, 20, 20)) : null);
                        
                                return Layout.Vertical()
                                    | new Button("Show Button", onClick: _ => showButton())
                                    | panelView;
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Floating Search Bar",
                Vertical().Gap(4)
                | new Markdown("A floating search bar that stays accessible:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new FloatingSearchView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class FloatingSearchView : ViewBase
                        {
                            public override object? Build()
                            {
                                var (panelView, showSearchBar) = UseTrigger((IState<bool> isOpen) =>
                                    isOpen.Value ? new FloatingPanel(
                                        new Card(
                                            Layout.Horizontal().Gap(2)
                                                | new TextInput(placeholder: "Search...")
                                                | new Button("Search").Icon(Icons.Search).Primary()
                                                | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary()
                                        ),
                                        Align.TopCenter)
                                        .Offset(new Thickness(0, 10, 0, 0)) : null);
                        
                                return Layout.Vertical()
                                    | new Button("Show Search Bar", onClick: _ => showSearchBar())
                                    | panelView;
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Multi-Panel Layout",
                Vertical().Gap(4)
                | new Markdown("Demonstrate multiple floating panels working together:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new MultiPanelView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class MultiPanelView : ViewBase
                        {
                            public override object? Build()
                            {
                                var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
                                {
                                    return Layout.Vertical()
                                        | new FloatingPanel(
                                            new Button("Menu")
                                                .Icon(Icons.Menu)
                                                .Large()
                                                .BorderRadius(BorderRadius.Full)
                                                .Secondary(),
                                            Align.TopLeft)
                                            .Offset(new Thickness(10, 10, 0, 0))
                                        | new FloatingPanel(
                                            new Button("Notifications")
                                                .Icon(Icons.Bell)
                                                .Large()
                                                .BorderRadius(BorderRadius.Full)
                                                .Secondary(),
                                            Align.TopRight)
                                            .Offset(new Thickness(0, 10, 10, 0))
                                        | new FloatingPanel(
                                            new Button("Chat")
                                                .Icon(Icons.MessageCircle)
                                                .Large()
                                                .BorderRadius(BorderRadius.Full)
                                                .Primary(),
                                            Align.BottomRight)
                                            .Offset(new Thickness(0, 0, 20, 20))
                                        | new FloatingPanel(
                                            new Card(
                                                Layout.Vertical()
                                                    | Text.Block("Quick Actions")
                                                    | new Button("Save").Small().Primary()
                                                    | new Button("Share").Small().Secondary()
                                                    | new Button("Close", onClick: _ => isOpen.Set(false)).Small().Secondary()
                                            ).Width(Size.Units(40)),
                                            Align.Left)
                                            .Offset(new Thickness(10, 0, 0, 0));
                                });
                        
                                return Layout.Vertical()
                                    | new Button("Show Panels", onClick: _ => showPanels())
                                    | panelView;
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.AlignApp)]; 
        return article;
    }
}


public class BasicFloatingPanelView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanel) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false))) : null);

        return Layout.Vertical()
            | new Button("Show Panel", onClick: _ => showPanel())
            | panelView;
    }
}

public class CornerAlignmentView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
        {
            var floatingButton = new Button("Action")
                .Icon(Icons.Star)
                .Large()
                .BorderRadius(BorderRadius.Full);
            return Layout.Vertical()
                | new FloatingPanel(floatingButton, Align.TopLeft)
                | new FloatingPanel(floatingButton, Align.TopRight)
                | new FloatingPanel(floatingButton, Align.BottomLeft)
                | new FloatingPanel(floatingButton, Align.BottomRight)
                | new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(), Align.Center);
        });

        return Layout.Vertical()
            | new Button("Show Panels", onClick: _ => showPanels())
            | panelView;
    }
}

public class EdgeCenterAlignmentView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
        {
            var floatingButton = new Button("Center")
                .Icon(Icons.Move)
                .Large()
                .BorderRadius(BorderRadius.Full);
            return Layout.Vertical()
                | new FloatingPanel(floatingButton, Align.TopCenter)
                | new FloatingPanel(floatingButton, Align.BottomCenter)
                | new FloatingPanel(floatingButton, Align.Left)
                | new FloatingPanel(floatingButton, Align.Right)
                | new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(), Align.Center);
        });

        return Layout.Vertical()
            | new Button("Show Panels", onClick: _ => showPanels())
            | panelView;
    }
}

public class CenterAlignmentView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanel) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new FloatingPanel(
                new Card(
                    Layout.Vertical()
                        | Text.H3("Centered Panel")
                        | Text.Block("This panel is positioned")
                        | Text.Block("in the center of the screen")
                        | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary()
                ),
                Align.Center) : null);

        return Layout.Vertical()
            | new Button("Show Panel", onClick: _ => showPanel())
            | panelView;
    }
}

public class BasicOffsetView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
        {
            return Layout.Vertical()
                | new FloatingPanel(
                    new Button("Default Position")
                        .Icon(Icons.Circle)
                        .Large()
                        .BorderRadius(BorderRadius.Full),
                    Align.TopRight)
                | new FloatingPanel(
                    new Button("Offset Down & Left")
                        .Icon(Icons.ArrowDownLeft)
                        .Large()
                        .BorderRadius(BorderRadius.Full),
                    Align.BottomLeft)
                    .Offset(new Thickness(0, 20, 0, 0))  // 20 units up from bottom edge
                | new FloatingPanel(
                    new Button("Custom Offset")
                        .Icon(Icons.Move)
                        .Large()
                        .BorderRadius(BorderRadius.Full),
                    Align.BottomRight)
                    .Offset(new Thickness(10, 0, 0, 10)) // Thickness(left, top, right, bottom): 10 from left edge, 10 from bottom edge
                | new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(), Align.Center);
        });

        return Layout.Vertical()
            | new Button("Show Panels", onClick: _ => showPanels())
            | panelView;
    }
}

public class ConvenienceOffsetView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
        {
            return Layout.Vertical()
                | new FloatingPanel(
                    new Button("Top Offset")
                        .Icon(Icons.ArrowUp)
                        .Large()
                        .BorderRadius(BorderRadius.Full),
                    Align.TopRight)
                    .OffsetTop(30)
                | new FloatingPanel(
                    new Button("Left Offset")
                        .Icon(Icons.ArrowLeft)
                        .Large()
                        .BorderRadius(BorderRadius.Full),
                    Align.TopRight)
                    .OffsetLeft(30)
                | new FloatingPanel(
                    new Button("Right Offset")
                        .Icon(Icons.ArrowRight)
                        .Large()
                        .BorderRadius(BorderRadius.Full),
                    Align.TopLeft)
                    .OffsetRight(30)
                | new FloatingPanel(
                    new Button("Bottom Offset")
                        .Icon(Icons.ArrowDown)
                        .Large()
                        .BorderRadius(BorderRadius.Full),
                    Align.BottomLeft)
                    .OffsetBottom(30)
                | new FloatingPanel(new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(), Align.Center);
        });

        return Layout.Vertical()
            | new Button("Show Panels", onClick: _ => showPanels())
            | panelView;
    }
}

public class NavigationPanelView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanel) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new FloatingPanel(
                Layout.Vertical().Gap(2)
                    | new Button("Home")
                        .Icon(Icons.House)
                        .Secondary()
                        .Width(Size.Units(12))
                    | new Button("Settings")
                        .Icon(Icons.Settings)
                        .Secondary()
                        .Width(Size.Units(12))
                    | new Button("Profile")
                        .Icon(Icons.User)
                        .Secondary()
                        .Width(Size.Units(12))
                    | new Button("Help")
                        .Icon(Icons.Info)
                        .Secondary()
                        .Width(Size.Units(12))
                    | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(),
                Align.Right)
                .Offset(new Thickness(0, 0, 10, 0)) : null);

        return Layout.Vertical()
            | new Button("Show Panel", onClick: _ => showPanel())
            | panelView;
    }
}

public class ActionPanelView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanel) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new FloatingPanel(
                Layout.Horizontal().Gap(2)
                    | new Button("New")
                        .Icon(Icons.Plus)
                        .Primary()
                        .BorderRadius(BorderRadius.Full)
                    | new Button("Edit")
                        .Icon(Icons.Pen)
                        .Secondary()
                        .BorderRadius(BorderRadius.Full)
                    | new Button("Delete")
                        .Icon(Icons.Trash)
                        .Destructive()
                        .BorderRadius(BorderRadius.Full)
                    | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(),
                Align.BottomCenter)
                .Offset(new Thickness(0, 0, 0, 20)) : null);

        return Layout.Vertical()
            | new Button("Show Panel", onClick: _ => showPanel())
            | panelView;
    }
}

public class BackToTopView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showButton) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new FloatingPanel(
                Layout.Horizontal().Gap(2).Align(Align.Center)
                    | new Button("Top")
                        .Icon(Icons.ArrowUp)
                        .Large()
                        .BorderRadius(BorderRadius.Full)
                        .Secondary()
                    | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary(),
                Align.BottomRight)
                .Offset(new Thickness(0, 0, 20, 20)) : null);

        return Layout.Vertical()
            | new Button("Show Button", onClick: _ => showButton())
            | panelView;
    }
}

public class FloatingSearchView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showSearchBar) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new FloatingPanel(
                new Card(
                    Layout.Horizontal().Gap(2)
                        | new TextInput(placeholder: "Search...")
                        | new Button("Search").Icon(Icons.Search).Primary()
                        | new Button("Close", onClick: _ => isOpen.Set(false)).Secondary()
                ),
                Align.TopCenter)
                .Offset(new Thickness(0, 10, 0, 0)) : null);

        return Layout.Vertical()
            | new Button("Show Search Bar", onClick: _ => showSearchBar())
            | panelView;
    }
}

public class MultiPanelView : ViewBase
{
    public override object? Build()
    {
        var (panelView, showPanels) = UseTrigger((IState<bool> isOpen) =>
        {
            return Layout.Vertical()
                | new FloatingPanel(
                    new Button("Menu")
                        .Icon(Icons.Menu)
                        .Large()
                        .BorderRadius(BorderRadius.Full)
                        .Secondary(),
                    Align.TopLeft)
                    .Offset(new Thickness(10, 10, 0, 0))
                | new FloatingPanel(
                    new Button("Notifications")
                        .Icon(Icons.Bell)
                        .Large()
                        .BorderRadius(BorderRadius.Full)
                        .Secondary(),
                    Align.TopRight)
                    .Offset(new Thickness(0, 10, 10, 0))
                | new FloatingPanel(
                    new Button("Chat")
                        .Icon(Icons.MessageCircle)
                        .Large()
                        .BorderRadius(BorderRadius.Full)
                        .Primary(),
                    Align.BottomRight)
                    .Offset(new Thickness(0, 0, 20, 20))
                | new FloatingPanel(
                    new Card(
                        Layout.Vertical()
                            | Text.Block("Quick Actions")
                            | new Button("Save").Small().Primary()
                            | new Button("Share").Small().Secondary()
                            | new Button("Close", onClick: _ => isOpen.Set(false)).Small().Secondary()
                    ).Width(Size.Units(40)),
                    Align.Left)
                    .Offset(new Thickness(10, 0, 0, 0));
        });

        return Layout.Vertical()
            | new Button("Show Panels", onClick: _ => showPanels())
            | panelView;
    }
}
