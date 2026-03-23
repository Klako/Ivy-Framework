# Ivy Framework Hallucinations

Known cases where the agent hallucinated Ivy Framework APIs. Use this as a reference when debugging build errors in agent sessions.


## Badge.Color(Colors.X) — non-existent fluent method

**Hallucinated API:**
```csharp
new Badge(match.Value).Color(Colors.Green)
new Badge("No match").Color(Colors.Red)
```

**Correct API:**
```csharp
// Via constructor variant parameter:
new Badge(match.Value, BadgeVariant.Success)

// Via fluent shortcut methods:
new Badge(match.Value).Success()
new Badge("No match").Destructive()

// Via explicit Variant() method:
new Badge(match.Value).Variant(BadgeVariant.Info)
```

Available `BadgeVariant` values: `Primary`, `Destructive`, `Secondary`, `Outline`, `Success`, `Warning`, `Info`. The agent confused `LabelExtensions.Color(Label, Colors)` (which exists for `Label`) with a Badge method. Badge uses `BadgeVariant`, not `Colors`.

**Found In:**
3c507fb4-71e1-4136-9d40-8eca6590250d
ce144de9-0688-490a-bef6-b2766e323154
642d3167-790d-48c4-a381-bfab78f928cc
857de09c-ab87-49a5-aac4-394f7d0aa207
86908281-cc6f-4973-a9c7-1c0186c013d2
0c7c0b33-a500-45c2-911b-b33ca1f9662e
6c834561-6c01-424b-b8fb-a4a473c1c86a
c9185561-51f5-4c76-ae5b-7448f5a68a0f
8b576f86-85cc-43b8-97e2-358bae83464a
0ecc3f8a-9217-46f6-84a5-bceab8469233
7a0c147e-4f69-4951-b9a6-2e76ac2689a6
e084455d-82f6-4fb9-bfec-699755bd37ea
259ba5f1-4ff7-4e57-80bf-d0426933e0ec

## Details() — empty constructor instead of passing items

**Hallucinated API:**
```csharp
new Details()
    | new Detail("Country Code", result.CountryCode, false)
    | new Detail("VAT Number", result.VatNumber, false)
```

**Error:** `CS7036: There is no argument given that corresponds to the required parameter 'items' of 'Details.Details(IEnumerable<Detail>)'`

**Correct API:**
```csharp
new Details(new[] {
    new Detail("Country Code", result.CountryCode, false),
    new Detail("VAT Number", result.VatNumber, false)
})
// or use the builder pattern:
result.ToDetails()
```

The `path` parameter was renamed to `group` in v1.2.18 to better reflect that it defines the organizational group/folder in the sidebar, not a URL path. This applies to both the `[App]` attribute and the `AppDescriptor` class (`Path` property → `Group` property). (Note: This was part of a broader refactoring to rename "Chrome" to "AppShell").

`Details` requires an `IEnumerable<Detail>` in its constructor. There is no parameterless public constructor, and the pipe operator `|` does not work on `Details` to add children. Use the collection constructor or the `.ToDetails()` builder pattern on a model.

**Found In:**
857de09c-ab87-49a5-aac4-394f7d0aa207
b6beb60d-478d-409e-b10d-7913ae911e85
fd5baba6-72aa-4d28-ac10-72e1be86e494
62ecf7b7-a59e-46ae-8131-db9044c391df
a62654f6-f041-4b48-89b8-7ca19d3e6819
8b101b29-228f-40d2-90e9-79aa54fed653
92bff7c2-a0f8-4b83-b5e0-8904397acd47
e084455d-82f6-4fb9-bfec-699755bd37ea
ca7c2127-8ed0-498f-8cf5-7ba23a38910c

## Toast() — standalone function call instead of IClientProvider method

**Hallucinated API:**
```csharp
Toast("Clocked in successfully");
Toast("Employee saved!");
```

**Error:** `CS0103: The name 'Toast' does not exist in the current context`

**Correct API:**
```csharp
var client = UseService<IClientProvider>();
client.Toast("Clocked in successfully");
client.Toast("Employee saved!", "Success");  // with title
client.Error("Something went wrong.");       // error toast
```

`Toast` is an extension method on `IClientProvider`, not a standalone/global function. The agent must first resolve the client via `UseService<IClientProvider>()`.

**Found In:**
8b576f86-85cc-43b8-97e2-358bae83464a
a8076804-1223-469e-a689-2af23d259566
5ba11e91-7b05-49e1-8a0f-5ea01235b192
0ecc3f8a-9217-46f6-84a5-bceab8469233
7a0c147e-4f69-4951-b9a6-2e76ac2689a6
ab8b991a-40ad-44d0-8a15-8df256f074b0
c9185561-51f5-4c76-ae5b-7448f5a68a0f

## InputBase.Label() — AxisExtensions method used on input

**Hallucinated API:**
```csharp
// NumberInputBase
stockAdjustment.ToNumberInput().Label("Adjustment amount")

// DateTimeInputBase
dateState.ToDateInput().Label("Birthdate")
```

**Error:** `The type 'Ivy.NumberInputBase' cannot be used as type parameter 'T' in the generic type or method 'AxisExtensions.Label<T>(T, string)'` (same CS0311 error for `DateTimeInputBase`, `TextInputBase`, `SelectInputBase`, `BoolInputBase`, etc.)

**Correct API:**
```csharp
// Use .WithField().Label() to wrap the input in a labeled field:
stockAdjustment.ToNumberInput().WithField().Label("Adjustment amount")
dateState.ToDateInput().WithField().Label("Birthdate")

// Or use Text.Label() as a separate element above the input:
Layout.Vertical()
    | Text.Label("Adjustment amount")
    | stockAdjustment.ToNumberInput()

// Or use a form with .Label() on the form builder:
state.ToForm().Label(m => m.Amount, "Adjustment amount")
```

`.Label()` is an `AxisExtensions` method for chart axes, not for inputs. This applies to ALL input types (`NumberInputBase`, `DateTimeInputBase`, `TextInputBase`, `SelectInputBase`, `BoolInputBase`, etc.). The preferred way to label an input is `.WithField().Label("...")`, which wraps the input in a `Field` with a label.

**Found In:**
f20dced8-1689-4289-a2d8-ee67136eb6ce
2e91e9c7-9c03-4b86-a9d2-c0417bcf715f
7a9aadf3-097e-448d-8d5c-bc86152710a6
3094f74c-2d04-41eb-b6d5-2b33b68cbcd8
e084455d-82f6-4fb9-bfec-699755bd37ea

## AppAttribute.path — renamed to group

**Hallucinated API:**
```csharp
[App(path: ["Tests"])]
```

**Error:** `'AppAttribute' does not contain a definition for 'path'`

**Correct API:**
```csharp
[App(group: ["Tests"])]
```

The `path` parameter was renamed to `group` in v1.2.18 to better reflect that it defines the organizational group/folder in the sidebar, not a URL path. This applies to both the `[App]` attribute and the `AppDescriptor` class (`Path` property → `Group` property).

**Found In:**
Ivy-Framework#2612
55eafb82-2cc2-48ba-9a66-cd2ed8d38d67
fd5baba6-72aa-4d28-ac10-72e1be86e494
(multiple sessions — agent uses old API names from training data)

## Button("text", Icons.X) — icon as constructor argument

**Hallucinated API:**
```csharp
new Button("Add Item", Icons.Plus)
```

**Error:** `Argument 2: cannot convert from 'Ivy.Icons' to 'System.Func<Ivy.Event<Ivy.Button>, System.Threading.Tasks.ValueTask>?'`

**Correct API:**
```csharp
new Button("Add Item").Icon(Icons.Plus)
```

The `Button` constructor signature is `Button(string label, Func<Event<Button>, ValueTask>? onClick = null, ...)`. The second parameter is a click handler, not an icon. Use the `.Icon(Icons.X)` fluent method to set an icon on a button.

**Found In:**
f20dced8-1689-4289-a2d8-ee67136eb6ce
7a9aadf3-097e-448d-8d5c-bc86152710a6
5ba11e91-7b05-49e1-8a0f-5ea01235b192
8516f1e3-9824-4bc7-8a6d-6e4ead3e2228

## BorderRadius.Medium — non-existent enum value

**Hallucinated API:**
```csharp
BorderRadius.Medium
BorderRadius.Large
BorderRadius.Small
```

**Error:** `'BorderRadius' does not contain a definition for 'Medium'`

**Correct API:**
```csharp
BorderRadius.None     // no rounding
BorderRadius.Rounded  // standard rounded corners
BorderRadius.Full     // fully rounded (pill shape)
```

Valid `BorderRadius` values: `None`, `Rounded`, `Full`. The agent hallucinates Tailwind-style size variants (`Small`, `Medium`, `Large`, `Xl`) that don't exist.

**Found In:**
050136ca-9275-4e1d-9740-e393b544c1b5
8a776329-6dc7-474f-aa4d-c8b4da753a25 (BorderRadius.Large)
4e59e443-3579-4df9-af4b-765b7b7d61c8 (BorderRadius.Small — via IvyMcp hallucination)
ab8b991a-40ad-44d0-8a15-8df256f074b0 (BorderRadius.Large)

## ToDataTable() on List\<T\> or T[] — wrong receiver type

**Hallucinated API:**
```csharp
var items = await db.Categories.ToListAsync();
items.ToDataTable()

// Also seen with arrays:
var rows = query.Select(...).ToArray();
rows.ToDataTable()
```

**Error:** `CS1061: 'List<Category>' does not contain a definition for 'ToDataTable'` / `CS1061: 'TestFileRow[]' does not contain a definition for 'ToDataTable'`

**Correct API:**
```csharp
// ToDataTable() is an extension on IQueryable<T>, not List<T> or T[]:
db.Categories.ToDataTable()

// Or use ToTable() for in-memory collections:
items.ToTable()
```

`ToDataTable()` is defined on `IQueryable<T>` (via `DataTableBuilder`), not on `List<T>`, `T[]`, or `IEnumerable<T>`. The agent often materializes a query to a List or array first, then tries to call `ToDataTable()` on the result. Pass the `IQueryable<T>` directly to `ToDataTable()` without materializing. For in-memory collections, use `.ToTable()` instead.

