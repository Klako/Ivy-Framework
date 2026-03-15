using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/03_Widgets.md", searchHints: ["components", "ui", "building-blocks", "elements", "widgets", "primitives"])]
public class WidgetsApp(bool onlyBody = false) : ViewBase
{
    public WidgetsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("widgets", "Widgets", 1), new ArticleHeading("basic-usage", "Basic usage", 2), new ArticleHeading("widget-library", "Widget Library", 3), new ArticleHeading("common-widgets", "Common Widgets", 3), new ArticleHeading("input-widgets", "Input Widgets", 3), new ArticleHeading("primitives", "Primitives", 3), new ArticleHeading("layouts", "Layouts", 3), new ArticleHeading("charts", "Charts", 3), new ArticleHeading("effects", "Effects", 3), new ArticleHeading("advanced", "Advanced", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Widgets").OnLinkClick(onLinkClick)
            | Lead("Discover the fundamental [building blocks](app://onboarding/concepts/views) of Ivy [applications](app://onboarding/concepts/apps) - Widgets provide declarative UI components inspired by React's component mode.")
            | new Markdown(
                """"
                Widgets are the fundamental building blocks of the Ivy framework. They represent the smallest unit of UI and are used to construct [Views](app://onboarding/concepts/views).
                
                ## Basic usage
                
                Ivy provides a comprehensive set of widgets organized into several [categories](app://widgets/_index):
                
                The most frequently used widgets for building user interfaces:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(2)
    | new Badge("Primary")
    | new Badge("New")
    | new Button("Primary Button")
    | new Progress(75)
    | new Card("Card Content"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(2)
                        | new Badge("Primary")
                        | new Badge("New")
                        | new Button("Primary Button")
                        | new Progress(75)
                        | new Card("Card Content")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Widget Library
                
                Ivy ships with a comprehensive set of strongly-typed widgets:
                
                | Category | Examples |
                |----------|----------|
                | Common | [Button](app://widgets/common/button), [Badge](app://widgets/common/badge), [Tooltip](app://widgets/common/tooltip), [Card](app://widgets/common/card), [Details](app://widgets/common/details), [Expandable](app://widgets/common/expandable), [List](app://widgets/common/list), [Table](app://widgets/common/table), [Pagination](app://widgets/common/pagination), [Progress](app://widgets/common/progress), [DropDownMenu](app://widgets/common/drop-down-menu), [Blades](app://widgets/common/blades), [MetricView](app://widgets/common/metric-view), [Terminal](app://widgets/common/terminal), [Dialog](app://widgets/common/dialog), [Tree](app://widgets/common/tree) |
                | Inputs | [Field](app://widgets/inputs/field), [TextInput](app://widgets/inputs/text-input), [NumberInput](app://widgets/inputs/number-input), [BoolInput](app://widgets/inputs/bool-input), [SelectInput](app://widgets/inputs/select-input), [AsyncSelectInput](app://widgets/inputs/async-select-input), [DateTimeInput](app://widgets/inputs/date-time-input), [DateRangeInput](app://widgets/inputs/date-range-input), [ColorInput](app://widgets/inputs/color-input), [FileInput](app://widgets/inputs/file-input), [CodeInput](app://widgets/inputs/code-input), [AudioInput](app://widgets/inputs/audio-input), [FeedbackInput](app://widgets/inputs/feedback-input), [ReadOnlyInput](app://widgets/inputs/read-only-input) |
                | Primitives | [Text](app://widgets/primitives/text-block), [Icon](app://widgets/primitives/icon), [Image](app://widgets/primitives/image), [Box](app://widgets/primitives/box), [Fragment](app://widgets/primitives/fragment), [Spacer](app://widgets/primitives/spacer), [Separator](app://widgets/primitives/separator), [Avatar](app://widgets/primitives/avatar), [Skeleton](app://widgets/primitives/skeleton), [CodeBlock](app://widgets/primitives/code-block), [Kbd](app://widgets/primitives/kbd), [Callout](app://widgets/primitives/callout), [Error](app://widgets/primitives/error), [Markdown](app://widgets/primitives/markdown), [Html](app://widgets/primitives/html), [Xml](app://widgets/primitives/xml), [Json](app://widgets/primitives/json), [Svg](app://widgets/primitives/svg), [Embed](app://widgets/primitives/embed), [Iframe](app://widgets/primitives/iframe), [AudioPlayer](app://widgets/primitives/audio-player), [VideoPlayer](app://widgets/primitives/video-player), [Stepper](app://widgets/primitives/stepper) |
                | Layouts | [StackLayout](app://widgets/layouts/stack-layout), [GridLayout](app://widgets/layouts/grid-layout), [HeaderLayout](app://widgets/layouts/header-layout), [FooterLayout](app://widgets/layouts/footer-layout), [SidebarLayout](app://widgets/layouts/sidebar-layout), [TabsLayout](app://widgets/layouts/tabs-layout), [ResizablePanelGroup](app://widgets/layouts/resizable-panel-group), [FloatingPanel](app://widgets/layouts/floating-panel) |
                | Effects | [Animation](app://widgets/effects/animation), [Confetti](app://widgets/effects/confetti) |
                | Charts | [LineChart](app://widgets/charts/line-chart), [BarChart](app://widgets/charts/bar-chart), [AreaChart](app://widgets/charts/area-chart), [PieChart](app://widgets/charts/pie-chart) |
                | Advanced | [DataTable](app://widgets/advanced/data-table), [Sheet](app://widgets/advanced/sheet), [Kanban](app://widgets/advanced/kanban), [Chat](app://widgets/advanced/chat), [ExternalWidgets](app://widgets/advanced/external-widgets) |
                
                ### Common Widgets
                
                The common widgets category offers you the opportunity to work with essential UI elements including [badges](app://widgets/common/badge), [blades](app://widgets/common/blades), [buttons](app://widgets/common/button), [cards](app://widgets/common/card), [details](app://widgets/common/details) implementations, [dropdown menus](app://widgets/common/drop-down-menu), [expandable](app://widgets/common/expandable) sections, [lists](app://widgets/common/list), [progress](app://widgets/common/progress) bars, [tables](app://widgets/common/table), and [tooltips](app://widgets/common/tooltip). Each widget is designed with Ivy's signature approach to simplicity and functionality.
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TB
                    A[Common Widgets] --> B[Badges, Blades, Buttons, Cards]
                    A --> C[Dropdown Menus, Expandables, Lists]
                    A --> D[Progress Bars, Tables, Tooltips]
                    A --> E[Details Implementations]
                ```
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new CommonWidgetsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class CommonWidgetsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            return Layout.Grid().Columns(2).Gap(4)
                                | new Card(
                                    Layout.Horizontal().Gap(2)
                                        | new Button("Click Me", onClick: _ => client.Toast("Hello!"))
                                        | new Button("Destructive").Destructive()
                                        | new Button("Secondary").Secondary()
                                ).Title("Buttons").Description("Interactive button variants").Height(Size.Units(40))
                                | new Card(
                                    Layout.Wrap().Gap(2)
                                        | new Badge("Primary")
                                        | new Badge("Success").Icon(Icons.Check)
                                        | new Badge("Outline").Outline()
                                ).Title("Badges").Description("Status and label badges").Height(Size.Units(40))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | new Progress(50).Goal("Task completion")
                                        | new Progress(75).Color(Colors.Amber)
                                        | new Progress(90)
                                ).Title("Progress").Description("Task completion indicators").Height(Size.Units(50))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | new Card("Clickable Card").OnClick(_ => client.Toast("Clicked!"))
                                ).Title("Cards").Description("Content containers").Height(Size.Units(50))
                                | new Card(
                                    new[] {
                                        new { Name = "Apple", Price = 1.20 },
                                        new { Name = "Banana", Price = 0.80 },
                                        new { Name = "Cherry", Price = 2.50 }
                                    }.ToTable()
                                ).Title("Table").Description("Structured data display").Height(Size.Units(70))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | new Expandable("Click to expand", "Hidden content appears here")
                                        | new Expandable("Another section", "More expandable content")
                                ).Title("Expandable").Description("Collapsible sections").Height(Size.Units(70))
                                | new Card(
                                    new List(new[]
                                    {
                                        new ListItem("First item", icon: Icons.Circle),
                                        new ListItem("Second item", icon: Icons.Circle),
                                        new ListItem("Third item", icon: Icons.Circle)
                                    })
                                ).Title("List").Description("Vertical item lists").Height(Size.Units(70))
                                | new Card(
                                    new { Name = "John Doe", Email = "john@example.com", Role = "Admin" }
                                        .ToDetails()
                                ).Title("Details").Description("Label-value pairs").Height(Size.Units(70))
                                | new Card(
                                    Layout.Vertical().Align(Align.Center)
                                        | new DropDownMenu(_ => { },
                                            new Button("Menu"),
                                            MenuItem.Default("Profile"),
                                            MenuItem.Default("Settings"),
                                            MenuItem.Separator(),
                                            MenuItem.Default("Logout"))
                                ).Title("DropDownMenu").Description("Action menus").Height(Size.Units(40))
                                | new Card(
                                    Layout.Horizontal().Align(Align.Center).Gap(2)
                                        | new Button("Hover").Icon(Icons.Info).WithTooltip("This is a tooltip")
                                        | new Button("Help").Icon(Icons.CircleQuestionMark).WithTooltip("Get help here")
                                ).Title("Tooltip").Description("Contextual information").Height(Size.Units(40));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Input Widgets
                
                We also provide our users with various input methods to capture user data. Users can work with simple input types such as [boolean inputs](app://widgets/inputs/bool-input), [feedback](app://widgets/inputs/feedback-input) forms, [text inputs](app://widgets/inputs/text-input), [number inputs](app://widgets/inputs/number-input), [date ranges](app://widgets/inputs/date-range-input), and [date-time pickers](app://widgets/inputs/date-time-input). Additionally, we offer specialized features including Ivy's [color](app://widgets/inputs/color-input) palette system and our implementation of [code](app://widgets/inputs/code-input) highlighting. We introduce our [file input](app://widgets/inputs/file-input) implementations, [read-only](app://widgets/inputs/read-only-input) statements, and provide the ability to work with complex structures like [async select](app://widgets/inputs/async-select-input) operations in a simple, intuitive way.
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph BT
                    A[Input Methods] --> B[Boolean, Feedback, Text, Number, Date, DateRange]
                    A --> C[Color Palette, Code Highlighting, File Inputs, Read-Only]
                    A --> D[Async Select, Complex Structures]
                ```
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new InputWidgetsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class InputWidgetsDemo : ViewBase
                    {
                        private static readonly string[] Categories = { "Electronics", "Clothing", "Books", "Home & Garden", "Sports" };
                    
                        public override object? Build()
                        {
                            var textState = UseState("");
                            var numberState = UseState(0);
                            var boolState = UseState(false);
                            var dateState = UseState(DateTime.Now);
                            var dateRangeState = UseState<(DateOnly?, DateOnly?)>((null, null));
                            var colorState = UseState("#00cc92");
                            var codeState = UseState("var x = 10;");
                            var fileState = UseState<FileUpload<byte[]>?>();
                            var fileUpload = UseUpload(MemoryStreamUploadHandler.Create(fileState));
                            var feedbackState = UseState(4);
                            var selectState = UseState("");
                            var asyncSelectState = UseState((string?)null);
                    
                            var selectedCategory = UseState<string?>(default(string?));
                    
                            QueryResult<Option<string>[]> QueryCategories(IViewContext context, string query)
                            {
                                return context.UseQuery<Option<string>[], (string, string)>(
                                    key: (nameof(QueryCategories), query),
                                    fetcher: ct => Task.FromResult(Categories
                                        .Where(c => c.Contains(query, StringComparison.OrdinalIgnoreCase))
                                        .Select(c => new Option<string>(c))
                                        .ToArray()));
                            }
                    
                            QueryResult<Option<string>?> LookupCategory(IViewContext context, string? category)
                            {
                                return context.UseQuery<Option<string>?, (string, string?)>(
                                    key: (nameof(LookupCategory), category),
                                    fetcher: ct => Task.FromResult(category != null ? new Option<string>(category) : null));
                            }
                    
                            return Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | new TextInput(textState).Placeholder("Enter text...")
                                        | new TextInput(textState).Variant(TextInputVariant.Password).Placeholder("Password")
                                        | new TextInput(textState).Variant(TextInputVariant.Email).Placeholder("Email")
                                        | new TextInput(textState).Variant(TextInputVariant.Search).Placeholder("Search...")
                                ).Title("TextInput").Description("Text input variants").Height(Size.Units(80))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | new NumberInput<double>(numberState).Min(0).Max(100).Variant(NumberInputVariant.Slider)
                                        | new NumberInput<int>(numberState).Placeholder("Enter number")
                                        | new NumberInput<decimal>(numberState).FormatStyle(NumberFormatStyle.Currency).Currency("USD").Placeholder("$0.00")
                                        | new NumberInput<double>(numberState).FormatStyle(NumberFormatStyle.Percent).Placeholder("0%")
                                ).Title("NumberInput").Description("Number and slider").Height(Size.Units(80))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | new BoolInput(boolState).Label("Accept terms and conditions")
                                        | boolState.ToSwitchInput().Label("Enable notifications")
                                ).Title("BoolInput").Description("Checkbox input").Height(Size.Units(65))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | fileState.ToFileInput(fileUpload).Placeholder("Upload file")
                                ).Title("FileInput").Description("File upload").Height(Size.Units(65))
                                | new Card(
                                    dateRangeState.ToDateRangeInput().Placeholder("Select date range")
                                ).Title("DateRange").Description("Date range picker").Height(Size.Units(40))
                                | new Card(
                                    new DateTimeInput<DateTime>(dateState).Placeholder("Select date")
                                ).Title("DateTimeInput").Description("Date and time picker").Height(Size.Units(40))
                                | new Card(
                                    new FeedbackInput<int>(feedbackState).Stars()
                                ).Title("Feedback").Description("Star rating").Height(Size.Units(40))
                                | new Card(
                                    colorState.ToColorInput().Variant(ColorInputVariant.Picker)
                                ).Title("Color").Description("Color picker").Height(Size.Units(40))
                                | new Card(
                                    codeState.ToCodeInput().Language(Languages.Javascript).Height(Size.Units(15))
                                ).Title("Code").Description("Code editor").Height(Size.Units(50))
                                | new Card(
                                    selectState.ToSelectInput(new[] { "Option 1", "Option 2", "Option 3" }.ToOptions()).Placeholder("Select option")
                                ).Title("Select").Description("Dropdown select").Height(Size.Units(50))
                                | new Card(
                                    new ReadOnlyInput<string>("Read-only value")
                                ).Title("ReadOnly").Description("Read-only display").Height(Size.Units(40))
                                | new Card(
                                    selectedCategory.ToAsyncSelectInput(QueryCategories, LookupCategory, "Search categories...")
                                ).Title("AsyncSelect").Description("Async dropdown").Height(Size.Units(40));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Primitives
                
                Ivy also provides a special experience when working with [primitive](app://widgets/primitives/_index) widgets. We make complex tasks simpler through our implementation of [boxes](app://widgets/primitives/box), [callouts](app://widgets/primitives/callout), [error](app://widgets/primitives/error) displays, and [text blocks](app://widgets/primitives/text-block). You can easily add [avatars](app://widgets/primitives/avatar), [icons](app://widgets/primitives/icon), [images](app://widgets/primitives/image), [spacers](app://widgets/primitives/spacer), and [separators](app://widgets/primitives/separator) to enhance your interfaces. We also provide our own implementations of [JSON](app://widgets/primitives/json), [XML](app://widgets/primitives/xml), [HTML](app://widgets/primitives/html), and [code](app://widgets/primitives/code-block) rendering capabilities.
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart LR
                    A[Primitive Widgets] --> B[Boxes, Callouts, Errors, Text Blocks]
                    A --> C[Avatars, Icons, Images, Spacers, Separators]
                    A --> D[JSON, XML, HTML, Code Rendering]
                ```
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new PrimitiveWidgetsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class PrimitiveWidgetsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.H3("Heading 3")
                                        | Text.P("Paragraph text")
                                        | Text.Label("Label text")
                                        | Text.P("Large text").Large()
                                        | Text.Lead("Lead text")
                                ).Title("Text").Description("Text variants").Height(Size.Units(75))
                                | new Card(
                                    Layout.Vertical().Align(Align.Center)
                                        | new Image("https://api.images.cat/150/150")
                                ).Title("Image").Description("Image display").Height(Size.Units(75))
                                | new Card(
                                    Layout.Horizontal().Gap(4)
                                        | new Icon(Icons.Heart, Colors.Red)
                                        | new Icon(Icons.Star, Colors.Yellow)
                                        | new Icon(Icons.Check, Colors.Green)
                                        | new Icon(Icons.Settings, Colors.Blue)
                                        | new Icon(Icons.Bell, Colors.Orange)
                                        | new Icon(Icons.Mail, Colors.Purple)
                                        | new Icon(Icons.User, Colors.Cyan)
                                ).Title("Icon").Description("Vector icons").Height(Size.Units(40))
                                | new Card(
                                    Layout.Horizontal().Gap(2)
                                        | new Avatar("John Doe")
                                        | new Avatar("JD", "https://api.images.cat/150/150?1")
                                        | new Avatar("AB")
                                        | new Avatar("Mary Smith")
                                        | new Avatar("TC", "https://api.images.cat/150/150?2")
                                        | new Avatar("XY")
                                ).Title("Avatar").Description("User avatars").Height(Size.Units(40))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | Callout.Info("Info message")
                                        | Callout.Warning("Warning message")
                                ).Title("Callout").Description("Alert messages").Height(Size.Units(70))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | new Box("Solid border").BorderStyle(BorderStyle.Solid)
                                        | new Box("Full radius").BorderRadius(BorderRadius.Full)
                                ).Title("Box").Description("Content container").Height(Size.Units(70))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Content above")
                                        | new Separator()
                                        | Text.P("Content below")
                                ).Title("Separator").Description("Visual divider").Height(Size.Units(50))
                                | new Card(
                                    Layout.Vertical()
                                        | Text.P("Top content")
                                        | new Spacer().Height(Size.Units(4))
                                        | Text.P("Bottom content")
                                ).Title("Spacer").Description("Empty space").Height(Size.Units(50))
                                | new Card(
                                    Text.Code("var x = 10;\nconsole.log(x);", Languages.Javascript)
                                ).Title("Code").Description("Syntax highlighting").Height(Size.Units(60))
                                | new Card(
                                    new Markdown("**Bold** and *italic* text\n\n- Item 1\n- Item 2")
                                ).Title("Markdown").Description("Markdown rendering").Height(Size.Units(60))
                                | new Card(
                                    Text.Json("{ \"name\": \"value\", \"count\": 42 }")
                                ).Title("Json").Description("JSON display").Height(Size.Units(50))
                                | new Card(
                                    Text.Xml("<root><item>Value</item></root>")
                                ).Title("Xml").Description("XML display").Height(Size.Units(50))
                                | new Card(
                                    Text.Html("<div><strong>Bold</strong> text and <em>italic</em> text</div>")
                                ).Title("Html").Description("HTML rendering").Height(Size.Units(40))
                                | new Card(
                                    new Error("An error occurred")
                                ).Title("Error").Description("Error display").Height(Size.Units(40));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Layouts
                
                Ivy makes working with layouts not just easier, but satisfying. We provide a much more intuitive way to work with layouts and their elements, allowing you to create complex arrangements with minimal effort.
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph LR
                    A[Layout Widgets] --> B[Basic Layouts]
                    A --> C[Panel Layouts]
                    A --> D[Section Layouts]
                
                    B --> B1[Grid]
                    B --> B2[Horizontal]
                    B --> B3[Vertical]
                    B --> B4[Wrap]
                
                    C --> C1[Floating Panel]
                    C --> C2[Resizable Panel Group]
                    C --> C3[Sidebar]
                    C --> C4[Tabs]
                
                    D --> D1[Header]
                    D --> D2[Footer]
                ```
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new LayoutWidgetsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class LayoutWidgetsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var showPanel = UseState(false);
                            var singleColumnExamples = Layout.Vertical().Gap(4).Width(Size.Full())
                                | new Card(
                                    Layout.Grid().Columns(2).Width(Size.Full()).Gap(2)
                                        | new Box("1").Width(Size.Full())
                                        | new Box("2").Width(Size.Full())
                                        | new Box("3").Width(Size.Full())
                                        | new Box("4").Width(Size.Full())
                                ).Title("GridLayout").Description("2D grid arrangement").Height(Size.Units(50))
                                | new Card(
                                    new HeaderLayout(
                                        header: new Card("Fixed Header Area").Title("Header"),
                                        content: Layout.Vertical().Gap(2)
                                            | Text.P("More scrollable content")
                                    )
                                ).Title("Header").Description("Fixed header").Height(Size.Units(60))
                                | new Card(
                                    new FooterLayout(
                                        footer: Layout.Horizontal().Gap(2)
                                            | new Button("Cancel").Secondary()
                                            | new Button("Save"),
                                        content: Layout.Vertical().Gap(2)
                                            | Text.P("Footer stays at bottom")
                                    )
                                ).Title("Footer").Description("Fixed footer").Height(Size.Units(60));
                    
                            var twoColumnExamples = Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
                                | new Card(
                                    Layout.Horizontal().Gap(2)
                                        | new Box("Item 1").Width(Size.Fraction(1/3f))
                                        | new Box("Item 2").Width(Size.Fraction(1/3f))
                                        | new Box("Item 3").Width(Size.Fraction(1/3f))
                                ).Title("Horizontal").Description("Horizontal flow").Height(Size.Units(50))
                                | new Card(
                                    Layout.Vertical().Gap(2)
                                        | new Box("Item 1").Width(Size.Full())
                                        | new Box("Item 2").Width(Size.Full())
                                ).Title("Vertical").Description("Vertical stack").Height(Size.Units(50))
                                | new Card(
                                    Layout.Wrap().Gap(2)
                                        | new Badge("Item 1")
                                        | new Badge("Item 2")
                                        | new Badge("Item 3")
                                        | new Badge("Item 4")
                                        | new Badge("Item 5")
                                        | new Badge("Item 6")
                                        | new Badge("Item 7")
                                ).Title("Wrap").Description("Auto-wrapping layout").Height(Size.Units(50))
                                | new Card(
                                    Layout.Tabs(
                                        new Tab("Tab 1", new Badge("Content 1")),
                                        new Tab("Tab 2", new Badge("Content 2")),
                                        new Tab("Tab 3", new Badge("Content 3"))
                                    )
                                ).Title("TabsLayout").Description("Tabbed interface").Height(Size.Units(50))
                                | new Card(
                                    Layout.Vertical().Gap(4)
                                    | new Card(
                                        Layout.Horizontal().Gap(2).Align(Align.Center)
                                            | new Button("Show Panel", onClick: _ => showPanel.Set(true))
                                            | new Button("Hide Panel", onClick: _ => showPanel.Set(false))
                                    ).Width(Size.Full())
                                    | (showPanel.Value ? new FloatingPanel(
                                        new Button("Floating Action")
                                            .Icon(Icons.Plus)
                                            .Large()
                                            .BorderRadius(BorderRadius.Full)
                                    ) : null)
                                ).Title("FloatingPanel").Description("Fixed position overlay").Height(Size.Units(60))
                                | new Card(
                                    new ResizablePanelGroup(
                                        new ResizablePanel(Size.Fraction(0.4f),
                                            new Card("Left")),
                                        new ResizablePanel(Size.Fraction(0.6f),
                                            new Card("Right"))
                                    )
                                ).Title("ResizablePanelGroup").Description("Resizable panels").Height(Size.Units(60));
                    
                            return Layout.Vertical().Gap(4)
                                | twoColumnExamples
                                | singleColumnExamples;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Charts
                
                Additionally, Ivy has its own implementation of charts, which makes data visualization much simpler to work with.
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                flowchart TB
                    A[Chart Widgets] --> B[Area Chart]
                    A --> C[Bar Chart]
                    A --> D[Line Chart]
                    A --> E[Pie Chart]
                ```
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ChartWidgetsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ChartWidgetsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var data = new[]
                            {
                                new { Month = "Jan", Desktop = 186, Mobile = 100 },
                                new { Month = "Feb", Desktop = 305, Mobile = 200 },
                                new { Month = "Mar", Desktop = 237, Mobile = 300 },
                                new { Month = "Apr", Desktop = 186, Mobile = 100 }
                            };
                    
                            return Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
                                | new Card(
                                    data.ToLineChart()
                                        .Dimension("Month", e => e.Month)
                                        .Measure("Desktop", e => e.Sum(f => f.Desktop))
                                        .Measure("Mobile", e => e.Sum(f => f.Mobile))
                                ).Title("LineChart").Description("Trend visualization").Height(Size.Units(100))
                                | new Card(
                                    data.ToBarChart()
                                        .Dimension("Month", e => e.Month)
                                        .Measure("Desktop", e => e.Sum(f => f.Desktop))
                                ).Title("BarChart").Description("Bar comparison").Height(Size.Units(100))
                                | new Card(
                                    data.ToAreaChart()
                                        .Dimension("Month", e => e.Month)
                                        .Measure("Desktop", e => e.Sum(f => f.Desktop))
                                ).Title("AreaChart").Description("Area under curve").Height(Size.Units(120))
                                | new Card(
                                    data.ToPieChart(
                                        e => e.Month,
                                        e => e.Sum(f => f.Desktop))
                                ).Title("PieChart").Description("Part-to-whole").Height(Size.Units(120));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Effects
                
                Ivy provides a rich collection of built-in effects and animations to enhance your user interfaces. Working with effects in Ivy is incredibly simple and intuitive. For detailed information about specific effects, refer to the [animation](app://hooks/core/use-effect) and [confetti](app://hooks/core/use-effect) documentation pages.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new EffectWidgetsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class EffectWidgetsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
                                | new Card(
                                    Layout.Horizontal().Align(Align.Center).Gap(2)
                                        | new Button("Click Confetti").WithConfetti(AnimationTrigger.Click)
                                        | new Button("Hover Confetti").WithConfetti(AnimationTrigger.Hover)
                                ).Title("Confetti").Description("Celebration effects").Height(Size.Units(40))
                                | new Card(
                                    Layout.Horizontal().Gap(4)
                                        | Icons.Heart.ToIcon().Color(Colors.Red).WithAnimation(AnimationType.Pulse).Trigger(AnimationTrigger.Click)
                                        | Icons.Bell.ToIcon().Color(Colors.Orange).WithAnimation(AnimationType.Shake).Trigger(AnimationTrigger.Click)
                                        | Icons.Star.ToIcon().Color(Colors.Yellow).WithAnimation(AnimationType.Bounce).Trigger(AnimationTrigger.Click)
                                        | Icons.LoaderCircle.ToIcon().Color(Colors.Blue).WithAnimation(AnimationType.Rotate).Trigger(AnimationTrigger.Click)
                                        | Icons.Zap.ToIcon().Color(Colors.Purple).WithAnimation(AnimationType.Pulse).Trigger(AnimationTrigger.Click)
                                        | Icons.TrendingUp.ToIcon().Color(Colors.Green).WithAnimation(AnimationType.Bounce).Trigger(AnimationTrigger.Click)
                                        | Icons.Sparkles.ToIcon().Color(Colors.Pink).WithAnimation(AnimationType.Rotate).Trigger(AnimationTrigger.Click)
                                        | Icons.CircleAlert.ToIcon().Color(Colors.Destructive).WithAnimation(AnimationType.Shake).Trigger(AnimationTrigger.Click)
                                ).Title("Animation").Description("Click icons to animate").Height(Size.Units(40));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Advanced
                
                In the Advanced section, we introduce our specialized implementations for working with [sheets](app://widgets/advanced/sheet) and [chat functionality](app://widgets/advanced/chat). These advanced widgets provide sophisticated features for complex user interface requirements.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AdvancedWidgetsDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AdvancedWidgetsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var messages = UseState(ImmutableArray.Create<ChatMessage>(
                                new ChatMessage(ChatSender.Assistant, "Hello! I'm a demo chat bot.")
                            ));
                    
                            void OnSend(Event<Chat, string> @event)
                            {
                                var currentMessages = messages.Value;
                                messages.Set(currentMessages.Add(new ChatMessage(ChatSender.User, @event.Value)));
                                messages.Set(currentMessages.Add(new ChatMessage(ChatSender.Assistant, $"You said: {@event.Value}")));
                            }
                    
                            return Layout.Vertical().Gap(4).Width(Size.Full())
                                | new Card(
                                    new Button("Open Sheet").WithSheet(
                                        () => Layout.Vertical()
                                            | Text.H3("Sheet Content")
                                            | Text.P("This is content inside a sheet")
                                            | new Button("Close"),
                                        title: "Demo Sheet",
                                        description: "Sheet demonstration"
                                    )
                                ).Title("Sheet").Description("Side panel overlay").Height(Size.Units(40))
                                | new Card(
                                    new Chat(messages.Value.ToArray(), OnSend)
                                        .Height(Size.Units(30))
                                ).Title("Chat").Description("Conversation interface").Height(Size.Units(70));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.AppsApp), typeof(Widgets._IndexApp), typeof(Widgets.Common.ButtonApp), typeof(Widgets.Common.BadgeApp), typeof(Widgets.Common.TooltipApp), typeof(Widgets.Common.CardApp), typeof(Widgets.Common.DetailsApp), typeof(Widgets.Common.ExpandableApp), typeof(Widgets.Common.ListApp), typeof(Widgets.Common.TableApp), typeof(Widgets.Common.PaginationApp), typeof(Widgets.Common.ProgressApp), typeof(Widgets.Common.DropDownMenuApp), typeof(Widgets.Common.BladesApp), typeof(Widgets.Common.MetricViewApp), typeof(Widgets.Common.TerminalApp), typeof(Widgets.Common.DialogApp), typeof(Widgets.Common.TreeApp), typeof(Widgets.Inputs.FieldApp), typeof(Widgets.Inputs.TextInputApp), typeof(Widgets.Inputs.NumberInputApp), typeof(Widgets.Inputs.BoolInputApp), typeof(Widgets.Inputs.SelectInputApp), typeof(Widgets.Inputs.AsyncSelectInputApp), typeof(Widgets.Inputs.DateTimeInputApp), typeof(Widgets.Inputs.DateRangeInputApp), typeof(Widgets.Inputs.ColorInputApp), typeof(Widgets.Inputs.FileInputApp), typeof(Widgets.Inputs.CodeInputApp), typeof(Widgets.Inputs.AudioInputApp), typeof(Widgets.Inputs.FeedbackInputApp), typeof(Widgets.Inputs.ReadOnlyInputApp), typeof(Widgets.Primitives.TextBlockApp), typeof(Widgets.Primitives.IconApp), typeof(Widgets.Primitives.ImageApp), typeof(Widgets.Primitives.BoxApp), typeof(Widgets.Primitives.FragmentApp), typeof(Widgets.Primitives.SpacerApp), typeof(Widgets.Primitives.SeparatorApp), typeof(Widgets.Primitives.AvatarApp), typeof(Widgets.Primitives.SkeletonApp), typeof(Widgets.Primitives.CodeBlockApp), typeof(Widgets.Primitives.KbdApp), typeof(Widgets.Primitives.CalloutApp), typeof(Widgets.Primitives.ErrorApp), typeof(Widgets.Primitives.MarkdownApp), typeof(Widgets.Primitives.HtmlApp), typeof(Widgets.Primitives.XmlApp), typeof(Widgets.Primitives.JsonApp), typeof(Widgets.Primitives.SvgApp), typeof(Widgets.Primitives.EmbedApp), typeof(Widgets.Primitives.IframeApp), typeof(Widgets.Primitives.AudioPlayerApp), typeof(Widgets.Primitives.VideoPlayerApp), typeof(Widgets.Primitives.StepperApp), typeof(Widgets.Layouts.StackLayoutApp), typeof(Widgets.Layouts.GridLayoutApp), typeof(Widgets.Layouts.HeaderLayoutApp), typeof(Widgets.Layouts.FooterLayoutApp), typeof(Widgets.Layouts.SidebarLayoutApp), typeof(Widgets.Layouts.TabsLayoutApp), typeof(Widgets.Layouts.ResizablePanelGroupApp), typeof(Widgets.Layouts.FloatingPanelApp), typeof(Widgets.Effects.AnimationApp), typeof(Widgets.Effects.ConfettiApp), typeof(Widgets.Charts.LineChartApp), typeof(Widgets.Charts.BarChartApp), typeof(Widgets.Charts.AreaChartApp), typeof(Widgets.Charts.PieChartApp), typeof(Widgets.Advanced.DataTableApp), typeof(Widgets.Advanced.SheetApp), typeof(Widgets.Advanced.KanbanApp), typeof(Widgets.Advanced.ChatApp), typeof(Widgets.Advanced.ExternalWidgetsApp), typeof(Widgets.Primitives._IndexApp), typeof(Hooks.Core.UseEffectApp)]; 
        return article;
    }
}


public class CommonWidgetsDemo : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        return Layout.Grid().Columns(2).Gap(4)
            | new Card(
                Layout.Horizontal().Gap(2)
                    | new Button("Click Me", onClick: _ => client.Toast("Hello!"))
                    | new Button("Destructive").Destructive()
                    | new Button("Secondary").Secondary()
            ).Title("Buttons").Description("Interactive button variants").Height(Size.Units(40))
            | new Card(
                Layout.Wrap().Gap(2)
                    | new Badge("Primary")
                    | new Badge("Success").Icon(Icons.Check)
                    | new Badge("Outline").Outline()
            ).Title("Badges").Description("Status and label badges").Height(Size.Units(40))
            | new Card(
                Layout.Vertical().Gap(2)
                    | new Progress(50).Goal("Task completion")
                    | new Progress(75).Color(Colors.Amber)
                    | new Progress(90)
            ).Title("Progress").Description("Task completion indicators").Height(Size.Units(50))
            | new Card(
                Layout.Vertical().Gap(2)
                    | new Card("Clickable Card").OnClick(_ => client.Toast("Clicked!"))
            ).Title("Cards").Description("Content containers").Height(Size.Units(50))
            | new Card(
                new[] {
                    new { Name = "Apple", Price = 1.20 },
                    new { Name = "Banana", Price = 0.80 },
                    new { Name = "Cherry", Price = 2.50 }
                }.ToTable()
            ).Title("Table").Description("Structured data display").Height(Size.Units(70))
            | new Card(
                Layout.Vertical().Gap(2)
                    | new Expandable("Click to expand", "Hidden content appears here")
                    | new Expandable("Another section", "More expandable content")
            ).Title("Expandable").Description("Collapsible sections").Height(Size.Units(70))
            | new Card(
                new List(new[]
                {
                    new ListItem("First item", icon: Icons.Circle),
                    new ListItem("Second item", icon: Icons.Circle),
                    new ListItem("Third item", icon: Icons.Circle)
                })
            ).Title("List").Description("Vertical item lists").Height(Size.Units(70))
            | new Card(
                new { Name = "John Doe", Email = "john@example.com", Role = "Admin" }
                    .ToDetails()
            ).Title("Details").Description("Label-value pairs").Height(Size.Units(70))
            | new Card(
                Layout.Vertical().Align(Align.Center)
                    | new DropDownMenu(_ => { }, 
                        new Button("Menu"),
                        MenuItem.Default("Profile"),
                        MenuItem.Default("Settings"),
                        MenuItem.Separator(),
                        MenuItem.Default("Logout"))
            ).Title("DropDownMenu").Description("Action menus").Height(Size.Units(40))
            | new Card(
                Layout.Horizontal().Align(Align.Center).Gap(2)
                    | new Button("Hover").Icon(Icons.Info).WithTooltip("This is a tooltip")
                    | new Button("Help").Icon(Icons.CircleQuestionMark).WithTooltip("Get help here")
            ).Title("Tooltip").Description("Contextual information").Height(Size.Units(40));
    }
}

public class InputWidgetsDemo : ViewBase
{
    private static readonly string[] Categories = { "Electronics", "Clothing", "Books", "Home & Garden", "Sports" };
    
    public override object? Build()
    {
        var textState = UseState("");
        var numberState = UseState(0);
        var boolState = UseState(false);
        var dateState = UseState(DateTime.Now);
        var dateRangeState = UseState<(DateOnly?, DateOnly?)>((null, null));
        var colorState = UseState("#00cc92");
        var codeState = UseState("var x = 10;");
        var fileState = UseState<FileUpload<byte[]>?>();
        var fileUpload = UseUpload(MemoryStreamUploadHandler.Create(fileState));
        var feedbackState = UseState(4);
        var selectState = UseState("");
        var asyncSelectState = UseState((string?)null);
        
        var selectedCategory = UseState<string?>(default(string?));

        QueryResult<Option<string>[]> QueryCategories(IViewContext context, string query)
        {
            return context.UseQuery<Option<string>[], (string, string)>(
                key: (nameof(QueryCategories), query),
                fetcher: ct => Task.FromResult(Categories
                    .Where(c => c.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .Select(c => new Option<string>(c))
                    .ToArray()));
        }

        QueryResult<Option<string>?> LookupCategory(IViewContext context, string? category)
        {
            return context.UseQuery<Option<string>?, (string, string?)>(
                key: (nameof(LookupCategory), category),
                fetcher: ct => Task.FromResult(category != null ? new Option<string>(category) : null));
        }

        return Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
            | new Card(
                Layout.Vertical().Gap(2)
                    | new TextInput(textState).Placeholder("Enter text...")
                    | new TextInput(textState).Variant(TextInputVariant.Password).Placeholder("Password")
                    | new TextInput(textState).Variant(TextInputVariant.Email).Placeholder("Email")
                    | new TextInput(textState).Variant(TextInputVariant.Search).Placeholder("Search...")
            ).Title("TextInput").Description("Text input variants").Height(Size.Units(80))
            | new Card(
                Layout.Vertical().Gap(2)
                    | new NumberInput<double>(numberState).Min(0).Max(100).Variant(NumberInputVariant.Slider)
                    | new NumberInput<int>(numberState).Placeholder("Enter number")
                    | new NumberInput<decimal>(numberState).FormatStyle(NumberFormatStyle.Currency).Currency("USD").Placeholder("$0.00")
                    | new NumberInput<double>(numberState).FormatStyle(NumberFormatStyle.Percent).Placeholder("0%")
            ).Title("NumberInput").Description("Number and slider").Height(Size.Units(80))
            | new Card(
                Layout.Vertical().Gap(2)
                    | new BoolInput(boolState).Label("Accept terms and conditions")
                    | boolState.ToSwitchInput().Label("Enable notifications")
            ).Title("BoolInput").Description("Checkbox input").Height(Size.Units(65))
            | new Card(
                Layout.Vertical().Gap(2)
                    | fileState.ToFileInput(fileUpload).Placeholder("Upload file")
            ).Title("FileInput").Description("File upload").Height(Size.Units(65))
            | new Card(
                dateRangeState.ToDateRangeInput().Placeholder("Select date range")
            ).Title("DateRange").Description("Date range picker").Height(Size.Units(40))
            | new Card(
                new DateTimeInput<DateTime>(dateState).Placeholder("Select date")
            ).Title("DateTimeInput").Description("Date and time picker").Height(Size.Units(40))
            | new Card(
                new FeedbackInput<int>(feedbackState).Stars()
            ).Title("Feedback").Description("Star rating").Height(Size.Units(40))
            | new Card(
                colorState.ToColorInput().Variant(ColorInputVariant.Picker)
            ).Title("Color").Description("Color picker").Height(Size.Units(40))
            | new Card(
                codeState.ToCodeInput().Language(Languages.Javascript).Height(Size.Units(15))
            ).Title("Code").Description("Code editor").Height(Size.Units(50))
            | new Card(
                selectState.ToSelectInput(new[] { "Option 1", "Option 2", "Option 3" }.ToOptions()).Placeholder("Select option")
            ).Title("Select").Description("Dropdown select").Height(Size.Units(50))
            | new Card(
                new ReadOnlyInput<string>("Read-only value")
            ).Title("ReadOnly").Description("Read-only display").Height(Size.Units(40))
            | new Card(
                selectedCategory.ToAsyncSelectInput(QueryCategories, LookupCategory, "Search categories...")
            ).Title("AsyncSelect").Description("Async dropdown").Height(Size.Units(40));
    }
}

public class PrimitiveWidgetsDemo : ViewBase
{
    public override object? Build()
    {
        return Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
            | new Card(
                Layout.Vertical().Gap(2)
                    | Text.H3("Heading 3")
                    | Text.P("Paragraph text")
                    | Text.Label("Label text")
                    | Text.P("Large text").Large()
                    | Text.Lead("Lead text")
            ).Title("Text").Description("Text variants").Height(Size.Units(75))
            | new Card(
                Layout.Vertical().Align(Align.Center)
                    | new Image("https://api.images.cat/150/150")
            ).Title("Image").Description("Image display").Height(Size.Units(75))
            | new Card(
                Layout.Horizontal().Gap(4)
                    | new Icon(Icons.Heart, Colors.Red)
                    | new Icon(Icons.Star, Colors.Yellow)
                    | new Icon(Icons.Check, Colors.Green)
                    | new Icon(Icons.Settings, Colors.Blue)
                    | new Icon(Icons.Bell, Colors.Orange)
                    | new Icon(Icons.Mail, Colors.Purple)
                    | new Icon(Icons.User, Colors.Cyan)
            ).Title("Icon").Description("Vector icons").Height(Size.Units(40))
            | new Card(
                Layout.Horizontal().Gap(2)
                    | new Avatar("John Doe")
                    | new Avatar("JD", "https://api.images.cat/150/150?1")
                    | new Avatar("AB")
                    | new Avatar("Mary Smith")
                    | new Avatar("TC", "https://api.images.cat/150/150?2")
                    | new Avatar("XY")
            ).Title("Avatar").Description("User avatars").Height(Size.Units(40))
            | new Card(
                Layout.Vertical().Gap(2)
                    | Callout.Info("Info message")
                    | Callout.Warning("Warning message")
            ).Title("Callout").Description("Alert messages").Height(Size.Units(70))
            | new Card(
                Layout.Vertical().Gap(2)
                    | new Box("Solid border").BorderStyle(BorderStyle.Solid)
                    | new Box("Full radius").BorderRadius(BorderRadius.Full)
            ).Title("Box").Description("Content container").Height(Size.Units(70))
            | new Card(
                Layout.Vertical().Gap(2)
                    | Text.P("Content above")
                    | new Separator()
                    | Text.P("Content below")
            ).Title("Separator").Description("Visual divider").Height(Size.Units(50))
            | new Card(
                Layout.Vertical()
                    | Text.P("Top content")
                    | new Spacer().Height(Size.Units(4))
                    | Text.P("Bottom content")
            ).Title("Spacer").Description("Empty space").Height(Size.Units(50))
            | new Card(
                Text.Code("var x = 10;\nconsole.log(x);", Languages.Javascript)
            ).Title("Code").Description("Syntax highlighting").Height(Size.Units(60))
            | new Card(
                new Markdown("**Bold** and *italic* text\n\n- Item 1\n- Item 2")
            ).Title("Markdown").Description("Markdown rendering").Height(Size.Units(60))
            | new Card(
                Text.Json("{ \"name\": \"value\", \"count\": 42 }")
            ).Title("Json").Description("JSON display").Height(Size.Units(50))
            | new Card(
                Text.Xml("<root><item>Value</item></root>")
            ).Title("Xml").Description("XML display").Height(Size.Units(50))
            | new Card(
                Text.Html("<div><strong>Bold</strong> text and <em>italic</em> text</div>")
            ).Title("Html").Description("HTML rendering").Height(Size.Units(40))
            | new Card(
                new Error("An error occurred")
            ).Title("Error").Description("Error display").Height(Size.Units(40));
    }
}

public class LayoutWidgetsDemo : ViewBase
{
    public override object? Build()
    {
        var showPanel = UseState(false);
        var singleColumnExamples = Layout.Vertical().Gap(4).Width(Size.Full())
            | new Card(
                Layout.Grid().Columns(2).Width(Size.Full()).Gap(2)
                    | new Box("1").Width(Size.Full())
                    | new Box("2").Width(Size.Full())
                    | new Box("3").Width(Size.Full())
                    | new Box("4").Width(Size.Full())
            ).Title("GridLayout").Description("2D grid arrangement").Height(Size.Units(50))
            | new Card(
                new HeaderLayout(
                    header: new Card("Fixed Header Area").Title("Header"),
                    content: Layout.Vertical().Gap(2)
                        | Text.P("More scrollable content")
                )
            ).Title("Header").Description("Fixed header").Height(Size.Units(60))
            | new Card(
                new FooterLayout(
                    footer: Layout.Horizontal().Gap(2)
                        | new Button("Cancel").Secondary()
                        | new Button("Save"),
                    content: Layout.Vertical().Gap(2)
                        | Text.P("Footer stays at bottom")
                )
            ).Title("Footer").Description("Fixed footer").Height(Size.Units(60));
        
        var twoColumnExamples = Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
            | new Card(
                Layout.Horizontal().Gap(2)
                    | new Box("Item 1").Width(Size.Fraction(1/3f))
                    | new Box("Item 2").Width(Size.Fraction(1/3f))
                    | new Box("Item 3").Width(Size.Fraction(1/3f))
            ).Title("Horizontal").Description("Horizontal flow").Height(Size.Units(50))
            | new Card(
                Layout.Vertical().Gap(2)
                    | new Box("Item 1").Width(Size.Full())
                    | new Box("Item 2").Width(Size.Full())
            ).Title("Vertical").Description("Vertical stack").Height(Size.Units(50))
            | new Card(
                Layout.Wrap().Gap(2)
                    | new Badge("Item 1")
                    | new Badge("Item 2")
                    | new Badge("Item 3")
                    | new Badge("Item 4")
                    | new Badge("Item 5")
                    | new Badge("Item 6")
                    | new Badge("Item 7")
            ).Title("Wrap").Description("Auto-wrapping layout").Height(Size.Units(50))
            | new Card(
                Layout.Tabs(
                    new Tab("Tab 1", new Badge("Content 1")),
                    new Tab("Tab 2", new Badge("Content 2")),
                    new Tab("Tab 3", new Badge("Content 3"))
                )
            ).Title("TabsLayout").Description("Tabbed interface").Height(Size.Units(50))
            | new Card(
                Layout.Vertical().Gap(4)
                | new Card(
                    Layout.Horizontal().Gap(2).Align(Align.Center)
                        | new Button("Show Panel", onClick: _ => showPanel.Set(true))
                        | new Button("Hide Panel", onClick: _ => showPanel.Set(false))
                ).Width(Size.Full())
                | (showPanel.Value ? new FloatingPanel(
                    new Button("Floating Action")
                        .Icon(Icons.Plus)
                        .Large()
                        .BorderRadius(BorderRadius.Full)
                ) : null)
            ).Title("FloatingPanel").Description("Fixed position overlay").Height(Size.Units(60))
            | new Card(
                new ResizablePanelGroup(
                    new ResizablePanel(Size.Fraction(0.4f),
                        new Card("Left")),
                    new ResizablePanel(Size.Fraction(0.6f),
                        new Card("Right"))
                )
            ).Title("ResizablePanelGroup").Description("Resizable panels").Height(Size.Units(60));
        
        return Layout.Vertical().Gap(4)
            | twoColumnExamples
            | singleColumnExamples;
    }
}

public class ChartWidgetsDemo : ViewBase
{
    public override object? Build()
    {
        var data = new[]
        {
            new { Month = "Jan", Desktop = 186, Mobile = 100 },
            new { Month = "Feb", Desktop = 305, Mobile = 200 },
            new { Month = "Mar", Desktop = 237, Mobile = 300 },
            new { Month = "Apr", Desktop = 186, Mobile = 100 }
        };
        
        return Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
            | new Card(
                data.ToLineChart()
                    .Dimension("Month", e => e.Month)
                    .Measure("Desktop", e => e.Sum(f => f.Desktop))
                    .Measure("Mobile", e => e.Sum(f => f.Mobile))
            ).Title("LineChart").Description("Trend visualization").Height(Size.Units(100))
            | new Card(
                data.ToBarChart()
                    .Dimension("Month", e => e.Month)
                    .Measure("Desktop", e => e.Sum(f => f.Desktop))
            ).Title("BarChart").Description("Bar comparison").Height(Size.Units(100))
            | new Card(
                data.ToAreaChart()
                    .Dimension("Month", e => e.Month)
                    .Measure("Desktop", e => e.Sum(f => f.Desktop))
            ).Title("AreaChart").Description("Area under curve").Height(Size.Units(120))
            | new Card(
                data.ToPieChart(
                    e => e.Month,
                    e => e.Sum(f => f.Desktop))
            ).Title("PieChart").Description("Part-to-whole").Height(Size.Units(120));
    }
}

public class EffectWidgetsDemo : ViewBase
{
    public override object? Build()
    {
        return Layout.Grid().Columns(2).Gap(4).Width(Size.Full())
            | new Card(
                Layout.Horizontal().Align(Align.Center).Gap(2)
                    | new Button("Click Confetti").WithConfetti(AnimationTrigger.Click)
                    | new Button("Hover Confetti").WithConfetti(AnimationTrigger.Hover)
            ).Title("Confetti").Description("Celebration effects").Height(Size.Units(40))
            | new Card(
                Layout.Horizontal().Gap(4)
                    | Icons.Heart.ToIcon().Color(Colors.Red).WithAnimation(AnimationType.Pulse).Trigger(AnimationTrigger.Click)
                    | Icons.Bell.ToIcon().Color(Colors.Orange).WithAnimation(AnimationType.Shake).Trigger(AnimationTrigger.Click)
                    | Icons.Star.ToIcon().Color(Colors.Yellow).WithAnimation(AnimationType.Bounce).Trigger(AnimationTrigger.Click)
                    | Icons.LoaderCircle.ToIcon().Color(Colors.Blue).WithAnimation(AnimationType.Rotate).Trigger(AnimationTrigger.Click)
                    | Icons.Zap.ToIcon().Color(Colors.Purple).WithAnimation(AnimationType.Pulse).Trigger(AnimationTrigger.Click)
                    | Icons.TrendingUp.ToIcon().Color(Colors.Green).WithAnimation(AnimationType.Bounce).Trigger(AnimationTrigger.Click)
                    | Icons.Sparkles.ToIcon().Color(Colors.Pink).WithAnimation(AnimationType.Rotate).Trigger(AnimationTrigger.Click)
                    | Icons.CircleAlert.ToIcon().Color(Colors.Destructive).WithAnimation(AnimationType.Shake).Trigger(AnimationTrigger.Click)
            ).Title("Animation").Description("Click icons to animate").Height(Size.Units(40));
    }
}

public class AdvancedWidgetsDemo : ViewBase
{
    public override object? Build()
    {
        var messages = UseState(ImmutableArray.Create<ChatMessage>(
            new ChatMessage(ChatSender.Assistant, "Hello! I'm a demo chat bot.")
        ));
        
        void OnSend(Event<Chat, string> @event)
        {
            var currentMessages = messages.Value;
            messages.Set(currentMessages.Add(new ChatMessage(ChatSender.User, @event.Value)));
            messages.Set(currentMessages.Add(new ChatMessage(ChatSender.Assistant, $"You said: {@event.Value}")));
        }
        
        return Layout.Vertical().Gap(4).Width(Size.Full())
            | new Card(
                new Button("Open Sheet").WithSheet(
                    () => Layout.Vertical()
                        | Text.H3("Sheet Content")
                        | Text.P("This is content inside a sheet")
                        | new Button("Close"),
                    title: "Demo Sheet",
                    description: "Sheet demonstration"
                )
            ).Title("Sheet").Description("Side panel overlay").Height(Size.Units(40))
            | new Card(
                new Chat(messages.Value.ToArray(), OnSend)
                    .Height(Size.Units(30))
            ).Title("Chat").Description("Conversation interface").Height(Size.Units(70));
    }
}
