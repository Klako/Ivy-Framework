using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:16, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/16_Tree.md", searchHints: ["tree", "hierarchy", "folder", "file", "directory", "structure", "nested"])]
public class TreeApp(bool onlyBody = false) : ViewBase
{
    public TreeApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("tree", "Tree", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("click-events", "Click Events", 2), new ArticleHeading("row-actions", "Row Actions", 2), new ArticleHeading("disabled-items", "Disabled Items", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Tree").OnLinkClick(onLinkClick)
            | Lead("Display hierarchical data structures like file trees, nested categories, and organizational charts with collapsible nodes.")
            | new Markdown(
                """"
                The `Tree` [widget](app://onboarding/concepts/widgets) renders recursive data in a familiar tree view using `MenuItem` for each node. Each `MenuItem` can contain nested children, supports icons, click events, and expand/collapse behavior.
                
                ## Basic Usage
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Tree(
                        new MenuItem("src")
                            .Icon(Icons.Folder)
                            .Expanded()
                            .Children(
                                new MenuItem("components")
                                    .Icon(Icons.Folder)
                                    .Expanded()
                                    .Children(
                                        new MenuItem("Button.tsx").Icon(Icons.Code),
                                        new MenuItem("Card.tsx").Icon(Icons.Code)
                                    ),
                                new MenuItem("App.tsx").Icon(Icons.Code)
                            )
                    )
                    """",Languages.Csharp)
                | new Box().Content(new Tree(
    new MenuItem("src")
        .Icon(Icons.Folder)
        .Expanded()
        .Children(
            new MenuItem("components")
                .Icon(Icons.Folder)
                .Expanded()
                .Children(
                    new MenuItem("Button.tsx").Icon(Icons.Code),
                    new MenuItem("Card.tsx").Icon(Icons.Code)
                ),
            new MenuItem("App.tsx").Icon(Icons.Code)
        )
))
            )
            | new Markdown(
                """"
                ## Click Events
                
                Use `OnSelect` on the Tree and `Tag` on each MenuItem to handle clicks. The `DefaultSelectHandler` routes events to individual `MenuItem.OnSelect` handlers.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TreeClickDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TreeClickDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var selected = UseState("");
                    
                            return Layout.Vertical().Gap(2)
                                | Text.Block($"Selected: {(string.IsNullOrEmpty(selected.Value) ? "nothing" : selected.Value)}")
                                | new Tree(
                                    new MenuItem("src")
                                        .Icon(Icons.Folder)
                                        .Expanded()
                                        .Children(
                                            new MenuItem("App.tsx").Icon(Icons.Code).Tag("App.tsx"),
                                            new MenuItem("index.ts").Icon(Icons.Code).Tag("index.ts")
                                        )
                                ).OnSelect(e => selected.Set(e.Value?.ToString() ?? ""));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Row Actions
                
                **Row actions** can be defined as a set of actions (buttons or menu items) displayed for each tree row. Configure them via `.RowActions()` with one or more `MenuItem`s, and subscribe to clicks with `.OnRowAction()`.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TreeRowActionsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TreeRowActionsDemo : ViewBase
                    {
                        private enum RowAction { Edit, More, Duplicate, Delete }
                    
                        public override object? Build()
                        {
                            var lastAction = UseState<string>("None");
                    
                            return Layout.Vertical()
                                | Text.Block($"  Last Action: {lastAction.Value}")
                                | new Tree(
                                    new MenuItem("src")
                                        .Icon(Icons.Folder)
                                        .Expanded()
                                        .Children(
                                            new MenuItem("components")
                                                .Icon(Icons.Folder)
                                                .Expanded()
                                                .Children(
                                                    new MenuItem("Button.tsx").Icon(Icons.Code).Tag("Button.tsx"),
                                                    new MenuItem("Card.tsx").Icon(Icons.Code).Tag("Card.tsx")
                                                ),
                                            new MenuItem("App.tsx").Icon(Icons.Code).Tag("App.tsx")
                                        ),
                                    new MenuItem("package.json").Icon(Icons.Braces).Tag("package.json")
                                )
                                .RowActions(
                                    MenuItem.Default(Icons.Pencil).Tag(RowAction.Edit).Label("Edit"),
                                    MenuItem.Default(Icons.Ellipsis).Tag(RowAction.More).Label("More").Children(
                                        MenuItem.Default(Icons.Copy).Tag(RowAction.Duplicate).Label("Duplicate"),
                                        MenuItem.Default(Icons.Trash).Tag(RowAction.Delete).Label("Delete")
                                    )
                                )
                                .OnRowAction(e => {
                                    var tagStr = e.Value.ActionTag?.ToString();
                                    if (Enum.TryParse<RowAction>(tagStr, ignoreCase: true, out var action))
                                        lastAction.Set($"{action} on {e.Value.ItemValue}");
                                    else if (tagStr != null)
                                        lastAction.Set($"{tagStr} on {e.Value.ItemValue}");
                                });
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Disabled Items
                
                Individual menu items can be disabled to prevent interaction.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Tree(
    new MenuItem("Available")
        .Icon(Icons.Folder)
        .Expanded()
        .Children(
            new MenuItem("editable.txt").Icon(Icons.FileText).Tag("editable"),
            new MenuItem("read-only.txt").Icon(Icons.Lock).Disabled()
        ),
    new MenuItem("Restricted")
        .Icon(Icons.FolderLock)
        .Disabled()
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Tree(
                        new MenuItem("Available")
                            .Icon(Icons.Folder)
                            .Expanded()
                            .Children(
                                new MenuItem("editable.txt").Icon(Icons.FileText).Tag("editable"),
                                new MenuItem("read-only.txt").Icon(Icons.Lock).Disabled()
                            ),
                        new MenuItem("Restricted")
                            .Icon(Icons.FolderLock)
                            .Disabled()
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Tree", "Ivy.TreeExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Tree/Tree.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class TreeClickDemo : ViewBase
{
    public override object? Build()
    {
        var selected = UseState("");

        return Layout.Vertical().Gap(2)
            | Text.Block($"Selected: {(string.IsNullOrEmpty(selected.Value) ? "nothing" : selected.Value)}")
            | new Tree(
                new MenuItem("src")
                    .Icon(Icons.Folder)
                    .Expanded()
                    .Children(
                        new MenuItem("App.tsx").Icon(Icons.Code).Tag("App.tsx"),
                        new MenuItem("index.ts").Icon(Icons.Code).Tag("index.ts")
                    )
            ).OnSelect(e => selected.Set(e.Value?.ToString() ?? ""));
    }
}

public class TreeRowActionsDemo : ViewBase
{
    private enum RowAction { Edit, More, Duplicate, Delete }

    public override object? Build()
    {
        var lastAction = UseState<string>("None");

        return Layout.Vertical()
            | Text.Block($"  Last Action: {lastAction.Value}")
            | new Tree(
                new MenuItem("src")
                    .Icon(Icons.Folder)
                    .Expanded()
                    .Children(
                        new MenuItem("components")
                            .Icon(Icons.Folder)
                            .Expanded()
                            .Children(
                                new MenuItem("Button.tsx").Icon(Icons.Code).Tag("Button.tsx"),
                                new MenuItem("Card.tsx").Icon(Icons.Code).Tag("Card.tsx")
                            ),
                        new MenuItem("App.tsx").Icon(Icons.Code).Tag("App.tsx")
                    ),
                new MenuItem("package.json").Icon(Icons.Braces).Tag("package.json")
            )
            .RowActions(
                MenuItem.Default(Icons.Pencil).Tag(RowAction.Edit).Label("Edit"),
                MenuItem.Default(Icons.Ellipsis).Tag(RowAction.More).Label("More").Children(
                    MenuItem.Default(Icons.Copy).Tag(RowAction.Duplicate).Label("Duplicate"),
                    MenuItem.Default(Icons.Trash).Tag(RowAction.Delete).Label("Delete")
                )
            )
            .OnRowAction(e => {
                var tagStr = e.Value.ActionTag?.ToString();
                if (Enum.TryParse<RowAction>(tagStr, ignoreCase: true, out var action))
                    lastAction.Set($"{action} on {e.Value.ItemValue}");
                else if (tagStr != null)
                    lastAction.Set($"{tagStr} on {e.Value.ItemValue}");
            });
    }
}