**Found In:**
9d8f5446-43c4-44a2-b6ce-3caeff413407 (TestFilesApp.cs and CategoriesApp.cs)
8b576f86-85cc-43b8-97e2-358bae83464a
16d32bb9-34f3-4b14-adf9-83f802350032
8b101b29-228f-40d2-90e9-79aa54fed653

## DateTimeVariant — wrong enum name

**Hallucinated API:**
```csharp
date.ToDateTimeInput().Variant(DateTimeVariant.Date)
```

**Error:** `The name 'DateTimeVariant' does not exist in the current context`

**Correct API:**
```csharp
date.ToDateInput()
// or:
date.ToDateTimeInput().Variant(DateTimeInputVariant.Date)
```

The enum is `DateTimeInputVariant` (singular), not `DateTimeVariant` (missing "Input") or `DateTimeInputVariants` (old plural name). All input variant enums were renamed from plural to singular in Ivy-Framework#2546. Values: `DateTime`, `Date`, `Time`, `Month`, `Week`. **Auto-fixed:** The refactoring service automatically rewrites both `DateTimeVariant` and `DateTimeInputVariants` to `DateTimeInputVariant`.

**Found In:**
d90474ac-78b9-48c7-8317-3860ff36b9dd (sub-tasks 002–006, appeared in ALL sub-tasks)

## Callout constructor — wrong constructor + invented enum / wrong argument order

**Hallucinated API:**
```csharp
// Variant 1: Invented enum (CalloutType does not exist)
new Callout("No to-do items.", CalloutType.Info)

// Variant 2: Correct enum but wrong argument position (CalloutVariant as 2nd arg instead of 3rd)
new Callout("Warning!", CalloutVariant.Destructive)
```

**Error:** `The type or namespace 'CalloutType' could not be found` (variant 1) or `CS1503: Argument 2: cannot convert from 'Ivy.CalloutVariant' to 'string?'` (variant 2)

**Correct API:**
```csharp
// Preferred: static factory methods
Callout.Info("No to-do items.")

// Constructor: (description, title, variant, icon) — title is the 2nd parameter, not variant
new Callout("Warning!", "Title", CalloutVariant.Destructive, Icons.AlertTriangle)
```

`Callout` uses static factory methods: `Callout.Info()`, `Callout.Warning()`, `Callout.Error()`, `Callout.Success()`. The `CalloutType` enum does not exist. When using the constructor directly, the parameter order is `(description, title, variant, icon)` — agents frequently put `CalloutVariant` as the 2nd argument where `title` (string) should be.

**Found In:**
bd5f45ac-569d-4be8-8ef8-882451e608a1
0c7c0b33-a500-45c2-911b-b33ca1f9662e
cdf77a72-658e-45df-9bdb-9bf7c79100b2

## AppAttribute.path old parameter name

**Hallucinated API:**
```csharp
[App("Dashboard", path: ["Dashboards"])]
```

**Error:** 'AppAttribute' does not contain a constructor that takes... / does not have a parameter named 'path'

**Correct API:**
```csharp
[App("Dashboard", group: ["Dashboards"])]
```

