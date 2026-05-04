namespace Ivy.Samples.Shared.Apps.Widgets.Inputs;

[App(icon: Icons.TextCursorInput, group: ["Widgets", "Inputs"], searchHints: ["password", "textarea", "search", "email"])]
public class TextInputApp : SampleBase
{
    protected override object? BuildSample()
    {
        return Layout.Vertical()
               | Text.H1("Text Input")
               | Layout.Tabs(
                   new Tab("Variants", new TextInputVariants()),
                   new Tab("Ghost", new TextInputGhostDemo()),
                   new Tab("Sizes", new TextInputSizes()),
                   new Tab("Affixes", new TextInputAffixes()),
                   new Tab("Data Binding", new TextInputDataBinding()),
                   new Tab("Length Constraints", new TextInputLengthConstraints()),
                   new Tab("Events", new TextInputEventsTab())
               ).Variant(TabsVariant.Content);
    }
}

public class TextInputVariants : ViewBase
{
    public override object Build()
    {
        var withoutValue = UseState((string?)null);
        var withValue = UseState("Hello");

        return Layout.Grid().Columns(5)
               | null!
               | Text.Monospaced("Empty")
               | Text.Monospaced("With Value")
               | Text.Monospaced("Disabled")
               | Text.Monospaced("Invalid")

               | Text.Monospaced("TextInputVariant.Text")
               | withoutValue.ToTextInput().Placeholder("Placeholder")
               | withValue.ToTextInput()
               | withValue.ToTextInput().Disabled()
               | withValue.ToTextInput().Invalid("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec eros")

               | Text.Monospaced("TextInputVariant.Password")
               | withoutValue.ToPasswordInput().Placeholder("Placeholder").ShortcutKey("Ctrl+L")
               | withValue.ToPasswordInput()
               | withValue.ToPasswordInput().Disabled()
               | withValue.ToPasswordInput().Invalid("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec eros")

               | Text.Monospaced("TextInputVariant.Textarea")
               | withoutValue.ToTextareaInput().Placeholder("Placeholder").ShortcutKey("Ctrl+T")
               | withValue.ToTextareaInput()
               | withValue.ToTextareaInput().Disabled()
               | withValue.ToTextareaInput().Invalid("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec eros")

               | Text.Monospaced("TextInputVariant.Search")
               | withoutValue.ToSearchInput().Placeholder("Placeholder").ShortcutKey("Ctrl+K")
               | withValue.ToSearchInput()
               | withValue.ToSearchInput().Disabled()
               | withValue.ToSearchInput().Invalid("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec eros")

               | Text.Monospaced("TextInputVariant.Email")
               | withoutValue.ToEmailInput().Placeholder("Placeholder").ShortcutKey("Ctrl+E")
               | withValue.ToEmailInput()
               | withValue.ToEmailInput().Disabled()
               | withValue.ToEmailInput().Invalid("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec eros")

               | Text.Monospaced("TextInputVariant.Tel")
               | withoutValue.ToTelInput().Placeholder("Placeholder").ShortcutKey("Ctrl+J")
               | withValue.ToTelInput()
               | withValue.ToTelInput().Disabled()
               | withValue.ToTelInput().Invalid("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec eros")

               | Text.Monospaced("TextInputVariant.Url")
               | withoutValue.ToUrlInput().Placeholder("Placeholder").ShortcutKey("Ctrl+U")
               | withValue.ToUrlInput()
               | withValue.ToUrlInput().Disabled()
               | withValue.ToUrlInput().Invalid("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec eros");
    }
}

public class TextInputDataBinding : ViewBase
{
    public override object Build()
    {
        var stringState = UseState("");
        var nullStringState = UseState<string?>();

        return Layout.Grid().Columns(3)

               | Text.Monospaced("string")
               | (Layout.Vertical()
                  | stringState.ToTextInput()
                  | stringState.ToTextareaInput()
                  | stringState.ToPasswordInput()
                  | stringState.ToSearchInput()
               )
               | stringState

               | Text.Monospaced("string?")
               | (Layout.Vertical()
                  | nullStringState.ToTextInput()
                  | nullStringState.ToTextareaInput()
                  | nullStringState.ToPasswordInput()
                  | nullStringState.ToSearchInput()
               )
               | nullStringState;
    }
}

