using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Advanced;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/07_Advanced/02_Sheet.md", searchHints: ["sidebar", "drawer", "panel", "slide-out", "modal", "overlay"])]
public class SheetApp(bool onlyBody = false) : ViewBase
{
    public SheetApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("sheet", "Sheet", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("custom-content", "Custom Content", 2), new ArticleHeading("footer-actions", "Footer Actions", 2), new ArticleHeading("different-widths", "Different Widths", 2), new ArticleHeading("different-sides", "Different Sides", 2), new ArticleHeading("opening-from-a-button", "Opening from a Button", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Sheet").OnLinkClick(onLinkClick)
            | Lead("Sheets slide in from the side of the screen and display additional content while allowing the user to dismiss them. They provide a non-intrusive way to show additional information or [forms](app://onboarding/concepts/forms) without navigating away from the current page.")
            | new Markdown(
                """"
                ## Basic Usage
                
                Use [UseTrigger](app://hooks/core/use-trigger) to open a sheet. When open, render a `Sheet` with an `onClose` action that calls `isOpen.Set(false)`:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicSheetExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen) =>
                                isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                                    new Card(
                                        "Welcome to the sheet!",
                                        "This is the content inside the sheet"
                                    ),
                                    title: "This is a sheet",
                                    description: "Lorem ipsum dolor sit amet")
                                    .Width(Size.Fraction(1/2f)) : null);
                    
                            return Layout.Vertical()
                                | new Button("Open Sheet", onClick: _ => showSheet())
                                | sheetView;
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicSheetExample())
            )
            | new Markdown(
                """"
                ## Custom Content
                
                You can build sheet content with a [Fragment](app://widgets/primitives/fragment), [Card](app://widgets/common/card), and any [widgets](app://onboarding/concepts/widgets). The following uses a card with a title, description, and an action button:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicSheetWithContent())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicSheetWithContent : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen) =>
                                isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                                    new Fragment(
                                        new Card(
                                            "Welcome to the sheet!",
                                            new Button("Action Button", onClick: _ => client.Toast("Button clicked!"))
                                        ).Title("Sheet Content").Description("This is a simple sheet with custom content")
                                    ),
                                    title: "Basic Sheet",
                                    description: "A simple example of sheet usage")
                                    .Width(Size.Fraction(1/3f)) : null);
                    
                            return Layout.Vertical()
                                | new Button("Open Basic Sheet", onClick: _ => showSheet())
                                | sheetView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Footer Actions
                
                Use [FooterLayout](app://widgets/layouts/footer-layout) to place action buttons in the sheet footer:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SheetWithFooterActions())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SheetWithFooterActions : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen) =>
                                isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                                    new FooterLayout(
                                        Layout.Horizontal().Gap(2)
                                            | new Button("Save").Variant(ButtonVariant.Primary).OnClick(_ => client.Toast("Profile saved successfully!").Success())
                                            | new Button("Cancel").Variant(ButtonVariant.Outline).OnClick(_ => isOpen.Set(false)),
                                        new Card(
                                            "This sheet has action buttons in the footer"
                                        ).Title("Content")
                                    ),
                                    title: "Actions Sheet")
                                    .Width(Size.Fraction(1/2f)) : null);
                    
                            return Layout.Vertical()
                                | new Button("Open Sheet with Actions", onClick: _ => showSheet())
                                | sheetView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Different Widths
                
                Control sheet width with the `.Width()` extension. For top/bottom sides, use `.Height()` instead. Widths use [Size](app://api-reference/ivy/size) values such as `Size.Rem(20)`, `Size.Fraction(1/2f)`, and `Size.Full()`. Use `UseTrigger<Size>` so each button opens a sheet with a different width:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SheetWidthExamples())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SheetWidthExamples : ViewBase
                    {
                        public override object? Build()
                        {
                            var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen, Size width) =>
                                isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                                    new Card("This sheet uses a custom width.").Title("Width Example"),
                                    title: "Custom Width")
                                    .Width(width) : null);
                    
                            return Layout.Horizontal().Gap(2)
                                | new Button("Rem(20)", onClick: _ => showSheet(Size.Rem(20)))
                                | new Button("Half", onClick: _ => showSheet(Size.Fraction(1/2f)))
                                | new Button("Two thirds", onClick: _ => showSheet(Size.Fraction(2/3f)))
                                | sheetView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Different Sides
                
                Sheets can slide in from any edge using the `.Side()` extension with `SheetSide`: `Left`, `Right` (default), `Top`, or `Bottom`. For top/bottom, the size parameter controls height instead of width. Use `UseTrigger<SheetSide>` when the trigger needs to know which side to open:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SheetSideExamples())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SheetSideExamples : ViewBase
                    {
                        public override object? Build()
                        {
                            var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen, SheetSide side) =>
                            {
                                if (!isOpen.Value) return null;
                                var sheet = new Sheet(_ => isOpen.Set(false),
                                    new Card($"This sheet slides in from the {side}.").Title($"{side} Sheet"),
                                    title: $"{side} Sheet")
                                    .Side(side);
                                return side is SheetSide.Top or SheetSide.Bottom
                                    ? sheet.Height(Size.Rem(20))
                                    : sheet.Width(Size.Fraction(1/3f));
                            });
                    