The path: parameter on AppAttribute was renamed to group: (Ivy-Framework#2587) because it is used to specify a group/category name in the sidebar. Agents trained on older data might still use path:. **Auto-fixed:** The refactoring service automatically rewrites path: to group: in [App] attributes.

**Found In:**
875efaff-8eb2-4604-b3aa-a2b5799df88c
a55e08b9-f212-49ef-97b9-d352b7b4beb8
798044de-edee-4bf9-85f1-291513fc076c

## TreeRowActionClickEventArgs on DataTable — wrong event args type

**Hallucinated API:**
```csharp
table.OnRowAction(e => {
    var tag = ((TreeRowActionClickEventArgs)e).Tag;
    var id = ((TreeRowActionClickEventArgs)e).Id;
});
```

**Error:** `CS1061: 'TreeRowActionClickEventArgs' does not contain a definition for 'Tag'/'Id'`

**Correct API:**
```csharp
table.OnRowAction(e => {
    var tag = e.Value.Tag;  // RowActionClickEventArgs.Tag
    var id = e.Value.Id;    // RowActionClickEventArgs.Id
});
```

DataTable's `OnRowAction` uses `Event<DataTable, RowActionClickEventArgs>`, not `TreeRowActionClickEventArgs`. Access properties via `e.Value.Tag` and `e.Value.Id`. The agent conflated `TreeRowActionClickEventArgs` (for `Tree` widget) with `RowActionClickEventArgs` (for `DataTable`).

**Found In:**
30c1b273-c528-4496-b194-c98e0ffeaa23
9d8f5446-43c4-44a2-b6ce-3caeff413407
16d32bb9-34f3-4b14-adf9-83f802350032

## UseAlert().ShowInfo() — wrong API usage

**Hallucinated API:**
```csharp
var alert = UseAlert();
alert.ShowInfo("title", "message");
```

**Error:** `'(IView? alertView, ShowAlertDelegate showAlert)' does not contain a definition for 'ShowInfo'`

**Correct API:**
```csharp
var (alertView, showAlert) = UseAlert();
showAlert("message", result => { }, "title", AlertButtonSet.Ok);
```

`UseAlert()` returns a tuple `(IView? alertView, ShowAlertDelegate showAlert)`, not an object with methods. Destructure the tuple and call the delegate directly. The `alertView` must be included in the returned view tree.

**Found In:**
276d383f-696e-4d67-bc6e-14502c59734b
c06ba6f6-2583-4fcc-81dc-f8da652471c6

## AppAttribute — PascalCase properties and invented parameters

**Hallucinated API:**
```csharp
[App(icon: Icons.Bot, group: new[] { "Apps" }, appShell: UseDefaultAppShell)]
[App(Icon = Icons.Waves)]
```

**Errors:**
- `CS0655: 'Icon' is not a valid named attribute argument` — PascalCase property used instead of constructor parameter
- `CS0246: The type or namespace name 'Group' could not be found` — parameter doesn't exist
- `CS0246: The type or namespace name 'AppShell' could not be found` — parameter doesn't exist

**Correct API:**
```csharp
[App(icon: Icons.Bot, group: new[] { "Apps" })]
```

The `AppAttribute` uses **lowercase named constructor parameters**, not PascalCase named properties. C# attributes with nullable property types cause CS0655 when accessed via `PropertyName = value` syntax. Use `parameterName: value` syntax instead.

Available parameters: `id`, `title`, `icon`, `description`, `group`, `isVisible`, `order`, `groupExpanded`, `documentSource`, `searchHints`. There is NO `appShell` parameter — configure app shell in `Program.cs` via `server.UseAppShell()`.

Note: `path` was renamed to `group` in v1.2.18 (Ivy-Framework#2587). See the `AppAttribute.path` entries above for details.

**Found In:**
7c547408-00b3-47e1-976e-59c9357c1e74
d6a5f377-bc84-404d-acca-71164d3754d4

## TextBuilder.Style() — non-existent styling method

**Hallucinated API:**
```csharp
Text.P("🐶").Style("font-size: 48px")
```

**Error:** `'TextBuilder' does not contain a definition for 'Style'`

**Correct API:**
```csharp
Text.P("🐶").Large()
Text.P("text").Medium()
Text.P("text").Small()
```

`TextBuilder` does not have a `.Style()` method for arbitrary CSS. Use `.Large()`, `.Medium()`, or `.Small()` fluent modifiers. The agent invented a CSS-style `.Style()` method similar to JSX `style` props. Variant of the documented `WithFontSize()` hallucination.

Also hallucinated: `Text.Code(expr).FontSize(24)` — CS1929: `.FontSize()` is an extension on `LabelList`, not `TextBuilder`.

**Found In:**
88e4f0bb-d358-4b34-9458-bc7eb98845e5, 625c285f-068b-4de3-b01c-ae2f7286a5d8

## TextBuilder.AlignCenter() / .Centered() — use .Center()

**Hallucinated API:**
```csharp
Text.H1("$0.00").AlignCenter()
Text.H1("title").Centered()
```

**Error:** `CS1061: 'TextBuilder' does not contain a definition for 'AlignCenter'` / `'Centered'`

**Correct API:**
```csharp
Text.H1("$0.00").Center()
```

`TextBuilder` now has a `.Center()` method (returns `Align(TextAlignment.Center)`). The agent sometimes hallucinates `.AlignCenter()` or `.Centered()` instead. The correct method name is `.Center()`.

**Found In:**
713546f7-32fb-4961-ab78-def91e7c010d, 5d2202d2-9d6b-4198-9922-c3763534aca5

## Table\<T\> — non-generic type used with type arguments

**Hallucinated API:**
```csharp
new Table<MyRecord>(items)
```

**Error:** `The non-generic type 'Table' cannot be used with type arguments`

**Correct API:**
```csharp
items.ToTable()
```

`Table` is non-generic. Use the `IEnumerable<T>.ToTable()` builder pattern to create a table from a collection. The type is inferred from the collection.

**Found In:**
a9ee3993-1cfb-4cba-9322-80a60b56c8d2
cab4c6bb-be1f-4fef-9d96-96c54e5f88ff

## Box.Opacity() — property used as method call

**Hallucinated API:**
```csharp
new Box(content).Opacity(0.3f)
```

**Error:** `CS1955: Non-invocable member 'Box.Opacity' cannot be used like a method.`

**Correct API:**
```csharp
// Use object initializer or with-expression:
new Box(content) { Opacity = 0.3f }

// Or use the Background extension which accepts an opacity parameter:
new Box(content).Background(Colors.Muted, 0.3f)
```

`Box.Opacity` is a property (`float?`), not a method. There is no `.Opacity()` extension method on `Box`. Use object initializer syntax or the `Background(Colors, float)` extension which sets both background color and opacity. This is the same CS1955 pattern as the `Image.ObjectFit("cover")` hallucination — the agent treats a settable property as a fluent method.

**Found In:**
15313dc3-1c7d-4af9-8998-8338a837d5fb

**Found In:**
7c547408-00b3-47e1-976e-59c9357c1e74

## WithMargin(top: 4) — Named parameters don't exist

**Hallucinated API:**
```csharp
widget.WithMargin(top: 4)
```

**Error:** `CS7036: There is no argument given that corresponds to the required parameter 'left' of 'LayoutExtensions.WithMargin(object, int, int, int, int)'`

**Correct API:**
```csharp
// WithMargin has three overloads, all with positional parameters:
widget.WithMargin(4)            // uniform margin
widget.WithMargin(4, 2)         // horizontal, vertical
widget.WithMargin(0, 4, 0, 0)   // left, top, right, bottom
```

**Found In:**
2e18b175-94ec-459c-94a5-8f28b81ecfdc
6c834561-6c01-424b-b8fb-a4a473c1c86a

## TextInputBase.Lines() — non-existent multi-line property

**Hallucinated API:**
```csharp
var text = UseState("");
text.ToTextInput().Lines(8)
```

**Error:** `CS1061: 'TextInputBase' does not contain a definition for 'Lines'`

**Correct API:**
```csharp
var text = UseState("");
text.ToTextareaInput()
// or equivalently:
text.ToTextInput().Multiline()
```

There is no `.Lines()` method. Use `ToTextareaInput()` or `ToTextInput().Multiline()` for multi-line text input. The textarea height can be controlled via `.Height()` on the widget.

**Found In:**
857de09c-ab87-49a5-aac4-394f7d0aa207
edd92ecc-6378-440a-b9cf-bb8e1cb29de9

## Edge — Non-existent margin edge enum

**Hallucinated API:**
```csharp
widget.Margin(Edge.Top, 4)
```

**Error:** `CS0103: The name 'Edge' does not exist in the current context`

**Correct API:**
```csharp
// Use WithMargin with positional int parameters (left, top, right, bottom):
widget.WithMargin(0, 4, 0, 0) // top margin of 4

// Or use Layout.Margin:
Layout.Vertical().Margin(0, 4, 0, 0) | widget
```

**Found In:**
2e18b175-94ec-459c-94a5-8f28b81ecfdc
e7fbe763-a413-4197-ae24-e706325de1eb

## Align used where TextAlignment expected — type confusion

**Hallucinated API:**
```csharp
Text.H1("0").Align(Align.Right)  // passing Ivy.Align instead of Ivy.TextAlignment
```

**Error:** `CS1503: Argument 1: cannot convert from 'Ivy.Align' to 'Ivy.TextAlignment'`

**Correct API:**
```csharp
Text.H1("0").Align(TextAlignment.Right)
```

`Align` (layout alignment for positioning elements) and `TextAlignment` (text alignment within a text block) are two different enums. `Align` has values like `TopLeft`, `Center`, `Stretch`, etc. `TextAlignment` has values `Left`, `Center`, `Right`, `Justify`. Methods accepting text alignment require `TextAlignment`, not `Align`.

**Found In:**
2739aa95-7d5b-481b-a456-af895e6268df
e7fbe763-a413-4197-ae24-e706325de1eb

## ListItem.Description / ListItem.Meta / ListItem.Actions — non-existent members

**Hallucinated API:**
```csharp
ListItem.Description("text")
ListItem.Meta("text")
ListItem.Actions(button1, button2)
```

**Error:** `'ListItem' does not contain a definition for 'Description'/'Meta'/'Actions'`

**Correct API:**
`ListItem` is a record with constructor parameters: `title`, `subtitle`, `onClick`, `icon`, `badge`, `tag`, `items`. Use `subtitle` for descriptions. There are no `.Description()`, `.Meta()`, or `.Actions()` methods. The only extension method is `.Content(child)`.

Source: `D:\Repos\_Ivy\Ivy-Framework\src\Ivy\Widgets\Lists\ListItem.cs`

**Found In:**
a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 008, 009)
8b101b29-228f-40d2-90e9-79aa54fed653

## OnClick() on non-clickable widgets — extension method receiver mismatch

**Hallucinated API:**
```csharp
myCustomView.OnClick(e => ...)
new LayoutView().OnClick(e => ...)
```

**Error:** `CS1929: 'MyView' does not contain a definition for 'OnClick' and the best extension method overload requires a receiver of type 'Card'/'Button'/'Badge'`

**Correct API:**
```csharp
// OnClick is only available on specific widgets: Card, Button, Badge, Image, Box
// For custom click handling, wrap in a Box or use a Button:
new Box(myCustomView).OnClick(e => ...)
// Or use a Card:
new Card(myCustomView).OnClick(e => ...)
```

**Found In:**
5c9cfb70-c9f5-4642-8de6-480be8f5ee85
8b101b29-228f-40d2-90e9-79aa54fed653

## Table.Columns() — non-existent fluent method

**Hallucinated API:**
```csharp
new Table(data).Columns("Name", "Age", "City")
```

**Error:** `CS1061: 'Table' does not contain a definition for 'Columns' and no accessible extension method 'Columns' accepting a first argument of type 'Table' could be found`

**Correct API:**
```csharp
// Table takes TableRow[] in its constructor. Build rows manually:
var headerRow = new TableRow(new TableCell("Name"), new TableCell("Age"), new TableCell("City")).IsHeader();
var dataRow = new TableRow(new TableCell("Alice"), new TableCell("30"), new TableCell("NYC"));
new Table(headerRow, dataRow)

// Or use the pipe operator:
var table = new Table()
    | new TableRow(new TableCell("Name"), new TableCell("Age")).IsHeader()
    | new TableRow(new TableCell("Alice"), new TableCell("30"));
```

`Table` is a simple HTML table container that takes `TableRow[]` in its constructor. There is no `.Columns()` fluent method. Column headers are defined by adding a `TableRow` with `.IsHeader()`. For typed/model-based tables, use `TableBuilder<T>` or `TableView<T>` instead.

**Found In:**
27c08f6f-438d-421d-9625-4646918e1c33
ca7c2127-8ed0-498f-8cf5-7ba23a38910c

## Button onClick — wrong callback signature (method group)

**Hallucinated API:**
```csharp
async Task GenerateEmbedding() { ... }
new Button("Generate Embedding", GenerateEmbedding)
```

**Error:** `Argument 2: cannot convert from 'method group' to 'System.Func<Ivy.Event<Ivy.Button>, System.Threading.Tasks.ValueTask>?'`

**Correct API:**
```csharp
async ValueTask GenerateEmbedding(Event<Button> e) { ... }
new Button("Generate Embedding", GenerateEmbedding)

// Or inline:
new Button("Generate", async (e) => { await DoWork(); })
```

The `Button` onClick parameter is `Func<Event<Button>, ValueTask>?`. The callback must: (1) accept `Event<Button>` as parameter, (2) return `ValueTask` (not `Task` or `void`). A method group with wrong signature (e.g., `async Task Foo()`) will fail with CS1503.

**Found In:**
55eafb82-2cc2-48ba-9a66-cd2ed8d38d67

## LayoutView.MaxWidth() — non-existent method

**Hallucinated API:**
```csharp
Layout.Vertical().MaxWidth(Size.Lg)
```

**Error:** `'LayoutView' does not contain a definition for 'MaxWidth'`

**Correct API:**
```csharp
Layout.Vertical().Width(Size.Lg)
```

`LayoutView` does not have a `.MaxWidth()` method. Use `.Width(Size)` instead.

**Found In:**
a9ee3993-1cfb-4cba-9322-80a60b56c8d2

## LayoutView.SpaceBetween() — non-existent method

**Hallucinated API:**
```csharp
Layout.Horizontal().SpaceBetween()
```

**Error:** `'LayoutView' does not contain a definition for 'SpaceBetween'` (CS1061)

**Correct API:**
```csharp
Layout.Horizontal(Align.SpaceBetween)
```

`SpaceBetween` is an `Align` enum value passed to the layout constructor, not a fluent method. The same applies to `SpaceAround` and `SpaceEvenly`.

**Found In:**
f6d6e841-9a14-4475-9fa5-0791be30e578

## Callout.Destructive() — fluent method on constructor instance

**Hallucinated API:**
```csharp
new Callout("Error message").Destructive()
```

**Error:** `'Callout' does not contain a definition for 'Destructive'`

**Correct API:**
```csharp
Callout.Error("Error message")
```

`Callout` uses static factory methods (`Callout.Info()`, `Callout.Warning()`, `Callout.Error()`, `Callout.Success()`), not a constructor + fluent style chain. `.Destructive()` is a `Button` style method — the agent confused the two APIs. No auto-fix is possible because the intent (error vs warning vs info) is ambiguous.

**Found In:**
d9116efb-830e-484a-a258-fc3193769158

## TextInputBase.OnEnter() — invented fluent method

**Hallucinated API:**
```csharp
newItemText.ToTextInput().Placeholder("Add a new to-do...").OnEnter(AddTodo)
```

**Error:** `'TextInputBase' does not contain a definition for 'OnEnter'`

**Correct API:**
`.OnEnter()` does not exist on `TextInput`. Use `OnSubmit()` to handle enter-key submission:
```csharp
text.ToTextInput().OnSubmit(() => DoSomething())
```

**Found In:**
bd5f45ac-569d-4be8-8ef8-882451e608a1

## TextInputVariants — old plural enum name

**Hallucinated API:**
```csharp
new TextInput(text.Value, e => text.Set(e.Value)).Variant(TextInputVariants.Textarea)
```

**Error:** `The name 'TextInputVariants' does not exist in the current context`

**Correct API:**
```csharp
text.ToTextInput().Variant(TextInputVariant.Textarea)
```

The enum is `TextInputVariant` (singular), not `TextInputVariants` (plural). All input variant enums were renamed from plural to singular in Ivy-Framework#2546 (e.g., `TextInputVariants` → `TextInputVariant`, `ColorInputVariants` → `ColorInputVariant`, etc.). **Auto-fixed:** The refactoring service automatically rewrites `TextInputVariants` → `TextInputVariant`. Values: `Text`, `Textarea`, `Email`, `Tel`, `Url`, `Password`, `Search`.

**Found In:**
4a94f8f6-865d-4663-8f4c-d4c09913398f

## SelectInputVariants — old plural enum name

**Hallucinated API:**
```csharp
state.ToSelectInput().Variant(SelectInputVariants.Toggle)
```

**Error:** `The name 'SelectInputVariants' does not exist in the current context`

**Correct API:**
```csharp
state.ToSelectInput().Variant(SelectInputVariant.Toggle)
```

The enum is `SelectInputVariant` (singular), not `SelectInputVariants` (plural). All input variant enums were renamed from plural to singular in Ivy-Framework#2546. Values: `Select`, `Toggle`, `Radio`, `Checkbox`. **Auto-fixed:** The refactoring service automatically rewrites `SelectInputVariants` → `SelectInputVariant`.

**Found In:**
a55e08b9-f212-49ef-97b9-d352b7b4beb8

## Event<T,E>.Data — non-existent property

**Hallucinated API:**
```csharp
args.Data.Id
args.Data.Tag
```

**Error:** `'Event<DataTable, RowActionClickEventArgs>' does not contain a definition for 'Data'`

**Correct API:**
```csharp
args.Value.Id
args.Value.Tag
```

`Event<TSender, TValue>` uses `.Value` to access the event args, not `.Data`. The agent likely confused this with other event patterns from different frameworks (e.g., WPF `DataContext`, JavaScript `event.data`).

**Found In:**
f20dced8-1689-4289-a2d8-ee67136eb6ce

## UseState\<T?\>(null) — ambiguous overload call

**Hallucinated API:**
```csharp
var selectedItem = UseState<InventoryItem?>(null);
```

**Error:** `The call is ambiguous between 'ViewBase.UseState<T>(T?, bool)' and 'ViewBase.UseState<T>(Func<T>, bool)'`

**Correct API:**
```csharp
// Best: omit the null argument — the default is already null:
var selectedItem = UseState<InventoryItem?>();
// Or cast null to the explicit type:
var selectedItem = UseState<InventoryItem?>((InventoryItem?)null);
// Or use a lambda:
var selectedItem = UseState(() => (InventoryItem?)null);
```

When `T` is a reference type, `null` matches both `T?` and `Func<T>`, causing overload ambiguity. The simplest fix is to omit the `null` argument entirely — the default parameter is already `null`/`default`. Alternatively, cast null to the explicit type or wrap it in a lambda.

**Note:** Unlike `IState<T>.Set(null)` (which was fixed via `[OverloadResolutionPriority(1)]`), `UseState` cannot use the same approach because T is inferred from the argument — C# 10+ lambda natural types cause the `T?` overload to steal ALL lambda calls when given higher priority, breaking `UseState(() => expr)` throughout the codebase.

**Found In:**
f20dced8-1689-4289-a2d8-ee67136eb6ce

## Tab.Content() — non-existent fluent method

**Hallucinated API:**
```csharp
new Tab("Customer Info").Content(
    Layout.Vertical() | ...
)
```

**Error:** `'Tab' does not contain a definition for 'Content' and the best extension method overload 'ButtonExtensions.Content(Button, object)' requires a receiver of type 'Ivy.Button'`

**Correct API:**
```csharp
new Tab("Customer Info", Layout.Vertical() | ...)
```

`Tab` takes content as the second constructor parameter: `Tab(string title, object? content = null)`. There is no `.Content()` fluent method. This is the same pattern as `ListItem.Content()` — the agent invents fluent `.Content()` methods on widgets that accept content through constructors.

**Note:** The IvyQuestion MCP tool also hallucinated this same API, returning `.Content()` as valid in two separate answers, reinforcing the agent's mistake.

**Found In:**
41ae072b-2845-46f1-bd0b-a4a6370c6807

## Layout.Tabs() | Tab — pipe operator on TabView

**Hallucinated API:**
```csharp
Layout.Tabs()
    | customerInfoTab
    | yourInfoTab
```

**Error:** `Operator '|' cannot be applied to operands of type 'TabView' and 'Tab'`

**Correct API:**
```csharp
Layout.Tabs(customerInfoTab, yourInfoTab)
```

The `|` pipe operator works on `LayoutView` (for composing children) but does NOT exist on `TabView`. Tabs must be passed as constructor arguments via `Layout.Tabs(params Tab[] tabs)`.

**Found In:**
41ae072b-2845-46f1-bd0b-a4a6370c6807

## FormBuilder.Header() — non-existent method

**Hallucinated API:**
```csharp
entity.ToForm()
    .Header("Edit Fund")
    .Field(f => f.Name)
```

**Error:** `'FormBuilder<T>' does not contain a definition for 'Header'`

**Correct API:**
```csharp
entity.ToForm()
    .Field(f => f.Name)
    .ToSheet(title: "Edit Fund")
```

`FormBuilder` does not have a `.Header()` method. The title/header is set when converting the form to a dialog or sheet via `.ToDialog(title:)` or `.ToSheet(title:)`. The agent confused this with `Card.Header()` or `BladeHeader`.

**Found In:**
d90474ac-78b9-48c7-8317-3860ff36b9dd (sub-tasks 002, 003)

## Callout.Color(Colors.X) — non-existent fluent method

**Hallucinated API:**
```csharp
new Callout("Error message").Color(Colors.Destructive)
```

**Error:** `'Callout' does not contain a definition for 'Color' and the best extension method overload 'LabelExtensions.Color(Label, Colors)' requires a receiver of type 'Ivy.Label'`

**Correct API:**
```csharp
Callout.Error("Error message")
Callout.Warning("Warning message")
Callout.Info("Info message")
Callout.Success("Success message")
```

`Callout` uses static factory methods, not a constructor + `.Color()` chain. This is a variant of the documented `Callout.Destructive()` hallucination — both stem from the agent trying to apply fluent styling to Callout instead of using the static factory pattern. To change variant after creation, use `.Variant(CalloutVariant.Warning)`.

**Found In:**
3c507fb4-71e1-4136-9d40-8eca6590250d

## Spacer(int) constructor — non-existent constructor overload

**Hallucinated API:**
```csharp
new Spacer(6)
new Spacer(2)
new Spacer(4)
```

**Error:** `'Spacer' does not contain a constructor that takes 1 arguments`

**Correct API:**
```csharp
new Spacer().Height(Size.Units(6))
// or
new Spacer().Width(Size.Units(6))
```

Spacer has only a parameterless constructor. Use fluent `.Height()` or `.Width()` to set size.

**Found In:**
276d383f-696e-4d67-bc6e-14502c59734b

## Button.Color(Colors.X) — non-existent fluent method

**Hallucinated API:**
```csharp
new Button(label).Color(colors[i])
```

**Error:** `'Button' does not contain a definition for 'Color' and the best extension method overload 'LabelExtensions.Color(Label, Colors)' requires a receiver of type 'Ivy.Label'`

**Correct API:**
Button doesn't have `.Color()`. Use `.Variant(ButtonVariant.X)` or fluent shortcuts like `.Primary()`, `.Destructive()`. `.Color()` only exists on `Label` via `LabelExtensions`. Variant of documented `Badge.Color()` and `Callout.Color()` patterns.

**Found In:**
276d383f-696e-4d67-bc6e-14502c59734b

## Button.Danger() — non-existent fluent method

**Hallucinated API:**
```csharp
new Button("Remove").Danger()
```

**Error:** `CS1061: 'Button' does not contain a definition for 'Danger'`

**Correct API:**
```csharp
new Button("Remove").Destructive()
```

Button uses `.Destructive()` (from `ButtonVariant.Destructive`), not `.Danger()`. The agent confused CSS/Bootstrap "danger" naming with Ivy's "destructive" terminology. Related: `Button.Color()` is also hallucinated — Button doesn't have `.Color()` either.

**Found In:**
62ecf7b7-a59e-46ae-8131-db9044c391df

## Size.Flex() — non-existent static method

**Hallucinated API:**
```csharp
new Spacer().Width(Size.Flex())
.Height(Size.Flex())
```

**Error:** `'Size' does not contain a definition for 'Flex'`

**Correct API:**
```csharp
new Spacer().Width(Size.Grow())
```

The agent confused CSS flexbox terminology with Ivy's API.

**Found In:**
276d383f-696e-4d67-bc6e-14502c59734b

## RefreshToken.Version — non-existent property

**Hallucinated API:**
```csharp
refreshToken.Version
```

**Error:** `'RefreshToken' does not contain a definition for 'Version'`

**Correct API:**
`RefreshToken` has these members: `Token` (Guid), `ReturnValue` (object?), `IsRefreshed` (bool), `Refresh()`, `ToTrigger()`. There is no `Version` property. Pass `refreshToken` directly as a dependency to `UseQuery`, or use `refreshToken.Token` if you need a changing value.

Source: `D:\Repos\_Ivy\Ivy-Framework\src\Ivy\Hooks\UseRefreshToken.cs`

**Found In:**
a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 002-005, 008-009)

