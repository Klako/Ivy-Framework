using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Inputs;

[App(order:2, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/04_Inputs/02_TextInput.md", searchHints: ["input", "textbox", "password", "textarea", "email", "search"])]
public class TextInputApp(bool onlyBody = false) : ViewBase
{
    public TextInputApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("textinput", "TextInput", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("password", "Password", 3), new ArticleHeading("textarea", "Textarea", 3), new ArticleHeading("search", "Search", 3), new ArticleHeading("email", "Email", 3), new ArticleHeading("telephone", "Telephone", 3), new ArticleHeading("url", "URL", 3), new ArticleHeading("minlength-validation", "MinLength Validation", 2), new ArticleHeading("event-handling", "Event Handling", 2), new ArticleHeading("styling", "Styling", 2), new ArticleHeading("prefix-and-suffix", "Prefix and Suffix", 2), new ArticleHeading("shortcuts", "Shortcuts", 2), new ArticleHeading("helper-functions", "Helper functions", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# TextInput").OnLinkClick(onLinkClick)
            | Lead("Capture user text input with validation, formatting, and interactive features like autocomplete and real-time feedback.")
            | new Markdown(
                """"
                The `TextInput` [widget](app://onboarding/concepts/widgets) provides a standard text entry field. It supports various text [input](app://onboarding/concepts/widgets) types including single-line text, multi-line text, password fields, email, phone number, URL and offers features like placeholder text, validation, shortcut keys and text formatting.
                
                ## Basic Usage
                
                Here's a simple example of a text input with a placeholder:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicUsageDemo())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicUsageDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var text = UseState("");
                            return new TextInput(text).Placeholder("Enter text here...");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("The TextInput class now defaults to `string` type, so you can use `new TextInput(...)` instead of `new TextInput<string>(...)`. The generic version is still available if you need to work with other string-compatible types.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ## Variants
                
                `TextInput`s come in several variants to suit different use cases:
                The following blocks shows how to use these.
                
                ### Password
                
                For capturing passwords, `TextInputVariant.Password` variant needs to be used. The following code shows how to capture
                a new password.
                
                See it in action here.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class PasswordCaptureDemo: ViewBase
                    {
                        public override object? Build()
                        {
                            var password = UseState("");
                            return new TextInput(password)
                                         .Placeholder("Password")
                                         .Variant(TextInputVariant.Password)
                                         .WithField()
                                         .Label("Enter Password");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new PasswordCaptureDemo())
            )
            | new Markdown(
                """"
                ### Textarea
                
                When a multiline text is needed, `TextInputVariant.Textarea` variant should be used. A common use-case is for capturing address
                that typically spans over multiple lines. The following demo shows how to use it.
                
                See it in action here.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class CaptureAddressDemo: ViewBase
                    {
                        public override object? Build()
                        {
                            var address = UseState("");
                            return new TextInput(address)
                                                   .Placeholder("Åkervägen 9, \n132 39 Saltsjö-Boo, \nSweden")
                                                   .Variant(TextInputVariant.Textarea)
                                                   .Height(Size.Units(30))
                                                   .Width(Size.Units(100))
                                                   .WithField()
                                                   .Label("Address");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new CaptureAddressDemo())
            )
            | new Markdown("Please note that how the newlines (`\\n`) are recognized and used to create newlines in the textarea.").OnLinkClick(onLinkClick)
            | new Callout("A `.Multiline()` extension method on `TextInputBase` lets you turn any TextInput into a textarea without changing the variant explicitly. `notes.ToTextInput().Multiline()` is equivalent to `notes.ToTextareaInput()`.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Search
                
                When it is necessary to find an element from a collection of items, it is better to give users a visual clue.
                Using the `TextInputVariant.Search` variant, this visual clue (with a looking glass icon) becomes obvious.
                The following demo shows how to add such an text input.
                
                See it in action here.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class SearchBarDemo: ViewBase
                    {
                        public override object? Build()
                        {
                            var searchThis = UseState("");
                            return new TextInput(searchThis)
                                                   .Placeholder("search for?")
                                                   .Variant(TextInputVariant.Search)
                                                   .WithField()
                                                   .Label("Search");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new SearchBarDemo())
            )
            | new Markdown(
                """"
                ### Email
                
                To capture the emails `TextInputVariant.Email` variant should be used.
                
                See it in action here.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class EmailEnterDemo: ViewBase
                    {
                        public override object? Build()
                        {
                            var email = UseState("");
                            return new TextInput(email)
                                           .Placeholder("user@domain.com")
                                           .Variant(TextInputVariant.Email)
                                           .WithField()
                                           .Label("Email");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new EmailEnterDemo())
            )
            | new Markdown(
                """"
                ### Telephone
                
                To capture the phone numbers `TextInputVariant.Tel` variant needs to be used.
                
                see it in action here.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class PhoneEnterDemo: ViewBase
                    {
                        public override object? Build()
                        {
                            var tel = UseState("");
                            return new TextInput(tel)
                                          .Placeholder("+1-123-3456")
                                          .Variant(TextInputVariant.Tel)
                                          .WithField()
                                          .Label("Phone");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new PhoneEnterDemo())
            )
            | new Markdown(
                """"
                ### URL
                
                To capture the URLs/Links  `TextInputVariant.Url` variant needs to be used.
                
                see it in action here.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class URLEnterDemo: ViewBase
                    {
                        public override object? Build()
                        {
                            var url = UseState("");
                            return new TextInput(url)
                                          .Placeholder("https://ivy.app/")
                                          .Variant(TextInputVariant.Url)
                                          .WithField()
                                          .Label("Website");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new URLEnterDemo())
            )
            | new Markdown(
                """"
                ## MinLength Validation
                
                The TextInput widget and all its variants (Password, Search, Textarea) support minimum length validation with the `.MinLength()` method. Combine it with `.MaxLength()` for range constraints:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class MinLengthValidationDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var usernameState = UseState("");
                            return usernameState.ToTextInput()
                                .Placeholder("Between 5 and 10 characters")
                                .MinLength(5)
                                .MaxLength(10)
                                .WithField()
                                .Label("Username");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new MinLengthValidationDemo())
            )
            | new Markdown(
                """"
                ## Event Handling
                
                Use the [`OnChange`](app://onboarding/concepts/event-handlers) callback to react to text input changes. The callback receives an event with the current value.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new EventsDemoApp())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class EventsDemoApp : ViewBase
                    {
                        public override object? Build()
                        {
                            var name = UseState("");
                            return Layout.Vertical()
                                | new TextInput(name.Value, e => name.Set(e.Value))
                                      .Placeholder("Enter your name...").WithField().Label("Name")
                                | (name.Value.Length > 0 ? $"Hello, {name.Value}!" : "");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Styling
                
                `TextInput` can be styled to provide visual feedback to users about the input state. Use `.Invalid()` for [validation](app://onboarding/concepts/forms) error messages.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class TextInputStylingDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var text = UseState("");
                            return Layout.Vertical()
                                | new TextInput(text).Placeholder("Invalid input").Invalid("This field has an error")
                                | new TextInput(text).Placeholder("Disabled input").Disabled();
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new TextInputStylingDemo())
            )
            | new Markdown(
                """"
                Hover over the `i` icon on the invalid input to see the error message.
                
                ## Prefix and Suffix
                
                In certain scenarios, it is beneficial to prepend or append static content—such as text fragments or icons—to an input field. This practice is particularly useful for displaying the protocol in a URL field, a currency symbol, or an icon that denotes the expected input.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class UrlInputDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var domain = UseState("example");
                            return domain.ToTextInput()
                                         .Prefix(Icons.Globe)
                                         .Suffix(".com");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new UrlInputDemo())
            )
            | new Markdown(
                """"
                The `Prefix` and `Suffix` methods accept either a `string` or an `IWidget`, thereby providing complete flexibility for augmenting the contextual information of the input.
                
                ## Shortcuts
                
                We can use associate keyboard shortcuts to text inputs the following way.
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                 new TextInput(name)
                    .Placeholder("Name (Ctrl+S)")
                    .ShortcutKey("Ctrl+S")
                """",Languages.Csharp)
            | new Markdown(
                """"
                The following demo shows this in action with multiple `TextInput`s each
                with different shortcut keys.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ShortCutDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var name = UseState("");
                            var email = UseState("");
                            var message = UseState("");
                            return Layout.Vertical()
                                    | Text.Inline("Keyboard Shortcuts Demo")
                                    | Text.Inline("Ctrl+J - Focus Name, Ctrl+E - Focus Email, Ctrl+M - Focus Message")
                                    | new TextInput(name)
                                          .Placeholder("Name (Ctrl+J)")
                                          .ShortcutKey("Ctrl+J")
                                    | new TextInput(email)
                                          .Placeholder("Email (Ctrl+E)")
                                          .ShortcutKey("Ctrl+E")
                                          .Variant(TextInputVariant.Email)
                                    | new TextInput(message)
                                          .Placeholder("Message (Ctrl+M)")
                                          .ShortcutKey("Ctrl+M")
                                          .Variant(TextInputVariant.Textarea);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ShortCutDemo())
            )
            | new Markdown(
                """"
                ## Helper functions
                
                There are several helper functions to create `TextInput` variants from state instances. Instead of employing the constructor to create a
                `TextInput`, these functions should be used. The following is an example of how Ivy can be employed to generate [UI](app://onboarding/concepts/views) idiomatically using
                these functions.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class DataCaptureUsingExtensionDemo: ViewBase
                    {
                        public override object? Build()
                        {
                            var userName = UseState("");
                            var password = UseState("");
                            var email = UseState("");
                            var tel = UseState("");
                            var address = UseState("");
                            var website = UseState("");
                            return Layout.Vertical()
                                    | userName.ToTextInput()
                                              .Placeholder("User name")
                                              .WithField()
                                              .Label("Username")
                                    | password.ToPasswordInput(placeholder: "Password")
                                              .Disabled(userName.Value.Length == 0)
                                              .WithField()
                                              .Label("Password")
                                    | email.ToEmailInput()
                                           .Placeholder("Email")
                                           .WithField()
                                           .Label("Email")
                                    | tel.ToTelInput()
                                         .Placeholder("Mobile")
                                         .WithField()
                                         .Label("Mobile")
                                    | address.ToTextareaInput()
                                             .Placeholder("Address Line1\nAddress Line2\nAddress Line 3")
                                             .Height(Size.Units(40))
                                             .Width(Size.Units(100))
                                             .WithField()
                                             .Label("Address")
                                    | website.ToUrlInput()
                                             .Placeholder("https://ivy.app/")
                                             .WithField()
                                             .Label("Website");
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new DataCaptureUsingExtensionDemo())
            )
            | new Markdown(
                """"
                There is also another extension function to create a `TextInput.Search` variant.
                
                Here is how it can be used.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    
                    public class BasicFilter : ViewBase
                    {
                        public override object Build()
                        {
                            var searchState = UseState("");
                            var result = UseState("");
                            var fruits = new[] {
                                "Apple", "Banana", "Cherry", "Date", "Elderberry",
                                "Stawberry", "Blueberry", "Watermelon", "Muskmelon",
                                "Fig", "Grape", "Kiwi", "Lemon", "Mango"
                            };
                    
                            var filtered = fruits
                                .Where(fruit => fruit.ToLower().Contains(searchState.Value.ToLower()))
                                .ToArray();
                    
                            var content = string.Join("\n", filtered);
                    
                            return Layout.Vertical()
                                | searchState.ToSearchInput().Placeholder("Which fruit you like?")
                                | result.ToTextareaInput(content);
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicFilter())
            )
            | new WidgetDocsView("Ivy.TextInput", "Ivy.TextInputExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Inputs/TextInput.cs")
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("Validate Email Demo",
                Vertical().Gap(4)
                | new Markdown("In this example, if the email format is wrong, the input is invalidated and a message is shown.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new EmailValidationDemo())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class EmailValidationDemo : ViewBase
                        {
                            // Email regex pattern
                            private static readonly System.Text.RegularExpressions.Regex EmailRegex = new
                                System.Text.RegularExpressions.Regex(
                                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                                System.Text.RegularExpressions.RegexOptions.Compiled |
                                System.Text.RegularExpressions.RegexOptions.IgnoreCase
                            );
                        
                            public override object? Build()
                            {
                                var onChangedState = UseState("");
                                var invalidState = UseState("");
                        
                                return new TextInput(onChangedState.Value, e =>
                                      {
                                        onChangedState.Set(e.Value);
                                        if (string.IsNullOrWhiteSpace(e.Value))
                                        {
                                            invalidState.Set("");
                                        }
                                        else if (!EmailRegex.IsMatch(e.Value))
                                        {
                                            invalidState.Set("Invalid email address");
                                        }
                                        else
                                        {
                                            invalidState.Set("");
                                        }
                                      })
                                      .Invalid(invalidState.Value)
                                      .WithField()
                                      .Label("Email");
                        
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Conditional Enabling/Disabling of Text Inputs",
                Vertical().Gap(4)
                | new Markdown("In this demo, password field is enabled only when the username field has a value.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new LoginForm())),
                    new Tab("Code", new CodeBlock(
                        """"
                        
                        public class LoginForm : ViewBase
                        {
                            public override object Build()
                            {
                                var usernameState = UseState("");
                                var passwordState = UseState("");
                        
                                return Layout.Vertical()
                                    | Text.Label("Login")
                                    | Layout.Vertical()
                                        | usernameState.ToTextInput()
                                            .Placeholder("Enter your username")
                                            .WithField()
                                            .Label("Username")
                                        | passwordState.ToPasswordInput()
                                            .Placeholder("Enter your password")
                                             // Disabled when username is empty
                                            .Disabled(string.IsNullOrWhiteSpace(usernameState.Value))
                                            .WithField()
                                            .Label("Password")
                                        | new Button("Login")
                                            .Disabled(string.IsNullOrWhiteSpace(usernameState.Value) ||
                                                string.IsNullOrWhiteSpace(passwordState.Value));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("How do I create a multiline textarea TextInput in Ivy?",
                Vertical().Gap(4)
                | new Markdown("Use the `TextInputVariant.Textarea` variant or the dedicated `ToTextareaInput` extension:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    state.ToTextareaInput(placeholder: "Enter text...")
                    """",Languages.Csharp)
            )
            | new Expandable("How do I handle enter key press on a TextInput?",
                Vertical().Gap(4)
                | new Markdown("Single-line TextInputs automatically blur when the user presses Enter, so use `OnBlur` to react to the Enter key:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var input = UseState("");
                    input.ToTextInput()
                        .Placeholder("Type and press Enter")
                        .OnBlur(() => DoSomething(input.Value))
                    """",Languages.Csharp)
                | new Markdown("`OnBlur` takes an `Action` that is invoked when the input loses focus — which happens automatically on Enter for single-line text inputs.").OnLinkClick(onLinkClick)
            )
            | new Expandable("How to create a form with a dynamic number of fields (e.g. dictionary input)?",
                Vertical().Gap(4)
                | new Markdown("Since hooks cannot be called inside loops (IVYHOOK003), you cannot use `UseState` in a `for`/`foreach`/LINQ loop. Instead, use **one state variable** that holds all field values:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    public override object Build()
                    {
                        var columns = GetColumnNames(); // e.g. ["Name", "Age", "City"]
                        var values = UseState(new Dictionary<string, string>());
                    
                        var layout = Layout.Vertical();
                        foreach (var col in columns)
                        {
                            var currentValue = values.Value.GetValueOrDefault(col, "");
                            layout.Add(
                                new TextInput(currentValue, e =>
                                {
                                    var updated = new Dictionary<string, string>(values.Value) { [col] = e.Value };
                                    values.Set(updated);
                                })
                                .Placeholder(col)
                                .WithField()
                                .Label(col)
                            );
                        }
                        return layout;
                    }
                    """",Languages.Csharp)
                | new Markdown(
                    """"
                    Key points:
                    
                    - Only one `UseState` call at the top level — no hook rule violations
                    - The dictionary keys map to column names, values map to user input
                    - Create a new dictionary on each update to trigger a re-render
                    - This pattern works for any dynamic input scenario (forms, dialogs, etc.)
                    """").OnLinkClick(onLinkClick)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.EventHandlersApp), typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.ViewsApp)]; 
        return article;
    }
}


public class BasicUsageDemo : ViewBase
{
    public override object? Build()
    { 
        var text = UseState("");
        return new TextInput(text).Placeholder("Enter text here...");
    }
}

public class PasswordCaptureDemo: ViewBase
{
    public override object? Build()
    {
        var password = UseState("");
        return new TextInput(password)
                     .Placeholder("Password")
                     .Variant(TextInputVariant.Password)
                     .WithField()
                     .Label("Enter Password");         
    }
}

public class CaptureAddressDemo: ViewBase
{
    public override object? Build()
    {
        var address = UseState("");
        return new TextInput(address)
                               .Placeholder("Åkervägen 9, \n132 39 Saltsjö-Boo, \nSweden")
                               .Variant(TextInputVariant.Textarea)
                               .Height(Size.Units(30))
                               .Width(Size.Units(100))
                               .WithField()
                               .Label("Address");         
    }
}

public class SearchBarDemo: ViewBase
{
    public override object? Build()
    {
        var searchThis = UseState("");
        return new TextInput(searchThis)
                               .Placeholder("search for?")
                               .Variant(TextInputVariant.Search)
                               .WithField()
                               .Label("Search");
    }
}

public class EmailEnterDemo: ViewBase
{
    public override object? Build()
    {
        var email = UseState("");
        return new TextInput(email)
                       .Placeholder("user@domain.com")
                       .Variant(TextInputVariant.Email)
                       .WithField()
                       .Label("Email");
    }
}

public class PhoneEnterDemo: ViewBase
{
    public override object? Build()
    {
        var tel = UseState("");
        return new TextInput(tel)
                      .Placeholder("+1-123-3456")
                      .Variant(TextInputVariant.Tel)
                      .WithField()
                      .Label("Phone");
    }
}

public class URLEnterDemo: ViewBase
{
    public override object? Build()
    {
        var url = UseState("");
        return new TextInput(url)
                      .Placeholder("https://ivy.app/")
                      .Variant(TextInputVariant.Url)
                      .WithField()
                      .Label("Website");
    }
}

public class MinLengthValidationDemo : ViewBase
{
    public override object? Build()
    {
        var usernameState = UseState("");
        return usernameState.ToTextInput()
            .Placeholder("Between 5 and 10 characters")
            .MinLength(5)
            .MaxLength(10)
            .WithField()
            .Label("Username");
    }
}

public class EventsDemoApp : ViewBase
{
    public override object? Build()
    {
        var name = UseState("");
        return Layout.Vertical()
            | new TextInput(name.Value, e => name.Set(e.Value))
                  .Placeholder("Enter your name...").WithField().Label("Name")
            | (name.Value.Length > 0 ? $"Hello, {name.Value}!" : "");
    }
}

public class TextInputStylingDemo : ViewBase
{
    public override object? Build()
    {
        var text = UseState("");
        return Layout.Vertical()
            | new TextInput(text).Placeholder("Invalid input").Invalid("This field has an error")
            | new TextInput(text).Placeholder("Disabled input").Disabled();
    }
}

public class UrlInputDemo : ViewBase
{
    public override object? Build()
    {
        var domain = UseState("example");
        return domain.ToTextInput()
                     .Prefix(Icons.Globe)
                     .Suffix(".com");
    }
}

public class ShortCutDemo : ViewBase
{
    public override object? Build()
    { 
        var name = UseState("");
        var email = UseState("");
        var message = UseState("");
        return Layout.Vertical()
                | Text.Inline("Keyboard Shortcuts Demo")
                | Text.Inline("Ctrl+J - Focus Name, Ctrl+E - Focus Email, Ctrl+M - Focus Message")  
                | new TextInput(name)
                      .Placeholder("Name (Ctrl+J)")
                      .ShortcutKey("Ctrl+J")    
                | new TextInput(email)
                      .Placeholder("Email (Ctrl+E)")
                      .ShortcutKey("Ctrl+E")
                      .Variant(TextInputVariant.Email)    
                | new TextInput(message)
                      .Placeholder("Message (Ctrl+M)")
                      .ShortcutKey("Ctrl+M")
                      .Variant(TextInputVariant.Textarea);
    }
}

public class DataCaptureUsingExtensionDemo: ViewBase
{
    public override object? Build()
    {
        var userName = UseState("");
        var password = UseState("");
        var email = UseState("");
        var tel = UseState("");
        var address = UseState("");
        var website = UseState("");
        return Layout.Vertical()
                | userName.ToTextInput()
                          .Placeholder("User name")
                          .WithField()
                          .Label("Username")
                | password.ToPasswordInput(placeholder: "Password")
                          .Disabled(userName.Value.Length == 0)
                          .WithField()
                          .Label("Password")
                | email.ToEmailInput()
                       .Placeholder("Email")
                       .WithField()
                       .Label("Email")
                | tel.ToTelInput()
                     .Placeholder("Mobile")
                     .WithField()
                     .Label("Mobile")
                | address.ToTextareaInput()
                         .Placeholder("Address Line1\nAddress Line2\nAddress Line 3")
                         .Height(Size.Units(40))
                         .Width(Size.Units(100))
                         .WithField()
                         .Label("Address")
                | website.ToUrlInput()
                         .Placeholder("https://ivy.app/")
                         .WithField()
                         .Label("Website");                             
    }
}


public class BasicFilter : ViewBase 
{      
    public override object Build()
    {         
        var searchState = UseState("");
        var result = UseState("");
        var fruits = new[] { 
            "Apple", "Banana", "Cherry", "Date", "Elderberry", 
            "Stawberry", "Blueberry", "Watermelon", "Muskmelon",
            "Fig", "Grape", "Kiwi", "Lemon", "Mango" 
        };

        var filtered = fruits
            .Where(fruit => fruit.ToLower().Contains(searchState.Value.ToLower()))
            .ToArray();
            
        var content = string.Join("\n", filtered);
        
        return Layout.Vertical()
            | searchState.ToSearchInput().Placeholder("Which fruit you like?")
            | result.ToTextareaInput(content);
    }     
}

public class EmailValidationDemo : ViewBase 
{      
    // Email regex pattern
    private static readonly System.Text.RegularExpressions.Regex EmailRegex = new 
        System.Text.RegularExpressions.Regex(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        System.Text.RegularExpressions.RegexOptions.Compiled | 
        System.Text.RegularExpressions.RegexOptions.IgnoreCase
    );

    public override object? Build()
    {         
        var onChangedState = UseState("");         
        var invalidState = UseState("");         
        
        return new TextInput(onChangedState.Value, e =>                    
              {                        
                onChangedState.Set(e.Value);
                if (string.IsNullOrWhiteSpace(e.Value))
                {
                    invalidState.Set("");
                }
                else if (!EmailRegex.IsMatch(e.Value))                        
                {                             
                    invalidState.Set("Invalid email address");
                }                        
                else                        
                {                         
                    invalidState.Set(""); 
                }                    
              })
              .Invalid(invalidState.Value)
              .WithField()
              .Label("Email");
         
    }     
}


public class LoginForm : ViewBase 
{      
    public override object Build()
    {         
        var usernameState = UseState("");         
        var passwordState = UseState("");         
        
        return Layout.Vertical()
            | Text.Label("Login")
            | Layout.Vertical()
                | usernameState.ToTextInput()
                    .Placeholder("Enter your username")
                    .WithField()
                    .Label("Username")
                | passwordState.ToPasswordInput()
                    .Placeholder("Enter your password")
                     // Disabled when username is empty
                    .Disabled(string.IsNullOrWhiteSpace(usernameState.Value))
                    .WithField()
                    .Label("Password")
                | new Button("Login")
                    .Disabled(string.IsNullOrWhiteSpace(usernameState.Value) || 
                        string.IsNullOrWhiteSpace(passwordState.Value));
    }
}
