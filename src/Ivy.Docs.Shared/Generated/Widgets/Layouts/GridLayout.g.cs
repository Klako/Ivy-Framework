using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Layouts;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/02_Layouts/03_GridLayout.md", searchHints: ["layout", "grid", "columns", "rows", "responsive", "arrangement"])]
public class GridLayoutApp(bool onlyBody = false) : ViewBase
{
    public GridLayoutApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("gridlayout", "GridLayout", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("grid-definition-properties", "Grid Definition Properties", 2), new ArticleHeading("columns-and-rows", "Columns and Rows", 3), new ArticleHeading("gap-and-padding", "Gap and Padding", 3), new ArticleHeading("auto-flow", "Auto Flow", 3), new ArticleHeading("child-positioning", "Child Positioning", 2), new ArticleHeading("grid-column-and-row", "Grid Column and Row", 3), new ArticleHeading("spanning-multiple-cells", "Spanning Multiple Cells", 3), new ArticleHeading("construction-patterns", "Construction Patterns", 2), new ArticleHeading("using-layoutgrid-recommended", "Using Layout.Grid() (Recommended)", 3), new ArticleHeading("using-gridlayout-directly", "Using GridLayout Directly", 3), new ArticleHeading("examples", "Examples", 2), new ArticleHeading("properties-reference", "Properties Reference", 2), new ArticleHeading("griddefinition-properties", "GridDefinition Properties", 3), new ArticleHeading("child-positioning-extensions", "Child Positioning Extensions", 3), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# GridLayout").OnLinkClick(onLinkClick)
            | Lead("Create responsive two-dimensional grid layouts with precise control over positioning, spacing, and spanning for complex [UI](app://onboarding/concepts/layout) arrangements.")
            | new Markdown(
                """"
                The `GridLayout` [widget](app://onboarding/concepts/widgets) arranges child elements in a two-dimensional grid system with precise control over positioning, spacing, and spanning. It provides both automatic flow and explicit positioning for flexible grid layouts.
                
                ## Basic Usage
                
                Here's a simple 2x2 grid layout:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Layout.Grid()
                    .Columns(2)
                    .Rows(2)
                    | Text.Block("Cell 1")
                    | Text.Block("Cell 2")
                    | Text.Block("Cell 3")
                    | Text.Block("Cell 4")
                """",Languages.Csharp)
            | new Box().Content(Layout.Grid()
    .Columns(2)
    .Rows(2)
    | new Card("Cell 1")
    | new Card("Cell 2")
    | new Card("Cell 3")
    | new Card("Cell 4"))
            | new Markdown(
                """"
                ## Grid Definition Properties
                
                ### Columns and Rows
                
                Define the number of columns and rows in your grid:
                """").OnLinkClick(onLinkClick)
            | new Box().Content(Layout.Grid()
    .Columns(3)
    .Rows(2)
    | new Card("1")
    | new Card("2")
    | new Card("3")
    | new Card("4")
    | new Card("5")
    | new Card("6"))
            | new Markdown(
                """"
                ### Gap and Padding
                
                Control spacing between grid items and around the grid:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | Text.Block("With Gap")
    | Layout.Grid()
        .Columns(3)
        .Gap(2)
        | new Card("A")
        | new Card("B")
        | new Card("C")
    | Text.Block("With Padding")
    | Layout.Grid()
        .Columns(3)
        .Padding(16)
        | new Card("A")
        | new Card("B")
        | new Card("C"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | Text.Block("With Gap")
                        | Layout.Grid()
                            .Columns(3)
                            .Gap(2)
                            | new Card("A")
                            | new Card("B")
                            | new Card("C")
                        | Text.Block("With Padding")
                        | Layout.Grid()
                            .Columns(3)
                            .Padding(16)
                            | new Card("A")
                            | new Card("B")
                            | new Card("C")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Auto Flow
                
                Control how items are automatically placed in the grid:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | Text.Block("Row (Default)")
    | Layout.Grid()
        .Columns(2)
        .AutoFlow(AutoFlow.Row)
        | new Card("1")
        | new Card("2")
        | new Card("3")
    | Text.Block("Column")
    | Layout.Grid()
        .Columns(2)
        .AutoFlow(AutoFlow.Column)
        | new Card("1")
        | new Card("2")
        | new Card("3"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | Text.Block("Row (Default)")
                        | Layout.Grid()
                            .Columns(2)
                            .AutoFlow(AutoFlow.Row)
                            | new Card("1")
                            | new Card("2")
                            | new Card("3")
                        | Text.Block("Column")
                        | Layout.Grid()
                            .Columns(2)
                            .AutoFlow(AutoFlow.Column)
                            | new Card("1")
                            | new Card("2")
                            | new Card("3")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Child Positioning
                
                ### Grid Column and Row
                
                Position children at specific grid coordinates:
                """").OnLinkClick(onLinkClick)
            | new Box().Content(Layout.Grid()
    .Columns(3)
    .Rows(3)
    | new Card("Top-Left").GridColumn(1).GridRow(1)
    | new Card("Center").GridColumn(2).GridRow(2)
    | new Card("Bottom-Right").GridColumn(3).GridRow(3))
            | new Markdown(
                """"
                ### Spanning Multiple Cells
                
                Make children span across multiple columns or rows:
                """").OnLinkClick(onLinkClick)
            | new Box().Content(Layout.Grid()
    .Columns(3)
    .Rows(3)
    | new Card("Header").GridColumn(1).GridRow(1).GridColumnSpan(3)
    | new Card("Sidebar").GridColumn(1).GridRow(2).GridRowSpan(2)
    | new Card("Main").GridColumn(2).GridRow(2).GridColumnSpan(2)
    | new Card("Footer").GridColumn(2).GridRow(3).GridColumnSpan(2))
            | new Markdown(
                """"
                ## Construction Patterns
                
                ### Using Layout.Grid() (Recommended)
                
                The fluent API provides a clean way to build grids:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Layout.Grid()
                    .Columns(2)
                    .Gap(4)
                    .Padding(8)
                    | content1
                    | content2
                    | content3
                """",Languages.Csharp)
            | new Markdown(
                """"
                ### Using GridLayout Directly
                
                For more control, you can use the GridLayout class directly:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new GridLayout(
                    new GridDefinition
                    {
                        Columns = 2,
                        Rows = 2,
                        Gap = 4,
                        Padding = 8,
                        AutoFlow = AutoFlow.Row
                    },
                    content1,
                    content2,
                    content3
                )
                """",Languages.Csharp)
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Responsive Card Grid",
                Vertical().Gap(4)
                | new Box().Content(Layout.Grid()
    .Columns(3)
    .Gap(6)
    | new Card(
        new Button("Action 1", _ => client.Toast("Card 1 clicked"))
      ).GridColumnSpan(2)
    | new Card(
        new Button("Action 2", _ => client.Toast("Card 2 clicked"))
      )
    | new Card(
        new Button("Action 3", _ => client.Toast("Card 3 clicked"))
      )
    | new Card(
        new Button("Action 4", _ => client.Toast("Card 4 clicked"))
      ).GridColumnSpan(2))
            )
            | new Expandable("Dashboard Layout",
                Vertical().Gap(4)
                | new Box().Content(Layout.Grid()
    .Columns(4)
    .Rows(3)
    .Gap(4)
    | new Card("Header").GridColumn(1).GridRow(1).GridColumnSpan(4)
    | new Card("Nav").GridColumn(1).GridRow(2).GridRowSpan(2)
    | new Card("Main Content").GridColumn(2).GridRow(2).GridColumnSpan(2).GridRowSpan(2)
    | new Card("Sidebar").GridColumn(4).GridRow(2).GridRowSpan(2))
            )
            | new Markdown(
                """"
                The `AutoFlow` enum provides different ways to automatically place grid items:
                
                - **Row**: Fill each row before moving to the next (default)
                - **Column**: Fill each column before moving to the next
                - **RowDense**: Fill rows, but try to fill gaps with later items
                - **ColumnDense**: Fill columns, but try to fill gaps with later items
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | Text.Block("Row Dense")
    | Layout.Grid()
        .Columns(3)
        .AutoFlow(AutoFlow.RowDense)
        | new Card("Wide Item").GridColumnSpan(2)
        | new Card("1")
        | new Card("2")
        | new Card("3")
    | Text.Block("Column Dense")
    | Layout.Grid()
        .Columns(3)
        .AutoFlow(AutoFlow.ColumnDense)
        | new Card("Tall Item").GridRowSpan(2)
        | new Card("1")
        | new Card("2")
        | new Card("3"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | Text.Block("Row Dense")
                        | Layout.Grid()
                            .Columns(3)
                            .AutoFlow(AutoFlow.RowDense)
                            | new Card("Wide Item").GridColumnSpan(2)
                            | new Card("1")
                            | new Card("2")
                            | new Card("3")
                        | Text.Block("Column Dense")
                        | Layout.Grid()
                            .Columns(3)
                            .AutoFlow(AutoFlow.ColumnDense)
                            | new Card("Tall Item").GridRowSpan(2)
                            | new Card("1")
                            | new Card("2")
                            | new Card("3")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Properties Reference
                
                ### GridDefinition Properties
                
                | Property | Type | Default | Description |
                |----------|------|---------|-------------|
                | Columns | int? | null | Number of columns in the grid |
                | Rows | int? | null | Number of rows in the grid |
                | Gap | int | 4 | Space between grid items |
                | Padding | int | 0 | Padding around the grid |
                | AutoFlow | AutoFlow? | null | How items are automatically placed |
                | Width | [Size](app://api-reference/ivy/size)? | null | Grid container width |
                | Height | [Size](app://api-reference/ivy/size)? | null | Grid container height |
                
                ### Child Positioning Extensions
                
                | Extension | Description |
                |-----------|-------------|
                | GridColumn(int) | Position child at specific column |
                | GridRow(int) | Position child at specific row |
                | GridColumnSpan(int) | Span child across multiple columns |
                | GridRowSpan(int) | Span child across multiple rows |
                """").OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.GridLayout", "Ivy.GridExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Layouts/GridLayout.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.LayoutApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.SizeApp)]; 
        return article;
    }
}