## QueryResult\<T\>.Data — wrong property name

**Hallucinated API:**
```csharp
queryResult.Data
```

**Error:** `'QueryResult<T>' does not contain a definition for 'Data'`

**Correct API:**
`queryResult.Value` — The property is `.Value`, not `.Data`. `QueryResult<T>` is a record with: `Value` (T?), `Loading` (bool), `Validating` (bool), `Previous` (bool), `Mutator` (QueryMutator<T>), `Error` (Exception?).

Source: `D:\Repos\_Ivy\Ivy-Framework\src\Ivy\Hooks\UseQuery.cs`

**Found In:**
a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 002, 004)

## QueryResult\<T\>.IsLoading — wrong property name

**Hallucinated API:**
```csharp
queryResult.IsLoading
```

**Error:** `'QueryResult<T>' does not contain a definition for 'IsLoading'`

**Correct API:**
`queryResult.Loading` — The property is `.Loading`, not `.IsLoading`. Similarly, `.Validating` not `.IsValidating`, and `.Previous` not `.IsPrevious`.

**Found In:**
a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 002, 004)

## QueryMutator.Trigger() / .IsLoading / .Error — non-existent properties

**Hallucinated API:**
```csharp
var mutation = UseMutation(key);
mutation.Trigger();    // doesn't exist
mutation.IsLoading     // doesn't exist
mutation.Error         // doesn't exist
```

**Error:** `CS1061: 'QueryMutator' does not contain a definition for 'Trigger'/'IsLoading'/'Error'`

**Correct API:**
`QueryMutator` only has `Revalidate` (Action) and `Invalidate` (Action). `QueryMutator<T>` adds `Mutate` (MutateDelegate<T>). For loading state and error, use `QueryResult<T>` from `UseQuery()`, which has `.Loading`, `.Error`, and `.Value`.

For async operations triggered by a button click, use the button's async `OnClick` handler directly:
```csharp
new Button("Validate", async () => {
    result = await service.ValidateAsync(input);
})
```

Source: `D:\Repos\_Ivy\Ivy-Framework\src\Ivy\Hooks\UseQuery.cs`

