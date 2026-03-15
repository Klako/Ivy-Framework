using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Onboarding.Concepts;

[App(order:13, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/01_Onboarding/02_Concepts/13_Clients.md", searchHints: ["browser", "client-side", "javascript", "interop", "api", "provider"])]
public class ClientsApp(bool onlyBody = false) : ViewBase
{
    public ClientsApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("clients", "Clients", 1), new ArticleHeading("overview", "Overview", 2), new ArticleHeading("common-use-cases", "Common Use Cases", 2), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("ui-refresh--state-management", "UI Refresh & State Management", 2), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Clients").OnLinkClick(onLinkClick)
            | Lead("Bridge server-side C# code with client-side React components for seamless browser interactions and client-side operations.")
            | new Markdown(
                """"
                ## Overview
                
                The `IClientProvider` interface is the main entry point for client-side interactions. It's available through dependency injection using the [UseService](app://hooks/core/use-service) hook:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock("var client = UseService<IClientProvider>();",Languages.Csharp)
            | new Markdown(
                """"
                ## Common Use Cases
                
                You can show [toasts](app://onboarding/concepts/alerts):
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Simple toast
                client.Toast("Operation successful!");
                
                // Toast with title
                client.Toast("Data saved", "Success");
                """",Languages.Csharp)
            | new Markdown("You can [navigate](app://onboarding/concepts/navigation) to different pages within the app:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Navigate to different pages within the app
                client.Navigate("/dashboard");
                
                // Redirect to external site (replaces current page)
                client.Redirect("https://example.com");
                
                // Open URL in new tab (keeps current page open)
                client.OpenUrl("https://github.com");
                client.OpenUrl(new Uri("https://stackoverflow.com"));
                """",Languages.Csharp)
            | new Markdown("You can download and [upload](app://widgets/inputs/file-input) files:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Download CSV data
                var csvData = Encoding.UTF8.GetBytes("Name,Age\nJohn,30\nJane,25");
                client.DownloadFile("users.csv", csvData, "text/csv");
                
                // Download with progress tracking
                var progress = UseState(0.0);
                client.DownloadFile("large-file.zip", fileData, onProgress: p => progress.Value = p);
                """",Languages.Csharp)
            | new CodeBlock(
                """"
                // Handle single file upload
                client.UploadFiles(async files => {
                    var file = files.FirstOrDefault();
                    if (file != null)
                    {
                        var content = await file.GetContentAsync();
                        await ProcessFileAsync(file.FileName, content);
                        client.Toast($"Uploaded {file.FileName}");
                    }
                });
                """",Languages.Csharp)
            | new Markdown("You can copy text to the clipboard:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Copy text to clipboard
                client.CopyToClipboard("Copied to clipboard!");
                client.Toast("Text copied!");
                """",Languages.Csharp)
            | new Markdown("You can set the [theme mode](app://onboarding/concepts/theming) and apply custom CSS:").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Set theme mode
                client.SetThemeMode(ThemeMode.Dark);
                
                // Apply custom CSS
                var customCss = @"
                :root {
                    --primary: #ff6b6b;
                    --secondary: #4ecdc4;
                }";
                client.ApplyTheme(customCss);
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Best Practices
                
                1. **Dependency Injection**: Always use `UseService<IClientProvider>()` to get the client instance.
                2. **Error Handling**: Wrap client operations in try-catch blocks when appropriate.
                3. **Async Operations**: Use async/await for operations that might take time.
                4. **State Management**: Use clients in combination with [state management](app://hooks/core/use-state) for reactive updates.
                
                ## UI Refresh & State Management
                
                Ivy automatically handles UI refreshes in most cases. You typically **don't need** to manually refresh the UI:
                
                - **[Form](app://onboarding/concepts/forms) submissions**: When forms are submitted successfully, the UI automatically updates
                - **State changes**: When [state](app://hooks/core/use-state) values change, the UI automatically re-renders
                - **Sheet dismissal**: [Sheets](app://widgets/advanced/sheet) are automatically closed by the framework when forms are submitted successfully
                - **Navigation**: Page [navigation](app://onboarding/concepts/navigation) automatically refreshes the UI
                
                **Do this instead** - Let the framework handle it:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // Just update state, UI refreshes automatically
                var isOpen = UseState(false);
                var formData = UseState("");
                
                // When form submits successfully, sheet closes automatically
                if (formSubmitted.Value)
                {
                    formSubmitted.Value = false;
                    isOpen.Value = false; // This triggers UI update
                    client.Toast("Form saved successfully!");
                }
                """",Languages.Csharp)
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("Form Submission with Toast Feedback",
                Tabs( 
                    new Tab("Demo", new Box().Content(new FormSubmissionApp())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class FormSubmissionApp : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                                var nameState = UseState("");
                                var submitTrigger = UseState(false);
                        
                                if (submitTrigger.Value)
                                {
                                    submitTrigger.Value = false;
                                    var name = nameState.Value;
                                    if (string.IsNullOrEmpty(name))
                                    {
                                        client.Toast("Please enter a name", "Validation Error");
                                    }
                                    else
                                    {
                                        client.Toast($"Hello, {name}! Form submitted successfully.");
                                    }
                                }
                        
                                return Layout.Vertical(
                                    new TextInput(nameState.Value, e => nameState.Value = e.Value) { Placeholder = "Your name" },
                                    new Button("Submit Form", _ => submitTrigger.Value = true)
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("File Operations Simulation",
                Tabs( 
                    new Tab("Demo", new Box().Content(new FileOperationsApp())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class FileOperationsApp : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                                var downloadTrigger = UseState(false);
                                var uploadTrigger = UseState(false);
                                var downloadComplete = UseState(false);
                                var uploadComplete = UseState(false);
                        
                                if (downloadTrigger.Value)
                                {
                                    downloadTrigger.Value = false;
                                    downloadComplete.Value = false;
                                    client.Toast("Downloading file...", "Download Started");
                        
                                    // Simulate download completion after 2 seconds
                                    Task.Run(async () => {
                                        await Task.Delay(2000);
                                        downloadComplete.Value = true;
                                    });
                                }
                        
                                if (uploadTrigger.Value)
                                {
                                    uploadTrigger.Value = false;
                                    uploadComplete.Value = false;
                                    client.Toast("Uploading files...", "Upload Started");
                        
                                    // Simulate upload completion after 3 seconds
                                    Task.Run(async () => {
                                        await Task.Delay(3000);
                                        uploadComplete.Value = true;
                                    });
                                }
                        
                                // Show completion messages when state changes
                                if (downloadComplete.Value)
                                {
                                    downloadComplete.Value = false;
                                    client.Toast("File downloaded successfully!");
                                }
                        
                                if (uploadComplete.Value)
                                {
                                    uploadComplete.Value = false;
                                    client.Toast("Files uploaded successfully!");
                                }
                        
                                return Layout.Vertical(
                                    new Button("Simulate File Download", _ => downloadTrigger.Value = true),
                                    new Button("Simulate File Upload", _ => uploadTrigger.Value = true)
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Navigation and URL Management",
                Tabs( 
                    new Tab("Demo", new Box().Content(new ClientNavigationApp())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class ClientNavigationApp : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                                var copyTrigger = UseState(false);
                                var openTabsTrigger = UseState(false);
                        
                                if (copyTrigger.Value)
                                {
                                    copyTrigger.Value = false;
                                    var appDescriptor = UseService<AppDescriptor>();
                                    var info = $"Current app: {appDescriptor.Title}";
                                    client.CopyToClipboard(info);
                                    client.Toast("Page info copied to clipboard!");
                                }
                        
                                if (openTabsTrigger.Value)
                                {
                                    openTabsTrigger.Value = false;
                                    client.OpenUrl("https://google.com");
                                    client.OpenUrl("https://github.com");
                                    client.Toast("Opened multiple tabs", "Navigation");
                                }
                        
                                return Layout.Vertical(
                                    new Button("Copy Current Page Info", _ => copyTrigger.Value = true),
                                    new Button("Open Multiple Tabs", _ => openTabsTrigger.Value = true)
                                );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("How do I copy text to the clipboard in Ivy?",
                Vertical().Gap(4)
                | new Markdown("Use the `CopyToClipboard` extension method on `IClientProvider`:").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    var client = UseService<IClientProvider>();
                    client.CopyToClipboard(content);
                    client.Toast("Copied to clipboard!");
                    """",Languages.Csharp)
                | new Markdown("**Note:** There is no `UseClipboard` hook. Clipboard access is provided through `IClientProvider`, not through a dedicated hook. Access it via `UseService<IClientProvider>()`.").OnLinkClick(onLinkClick)
            )
            | new Markdown(
                """"
                ## See Also
                
                - [Forms](app://onboarding/concepts/forms)
                - [State Management](app://hooks/core/use-state)
                - [Effects](app://hooks/core/use-effect)
                - [Alerts & Notifications](app://onboarding/concepts/alerts)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Hooks.Core.UseServiceApp), typeof(Onboarding.Concepts.AlertsApp), typeof(Onboarding.Concepts.NavigationApp), typeof(Widgets.Inputs.FileInputApp), typeof(Onboarding.Concepts.ThemingApp), typeof(Hooks.Core.UseStateApp), typeof(Onboarding.Concepts.FormsApp), typeof(Widgets.Advanced.SheetApp), typeof(Hooks.Core.UseEffectApp)]; 
        return article;
    }
}


public class FormSubmissionApp : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var nameState = UseState("");
        var submitTrigger = UseState(false);
        
        if (submitTrigger.Value)
        {
            submitTrigger.Value = false;
            var name = nameState.Value;
            if (string.IsNullOrEmpty(name))
            {
                client.Toast("Please enter a name", "Validation Error");
            }
            else
            {
                client.Toast($"Hello, {name}! Form submitted successfully.");
            }
        }
        
        return Layout.Vertical(
            new TextInput(nameState.Value, e => nameState.Value = e.Value) { Placeholder = "Your name" },
            new Button("Submit Form", _ => submitTrigger.Value = true)
        );
    }
}

public class FileOperationsApp : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var downloadTrigger = UseState(false);
        var uploadTrigger = UseState(false);
        var downloadComplete = UseState(false);
        var uploadComplete = UseState(false);
        
        if (downloadTrigger.Value)
        {
            downloadTrigger.Value = false;
            downloadComplete.Value = false;
            client.Toast("Downloading file...", "Download Started");
            
            // Simulate download completion after 2 seconds
            Task.Run(async () => {
                await Task.Delay(2000);
                downloadComplete.Value = true;
            });
        }
        
        if (uploadTrigger.Value)
        {
            uploadTrigger.Value = false;
            uploadComplete.Value = false;
            client.Toast("Uploading files...", "Upload Started");
            
            // Simulate upload completion after 3 seconds
            Task.Run(async () => {
                await Task.Delay(3000);
                uploadComplete.Value = true;
            });
        }
        
        // Show completion messages when state changes
        if (downloadComplete.Value)
        {
            downloadComplete.Value = false;
            client.Toast("File downloaded successfully!");
        }
        
        if (uploadComplete.Value)
        {
            uploadComplete.Value = false;
            client.Toast("Files uploaded successfully!");
        }
        
        return Layout.Vertical(
            new Button("Simulate File Download", _ => downloadTrigger.Value = true),
            new Button("Simulate File Upload", _ => uploadTrigger.Value = true)
        );
    }
}

public class ClientNavigationApp : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var copyTrigger = UseState(false);
        var openTabsTrigger = UseState(false);
        
        if (copyTrigger.Value)
        {
            copyTrigger.Value = false;
            var appDescriptor = UseService<AppDescriptor>();
            var info = $"Current app: {appDescriptor.Title}";
            client.CopyToClipboard(info);
            client.Toast("Page info copied to clipboard!");
        }
        
        if (openTabsTrigger.Value)
        {
            openTabsTrigger.Value = false;
            client.OpenUrl("https://google.com");
            client.OpenUrl("https://github.com");
            client.Toast("Opened multiple tabs", "Navigation");
        }
        
        return Layout.Vertical(
            new Button("Copy Current Page Info", _ => copyTrigger.Value = true),
            new Button("Open Multiple Tabs", _ => openTabsTrigger.Value = true)
        );
    }
}
