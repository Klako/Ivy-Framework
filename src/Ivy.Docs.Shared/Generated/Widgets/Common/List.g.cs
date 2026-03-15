using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:7, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/07_List.md", searchHints: ["items", "collection", "listitem", "menu", "rows", "scroll"])]
public class ListApp(bool onlyBody = false) : ViewBase
{
    public ListApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("list", "List", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("listitem-configuration", "ListItem Configuration", 2), new ArticleHeading("interactive-lists", "Interactive Lists", 2), new ArticleHeading("clickable-items", "Clickable Items", 3), new ArticleHeading("dynamic-content", "Dynamic Content", 3), new ArticleHeading("search-and-filter", "Search and Filter", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# List").OnLinkClick(onLinkClick)
            | Lead("Display collections of items in organized, styled lists with customizable formatting and interactive [elements](app://onboarding/concepts/widgets).")
            | new Markdown(
                """"
                The `List` [widget](app://onboarding/concepts/widgets) is a container designed to render collections of items in a vertical layout. It works seamlessly with `ListItem` components to create interactive, searchable, and customizable lists that are perfect for [navigation menus](app://onboarding/concepts/navigation), data displays, and [user interfaces](app://onboarding/concepts/views).
                
                ## Basic Usage
                
                The simplest way to create a list is by passing items directly to the constructor:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicListDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var items = new[]
                            {
                                new ListItem("Apple"),
                                new ListItem("Banana"),
                                new ListItem("Cherry")
                            };
                    
                            return new List(items);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicListDemo())
            )
            | new Markdown(
                """"
                ## ListItem Configuration
                
                `ListItem`s are highly customizable, supporting titles, subtitles, [icons](app://widgets/primitives/icon), [badges](app://widgets/common/badge), and custom content via the `.Content()` extension method.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ListConfigDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ListConfigDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var notifications = UseState(false);
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Title and Subtitle").Large()
                                | new List(new[]
                                {
                                    new ListItem("John Doe").Subtitle("Software Engineer"),
                                    new ListItem("Jane Smith").Subtitle("Product Manager")
                                })
                                | Text.P("Icons").Large()
                                | new List(new[]
                                {
                                    new ListItem("Dashboard").Icon(Icons.House).Subtitle("Main overview"),
                                    new ListItem("Settings").Icon(Icons.Settings).Subtitle("Configuration")
                                })
                                | Text.P("Badges").Large()
                                | new List(new[]
                                {
                                    new ListItem("New Message").Subtitle("From John Doe").Badge("3"),
                                    new ListItem("System Update").Subtitle("Available now").Badge("!")
                                })
                                | Text.P("Custom Content").Large()
                                | new List(new[]
                                {
                                    new ListItem("Notifications").Icon(Icons.Bell)
                                        .Content(new BoolInput(notifications, variant: BoolInputVariant.Switch)),
                                    new ListItem("Status").Icon(Icons.Activity)
                                        .Content(
                                            Layout.Horizontal().Gap(2)
                                                | new Badge("Online", BadgeVariant.Success)
                                                | Text.Muted("Last seen 2 min ago")
                                        ),
                                    new ListItem("Search").Icon(Icons.Search)
                                        .Content(new TextInput("", placeholder: "Type to search..."))
                                });
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Interactive Lists
                
                Make list items interactive with click handlers and dynamic updates.
                
                ### Clickable Items
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new InteractiveListDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class InteractiveListDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                    
                            var onItemClick = new Action<Event<ListItem>>(e =>
                            {
                                var item = e.Sender;
                                client.Toast($"Clicked: {item.Title}", "Item Selected");
                            });
                    
                            var items = new[]
                            {
                                new ListItem("Click me!").OnClick(onItemClick).Icon(Icons.MousePointer),
                                new ListItem("Me too!").OnClick(onItemClick).Icon(Icons.MousePointer),
                                new ListItem("Unavailable action").OnClick(onItemClick).Icon(Icons.Ban).Disabled()
                            };
                    
                            return new List(items);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Dynamic Content
                
                Create lists from dynamic data sources using [UseState](app://hooks/core/use-state).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new DynamicListDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class DynamicListDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var items = UseState(new[] { "Item 1", "Item 2", "Item 3" });
                    
                            var addItem = new Action<Event<Button>>(e =>
                            {
                                var newItems = items.Value.Append($"Item {items.Value.Length + 1}").ToArray();
                                items.Set(newItems);
                            });
                    
                            var removeItem = new Action<Event<Button>>(e =>
                            {
                                if (items.Value.Length > 0)
                                {
                                    var newItems = items.Value.Take(items.Value.Length - 1).ToArray();
                                    items.Set(newItems);
                                }
                            });
                    
                            return Layout.Vertical().Gap(2)
                                | (Layout.Horizontal().Gap(1)
                                    | new Button("Add Item", addItem).Variant(ButtonVariant.Secondary)
                                    | new Button("Remove Item", removeItem).Variant(ButtonVariant.Destructive))
                                | new List(items.Value.Select(item => new ListItem(item)));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                Lists in Ivy are highly customizable. You can combine them with other widgets like Cards, Badges, and Buttons to create rich, interactive [interfaces](app://onboarding/concepts/views). The `OnClick` event on ListItems makes it easy to build [navigation](app://onboarding/concepts/navigation) and user interactions.
                
                ### Search and Filter
                
                Implement search functionality with filtered lists:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SearchableListDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SearchableListDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var allItems = new[] { "Apple", "Banana", "Cherry", "Date" };
                            var searchTerm = UseState("");
                            var filteredItems = UseState(allItems);
                    
                            UseEffect(() =>
                            {
                                var filtered = allItems.Where(item =>
                                    item.Contains(searchTerm.Value, StringComparison.OrdinalIgnoreCase)).ToArray();
                                filteredItems.Set(filtered);
                            }, [searchTerm]);
                    
                            var listItems = filteredItems.Value.Select(item => new ListItem(item));
                    
                            return Layout.Vertical().Gap(2)
                                | searchTerm.ToSearchInput().Placeholder("Search fruits...")
                                | new List(listItems);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.List", "Ivy.WidgetBaseExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Lists/List.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ExamplesListDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ExamplesListDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            // Custom Item Rendering Data
                            var products = new[]
                            {
                                new { Name = "Laptop", Price = 999.99m, Stock = 15 },
                                new { Name = "Mouse", Price = 29.99m, Stock = 50 }
                            };
                    
                            var customItems = products.Select(product => new ListItem(product.Name)
                                .Subtitle($"${product.Price} - {product.Stock} in stock")
                                .Content(
                                    Layout.Horizontal().Gap(2)
                                        | Text.Block($"${product.Price}")
                                        | new Badge(product.Stock.ToString()).Variant(BadgeVariant.Secondary)
                                )
                            );
                    
                            // Time Rendering Data
                            var timeItem = new ListItem("Task created")
                                .Subtitle($"Created at {DateTime.Now:HH:mm:ss}");
                    
                            return Layout.Vertical().Gap(4)
                                | Text.P("Custom Item Rendering").Large()
                                | new List(customItems)
                                | Text.P("Time Rendering").Large()
                                | new List(new[] { timeItem });
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Widgets.Primitives.IconApp), typeof(Widgets.Common.BadgeApp), typeof(Hooks.Core.UseStateApp)]; 
        return article;
    }
}


public class BasicListDemo : ViewBase
{
    public override object? Build()
    {
        var items = new[]
        {
            new ListItem("Apple"),
            new ListItem("Banana"),
            new ListItem("Cherry")
        };
        
        return new List(items);
    }
}

public class ListConfigDemo : ViewBase
{
    public override object? Build()
    {
        var notifications = UseState(false);
    
        return Layout.Vertical().Gap(4)
            | Text.P("Title and Subtitle").Large()
            | new List(new[]
            {
                new ListItem("John Doe").Subtitle("Software Engineer"),
                new ListItem("Jane Smith").Subtitle("Product Manager")
            })
            | Text.P("Icons").Large()
            | new List(new[]
            {
                new ListItem("Dashboard").Icon(Icons.House).Subtitle("Main overview"),
                new ListItem("Settings").Icon(Icons.Settings).Subtitle("Configuration")
            })
            | Text.P("Badges").Large()
            | new List(new[]
            {
                new ListItem("New Message").Subtitle("From John Doe").Badge("3"),
                new ListItem("System Update").Subtitle("Available now").Badge("!")
            })
            | Text.P("Custom Content").Large()
            | new List(new[]
            {
                new ListItem("Notifications").Icon(Icons.Bell)
                    .Content(new BoolInput(notifications, variant: BoolInputVariant.Switch)),
                new ListItem("Status").Icon(Icons.Activity)
                    .Content(
                        Layout.Horizontal().Gap(2)
                            | new Badge("Online", BadgeVariant.Success)
                            | Text.Muted("Last seen 2 min ago")
                    ),
                new ListItem("Search").Icon(Icons.Search)
                    .Content(new TextInput("", placeholder: "Type to search..."))
            });
    }
}

public class InteractiveListDemo : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var onItemClick = new Action<Event<ListItem>>(e =>
        {
            var item = e.Sender;
            client.Toast($"Clicked: {item.Title}", "Item Selected");
        });
        
        var items = new[]
        {
            new ListItem("Click me!").OnClick(onItemClick).Icon(Icons.MousePointer),
            new ListItem("Me too!").OnClick(onItemClick).Icon(Icons.MousePointer),
            new ListItem("Unavailable action").OnClick(onItemClick).Icon(Icons.Ban).Disabled()
        };

        return new List(items);
    }
}

public class DynamicListDemo : ViewBase
{
    public override object? Build()
    {
        var items = UseState(new[] { "Item 1", "Item 2", "Item 3" });
        
        var addItem = new Action<Event<Button>>(e =>
        {
            var newItems = items.Value.Append($"Item {items.Value.Length + 1}").ToArray();
            items.Set(newItems);
        });
        
        var removeItem = new Action<Event<Button>>(e =>
        {
            if (items.Value.Length > 0)
            {
                var newItems = items.Value.Take(items.Value.Length - 1).ToArray();
                items.Set(newItems);
            }
        });
        
        return Layout.Vertical().Gap(2)
            | (Layout.Horizontal().Gap(1)
                | new Button("Add Item", addItem).Variant(ButtonVariant.Secondary)
                | new Button("Remove Item", removeItem).Variant(ButtonVariant.Destructive))
            | new List(items.Value.Select(item => new ListItem(item)));
    }
}

public class SearchableListDemo : ViewBase
{
    public override object? Build()
    {
        var allItems = new[] { "Apple", "Banana", "Cherry", "Date" };
        var searchTerm = UseState("");
        var filteredItems = UseState(allItems);
        
        UseEffect(() =>
        {
            var filtered = allItems.Where(item => 
                item.Contains(searchTerm.Value, StringComparison.OrdinalIgnoreCase)).ToArray();
            filteredItems.Set(filtered);
        }, [searchTerm]);
        
        var listItems = filteredItems.Value.Select(item => new ListItem(item));
        
        return Layout.Vertical().Gap(2)
            | searchTerm.ToSearchInput().Placeholder("Search fruits...")
            | new List(listItems);
    }
}

public class ExamplesListDemo : ViewBase
{
    public override object? Build()
    {
        // Custom Item Rendering Data
        var products = new[]
        {
            new { Name = "Laptop", Price = 999.99m, Stock = 15 },
            new { Name = "Mouse", Price = 29.99m, Stock = 50 }
        };
        
        var customItems = products.Select(product => new ListItem(product.Name)
            .Subtitle($"${product.Price} - {product.Stock} in stock")
            .Content(
                Layout.Horizontal().Gap(2)
                    | Text.Block($"${product.Price}")
                    | new Badge(product.Stock.ToString()).Variant(BadgeVariant.Secondary)
            )
        );

        // Time Rendering Data
        var timeItem = new ListItem("Task created")
            .Subtitle($"Created at {DateTime.Now:HH:mm:ss}");

        return Layout.Vertical().Gap(4)
            | Text.P("Custom Item Rendering").Large()
            | new List(customItems)
            | Text.P("Time Rendering").Large()
            | new List(new[] { timeItem });
    }
}