**Found In:**
857de09c-ab87-49a5-aac4-394f7d0aa207

## Size.Sm — non-existent member

**Hallucinated API:**
```csharp
Size.Sm
```

**Error:** `'Size' does not contain a definition for 'Sm'`

**Correct API:**
`Size` does not have Tailwind-style size aliases like `Sm`, `Md`, `Lg`. Use `Size.Units(n)` for specific pixel values, or `Size.Full()`, `Size.Grow()`, `Size.Fit()` for relative sizing.

**Found In:**
a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 008, 009)

## String literal as Icons? — wrong type

**Hallucinated API:**
```csharp
// Using string literals like "edit", "delete", "trash" where Icons? is expected
new RowAction("Edit", icon: "edit")
```

**Error:** `Cannot implicitly convert type 'string' to 'Ivy.Icons?'`

**Correct API:**
Always use the `Icons` enum: `Icons.Pencil`, `Icons.Trash2`, `Icons.Plus`, etc. There is no implicit conversion from string to Icons. The refactoring service already handles invalid Icons enum values via LLM-based matching, but it cannot fix string-to-enum type mismatches.

**Found In:**
a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 002, 003, 005, 008)

## Text.Small("text") — static factory confusion

**Hallucinated API:**
```csharp
Text.Small(frequencyText).Muted()
```

**Error:** `No overload for method 'Small' takes 1 arguments`

**Correct API:**
```csharp
Text.P(frequencyText).Small().Muted()
// or
Text.Block(frequencyText).Small().Muted()
```

`Small()` is an instance modifier on `TextBuilder` (returns `Scale(Ivy.Scale.Small)`), not a static factory. The static factories are `Text.P()`, `Text.H1()`, `Text.H2()`, `Text.H3()`, `Text.H4()`, `Text.Block()`, `Text.Label()`, etc. Chain `.Small()` after creating the text.

**Found In:**
ce144de9-0688-490a-bef6-b2766e323154

## Box.BorderRadius(int) — wrong argument type

**Hallucinated API:**
```csharp
new Box(content).BorderRadius(8)
```

**Error:** `'Box' does not contain a definition for 'BorderRadius'` (CS1929 — no extension matches `Box.BorderRadius(int)`)

**Correct API:**
```csharp
new Box(content).BorderRadius(BorderRadius.Rounded)
```

`Box.BorderRadius()` takes a `BorderRadius` enum (`None`, `Rounded`, `Full`), not an integer. The agent ignored the IvyQuestion MCP response and used an int literal instead.

**Found In:**
ce144de9-0688-490a-bef6-b2766e323154

## GridView.Background() — non-existent method

**Hallucinated API:**
```csharp
Layout.Grid(items).Columns(8).Gap(1).Background(Colors.Slate)
```

**Error:** `'GridView' does not contain a definition for 'Background'`

**Correct API:**
```csharp
new Box(
    Layout.Grid(items).Columns(8).Gap(1)
).Color(Colors.Slate)
```

`GridView` does not have a `.Background()` method. To add a background color to a grid, wrap it in a `Box` and use `.Color()` on the Box. This pattern applies to any view that needs a background color — `Box` is the universal container for adding visual styling.

**Found In:**
7e97011f-41b3-42d3-98ea-3b7faad347c2

## GridView.AddChildren() — non-existent method

**Hallucinated API:**
```csharp
var grid = new GridView();
grid.AddChildren(widget1, widget2);
```

**Error:** `CS1061: 'GridView' does not contain a definition for 'AddChildren'`

**Correct API:**
```csharp
// Use the .Children() extension to replace children:
new GridView(columns: 8).Children(widget1, widget2);
// Or use the pipe operator to append children:
var grid = new GridView(columns: 8);
grid | widget1 | widget2;
// Or pass children in constructor:
new GridView(columns: 8, children: new[] { widget1, widget2 });
```

**Found In:**
5c9cfb70-c9f5-4642-8de6-480be8f5ee85

## TableBuilder\<T\>.OnClick() / IBuilderFactory\<T\>.Actions() — non-existent table interactivity methods

**Hallucinated API:**
```csharp
// Trying to add click handler to table rows via builder:
db.Tasks.ToTable()
    .OnClick(row => bladeService.Push(...))

// Trying to add action buttons via builder factory:
db.Lines.ToTable()
    .Actions(new Button("Edit").OnClick(...))
```

**Error:** `CS1929: 'TableBuilder<T>' does not contain a definition for 'OnClick'` / `CS1061: 'IBuilderFactory<T>' does not contain a definition for 'Actions'`

**Correct API:**
```csharp
// Tables don't have row-click handlers via builder.
// For clickable rows, use a List with ListItem instead:
items.Select(item => new ListItem(
    title: item.Name,
    onClick: _ => bladeService.Push(new DetailBlade(item.Id))
))

// For row actions in DataTable, use DataTableBuilder.RowActions():
db.Items.ToDataTable()
    .RowActions(row => new Button("Edit").OnClick(_ => ...))
```

`TableBuilder<T>` (from `.ToTable()`) does not support `.OnClick()` or `.Actions()`. The agent confused `Table` (simple display widget) with `DataTable` (which supports `RowActions`). For row-level click behavior, use `ListItem` with `onClick`. For row actions, use `DataTableBuilder.RowActions()`.

**Found In:**
8b101b29-228f-40d2-90e9-79aa54fed653

## Tab(title, icon, content) — 3-argument constructor

**Hallucinated API:**
```csharp
new Tab("Rate Calculator", Icons.Calculator, rateCalculatorContent)
```

**Error:** `CS1729: 'Tab' does not contain a constructor that takes 3 arguments`

**Correct API:**
```csharp
new Tab("Rate Calculator", rateCalculatorContent).Icon(Icons.Calculator)
```

`Tab` constructor is `Tab(string title, object? content = null)` — only 2 parameters. Icons are set via the `.Icon(Icons.X)` fluent method, not as a constructor argument. This is the same pattern as the `Button("text", Icons.X)` hallucination — the agent passes an icon as a positional constructor argument.

**Found In:**
3094f74c-2d04-41eb-b6d5-2b33b68cbcd8

## Button.WithIcon() — non-existent fluent method

**Hallucinated API:**
```csharp
new Button("Get Rate Quote").WithIcon(Icons.Search)
```

**Error:** `CS1061: 'Button' does not contain a definition for 'WithIcon'`

**Correct API:**
```csharp
new Button("Get Rate Quote").Icon(Icons.Search)
```

The correct method is `.Icon(Icons.X)`, not `.WithIcon(Icons.X)`. The agent conflates `.WithField()` / `.WithMargin()` naming patterns with the simpler `.Icon()` method. Related to `Button("text", Icons.X)` positional icon hallucination — different syntax, same confusion about how to set icons on buttons.

**Found In:**
3094f74c-2d04-41eb-b6d5-2b33b68cbcd8

## SelectOption — non-existent type

**Hallucinated API:**
```csharp
new SelectOption("value", "Display Text")
var options = new List<SelectOption> { ... };
```

**Error:** `CS0246: The type or namespace name 'SelectOption' could not be found`

**Correct API:**
```csharp
new Option<string>("value", "Display Text")
// or for enum-based:
state.ToSelectInput(options: Enum.GetValues<MyEnum>())
```

`SelectOption` does not exist. The correct type for select input options is `Option<T>`. When using `SelectInput` with custom options, pass `IEnumerable<Option<T>>` to the `.Options()` method or use the `options:` parameter.

**Found In:**
3094f74c-2d04-41eb-b6d5-2b33b68cbcd8

## Size.Pixels() — wrong method name

**Hallucinated API:**
```csharp
Size.Pixels(280)
```

**Error:** `'Size' does not contain a definition for 'Pixels'`

**Correct API:**
```csharp
Size.Px(280)
```

The method is `Size.Px()`, not `Size.Pixels()`. The agent expanded the abbreviated name. **Auto-fixed:** The refactoring service automatically rewrites `Size.Pixels(...)` → `Size.Px(...)`.

**Found In:**
7c51c481-c48e-4398-8db3-60cfac6379d5 (trace 002)

## string.ToCodeInput() — wrong receiver type

**Hallucinated API:**
```csharp
responseBody.Value.ToCodeInput().Language(Languages.Json)
responseHeaders.Value.ToCodeInput().Language(Languages.Text)
```

**Error:** `'string' does not contain a definition for 'ToCodeInput' and the best extension method overload 'CodeInputExtensions.ToCodeInput(IAnyState, ...)' requires a receiver of type 'Ivy.IAnyState'`

**Correct API:**
```csharp
// For read-only display of code, use CodeBlock:
new CodeBlock(stringValue, Languages.Json)

// For editable code input, bind to state first:
var editableState = UseState(stringValue);
editableState.ToCodeInput().Language(Languages.Json)
```

`.ToCodeInput()` is an extension on `IAnyState`, not on `string`. For display-only code, use `CodeBlock` instead of a code input. Only use `.ToCodeInput()` when the user needs to edit the code, and bind the string to state first.

**Found In:**
535f38d4-b9d5-43bf-a3d9-b4b17e6ecbb0

## State\<T\> — non-existent type

**Hallucinated API:**
```csharp
private State<List<Player>> _players;
```

**Error:** `The type or namespace name 'State<>' could not be found`

**Correct API:**
```csharp
var players = UseState(new List<Player>());
```

`State<T>` does not exist. `UseState<T>()` returns `IState<T>`. State is created inside `Build()` via hooks, not stored as fields.

**Found In:**
84faf65a-c7df-4b5a-888b-4c49255c50ab (traces 004, 006, 008)

## IRefreshToken — non-existent interface

**Hallucinated API:**
```csharp
private readonly IRefreshToken _refreshToken;
```

**Error:** `The type or namespace name 'IRefreshToken' could not be found`

**Correct API:**
```csharp
var refreshToken = UseRefreshToken();
```

`IRefreshToken` does not exist. `UseRefreshToken()` returns a `RefreshToken` class. Like all hooks, call inside `Build()`.

**Found In:**
84faf65a-c7df-4b5a-888b-4c49255c50ab (traces 004, 005, 006)

## Image.ObjectFit("cover") — property used as method call

**Hallucinated API:**
```csharp
new Image(url).ObjectFit("cover")
```

**Error:** `CS1955: Non-invocable member 'Image.ObjectFit' cannot be used like a method.`