                            return Layout.Horizontal().Gap(2)
                                | new Button("Left Sheet", onClick: _ => showSheet(SheetSide.Left))
                                | new Button("Right Sheet", onClick: _ => showSheet(SheetSide.Right))
                                | new Button("Top Sheet", onClick: _ => showSheet(SheetSide.Top))
                                | new Button("Bottom Sheet", onClick: _ => showSheet(SheetSide.Bottom))
                                | sheetView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Opening from a Button
                
                When the only trigger is a single button, you can use the `WithSheet` extension on [Button](app://widgets/common/button) instead of wiring `UseTrigger` yourself. It creates the open state and sheet for you:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ButtonWithSheetExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ButtonWithSheetExample : ViewBase
                    {
                        public override object? Build()
                        {
                            return new Button("Open Sheet").WithSheet(
                                () => new SheetView(),
                                title: "This is a sheet",
                                description: "Lorem ipsum dolor sit amet",
                                width: Size.Fraction(1/2f)
                            );
                        }
                    }
                    
                    public class SheetView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new Card(
                                "Welcome to the sheet!",
                                "This is the content inside the sheet"
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Sheet", "Ivy.SheetExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Sheet.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Sheet with Navigation",
                Vertical().Gap(4)
                | new Markdown("You can keep internal navigation state inside the sheet (e.g. tabs or wizard steps) using [UseState](app://hooks/core/use-state):").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new NavigationSheet())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class NavigationSheet : ViewBase
                        {
                            public override object? Build()
                            {
                                var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen) =>
                                    isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                                        new NavigationSheetContent(),
                                        title: "Navigation Sheet")
                                        .Width(Size.Fraction(1/2f)) : null);
                        
