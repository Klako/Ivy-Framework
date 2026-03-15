using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:13, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/13_Error.md", searchHints: ["exception", "error", "failure", "crash", "debugging", "message"])]
public class ErrorApp(bool onlyBody = false) : ViewBase
{
    public ErrorApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("error", "Error", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("title", "Title", 3), new ArticleHeading("message", "Message", 3), new ArticleHeading("stack-trace", "Stack Trace", 3), new ArticleHeading("alternative-error-display-methods", "Alternative Error Display Methods", 2), new ArticleHeading("error-callouts", "Error Callouts", 3), new ArticleHeading("error-toasts", "Error Toasts", 3), new ArticleHeading("text-based-error-messages", "Text-Based Error Messages", 3), new ArticleHeading("exception-handling", "Exception Handling", 3), new ArticleHeading("effect-error-handling", "Effect Error Handling", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Error").OnLinkClick(onLinkClick)
            | Lead("Display error states consistently with standardized messaging, optional details, and recovery options for better user experience.")
            | new Markdown(
                """"
                The `Error` [widget](app://onboarding/concepts/widgets) provides a standardized way to display error states in your [app](app://onboarding/concepts/apps). It's designed to communicate that something went wrong and optionally provide details and recovery options.
                
                ## Basic Usage
                
                The simplest way to create an Error widget is by passing content directly to the constructor:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicErrorView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicErrorView : ViewBase
                    {
                        public override object? Build()
                        {
                            return new Error("Connection Failed", "Unable to connect to the server");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Error widgets come with sensible defaults: no title, no message, and no stack trace. You can set these properties individually using the fluent extension methods.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Title
                
                Set a descriptive title for the error:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ErrorTitleView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ErrorTitleView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Error().Title("Authentication Failed")
                                | new Error().Title("Network Error")
                                | new Error().Title("Validation Error");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Message
                
                Provide a user-friendly error message:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ErrorMessageView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ErrorMessageView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Error()
                                    .Title("Login Failed")
                                    .Message("Invalid username or password. Please try again.")
                                | new Error()
                                    .Title("File Upload Error")
                                    .Message("The file size exceeds the maximum allowed limit of 10MB.")
                                | new Error()
                                    .Title("Permission Denied")
                                    .Message("You don't have sufficient privileges to access this resource.");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Stack Trace
                
                Include technical details for debugging (useful for developers):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ErrorStackTraceView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ErrorStackTraceView : ViewBase
                    {
                        public override object? Build()
                        {
                            var stackTrace = @"at System.Net.Http.HttpClient.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                    at MyApp.Services.ApiService.GetDataAsync(String endpoint) in /src/Services/ApiService.cs:line 45
                    at MyApp.Views.DataView.LoadDataAsync() in /src/Views/DataView.cs:line 23";
                    
                            return new Error()
                                .Title("API Connection Error")
                                .Message("Failed to connect to the external service")
                                .StackTrace(stackTrace);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Alternative Error Display Methods
                
                While the `Error` widget is excellent for detailed error information, Ivy Framework provides several other ways to display errors depending on your needs:
                
                ### Error Callouts
                
                Use [Callout.Error](app://widgets/primitives/callout) for prominent error messages that need attention:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ErrorCalloutExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ErrorCalloutExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | Callout.Error("Failed to connect to the server. Please check your internet connection.")
                                | Callout.Error("Invalid email format. Please enter a valid email address.", "Validation Error");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Error Toasts
                
                Use `client.Toast` for non-intrusive error notifications:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ClientErrorExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ClientErrorExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            return new Button("Show System Error").Destructive()
                                .OnClick(_ => client.Error(new InvalidOperationException("System configuration validation failed")));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Text-Based Error Messages
                
                Use [Text.Danger](app://widgets/primitives/text-block) for inline error text:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TextErrorExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TextErrorExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Text.Danger("Invalid email format");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("Display [validation errors](app://onboarding/concepts/forms) using the `Invalid` property on [form inputs](app://onboarding/concepts/forms):").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormValidationErrorExamplesView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FormValidationErrorExamplesView : ViewBase
                    {
                        public override object? Build()
                        {
                            var email = UseState("");
                            var password = UseState("");
                            var age = UseState(0);
                    
                            var emailError = UseState<string?>();
                            var passwordError = UseState<string?>();
                            var ageError = UseState<string?>();
                    
                            void ValidateForm()
                            {
                                // Clear previous errors
                                emailError.Set((string?)null);
                                passwordError.Set((string?)null);
                                ageError.Set((string?)null);
                    
                                var hasErrors = false;
                    
                                if (string.IsNullOrWhiteSpace(email.Value)) {
                                    emailError.Set("Email is required");
                                    hasErrors = true;
                                } else if (!email.Value.Contains("@")) {
                                    emailError.Set("Email must be a valid email address");
                                    hasErrors = true;
                                }
                    
                                if (string.IsNullOrWhiteSpace(password.Value)) {
                                    passwordError.Set("Password is required");
                                    hasErrors = true;
                                } else if (password.Value.Length < 8) {
                                    passwordError.Set("Password must be at least 8 characters long");
                                    hasErrors = true;
                                }
                    
                                if (age.Value < 18) {
                                    ageError.Set("You must be at least 18 years old");
                                    hasErrors = true;
                                }
                    
                                if (!hasErrors) {
                                    // Form is valid, proceed with submission
                                    // This would typically call an API or perform an action
                                }
                            }
                    
                            return Layout.Vertical().Gap(4)
                                | Text.H3("Form Validation Errors")
                                | Layout.Vertical().Gap(3)
                                    | Text.Label("Email")
                                    | email.ToTextInput()
                                        .Placeholder("Enter your email")
                                        .Invalid(emailError.Value)
                                    | Text.Label("Password")
                                    | password.ToPasswordInput()
                                        .Placeholder("Enter your password")
                                        .Invalid(passwordError.Value)
                                    | Text.Label("Age")
                                    | age.ToNumberInput()
                                        .Placeholder("Enter your age")
                                        .Invalid(ageError.Value)
                                | new Button("Validate Form")
                                    .OnClick(ValidateForm)
                                | (emailError.Value != null || passwordError.Value != null || ageError.Value != null
                                    ? Callout.Error("Please fix the validation errors above", "Form Validation Failed")
                                    : null);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Exception Handling
                
                Use the Error widget to display error states:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ExceptionHandlingView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ExceptionHandlingView : ViewBase
                    {
                        public override object? Build()
                        {
                            var showError = UseState(false);
                            var showDetails = UseState(false);
                    
                            void SimulateError()
                            {
                                showError.Set(true);
                            }
                    
                            return Layout.Vertical().Gap(4)
                                | new Button("Simulate Error").OnClick(SimulateError).Destructive()
                                | (showError.Value
                                    ? Layout.Vertical().Gap(4)
                                        | new Error()
                                            .Title("Simulated Error")
                                            .Message("This is a simulated error for demonstration purposes")
                                            .StackTrace(showDetails.Value ? "at MyApp.Views.ErrorView.SimulateError() in /src/Views/ErrorView.cs:line 15" : null)
                                        | new Button(showDetails.Value ? "Hide Details" : "Show Details")
                                            .Variant(ButtonVariant.Outline)
                                            .OnClick(() => showDetails.Set(!showDetails.Value))
                                    : Text.Muted("Click the button above to simulate an error"));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Effect Error Handling
                
                Demonstrate how effects can handle error states:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new EffectErrorView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class EffectErrorView : ViewBase
                    {
                        public override object? Build()
                        {
                            var showError = UseState(false);
                    
                            return Layout.Vertical().Gap(4)
                                | new Button("Show Error")
                                    .OnClick(_ => showError.Set(true))
                                | (showError.Value
                                    ? new Error()
                                        .Title("Effect Failed")
                                        .Message("The effect encountered an error during execution")
                                    : Text.Muted("Click button to show error"));
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Error", "Ivy.ErrorExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Error.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Data Loading with Error Handling",
                Vertical().Gap(4)
                | new Markdown("Handle errors gracefully in data loading scenarios:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new DataLoadingView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class DataLoadingView : ViewBase
                        {
                            public override object? Build()
                            {
                                var isLoading = UseState(false);
                                var hasError = UseState(false);
                                var data = UseState<List<string>?>();
                        
                                async Task LoadData()
                                {
                                    isLoading.Set(true);
                                    hasError.Set(false);
                        
                                    // Simulate API call with random chance of failure
                                    await Task.Delay(1000);
                                    if (Random.Shared.Next(2) == 0)
                                    {
                                        hasError.Set(true);
                                    }
                                    else
                                    {
                                        data.Set(new List<string> { "Item 1", "Item 2", "Item 3" });
                                    }
                        
                                    isLoading.Set(false);
                                }
                        
                                // Initial load
                                UseEffect(() => {
                                    _ = LoadData();
                                }, []);
                        
                                return Layout.Vertical().Gap(4)
                                    | Layout.Horizontal().Gap(2)
                                        | new Button("Reload Data").OnClick(async _ => await LoadData())
                                    | (isLoading.Value
                                        ? "Loading..."
                                        : hasError.Value
                                            ? new Error()
                                                .Title("Failed to load data")
                                                .Message("There was a problem connecting to the server")
                                            : data.Value != null
                                                ? Layout.Vertical() | Text.H3("Data Items") | string.Join(", ", data.Value)
                                                : null);
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Error with Recovery Actions",
                Vertical().Gap(4)
                | new Markdown("Combine error display with actionable recovery options:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new ErrorRecoveryExamplesView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class ErrorRecoveryExamplesView : ViewBase
                        {
                            public override object? Build()
                            {
                                var errorState = UseState<Exception?>();
                                var recoveryStep = UseState(0);
                        
                                void SimulateRecoverableError()
                                {
                                    errorState.Set(new InvalidOperationException("File system access denied. Insufficient permissions."));
                                    recoveryStep.Set(1);
                                }
                        
                                void TryRecovery()
                                {
                                    if (recoveryStep.Value < 3)
                                    {
                                        recoveryStep.Set(recoveryStep.Value + 1);
                                    }
                                    else
                                    {
                                        errorState.Set((Exception?)null);
                                        recoveryStep.Set(0);
                                    }
                                }
                        
                                void SkipRecovery()
                                {
                                    errorState.Set((Exception?)null);
                                    recoveryStep.Set(0);
                                }
                        
                                return Layout.Vertical().Gap(4)
                                    | (Layout.Horizontal().Gap(2)
                                        | new Button("Simulate Error").Destructive()
                                            .OnClick(SimulateRecoverableError)
                                        | new Button("Try Recovery")
                                            .OnClick(TryRecovery)
                                            .Disabled(errorState.Value == null)
                                        | new Button("Skip Recovery").Outline()
                                            .OnClick(SkipRecovery)
                                            .Disabled(errorState.Value == null))
                                    | (errorState.Value != null
                                        ? Layout.Vertical().Gap(3)
                                            | new Error()
                                                .Title("Recoverable Error")
                                                .Message($"Step {recoveryStep.Value}/3: {errorState.Value.Message}")
                                            | Callout.Info($"Recovery attempt {recoveryStep.Value} of 3. Click 'Try Recovery' to proceed.")
                                            | (recoveryStep.Value >= 3
                                                ? Text.Success("Recovery completed successfully!")
                                                : Text.Muted("Continue with recovery steps..."))
                                        : Text.Muted("Click 'Simulate Error' to start the recovery workflow"));
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.AppsApp), typeof(Widgets.Primitives.CalloutApp), typeof(Widgets.Primitives.TextBlockApp), typeof(Onboarding.Concepts.FormsApp)]; 
        return article;
    }
}


public class BasicErrorView : ViewBase
{
    public override object? Build()
    {
        return new Error("Connection Failed", "Unable to connect to the server");
    }
}

public class ErrorTitleView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Error().Title("Authentication Failed")
            | new Error().Title("Network Error")
            | new Error().Title("Validation Error");
    }
}

public class ErrorMessageView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Error()
                .Title("Login Failed")
                .Message("Invalid username or password. Please try again.")
            | new Error()
                .Title("File Upload Error")
                .Message("The file size exceeds the maximum allowed limit of 10MB.")
            | new Error()
                .Title("Permission Denied")
                .Message("You don't have sufficient privileges to access this resource.");
    }
}

public class ErrorStackTraceView : ViewBase
{
    public override object? Build()
    {
        var stackTrace = @"at System.Net.Http.HttpClient.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
at MyApp.Services.ApiService.GetDataAsync(String endpoint) in /src/Services/ApiService.cs:line 45
at MyApp.Views.DataView.LoadDataAsync() in /src/Views/DataView.cs:line 23";

        return new Error()
            .Title("API Connection Error")
            .Message("Failed to connect to the external service")
            .StackTrace(stackTrace);
    }
}

public class ErrorCalloutExamplesView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Callout.Error("Failed to connect to the server. Please check your internet connection.")
            | Callout.Error("Invalid email format. Please enter a valid email address.", "Validation Error");
    }
}

public class ClientErrorExamplesView : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        return new Button("Show System Error").Destructive()
            .OnClick(_ => client.Error(new InvalidOperationException("System configuration validation failed")));
    }
}

public class TextErrorExamplesView : ViewBase
{
    public override object? Build()
    {
        return Text.Danger("Invalid email format");
    }
}

public class FormValidationErrorExamplesView : ViewBase
{
    public override object? Build()
    {
        var email = UseState("");
        var password = UseState("");
        var age = UseState(0);
        
        var emailError = UseState<string?>();
        var passwordError = UseState<string?>();
        var ageError = UseState<string?>();
        
        void ValidateForm()
        {
            // Clear previous errors
            emailError.Set((string?)null);
            passwordError.Set((string?)null);
            ageError.Set((string?)null);
            
            var hasErrors = false;
            
            if (string.IsNullOrWhiteSpace(email.Value)) {
                emailError.Set("Email is required");
                hasErrors = true;
            } else if (!email.Value.Contains("@")) {
                emailError.Set("Email must be a valid email address");
                hasErrors = true;
            }
            
            if (string.IsNullOrWhiteSpace(password.Value)) {
                passwordError.Set("Password is required");
                hasErrors = true;
            } else if (password.Value.Length < 8) {
                passwordError.Set("Password must be at least 8 characters long");
                hasErrors = true;
            }
            
            if (age.Value < 18) {
                ageError.Set("You must be at least 18 years old");
                hasErrors = true;
            }
            
            if (!hasErrors) {
                // Form is valid, proceed with submission
                // This would typically call an API or perform an action
            }
        }
        
        return Layout.Vertical().Gap(4)
            | Text.H3("Form Validation Errors")
            | Layout.Vertical().Gap(3)
                | Text.Label("Email")
                | email.ToTextInput()
                    .Placeholder("Enter your email")
                    .Invalid(emailError.Value)
                | Text.Label("Password")
                | password.ToPasswordInput()
                    .Placeholder("Enter your password")
                    .Invalid(passwordError.Value)
                | Text.Label("Age")
                | age.ToNumberInput()
                    .Placeholder("Enter your age")
                    .Invalid(ageError.Value)
            | new Button("Validate Form")
                .OnClick(ValidateForm)
            | (emailError.Value != null || passwordError.Value != null || ageError.Value != null
                ? Callout.Error("Please fix the validation errors above", "Form Validation Failed")
                : null);
    }
}

public class ExceptionHandlingView : ViewBase
{
    public override object? Build()
    {
        var showError = UseState(false);
        var showDetails = UseState(false);
        
        void SimulateError()
        {
            showError.Set(true);
        }
        
        return Layout.Vertical().Gap(4)
            | new Button("Simulate Error").OnClick(SimulateError).Destructive()
            | (showError.Value 
                ? Layout.Vertical().Gap(4)
                    | new Error()
                        .Title("Simulated Error")
                        .Message("This is a simulated error for demonstration purposes")
                        .StackTrace(showDetails.Value ? "at MyApp.Views.ErrorView.SimulateError() in /src/Views/ErrorView.cs:line 15" : null)
                    | new Button(showDetails.Value ? "Hide Details" : "Show Details")
                        .Variant(ButtonVariant.Outline)
                        .OnClick(() => showDetails.Set(!showDetails.Value))
                : Text.Muted("Click the button above to simulate an error"));
    }
}

public class EffectErrorView : ViewBase
{
    public override object? Build()
    {
        var showError = UseState(false);
        
        return Layout.Vertical().Gap(4)
            | new Button("Show Error")
                .OnClick(_ => showError.Set(true))
            | (showError.Value 
                ? new Error()
                    .Title("Effect Failed")
                    .Message("The effect encountered an error during execution")
                : Text.Muted("Click button to show error"));
    }
}

public class DataLoadingView : ViewBase
{
    public override object? Build()
    {
        var isLoading = UseState(false);
        var hasError = UseState(false);
        var data = UseState<List<string>?>();
        
        async Task LoadData()
        {
            isLoading.Set(true);
            hasError.Set(false);
            
            // Simulate API call with random chance of failure
            await Task.Delay(1000);
            if (Random.Shared.Next(2) == 0)
            {
                hasError.Set(true);
            }
            else
            {
                data.Set(new List<string> { "Item 1", "Item 2", "Item 3" });
            }
            
            isLoading.Set(false);
        }
        
        // Initial load
        UseEffect(() => {
            _ = LoadData();
        }, []);
        
        return Layout.Vertical().Gap(4)
            | Layout.Horizontal().Gap(2)
                | new Button("Reload Data").OnClick(async _ => await LoadData())
            | (isLoading.Value 
                ? "Loading..." 
                : hasError.Value 
                    ? new Error()
                        .Title("Failed to load data")
                        .Message("There was a problem connecting to the server")
                    : data.Value != null 
                        ? Layout.Vertical() | Text.H3("Data Items") | string.Join(", ", data.Value)
                        : null);
    }
}

public class ErrorRecoveryExamplesView : ViewBase
{
    public override object? Build()
    {
        var errorState = UseState<Exception?>();
        var recoveryStep = UseState(0);
        
        void SimulateRecoverableError()
        {
            errorState.Set(new InvalidOperationException("File system access denied. Insufficient permissions."));
            recoveryStep.Set(1);
        }
        
        void TryRecovery()
        {
            if (recoveryStep.Value < 3)
            {
                recoveryStep.Set(recoveryStep.Value + 1);
            }
            else
            {
                errorState.Set((Exception?)null);
                recoveryStep.Set(0);
            }
        }
        
        void SkipRecovery()
        {
            errorState.Set((Exception?)null);
            recoveryStep.Set(0);
        }
        
        return Layout.Vertical().Gap(4)
            | (Layout.Horizontal().Gap(2)
                | new Button("Simulate Error").Destructive()
                    .OnClick(SimulateRecoverableError)
                | new Button("Try Recovery")
                    .OnClick(TryRecovery)
                    .Disabled(errorState.Value == null)
                | new Button("Skip Recovery").Outline()
                    .OnClick(SkipRecovery)
                    .Disabled(errorState.Value == null))
            | (errorState.Value != null 
                ? Layout.Vertical().Gap(3)
                    | new Error()
                        .Title("Recoverable Error")
                        .Message($"Step {recoveryStep.Value}/3: {errorState.Value.Message}")
                    | Callout.Info($"Recovery attempt {recoveryStep.Value} of 3. Click 'Try Recovery' to proceed.")
                    | (recoveryStep.Value >= 3 
                        ? Text.Success("Recovery completed successfully!")
                        : Text.Muted("Continue with recovery steps..."))
                : Text.Muted("Click 'Simulate Error' to start the recovery workflow"));
    }
}