**Correct API:**
```csharp
new Image(url) { ObjectFit = ImageFit.Cover }
```

`Image.ObjectFit` is a property of type `ImageFit?`, not a method. Use object initializer syntax with the `ImageFit` enum (`Cover`, `Contain`, `Fill`, `None`, `ScaleDown`). The agent confused property-setter syntax with a fluent method call, and used a string argument instead of the `ImageFit` enum.

**Found In:**
86908281-cc6f-4973-a9c7-1c0186c013d2

## DataTable\<T\> — non-generic type used with type arguments

**Hallucinated API:**
```csharp
new DataTable<Player>(players)
```

**Error:** `The non-generic type 'DataTable' cannot be used with type arguments`

**Correct API:**
```csharp
players.ToDataTable()
```

`DataTable` is non-generic. Use `.ToDataTable()` extension method on `IEnumerable<T>` or `IQueryable<T>`.

**Found In:**
84faf65a-c7df-4b5a-888b-4c49255c50ab (trace 004)

## Shrink(int) — method takes no arguments

**Hallucinated API:**
```csharp
Text.P("vs").Shrink(1)
```

**Error:** `No overload for method 'Shrink' takes 1 arguments`

**Correct API:**
```csharp
Text.P("vs").Shrink()
```

`.Shrink()` takes no arguments. It is a simple fluent modifier.

**Found In:**
84faf65a-c7df-4b5a-888b-4c49255c50ab (traces 004, 007)

## Card.Padding() — non-existent method

**Hallucinated API:**
```csharp
new Card(content).Padding(20)
```

**Error:** `'Card' does not contain a definition for 'Padding'`

**Correct API:**
```csharp
new Box(content).Padding(20)
```

`Card` has no `.Padding()` method. Cards have built-in padding. For custom padding, wrap content in a `Box`.

**Found In:**
84faf65a-c7df-4b5a-888b-4c49255c50ab (trace 004)

## SelectInput<T>.Width() — generic constraint mismatch

**Hallucinated API:**
```csharp
language.ToSelectInput(options).Width(Size.Px(200))
```

**Error:** `CS0311: The type 'Ivy.SelectInput<string>' cannot be used as type parameter 'T' in the generic type or method 'WidgetBaseExtensions.Width<T>(T, Size?)'`

**Correct API:**
```csharp
// Cast to SelectInputBase first:
(SelectInputBase)language.ToSelectInput(options).Width(Size.Px(200))
// Or wrap in a Box with width:
new Box(language.ToSelectInput(options)).Width(Size.Px(200))
```

`SelectInput<T>` inherits from `SelectInputBase : WidgetBase<SelectInputBase>`, not `WidgetBase<SelectInput<T>>`. The `Width<T>()` extension requires `T : WidgetBase<T>`, which `SelectInput<T>` doesn't satisfy.

### Found In
852f6bec-756c-48f8-93da-ad426af73fab

## MetricCard — non-existent class name

**Hallucinated API:**
```csharp
new MetricCard("Title", "Value", Icons.Activity)
```

**Error:** `CS0246: The type or namespace name 'MetricCard' could not be found`

**Correct API:**
```csharp
new MetricView("Title", "Value", icon: Icons.Activity)
```

`MetricCard` does not exist. The correct class is `MetricView`. Constructor: `MetricView(string title, string value, string? description = null, Icons? icon = null, IView? chart = null)`.

**Found In:**
c008af27-1cb1-4ab3-b41a-36aa711c6a41

## Disposable.Create() — missing using statement

**Hallucinated usage (missing using):**
```csharp
return Disposable.Create(() => timer?.Dispose());
```

**Error:** `CS0103: The name 'Disposable' does not exist in the current context`

**Fix:** Add the using statement — the package IS available as a transitive dependency:
```csharp
using System.Reactive.Disposables;

return Disposable.Create(() => timer?.Dispose());
```

`System.Reactive` is a transitive dependency of Ivy Framework. The error occurs because the agent omits the `using System.Reactive.Disposables;` directive, not because the package is missing.

**Found In:**
fb184b5b-8254-4a1f-b8f2-ab8e8657fdbc

## Button.Visible() / Widget.Visible() — removed conditional rendering method

**Hallucinated API:**
```csharp
new Button("Reset").Visible(hasDate)
```

**Error:** `'Button' does not contain a definition for 'Visible'` (CS1061)

**Correct API:**
```csharp
// Use a simple if statement for conditional rendering:
if (hasDate)
    yield return new Button("Reset");

// Or use a ternary:
var resetButton = hasDate ? new Button("Reset") : null;
```

The `.Visible()` extension method was removed from `WidgetBase` (commit f869df302). `LayoutView.Visible()` was also removed. The only remaining `.Visible()` is `FormBuilder<TModel>.Visible(field, predicate)` which controls form field visibility — not widget rendering. The agent confuses this with the old WidgetBase API or UI frameworks like WPF/WinForms that have a `Visible` property. In Ivy, conditional rendering is done with standard C# control flow (`if`, ternary, etc.) like in React.

**Found In:**
18763683-ff01-4f76-8dc5-6f0bfe750e4a

## Card.Secondary() — Badge extension used on Card

**Hallucinated API:**
```csharp
new Card(...).Secondary()
```

**Error:** `CS1929: 'Card' does not contain a definition for 'Secondary' and the best extension method overload 'BadgeExtensions.Secondary(Badge)' requires a receiver of type 'Ivy.Badge'`

**Correct API:**
```csharp
// Cards don't have variants. To style card content, style the children:
new Card(new Text("Content").Secondary())
// Or use a Box with background:
new Box(content).Background(Colors.Gray100)
```

**Found In:**
ab38eba1-af47-4003-905b-4fe9cea8ba4f

## Card.Child — Non-existent property

**Hallucinated API:**
```csharp
Card.Child(content)
// or
new Card { Child = content }
```

**Error:** `CS0117: 'Card' does not contain a definition for 'Child'`

**Correct API:**
```csharp
// Use the constructor, pipe operator, or .Content():
new Card(content)
new Card() | content
new Card().Content(content)
```

**Found In:**
2e18b175-94ec-459c-94a5-8f28b81ecfdc

## Card.Background() — Box extension used on Card

**Hallucinated API:**
```csharp
new Card(...).Background(Colors.Gray100)
```

**Error:** `CS1929: 'Card' does not contain a definition for 'Background' and the best extension method overload 'BoxExtensions.Background(Box, Colors)' requires a receiver of type 'Ivy.Box'`

**Correct API:**
```csharp
// Wrap in a Box for background color:
new Box(new Card(content)).Background(Colors.Gray100)
// Or use Card's built-in styling via content:
new Card(content)
```

Similar to the GridView.Background() hallucination — `.Background()` is a Box-only extension.

**Found In:**
ab38eba1-af47-4003-905b-4fe9cea8ba4f

## Button.ColSpan() — non-existent grid span method

**Hallucinated API:**
```csharp
new Button("=").ColSpan(2)
```

**Error:** `CS1061: 'Button' does not contain a definition for 'ColSpan'`

**Correct API:**
```csharp
// Grid column spanning is not set on child widgets.
// Use GridLayout column definitions to control spans,
// or use multiple grid cells for the same widget.
```

**Found In:**
ab38eba1-af47-4003-905b-4fe9cea8ba4f

## IState\<T\>.ToTextArea() — incorrect textarea method name

**Hallucinated API:**
```csharp
var text = UseState("");
text.ToTextArea()
```

**Error:** `CS1061: 'IState<string>' does not contain a definition for 'ToTextArea'`

**Correct API:**
```csharp
var text = UseState("");
text.ToTextareaInput()
// or equivalently:
text.ToTextInput().Multiline()
```

The method is `ToTextareaInput()`, not `ToTextArea()`. Alternatively use `ToTextInput().Multiline()`. See `Docs/02_Widgets/04_Inputs/02_TextInput.md` for full textarea documentation.

**Found In:**
19ec33cf-3e86-409e-806c-babf0d20730f

## new TextInput() — parameterless constructor doesn't exist

**Hallucinated API:**
```csharp
var output = new TextInput();
output.Disabled()
```

**Error:** `CS1729: 'TextInput' does not contain a constructor that takes 0 arguments`

**Correct API:**
```csharp
var output = UseState("");
output.ToTextInput()
// or for textarea:
output.ToTextareaInput()
```

TextInput has no public parameterless constructor. All input widgets are created via `IState<T>.ToXxxInput()` extension methods, not via `new`.

**Found In:**
edd92ecc-6378-440a-b9cf-bb8e1cb29de9

## IState\<T\>.ToSelect() — incorrect select method name

**Hallucinated API:**
```csharp
var format = UseState("Option1");
format.ToSelect(options)
```

**Error:** `CS1061: 'IState<string>' does not contain a definition for 'ToSelect'`

**Correct API:**
```csharp
var format = UseState("Option1");
format.ToSelectInput(new[] { "Option1", "Option2" }.ToOptions())
```

The method is `ToSelectInput()`, not `ToSelect()`. Options are passed as `IEnumerable<IAnyOption>` — use `.ToOptions()` on a string array to convert.

**Found In:**
19ec33cf-3e86-409e-806c-babf0d20730f

## Card.When() — non-existent conditional rendering method

**Hallucinated API:**
```csharp
new Card(outputText).When(hasOutput)
```

**Error:** `CS1061: 'Card' does not contain a definition for 'When'`

**Correct API:**
```csharp
// Use standard C# control flow for conditional rendering:
if (hasOutput)
{
    new Card(outputText);
}
```

There is no `.When()` method on any widget. Ivy uses standard C# `if` statements for conditional rendering, similar to React's conditional rendering pattern. See also the existing `.Visible()` hallucination entry.

**Found In:**
19ec33cf-3e86-409e-806c-babf0d20730f

## Card.Style() / Card.ClassName() / Card.WithStyle() — non-existent CSS methods

**Hallucinated API:**
```csharp
new Card(...).Style("background: green")
new Card(...).ClassName("my-class")
new Card(...).WithStyle(new { Background = "green" })
```

**Error:** `CS1061: 'Card' does not contain a definition for 'Style'/'ClassName'/'WithStyle'`

**Correct API:**
```csharp
// Cards don't support direct CSS styling. To add a colored background, wrap in a Box:
new Box(new Card(content)).Background(Colors.Green)
// Or use a Box directly instead of Card when you need full styling control:
new Box(content).Background(Colors.Green).Padding(20).Rounded()
```