                                return Layout.Vertical()
                                    | new Button("Open Navigation Sheet", onClick: _ => showSheet())
                                    | sheetView;
                            }
                        }
                        
                        public class NavigationSheetContent : ViewBase
                        {
                            public override object? Build()
                            {
                                var currentPage = UseState(0);
                                var pages = new[] { "Home", "Profile", "Settings", "Help" };
                        
                                return Layout.Vertical()
                                    | (Layout.Horizontal().Gap(2)
                                        | pages.Select((page, index) =>
                                            new Button(page)
                                                .Variant(currentPage.Value == index ? ButtonVariant.Primary : ButtonVariant.Outline)
                                                .OnClick(_ => currentPage.Set(index))
                                        ).ToArray())
                                    | new Card(
                                        $"This is the {pages[currentPage.Value]} page content"
                                    ).Title("Page Content");
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Complex Layout with Kanban",
                Vertical().Gap(4)
                | new Markdown("This pattern uses `UseTrigger` with a parameter so that clicking a card opens an edit sheet for that item. The trigger callback receives the task id; the sheet content reads it and updates shared state:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new KanbanWithSheetExample())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public record TaskItem(string Id, string Title, string Status, int Priority, string Description);
                        
                        public class KanbanWithSheetExample : ViewBase
                        {
                            public override object? Build()
                            {
                                var tasks = UseState(new[]
                                {
                                    new TaskItem("1", "Design Homepage", "Todo", 1, "Create wireframes and mockups"),
                                    new TaskItem("2", "Setup Database", "Todo", 2, "Configure PostgreSQL instance"),
                                    new TaskItem("3", "Code Review", "In Progress", 1, "Review pull requests"),
                                    new TaskItem("4", "Performance Optimization", "In Progress", 2, "Optimize database queries"),
                                    new TaskItem("5", "Unit Tests", "Done", 1, "Write comprehensive test suite"),
                                });
                        
                                var client = UseService<IClientProvider>();
                        
                                var (sheetView, showEdit) = UseTrigger((IState<bool> isOpen, string taskId) =>
                                    new TaskFormSheet(isOpen, taskId, tasks, client));
                        
                                var kanban = tasks.Value
                                    .ToKanban(
                                        groupBySelector: t => t.Status,
                                        idSelector: t => t.Id,
                                        orderSelector: t => t.Priority)
                                    .CardBuilder(task => new Card(task.Title, task.Description)
                                        .OnClick(() => showEdit(task.Id)))
                                    .OnMove(moveData =>
                                    {
                                        var taskId = moveData.CardId?.ToString();
                                        if (string.IsNullOrEmpty(taskId)) return;
                        
                                        var updatedTasks = tasks.Value.ToList();
                                        var taskToMove = updatedTasks.FirstOrDefault(t => t.Id == taskId);
                                        if (taskToMove == null) return;
                        
                                        var updated = taskToMove with { Status = moveData.ToColumn };
                                        updatedTasks.RemoveAll(t => t.Id == taskId);
                        
                                        int insertIndex = updatedTasks.Count;
                        
                                        var taskAtTargetIndex = updatedTasks
                                            .Where(t => t.Status == moveData.ToColumn)
                                            .ElementAtOrDefault(moveData.TargetIndex ?? -1);
                        
                                        if (taskAtTargetIndex != null)
                                        {
                                            insertIndex = updatedTasks.IndexOf(taskAtTargetIndex);
                                        }
                                        else
                                        {
                                            var lastTaskInColumn = updatedTasks.LastOrDefault(t => t.Status == moveData.ToColumn);
                                            if (lastTaskInColumn != null)
                                            {
                                                insertIndex = updatedTasks.IndexOf(lastTaskInColumn) + 1;
                                            }
                                        }
                        
                                        updatedTasks.Insert(insertIndex, updated);
                                        tasks.Set(updatedTasks.ToArray());
                                    });
                        
                                return new Fragment()
                                    | kanban
                                    | sheetView;
                            }
                        }
                        
                        public class TaskFormSheet : ViewBase
                        {
                            private readonly IState<bool> _isOpen;
                            private readonly string _taskId;
                            private readonly IState<TaskItem[]> _tasks;
                            private readonly IClientProvider _client;
                        
                            public TaskFormSheet(IState<bool> isOpen, string taskId, IState<TaskItem[]> tasks, IClientProvider client)
                            {
                                _isOpen = isOpen;
                                _taskId = taskId;
                                _tasks = tasks;
                                _client = client;
                            }
                        
                            public override object? Build()
                            {
                                var task = UseState(() => _tasks.Value.FirstOrDefault(t => t.Id == _taskId) ??
                                    new TaskItem(_taskId, "", "Todo", 1, ""));
                        
                                var (onSubmit, formView, validationView, loading) = UseForm(() => task.ToForm()
                                    .Required(m => m.Title, m => m.Description)
                                    .Builder(m => m.Status, s => s.ToSelectInput(new[] { "Todo", "In Progress", "Done" }.ToOptions()))
                                    .Builder(m => m.Description, s => s.ToTextareaInput())
                                    .Remove(m => m.Id));
                        
                                async ValueTask HandleSubmit()
                                {
                                    if (await onSubmit())
                                    {
                                        var updatedTasks = _tasks.Value.ToList();
                                        var index = updatedTasks.FindIndex(t => t.Id == _taskId);
                                        if (index >= 0)
                                        {
                                            updatedTasks[index] = task.Value;
                                        }
                                        _tasks.Set(updatedTasks.ToArray());
                                        _client.Toast($"Updated: {task.Value.Title}").Success();
                                        _isOpen.Set(false);
                                    }
                                }
                        
                                var layout = new FooterLayout(
                                    Layout.Horizontal().Gap(2)
                                        | new Button("Save").OnClick(_ => HandleSubmit())
                                            .Loading(loading).Disabled(loading)
                                        | new Button("Cancel").Variant(ButtonVariant.Outline).OnClick(_ => _isOpen.Set(false))
                                        | validationView,
                                    formView
                                );
                        
                                return new Sheet(_ => _isOpen.Set(false), layout,
                                    title: "Edit Task",
                                    description: "Update task details")
                                    .Width(Size.Fraction(1/3f));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp), typeof(Hooks.Core.UseTriggerApp), typeof(Widgets.Primitives.FragmentApp), typeof(Widgets.Common.CardApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Widgets.Layouts.FooterLayoutApp), typeof(ApiReference.Ivy.SizeApp), typeof(Widgets.Common.ButtonApp)]; 
        return article;
    }
}


public class BasicSheetExample : ViewBase
{
    public override object? Build()
    {
        var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                new Card(
                    "Welcome to the sheet!",
                    "This is the content inside the sheet"
                ),
                title: "This is a sheet",
                description: "Lorem ipsum dolor sit amet")
                .Width(Size.Fraction(1/2f)) : null);

        return Layout.Vertical()
            | new Button("Open Sheet", onClick: _ => showSheet())
            | sheetView;
    }
}

