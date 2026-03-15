using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Layouts;

[App(order:4, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/02_Layouts/04_HeaderLayout.md", searchHints: ["layout", "header", "sticky", "toolbar", "fixed", "top"])]
public class HeaderLayoutApp(bool onlyBody = false) : ViewBase
{
    public HeaderLayoutApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("headerlayout", "HeaderLayout", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("common-use-cases", "Common Use Cases", 2), new ArticleHeading("toolbar-with-actions", "Toolbar with Actions", 3), new ArticleHeading("dashboard-header", "Dashboard Header", 3), new ArticleHeading("navigation-header", "Navigation Header", 3), new ArticleHeading("form-with-header-actions", "Form with Header Actions", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# HeaderLayout").OnLinkClick(onLinkClick)
            | Lead("HeaderLayout provides a fixed header area above scrollable content, perfect for toolbars, [navigation](app://onboarding/concepts/navigation), and persistent controls that should remain visible while users scroll through content.")
            | new Markdown(
                """"
                The `HeaderLayout` [widget](app://onboarding/concepts/widgets) creates a layout with a fixed header section at the top and a scrollable content area below. Perfect for applications that need persistent navigation, toolbars, or status information while allowing the main content to scroll independently.
                
                By default, the content area uses a ScrollArea wrapper that enables scrolling. You can disable this behavior using the `.Scroll()` method when your content (like a Kanban board) needs to handle its own scrolling.
                
                ## Basic Usage
                
                The simplest HeaderLayout takes a header and content as parameters:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicHeaderExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicHeaderExample : ViewBase
                    {
                        public override object? Build()
                        {
                            return new HeaderLayout(
                                header: new Card("Fixed Header Content")
                                    .Title("Header Area"),
                                content: Layout.Vertical().Gap(4)
                                    | Text.P("The header above remains fixed while content scrolls.")
                                    | Text.P("Add as much content as needed.")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("## Common Use Cases").OnLinkClick(onLinkClick)
            | new Callout("For [widgets](app://onboarding/concepts/widgets) that handle their own scrolling (like Kanban boards), use `.Scroll(Scroll.None)` to disable the HeaderLayout's ScrollArea wrapper. Height is automatically set to [Size.Full()](app://api-reference/ivy/size) when scroll is disabled.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Toolbar with Actions
                
                HeaderLayout is perfect for creating toolbars with action buttons that remain accessible:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ToolbarExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ToolbarExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var searchText = UseState("");
                    
                            var toolbar = new Card(
                                Layout.Horizontal().Gap(4)
                                    | searchText.ToTextInput()
                                        .Placeholder("Search items...")
                                        .Variant(TextInputVariant.Search)
                                    | new Button("Add Item")
                                        .Icon(Icons.Plus)
                                        .Variant(ButtonVariant.Primary)
                                        .OnClick(_ => client.Toast("Add item clicked"))
                                    | new Button("Filter")
                                        .Icon(Icons.Search)
                                        .Variant(ButtonVariant.Outline)
                                        .OnClick(_ => client.Toast("Filter clicked"))
                                    | new Button("Export")
                                        .Icon(Icons.Download)
                                        .Variant(ButtonVariant.Ghost)
                                        .OnClick(_ => client.Toast("Export clicked"))
                            );
                    
                            var content = Layout.Vertical().Gap(4)
                                | new Card("Item 1 - This is some sample content")
                                | new Card("Item 2 - More content that will scroll");
                            return new HeaderLayout(toolbar, content);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Dashboard Header
                
                Use HeaderLayout for dashboard-style interfaces with status indicators and quick actions:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DashboardHeaderExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DashboardHeaderExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                    
                            var dashboardHeader = new Card(
                                Layout.Horizontal().Gap(4)
                                    | Text.P("Analytics Dashboard")
                                    | new Spacer()
                                    | Layout.Horizontal().Gap(3)
                                        | new Badge("Live")
                                        | new Button("Refresh")
                                            .Icon(Icons.RefreshCw)
                                            .Variant(ButtonVariant.Outline)
                                            .OnClick(_ => client.Toast("Refreshing data..."))
                            );
                    
                            var dashboardContent = Layout.Grid().Columns(3).Rows(2).Gap(4)
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Users").Small()
                                        | Text.Label("12.3K").Color(Colors.Primary)
                                ).GridColumn(1).GridRow(1)
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Revenue").Small()
                                        | Text.Label("$54K").Color(Colors.Primary)
                                ).GridColumn(2).GridRow(1)
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Growth").Small()
                                        | Text.Label("+23%").Color(Colors.Primary)
                                ).GridColumn(3).GridRow(1)
                                | new Card("Chart Area - Interactive dashboard content")
                                    .GridColumn(1).GridRow(2).GridColumnSpan(2)
                                | new Card("Performance Metrics - System health status")
                                    .GridColumn(3).GridRow(2);
                    
                            return new HeaderLayout(dashboardHeader, dashboardContent);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Navigation Header
                
                Create navigation headers for content-heavy pages:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new NavigationHeaderExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class NavigationHeaderExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var currentSection = UseState("introduction");
                    
                            var navHeader = new Card(
                                Layout.Horizontal().Gap(3)
                                    | new Button("Intro")
                                        .Variant(currentSection.Value == "introduction" ? ButtonVariant.Primary : ButtonVariant.Ghost)
                                        .OnClick(_ => {
                                            currentSection.Value = "introduction";
                                            client.Toast("Navigated to Introduction");
                                        })
                                    | new Button("Guide")
                                        .Variant(currentSection.Value == "getting-started" ? ButtonVariant.Primary : ButtonVariant.Ghost)
                                        .OnClick(_ => {
                                            currentSection.Value = "getting-started";
                                            client.Toast("Navigated to Getting Started");
                                        })
                                    | new Button("Advanced")
                                        .Variant(currentSection.Value == "advanced" ? ButtonVariant.Primary : ButtonVariant.Ghost)
                                        .OnClick(_ => {
                                            currentSection.Value = "advanced";
                                            client.Toast("Navigated to Advanced");
                                        })
                                    | new Spacer()
                                    | new Button("Export").Icon(Icons.Download).Variant(ButtonVariant.Outline)
                            );
                    
                            object GetSectionContent()
                            {
                                return currentSection.Value switch
                                {
                                    "introduction" => Layout.Vertical().Gap(4)
                                        | Text.Label("Introduction")
                                        | Text.P("Welcome to our comprehensive guide. This section covers the fundamental concepts you need to understand.")
                                        | new Card("Key concepts highlighted here")
                                        | new Card("Getting familiar with the framework")
                                        | new Card("Understanding core principles")
                                        | Text.P("Continue reading to learn more about the framework's capabilities."),
                    
                                    "getting-started" => Layout.Vertical().Gap(4)
                                        | Text.Label("Getting Started")
                                        | Text.P("Follow these steps to get started quickly with your first project.")
                                        | new Card("Step 1: Install the framework")
                                        | new Card("Step 2: Create your first app")
                                        | new Card("Step 3: Build and run")
                                        | Text.Code("npm install ivy-framework")
                                        | Text.P("Once installed, you can start building amazing applications."),
                    
                                    "advanced" => Layout.Vertical().Gap(4)
                                        | Text.Label("Advanced Topics")
                                        | Text.P("Advanced usage patterns and techniques for experienced developers.")
                                        | new Card("Custom components and widgets")
                                        | new Card("Performance optimization techniques")
                                        | new Card("Advanced state management")
                                        | new Card("Integration with external services")
                                        | Text.P("These topics require a solid understanding of the framework basics."),
                    
                                    _ => Text.P("Section not found")
                                };
                            }
                    
                            return new HeaderLayout(navHeader, GetSectionContent());
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Form with Header Actions
                
                HeaderLayout works well for forms with header-level actions:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormHeaderExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FormHeaderExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var name = UseState("John Doe");
                            var email = UseState("john@example.com");
                            var bio = UseState("Software developer with 5 years of experience...");
                    
                            var formHeader = new Card(
                                Layout.Horizontal().Gap(4)
                                    | Text.Label("Edit Profile")
                                    | new Spacer().Width(Size.Grow())
                                    | new Button("Cancel").Variant(ButtonVariant.Ghost)
                                    | new Button("Save").Variant(ButtonVariant.Primary)
                                            .OnClick(_ => client.Toast("Profile saved!"))
                            );
                    
                            var formContent = Layout.Vertical().Gap(4)
                                | new Card(
                                    Layout.Vertical().Gap(3)
                                        | Text.P("Personal Information").Small()
                                        | name.ToTextInput().Placeholder("Full Name")
                                        | email.ToTextInput().Placeholder("Email")
                                        | bio.ToTextInput().Placeholder("Bio").Variant(TextInputVariant.Textarea)
                                )
                                | new Card(
                                    Layout.Vertical().Gap(3)
                                        | Text.P("Account Settings").Small()
                                        | new BoolInput<bool>(UseState(true)).Label("Email notifications")
                                        | new BoolInput<bool>(UseState(false)).Label("SMS notifications")
                                        | new BoolInput<bool>(UseState(true)).Label("Marketing emails")
                                )
                                | new Card(
                                    Layout.Vertical().Gap(3)
                                        | Text.P("Privacy").Small()
                                        | new BoolInput<bool>(UseState(true)).Label("Profile visibility")
                                        | new BoolInput<bool>(UseState(false)).Label("Show online status")
                                );
                    
                            return new HeaderLayout(formHeader, formContent);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.HeaderLayout", null, "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Layouts/HeaderLayout.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.SizeApp)]; 
        return article;
    }
}


public class BasicHeaderExample : ViewBase
{
    public override object? Build()
    {
        return new HeaderLayout(
            header: new Card("Fixed Header Content")
                .Title("Header Area"),
            content: Layout.Vertical().Gap(4)
                | Text.P("The header above remains fixed while content scrolls.")
                | Text.P("Add as much content as needed.")
        );
    }
}

public class ToolbarExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var searchText = UseState("");
        
        var toolbar = new Card(
            Layout.Horizontal().Gap(4)
                | searchText.ToTextInput()
                    .Placeholder("Search items...")
                    .Variant(TextInputVariant.Search)
                | new Button("Add Item")
                    .Icon(Icons.Plus)
                    .Variant(ButtonVariant.Primary)
                    .OnClick(_ => client.Toast("Add item clicked"))
                | new Button("Filter")
                    .Icon(Icons.Search)
                    .Variant(ButtonVariant.Outline)
                    .OnClick(_ => client.Toast("Filter clicked"))
                | new Button("Export")
                    .Icon(Icons.Download)
                    .Variant(ButtonVariant.Ghost)
                    .OnClick(_ => client.Toast("Export clicked"))
        );

        var content = Layout.Vertical().Gap(4)
            | new Card("Item 1 - This is some sample content")
            | new Card("Item 2 - More content that will scroll");
        return new HeaderLayout(toolbar, content);
    }
}

public class DashboardHeaderExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var dashboardHeader = new Card(
            Layout.Horizontal().Gap(4)
                | Text.P("Analytics Dashboard")
                | new Spacer()
                | Layout.Horizontal().Gap(3)
                    | new Badge("Live")
                    | new Button("Refresh")
                        .Icon(Icons.RefreshCw)
                        .Variant(ButtonVariant.Outline)
                        .OnClick(_ => client.Toast("Refreshing data..."))
        );

        var dashboardContent = Layout.Grid().Columns(3).Rows(2).Gap(4)
            | new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Users").Small()
                    | Text.Label("12.3K").Color(Colors.Primary)
            ).GridColumn(1).GridRow(1)
            | new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Revenue").Small()
                    | Text.Label("$54K").Color(Colors.Primary)
            ).GridColumn(2).GridRow(1)
            | new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Growth").Small()
                    | Text.Label("+23%").Color(Colors.Primary)
            ).GridColumn(3).GridRow(1)
            | new Card("Chart Area - Interactive dashboard content")
                .GridColumn(1).GridRow(2).GridColumnSpan(2)
            | new Card("Performance Metrics - System health status")
                .GridColumn(3).GridRow(2);

        return new HeaderLayout(dashboardHeader, dashboardContent);
    }
}

public class NavigationHeaderExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var currentSection = UseState("introduction");
        
        var navHeader = new Card(
            Layout.Horizontal().Gap(3)
                | new Button("Intro")
                    .Variant(currentSection.Value == "introduction" ? ButtonVariant.Primary : ButtonVariant.Ghost)
                    .OnClick(_ => {
                        currentSection.Value = "introduction";
                        client.Toast("Navigated to Introduction");
                    })
                | new Button("Guide")
                    .Variant(currentSection.Value == "getting-started" ? ButtonVariant.Primary : ButtonVariant.Ghost)
                    .OnClick(_ => {
                        currentSection.Value = "getting-started";
                        client.Toast("Navigated to Getting Started");
                    })
                | new Button("Advanced")
                    .Variant(currentSection.Value == "advanced" ? ButtonVariant.Primary : ButtonVariant.Ghost)
                    .OnClick(_ => {
                        currentSection.Value = "advanced";
                        client.Toast("Navigated to Advanced");
                    })
                | new Spacer()
                | new Button("Export").Icon(Icons.Download).Variant(ButtonVariant.Outline)
        );

        object GetSectionContent()
        {
            return currentSection.Value switch
            {
                "introduction" => Layout.Vertical().Gap(4)
                    | Text.Label("Introduction")
                    | Text.P("Welcome to our comprehensive guide. This section covers the fundamental concepts you need to understand.")
                    | new Card("Key concepts highlighted here")
                    | new Card("Getting familiar with the framework")
                    | new Card("Understanding core principles")
                    | Text.P("Continue reading to learn more about the framework's capabilities."),
                
                "getting-started" => Layout.Vertical().Gap(4)
                    | Text.Label("Getting Started")
                    | Text.P("Follow these steps to get started quickly with your first project.")
                    | new Card("Step 1: Install the framework")
                    | new Card("Step 2: Create your first app")
                    | new Card("Step 3: Build and run")
                    | Text.Code("npm install ivy-framework")
                    | Text.P("Once installed, you can start building amazing applications."),
                
                "advanced" => Layout.Vertical().Gap(4)
                    | Text.Label("Advanced Topics")
                    | Text.P("Advanced usage patterns and techniques for experienced developers.")
                    | new Card("Custom components and widgets")
                    | new Card("Performance optimization techniques")
                    | new Card("Advanced state management")
                    | new Card("Integration with external services")
                    | Text.P("These topics require a solid understanding of the framework basics."),
                
                _ => Text.P("Section not found")
            };
        }

        return new HeaderLayout(navHeader, GetSectionContent());
    }
}

public class FormHeaderExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var name = UseState("John Doe");
        var email = UseState("john@example.com");
        var bio = UseState("Software developer with 5 years of experience...");

        var formHeader = new Card(
            Layout.Horizontal().Gap(4)
                | Text.Label("Edit Profile")
                | new Spacer().Width(Size.Grow())
                | new Button("Cancel").Variant(ButtonVariant.Ghost)
                | new Button("Save").Variant(ButtonVariant.Primary)
                        .OnClick(_ => client.Toast("Profile saved!"))
        );

        var formContent = Layout.Vertical().Gap(4)
            | new Card(
                Layout.Vertical().Gap(3)
                    | Text.P("Personal Information").Small()
                    | name.ToTextInput().Placeholder("Full Name")
                    | email.ToTextInput().Placeholder("Email")
                    | bio.ToTextInput().Placeholder("Bio").Variant(TextInputVariant.Textarea)
            )
            | new Card(
                Layout.Vertical().Gap(3)
                    | Text.P("Account Settings").Small()
                    | new BoolInput<bool>(UseState(true)).Label("Email notifications")
                    | new BoolInput<bool>(UseState(false)).Label("SMS notifications")
                    | new BoolInput<bool>(UseState(true)).Label("Marketing emails")
            )
            | new Card(
                Layout.Vertical().Gap(3)
                    | Text.P("Privacy").Small()
                    | new BoolInput<bool>(UseState(true)).Label("Profile visibility")
                    | new BoolInput<bool>(UseState(false)).Label("Show online status")
            );

        return new HeaderLayout(formHeader, formContent);
    }
}