**Found In:**
5c9cfb70-c9f5-4642-8de6-480be8f5ee85

## Card.Border() — Box extension used on Card

**Hallucinated API:**
```csharp
new Card(...).Border(1)
```

**Error:** `CS1929: 'Card' does not contain a definition for 'Border'`

**Correct API:**
```csharp
// Cards have a built-in border. For custom borders, wrap in a Box:
new Box(new Card(content)).Border(1)
```

**Found In:**
5c9cfb70-c9f5-4642-8de6-480be8f5ee85

## Card.Color() — non-existent method on Card

**Hallucinated API:**
```csharp
new Card(...).Color(Colors.Green)
```

**Error:** `CS1061: 'Card' does not contain a definition for 'Color'`

**Correct API:**
```csharp
// Cards don't have a Color method. Use Box for colored containers:
new Box(content).Background(Colors.Green)
```

**Found In:**
5c9cfb70-c9f5-4642-8de6-480be8f5ee85

## Card.Align() — non-existent method on Card

**Hallucinated API:**
```csharp
new Card(...).Align(Align.Center)
```

**Error:** `CS1929: 'Card' does not contain a definition for 'Align'`

**Correct API:**
```csharp
// Use a Layout to control alignment of card content:
Layout.Vertical(Align.Center) | new Card(content)
```

**Found In:**
5c9cfb70-c9f5-4642-8de6-480be8f5ee85

## Nested Layout | operator without parentheses

**Hallucinated pattern:**
```csharp
Layout.Vertical()
    | Layout.Horizontal().Gap(4)
        | child1
        | child2
    | otherContent;
```

**Problem:** C# evaluates `|` left-to-right. Without parentheses, `child1` and `child2` are added to the outer `Vertical` layout, not the inner `Horizontal`. The indentation is misleading — C# ignores indentation.

**Correct pattern:**
```csharp
Layout.Vertical()
    | (Layout.Horizontal().Gap(4)
        | child1
        | child2)
    | otherContent;
```

Always wrap nested layouts in parentheses `(Layout.Horizontal() | child1 | child2)` to ensure children are added to the correct parent layout.

**Found In:**
19ec33cf-3e86-409e-806c-babf0d20730f

## Margin(new Thickness(...)) — Margin takes int, not Thickness

**Hallucinated API:**
```csharp
layout.Margin(new Thickness(0, 4, 0, 0))
```

**Error:** `CS1503: Argument 1: cannot convert from 'Ivy.Thickness' to 'int'`

**Correct API:**
```csharp
// Margin() takes int parameters directly:
layout.Margin(4)              // uniform
layout.Margin(4, 2)           // horizontal, vertical
layout.Margin(0, 4, 0, 0)    // left, top, right, bottom
```

**Found In:**
2e18b175-94ec-459c-94a5-8f28b81ecfdc

## Form() — internal constructor

**Hallucinated API:**
```csharp
new Form()
new Form(children)
```

**Error:** `CS1729: 'Form' does not contain a constructor that takes 0 arguments`

**Correct API:**
```csharp
// Forms are created from state objects:
state.ToForm()
    .Field(f => f.Name)
    .Field(f => f.Email)
```

`Form` constructors are `internal`. Forms must be created using the `.ToForm()` extension method on `IState<T>`. The agent should never use `new Form()` directly.

**Found In:**
5d2202d2-9d6b-4198-9922-c3763534aca5

## DataTable.RowActions() — Tree extension called on DataTable widget

**Hallucinated API:**
```csharp
var table = new DataTable(connection, null, null, columns, config);
table.RowActions(MenuItem.Default("Adjust Stock", tag: "adjust"), MenuItem.Default("Edit", tag: "edit"));
```

**Error:** `CS1929: 'DataTable' does not contain a definition for 'RowActions' and the best extension method overload 'TreeWidgetExtensions.RowActions(Tree, params MenuItem[])' requires a receiver of type 'Ivy.Tree'`

**Correct API:**
```csharp
// Via DataTableBuilder (preferred):
items.ToDataTable()
    .RowActions(MenuItem.Default("Adjust Stock", tag: "adjust"), MenuItem.Default("Edit", tag: "edit"))
    .OnRowAction(e => { var id = e.Value.Id; var tag = e.Value.Tag; })

// Or via property on DataTable record:
new DataTable(connection, null, null, columns, config) { RowActions = new[] { MenuItem.Default("Adjust Stock", tag: "adjust") } }
```

`.RowActions()` as a fluent extension method only exists on `Tree` (via `TreeWidgetExtensions`). For `DataTable`, either use the `DataTableBuilder<T>` pattern (`.ToDataTable().RowActions(...)`) or set the `RowActions` property directly on the record. The agent confused the Tree and DataTable APIs because both support row actions with similar names.

**Found In:**
30c1b273-c528-4496-b194-c98e0ffeaa23

## DataTableColumn without ColType — missing required member

**Hallucinated API:**
```csharp
new DataTableColumn { Header = "Name", Field = "Name" }
```

**Error:** `CS9035: Required member 'DataTableColumn.ColType' must be set in the object initializer or attribute constructor.`

**Correct API:**
```csharp
new DataTableColumn { Header = "Name", Field = "Name", ColType = ColType.String }
```

`DataTableColumn.ColType` is a `required` member. It must always be set when constructing a `DataTableColumn`. Valid values: `ColType.String`, `ColType.Number`, `ColType.Boolean`, `ColType.DateTime`, etc. The IvyQuestion MCP answer omitted this required field.

**Found In:**
30c1b273-c528-4496-b194-c98e0ffeaa23

## DataTable constructor — missing required width parameter

**Hallucinated API:**
```csharp
new DataTable(connection, columns: columns, options: config)
```

**Error:** `CS7036: There is no argument given that corresponds to the required parameter 'width' of 'DataTable.DataTable(DataTableConnection, Size?, Size?, DataTableColumn[], DataTableConfig)'`

**Correct API:**
```csharp
// Preferred: use the builder pattern
query.ToDataTable()
    .Column(m => m.Name)
    .Column(m => m.Category)

// Or construct directly with all required params:
new DataTable(connection, width: null, height: null, columns, config)
```

The `DataTable` public constructor requires all 5 positional parameters: `(DataTableConnection, Size?, Size?, DataTableColumn[], DataTableConfig)`. Prefer the `DataTableBuilder<T>` pattern via `.ToDataTable()` which handles construction details. The IvyQuestion answer provided an incorrect constructor signature.

**Found In:**
30c1b273-c528-4496-b194-c98e0ffeaa23

## using Ivy.Apps / using Ivy.Shared — non-existent namespaces

**Hallucinated API:**
```csharp
using Ivy.Apps;
using Ivy.Shared;
```

**Error:** `The type or namespace name 'Apps' does not exist in the namespace 'Ivy'` / `The type or namespace name 'Shared' does not exist in the namespace 'Ivy'`

**Correct API:**
```csharp
using Ivy;
```

There are no `Ivy.Apps` or `Ivy.Shared` namespaces. All Ivy widgets, hooks, and types are in the root `Ivy` namespace. The agent likely hallucinates these from ASP.NET conventions or other framework patterns where subnamespaces separate concerns.

**Found In:**
a55e08b9-f212-49ef-97b9-d352b7b4beb8

## await void OnSubmit callback — incorrect async pattern

**Hallucinated API:**
```csharp
state.ToForm().OnSubmit(async form => {
    await db.SaveChangesAsync(); // CS4008: Cannot await 'void'
})
```

**Error:** `CS4008: Cannot await 'void'`

**Correct API:**
```csharp
state.ToForm().OnSubmit(async form => {
    await db.SaveChangesAsync();
    // Ensure the callback signature returns Task, not void
})
```

The agent sometimes uses `await` on a method that returns `void` inside a form `OnSubmit` callback. This happens when the callback is inferred as `Action<T>` (returning void) rather than `Func<T, Task>`. Ensure the lambda is recognized as async Task-returning by the compiler. If the `OnSubmit` overload expects `Func<T, ValueTask>`, ensure the return type matches.

**Found In:**
9d8f5446-43c4-44a2-b6ce-3caeff413407 (TestsApp.cs)

## TabsLayout(params Tab[]) — simplified constructor doesn't exist

**Hallucinated API:**
```csharp
new TabsLayout(
    new Tab("Markets", new MarketsView()),
    new Tab("Chart", new ChartView()),
    new Tab("Portfolio", new PortfolioView())
)
```

**Error:** `CS1729: 'TabsLayout' does not contain a constructor that takes 1 arguments`

**Correct API:**
```csharp
new TabsLayout(
    onSelect: null, onClose: null, onRefresh: null, onReorder: null, selectedIndex: null,
    new Tab("Markets", new MarketsView()),
    new Tab("Chart", new ChartView()),
    new Tab("Portfolio", new PortfolioView())
)
```

`TabsLayout` has no simplified `(params Tab[])` constructor. The public constructor requires 5 positional parameters before the `params Tab[]`: `onSelect`, `onClose`, `onRefresh`, `onReorder`, `selectedIndex`. Pass `null` for all event handlers and selectedIndex when only tabs are needed. The agent tried to skip these parameters, causing repeated build failures (5 times in a single session).

**Found In:**
3d2cdc9c-aad3-410e-a1e4-7c007529077a

## Field.Invalid() — extension method called on wrong type

**Hallucinated API:**
```csharp
stat.ToNumberInput().WithField().Label("Strength").Invalid("Over budget")
```

**Error:** `CS1929: 'Field' does not contain a definition for 'Invalid' and the best extension method overload 'BoolInputExtensions.Invalid(BoolInputBase, string?)' requires a receiver of type 'Ivy.BoolInputBase'`

**Correct API:**
```csharp
// Call .Invalid() on the input BEFORE wrapping in a Field:
stat.ToNumberInput().Invalid("Over budget").WithField().Label("Strength")

// Or conditionally:
stat.ToNumberInput().Invalid(overBudget ? "Over budget" : null).WithField().Label("Strength")
```

`.Invalid()` is an extension method on input base types (`NumberInputBase`, `TextInputBase`, etc.), not on `Field`. When using the `.WithField()` fluent chain, `.Invalid()` must be called on the input before `.WithField()` converts it to a `Field`. The agent confused the fluent chain ordering.