public class BasicSheetWithContent : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                new Fragment(
                    new Card(
                        "Welcome to the sheet!",
                        new Button("Action Button", onClick: _ => client.Toast("Button clicked!"))
                    ).Title("Sheet Content").Description("This is a simple sheet with custom content")
                ),
                title: "Basic Sheet",
                description: "A simple example of sheet usage")
                .Width(Size.Fraction(1/3f)) : null);

        return Layout.Vertical()
            | new Button("Open Basic Sheet", onClick: _ => showSheet())
            | sheetView;
    }
}

public class SheetWithFooterActions : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                new FooterLayout(
                    Layout.Horizontal().Gap(2)
                        | new Button("Save").Variant(ButtonVariant.Primary).OnClick(_ => client.Toast("Profile saved successfully!").Success())
                        | new Button("Cancel").Variant(ButtonVariant.Outline).OnClick(_ => isOpen.Set(false)),
                    new Card(
                        "This sheet has action buttons in the footer"
                    ).Title("Content")
                ),
                title: "Actions Sheet")
                .Width(Size.Fraction(1/2f)) : null);

        return Layout.Vertical()
            | new Button("Open Sheet with Actions", onClick: _ => showSheet())
            | sheetView;
    }
}

public class SheetWidthExamples : ViewBase
{
    public override object? Build()
    {
        var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen, Size width) =>
            isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                new Card("This sheet uses a custom width.").Title("Width Example"),
                title: "Custom Width")
                .Width(width) : null);

        return Layout.Horizontal().Gap(2)
            | new Button("Rem(20)", onClick: _ => showSheet(Size.Rem(20)))
            | new Button("Half", onClick: _ => showSheet(Size.Fraction(1/2f)))
            | new Button("Two thirds", onClick: _ => showSheet(Size.Fraction(2/3f)))
            | sheetView;
    }
}

public class SheetSideExamples : ViewBase
{
    public override object? Build()
    {
        var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen, SheetSide side) =>
        {
            if (!isOpen.Value) return null;
            var sheet = new Sheet(_ => isOpen.Set(false),
                new Card($"This sheet slides in from the {side}.").Title($"{side} Sheet"),
                title: $"{side} Sheet")
                .Side(side);
            return side is SheetSide.Top or SheetSide.Bottom
                ? sheet.Height(Size.Rem(20))
                : sheet.Width(Size.Fraction(1/3f));
        });

        return Layout.Horizontal().Gap(2)
            | new Button("Left Sheet", onClick: _ => showSheet(SheetSide.Left))
            | new Button("Right Sheet", onClick: _ => showSheet(SheetSide.Right))
            | new Button("Top Sheet", onClick: _ => showSheet(SheetSide.Top))
            | new Button("Bottom Sheet", onClick: _ => showSheet(SheetSide.Bottom))
            | sheetView;
    }
}

public class ButtonWithSheetExample : ViewBase
{
    public override object? Build()
    {
        return new Button("Open Sheet").WithSheet(
            () => new SheetView(),
            title: "This is a sheet",
            description: "Lorem ipsum dolor sit amet",
            width: Size.Fraction(1/2f)
        );
    }
}

public class SheetView : ViewBase
{
    public override object? Build()
    {
        return new Card(
            "Welcome to the sheet!",
            "This is the content inside the sheet"
        );
    }
}

public class NavigationSheet : ViewBase
{
    public override object? Build()
    {
        var (sheetView, showSheet) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value ? new Sheet(_ => isOpen.Set(false),
                new NavigationSheetContent(),
                title: "Navigation Sheet")
                .Width(Size.Fraction(1/2f)) : null);

        return Layout.Vertical()
            | new Button("Open Navigation Sheet", onClick: _ => showSheet())
            | sheetView;
    }
}

public class NavigationSheetContent : ViewBase
{
    public override object? Build()
    {
        var currentPage = UseState(0);
        var pages = new[] { "Home", "Profile", "Settings", "Help" };

        return Layout.Vertical()
            | (Layout.Horizontal().Gap(2)
                | pages.Select((page, index) =>
                    new Button(page)
                        .Variant(currentPage.Value == index ? ButtonVariant.Primary : ButtonVariant.Outline)
                        .OnClick(_ => currentPage.Set(index))
                ).ToArray())
            | new Card(
                $"This is the {pages[currentPage.Value]} page content"
            ).Title("Page Content");
    }
}

