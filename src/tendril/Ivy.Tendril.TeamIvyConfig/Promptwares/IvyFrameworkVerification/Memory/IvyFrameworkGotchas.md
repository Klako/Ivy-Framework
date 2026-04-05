# Ivy Framework Gotchas

Common mistakes and issues encountered when working with the Ivy Framework during feature testing and development.

## Hook Rules (Critical)

### Hook Ordering — IVYHOOK005
**All Ivy hooks MUST be called at the very top of `Build()` method, before ANY other statements.**

❌ **WRONG — Causes IVYHOOK005 warning:**
```csharp
public override object? Build() {
    var state1 = UseState<int>(() => 0);
    var value = state1.Value * 2;        // ❌ Logic between hooks
    var state2 = UseState<string>("");   // ❌ Hook after logic
    return Layout.Vertical() | ...;
}
```

✅ **CORRECT — All hooks first:**
```csharp
public override object? Build() {
    var state1 = UseState<int>(() => 0);
    var state2 = UseState<string>(() => "");
    var service = UseService<IClientProvider>();

    var value = state1.Value * 2;
    return Layout.Vertical() | ...;
}
```

**Why**: Ivy's hook system relies on consistent call order across renders. Conditional or out-of-order hooks break state tracking.

### Extension Hooks on IViewContext (UseLoading, UseAlert, etc.)
❌ **`UseLoading()`** — direct call from ViewBase fails with CS0103
✅ **`this.Context.UseLoading()`** — extension methods on `IViewContext` must be called via `this.Context`
📝 **Why**: `ViewBase` wraps hooks like `UseState`, `UseEffect` as protected methods. But extension methods on `IViewContext` aren't wrapped.

### TryUseService Requires Context Prefix
❌ **`TryUseService<T>(out var x)`** — CS0103 from ViewBase
✅ **`Context.TryUseService<T>(out var x)`**
📝 `UseService<T>()` IS available directly on `ViewBase` (without `Context.`).

## Serialization & CamelCase Patterns

### DataKey String Values Are NOT CamelCased
Ivy's `WidgetSerializer` uses `JsonNamingPolicy.CamelCase` for property names and dictionary keys, but string VALUES (like `DataKey`) are sent as-is.

- Data `new { Height = 165 }` serializes to `{"height": 165}`
- `XAxis("Height").DataKey` serializes as the string `"Height"`
- Frontend: `d["Height"]` is `undefined` because the key is `"height"`

📝 **Pattern**: Always use case-insensitive lookups when mapping C#-serialized data to frontend chart properties. This affects any chart widget using DataKey for value lookup.

### Enum PascalCase vs Frontend Lowercase
C# enums serialize as PascalCase strings (e.g., `"Week"`). Frontend libraries often expect lowercase. Always normalize with `.toLowerCase()` when receiving C# enum values on the frontend.

📝 Any C# enum prop that the frontend expects as a number needs a string-to-number conversion, because Ivy's `JsonEnumConverter` always serializes enums as their string name.

### Enum Display Names — PascalCase Split
`EnumHelper.GetDescription()` (used by `typeof(MyEnum).ToOptions()`) calls `StringHelper.SplitPascalCase()`:
- `SciFi` → "Sci Fi", `ExtraLarge` → "Extra Large"

❌ In Playwright: `getByText('SciFi')` won't find the label
✅ Match the split name: `getByText('Sci Fi')`
📝 `enum.ToString()` still returns the raw member name (e.g., `SciFi`).

### WidgetSerializer Strips Default Enum Values — Ongoing Pattern
The WidgetSerializer strips properties matching the parameterless constructor default. `DataTableColumn.ColType` defaults to `ColType.Number` (enum value 0), so Number columns have `type: undefined` in the frontend.

**Files that need `(col.type ?? 'text')` null guard:**
- `columnHelpers.ts` — `mapColumnIcon()` ⚠️ (guard still missing as of Apr 2026)
- `calculateAutoWidth.ts` — `calculateAutoWidth()`
- `cellContent.ts` — already guarded
- `DataTableFilterOption.tsx` — already guarded

📝 **Warning**: When rewriting any of these files, always preserve null guards. Any new widget with enum props where value 0 is the "active" mode will hit this bug.

### Dictionary Key vs String Value CamelCase Mismatch
Ivy's `WidgetSerializer` uses `DictionaryKeyPolicy = JsonNamingPolicy.CamelCase` which camelCases dictionary keys, but string VALUES (like column.Icon) are sent as-is. When a dictionary key is used to look up a string value, the casing won't match.

## Widget Extension Method CS1660 Pattern

