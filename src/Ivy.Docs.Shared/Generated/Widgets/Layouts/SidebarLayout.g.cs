using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Layouts;

[App(order:6, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/02_Layouts/06_SidebarLayout.md", searchHints: ["navigation", "menu", "drawer", "side-panel", "layout", "aside", "resizable", "drag-to-resize"])]
public class SidebarLayoutApp(bool onlyBody = false) : ViewBase
{
    public SidebarLayoutApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("sidebarlayout", "SidebarLayout", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("with-header-and-footer", "With Header and Footer", 3), new ArticleHeading("sidebarmenu-integration", "SidebarMenu Integration", 3), new ArticleHeading("main-app-sidebar", "Main App Sidebar", 3), new ArticleHeading("resizable-sidebar", "Resizable Sidebar", 3), new ArticleHeading("sidebarmenu-widget", "SidebarMenu Widget", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# SidebarLayout").OnLinkClick(onLinkClick)
            | Lead("SidebarLayout provides a flexible sidebar [navigation](app://onboarding/concepts/navigation) layout with a main content area and collapsible sidebar. It supports header and footer sections, responsive behavior, and can be configured as the main application sidebar with toggle functionality.")
            | new Markdown(
                """"
                Sidebars are essential navigation [components](app://onboarding/concepts/widgets) in modern applications, providing users with quick access to different sections while keeping the main content area uncluttered. They can be used for primary navigation, contextual tools, or supplementary information display.
                
                ## Basic Usage
                
                The `SidebarLayout` creates a layout with a sidebar and main content area. The sidebar can optionally include header and footer sections:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicSidebarExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicSidebarExample : ViewBase
                    {
                        public override object? Build()
                        {
                            return new SidebarLayout(
                                mainContent: new Card(
                                    "This is the main content area. It takes up the remaining space after the sidebar."
                                ).Title("Main Content"),
                                sidebarContent: new Card(
                                    "This is the sidebar content where you can put navigation, menus, or other controls."
                                ).Title("Sidebar")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### With Header and Footer
                
                You can add optional header and footer sections to the sidebar:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SidebarWithHeaderFooterExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SidebarWithHeaderFooterExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            return new SidebarLayout(
                                mainContent: new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Welcome to the App").Large()
                                        | Text.P("This is the main content area with a sidebar navigation.")
                                        | new Button("Action Button")
                                            .Variant(ButtonVariant.Primary)
                                            .OnClick(_ => client.Toast("Action performed!"))
                                ).Title("Dashboard"),
                                sidebarContent: Layout.Vertical().Gap(2)
                                    | new Button("Home").Variant(ButtonVariant.Ghost)
                                    | new Button("Profile").Variant(ButtonVariant.Ghost)
                                    | new Button("Settings").Variant(ButtonVariant.Ghost)
                                    | new Button("Help").Variant(ButtonVariant.Ghost),
                                sidebarHeader: Layout.Vertical().Gap(1)
                                    | Text.Lead("Navigation"),
                                sidebarFooter: Layout.Vertical().Gap(1)
                                    | Text.P("Version 1.0.0").Small().Color(Colors.Gray)
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### SidebarMenu Integration
                
                The `SidebarMenu` [widget](app://onboarding/concepts/widgets) is designed to work seamlessly with `SidebarLayout` for navigation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SidebarMenuExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SidebarMenuExample : ViewBase
                    {
                        private enum NavPage { Home, Analytics, Profile, Account, Preferences, Help }
                    
                        public override object? Build()
                        {
                            var selectedPage = UseState<NavPage?>(NavPage.Home);
                            var client = UseService<IClientProvider>();
                    
                            MenuItem[] menuItems = new[]
                            {
                                MenuItem.Default("Dashboard")
                                    .Icon(Icons.LayoutDashboard).Tag(NavPage.Home),
                                MenuItem.Default("Analytics")
                                    .Icon(Icons.TrendingUp).Tag(NavPage.Analytics),
                                MenuItem.Default("Settings")
                                    .Icon(Icons.Settings).Children(
                                    MenuItem.Default("Profile")
                                        .Icon(Icons.User).Tag(NavPage.Profile),
                                    MenuItem.Default("Account")
                                        .Icon(Icons.UserCog).Tag(NavPage.Account),
                                    MenuItem.Default("Preferences")
                                        .Icon(Icons.Settings).Tag(NavPage.Preferences)
                                ),
                                MenuItem.Default("Help").Icon(Icons.CircleQuestionMark).Tag(NavPage.Help)
                            };
                    
                            var sidebarMenu = new SidebarMenu(
                                onSelect: evt => {
                                    if (Enum.TryParse<NavPage>(evt.Value?.ToString(), ignoreCase: true, out var page))
                                    {
                                        selectedPage.Set(page);
                                        client.Toast($"Navigated to {page}");
                                    }
                                },
                                items: menuItems
                            );
                    
                            object RenderContent()
                            {
                                return (selectedPage.Value ?? NavPage.Home) switch
                                {
                                    NavPage.Home => new Card("Welcome to the Dashboard!")
                                        .Title("Dashboard"),
                                    NavPage.Analytics => new Card("Analytics and reports go here.")
                                        .Title("Analytics"),
                                    NavPage.Profile => new Card("User profile settings.")
                                        .Title("Profile"),
                                    NavPage.Account => new Card("Account management.")
                                        .Title("Account"),
                                    NavPage.Preferences => new Card("User preferences.")
                                        .Title("Preferences"),
                                    NavPage.Help => new Card("Help and documentation.")
                                        .Title("Help & Support"),
                                    _ => new Card("Page not found.")
                                        .Title("404")
                                };
                            }
                    
                            return new SidebarLayout(
                                mainContent: RenderContent(),
                                sidebarContent: sidebarMenu,
                                sidebarHeader: Layout.Vertical().Gap(2)
                                    | Text.Lead("My App")
                                    | new TextInput(placeholder: "Search...", variant: TextInputVariant.Search),
                                sidebarFooter: Layout.Horizontal().Gap(2)
                                    | new Avatar("JD").Size(Size.Units(20))
                                    | (Layout.Vertical()
                                        | Text.P("John Doe").Small()
                                        | Text.P("john@example.com").Small().Color(Colors.Gray))
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Main App Sidebar
                
                When used as the main application sidebar, you can enable toggle mechanics and responsive behavior using the `.MainAppSidebar()` and `.Open()` APIs.
                
                - `.MainAppSidebar(bool isMainApp = true)`: Configures the layout as the primary application menu. This automatically adds a collapse/expand toggle button for the user and enables responsive behavior on smaller screens.
                - `.Open(bool open = true)`: Controls whether the layout starts in an expanded or collapsed state. By default, this is set to `true`.
                
                When utilizing `ChromeSettings` to define the main application chrome, you can also inject the initialization state through `ChromeSettings.SidebarOpen(false)`.
                """").OnLinkClick(onLinkClick)
            | new Callout(
                """"
                "You can combine `.MainAppSidebar(true)` and `.Open(false)` to construct a toggleable sidebar that specifically starts fully collapsed."
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MainAppSidebarExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class MainAppSidebarExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                    
                            return new SidebarLayout(
                                mainContent: Layout.Vertical().Gap(4)
                                    | new Card(
                                        Layout.Vertical().Gap(2)
                                            | Text.P("Main Application").Large()
                                            | Text.P("This sidebar is configured as the main app sidebar with toggle functionality.")
                                            | new Button("Test Action").OnClick(_ => client.Toast("Action performed!"))
                                    ).Title("Welcome")
                                    | new Card(
                                        "Additional content can be placed here. The sidebar will automatically collapse on smaller screens."
                                    ).Title("Content Area"),
                                sidebarContent: Layout.Vertical().Gap(1)
                                    | new Button("Dashboard").Variant(ButtonVariant.Ghost).OnClick(_ => client.Toast("Dashboard"))
                                    | new Button("Projects").Variant(ButtonVariant.Ghost).OnClick(_ => client.Toast("Projects"))
                                    | new Button("Team").Variant(ButtonVariant.Ghost).OnClick(_ => client.Toast("Team"))
                                    | new Button("Calendar").Variant(ButtonVariant.Ghost).OnClick(_ => client.Toast("Calendar")),
                                sidebarHeader: Layout.Vertical().Gap(2)
                                    | Text.Lead("Workspace")
                                    | new TextInput(placeholder: "Search...", variant: TextInputVariant.Search)
                            ).Open(false).MainAppSidebar(true);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout(
                """"
                "There is default padding of 2 in main content accessible via MainContentPadding by default."
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Resizable Sidebar
                
                You can make the sidebar resizable by users at runtime using the `.Resizable()` extension method. This adds a drag handle to the sidebar border that allows users to adjust the width:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ResizableSidebarExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ResizableSidebarExample : ViewBase
                    {
                        public override object? Build()
                        {
                            return new SidebarLayout(
                                mainContent: new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Resizable Sidebar Demo").Large()
                                        | Text.P("Drag the sidebar border to resize it. The sidebar width is constrained between 200px and 600px by default.")
                                ).Title("Main Content"),
                                sidebarContent: Layout.Vertical().Gap(2)
                                    | Text.P("Sidebar Content")
                                    | Text.P("Drag the right edge to resize this sidebar.").Small().Color(Colors.Gray)
                            ).Resizable();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("You can customize the min/max constraints using the `Size` API with `.Min()` and `.Max()`:").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ResizableSidebarCustomConstraintsExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ResizableSidebarCustomConstraintsExample : ViewBase
                    {
                        public override object? Build()
                        {
                            return new SidebarLayout(
                                mainContent: new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Custom Constraints").Large()
                                        | Text.P("This sidebar has custom width constraints: 150px min, 400px max, starting at 250px.")
                                ).Title("Main Content"),
                                sidebarContent: Layout.Vertical().Gap(2)
                                    | Text.P("Custom Width")
                                    | Text.P("Min: 150px, Max: 400px").Small().Color(Colors.Gray)
                            )
                            .Width(Size.Px(250).Min(Size.Px(150)).Max(Size.Px(400)))
                            .Resizable();
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout(
                """"
                "The resize handle supports mouse drag, touch gestures, and keyboard navigation with arrow keys for accessibility."
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### SidebarMenu Widget
                
                The `SidebarMenu` widget is specifically designed for sidebar navigation and provides advanced features like search highlighting and keyboard navigation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SidebarMenuAdvancedExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SidebarMenuAdvancedExample : ViewBase
                    {
                        private enum DocSection { Install, Quickstart, Examples, Buttons, Forms, Charts, Tables, Hooks, State, Performance }
                    
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var selectedItem = UseState<DocSection?>(null);
                    
                            MenuItem[] menuItems = new[]
                            {
                                MenuItem.Default("Getting Started")
                                    .Icon(Icons.Play).Children(
                                    MenuItem.Default("Installation")
                                        .Icon(Icons.Download).Tag(DocSection.Install),
                                    MenuItem.Default("Quick Start")
                                        .Icon(Icons.Zap).Tag(DocSection.Quickstart),
                                    MenuItem.Default("Examples")
                                        .Icon(Icons.Code).Tag(DocSection.Examples)
                                ),
                                MenuItem.Default("Components")
                                    .Icon(Icons.Package).Children(
                                    MenuItem.Default("Buttons")
                                        .Icon(Icons.MousePointer).Tag(DocSection.Buttons),
                                    MenuItem.Default("Forms")
                                        .Icon(Icons.FileText).Tag(DocSection.Forms),
                                    MenuItem.Default("Charts")
                                        .Icon(Icons.ChartBar).Tag(DocSection.Charts),
                                    MenuItem.Default("Tables")
                                        .Icon(Icons.Table).Tag(DocSection.Tables)
                                ),
                                MenuItem.Default("Advanced")
                                    .Icon(Icons.Cpu).Children(
                                    MenuItem.Default("Hooks")
                                        .Icon(Icons.Link).Tag(DocSection.Hooks),
                                    MenuItem.Default("State Management")
                                        .Icon(Icons.Database).Tag(DocSection.State),
                                    MenuItem.Default("Performance")
                                        .Icon(Icons.Gauge).Tag(DocSection.Performance)
                                )
                            };
                    
                            var menu = new SidebarMenu(
                                onSelect: evt =>
                                {
                                    if (Enum.TryParse<DocSection>(evt.Value?.ToString(), ignoreCase: true, out var section))
                                    {
                                        selectedItem.Set(section);
                                        client.Toast($"Selected: {section}");
                                    }
                                },
                                items: menuItems
                            );
                    
                            return new SidebarLayout(
                                mainContent: new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Documentation").Large()
                                        | Text.P($"Currently viewing: {(selectedItem.Value?.ToString() ?? "None")}")
                                ).Title("Content Area"),
                                sidebarContent: menu,
                                sidebarHeader: Text.Lead("Documentation Menu")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.SidebarLayout", "Ivy.SidebarLayoutExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Layouts/SidebarLayout.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class BasicSidebarExample : ViewBase
{
    public override object? Build()
    {
        return new SidebarLayout(
            mainContent: new Card(
                "This is the main content area. It takes up the remaining space after the sidebar."
            ).Title("Main Content"),
            sidebarContent: new Card(
                "This is the sidebar content where you can put navigation, menus, or other controls."
            ).Title("Sidebar")
        );
    }
}

public class SidebarWithHeaderFooterExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        return new SidebarLayout(
            mainContent: new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Welcome to the App").Large()
                    | Text.P("This is the main content area with a sidebar navigation.")
                    | new Button("Action Button")
                        .Variant(ButtonVariant.Primary)
                        .OnClick(_ => client.Toast("Action performed!"))
            ).Title("Dashboard"),
            sidebarContent: Layout.Vertical().Gap(2)
                | new Button("Home").Variant(ButtonVariant.Ghost)
                | new Button("Profile").Variant(ButtonVariant.Ghost)
                | new Button("Settings").Variant(ButtonVariant.Ghost)
                | new Button("Help").Variant(ButtonVariant.Ghost),
            sidebarHeader: Layout.Vertical().Gap(1)
                | Text.Lead("Navigation"),
            sidebarFooter: Layout.Vertical().Gap(1)
                | Text.P("Version 1.0.0").Small().Color(Colors.Gray)
        );
    }
}

public class SidebarMenuExample : ViewBase
{
    private enum NavPage { Home, Analytics, Profile, Account, Preferences, Help }

    public override object? Build()
    {
        var selectedPage = UseState<NavPage?>(NavPage.Home);
        var client = UseService<IClientProvider>();
        
        MenuItem[] menuItems = new[]
        {
            MenuItem.Default("Dashboard")
                .Icon(Icons.LayoutDashboard).Tag(NavPage.Home),
            MenuItem.Default("Analytics")
                .Icon(Icons.TrendingUp).Tag(NavPage.Analytics),
            MenuItem.Default("Settings")
                .Icon(Icons.Settings).Children(
                MenuItem.Default("Profile")
                    .Icon(Icons.User).Tag(NavPage.Profile),
                MenuItem.Default("Account")
                    .Icon(Icons.UserCog).Tag(NavPage.Account),
                MenuItem.Default("Preferences")
                    .Icon(Icons.Settings).Tag(NavPage.Preferences)
            ),
            MenuItem.Default("Help").Icon(Icons.CircleQuestionMark).Tag(NavPage.Help)
        };

        var sidebarMenu = new SidebarMenu(
            onSelect: evt => {
                if (Enum.TryParse<NavPage>(evt.Value?.ToString(), ignoreCase: true, out var page))
                {
                    selectedPage.Set(page);
                    client.Toast($"Navigated to {page}");
                }
            },
            items: menuItems
        );

        object RenderContent()
        {
            return (selectedPage.Value ?? NavPage.Home) switch
            {
                NavPage.Home => new Card("Welcome to the Dashboard!")
                    .Title("Dashboard"),
                NavPage.Analytics => new Card("Analytics and reports go here.")
                    .Title("Analytics"),
                NavPage.Profile => new Card("User profile settings.")
                    .Title("Profile"),
                NavPage.Account => new Card("Account management.")
                    .Title("Account"),
                NavPage.Preferences => new Card("User preferences.")
                    .Title("Preferences"),
                NavPage.Help => new Card("Help and documentation.")
                    .Title("Help & Support"),
                _ => new Card("Page not found.")
                    .Title("404")
            };
        }

        return new SidebarLayout(
            mainContent: RenderContent(),
            sidebarContent: sidebarMenu,
            sidebarHeader: Layout.Vertical().Gap(2)
                | Text.Lead("My App")
                | new TextInput(placeholder: "Search...", variant: TextInputVariant.Search),
            sidebarFooter: Layout.Horizontal().Gap(2)
                | new Avatar("JD").Size(Size.Units(20))
                | (Layout.Vertical()
                    | Text.P("John Doe").Small()
                    | Text.P("john@example.com").Small().Color(Colors.Gray))
        );
    }
}

public class MainAppSidebarExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();

        return new SidebarLayout(
            mainContent: Layout.Vertical().Gap(4)
                | new Card(
                    Layout.Vertical().Gap(2)
                        | Text.P("Main Application").Large()
                        | Text.P("This sidebar is configured as the main app sidebar with toggle functionality.")
                        | new Button("Test Action").OnClick(_ => client.Toast("Action performed!"))
                ).Title("Welcome")
                | new Card(
                    "Additional content can be placed here. The sidebar will automatically collapse on smaller screens."
                ).Title("Content Area"),
            sidebarContent: Layout.Vertical().Gap(1)
                | new Button("Dashboard").Variant(ButtonVariant.Ghost).OnClick(_ => client.Toast("Dashboard"))
                | new Button("Projects").Variant(ButtonVariant.Ghost).OnClick(_ => client.Toast("Projects"))
                | new Button("Team").Variant(ButtonVariant.Ghost).OnClick(_ => client.Toast("Team"))
                | new Button("Calendar").Variant(ButtonVariant.Ghost).OnClick(_ => client.Toast("Calendar")),
            sidebarHeader: Layout.Vertical().Gap(2)
                | Text.Lead("Workspace")
                | new TextInput(placeholder: "Search...", variant: TextInputVariant.Search)
        ).Open(false).MainAppSidebar(true);
    }
}

public class ResizableSidebarExample : ViewBase
{
    public override object? Build()
    {
        return new SidebarLayout(
            mainContent: new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Resizable Sidebar Demo").Large()
                    | Text.P("Drag the sidebar border to resize it. The sidebar width is constrained between 200px and 600px by default.")
            ).Title("Main Content"),
            sidebarContent: Layout.Vertical().Gap(2)
                | Text.P("Sidebar Content")
                | Text.P("Drag the right edge to resize this sidebar.").Small().Color(Colors.Gray)
        ).Resizable();
    }
}

public class ResizableSidebarCustomConstraintsExample : ViewBase
{
    public override object? Build()
    {
        return new SidebarLayout(
            mainContent: new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Custom Constraints").Large()
                    | Text.P("This sidebar has custom width constraints: 150px min, 400px max, starting at 250px.")
            ).Title("Main Content"),
            sidebarContent: Layout.Vertical().Gap(2)
                | Text.P("Custom Width")
                | Text.P("Min: 150px, Max: 400px").Small().Color(Colors.Gray)
        )
        .Width(Size.Px(250).Min(Size.Px(150)).Max(Size.Px(400)))
        .Resizable();
    }
}

public class SidebarMenuAdvancedExample : ViewBase
{
    private enum DocSection { Install, Quickstart, Examples, Buttons, Forms, Charts, Tables, Hooks, State, Performance }

    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var selectedItem = UseState<DocSection?>(null);
        
        MenuItem[] menuItems = new[]
        {
            MenuItem.Default("Getting Started")
                .Icon(Icons.Play).Children(
                MenuItem.Default("Installation")
                    .Icon(Icons.Download).Tag(DocSection.Install),
                MenuItem.Default("Quick Start")
                    .Icon(Icons.Zap).Tag(DocSection.Quickstart),
                MenuItem.Default("Examples")
                    .Icon(Icons.Code).Tag(DocSection.Examples)
            ),
            MenuItem.Default("Components")
                .Icon(Icons.Package).Children(
                MenuItem.Default("Buttons")
                    .Icon(Icons.MousePointer).Tag(DocSection.Buttons),
                MenuItem.Default("Forms")
                    .Icon(Icons.FileText).Tag(DocSection.Forms),
                MenuItem.Default("Charts")
                    .Icon(Icons.ChartBar).Tag(DocSection.Charts),
                MenuItem.Default("Tables")
                    .Icon(Icons.Table).Tag(DocSection.Tables)
            ),
            MenuItem.Default("Advanced")
                .Icon(Icons.Cpu).Children(
                MenuItem.Default("Hooks")
                    .Icon(Icons.Link).Tag(DocSection.Hooks),
                MenuItem.Default("State Management")
                    .Icon(Icons.Database).Tag(DocSection.State),
                MenuItem.Default("Performance")
                    .Icon(Icons.Gauge).Tag(DocSection.Performance)
            )
        };

        var menu = new SidebarMenu(
            onSelect: evt =>
            {
                if (Enum.TryParse<DocSection>(evt.Value?.ToString(), ignoreCase: true, out var section))
                {
                    selectedItem.Set(section);
                    client.Toast($"Selected: {section}");
                }
            },
            items: menuItems
        );

        return new SidebarLayout(
            mainContent: new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Documentation").Large()
                    | Text.P($"Currently viewing: {(selectedItem.Value?.ToString() ?? "None")}")
            ).Title("Content Area"),
            sidebarContent: menu,
            sidebarHeader: Text.Lead("Documentation Menu")
        );
    }
}