public class TextInputEventsTab : ViewBase
{
    public override object Build()
    {
        var onChangedState = UseState("");
        var onChangeLabel = UseState("");
        UseEffect(() => { onChangeLabel.Set(string.IsNullOrEmpty(onChangedState.Value) ? "" : "Changed"); }, onChangedState);
        var onBlurState = UseState("");
        var onBlurLabel = UseState("");
        var onFocusState = UseState("");
        var onFocusLabel = UseState("");
        var searchQuery = UseState("");
        var searchResult = UseState("");
        var tag = UseState("");
        var tags = UseState<List<string>>(new List<string>());
        var password = UseState("");
        var loginResult = UseState("");

        return Layout.Vertical()
               | Text.H3("OnChange")
               | Layout.Horizontal(
                   onChangedState.ToTextInput(),
                   onChangeLabel
                )
               | (Layout.Vertical()
                   | new Card(
                       Layout.Vertical().Gap(2)
                           | Text.P("The blur event fires when the text input loses focus.").Small()
                           | onBlurState.ToTextInput().OnBlur(e => onBlurLabel.Set("Blur Event Triggered"))
                           | (onBlurLabel.Value != ""
                               ? Callout.Success(onBlurLabel.Value)
                               : Callout.Info("Interact then click away to see blur events"))
                   ).Title("OnBlur Handler")
                   | new Card(
                       Layout.Vertical().Gap(2)
                           | Text.P("The focus event fires when you click on or tab into the text input.").Small()
                           | onFocusState.ToTextInput().OnFocus(e => onFocusLabel.Set("Focus Event Triggered"))
                           | (onFocusLabel.Value != ""
                               ? Callout.Success(onFocusLabel.Value)
                               : Callout.Info("Click or tab into the input to see focus events"))
                   ).Title("OnFocus Handler")
               )
               | Text.H3("OnSubmit (press Enter)")
               | Text.P("Search example (type and press Enter):")
               | Layout.Horizontal(
                   searchQuery.ToSearchInput()
                       .Placeholder("Search...")
                       .OnSubmit(() => searchResult.Set($"Searched for: {searchQuery.Value}")),
                   searchResult
               )
               | Text.P("Quick-add tags (type and press Enter to add):")
               | Layout.Horizontal(
                   tag.ToTextInput()
                       .Placeholder("Add a tag...")
                       .OnSubmit(() =>
                       {
                           if (!string.IsNullOrWhiteSpace(tag.Value))
                           {
                               tags.Set(new List<string>(tags.Value) { tag.Value });
                               tag.Set("");
                           }
                       }),
                   Layout.Horizontal().Gap(2) | tags.Value.Select(t => new Badge(t))
               )
               | Text.P("Password submit (type and press Enter to login):")
               | Layout.Horizontal(
                   password.ToPasswordInput()
                       .Placeholder("Enter password...")
                       .ShortcutKey("Ctrl+Enter")
                       .OnSubmit(() => loginResult.Set(
                           string.IsNullOrWhiteSpace(password.Value)
                               ? "Password cannot be empty"
                               : "Login submitted")),
                   loginResult
               );
    }
}

public class TextInputLengthConstraints : ViewBase
{
    public override object Build()
    {
        var minLengthState = UseState("");
        var maxLengthState = UseState("");
        var bothLengthState = UseState("");

        return Layout.Grid().Columns(3)
               | Text.Monospaced("MinLength(3)")
               | Text.Monospaced("MaxLength(10)")
               | Text.Monospaced("MinLength(5) + MaxLength(10)")
               | minLengthState.ToTextInput().Placeholder("At least 3 characters").MinLength(3)
               | maxLengthState.ToTextInput().Placeholder("Up to 10 characters").MaxLength(10)
               | bothLengthState.ToTextInput().Placeholder("Between 5 and 10 characters").MinLength(5).MaxLength(10);
    }
}

public class TextInputSizes : ViewBase
{
    public override object Build()
    {
        var textState = UseState("Hello");
        var passwordState = UseState("Hello");
        var textareaState = UseState("Hello");
        var searchState = UseState("Hello");

        return Layout.Grid().Columns(4)
               | Text.Monospaced("Description")
               | Text.Monospaced("Small")
               | Text.Monospaced("Medium")
               | Text.Monospaced("Large")

               | Text.Monospaced("TextInputVariant.Text")
               | textState.ToTextInput().Small()
               | textState.ToTextInput()
               | textState.ToTextInput().Large()

               | Text.Monospaced("TextInputVariant.Password")
               | passwordState.ToPasswordInput().Small()
               | passwordState.ToPasswordInput()
               | passwordState.ToPasswordInput().Large()

               | Text.Monospaced("TextInputVariant.Textarea")
               | textareaState.ToTextareaInput().Small()
               | textareaState.ToTextareaInput()
               | textareaState.ToTextareaInput().Large()

               | Text.Monospaced("TextInputVariant.Search")
               | searchState.ToSearchInput().Small()
               | searchState.ToSearchInput()
               | searchState.ToSearchInput().Large();
    }
}

