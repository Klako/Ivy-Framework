using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:12, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/12_Callout.md", searchHints: ["alert", "notice", "warning", "info", "banner", "message"])]
public class CalloutApp(bool onlyBody = false) : ViewBase
{
    public CalloutApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("callout", "Callout", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("with-titles", "With Titles", 3), new ArticleHeading("constructor-syntax", "Constructor Syntax", 3), new ArticleHeading("variants", "Variants", 2), new ArticleHeading("info-callouts", "Info Callouts", 3), new ArticleHeading("success-callouts", "Success Callouts", 3), new ArticleHeading("warning-callouts", "Warning Callouts", 3), new ArticleHeading("error-callouts", "Error Callouts", 3), new ArticleHeading("complex-content", "Complex Content", 3), new ArticleHeading("closable-callouts", "Closable callouts", 3), new ArticleHeading("form-integration", "Form Integration", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("faq", "Faq", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Callout").OnLinkClick(onLinkClick)
            | Lead("Create attention-grabbing [components](app://onboarding/concepts/widgets) to highlight important information, warnings, tips, and success messages. Callouts come in different [variants](app://onboarding/concepts/theming) including info, warning, error, and success, with customizable icons and styling.")
            | new Markdown(
                """"
                The `Callout` [widget](app://onboarding/concepts/widgets) displays prominent, styled information boxes that draw attention to important content. They're perfect for user guidance, system messages, warnings, and success confirmations. Each variant has distinct visual styling and appropriate icons to help users quickly understand the message type.
                
                ## Basic Usage
                
                The simplest way to create a callout is using the static factory methods:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Callout.Info("This is an informational message that provides helpful context.");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### With Titles
                
                Add descriptive titles to make your callouts more informative:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new TitledCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class TitledCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | Callout.Info("This feature requires admin privileges.", "Access Note")
                                | Callout.Success("Your settings have been saved successfully!", "Success")
                                | Callout.Warning("Changes made here cannot be automatically undone.", "Caution")
                                | Callout.Error("API connection failed. Check your network settings.", "Connection Error");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Constructor Syntax
                
                You can also use the constructor directly for more control:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ConstructorCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ConstructorCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Callout("This is a basic info callout")
                                | new Callout("Success message with title", "Operation Complete", CalloutVariant.Success)
                                | new Callout("Warning with custom icon", "Important Notice", CalloutVariant.Warning, Icons.TriangleAlert)
                                | new Callout("Error details here", "System Error", CalloutVariant.Error, Icons.Bug);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Variants
                
                ### Info Callouts
                
                Info callouts are perfect for general information, tips, and helpful guidance:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new InfoCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class InfoCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | Callout.Info("Welcome to the new dashboard! Here you can monitor all your system metrics in real-time.")
                                | Callout.Info("Pro tip: Use the search bar to quickly find what you're looking for.", "Quick Tip")
                                | Callout.Info("This feature is currently in beta. Please report any issues you encounter.", "Beta Feature")
                                | Callout.Info("Remember to save your work frequently. Auto-save is enabled every 5 minutes.", "Workflow Reminder");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Success Callouts
                
                Success callouts confirm completed actions and positive outcomes:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SuccessCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SuccessCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | Callout.Success("Your profile has been updated successfully!")
                                | Callout.Success("File uploaded successfully. You can now share it with your team.", "Upload Complete")
                                | Callout.Success("Payment processed successfully. A confirmation email has been sent.", "Payment Confirmed")
                                | Callout.Success("All changes have been saved and synchronized across devices.", "Sync Complete");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Warning Callouts
                
                Warning callouts alert users to potential issues or important considerations:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new WarningCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class WarningCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | Callout.Warning("This action cannot be undone. Please confirm before proceeding.")
                                | Callout.Warning("Your session will expire in 5 minutes. Please save your work.", "Session Expiry")
                                | Callout.Warning("Some features may not work properly in older browsers.", "Browser Compatibility")
                                | Callout.Warning("Large file uploads may take several minutes to complete.", "Upload Time");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Error Callouts
                
                Error callouts communicate problems and guide users toward solutions:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ErrorCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ErrorCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | Callout.Error("Failed to connect to the server. Please check your internet connection.")
                                | Callout.Error("Invalid email format. Please enter a valid email address.", "Validation Error")
                                | Callout.Error("Access denied. You don't have permission to perform this action.", "Permission Error")
                                | Callout.Error("The requested resource was not found. It may have been moved or deleted.", "Resource Not Found");
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Complex Content
                
                Callouts can contain rich content beyond simple text:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ComplexContentCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ComplexContentCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            return Layout.Vertical().Gap(4)
                                | new Callout(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("This callout contains multiple elements including badges and rich content. You can include any widgets as children!")
                                        | (Layout.Horizontal().Gap(2)
                                            | new Badge("Feature", BadgeVariant.Secondary)
                                            | new Badge("New", BadgeVariant.Primary))
                                        | Text.P("Additional content can be added as children"),
                                    "Rich Content Example",
                                    CalloutVariant.Info)
                                | new Callout(
                                    Layout.Vertical().Gap(1)
                                        | Text.P("Please review the following items before proceeding:")
                                        | Text.P("• Check your email settings")
                                        | Text.P("• Verify your phone number")
                                        | Text.P("• Update your profile picture"),
                                    "Action Required",
                                    CalloutVariant.Warning)
                                | new Callout(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("System error details are shown below. Please contact support if this issue persists.")
                                        | new CodeBlock("Error Code: E-1001\nTimestamp: 2024-01-15 14:30:00")
                                        | Text.P("Technical details for debugging"),
                                    "System Error",
                                    CalloutVariant.Error);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Closable callouts
                
                When you set an `OnClose` handler, the callout shows a close (X) button in the top-right corner. Clicking it fires the handler so you can hide the callout. Use [UseTrigger](app://hooks/core/use-trigger) to control visibility.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new ClosableCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class ClosableCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            var (calloutView, showCallout) = UseTrigger((IState<bool> isOpen) =>
                                isOpen.Value
                                    ? Callout.Info("A new version is available. Refresh to update.", "Update Available")
                                        .OnClose(() => isOpen.Set(false))
                                    : null);
                    
                            return Layout.Vertical().Gap(6)
                                | new Button("Show callout", onClick: _ => showCallout())
                                | calloutView;
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Form Integration
                
                Use callouts to contain [forms](app://onboarding/concepts/forms) and provide context:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormCalloutView())),
                new Tab("Code", new CodeBlock(
                    """"
                    public record LoginModel(string Email = "", string Password = "");
                    
                    public class FormCalloutView : ViewBase
                    {
                        public override object? Build()
                        {
                            var loginModel = UseState(() => new LoginModel());
                    
                            return Layout.Vertical().Gap(4)
                                | new Callout(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("All fields marked with * are required. Your information will be kept secure.")
                                        | loginModel.ToForm()
                                            .Builder(m => m.Email, s => s.ToEmailInput().Placeholder("Enter your email"))
                                            .Label(m => m.Email, "Email Address *"),
                                    "Form Guidelines",
                                    CalloutVariant.Info)
                                | new Callout(
                                    Layout.Vertical().Gap(2)
                                        | Text.P("Please use your work email address for business communications.")
                                        | loginModel.ToForm()
                                            .Builder(m => m.Password, s => s.ToPasswordInput().Placeholder("Enter your password"))
                                            .Label(m => m.Password, "Password *"),
                                    "Email Policy",
                                    CalloutVariant.Warning);
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Callout", "Ivy.CalloutExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Callout.cs")
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("Dashboard Notifications",
                Vertical().Gap(4)
                | new Markdown("Create informative dashboard notifications with callouts:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new DashboardCalloutView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class DashboardCalloutView : ViewBase
                        {
                            public override object? Build()
                            {
                                return Layout.Vertical().Gap(4)
                                    | Callout.Success("Revenue increased by 15% this month", "Financial Update").Icon(Icons.TrendingUp)
                                    | Callout.Info("3 new team members joined this week", "Team Update").Icon(Icons.Users)
                                    | Callout.Warning("Server maintenance scheduled for 2:00 AM", "System Notice").Icon(Icons.Server)
                                    | Callout.Error("2 failed login attempts detected", "Security Alert").Icon(Icons.Shield)
                                    | Callout.Info("Your daily backup is running in the background", "Background Process").Icon(Icons.Clock)
                                    | Callout.Success("All systems operational", "System Status").Icon(Icons.Check);
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Custom Icons",
                Vertical().Gap(4)
                | new Markdown("Override the default variant icons with custom ones for more specific messaging:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new CustomIconCalloutView())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class CustomIconCalloutView : ViewBase
                        {
                            public override object? Build()
                            {
                                return Layout.Vertical().Gap(4)
                                    | Callout.Info("New features available!", "What's New").Icon(Icons.Sparkles)
                                    | Callout.Success("Backup completed successfully!", "Backup Status").Icon(Icons.Database)
                                    | Callout.Warning("Maintenance scheduled for tonight", "System Notice").Icon(Icons.Wrench)
                                    | Callout.Error("Security alert detected", "Security Warning").Icon(Icons.Shield)
                                    | Callout.Info("Download in progress...", "File Transfer").Icon(Icons.Download)
                                    | Callout.Success("Integration connected successfully!", "Connection Status").Icon(Icons.Link);
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("How do I create a Callout or info/warning/error message box in Ivy?",
                Vertical().Gap(4)
                | new Markdown("Use the static factory methods on `Callout`:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    Callout.Info("No items found.")
                    Callout.Warning("This action cannot be undone.")
                    Callout.Error("Something went wrong.")
                    Callout.Success("Operation completed!")
                    """",Languages.Csharp)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.ThemingApp), typeof(Hooks.Core.UseTriggerApp), typeof(Onboarding.Concepts.FormsApp)]; 
        return article;
    }
}


public class BasicCalloutView : ViewBase
{
    public override object? Build()
    {
        return Callout.Info("This is an informational message that provides helpful context.");
    }
}

public class TitledCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Callout.Info("This feature requires admin privileges.", "Access Note")
            | Callout.Success("Your settings have been saved successfully!", "Success")
            | Callout.Warning("Changes made here cannot be automatically undone.", "Caution")
            | Callout.Error("API connection failed. Check your network settings.", "Connection Error");
    }
}

public class ConstructorCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Callout("This is a basic info callout")
            | new Callout("Success message with title", "Operation Complete", CalloutVariant.Success)
            | new Callout("Warning with custom icon", "Important Notice", CalloutVariant.Warning, Icons.TriangleAlert)
            | new Callout("Error details here", "System Error", CalloutVariant.Error, Icons.Bug);
    }
}

public class InfoCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Callout.Info("Welcome to the new dashboard! Here you can monitor all your system metrics in real-time.")
            | Callout.Info("Pro tip: Use the search bar to quickly find what you're looking for.", "Quick Tip")
            | Callout.Info("This feature is currently in beta. Please report any issues you encounter.", "Beta Feature")
            | Callout.Info("Remember to save your work frequently. Auto-save is enabled every 5 minutes.", "Workflow Reminder");
    }
}

public class SuccessCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Callout.Success("Your profile has been updated successfully!")
            | Callout.Success("File uploaded successfully. You can now share it with your team.", "Upload Complete")
            | Callout.Success("Payment processed successfully. A confirmation email has been sent.", "Payment Confirmed")
            | Callout.Success("All changes have been saved and synchronized across devices.", "Sync Complete");
    }
}

public class WarningCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Callout.Warning("This action cannot be undone. Please confirm before proceeding.")
            | Callout.Warning("Your session will expire in 5 minutes. Please save your work.", "Session Expiry")
            | Callout.Warning("Some features may not work properly in older browsers.", "Browser Compatibility")
            | Callout.Warning("Large file uploads may take several minutes to complete.", "Upload Time");
    }
}

public class ErrorCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Callout.Error("Failed to connect to the server. Please check your internet connection.")
            | Callout.Error("Invalid email format. Please enter a valid email address.", "Validation Error")
            | Callout.Error("Access denied. You don't have permission to perform this action.", "Permission Error")
            | Callout.Error("The requested resource was not found. It may have been moved or deleted.", "Resource Not Found");
    }
}

public class ComplexContentCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | new Callout(
                Layout.Vertical().Gap(2)
                    | Text.P("This callout contains multiple elements including badges and rich content. You can include any widgets as children!")
                    | (Layout.Horizontal().Gap(2)
                        | new Badge("Feature", BadgeVariant.Secondary)
                        | new Badge("New", BadgeVariant.Primary))
                    | Text.P("Additional content can be added as children"),
                "Rich Content Example",
                CalloutVariant.Info)
            | new Callout(
                Layout.Vertical().Gap(1)
                    | Text.P("Please review the following items before proceeding:")
                    | Text.P("• Check your email settings")
                    | Text.P("• Verify your phone number")
                    | Text.P("• Update your profile picture"),
                "Action Required",
                CalloutVariant.Warning)
            | new Callout(
                Layout.Vertical().Gap(2)
                    | Text.P("System error details are shown below. Please contact support if this issue persists.")
                    | new CodeBlock("Error Code: E-1001\nTimestamp: 2024-01-15 14:30:00")
                    | Text.P("Technical details for debugging"),
                "System Error",
                CalloutVariant.Error);
    }
}

public class ClosableCalloutView : ViewBase
{
    public override object? Build()
    {
        var (calloutView, showCallout) = UseTrigger((IState<bool> isOpen) =>
            isOpen.Value
                ? Callout.Info("A new version is available. Refresh to update.", "Update Available")
                    .OnClose(() => isOpen.Set(false))
                : null);

        return Layout.Vertical().Gap(6)
            | new Button("Show callout", onClick: _ => showCallout())
            | calloutView;
    }
}

public record LoginModel(string Email = "", string Password = "");

public class FormCalloutView : ViewBase
{
    public override object? Build()
    {
        var loginModel = UseState(() => new LoginModel());
        
        return Layout.Vertical().Gap(4)
            | new Callout(
                Layout.Vertical().Gap(2)
                    | Text.P("All fields marked with * are required. Your information will be kept secure.")
                    | loginModel.ToForm()
                        .Builder(m => m.Email, s => s.ToEmailInput().Placeholder("Enter your email"))
                        .Label(m => m.Email, "Email Address *"),
                "Form Guidelines",
                CalloutVariant.Info)
            | new Callout(
                Layout.Vertical().Gap(2)
                    | Text.P("Please use your work email address for business communications.")
                    | loginModel.ToForm()
                        .Builder(m => m.Password, s => s.ToPasswordInput().Placeholder("Enter your password"))
                        .Label(m => m.Password, "Password *"),
                "Email Policy",
                CalloutVariant.Warning);
    }
}

public class DashboardCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Callout.Success("Revenue increased by 15% this month", "Financial Update").Icon(Icons.TrendingUp)
            | Callout.Info("3 new team members joined this week", "Team Update").Icon(Icons.Users)
            | Callout.Warning("Server maintenance scheduled for 2:00 AM", "System Notice").Icon(Icons.Server)
            | Callout.Error("2 failed login attempts detected", "Security Alert").Icon(Icons.Shield)
            | Callout.Info("Your daily backup is running in the background", "Background Process").Icon(Icons.Clock)
            | Callout.Success("All systems operational", "System Status").Icon(Icons.Check);
    }
}

public class CustomIconCalloutView : ViewBase
{
    public override object? Build()
    {
        return Layout.Vertical().Gap(4)
            | Callout.Info("New features available!", "What's New").Icon(Icons.Sparkles)
            | Callout.Success("Backup completed successfully!", "Backup Status").Icon(Icons.Database)
            | Callout.Warning("Maintenance scheduled for tonight", "System Notice").Icon(Icons.Wrench)
            | Callout.Error("Security alert detected", "Security Warning").Icon(Icons.Shield)
            | Callout.Info("Download in progress...", "File Transfer").Icon(Icons.Download)
            | Callout.Success("Integration connected successfully!", "Connection Status").Icon(Icons.Link);
    }
}