**Found In:**
8111879a-ebe9-48d0-bd8e-936becb133ee

## NumberInput without generic type argument

**Hallucinated API:**
```csharp
(NumberInput)input
// or referencing NumberInput as a non-generic type
```

**Error:** `CS0305: Using the generic type 'NumberInput<TNumber>' requires 1 type arguments`

**Correct API:**
```csharp
// Use the fluent builder pattern — no need to reference the type directly:
intState.ToNumberInput()

// If you must reference the type, include the type parameter:
NumberInput<int>
```

`NumberInput<TNumber>` is a generic type and cannot be referenced without its type argument. In practice, use `.ToNumberInput()` on an `IState<int>` (or other numeric type) and chain fluent methods. There is rarely a need to reference the `NumberInput<T>` type directly.

**Found In:**
8111879a-ebe9-48d0-bd8e-936becb133ee

## TextBuilder.Icon() — extension method receiver mismatch

**Hallucinated API:**
```csharp
Text.H3("Task Hub").Icon(Icons.KanbanSquare)
```

**Error:** `CS1929: 'TextBuilder' does not contain a definition for 'Icon' and the best extension method overload 'MenuItemExtensions.Icon(MenuItem, Icons)' requires a receiver of type 'Ivy.MenuItem'`

**Correct API:**
```csharp
// Icon() is only available on MenuItem, not on TextBuilder.
// To show an icon next to text, use a layout:
new Horizontal(new Icon(Icons.KanbanSquare), Text.H3("Task Hub"))
```

The agent assumed `.Icon()` was a chainable method on `TextBuilder`, but `Icon()` is an extension method that only applies to `MenuItem`. The `ReplaceInvalidIcons` refactoring rule caught and fixed the invalid icon name, but not the wrong receiver type.

**Found In:**
c1f8feae-b342-4bf1-a18c-9b88ee8d6d17

## Button.Left() — extension method receiver mismatch

**Hallucinated API:**
```csharp
new Button("Label").Left()
```

**Error:** `CS1929: 'Button' does not contain a definition for 'Left' and the best extension method overload 'LegendExtensions.Left(Legend)' requires a receiver of type 'Ivy.Legend'`

**Correct API:**
```csharp
// .Left() is only available on Legend, DropDownMenu, Toolbox, and Markdown — not on Button.
// Buttons don't need alignment — they render their label as-is.
// To align a button within a layout, use Layout alignment:
Layout.Horizontal(Align.Left) | new Button("Label")
```

`.Left()` is a positioning/alignment extension that exists on specific widgets (`Legend`, `DropDownMenu`, `Toolbox`, `Markdown`) but not on `Button`. The agent assumed it was a universal alignment method. The error pattern is the same as `Button.Color()` — the agent applies an extension method from a different widget type.

**Found In:**
885c440d-f69b-4640-953d-7946883b3eda

## Sheet(content) — single-argument constructor doesn't exist

**Hallucinated API:**
```csharp
new Sheet(detailContent)
```

**Error:** `CS1729: 'Sheet' does not contain a constructor that takes 1 arguments`

**Correct API:**
```csharp
// Sheet requires an onClose callback as the first parameter:
new Sheet(onClose: null, content: detailContent, title: "Details")

// Or with a close handler:
new Sheet(() => isOpen.Value = false, detailContent, title: "Details")
```

`Sheet` constructors all require at least two parameters: `(onClose, content)`. The first parameter is a close callback (`Action?`, `Action<Event<Sheet>>?`, or `Func<Event<Sheet>, ValueTask>?`). The agent assumed a simple `Sheet(content)` constructor similar to other wrapper widgets. Use `Button.WithSheet()` for the common pattern of opening a sheet from a button click.

**Found In:**
a62654f6-f041-4b48-89b8-7ca19d3e6819

## Dictionary as SelectInput options — wrong options type

**Hallucinated API:**
```csharp
var options = new Dictionary<string, string> { { ",", "Comma" }, { ";", "Semicolon" } };
delimiterState.ToSelectInput(options)
```

**Error:** `CS1503: Argument 2: cannot convert from 'System.Collections.Generic.Dictionary<string, string>' to 'System.Collections.Generic.IEnumerable<Ivy.IAnyOption>?'`

**Correct API:**
```csharp
var options = new Option<string>[]
{
    new(",") { Label = "Comma (,)" },
    new(";") { Label = "Semicolon (;)" },
};
delimiterState.ToSelectInput(options)
```

`SelectInput` options must be `IEnumerable<Option<T>>`, not `Dictionary<string, string>`. Each option is an `Option<T>` with a value and optional `Label` property. Related: see also the `SelectOption` hallucination (non-existent type).

**Found In:**
27c08f6f-438d-421d-9625-4646918e1c33

## QueryResult\<T\>.Key — non-existent property

**Hallucinated API:**
```csharp
var result = UseQuery(...);
var key = result.Key;
```

**Error:** `CS1061: 'QueryResult<BankDetails?>' does not contain a definition for 'Key'`

**Correct API:**
```csharp
var result = UseQuery(...);
// QueryResult<T> has these properties:
// result.Value    — the query result data
// result.Loading  — whether the query is in progress
// result.Error    — exception if the query failed
// result.Validating — whether revalidation is in progress
// result.Previous — whether showing previous data
```

`TextBuilder` does not have `.Padding()`. Padding is available on container widgets (`Box`, `LayoutView`, `TabView`, `GridView`). To add padding around text, wrap it in a `Box` or layout. This is a variant of the `TextBuilder.AlignCenter()` and `TextBuilder.Style()` hallucinations — the agent applies container-level styling to text elements.

## HandleSubmit / Handle* — renamed event handler methods

**Hallucinated API:**
```csharp
input.ToTextInput().HandleSubmit(() => Save())
button.HandleClick(() => DoSomething())
input.HandleBlur(() => Validate())
```

**Error:** `does not contain a definition for 'HandleSubmit'` (or `HandleClick`, `HandleBlur`, etc.)

**Correct API:**
```csharp
input.ToTextInput().OnSubmit(() => Save())
button.OnClick(() => DoSomething())
input.OnBlur(() => Validate())
```

All `Handle*` event handler extension methods were renamed to `On*` in v1.2.17 (Ivy-Framework#2459, #2510): `HandleClick` → `OnClick`, `HandleSubmit` → `OnSubmit`, `HandleChange` → `OnChange`, `HandleSelect` → `OnSelect`, `HandleClose` → `OnClose`, `HandleBlur` → `OnBlur`, `HandleRowAction` → `OnRowAction`, `HandleCardMove` → `OnCardMove`, `HandleExpand` → `OnExpand`, `HandleCollapse` → `OnCollapse`, `HandlePageChange` → `OnPageChange`, `HandleUpload` → `OnUpload`, `HandleDownload` → `OnDownload`. **Auto-fixed:** The refactoring service automatically rewrites all `Handle*` calls to `On*`.

## Server Configuration

| Hallucinated API | Correct API |
|-----------------|-------------|
| `server.UseSingleApp()` | `server.UseDefaultApp(typeof(AppType))` |
| `server.UseNoAppShell()` | `server.UseDefaultApp(typeof(AppType))` — omit `UseAppShell()` instead |
| `server.UseDefaultApp<T>()` | `server.UseDefaultApp(typeof(T))` — takes Type, not generic |

## UseService vs UseContext — blade/context services

LLMs sometimes use `UseService<IBladeService>()` to obtain the blade service. This is incorrect — `IBladeService` is a **context** service provided by `UseBlades()`, not a DI-registered service. Using `UseService` returns `null`, causing `NullReferenceException` at runtime.

**Wrong:**
```csharp
var bladeService = UseService<IBladeService>(); // Returns null!
```

**Correct:**
```csharp
var bladeService = UseContext<IBladeService>();
```

**Rule:** Use `UseContext<T>()` for framework-provided context services (`IBladeService`, etc.). Use `UseService<T>()` only for application-registered DI services (e.g., `DbContextFactory`, `HttpClient`).

## ToForm(OnSubmit: ...) — OnSubmit is an extension method, not a parameter

**Hallucinated API:**
```csharp
state.ToForm(OnSubmit: async form => { ... })
```

**Error:** `CS1739: The best overload for 'ToForm' does not have a parameter named 'OnSubmit'`

**Correct API:**
```csharp
state.ToForm().OnSubmit(async form => { ... })
```

`OnSubmit` is a fluent extension method that chains after `ToForm()`, not a constructor parameter. The same pattern applies to other form event handlers like `OnChange`.

## UseEffect fires on initial render — it does not

**Hallucinated behavior:**
The agent assumes `UseEffect` with a state dependency fires on the initial render (like React's `useEffect`), so it initializes state as empty and relies on the effect to populate it:
```csharp
// Agent writes this expecting the effect to run immediately:
var count = UseState(10);
var items = UseState(() => new List<User>());
UseEffect(() => { items.Set(GenerateUsers(count.Value)); }, count);
// Result: table is empty on first load
```

**Correct pattern:**
`UseEffect` with state dependencies only fires when the dependency **changes**, not on the first render. Initialize state with data directly:

```csharp
var count = UseState(10);
var items = UseState(() => GenerateUsers(count.Value));
UseEffect(() => { items.Set(GenerateUsers(count.Value)); }, count);
```

This is a behavioral difference from React's `useEffect`, which fires on mount and on dependency changes. Ivy's `UseEffect` with state triggers uses `AfterChange` semantics only.

## ToastVariant — non-existent enum — now supported

**Hallucinated API:**
```csharp
client.Toast("Error!", ToastVariant.Destructive)
```

**Error:** `The name 'ToastVariant' does not exist in the current context`

**Correct API:**
```csharp
client.Toast("Success message");       // neutral toast
client.Toast("Done!", "Title");        // with title
client.Error("Something went wrong."); // error toast
```

`ToastVariant` does not exist. The `IClientProvider.Toast()` method takes `(string message)` or `(string message, string title)`. For error toasts, use `client.Error(message)` instead.

`QueryResult<T>` does not have a `.Key` property. The agent likely confused it with dictionary/cache APIs. Access the result data via `.Value`, check loading state via `.Loading`, and errors via `.Error`.

**Found In:**
e084455d-82f6-4fb9-bfec-699755bd37ea