### OnSave/OnCancel/OnLineClick — Property Shadows Extension Method
When a widget record has an `[Event]` property (e.g., `OnSave`) that is a `Func<Event<T,V>, ValueTask>?`, calling `.OnSave(lambda)` resolves to **delegate invocation** (the property), not the extension method.

❌ **`.OnSave(() => {...})`** — CS1660 on ScreenshotFeedback, DiffView, and similar widgets
✅ **Use `with` expression:**
```csharp
new DiffView().Diff(diff) with
{
    OnLineClick = e => { state.Set(e.Value); return ValueTask.CompletedTask; }
}
```

📝 **Affected widgets**: ScreenshotFeedback (OnSave, OnCancel), DiffView (OnLineClick), and any widget with `Func<Event<T,V>, ValueTask>?` properties. The zero-arg overload works because the delegate requires one parameter and doesn't match.

## TestId / DOM Structure Issues

### TestId Only Works on WidgetBase Types
✅ **Works on**: `WidgetBase` types (inputs, buttons, cards)
❌ **Doesn't work on**: `TextBuilder`, `LayoutView`, and other non-widget types — `TextBuilder` does NOT extend `WidgetBase<TextBuilder>`, so `.TestId()` causes CS0311
✅ **Workaround**: Use `getByText()` for text content verification

### Widgets Where TestId Does NOT Render in DOM

| Widget | Workaround |
|--------|------------|
| Badge | `getByText()` for text content |
| ColorInput | `page.locator("input[type='color']")` |
| VideoPlayer | `page.locator('video').nth(0)` or `page.locator('iframe').nth(0)` |

📝 VideoPlayer's `.Id()` sets an Ivy-generated short hash, not the value you pass.

### NumberInput TestId Is on the `<input>`, Not a Wrapper
❌ **`page.getByTestId('my-number').locator('input')`** — testid IS the input
✅ **`page.getByTestId('my-number')`** — directly references the `<input>`
📝 The clear (X) button is NOT inside the testid element — walk up the DOM to find it.

### `ivy-widget` Custom Elements Have Height 0
❌ **`el.closest("ivy-widget")?.getBoundingClientRect().height`** — always 0
✅ **Measure content div inside**: `iw.querySelector(":scope > div")?.children[0]?.getBoundingClientRect()`

### Badge Renders as `<div>`, Not `<span>`
❌ **`document.querySelectorAll("span")` for badges** — Badge renders `<div>`
📝 CSS `inline-flex` gets blockified to `flex` inside flex containers. Check `className` instead of computed style.

## DataTable Gotchas

### Canvas-Based Grid — NOT HTML Table
❌ **`page.locator('[data-testid="gdg-canvas"]')`** — does not exist
✅ **`page.locator('canvas').first()`** or **`page.locator('[data-testid="data-grid-canvas"]')`**

### Canvas Click Intercepted by Scroller Overlay
❌ **`page.locator('canvas').first().click()`** — blocked by `dvn-scroller`
✅ **`page.locator('.dvn-scroller').first().click({ position: { x, y } })`**

### Dual Data Path (Cell Rendering vs Data Loading)
- `convertArrowTableToData()` — used for column metadata, row counts (NOT cell display)
- `useRowData()` — reads raw Arrow table for cell rendering

📝 **When fixing data conversion bugs**, the fix must be in `useRowData.ts`, not `convertArrowTableToData()`.

### Filter Panel Overlaps HeaderLeft Slot
❌ **Buttons in `.HeaderLeft()` unclickable when `AllowFiltering=true`** (default)
✅ **Workaround**: `.Config(config => config.AllowFiltering = false)` if filtering not needed

### TableBuilder `.Builder()` — No `.Custom()` Method
❌ **`f.Custom(val => widget)`** — does not exist
✅ **`f.Func((string val) => (object)widget)`**
📝 Available: `Default()`, `Text()`, `Link()`, `CopyToClipboard()`, `Func<TIn>()`, `Progress()`

## Component API Quick Reference

| Widget/Type | Missing API | Use Instead |
|-------------|-------------|-------------|
| Card | `.Default()` | `new Card(content)` |
| Size | `.Vh()`, `.Pixels()` | `.Px()` for pixels, `.Full()` for 100% |
| IState | `.Pipe()` | Inline expressions or local variable |
| AppBase | — | Use `ViewBase` for Ivy views |
| ButtonVariant | `.Default` | Use `Primary`, `Secondary`, `Ghost`, etc. |
| SelectInput | `.Value()`, `.OnChange()` | `state.ToSelectInput(options)` with UseEffect for side effects |
| MemoryStreamUploadHandler | `.Create()` zero-arg | `.Create(state)` with `IState<FileUpload<byte[]>?>` |
| Layout.Gap() | `Size` param | `bool` or `int` param |
| RefreshToken | `.Trigger()` | `.Refresh()` or `.Refresh(returnValue)` |
| CodeInput | Direct instantiation | `.ToCodeInput()` on `IState<string>`, or use `Markdown` with fenced code block |

