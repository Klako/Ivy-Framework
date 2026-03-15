using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Hallucinations.md")]
public class HallucinationsApp(bool onlyBody = false) : ViewBase
{
    public HallucinationsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivy-framework-hallucinations", "Ivy Framework Hallucinations", 1), new ArticleHeading("ireft--now-supported", "IRef<T> — now supported", 2), new ArticleHeading("selectinputbaseoptions--chained-options-method", "SelectInputBase.Options() — chained options method", 2), new ArticleHeading("tablet--non-generic-type-used-with-type-arguments", "Table<T> — non-generic type used with type arguments", 2), new ArticleHeading("layoutviewmaxwidth--non-existent-method", "LayoutView.MaxWidth() — non-existent method", 2), new ArticleHeading("layoutviewspacebetween--non-existent-method", "LayoutView.SpaceBetween() — non-existent method", 2), new ArticleHeading("callout-constructor--wrong-constructor--invented-enum", "Callout constructor — wrong constructor + invented enum", 2), new ArticleHeading("calloutdestructive--fluent-method-on-constructor-instance", "Callout.Destructive() — fluent method on constructor instance", 2), new ArticleHeading("handlesubmit--handle--renamed-event-handler-methods", "HandleSubmit / Handle* — renamed event handler methods", 2), new ArticleHeading("textinputbaseonenter--invented-fluent-method", "TextInputBase.OnEnter() — invented fluent method", 2), new ArticleHeading("textinputvariants--old-plural-enum-name", "TextInputVariants — old plural enum name", 2), new ArticleHeading("eventtedata--non-existent-property", "Event<T,E>.Data — non-existent property", 2), new ArticleHeading("usestatetnull--ambiguous-overload-call", "UseState<T?>(null) — ambiguous overload call", 2), new ArticleHeading("buttontext-iconsx--icon-as-constructor-argument", "Button(\"text\", Icons.X) — icon as constructor argument", 2), new ArticleHeading("inputbaselabel--axisextensions-method-used-on-input", "InputBase.Label() — AxisExtensions method used on input", 2), new ArticleHeading("tabcontent--non-existent-fluent-method", "Tab.Content() — non-existent fluent method", 2), new ArticleHeading("layouttabs--tab--pipe-operator-on-tabview", "Layout.Tabs() | Tab — pipe operator on TabView", 2), new ArticleHeading("toastvariant--non-existent-enum", "ToastVariant — non-existent enum", 2), new ArticleHeading("datetimevariant--wrong-enum-name", "DateTimeVariant — wrong enum name", 2), new ArticleHeading("formbuilderheader--non-existent-method", "FormBuilder.Header() — non-existent method", 2), new ArticleHeading("badgecolorcolorsx--non-existent-fluent-method", "Badge.Color(Colors.X) — non-existent fluent method", 2), new ArticleHeading("calloutcolorcolorsx--non-existent-fluent-method", "Callout.Color(Colors.X) — non-existent fluent method", 2), new ArticleHeading("spacerint-constructor--non-existent-constructor-overload", "Spacer(int) constructor — non-existent constructor overload", 2), new ArticleHeading("buttoncolorcolorsx--non-existent-fluent-method", "Button.Color(Colors.X) — non-existent fluent method", 2), new ArticleHeading("usealertshowinfo--wrong-api-usage", "UseAlert().ShowInfo() — wrong API usage", 2), new ArticleHeading("sizeflex--non-existent-static-method", "Size.Flex() — non-existent static method", 2), new ArticleHeading("refreshtokenversion--non-existent-property", "RefreshToken.Version — non-existent property", 2), new ArticleHeading("queryresulttdata--wrong-property-name", "QueryResult<T>.Data — wrong property name", 2), new ArticleHeading("queryresulttisloading--wrong-property-name", "QueryResult<T>.IsLoading — wrong property name", 2), new ArticleHeading("listitemdescription--listitemmeta--listitemactions--non-existent-members", "ListItem.Description / ListItem.Meta / ListItem.Actions — non-existent members", 2), new ArticleHeading("sizesm--non-existent-member", "Size.Sm — non-existent member", 2), new ArticleHeading("string-literal-as-icons--wrong-type", "String literal as Icons? — wrong type", 2), new ArticleHeading("textsmalltext--static-factory-confusion", "Text.Small(\"text\") — static factory confusion", 2), new ArticleHeading("textsecondarytext--non-existent-static-factory", "Text.Secondary(\"text\") — non-existent static factory", 2), new ArticleHeading("boxborderradiusint--wrong-argument-type", "Box.BorderRadius(int) — wrong argument type", 2), new ArticleHeading("borderradiusmedium--non-existent-enum-value", "BorderRadius.Medium — non-existent enum value", 2), new ArticleHeading("gridviewbackground--non-existent-method", "GridView.Background() — non-existent method", 2), new ArticleHeading("sizepixels--wrong-method-name", "Size.Pixels() — wrong method name", 2), new ArticleHeading("stringtocodeinput--wrong-receiver-type", "string.ToCodeInput() — wrong receiver type", 2), new ArticleHeading("statet--non-existent-type", "State<T> — non-existent type", 2), new ArticleHeading("irefreshtoken--non-existent-interface", "IRefreshToken — non-existent interface", 2), new ArticleHeading("datatablet--non-generic-type-used-with-type-arguments", "DataTable<T> — non-generic type used with type arguments", 2), new ArticleHeading("shrinkint--method-takes-no-arguments", "Shrink(int) — method takes no arguments", 2), new ArticleHeading("cardpadding--non-existent-method", "Card.Padding() — non-existent method", 2), new ArticleHeading("selectinputwidth--generic-constraint-mismatch", "SelectInput.Width() — generic constraint mismatch", 2), new ArticleHeading("found-in", "Found In", 3), new ArticleHeading("alignend--alignstart--css-inspired-enum-values", "Align.End / Align.Start — CSS-inspired enum values", 2), new ArticleHeading("layoutviewborder--now-supported", "LayoutView.Border() — now supported", 2), new ArticleHeading("server-configuration", "Server Configuration", 2), new ArticleHeading("textbuilderstyle--non-existent-styling-method", "TextBuilder.Style() — non-existent styling method", 2), new ArticleHeading("textbuilderaligncenter--non-existent-method", "TextBuilder.AlignCenter() — non-existent method", 2), new ArticleHeading("fileuploadstatuscompleted--non-existent-enum-value", "FileUploadStatus.Completed — non-existent enum value", 2), new ArticleHeading("usedownload--ambiguous-overload-between-sync-and-async", "UseDownload — ambiguous overload between sync and async", 2), new ArticleHeading("serveronready--serveronstartup--non-existent-lifecycle-callbacks", "Server.OnReady / Server.OnStartup — non-existent lifecycle callbacks", 2), new ArticleHeading("metriccard--non-existent-class-name", "MetricCard — non-existent class name", 2), new ArticleHeading("disposablecreate--missing-using-statement", "Disposable.Create() — missing using statement", 2), new ArticleHeading("fragmentempty--non-existent-static-member", "Fragment.Empty — non-existent static member", 2), new ArticleHeading("textinputgrow--box-only-extension-called-on-textinput", "TextInput.Grow() — Box-only extension called on TextInput", 2), new ArticleHeading("appattributepath-old-parameter-name", "AppAttribute.path old parameter name", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # Ivy Framework Hallucinations
                
                Known cases where the agent hallucinated Ivy Framework APIs. Use this as a reference when debugging build errors in agent sessions.
                
                ## IRef<T> — now supported
                
                `IRef<T>` was previously a hallucinated interface. It has since been added to the framework as `IRef<T> : IState<T>`. Both `UseRef<T>()` return types are now `IRef<T>`, while `UseState<T>()` continues to return `IState<T>`. The two interfaces are interchangeable — `IRef<T>` is a marker subtype used for clarity.
                
                ## SelectInputBase.Options() — chained options method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                defaultBehavior.ToSelectInput().Options(["Refused", "Allowed", "Ignored"])
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'SelectInputBase' does not contain a definition for 'Options'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                defaultBehavior.ToSelectInput(new[] { "Refused", "Allowed", "Ignored" }.ToOptions())
                """",Languages.Csharp)
            | new Markdown(
                """"
                Options are passed as `IEnumerable<IAnyOption>` to `ToSelectInput(options)`, not chained via a `.Options()` method. Use the `.ToOptions()` extension method on a string array to convert to the correct type.
                
                **Found In:**
                4eb1799f-39b2-4325-a0bd-37b769a33432``
                
                https://github.com/Ivy-Interactive/Ivy-Framework/issues/2271
                
                ## Table<T> — non-generic type used with type arguments
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new Table<MyRecord>(items)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The non-generic type 'Table' cannot be used with type arguments`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("items.ToTable()",Languages.Csharp)
            | new Markdown(
                """"
                `Table` is non-generic. Use the `IEnumerable<T>.ToTable()` builder pattern to create a table from a collection. The type is inferred from the collection.
                
                **Found In:**
                a9ee3993-1cfb-4cba-9322-80a60b56c8d2
                
                ## LayoutView.MaxWidth() — non-existent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Layout.Vertical().MaxWidth(Size.Lg)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'LayoutView' does not contain a definition for 'MaxWidth'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Layout.Vertical().Width(Size.Lg)",Languages.Csharp)
            | new Markdown(
                """"
                `LayoutView` does not have a `.MaxWidth()` method. Use `.Width(Size)` instead.
                
                **Found In:**
                a9ee3993-1cfb-4cba-9322-80a60b56c8d2
                
                ## LayoutView.SpaceBetween() — non-existent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Layout.Horizontal().SpaceBetween()",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'LayoutView' does not contain a definition for 'SpaceBetween'` (CS1061)
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Layout.Horizontal(Align.SpaceBetween)",Languages.Csharp)
            | new Markdown(
                """"
                `SpaceBetween` is an `Align` enum value passed to the layout constructor, not a fluent method. The same applies to `SpaceAround` and `SpaceEvenly`.
                
                **Found In:**
                f6d6e841-9a14-4475-9fa5-0791be30e578
                
                ## Callout constructor — wrong constructor + invented enum
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Callout("No to-do items.", CalloutType.Info)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The typeound`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Callout.Info("No to-do items.")
                """",Languages.Csharp)
            | new Markdown(
                """"
                `Callout` uses static factory methods: `Callout.Info()`, `Callout.Warning()`, `Callout.Error()`, `Callout.Success()`. The `CalloutType` enum does not exist.
                
                **Found In:**
                bd5f45ac-569d-4be8-8ef8-882451e608a1
                
                ## Callout.Destructive() — fluent method on constructor instance
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Callout("Error message").Destructive()
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Callout' does not contain a definition for 'Destructive'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Callout.Error("Error message")
                """",Languages.Csharp)
            | new Markdown(
                """"
                `Callout` uses static factory methods (`Callout.Info()`, `Callout.Warning()`, `Callout.Error()`, `Callout.Success()`), not a constructor + fluent style chain. `.Destructive()` is a `Button` style method — the agent confused the two APIs. No auto-fix is possible because the intent (error vs warning vs info) is ambiguous.
                
                **Found In:**
                d9116efb-830e-484a-a258-fc3193769158
                
                ## HandleSubmit / Handle* — renamed event handler methods
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                input.ToTextInput().HandleSubmit(() => Save())
                button.HandleClick(() => DoSomething())
                input.HandleBlur(() => Validate())
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `does not contain a definition for 'HandleSubmit'` (or `HandleClick`, `HandleBlur`, etc.)
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                input.ToTextInput().OnSubmit(() => Save())
                button.OnClick(() => DoSomething())
                input.OnBlur(() => Validate())
                """",Languages.Csharp)
            | new Markdown(
                """"
                All `Handle*` event handler extension methods were renamed to `On*` in v1.2.17 (Ivy-Framework#2459, #2510): `HandleClick` → `OnClick`, `HandleSubmit` → `OnSubmit`, `HandleChange` → `OnChange`, `HandleSelect` → `OnSelect`, `HandleClose` → `OnClose`, `HandleBlur` → `OnBlur`, `HandleRowAction` → `OnRowAction`, `HandleCardMove` → `OnCardMove`, `HandleExpand` → `OnExpand`, `HandleCollapse` → `OnCollapse`, `HandlePageChange` → `OnPageChange`, `HandleUpload` → `OnUpload`, `HandleDownload` → `OnDownload`. **Auto-fixed:** The refactoring service automatically rewrites all `Handle*` calls to `On*`.
                
                **Found In:**
                (multiple sessions — agent uses old API names from training data)
                
                ## TextInputBase.OnEnter() — invented fluent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                newItemText.ToTextInput().Placeholder("Add a new to-do...").OnEnter(AddTodo)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'TextInputBase' does not contain a definition for 'OnEnter'`
                
                **Correct API:**
                `.OnEnter()` does not exist on `TextInput`. Use `OnSubmit()` to handle enter-key submission:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("text.ToTextInput().OnSubmit(() => DoSomething())",Languages.Csharp)
            | new Markdown(
                """"
                **Found In:**
                bd5f45ac-569d-4be8-8ef8-882451e608a1
                
                ## TextInputVariants — old plural enum name
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new TextInput(text.Value, e => text.Set(e.Value)).Variant(TextInputVariants.Textarea)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The name 'TextInputVariants' does not exist in the current context`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new TextInput(text.Value, e => text.Set(e.Value)).Variant(TextInputVariant.Textarea)",Languages.Csharp)
            | new Markdown(
                """"
                The enum is `TextInputVariant` (singular), not `TextInputVariants` (plural). All input variant enums were renamed from plural to singular in Ivy-Framework#2546 (e.g., `TextInputVariants` → `TextInputVariant`, `ColorInputVariants` → `ColorInputVariant`, etc.). **Auto-fixed:** The refactoring service automatically rewrites `TextInputVariants` → `TextInputVariant`. Values: `Text`, `Textarea`, `Email`, `Tel`, `Url`, `Password`, `Search`.
                
                **Found In:**
                4a94f8f6-865d-4663-8f4c-d4c09913398f
                
                ## Event<T,E>.Data — non-existent property
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                args.Data.Id
                args.Data.Tag
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Event<DataTable, RowActionClickEventArgs>' does not contain a definition for 'Data'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                args.Value.Id
                args.Value.Tag
                """",Languages.Csharp)
            | new Markdown(
                """"
                `Event<TSender, TValue>` uses `.Value` to access the event args, not `.Data`. The agent likely confused this with other event patterns from different frameworks (e.g., WPF `DataContext`, JavaScript `event.data`).
                
                **Found In:**
                f20dced8-1689-4289-a2d8-ee67136eb6ce
                
                ## UseState<T?>(null) — ambiguous overload call
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("var selectedItem = UseState<InventoryItem?>(null);",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The call is ambiguous between 'ViewBase.UseState<T>(T?, bool)' and 'ViewBase.UseState<T>(Func<T>, bool)'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Best: omit the null argument — the default is already null:
                var selectedItem = UseState<InventoryItem?>();
                // Or cast null to the explicit type:
                var selectedItem = UseState<InventoryItem?>((InventoryItem?)null);
                // Or use a lambda:
                var selectedItem = UseState(() => (InventoryItem?)null);
                """",Languages.Csharp)
            | new Markdown(
                """"
                When `T` is a reference type, `null` matches both `T?` and `Func<T>`, causing overload ambiguity. The simplest fix is to omit the `null` argument entirely — the default parameter is already `null`/`default`. Alternatively, cast null to the explicit type or wrap it in a lambda.
                
                **Note:** Unlike `IState<T>.Set(null)` (which was fixed via `[OverloadResolutionPriority(1)]`), `UseState` cannot use the same approach because T is inferred from the argument — C# 10+ lambda natural types cause the `T?` overload to steal ALL lambda calls when given higher priority, breaking `UseState(() => expr)` throughout the codebase.
                
                **Found In:**
                f20dced8-1689-4289-a2d8-ee67136eb6ce
                
                ## Button("text", Icons.X) — icon as constructor argument
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Button("Add Item", Icons.Plus)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `Argument 2: cannot convert from 'Ivy.Icons' to 'System.Func<Ivy.Event<Ivy.Button>, System.Threading.Tasks.ValueTask>?'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Button("Add Item").Icon(Icons.Plus)
                """",Languages.Csharp)
            | new Markdown(
                """"
                The `Button` constructor signature is `Button(string label, Func<Event<Button>, ValueTask>? onClick = null, ...)`. The second parameter is a click handler, not an icon. Use the `.Icon(Icons.X)` fluent method to set an icon on a button.
                
                **Found In:**
                f20dced8-1689-4289-a2d8-ee67136eb6ce
                7a9aadf3-097e-448d-8d5c-bc86152710a6
                
                ## InputBase.Label() — AxisExtensions method used on input
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // NumberInputBase
                stockAdjustment.ToNumberInput().Label("Adjustment amount")
                
                // DateTimeInputBase
                dateState.ToDateInput().Label("Birthdate")
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The type 'Ivy.NumberInputBase' cannot be used as type parameter 'T' in the generic type or method 'AxisExtensions.Label<T>(T, string)'` (same CS0311 error for `DateTimeInputBase`, `TextInputBase`, `SelectInputBase`, `BoolInputBase`, etc.)
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Use .WithField().Label() to wrap the input in a labeled field:
                stockAdjustment.ToNumberInput().WithField().Label("Adjustment amount")
                dateState.ToDateInput().WithField().Label("Birthdate")
                
                // Or use Text.Label() as a separate element above the input:
                Layout.Vertical()
                    | Text.Label("Adjustment amount")
                    | stockAdjustment.ToNumberInput()
                
                // Or use a form with .Label() on the form builder:
                state.ToForm().Label(m => m.Amount, "Adjustment amount")
                """",Languages.Csharp)
            | new Markdown(
                """"
                `.Label()` is an `AxisExtensions` method for chart axes, not for inputs. This applies to ALL input types (`NumberInputBase`, `DateTimeInputBase`, `TextInputBase`, `SelectInputBase`, `BoolInputBase`, etc.). The preferred way to label an input is `.WithField().Label("...")`, which wraps the input in a `Field` with a label.
                
                **Found In:**
                f20dced8-1689-4289-a2d8-ee67136eb6ce
                2e91e9c7-9c03-4b86-a9d2-c0417bcf715f
                7a9aadf3-097e-448d-8d5c-bc86152710a6
                
                ## Tab.Content() — non-existent fluent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Tab("Customer Info").Content(
                    Layout.Vertical() | ...
                )
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Tab' does not contain a definition for 'Content' and the best extension method overload 'ButtonExtensions.Content(Button, object)' requires a receiver of type 'Ivy.Button'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Tab("Customer Info", Layout.Vertical() | ...)
                """",Languages.Csharp)
            | new Markdown(
                """"
                `Tab` takes content as the second constructor parameter: `Tab(string title, object? content = null)`. There is no `.Content()` fluent method. This is the same pattern as `ListItem.Content()` — the agent invents fluent `.Content()` methods on widgets that accept content through constructors.
                
                **Note:** The IvyQuestion MCP tool also hallucinated this same API, returning `.Content()` as valid in two separate answers, reinforcing the agent's mistake.
                
                **Found In:**
                41ae072b-2845-46f1-bd0b-a4a6370c6807
                
                ## Layout.Tabs() | Tab — pipe operator on TabView
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Layout.Tabs()
                    | customerInfoTab
                    | yourInfoTab
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `Operator '|' cannot be applied to operands of type 'TabView' and 'Tab'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Layout.Tabs(customerInfoTab, yourInfoTab)",Languages.Csharp)
            | new Markdown(
                """"
                The `|` pipe operator works on `LayoutView` (for composing children) but does NOT exist on `TabView`. Tabs must be passed as constructor arguments via `Layout.Tabs(params Tab[] tabs)`.
                
                **Found In:**
                41ae072b-2845-46f1-bd0b-a4a6370c6807
                
                ## ToastVariant — non-existent enum
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                client.Toast("Error!", ToastVariant.Destructive)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The name 'ToastVariant' does not exist in the current context`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                client.Toast("Success message");       // neutral toast
                client.Toast("Done!", "Title");        // with title
                client.Error("Something went wrong."); // error toast
                """",Languages.Csharp)
            | new Markdown(
                """"
                `ToastVariant` does not exist. The `IClientProvider.Toast()` method takes `(string message)` or `(string message, string title)`. For error toasts, use `client.Error(message)` instead.
                
                **Found In:**
                d90474ac-78b9-48c7-8317-3860ff36b9dd (sub-tasks 002–006, appeared in ALL sub-tasks)
                
                ## DateTimeVariant — wrong enum name
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("date.ToDateTimeInput().Variant(DateTimeVariant.Date)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The name 'DateTimeVariant' does not exist in the current context`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                date.ToDateInput()
                // or:
                date.ToDateTimeInput().Variant(DateTimeInputVariant.Date)
                """",Languages.Csharp)
            | new Markdown(
                """"
                The enum is `DateTimeInputVariant` (singular), not `DateTimeVariant` (missing "Input") or `DateTimeInputVariants` (old plural name). All input variant enums were renamed from plural to singular in Ivy-Framework#2546. Values: `DateTime`, `Date`, `Time`, `Month`, `Week`. **Auto-fixed:** The refactoring service automatically rewrites both `DateTimeVariant` and `DateTimeInputVariants` to `DateTimeInputVariant`.
                
                **Found In:**
                d90474ac-78b9-48c7-8317-3860ff36b9dd (sub-tasks 002–006, appeared in ALL sub-tasks)
                
                ## FormBuilder.Header() — non-existent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                entity.ToForm()
                    .Header("Edit Fund")
                    .Field(f => f.Name)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'FormBuilder<T>' does not contain a definition for 'Header'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                entity.ToForm()
                    .Field(f => f.Name)
                    .ToSheet(title: "Edit Fund")
                """",Languages.Csharp)
            | new Markdown(
                """"
                `FormBuilder` does not have a `.Header()` method. The title/header is set when converting the form to a dialog or sheet via `.ToDialog(title:)` or `.ToSheet(title:)`. The agent confused this with `Card.Header()` or `BladeHeader`.
                
                **Found In:**
                d90474ac-78b9-48c7-8317-3860ff36b9dd (sub-tasks 002, 003)
                
                ## Badge.Color(Colors.X) — non-existent fluent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Badge(match.Value).Color(Colors.Green)
                new Badge("No match").Color(Colors.Red)
                """",Languages.Csharp)
            | new Markdown("**Correct API:**").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Via constructor variant parameter:
                new Badge(match.Value, BadgeVariant.Success)
                
                // Via fluent shortcut methods:
                new Badge(match.Value).Success()
                new Badge("No match").Destructive()
                
                // Via explicit Variant() method:
                new Badge(match.Value).Variant(BadgeVariant.Info)
                """",Languages.Csharp)
            | new Markdown(
                """"
                Available `BadgeVariant` values: `Primary`, `Destructive`, `Secondary`, `Outline`, `Success`, `Warning`, `Info`. The agent confused `LabelExtensions.Color(Label, Colors)` (which exists for `Label`) with a Badge method. Badge uses `BadgeVariant`, not `Colors`.
                
                **Found In:**
                3c507fb4-71e1-4136-9d40-8eca6590250d
                ce144de9-0688-490a-bef6-b2766e323154
                642d3167-790d-48c4-a381-bfab78f928cc
                
                ## Callout.Color(Colors.X) — non-existent fluent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Callout("Error message").Color(Colors.Destructive)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Callout' does not contain a definition for 'Color' and the best extension method overload 'LabelExtensions.Color(Label, Colors)' requires a receiver of type 'Ivy.Label'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Callout.Error("Error message")
                Callout.Warning("Warning message")
                Callout.Info("Info message")
                Callout.Success("Success message")
                """",Languages.Csharp)
            | new Markdown(
                """"
                `Callout` uses static factory methods, not a constructor + `.Color()` chain. This is a variant of the documented `Callout.Destructive()` hallucination — both stem from the agent trying to apply fluent styling to Callout instead of using the static factory pattern. To change variant after creation, use `.Variant(CalloutVariant.Warning)`.
                
                **Found In:**
                3c507fb4-71e1-4136-9d40-8eca6590250d
                
                ## Spacer(int) constructor — non-existent constructor overload
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Spacer(6)
                new Spacer(2)
                new Spacer(4)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Spacer' does not contain a constructor that takes 1 arguments`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Spacer().Height(Size.Units(6))
                // or
                new Spacer().Width(Size.Units(6))
                """",Languages.Csharp)
            | new Markdown(
                """"
                Spacer has only a parameterless constructor. Use fluent `.Height()` or `.Width()` to set size.
                
                **Found In:**
                276d383f-696e-4d67-bc6e-14502c59734b
                
                ## Button.Color(Colors.X) — non-existent fluent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new Button(label).Color(colors[i])",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Button' does not contain a definition for 'Color' and the best extension method overload 'LabelExtensions.Color(Label, Colors)' requires a receiver of type 'Ivy.Label'`
                
                **Correct API:**
                Button doesn't have `.Color()`. Use `.Variant(ButtonVariant.X)` or fluent shortcuts like `.Primary()`, `.Destructive()`. `.Color()` only exists on `Label` via `LabelExtensions`. Variant of documented `Badge.Color()` and `Callout.Color()` patterns.
                
                **Found In:**
                276d383f-696e-4d67-bc6e-14502c59734b
                
                ## UseAlert().ShowInfo() — wrong API usage
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var alert = UseAlert();
                alert.ShowInfo("title", "message");
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'(IView? alertView, ShowAlertDelegate showAlert)' does not contain a definition for 'ShowInfo'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var (alertView, showAlert) = UseAlert();
                showAlert("message", result => { }, "title", AlertButtonSet.Ok);
                """",Languages.Csharp)
            | new Markdown(
                """"
                `UseAlert()` returns a tuple `(IView? alertView, ShowAlertDelegate showAlert)`, not an object with methods. Destructure the tuple and call the delegate directly. The `alertView` must be included in the returned view tree.
                
                **Found In:**
                276d383f-696e-4d67-bc6e-14502c59734b
                
                ## Size.Flex() — non-existent static method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Spacer().Width(Size.Flex())
                .Height(Size.Flex())
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Size' does not contain a definition for 'Flex'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new Spacer().Width(Size.Grow())",Languages.Csharp)
            | new Markdown(
                """"
                The agent confused CSS flexbox terminology with Ivy's API.
                
                **Found In:**
                276d383f-696e-4d67-bc6e-14502c59734b
                
                ## RefreshToken.Version — non-existent property
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("refreshToken.Version",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'RefreshToken' does not contain a definition for 'Version'`
                
                **Correct API:**
                `RefreshToken` has these members: `Token` (Guid), `ReturnValue` (object?), `IsRefreshed` (bool), `Refresh()`, `ToTrigger()`. There is no `Version` property. Pass `refreshToken` directly as a dependency to `UseQuery`, or use `refreshToken.Token` if you need a changing value.
                
                Source: `D:\Repos\_Ivy\Ivy-Framework\src\Ivy\Hooks\UseRefreshToken.cs`
                
                **Found In:**
                a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 002-005, 008-009)
                
                ## QueryResult<T>.Data — wrong property name
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("queryResult.Data",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'QueryResult<T>' does not contain a definition for 'Data'`
                
                **Correct API:**
                `queryResult.Value` — The property is `.Value`, not `.Data`. `QueryResult<T>` is a record with: `Value` (T?), `Loading` (bool), `Validating` (bool), `Previous` (bool), `Mutator` (QueryMutator<T>), `Error` (Exception?).
                
                Source: `D:\Repos\_Ivy\Ivy-Framework\src\Ivy\Hooks\UseQuery.cs`
                
                **Found In:**
                a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 002, 004)
                
                ## QueryResult<T>.IsLoading — wrong property name
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("queryResult.IsLoading",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'QueryResult<T>' does not contain a definition for 'IsLoading'`
                
                **Correct API:**
                `queryResult.Loading` — The property is `.Loading`, not `.IsLoading`. Similarly, `.Validating` not `.IsValidating`, and `.Previous` not `.IsPrevious`.
                
                **Found In:**
                a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 002, 004)
                
                ## ListItem.Description / ListItem.Meta / ListItem.Actions — non-existent members
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                ListItem.Description("text")
                ListItem.Meta("text")
                ListItem.Actions(button1, button2)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'ListItem' does not contain a definition for 'Description'/'Meta'/'Actions'`
                
                **Correct API:**
                `ListItem` is a record with constructor parameters: `title`, `subtitle`, `onClick`, `icon`, `badge`, `tag`, `items`. Use `subtitle` for descriptions. There are no `.Description()`, `.Meta()`, or `.Actions()` methods. The only extension method is `.Content(child)`.
                
                Source: `D:\Repos\_Ivy\Ivy-Framework\src\Ivy\Widgets\Lists\ListItem.cs`
                
                **Found In:**
                a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 008, 009)
                
                ## Size.Sm — non-existent member
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Size.Sm",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Size' does not contain a definition for 'Sm'`
                
                **Correct API:**
                `Size` does not have Tailwind-style size aliases like `Sm`, `Md`, `Lg`. Use `Size.Units(n)` for specific pixel values, or `Size.Full()`, `Size.Grow()`, `Size.Fit()` for relative sizing.
                
                **Found In:**
                a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 008, 009)
                
                ## String literal as Icons? — wrong type
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Using string literals like "edit", "delete", "trash" where Icons? is expected
                new RowAction("Edit", icon: "edit")
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `Cannot implicitly convert type 'string' to 'Ivy.Icons?'`
                
                **Correct API:**
                Always use the `Icons` enum: `Icons.Pencil`, `Icons.Trash2`, `Icons.Plus`, etc. There is no implicit conversion from string to Icons. The refactoring service already handles invalid Icons enum values via LLM-based matching, but it cannot fix string-to-enum type mismatches.
                
                **Found In:**
                a224c9f6-94b2-4b9f-9d5c-6a9ba67d5b3b (traces 002, 003, 005, 008)
                
                ## Text.Small("text") — static factory confusion
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Text.Small(frequencyText).Muted()",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `No overload for method 'Small' takes 1 arguments`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Text.P(frequencyText).Small().Muted()
                // or
                Text.Block(frequencyText).Small().Muted()
                """",Languages.Csharp)
            | new Markdown(
                """"
                `Small()` is an instance modifier on `TextBuilder` (returns `Scale(Ivy.Scale.Small)`), not a static factory. The static factories are `Text.P()`, `Text.H1()`, `Text.H2()`, `Text.H3()`, `Text.H4()`, `Text.Block()`, `Text.Label()`, etc. Chain `.Small()` after creating the text.
                
                **Found In:**
                ce144de9-0688-490a-bef6-b2766e323154
                
                ## Text.Secondary("text") — non-existent static factory
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Text.Secondary("some text")
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `CS1501: No overload for method 'Secondary' takes 1 arguments`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Use Text.Muted() for secondary/muted appearance:
                Text.Muted("some text")
                // Or use Text.P() with .Muted() chained:
                Text.P("some text").Muted()
                // Or use Text.P() with Colors.Secondary color:
                Text.P("some text").Color(Colors.Secondary)
                """",Languages.Csharp)
            | new Markdown(
                """"
                `Text.Secondary()` does not exist as a static factory method. The static factories on `Text` are: `H1`, `H2`, `H3`, `H4`, `H5`, `H6`, `P`, `Inline`, `Block`, `Blockquote`, `Monospaced`, `Lead`, `Label`, `Muted`, `Strong`, `Bold`, `Danger`, `Warning`, `Success`, `Code`, `Markdown`, `Json`, `Xml`, `Html`, `Latex`, `Display`, `Literal`, `Rich`. The agent likely confused `Secondary` from `ButtonVariant.Secondary` / `Button.Secondary()` or `BadgeVariant.Secondary` / `Badge.Secondary()` with the `Text` API. `.Secondary()` is a fluent method on `Button` and `Badge`, not on `Text`.
                
                **Found In:**
                (session not yet recorded)
                
                ## Box.BorderRadius(int) — wrong argument type
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new Box(content).BorderRadius(8)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Box' does not contain a definition for 'BorderRadius'` (CS1929 — no extension matches `Box.BorderRadius(int)`)
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new Box(content).BorderRadius(BorderRadius.Rounded)",Languages.Csharp)
            | new Markdown(
                """"
                `Box.BorderRadius()` takes a `BorderRadius` enum (`None`, `Rounded`, `Full`), not an integer. The agent ignored the IvyQuestion MCP response and used an int literal instead.
                
                **Found In:**
                ce144de9-0688-490a-bef6-b2766e323154
                
                ## BorderRadius.Medium — non-existent enum value
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                BorderRadius.Medium
                BorderRadius.Large
                BorderRadius.Small
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'BorderRadius' does not contain a definition for 'Medium'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                BorderRadius.None     // no rounding
                BorderRadius.Rounded  // standard rounded corners
                BorderRadius.Full     // fully rounded (pill shape)
                """",Languages.Csharp)
            | new Markdown(
                """"
                Valid `BorderRadius` values: `None`, `Rounded`, `Full`. The agent hallucinates Tailwind-style size variants (`Small`, `Medium`, `Large`, `Xl`) that don't exist.
                
                **Found In:**
                050136ca-9275-4e1d-9740-e393b544c1b5
                8a776329-6dc7-474f-aa4d-c8b4da753a25 (BorderRadius.Large)
                4e59e443-3579-4df9-af4b-765b7b7d61c8 (BorderRadius.Small — via IvyMcp hallucination)
                
                ## GridView.Background() — non-existent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Layout.Grid(items).Columns(8).Gap(1).Background(Colors.Slate)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'GridView' does not contain a definition for 'Background'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new Box(
                    Layout.Grid(items).Columns(8).Gap(1)
                ).Color(Colors.Slate)
                """",Languages.Csharp)
            | new Markdown(
                """"
                `GridView` does not have a `.Background()` method. To add a background color to a grid, wrap it in a `Box` and use `.Color()` on the Box. This pattern applies to any view that needs a background color — `Box` is the universal container for adding visual styling.
                
                **Found In:**
                7e97011f-41b3-42d3-98ea-3b7faad347c2
                
                ## Size.Pixels() — wrong method name
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Size.Pixels(280)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Size' does not contain a definition for 'Pixels'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("Size.Px(280)",Languages.Csharp)
            | new Markdown(
                """"
                The method is `Size.Px()`, not `Size.Pixels()`. The agent expanded the abbreviated name. **Auto-fixed:** The refactoring service automatically rewrites `Size.Pixels(...)` → `Size.Px(...)`.
                
                **Found In:**
                7c51c481-c48e-4398-8db3-60cfac6379d5 (trace 002)
                
                ## string.ToCodeInput() — wrong receiver type
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                responseBody.Value.ToCodeInput().Language(Languages.Json)
                responseHeaders.Value.ToCodeInput().Language(Languages.Text)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'string' does not contain a definition for 'ToCodeInput' and the best extension method overload 'CodeInputExtensions.ToCodeInput(IAnyState, ...)' requires a receiver of type 'Ivy.IAnyState'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // For read-only display of code, use CodeBlock:
                new CodeBlock(stringValue, Languages.Json)
                
                // For editable code input, bind to state first:
                var editableState = UseState(stringValue);
                editableState.ToCodeInput().Language(Languages.Json)
                """",Languages.Csharp)
            | new Markdown(
                """"
                `.ToCodeInput()` is an extension on `IAnyState`, not on `string`. For display-only code, use `CodeBlock` instead of a code input. Only use `.ToCodeInput()` when the user needs to edit the code, and bind the string to state first.
                
                **Found In:**
                535f38d4-b9d5-43bf-a3d9-b4b17e6ecbb0
                
                ## State<T> — non-existent type
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("private State<List<Player>> _players;",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The type or namespace name 'State<>' could not be found`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("var players = UseState(new List<Player>());",Languages.Csharp)
            | new Markdown(
                """"
                `State<T>` does not exist. `UseState<T>()` returns `IState<T>`. State is created inside `Build()` via hooks, not stored as fields.
                
                **Found In:**
                84faf65a-c7df-4b5a-888b-4c49255c50ab (traces 004, 006, 008)
                
                ## IRefreshToken — non-existent interface
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("private readonly IRefreshToken _refreshToken;",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The type or namespace name 'IRefreshToken' could not be found`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("var refreshToken = UseRefreshToken();",Languages.Csharp)
            | new Markdown(
                """"
                `IRefreshToken` does not exist. `UseRefreshToken()` returns a `RefreshToken` class. Like all hooks, call inside `Build()`.
                
                **Found In:**
                84faf65a-c7df-4b5a-888b-4c49255c50ab (traces 004, 005, 006)
                
                ## DataTable<T> — non-generic type used with type arguments
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new DataTable<Player>(players)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `The non-generic type 'DataTable' cannot be used with type arguments`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("players.ToDataTable()",Languages.Csharp)
            | new Markdown(
                """"
                `DataTable` is non-generic. Use `.ToDataTable()` extension method on `IEnumerable<T>` or `IQueryable<T>`.
                
                **Found In:**
                84faf65a-c7df-4b5a-888b-4c49255c50ab (trace 004)
                
                ## Shrink(int) — method takes no arguments
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Text.P("vs").Shrink(1)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `No overload for method 'Shrink' takes 1 arguments`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Text.P("vs").Shrink()
                """",Languages.Csharp)
            | new Markdown(
                """"
                `.Shrink()` takes no arguments. It is a simple fluent modifier.
                
                **Found In:**
                84faf65a-c7df-4b5a-888b-4c49255c50ab (traces 004, 007)
                
                ## Card.Padding() — non-existent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new Card(content).Padding(20)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Card' does not contain a definition for 'Padding'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new Box(content).Padding(20)",Languages.Csharp)
            | new Markdown(
                """"
                `Card` has no `.Padding()` method. Cards have built-in padding. For custom padding, wrap content in a `Box`.
                
                **Found In:**
                84faf65a-c7df-4b5a-888b-4c49255c50ab (trace 004)
                
                ## SelectInput.Width() — generic constraint mismatch
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("language.ToSelectInput(options).Width(Size.Px(200))",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `CS0311: The type 'Ivy.SelectInput<string>' cannot be used as type parameter 'T' in the generic type or method 'WidgetBaseExtensions.Width<T>(T, Size?)'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Cast to SelectInputBase first:
                (SelectInputBase)language.ToSelectInput(options).Width(Size.Px(200))
                // Or wrap in a Box with width:
                new Box(language.ToSelectInput(options)).Width(Size.Px(200))
                """",Languages.Csharp)
            | new Markdown(
                """"
                `SelectInput<T>` inherits from `SelectInputBase : WidgetBase<SelectInputBase>`, not `WidgetBase<SelectInput<T>>`. The `Width<T>()` extension requires `T : WidgetBase<T>`, which `SelectInput<T>` doesn't satisfy.
                
                ### Found In
                
                852f6bec-756c-48f8-93da-ad426af73fab
                
                ## Align.End / Align.Start — CSS-inspired enum values
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Align.End
                Align.Start
                Align.FlexEnd
                Align.FlexStart
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Align' does not contain a definition for 'End'` (CS0117)
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Align.Right   // instead of Align.End or Align.FlexEnd
                Align.Left    // instead of Align.Start or Align.FlexStart
                """",Languages.Csharp)
            | new Markdown(
                """"
                Valid `Align` values: `TopLeft`, `TopRight`, `TopCenter`, `BottomLeft`, `BottomRight`, `BottomCenter`, `Left`, `Right`, `Center`, `Stretch`, `SpaceBetween`, `SpaceAround`, `SpaceEvenly`.
                
                The agent draws from CSS `justify-content: flex-end` / `align-items: flex-end` terminology. **Auto-fixed:** The refactoring service automatically rewrites `Align.End` → `Align.Right`, `Align.Start` → `Align.Left`, etc.
                
                **Found In:**
                DecisionMatrixApp.cs (two occurrences of `Align.End`)
                
                ## LayoutView.Border() — now supported
                
                LayoutView supports `.Border(color, thickness)` for adding borders. Example:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new LayoutView()
                    .Border(Colors.Gray, 1)
                    .Padding(4)
                    .Vertical(content);
                """",Languages.Csharp)
            | new Markdown(
                """"
                Individual properties are also available: `.BorderColor()`, `.BorderThickness()`, `.BorderStyle()`, `.BorderRadius()`.
                
                Note: `.Border()` expects a `Colors` enum as the first argument, not a string. Thickness accepts `int` (uniform) or `Thickness` struct — do NOT pass `Ivy.Thickness` where `int` is expected.
                
                ## Server Configuration
                
                | Hallucinated API | Correct API |
                |-----------------|-------------|
                | `server.UseSingleApp()` | `server.UseDefaultApp(typeof(AppType))` |
                | `server.UseNoChrome()` | `server.UseDefaultApp(typeof(AppType))` — omit `UseChrome()` instead |
                | `server.UseDefaultApp<T>()` | `server.UseDefaultApp(typeof(T))` — takes Type, not generic |
                
                ## TextBuilder.Style() — non-existent styling method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Text.P("🐶").Style("font-size: 48px")
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'TextBuilder' does not contain a definition for 'Style'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Text.P("🐶").Large()
                Text.P("text").Medium()
                Text.P("text").Small()
                """",Languages.Csharp)
            | new Markdown(
                """"
                `TextBuilder` does not have a `.Style()` method for arbitrary CSS. Use `.Large()`, `.Medium()`, or `.Small()` fluent modifiers. The agent invented a CSS-style `.Style()` method similar to JSX `style` props. Variant of the documented `WithFontSize()` hallucination.
                
                Also hallucinated: `Text.Code(expr).FontSize(24)` — CS1929: `.FontSize()` is an extension on `LabelList`, not `TextBuilder`.
                
                **Found In:**
                88e4f0bb-d358-4b34-9458-bc7eb98845e5, 625c285f-068b-4de3-b01c-ae2f7286a5d8
                
                ## TextBuilder.AlignCenter() — non-existent method
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                Text.H1("$0.00").AlignCenter()
                Text.H3("00:00:00").AlignCenter()
                Text.P("Rate: $50.00/hour").AlignCenter()
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `CS1061: 'TextBuilder' does not contain a definition for 'AlignCenter'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // TextBuilder does not have alignment methods.
                // To center text, wrap it in a layout:
                Layout.Vertical().Align(Align.Center)
                    | Text.H1("$0.00")
                    | Text.H3("00:00:00")
                
                // Or use a Box:
                new Box(Text.H1("$0.00")).Align(Align.Center)
                """",Languages.Csharp)
            | new Markdown(
                """"
                `TextBuilder` has no `.AlignCenter()` method. Text alignment is controlled at the layout/container level, not on individual text elements.
                
                **Found In:**
                713546f7-32fb-4961-ab78-def91e7c010d
                
                ## FileUploadStatus.Completed — non-existent enum value
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("if (upload.Status == FileUploadStatus.Completed)",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'FileUploadStatus' does not contain a definition for 'Completed'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("if (upload.Status == FileUploadStatus.Finished)",Languages.Csharp)
            | new Markdown(
                """"
                `FileUploadStatus` values are: `Pending`, `Aborted`, `Loading`, `Failed`, `Finished`. There is no `Completed` value. **Auto-fixed:** The refactoring service automatically rewrites `FileUploadStatus.Completed` → `FileUploadStatus.Finished`.
                
                **Found In:**
                (session not yet recorded)
                
                ## UseDownload — ambiguous overload between sync and async
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                UseDownload(() => bytes, "file.txt", "text/plain")
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `CS0121: The call is ambiguous between 'ViewBase.UseDownload(Func<byte[]>, string, string)' and 'ViewBase.UseDownload(Func<Task<byte[]>>, string, string)'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // For sync: explicitly type the delegate
                UseDownload((Func<byte[]>)(() => bytes), "file.txt", "text/plain")
                
                // Or use a named method:
                byte[] GetBytes() => bytes;
                UseDownload(GetBytes, "file.txt", "text/plain")
                """",Languages.Csharp)
            | new Markdown(
                """"
                When using `UseDownload` with a lambda, you must explicitly cast to `Func<byte[]>` or `Func<Task<byte[]>>` to avoid ambiguity.
                
                **Found In:**
                (session not yet recorded)
                
                ## Server.OnReady / Server.OnStartup — non-existent lifecycle callbacks
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                server.OnReady(() => { /* seed data */ });
                server.OnStartup(() => { /* initialize */ });
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `CS1061: 'Server' does not contain a definition for 'OnReady'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Seed data via the context factory pattern:
                var connection = server.UseConnection<MyDbContext>(options =>
                    options.ContextFactory = () =>
                    {
                        var ctx = new MyDbContext();
                        ctx.Database.EnsureCreated();
                        SeedData(ctx);
                        return ctx;
                    });
                
                // Or resolve services directly in Program.cs:
                var myService = server.Services.GetRequiredService<IMyService>();
                myService.Initialize();
                """",Languages.Csharp)
            | new Markdown(
                """"
                The `Server` class does not have `OnReady`, `OnStartup`, or similar lifecycle callback methods. To run initialization code (e.g., database seeding), use the connection's context factory pattern — seed data in the factory's `CreateContext` method or use `server.Services` to resolve and call services directly in `Program.cs`.
                
                **Found In:**
                (session not yet recorded)
                
                ## MetricCard — non-existent class name
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new MetricCard("Title", "Value", Icons.Activity)
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `CS0246: The type or namespace name 'MetricCard' could not be found`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                new MetricView("Title", "Value", icon: Icons.Activity)
                """",Languages.Csharp)
            | new Markdown(
                """"
                `MetricCard` does not exist. The correct class is `MetricView`. Constructor: `MetricView(string title, string value, string? description = null, Icons? icon = null, IView? chart = null)`.
                
                **Found In:**
                c008af27-1cb1-4ab3-b41a-36aa711c6a41
                
                ## Disposable.Create() — missing using statement
                
                **Hallucinated usage (missing using):**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("return Disposable.Create(() => timer?.Dispose());",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `CS0103: The name 'Disposable' does not exist in the current context`
                
                **Fix:** Add the using statement — the package IS available as a transitive dependency:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                using System.Reactive.Disposables;
                
                return Disposable.Create(() => timer?.Dispose());
                """",Languages.Csharp)
            | new Markdown(
                """"
                `System.Reactive` is a transitive dependency of Ivy Framework. The error occurs because the agent omits the `using System.Reactive.Disposables;` directive, not because the package is missing.
                
                **Found In:**
                fb184b5b-8254-4a1f-b8f2-ab8e8657fdbc
                
                ## Fragment.Empty — non-existent static member
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("return Fragment.Empty;",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `'Fragment' does not contain a definition for 'Empty'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Use ViewBase.Empty:
                return ViewBase.Empty;
                
                // Or return an empty Fragment:
                return new Fragment();
                
                // Or just return null:
                return null;
                """",Languages.Csharp)
            | new Markdown(
                """"
                `Fragment` does not have an `Empty` static member. To return nothing from a view, use `ViewBase.Empty`, `new Fragment()`, or `null`.
                
                **Found In:**
                (session not yet recorded)
                
                ## TextInput.Grow() — Box-only extension called on TextInput
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new TextInput(query).Grow()",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** `CS1929: 'TextInput' does not contain a definition for 'Grow'`
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("new TextInput(query).Width(Size.Grow())",Languages.Csharp)
            | new Markdown(
                """"
                `Grow()` was originally defined only as a `Box`-specific extension method in `Box.cs`. It is not available on `TextInput` or other widget types. Use `.Width(Size.Grow())` directly, or note that `Grow()` has since been promoted to a generic `WidgetBase<T>` extension and is now available on all widgets.
                
                **Found In:**
                7a9aadf3
                
                ## AppAttribute.path old parameter name
                
                **Hallucinated API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App("Dashboard", path: ["Dashboards"])]
                """",Languages.Csharp)
            | new Markdown(
                """"
                **Error:** 'AppAttribute' does not contain a constructor that takes... / does not have a parameter named 'path'
                
                **Correct API:**
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                [App("Dashboard", group: ["Dashboards"])]
                """",Languages.Csharp)
            | new Markdown(
                """"
                The path: parameter on AppAttribute was renamed to group: (Ivy-Framework#2587) because it is used to specify a group/category name in the sidebar. Agents trained on older data might still use path:. **Auto-fixed:** The refactoring service automatically rewrites path: to group: in [App] attributes.
                
                **Found In:**
                875efaff-8eb2-4604-b3aa-a2b5799df88c
                """").OnLinkClick(onLinkClick)
            ;
        return article;
    }
}