/// <summary>Variants grid with <c>.Ghost()</c>, plus rows that combine suffix, <c>ShortcutKey</c> (kbd), and <c>Invalid</c>.</summary>
public class TextInputGhostDemo : ViewBase
{
    private const string InvalidSample =
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam nec purus nec eros";

    public override object Build()
    {
        var withoutValue = UseState((string?)null);
        var withValue = UseState("Hello");

        var matrix = Layout.Grid().Columns(5)
               | null!
               | Text.Monospaced("Empty")
               | Text.Monospaced("With Value")
               | Text.Monospaced("Disabled")
               | Text.Monospaced("Invalid")

               | Text.Monospaced("TextInputVariant.Text")
               | withoutValue.ToTextInput().Placeholder("Placeholder").Ghost()
               | withValue.ToTextInput().Ghost()
               | withValue.ToTextInput().Ghost().Disabled()
               | withValue.ToTextInput().Ghost().Invalid(InvalidSample)

               | Text.Monospaced("TextInputVariant.Password")
               | withoutValue.ToPasswordInput().Placeholder("Placeholder").ShortcutKey("Ctrl+L").Ghost()
               | withValue.ToPasswordInput().Ghost()
               | withValue.ToPasswordInput().Ghost().Disabled()
               | withValue.ToPasswordInput().Ghost().Invalid(InvalidSample)

               | Text.Monospaced("TextInputVariant.Textarea")
               | withoutValue.ToTextareaInput().Placeholder("Placeholder").ShortcutKey("Ctrl+T").Ghost()
               | withValue.ToTextareaInput().Ghost()
               | withValue.ToTextareaInput().Ghost().Disabled()
               | withValue.ToTextareaInput().Ghost().Invalid(InvalidSample)

               | Text.Monospaced("TextInputVariant.Search")
               | withoutValue.ToSearchInput().Placeholder("Placeholder").ShortcutKey("Ctrl+K").Ghost()
               | withValue.ToSearchInput().Ghost()
               | withValue.ToSearchInput().Ghost().Disabled()
               | withValue.ToSearchInput().Ghost().Invalid(InvalidSample)

               | Text.Monospaced("Search + Suffix (Ghost)")
               | withoutValue.ToSearchInput().Placeholder("Search").Ghost().Suffix(Icons.ChevronDown)
               | withValue.ToSearchInput().Placeholder("Search").Ghost().Suffix(Icons.ChevronDown)
               | withValue.ToSearchInput().Placeholder("Search").Ghost().Suffix(Icons.ChevronDown).Disabled()
               | withValue.ToSearchInput().Placeholder("Search").Ghost().Suffix(Icons.ChevronDown).Invalid(InvalidSample)

               | Text.Monospaced("Search + Suffix + ShortcutKey")
               | withoutValue.ToSearchInput().Placeholder("Search").Ghost().Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+H")
               | withValue.ToSearchInput().Placeholder("Search").Ghost().Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+H")
               | withValue.ToSearchInput().Placeholder("Search").Ghost().Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+H").Disabled()
               | withValue.ToSearchInput().Placeholder("Search").Ghost().Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+H").Invalid(InvalidSample)

               | Text.Monospaced("Text + Suffix + ShortcutKey")
               | withoutValue.ToTextInput().Placeholder("Query").Ghost().Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+Y")
               | withValue.ToTextInput().Ghost().Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+Y")
               | withValue.ToTextInput().Ghost().Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+Y").Disabled()
               | withValue.ToTextInput().Ghost().Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+Y").Invalid(InvalidSample)

               | Text.Monospaced("Text + Prefix + Suffix + ShortcutKey")
               | withoutValue.ToTextInput().Placeholder("Token").Ghost().Prefix(Icons.Mail).Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+Shift+Y")
               | withValue.ToTextInput().Ghost().Prefix(Icons.Mail).Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+Shift+Y")
               | withValue.ToTextInput().Ghost().Prefix(Icons.Mail).Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+Shift+Y").Disabled()
               | withValue.ToTextInput().Ghost().Prefix(Icons.Mail).Suffix(Icons.ChevronDown).ShortcutKey("Ctrl+Shift+Y").Invalid(InvalidSample)

               | Text.Monospaced("TextInputVariant.Email")
               | withoutValue.ToEmailInput().Placeholder("Placeholder").ShortcutKey("Ctrl+E").Ghost()
               | withValue.ToEmailInput().Ghost()
               | withValue.ToEmailInput().Ghost().Disabled()
               | withValue.ToEmailInput().Ghost().Invalid(InvalidSample)

               | Text.Monospaced("TextInputVariant.Tel")
               | withoutValue.ToTelInput().Placeholder("Placeholder").ShortcutKey("Ctrl+J").Ghost()
               | withValue.ToTelInput().Ghost()
               | withValue.ToTelInput().Ghost().Disabled()
               | withValue.ToTelInput().Ghost().Invalid(InvalidSample)

               | Text.Monospaced("TextInputVariant.Url")
               | withoutValue.ToUrlInput().Placeholder("Placeholder").ShortcutKey("Ctrl+U").Ghost()
               | withValue.ToUrlInput().Ghost()
               | withValue.ToUrlInput().Ghost().Disabled()
               | withValue.ToUrlInput().Ghost().Invalid(InvalidSample);

        return Layout.Vertical().Gap(6)
               | Text.P(
                   "Ghost removes the default field border. Search with a suffix uses a tight trailing strip for clear (×), shortcut kbd, and invalid icon. Text inputs keep the overlay on the field; rows below mix suffix, ShortcutKey, and Invalid.")
               | matrix;
    }
}

