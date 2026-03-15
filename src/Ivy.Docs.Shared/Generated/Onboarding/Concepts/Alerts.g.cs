using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:17, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/17_Alerts.md", searchHints: ["dialog", "toast", "notification", "modal", "confirm", "message", "alert", "usealert", "show-alert"])]
public class AlertsApp(bool onlyBody = false) : ViewBase
{
    public AlertsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("alerts--notifications", "Alerts & Notifications", 1), new ArticleHeading("types-of-alerts", "Types of Alerts", 2), new ArticleHeading("dialog-alerts", "Dialog Alerts", 2), new ArticleHeading("basic-dialog-alert", "Basic Dialog Alert", 3), new ArticleHeading("alert-button-sets", "Alert Button Sets", 3), new ArticleHeading("toast-notifications", "Toast Notifications", 2), new ArticleHeading("basic-toast-notifications", "Basic Toast Notifications", 3), new ArticleHeading("toast-with-exception-handling", "Toast with Exception Handling", 3), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("form-submission-with-feedback", "Form Submission with Feedback", 3), new ArticleHeading("usealert", "UseAlert", 2), new ArticleHeading("basic-usage", "Basic Usage", 3), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Alerts & Notifications").OnLinkClick(onLinkClick)
            | Lead("Communicate with users effectively using modal dialog alerts for important confirmations and toast notifications for feedback messages.")
            | new Markdown(
                """"
                ## Types of Alerts
                
                Ivy provides two main types of alerts:
                
                1. **Dialog Alerts** - Modal dialogs for important confirmations and decisions
                2. **Toast Notifications** - Non-blocking notifications for [feedback](app://widgets/inputs/feedback-input) and status updates
                
                ## Dialog Alerts
                
                Dialog alerts are modal windows that require [user interaction](app://onboarding/concepts/event-handlers). They're perfect for confirmations, important messages, or collecting user decisions.
                
                ### Basic Dialog Alert
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicDialogAlertDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var (alertView, showAlert) = UseAlert();
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Vertical(
                                new Button("Show Alert", _ =>
                                    showAlert("Are you sure you want to continue?", result => {
                                        client.Toast($"You selected: {result}");
                                    }, "Alert title")
                                ),
                                alertView
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicDialogAlertDemo())
            )
            | new Markdown(
                """"
                ### Alert Button Sets
                
                Dialog alerts support different button combinations:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class AlertButtonSetsDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var (alertView, showAlert) = UseAlert();
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Horizontal(
                                new Button("Ok Only", _ =>
                                    showAlert("This is an info message", _ => {}, "Information", AlertButtonSet.Ok)
                                ),
                                new Button("Ok/Cancel", _ =>
                                    showAlert("Do you want to save changes?", result => {
                                        client.Toast($"Result: {result}");
                                    }, "Confirm Save", AlertButtonSet.OkCancel)
                                ),
                                new Button("Yes/No", _ =>
                                    showAlert("Do you like Ivy?", result => {
                                        client.Toast($"Answer: {result}");
                                    }, "Quick Poll", AlertButtonSet.YesNo)
                                ),
                                new Button("Yes/No/Cancel", _ =>
                                    showAlert("Save changes before closing?", result => {
                                        client.Toast($"Choice: {result}");
                                    }, "Unsaved Changes", AlertButtonSet.YesNoCancel)
                                ),
                                alertView
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new AlertButtonSetsDemo())
            )
            | new Markdown(
                """"
                ## Toast Notifications
                
                Toast notifications are lightweight, non-blocking messages that appear temporarily and then disappear automatically. They're perfect for providing quick feedback about user actions.
                
                ### Basic Toast Notifications
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class BasicToastDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Horizontal(
                                new Button("Success Toast", _ =>
                                    client.Toast("Operation completed successfully!", "Success").Success()
                                ),
                                new Button("Error Toast", _ =>
                                    client.Toast("Something went wrong!", "Error").Destructive()
                                ),
                                new Button("Info Toast", _ =>
                                    client.Toast("Here's some helpful information", "Info").Info()
                                ),
                                new Button("Simple Toast", _ =>
                                    client.Toast("Just a simple message")
                                )
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new BasicToastDemo())
            )
            | new Markdown("### Toast with Exception Handling").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class ToastExceptionDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Horizontal(
                                new Button("Simulate Error", _ => {
                                    try {
                                        throw new InvalidOperationException("Something went wrong!");
                                    } catch (Exception ex) {
                                        client.Toast(ex); // Automatically formats exception
                                    }
                                }),
                                new Button("Custom Error Toast", _ =>
                                    client.Toast("Custom error message", "Error")
                                )
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new ToastExceptionDemo())
            )
            | new Markdown(
                """"
                ## Faq
                
                ### Form Submission with Feedback
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    public class FormSubmissionDemo : ViewBase
                    {
                        public override object? Build()
                        {
                            var (alertView, showAlert) = UseAlert();
                            var client = UseService<IClientProvider>();
                            var isSubmitting = UseState(false);
                    
                            return Layout.Vertical(
                                new Button(
                                    isSubmitting.Value ? "Submitting..." : "Submit Form",
                                    _ => {
                                        showAlert("Are you ready to submit this form?", async result => {
                                            if (result == AlertResult.Ok) {
                                                isSubmitting.Set(true);
                    
                                                // Simulate API call
                                                await Task.Delay(2000);
                    
                                                isSubmitting.Set(false);
                                                client.Toast("Form submitted successfully!", "Success");
                                            }
                                        }, "Confirm Submission", AlertButtonSet.OkCancel);
                                    }
                                ).Disabled(isSubmitting.Value),
                                alertView
                            );
                        }
                    }
                    """",Languages.Csharp)
                | new Box().Content(new FormSubmissionDemo())
            )
            | new Markdown(
                """"
                ## UseAlert
                
                The [UseAlert](app://onboarding/concepts/alerts) hook returns a tuple containing an alert view and a show alert delegate. It manages alert state and provides a programmatic way to trigger modal dialog alerts with customizable button sets and callbacks.
                """").OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ```mermaid
                graph LR
                    A[UseAlert Hook] --> B[Create Alert State]
                    B --> C[Create Alert View]
                    C --> D[Create Show Delegate]
                    D --> E[Return Alert View]
                    E --> F[Return Show Delegate]
                ```
                """").OnLinkClick(onLinkClick)
            | new Callout("The `UseAlert` hook returns an alert view that should be included in your component's render output, and a delegate function that can be called to show alerts programmatically.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new Markdown(
                """"
                ### Basic Usage
                
                Use `UseAlert` to create modal dialog alerts for confirmations and user feedback.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new AlertExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class AlertExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var (alertView, showAlert) = UseAlert();
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Vertical()
                                | new Button("Show Alert", onClick: _ =>
                                    showAlert("Are you sure you want to continue?", result =>
                                    {
                                        client.Toast($"You selected: {result}");
                                    }, "Alert Title"))
                                | alertView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Expandable("How do I show an alert dialog to the user?",
                Vertical().Gap(4)
                | new Markdown("Use the `UseAlert` hook, which returns a tuple of `(alertView, showAlert)`:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var (alertView, showAlert) = UseAlert();
                    
                    return Layout.Vertical()
                        | new Button("Show Alert", _ =>
                            showAlert("Are you sure?", result =>
                            {
                                // result is the AlertResult (Ok, Cancel, Yes, No)
                            }, "Confirmation", AlertButtonSet.OkCancel))
                        | alertView; // IMPORTANT: alertView must be included in the view tree
                    """",Languages.Csharp)
                | new Markdown(
                    """"
                    **Key points:**
                    
                    - `UseAlert()` returns a **tuple**, not an object — always destructure it
                    - `showAlert` is a delegate: `showAlert(message, callback, title?, buttonSet?)`
                    - `alertView` must be rendered somewhere in your view tree for the dialog to appear
                    - Available button sets: `AlertButtonSet.Ok`, `AlertButtonSet.OkCancel`, `AlertButtonSet.YesNo`, `AlertButtonSet.YesNoCancel`
                    - For simple toast notifications, use `client.Toast("message")` or `client.Error("message")` via `IClientProvider`
                    """").OnLinkClick(onLinkClick)
            )
            | new Expandable("How do I show a delete confirmation before deleting an entity?",
                Vertical().Gap(4)
                | new Markdown("Use `UseAlert()` for confirmation dialogs:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var (alertView, showAlert) = UseAlert();
                    var client = UseService<IClientProvider>();
                    
                    void DeleteItem(int id)
                    {
                        showAlert("Are you sure you want to delete this item?", async result =>
                        {
                            if (result == AlertResult.Ok)
                            {
                                await using var db = dbFactory.CreateDbContext();
                                var item = await db.Items.FindAsync(id);
                                if (item != null)
                                {
                                    db.Items.Remove(item);
                                    await db.SaveChangesAsync();
                                    client.Toast("Item deleted");
                                    refreshToken.Refresh();
                                }
                            }
                        }, "Confirm Delete", AlertButtonSet.OkCancel);
                    }
                    
                    return Layout.Vertical()
                        | new Button("Delete", _ => DeleteItem(itemId)).Destructive()
                        | alertView; // IMPORTANT: must include alertView in the view tree
                    """",Languages.Csharp)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Inputs.FeedbackInputApp), typeof(Onboarding.Concepts.EventHandlersApp), typeof(Onboarding.Concepts.AlertsApp)]; 
        return article;
    }
}


public class BasicDialogAlertDemo : ViewBase
{
    public override object? Build()
    {
        var (alertView, showAlert) = UseAlert();
        var client = UseService<IClientProvider>();

        return Layout.Vertical(
            new Button("Show Alert", _ => 
                showAlert("Are you sure you want to continue?", result => {
                    client.Toast($"You selected: {result}");
                }, "Alert title")
            ),
            alertView
        );
    }
}

public class AlertButtonSetsDemo : ViewBase
{
    public override object? Build()
    {
        var (alertView, showAlert) = UseAlert();
        var client = UseService<IClientProvider>();

        return Layout.Horizontal(
            new Button("Ok Only", _ => 
                showAlert("This is an info message", _ => {}, "Information", AlertButtonSet.Ok)
            ),
            new Button("Ok/Cancel", _ => 
                showAlert("Do you want to save changes?", result => {
                    client.Toast($"Result: {result}");
                }, "Confirm Save", AlertButtonSet.OkCancel)
            ),
            new Button("Yes/No", _ => 
                showAlert("Do you like Ivy?", result => {
                    client.Toast($"Answer: {result}");
                }, "Quick Poll", AlertButtonSet.YesNo)
            ),
            new Button("Yes/No/Cancel", _ => 
                showAlert("Save changes before closing?", result => {
                    client.Toast($"Choice: {result}");
                }, "Unsaved Changes", AlertButtonSet.YesNoCancel)
            ),
            alertView
        );
    }
}

public class BasicToastDemo : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();

        return Layout.Horizontal(
            new Button("Success Toast", _ => 
                client.Toast("Operation completed successfully!", "Success").Success()
            ),
            new Button("Error Toast", _ => 
                client.Toast("Something went wrong!", "Error").Destructive()
            ),
            new Button("Info Toast", _ => 
                client.Toast("Here's some helpful information", "Info").Info()
            ),
            new Button("Simple Toast", _ => 
                client.Toast("Just a simple message")
            )
        );
    }
}

public class ToastExceptionDemo : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();

        return Layout.Horizontal(
            new Button("Simulate Error", _ => {
                try {
                    throw new InvalidOperationException("Something went wrong!");
                } catch (Exception ex) {
                    client.Toast(ex); // Automatically formats exception
                }
            }),
            new Button("Custom Error Toast", _ => 
                client.Toast("Custom error message", "Error")
            )
        );
    }
}

public class FormSubmissionDemo : ViewBase
{
    public override object? Build()
    {
        var (alertView, showAlert) = UseAlert();
        var client = UseService<IClientProvider>();
        var isSubmitting = UseState(false);

        return Layout.Vertical(
            new Button(
                isSubmitting.Value ? "Submitting..." : "Submit Form", 
                _ => {
                    showAlert("Are you ready to submit this form?", async result => {
                        if (result == AlertResult.Ok) {
                            isSubmitting.Set(true);
                            
                            // Simulate API call
                            await Task.Delay(2000);
                            
                            isSubmitting.Set(false);
                            client.Toast("Form submitted successfully!", "Success");
                        }
                    }, "Confirm Submission", AlertButtonSet.OkCancel);
                }
            ).Disabled(isSubmitting.Value),
            alertView
        );
    }
}

public class AlertExample : ViewBase
{
    public override object? Build()
    {
        var (alertView, showAlert) = UseAlert();
        var client = UseService<IClientProvider>();

        return Layout.Vertical()
            | new Button("Show Alert", onClick: _ =>
                showAlert("Are you sure you want to continue?", result =>
                {
                    client.Toast($"You selected: {result}");
                }, "Alert Title"))
            | alertView;
    }
}