public record TaskItem(string Id, string Title, string Status, int Priority, string Description);

public class KanbanWithSheetExample : ViewBase
{
    public override object? Build()
    {
        var tasks = UseState(new[]
        {
            new TaskItem("1", "Design Homepage", "Todo", 1, "Create wireframes and mockups"),
            new TaskItem("2", "Setup Database", "Todo", 2, "Configure PostgreSQL instance"),
            new TaskItem("3", "Code Review", "In Progress", 1, "Review pull requests"),
            new TaskItem("4", "Performance Optimization", "In Progress", 2, "Optimize database queries"),
            new TaskItem("5", "Unit Tests", "Done", 1, "Write comprehensive test suite"),
        });

        var client = UseService<IClientProvider>();

        var (sheetView, showEdit) = UseTrigger((IState<bool> isOpen, string taskId) =>
            new TaskFormSheet(isOpen, taskId, tasks, client));

        var kanban = tasks.Value
            .ToKanban(
                groupBySelector: t => t.Status,
                idSelector: t => t.Id,
                orderSelector: t => t.Priority)
            .CardBuilder(task => new Card(task.Title, task.Description)
                .OnClick(() => showEdit(task.Id)))
            .OnMove(moveData =>
            {
                var taskId = moveData.CardId?.ToString();
                if (string.IsNullOrEmpty(taskId)) return;

                var updatedTasks = tasks.Value.ToList();
                var taskToMove = updatedTasks.FirstOrDefault(t => t.Id == taskId);
                if (taskToMove == null) return;

                var updated = taskToMove with { Status = moveData.ToColumn };
                updatedTasks.RemoveAll(t => t.Id == taskId);

                int insertIndex = updatedTasks.Count;

                var taskAtTargetIndex = updatedTasks
                    .Where(t => t.Status == moveData.ToColumn)
                    .ElementAtOrDefault(moveData.TargetIndex ?? -1);

                if (taskAtTargetIndex != null)
                {
                    insertIndex = updatedTasks.IndexOf(taskAtTargetIndex);
                }
                else
                {
                    var lastTaskInColumn = updatedTasks.LastOrDefault(t => t.Status == moveData.ToColumn);
                    if (lastTaskInColumn != null)
                    {
                        insertIndex = updatedTasks.IndexOf(lastTaskInColumn) + 1;
                    }
                }

                updatedTasks.Insert(insertIndex, updated);
                tasks.Set(updatedTasks.ToArray());
            });

        return new Fragment()
            | kanban
            | sheetView;
    }
}

public class TaskFormSheet : ViewBase
{
    private readonly IState<bool> _isOpen;
    private readonly string _taskId;
    private readonly IState<TaskItem[]> _tasks;
    private readonly IClientProvider _client;

    public TaskFormSheet(IState<bool> isOpen, string taskId, IState<TaskItem[]> tasks, IClientProvider client)
    {
        _isOpen = isOpen;
        _taskId = taskId;
        _tasks = tasks;
        _client = client;
    }

    public override object? Build()
    {
        var task = UseState(() => _tasks.Value.FirstOrDefault(t => t.Id == _taskId) ??
            new TaskItem(_taskId, "", "Todo", 1, ""));

        var (onSubmit, formView, validationView, loading) = UseForm(() => task.ToForm()
            .Required(m => m.Title, m => m.Description)
            .Builder(m => m.Status, s => s.ToSelectInput(new[] { "Todo", "In Progress", "Done" }.ToOptions()))
            .Builder(m => m.Description, s => s.ToTextareaInput())
            .Remove(m => m.Id));

        async ValueTask HandleSubmit()
        {
            if (await onSubmit())
            {
                var updatedTasks = _tasks.Value.ToList();
                var index = updatedTasks.FindIndex(t => t.Id == _taskId);
                if (index >= 0)
                {
                    updatedTasks[index] = task.Value;
                }
                _tasks.Set(updatedTasks.ToArray());
                _client.Toast($"Updated: {task.Value.Title}").Success();
                _isOpen.Set(false);
            }
        }

        var layout = new FooterLayout(
            Layout.Horizontal().Gap(2)
                | new Button("Save").OnClick(_ => HandleSubmit())
                    .Loading(loading).Disabled(loading)
                | new Button("Cancel").Variant(ButtonVariant.Outline).OnClick(_ => _isOpen.Set(false))
                | validationView,
            formView
        );

        return new Sheet(_ => _isOpen.Set(false), layout,
            title: "Edit Task",
            description: "Update task details")
            .Width(Size.Fraction(1/3f));
    }
}