### App Attribute: `group` Not `path`
❌ **`[App(path: new[] { "Tests" })]`** — CS1739
✅ **`[App(group: new[] { "Tests" })]`**

### Icons Enum
- ❌ `Icons.AlignCenter` → ✅ `Icons.AlignCenterHorizontal`
- 📝 Lucide renamed some icons (e.g., `AlertTriangle` → `TriangleAlert`)
- 📝 Chart icons: Use `Icons.ChartBarStacked` (not `Icons.BarChart`, `Icons.BarChart3`)

## Frontend Build Issues

### Stale Frontend Assets
When testing commits that change TypeScript files, the server may serve old bundled JS because assets are embedded into `Ivy.dll`.
✅ Always rebuild frontend before testing: `cd src/frontend && npm run build`

### WidgetRenderer.tsx File Casing Issue
Git tracks as `widgetRenderer.tsx` (lowercase) but disk is `WidgetRenderer.tsx`. Existing imports use lowercase. This causes TS1261 errors on modification. Use `npx vite build` to bypass `tsc -b` check if needed.

## Project Setup Issues

### Missing `using Ivy;` Directive
Compilation errors for `ViewBase`, `Icons`, etc. → add `using Ivy;`

### App Registration Required
Ivy does NOT auto-discover apps. Required in Program.cs:
```csharp
server.AddAppsFromAssembly(Assembly.GetExecutingAssembly());
```

### Nullable Enable Required
CS8632 warnings → add `<Nullable>enable</Nullable>` to `.csproj`

### Ivy.Analyser Reference
```xml
<ProjectReference Include="...\Ivy.Analyser.csproj"
                  OutputItemType="Analyzer"
                  ReferenceOutputAssembly="false" />
```

## Chrome Behavior

### Direct URL Routing Not Available With Chrome
`UseChrome()` active → `/app-id` shows "App Not Found". Apps must be accessed via sidebar clicks. `?chrome=false` also shows "App Not Found".

### `?chrome=false` Hides the Sidebar
Don't use `?chrome=false` when testing sidebar labels or navigation items.

### NavigateSignal Requires Chrome Wrapper
❌ `navigator.Navigate(beacon, entity)` in chrome=false mode — does NOT redirect
📝 `NavigateSignal` is `[Signal(BroadcastType.Chrome)]` — consumed by Chrome sidebar component only.

**Testing navigation:**
- ✅ Test state feedback before navigation (click counters, action logs)
- ✅ Test beacon discovery (`UseNavigationBeacon` returns non-null)
- ✅ Test target apps by navigating directly: `page.goto(\`http://localhost:\${port}/app-id?chrome=false\`)`

### Beacon AppId Must Match Full Registered ID
❌ `AppId: "customer-details"` → ✅ `AppId: "my-namespace/customer-details"`
📝 Use `dotnet run -- --describe` to find exact registered app ID.

## Playwright-Specific Gotchas

### DateTimeInput Click Target
❌ **`page.getByTestId('my-date').locator('button').first().click()`** — DateInput is a Popover trigger div, not a button
✅ **`page.getByTestId('my-date').click()`**
📝 TimeVariant uses `<input type="time">` directly.

### Clicking Buttons Behind Modal Overlays
❌ **`page.getByTestId('btn').click()`** — overlay intercepts pointer events
❌ **`click({ force: true })`** — Ivy's event handler doesn't process it
✅ **Use `page.evaluate` with `dispatchEvent`:**
```typescript
await page.evaluate((id) => {
  const btn = document.querySelector(`[data-testid="${id}"]`) as HTMLElement;
  if (btn) btn.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true }));
}, 'my-button-testid');
```

### SelectInput Multi-Select Uses CMDK, Not Radix Select
| Variant | Trigger | Items |
|---------|---------|-------|
| Single-select | `button[role="combobox"]` | `[role="option"]` |
| Multi-select | `getByPlaceholder()` | `[cmdk-item]` |

Multi-select popover stays open after selecting. Close with `Escape`.

### Float Formatting Locale Issues
❌ **`$"{volume.Value:F2}"`** — European locales produce `"0,50"`
✅ **`volume.Value.ToString("F2", CultureInfo.InvariantCulture)`**

### react-day-picker v9 Changes
- ❌ `fromDate`/`toDate` — v8 props, silently ignored in v9
- ✅ Use `disabled` with `DateBefore`/`DateAfter` matchers + `startMonth`/`endMonth`
- ❌ `page.locator("table thead th")` — v9 uses flex layout, NOT `<table>`
- ✅ `page.locator(".rdp-weekdays .rdp-weekday")` for weekday headers

