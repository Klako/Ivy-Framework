using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/05_EventHandlers.md", searchHints: ["onblur", "events", "focus", "blur", "event-handlers", "input-events", "form-events"])]
public class EventHandlersApp(bool onlyBody = false) : ViewBase
{
    public EventHandlersApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("event-handlers", "Event Handlers", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("onblur-event-handler", "OnBlur Event Handler", 2), new ArticleHeading("when-onblur-fires", "When OnBlur Fires", 3), new ArticleHeading("available-on-input-widgets", "Available on Input Widgets", 3), new ArticleHeading("common-patterns", "Common Patterns", 2), new ArticleHeading("validation-pattern", "Validation Pattern", 3), new ArticleHeading("auto-save--formatting-pattern", "Auto-Save & Formatting Pattern", 3), new ArticleHeading("async-operations-pattern", "Async Operations Pattern", 3), new ArticleHeading("onblur-method-signatures", "OnBlur Method Signatures", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Event Handlers").OnLinkClick(onLinkClick)
            | Lead("Handle user interactions and input events in Ivy with event handlers like OnBlur, enabling [validation](app://onboarding/concepts/forms), [data persistence](app://onboarding/concepts/clients), and reactive user experiences.")
            | new Markdown(
                """"
                Event handlers allow you to respond to user interactions with [widgets](app://onboarding/concepts/widgets) in your Ivy [applications](app://onboarding/concepts/apps). They enable you to execute custom logic when users interact with UI elements, such as clicking buttons, changing input values, or moving focus between fields.
                
                ## Basic Usage
                
                The simplest form of `OnBlur` handler performs an action when the input loses focus:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicBlurExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicBlurExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var name = UseState("");
                            var message = UseState("");
                    
                            return Layout.Vertical()
                                | name.ToTextInput("Your Name")
                                    .Placeholder("Enter your name...")
                                    .OnBlur(_ => message.Set($"Hello, {name.Value}!"))
                                | Text.P(message.Value);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## OnBlur Event Handler
                
                The `OnBlur` event handler is triggered when an input widget loses focus. This is particularly useful for validation, auto-saving data, analytics tracking, or performing cleanup operations when a user finishes interacting with a field.
                
                ### When OnBlur Fires
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph LR
                    A[User types in field] --> B[User clicks away]
                    B --> C[OnBlur fires]
                ```
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Available on Input Widgets
                
                The `OnBlur` event handler is available on all input widgets that implement the `IAnyInput` interface:
                
                | Input Widget | Description |
                |--------------|-------------|
                | [TextInput](app://widgets/inputs/text-input) | Text, password, email, search, and textarea inputs |
                | [NumberInput](app://widgets/inputs/number-input) | Number and slider inputs |
                | [SelectInput](app://widgets/inputs/select-input) | Dropdown select inputs |
                | [AsyncSelectInput](app://widgets/inputs/async-select-input) | Async dropdown inputs with server-side data |
                | [BoolInput](app://widgets/inputs/bool-input) | Checkbox and switch inputs |
                | [DateTimeInput](app://widgets/inputs/date-time-input) | Date and time picker inputs |
                | [DateRangeInput](app://widgets/inputs/date-range-input) | Date range picker inputs |
                | [FileInput](app://widgets/inputs/file-input) | File upload inputs |
                | [ColorInput](app://widgets/inputs/color-input) | Color picker inputs |
                | [CodeInput](app://widgets/inputs/code-input) | Code editor inputs |
                | [FeedbackInput](app://widgets/inputs/feedback-input) | Star rating and feedback inputs |
                | [ReadOnlyInput](app://widgets/inputs/read-only-input) | Read-only display inputs |
                
                ## Common Patterns
                
                ### Validation Pattern
                
                Validate [fields](app://widgets/inputs/field) when the user finishes editing using validation patterns:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ValidationBlurExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ValidationBlurExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var email = UseState("");
                            var error = UseState(() => (string?)null);
                    
                            return email.ToTextInput()
                                .Placeholder("your.email@example.com")
                                .OnBlur(() =>
                                {
                                    error.Set(string.IsNullOrWhiteSpace(email.Value) ? "Required"
                                        : !email.Value.Contains("@") ? "Invalid email"
                                        : null);
                                })
                                .Invalid(error.Value);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Auto-Save & Formatting Pattern
                
                Perform actions like saving or formatting when focus is lost:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AutoSaveFormatExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AutoSaveFormatExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var phoneNumber = UseState("");
                            var title = UseState("");
                            var lastSaved = UseState(() => (DateTime?)null);
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Vertical()
                                // Auto-save pattern
                                | title.ToTextInput()
                                    .Placeholder("Document title")
                                    .OnBlur(async () =>
                                    {
                                        await Task.Delay(500); // Save to database
                                        lastSaved.Set(DateTime.Now);
                                        client.Toast("Saved!");
                                    })
                                | Text.Muted(lastSaved.Value != null ? $"Saved at {lastSaved.Value:HH:mm:ss}" : "")
                    
                                // Format pattern
                                | phoneNumber.ToTextInput()
                                    .Placeholder("Enter 10-digit phone")
                                    .OnBlur(() =>
                                    {
                                        var digits = new string(phoneNumber.Value.Where(char.IsDigit).ToArray());
                                        if (digits.Length == 10)
                                            phoneNumber.Set($"({digits.Substring(0, 3)}) {digits.Substring(3, 3)}-{digits.Substring(6, 4)}");
                                    });
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Async Operations Pattern
                
                Handle async operations like API validation:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AsyncBlurExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AsyncBlurExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var username = UseState("");
                            var message = UseState("");
                    
                            return Layout.Vertical()
                                | username.ToTextInput()
                                    .Placeholder("Choose username")
                                    .OnBlur(async () =>
                                    {
                                        if (string.IsNullOrWhiteSpace(username.Value)) return;
                    
                                        message.Set("Checking...");
                                        await Task.Delay(1000); // API call
                    
                                        var isAvailable = !username.Value.Equals("admin", StringComparison.OrdinalIgnoreCase);
                                        message.Set(isAvailable ? "Available" : "Taken");
                                    })
                                | Text.P(message.Value);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("## OnBlur Method Signatures").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Simple action (most common)
                input.OnBlur(() => Validate());
                
                // With event parameter
                input.OnBlur((Event<IAnyInput> e) => Log(e.Id));
                
                // Async operation
                input.OnBlur(async () => await SaveToApi());
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [Forms](app://onboarding/concepts/forms) - Building forms with validation
                - [State](app://hooks/core/use-state) - Managing component state
                - [Effects](app://hooks/core/use-effect) - Performing side effects
                - [Widgets](app://onboarding/concepts/widgets) - Understanding Ivy widgets
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.ClientsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp), typeof(Widgets.Inputs.TextInputApp), typeof(Widgets.Inputs.NumberInputApp), typeof(Widgets.Inputs.SelectInputApp), typeof(Widgets.Inputs.AsyncSelectInputApp), typeof(Widgets.Inputs.BoolInputApp), typeof(Widgets.Inputs.DateTimeInputApp), typeof(Widgets.Inputs.DateRangeInputApp), typeof(Widgets.Inputs.FileInputApp), typeof(Widgets.Inputs.ColorInputApp), typeof(Widgets.Inputs.CodeInputApp), typeof(Widgets.Inputs.FeedbackInputApp), typeof(Widgets.Inputs.ReadOnlyInputApp), typeof(Widgets.Inputs.FieldApp), typeof(Hooks.Core.UseStateApp), typeof(Hooks.Core.UseEffectApp)]; 
        return article;
    }
}


public class BasicBlurExample : ViewBase
{
    public override object? Build()
    {
        var name = UseState("");
        var message = UseState("");
        
        return Layout.Vertical()
            | name.ToTextInput("Your Name")
                .Placeholder("Enter your name...")
                .OnBlur(_ => message.Set($"Hello, {name.Value}!"))
            | Text.P(message.Value);
    }
}

public class ValidationBlurExample : ViewBase
{
    public override object? Build()
    {
        var email = UseState("");
        var error = UseState(() => (string?)null);
        
        return email.ToTextInput()
            .Placeholder("your.email@example.com")
            .OnBlur(() => 
            {
                error.Set(string.IsNullOrWhiteSpace(email.Value) ? "Required" 
                    : !email.Value.Contains("@") ? "Invalid email" 
                    : null);
            })
            .Invalid(error.Value);
    }
}

public class AutoSaveFormatExample : ViewBase
{
    public override object? Build()
    {
        var phoneNumber = UseState("");
        var title = UseState("");
        var lastSaved = UseState(() => (DateTime?)null);
        var client = UseService<IClientProvider>();
        
        return Layout.Vertical()
            // Auto-save pattern
            | title.ToTextInput()
                .Placeholder("Document title")
                .OnBlur(async () => 
                {
                    await Task.Delay(500); // Save to database
                    lastSaved.Set(DateTime.Now);
                    client.Toast("Saved!");
                })
            | Text.Muted(lastSaved.Value != null ? $"Saved at {lastSaved.Value:HH:mm:ss}" : "")
            
            // Format pattern
            | phoneNumber.ToTextInput()
                .Placeholder("Enter 10-digit phone")
                .OnBlur(() => 
                {
                    var digits = new string(phoneNumber.Value.Where(char.IsDigit).ToArray());
                    if (digits.Length == 10)
                        phoneNumber.Set($"({digits.Substring(0, 3)}) {digits.Substring(3, 3)}-{digits.Substring(6, 4)}");
                });
    }
}

public class AsyncBlurExample : ViewBase
{
    public override object? Build()
    {
        var username = UseState("");
        var message = UseState("");
        
        return Layout.Vertical()
            | username.ToTextInput()
                .Placeholder("Choose username")
                .OnBlur(async () =>
                {
                    if (string.IsNullOrWhiteSpace(username.Value)) return;
                    
                    message.Set("Checking...");
                    await Task.Delay(1000); // API call
                    
                    var isAvailable = !username.Value.Equals("admin", StringComparison.OrdinalIgnoreCase);
                    message.Set(isAvailable ? "Available" : "Taken");
                })
            | Text.P(message.Value);
    }
}
