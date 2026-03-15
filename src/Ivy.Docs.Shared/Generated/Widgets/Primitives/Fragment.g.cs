using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/05_Fragment.md", searchHints: ["wrapper", "container", "grouping", "fragment", "virtual", "invisible"])]
public class FragmentApp(bool onlyBody = false) : ViewBase
{
    public FragmentApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("fragment", "Fragment", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("conditional-rendering", "Conditional Rendering", 3), new ArticleHeading("multiple-return-elements", "Multiple Return Elements", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Fragment").OnLinkClick(onLinkClick)
            | Lead("Group multiple elements without adding extra DOM markup, similar to React Fragments, for clean component composition.")
            | new Markdown(
                """"
                The `Fragment` [widget](app://onboarding/concepts/widgets) is a container component that doesn't produce any HTML elements itself. It's useful for grouping multiple elements without adding extra markup to the DOM, similar to React Fragments. This makes it perfect for conditional rendering, returning multiple [widgets](app://onboarding/concepts/widgets) from a [view](app://onboarding/concepts/views), and creating clean component compositions.
                
                ## Basic Usage
                
                Fragment groups multiple widgets without adding DOM elements. Here's the simplest example:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicFragmentView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicFragmentView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new Fragment(
                                Text.P("Welcome"),
                                Text.P("This text is grouped with the heading above.")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Conditional Rendering
                
                Fragment is excellent for conditional rendering, allowing you to show or hide content based on [state](app://hooks/core/use-state):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ConditionalRenderingView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ConditionalRenderingView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var viewMode = UseState("user");
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("User Dashboard")
                                | (Layout.Horizontal().Gap(2)
                                    | new Button("User View", _ => {
                                        viewMode.Set("user");
                                        client.Toast("Switched to user view");
                                      })
                                        .Variant(viewMode.Value == "user" ? ButtonVariant.Primary : ButtonVariant.Secondary)
                                    | new Button("Admin View", _ => {
                                        viewMode.Set("admin");
                                        client.Toast("Switched to admin view");
                                      })
                                        .Variant(viewMode.Value == "admin" ? ButtonVariant.Primary : ButtonVariant.Secondary))
                                | (viewMode.Value == "admin"
                                    ? new Fragment(
                                        Text.P("Admin Controls"),
                                        Layout.Horizontal().Gap(2)
                                            | new Button("Reset System", _ => client.Toast("System reset initiated!")).Destructive()
                                            | new Button("View Logs", _ => client.Toast("Opening system logs..."))
                                            | new Button("Manage Users", _ => client.Toast("User management panel opened"))
                                      )
                                    : new Fragment(
                                        Text.P("User Profile"),
                                        Layout.Horizontal().Gap(2)
                                            | new Button("Edit Profile", _ => client.Toast("Profile editor opened"))
                                            | new Button("Change Password", _ => client.Toast("Password change dialog opened"))
                                            | new Button("View Settings", _ => client.Toast("User settings displayed"))
                                      ));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Fragment excels at conditional rendering. You can use it to show different content based on state, user roles, or any other conditions while keeping your code clean and readable.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Multiple Return Elements
                
                Fragment allows you to return multiple widgets from a single view method:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new MultipleElementsView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class MultipleElementsView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var selectedTab = UseState(0);
                            var tabs = new[] { "Overview", "Details", "Settings" };
                    
                            return new Fragment(
                                Text.P("Application"),
                                new Spacer().Height(Size.Units(4)),
                                // Tab navigation
                                Layout.Horizontal().Gap(2)
                                    | tabs.Select((tab, index) =>
                                        new Button(tab, _ => {
                                            selectedTab.Set(index);
                                            client.Toast($"Switched to {tab} tab");
                                          })
                                            .Variant(selectedTab.Value == index ? ButtonVariant.Primary : ButtonVariant.Secondary)
                                    ),
                                new Spacer().Height(Size.Units(4)),
                                // Content area
                                new Card(
                                    selectedTab.Value == 0 ? Text.P("Overview content here...") :
                                    selectedTab.Value == 1 ? Text.P("Details content here...") :
                                    Text.P("Settings content here...")
                                ).Title("Content"),
                                new Spacer().Height(Size.Units(4)),
                                // Header section
                                Layout.Horizontal().Gap(4)
                                    | new Button("Save", _ => client.Toast("Changes saved successfully!"))
                                    | new Button("Cancel", _ => client.Toast("Changes cancelled"))
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Fragment", "Ivy.FragmentExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Fragment.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Dynamic Content Generation",
                Vertical().Gap(4)
                | new Markdown("Fragment can be used to dynamically generate content based on data:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new DynamicContentView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class DynamicContentView : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                                var items = UseState(() => new string[] { "Item 1", "Item 2" });
                                var showDetails = UseState(false);
                        
                                return Layout.Vertical().Gap(4)
                                    | Text.P("Dynamic Content")
                                    | new Fragment(
                                        // Static controls
                                        Layout.Horizontal().Gap(2)
                                            | new Button("Add Item", _ => {
                                                var newItems = items.Value.Append($"Item {items.Value.Length + 1}").ToArray();
                                                items.Set(newItems);
                                                client.Toast($"Added Item {newItems.Length}");
                                              })
                                            | new Button("Toggle Details", _ => {
                                                showDetails.Set(!showDetails.Value);
                                                client.Toast(!showDetails.Value ? "Details hidden" : "Details shown");
                                              })
                                            | new Spacer().Width(Size.Grow())
                                            | new Button("Reset", _ => {
                                                items.Set(new string[] { "Item 1", "Item 2" });
                                                client.Toast("Table reset to default");
                                              }),
                        
                                        // Dynamic list
                                        Layout.Vertical().Gap(2)
                                            | items.Value.Select(item =>
                                                new Card(
                                                    Layout.Vertical().Gap(2)
                                                        | Text.H3(item)
                                                        | Text.P("Active").Small().Color(Colors.Green)
                                                        | (showDetails.Value ? Text.P($"Details for {item}").Small() : Text.P("Click 'Toggle Details' to see more").Small())
                                                ).Title("List Item")
                                            )
                                      );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}


public class BasicFragmentView : ViewBase
{
    public override object? Build()
    {
        return new Fragment(
            Text.P("Welcome"),
            Text.P("This text is grouped with the heading above.")
        );
    }
}

public class ConditionalRenderingView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var viewMode = UseState("user"); 
        
        return Layout.Vertical().Gap(4)
            | Text.P("User Dashboard")
            | (Layout.Horizontal().Gap(2)
                | new Button("User View", _ => {
                    viewMode.Set("user");
                    client.Toast("Switched to user view");
                  })
                    .Variant(viewMode.Value == "user" ? ButtonVariant.Primary : ButtonVariant.Secondary)
                | new Button("Admin View", _ => {
                    viewMode.Set("admin");
                    client.Toast("Switched to admin view");
                  })
                    .Variant(viewMode.Value == "admin" ? ButtonVariant.Primary : ButtonVariant.Secondary))
            | (viewMode.Value == "admin"
                ? new Fragment(
                    Text.P("Admin Controls"),
                    Layout.Horizontal().Gap(2)
                        | new Button("Reset System", _ => client.Toast("System reset initiated!")).Destructive()
                        | new Button("View Logs", _ => client.Toast("Opening system logs..."))
                        | new Button("Manage Users", _ => client.Toast("User management panel opened"))
                  )
                : new Fragment(
                    Text.P("User Profile"),
                    Layout.Horizontal().Gap(2)
                        | new Button("Edit Profile", _ => client.Toast("Profile editor opened"))
                        | new Button("Change Password", _ => client.Toast("Password change dialog opened"))
                        | new Button("View Settings", _ => client.Toast("User settings displayed"))
                  ));
    }
}

public class MultipleElementsView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var selectedTab = UseState(0);
        var tabs = new[] { "Overview", "Details", "Settings" };
        
        return new Fragment(
            Text.P("Application"),
            new Spacer().Height(Size.Units(4)),
            // Tab navigation
            Layout.Horizontal().Gap(2)
                | tabs.Select((tab, index) => 
                    new Button(tab, _ => {
                        selectedTab.Set(index);
                        client.Toast($"Switched to {tab} tab");
                      })
                        .Variant(selectedTab.Value == index ? ButtonVariant.Primary : ButtonVariant.Secondary)
                ),
            new Spacer().Height(Size.Units(4)),
            // Content area
            new Card(
                selectedTab.Value == 0 ? Text.P("Overview content here...") :
                selectedTab.Value == 1 ? Text.P("Details content here...") :
                Text.P("Settings content here...")
            ).Title("Content"),
            new Spacer().Height(Size.Units(4)),
            // Header section
            Layout.Horizontal().Gap(4)
                | new Button("Save", _ => client.Toast("Changes saved successfully!"))
                | new Button("Cancel", _ => client.Toast("Changes cancelled"))
        );
    }
}

public class DynamicContentView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var items = UseState(() => new string[] { "Item 1", "Item 2" });
        var showDetails = UseState(false);
        
        return Layout.Vertical().Gap(4)
            | Text.P("Dynamic Content")
            | new Fragment(
                // Static controls
                Layout.Horizontal().Gap(2)
                    | new Button("Add Item", _ => {
                        var newItems = items.Value.Append($"Item {items.Value.Length + 1}").ToArray();
                        items.Set(newItems);
                        client.Toast($"Added Item {newItems.Length}");
                      })
                    | new Button("Toggle Details", _ => {
                        showDetails.Set(!showDetails.Value);
                        client.Toast(!showDetails.Value ? "Details hidden" : "Details shown");
                      })
                    | new Spacer().Width(Size.Grow())
                    | new Button("Reset", _ => {
                        items.Set(new string[] { "Item 1", "Item 2" });
                        client.Toast("Table reset to default");
                      }),
                
                // Dynamic list
                Layout.Vertical().Gap(2)
                    | items.Value.Select(item => 
                        new Card(
                            Layout.Vertical().Gap(2)
                                | Text.H3(item)
                                | Text.P("Active").Small().Color(Colors.Green)
                                | (showDetails.Value ? Text.P($"Details for {item}").Small() : Text.P("Click 'Toggle Details' to see more").Small())
                        ).Title("List Item")
                    )
              );
    }
}
