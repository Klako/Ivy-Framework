using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Layouts;

[App(order:7, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/02_Layouts/07_TabsLayout.md", searchHints: ["navigation", "panels", "pages", "switcher", "tabbed", "sections"])]
public class TabsLayoutApp(bool onlyBody = false) : ViewBase
{
    public TabsLayoutApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("tabslayout", "TabsLayout", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("tabview-with-customization", "TabView with Customization", 3), new ArticleHeading("tabslayout-usage", "TabsLayout usage", 2), new ArticleHeading("with-event-handlers", "With Event Handlers", 3), new ArticleHeading("variant-usage", "Variant usage", 2), new ArticleHeading("content-variant-default", "Content Variant (default)", 3), new ArticleHeading("tabs-variant", "Tabs Variant", 3), new ArticleHeading("customize", "Customize", 2), new ArticleHeading("with-icons-and-badges", "With Icons and Badges", 3), new ArticleHeading("responsive-overflow", "Responsive Overflow", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # TabsLayout
                
                The TabsLayout [widget](app://onboarding/concepts/widgets) creates a tabbed [interface](app://onboarding/concepts/views) that allows users to switch between different content sections. It supports both traditional tabs and content-based variants, with features such as closable tabs, [badges](app://widgets/common/badge), [icons](app://widgets/primitives/icon), and drag-and-drop reordering.
                
                ## Basic Usage
                
                We recommend using Layout.Tabs to create simple tabbed interfaces.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Tabs(
    new Tab("Profile", "User profile information"),
    new Tab("Security", "Security settings"),
    new Tab("Preferences", "User preferences")
))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Tabs(
                        new Tab("Profile", "User profile information"),
                        new Tab("Security", "Security settings"),
                        new Tab("Preferences", "User preferences")
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                This example creates a basic layout with three tabs.
                
                ### TabView with Customization
                
                This example demonstrates how to combine multiple TabView features, including icons, badges, variant selection, and size control:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Tabs(
    new Tab("Customers", "Customer list").Icon(Icons.User).Badge("10"),
    new Tab("Orders", "Order management").Icon(Icons.DollarSign).Badge("0"),
    new Tab("Settings", "Configuration").Icon(Icons.Settings).Badge("999")
))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Tabs(
                        new Tab("Customers", "Customer list").Icon(Icons.User).Badge("10"),
                        new Tab("Orders", "Order management").Icon(Icons.DollarSign).Badge("0"),
                        new Tab("Settings", "Configuration").Icon(Icons.Settings).Badge("999")
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                It showcases the fluent API of TabView, which allows chaining multiple configuration methods for a complete tab setup with visual indicators and precise layout control.
                
                ## TabsLayout usage
                
                If you need more flexibility in creating and managing tabs, TabsLayout offers a comprehensive API for enhanced tab configuration.
                
                The first parameter is the selected tab index (0), and the remaining parameters are the Tab objects.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TabsLayout(null, null, null, null, 0,
    new Tab("Overview", "This is the overview content"),
    new Tab("Details", "This is the details content"),
    new Tab("Settings", "This is the settings content")
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new TabsLayout(null, null, null, null, 0,
                        new Tab("Overview", "This is the overview content"),
                        new Tab("Details", "This is the details content"),
                        new Tab("Settings", "This is the settings content")
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### With Event Handlers
                
                - `onSelect`: Handles tab selection events
                - `onClose`: Adds close functionality to tabs
                - `onRefresh`: Adds refresh buttons to tabs
                - `onReorder`: Enables drag-and-drop tab reordering
                - `selectedIndex`: Sets the initially selected tab
                
                This example demonstrates how to handle all available events. The event handlers receive the tab index and can perform custom actions such as logging, [state](app://hooks/core/use-state) updates, or API calls.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TabsLayout(
    onSelect: (e) => Console.WriteLine($"Selected: {e.Value}"),
    onClose: (e) => Console.WriteLine($"Closed: {e.Value}"),
    onRefresh: (e) => Console.WriteLine($"Refreshed: {e.Value}"),
    onReorder: null,
    selectedIndex: 0,
    new Tab("Tab 1", "Content 1"),
    new Tab("Tab 2", "Content 2"),
    new Tab("Tab 3", "Content 3")
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new TabsLayout(
                        onSelect: (e) => Console.WriteLine($"Selected: {e.Value}"),
                        onClose: (e) => Console.WriteLine($"Closed: {e.Value}"),
                        onRefresh: (e) => Console.WriteLine($"Refreshed: {e.Value}"),
                        onReorder: null,
                        selectedIndex: 0,
                        new Tab("Tab 1", "Content 1"),
                        new Tab("Tab 2", "Content 2"),
                        new Tab("Tab 3", "Content 3")
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Variant usage
                
                The default variant is `Content`, which emphasizes the content area. Use `TabsVariant.Tabs` only when tab navigation is itself a primary UI concern (e.g., a settings page with many sections).
                
                ### Content Variant (default)
                
                The Content variant emphasizes the content area with subtle tab indicators. This is ideal for content-heavy apps where the focus should be on the displayed information. Since `Content` is the default, you don't need to specify it explicitly.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TabsLayout(null, null, null, null, 0,
    new Tab("Overview", "Overview content here"),
    new Tab("Details", "Detailed information here"),
    new Tab("Settings", "Configuration options here")
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new TabsLayout(null, null, null, null, 0,
                        new Tab("Overview", "Overview content here"),
                        new Tab("Details", "Detailed information here"),
                        new Tab("Settings", "Configuration options here")
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Tabs Variant
                
                The Tabs variant displays tabs as clickable buttons with an underline indicator for the active tab, providing a traditional tab navigation interface.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TabsLayout(null, null, null, null, 0,
    new Tab("First", "First tab content"),
    new Tab("Second", "Second tab content"),
    new Tab("Third", "Third tab content")
).Variant(TabsVariant.Tabs))),
                new Tab("Code", new CodeBlock(
                    """"
                    new TabsLayout(null, null, null, null, 0,
                        new Tab("First", "First tab content"),
                        new Tab("Second", "Second tab content"),
                        new Tab("Third", "Third tab content")
                    ).Variant(TabsVariant.Tabs)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Customize
                
                ### With Icons and Badges
                
                Enhance tabs with icons and badges for better visual representation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TabsLayout(null, null, null, null, 0,
    new Tab("Customers", "Customer list").Icon(Icons.User).Badge("10"),
    new Tab("Orders", "Order management").Icon(Icons.DollarSign).Badge("0"),
    new Tab("Settings", "Configuration").Icon(Icons.Settings).Badge("999")
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new TabsLayout(null, null, null, null, 0,
                        new Tab("Customers", "Customer list").Icon(Icons.User).Badge("10"),
                        new Tab("Orders", "Order management").Icon(Icons.DollarSign).Badge("0"),
                        new Tab("Settings", "Configuration").Icon(Icons.Settings).Badge("999")
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Responsive Overflow
                
                When there are many tabs that don't fit in the available width, the component automatically shows a dropdown menu for hidden tabs. Try resizing your browser window to see this in action.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Tabs(
    new Tab("Home", "Home content"),
    new Tab("Products", "Products content"),
    new Tab("Services", "Services content"),
    new Tab("About", "About content"),
    new Tab("Contact", "Contact content"),
    new Tab("Blog", "Blog content"),
    new Tab("FAQ", "FAQ content"),
    new Tab("Support", "Support content"),
    new Tab("Careers", "Careers content"),
    new Tab("Partners", "Partners content"),
    new Tab("Pricing", "Pricing content"),
    new Tab("Documentation", "Documentation content"),
    new Tab("Community", "Community content")
))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Tabs(
                        new Tab("Home", "Home content"),
                        new Tab("Products", "Products content"),
                        new Tab("Services", "Services content"),
                        new Tab("About", "About content"),
                        new Tab("Contact", "Contact content"),
                        new Tab("Blog", "Blog content"),
                        new Tab("FAQ", "FAQ content"),
                        new Tab("Support", "Support content"),
                        new Tab("Careers", "Careers content"),
                        new Tab("Partners", "Partners content"),
                        new Tab("Pricing", "Pricing content"),
                        new Tab("Documentation", "Documentation content"),
                        new Tab("Community", "Community content")
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.TabsLayout", "Ivy.TabsLayoutExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Layouts/TabsLayout.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Widgets.Common.BadgeApp), typeof(Widgets.Primitives.IconApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}