public class TextInputAffixes : ViewBase
{
    public override object Build()
    {
        var textState = UseState("example");
        var nullableState = UseState<string?>((string?)null);

        return Layout.Grid().Columns(4)
               | null!
               | Text.Monospaced("Prefix only")
               | Text.Monospaced("Suffix only")
               | Text.Monospaced("Both")

               | Text.Monospaced("Text prefix/suffix")
               | textState.ToTextInput().Prefix("https://")
               | textState.ToTextInput().Suffix(".com")
               | textState.ToTextInput().Prefix("https://").Suffix(".com")

               | Text.Monospaced("Icon prefix/suffix")
               | textState.ToTextInput().Prefix(Icons.Mail)
               | textState.ToTextInput().Suffix(Icons.Mail)
               | textState.ToTextInput().Prefix(Icons.Mail).Suffix(Icons.Mail)

               | Text.Monospaced("Button prefix/suffix")
               | textState.ToTextInput().Prefix(new Button("Copy", () => { }, icon: Icons.Copy).Ghost().Small())
               | textState.ToTextInput().Suffix(new Button("Clear", () => { textState.Value = ""; }).Ghost().Small())
               | textState.ToTextInput().Prefix(new Button("Copy", () => { }, icon: Icons.Copy).Ghost().Small()).Suffix(new Button("Send").Ghost().Small())

               | Text.Monospaced("Badge prefix/suffix")
               | textState.ToTextInput().Prefix(new Badge("NEW", BadgeVariant.Success))
               | textState.ToTextInput().Suffix(new Badge($"{textState.Value.Length} chars", BadgeVariant.Secondary))
               | textState.ToTextInput().Prefix(new Badge("v2", BadgeVariant.Info)).Suffix(new Badge("OK", BadgeVariant.Success))

               | Text.Monospaced("Nullable with prefix/suffix")
               | nullableState.ToTextInput().Prefix("$").Placeholder("Amount")
               | nullableState.ToTextInput().Suffix("%").Placeholder("Percentage")
               | nullableState.ToTextInput().Prefix("https://").Suffix(".com").Placeholder("domain")

               | Text.Monospaced("Nullable + Invalid + ShortcutKey")
               | nullableState.ToTextInput().Prefix("@").Invalid("Required field").ShortcutKey("Ctrl+P")
               | nullableState.ToTextInput().Suffix(Icons.Search).Invalid("Invalid input").ShortcutKey("Ctrl+F")
               | nullableState.ToTextInput().Prefix(Icons.Mail).Suffix(".com").Invalid("Error").ShortcutKey("Ctrl+B");
    }
}