### react-markdown v10 — No `inline` Prop on Code Component
❌ **`if (inline)`** — always `undefined` in v10
✅ **`!className`** to detect inline code (no language = inline)
✅ **`!el.closest('pre')`** — inline code has no `<pre>` wrapper

### Image Widget MinContent Default — Hidden in Playwright
❌ **`new Image(url)`** with default sizing — 0x0 until image loads
✅ **Set explicit dimensions**: `Width = Size.Px(200), Height = Size.Px(150)`

## Miscellaneous

### Namespace Conflicts with External Widget Types
❌ **`namespace ScreenshotFeedback`** when using `Ivy.Widgets.ScreenshotFeedback.ScreenshotFeedback`
✅ Use different namespace (e.g., `namespace ScreenshotFeedbackTest`)

### Ivy.Widgets.Xterm.Terminal Ambiguity with Ivy.Terminal
❌ `using Ivy; using Ivy.Widgets.Xterm;` — `Terminal` is ambiguous
✅ Use fully qualified extension methods: `Ivy.Widgets.Xterm.TerminalExtensions.OnInput(terminal, data => ...)`
✅ For non-event fluent methods: `using XTerminal = Ivy.Widgets.Xterm.Terminal;` works

### SVG xmlns Required for Data URI / SpriteMap Usage
❌ `<svg width="24" ...>` — missing xmlns causes silent decode failure
✅ `<svg xmlns="http://www.w3.org/2000/svg" ...>` — always include xmlns

### PathToAppIdMiddleware Intercepts Custom File Extensions
If you add middleware serving files with a custom extension (e.g., `.md`), add that extension to `staticFileExtensions` in `src/frontend/src/routing-constants.json`.

### Widget Children Pattern (Calendar/Kanban)
When passing structured children to widgets:
1. Add child type filter in `WidgetRenderer.tsx` (both paths)
2. Use `widgetNodeChildren` for metadata, `slots.default[index]` for rendered content
3. Register both parent and child in `widgetMap.ts`
4. Check `widgetNode.children && widgetNode.children.length > 0` before using slot content (empty fragments are truthy)

### Ivy.csproj Rust Binary Build — MSBuild Glob Issue
❌ `dotnet build` fails with MSB3024 for `*rustserver*.dll` glob
✅ Create CI/CD artifacts path: `mkdir -p src/RustServer/artifacts/native/win-x64 && cp src/RustServer/target/release/rustserver.dll src/RustServer/artifacts/native/win-x64/`

## Historical Issues (Fixed)

These bugs have been fixed. Kept for reference in case of regressions.

### RadarChart CamelCase Lookup (FIXED)
`RadarChartWidget.tsx` line 122 used case-sensitive `item[ind.name]` for explicit `.Radar()` config. Fixed to use `getPropertyValue(item, ind.name)` (case-insensitive).

### DayOfWeek Enum Serialization (FIXED)
C# `DayOfWeek` serialized as string but `react-day-picker` `weekStartsOn` expects a number. Fixed with `resolveDayOfWeek()` in `DateTimeInputWidget.tsx` and `DateRangeInputWidget.tsx`.

### Video PlaybackRate Browser Reset (FIXED)
Setting only `videoElement.playbackRate` in useEffect was reset during media load. Fixed by setting both `defaultPlaybackRate` and `playbackRate`, plus re-applying in `onLoadedData` handler.

### SignatureInput OnChange Not Wired (FIXED)
`OnChange => null` (expression-body) meant OnChange was never set. Fixed to `OnChange { get; }` with constructor wiring. Also: frontend must strip `data:` prefix from `canvas.toDataURL()` before sending to C# byte[] deserialization.

### DataTable Custom Header Icons — Three Bugs (FIXED)
1. `mapColumnIcon()` discarded custom icon names (returned `GridColumnIcon.HeaderString`)
2. `showColumnTypeIcons` gate blocked explicit icons
3. CamelCase mismatch between dictionary keys and Icon values

### FileDialog Upload Mode Prop Stripped (FIXED)
`FileDialogMode.Upload` (enum value 0) was stripped by WidgetSerializer. Fixed with `mode = 'Upload'` default in `FileDialogWidget.tsx`.

### WidgetSerializer Default Enum Value Stripping (FIXED)
Number columns had `type: undefined` because `ColType.Number` (enum 0) was stripped. Fixed with null guards in `calculateAutoWidth.ts` and `cellContent.ts`. See "Ongoing Pattern" in Serialization section for remaining guard needs.

## Future Gotchas

As we encounter more issues, add them with:
- ❌ **What doesn't work**
- ✅ **What does work** (solution)
- 📝 **Why** (explanation when helpful)
