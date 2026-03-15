using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:3, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/03_Tooltip.md", searchHints: ["hint", "hover", "popover", "help", "info", "tooltip"])]
public class TooltipApp(bool onlyBody = false) : ViewBase
{
    public TooltipApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("tooltip", "Tooltip", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("form-validation", "Form Validation", 3), new ArticleHeading("with-icons", "With Icons", 3), new ArticleHeading("status-indicators", "Status Indicators", 3), new ArticleHeading("rich-content", "Rich Content", 3), new ArticleHeading("examples", "Examples", 2), new ArticleHeading("best-practices", "Best Practices", 2), new ArticleHeading("keep-tooltips-concise", "Keep Tooltips Concise", 3), new ArticleHeading("use-consistent-language", "Use Consistent Language", 3), new ArticleHeading("provide-actionable-information", "Provide Actionable Information", 3), new ArticleHeading("accessibility-considerations", "Accessibility Considerations", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Tooltip").OnLinkClick(onLinkClick)
            | Lead("Enhance user experience with contextual tooltips that provide helpful information on hover or focus without cluttering the interface.")
            | new Markdown(
                """"
                `Tooltip`s provide contextual information when hovering or focusing on a [widget](app://onboarding/concepts/widgets). They are essential for improving user experience by offering additional context without cluttering the interface.
                
                ## Basic Usage
                
                Here's a simple example of a tooltip on a button:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Button("Hover Me").WithTooltip("Hello World!")
                    """",Languages.Csharp)
                | new Box().Content(new Button("Hover Me").WithTooltip("Hello World!"))
            )
            | new Markdown(
                """"
                ### Form Validation
                
                Tooltips are perfect for displaying validation errors or helpful hints on form inputs:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical()
    | new TextInput(placeholder: "Enter email").WithTooltip("Enter a valid email address")
    | new TextInput(placeholder: "Enter password").WithTooltip("Password must be at least 8 characters long")
    | new NumberInput<int>(placeholder: "Enter age").WithTooltip("Must be between 18 and 100"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical()
                        | new TextInput(placeholder: "Enter email").WithTooltip("Enter a valid email address")
                        | new TextInput(placeholder: "Enter password").WithTooltip("Password must be at least 8 characters long")
                        | new NumberInput<int>(placeholder: "Enter age").WithTooltip("Must be between 18 and 100")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### With Icons
                
                Use tooltips to explain the meaning of icons, especially in toolbars or [navigation](app://onboarding/concepts/navigation) (using icon-only buttons):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Button().Icon(Icons.Save).WithTooltip("Save changes")
    | new Button().Icon(Icons.Download).WithTooltip("Download file")
    | new Button().Icon(Icons.Settings).WithTooltip("Open settings")
    | new Button().Icon(Icons.Info).WithTooltip("Get help"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Button().Icon(Icons.Save).WithTooltip("Save changes")
                        | new Button().Icon(Icons.Download).WithTooltip("Download file")
                        | new Button().Icon(Icons.Settings).WithTooltip("Open settings")
                        | new Button().Icon(Icons.Info).WithTooltip("Get help")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Status Indicators
                
                Provide additional context for status badges and indicators:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Badge("Online", icon: Icons.Circle).WithTooltip("User is currently active")
    | new Badge("Away", icon: Icons.Circle).WithTooltip("User is away from keyboard")
    | new Badge("Busy", icon: Icons.Circle).WithTooltip("User is in a meeting")
    | new Badge("Offline", icon: Icons.Circle).WithTooltip("User is not available"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Badge("Online", icon: Icons.Circle).WithTooltip("User is currently active")
                        | new Badge("Away", icon: Icons.Circle).WithTooltip("User is away from keyboard")
                        | new Badge("Busy", icon: Icons.Circle).WithTooltip("User is in a meeting")
                        | new Badge("Offline", icon: Icons.Circle).WithTooltip("User is not available")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Rich Content
                
                Tooltips can contain detailed multi-line text to provide comprehensive information.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Button("Advanced Tooltip")
                        .WithTooltip("This tooltip contains multiple lines of text and can be quite detailed to provide comprehensive information to the user.")
                    """",Languages.Csharp)
                | new Box().Content(new Button("Advanced Tooltip")
    .WithTooltip("This tooltip contains multiple lines of text and can be quite detailed to provide comprehensive information to the user."))
            )
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Various Widgets",
                Vertical().Gap(4)
                | new Markdown("Tooltips work with any interactive [element](app://onboarding/concepts/widgets).").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(Layout.Grid().Columns(2)
    | new Button("Primary").WithTooltip("Primary action button")
    | new Button("Secondary").Secondary().WithTooltip("Secondary action button")
    | new Badge("Success").WithTooltip("Operation completed successfully")
    | new Badge("Error", variant: BadgeVariant.Destructive).WithTooltip("An error occurred")
    | new Card("Card Title").WithTooltip("This card contains detailed information")
    | Text.Literal("Important Text").WithTooltip("This text requires attention"))),
                    new Tab("Code", new CodeBlock(
                        """"
                        Layout.Grid().Columns(2)
                            | new Button("Primary").WithTooltip("Primary action button")
                            | new Button("Secondary").Secondary().WithTooltip("Secondary action button")
                            | new Badge("Success").WithTooltip("Operation completed successfully")
                            | new Badge("Error", variant: BadgeVariant.Destructive).WithTooltip("An error occurred")
                            | new Card("Card Title").WithTooltip("This card contains detailed information")
                            | Text.Literal("Important Text").WithTooltip("This text requires attention")
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            | new Expandable("Navigation",
                Vertical().Gap(4)
                | new Markdown("Help users understand navigation elements:").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        Layout.Horizontal()
                            | new Button("Dashboard").Icon(Icons.House).WithTooltip("View your main dashboard")
                            | new Button("Projects").Icon(Icons.Folder).WithTooltip("Manage your projects")
                            | new Button("Settings").Icon(Icons.Settings).WithTooltip("Configure your account settings")
                            | new Button("Profile").Icon(Icons.User).WithTooltip("View and edit your profile")
                            | new Button("Help").Icon(Icons.Info).WithTooltip("Get help and support")
                        """",Languages.Csharp)
                    | new Box().Content(Layout.Horizontal()
    | new Button("Dashboard").Icon(Icons.House).WithTooltip("View your main dashboard")
    | new Button("Projects").Icon(Icons.Folder).WithTooltip("Manage your projects")
    | new Button("Settings").Icon(Icons.Settings).WithTooltip("Configure your account settings")
    | new Button("Profile").Icon(Icons.User).WithTooltip("View and edit your profile")
    | new Button("Help").Icon(Icons.Info).WithTooltip("Get help and support"))
                )
            )
            | new Expandable("Form Help",
                Vertical().Gap(4)
                | new Markdown("Provide contextual help for form fields:").OnLinkClick(onLinkClick)
                | (Vertical() 
                    | new CodeBlock(
                        """"
                        Layout.Vertical()
                            | new TextInput(placeholder: "Enter username").WithTooltip("Choose a unique username that will be visible to other users")
                            | new TextInput(placeholder: "Enter email").WithTooltip("We'll use this email for account verification and notifications")
                            | new NumberInput<int>(placeholder: "Enter age").WithTooltip("You must be at least 13 years old to create an account")
                            | new BoolInput("Newsletter").WithTooltip("Receive updates about new features and improvements")
                        """",Languages.Csharp)
                    | new Box().Content(Layout.Vertical()
    | new TextInput(placeholder: "Enter username").WithTooltip("Choose a unique username that will be visible to other users")
    | new TextInput(placeholder: "Enter email").WithTooltip("We'll use this email for account verification and notifications")
    | new NumberInput<int>(placeholder: "Enter age").WithTooltip("You must be at least 13 years old to create an account")
    | new BoolInput("Newsletter").WithTooltip("Receive updates about new features and improvements"))
                )
            )
            | new Markdown(
                """"
                ## Best Practices
                
                ### Keep Tooltips Concise
                
                Tooltips should provide quick, helpful information without being overwhelming:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Button("Good").WithTooltip("Clear and concise")
    | new Button("Too Long").WithTooltip("This tooltip is unnecessarily long and verbose, providing more information than the user needs at this moment, which can be distracting and counterproductive to the user experience"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Button("Good").WithTooltip("Clear and concise")
                        | new Button("Too Long").WithTooltip("This tooltip is unnecessarily long and verbose, providing more information than the user needs at this moment, which can be distracting and counterproductive to the user experience")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Use Consistent Language
                
                Maintain consistent terminology and tone across your tooltips:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Button("Save").WithTooltip("Save changes")
    | new Button("Cancel").WithTooltip("Cancel changes")
    | new Button("Reset").WithTooltip("Reset to default")
    | new Button("Apply").WithTooltip("Apply changes"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Button("Save").WithTooltip("Save changes")
                        | new Button("Cancel").WithTooltip("Cancel changes")
                        | new Button("Reset").WithTooltip("Reset to default")
                        | new Button("Apply").WithTooltip("Apply changes")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### Provide Actionable Information
                
                When possible, tell users what will happen when they interact with an element:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Button("Delete").WithTooltip("Permanently delete this item (cannot be undone)")
    | new Button("Archive").WithTooltip("Move to archive (can be restored later)")
    | new Button("Share").WithTooltip("Share this item with other users")
    | new Button("Export").WithTooltip("Download as a file"))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Button("Delete").WithTooltip("Permanently delete this item (cannot be undone)")
                        | new Button("Archive").WithTooltip("Move to archive (can be restored later)")
                        | new Button("Share").WithTooltip("Share this item with other users")
                        | new Button("Export").WithTooltip("Download as a file")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Accessibility Considerations
                
                Tooltips enhance accessibility by providing additional context for screen readers and keyboard navigation:
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    Layout.Vertical()
                        | Text.Literal("Accessible content").WithTooltip("This tooltip provides additional context for assistive technologies")
                        | new Button("Accessible button").WithTooltip("This button performs a specific action that is described in the tooltip")
                        | new Badge("Status").WithTooltip("Current status is clearly indicated for all users")
                    """",Languages.Csharp)
                | new Box().Content(Layout.Vertical()
    | Text.Literal("Accessible content").WithTooltip("This tooltip provides additional context for assistive technologies")
    | new Button("Accessible button").WithTooltip("This button performs a specific action that is described in the tooltip")
    | new Badge("Status").WithTooltip("Current status is clearly indicated for all users"))
            )
            | new WidgetDocsView("Ivy.Tooltip", "Ivy.TooltipExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Tooltip.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.NavigationApp)]; 
        return article;
    }
}

