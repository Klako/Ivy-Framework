using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:6, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/06_Expandable.md", searchHints: ["accordion", "collapse", "expand", "toggle", "disclosure", "collapsible"])]
public class ExpandableApp(bool onlyBody = false) : ViewBase
{
    public ExpandableApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("expandable", "Expandable", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("nested-expandables", "Nested Expandables", 3), new ArticleHeading("disabled-open-and-closed", "Disabled, Open and Closed", 3), new ArticleHeading("with-icon", "With Icon", 3), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Expandable").OnLinkClick(onLinkClick)
            | Lead("Create collapsible content sections that users can expand and collapse to maintain clean, organized [layouts](app://onboarding/concepts/layout).")
            | new Markdown(
                """"
                The `Expandable` [widget](app://onboarding/concepts/widgets) allows you to hide and show content interactively, providing a clean and organized way to present information. It's perfect for organizing content into collapsible sections, FAQs, or any scenario where you want to reduce visual clutter.
                
                ## Basic Usage
                
                Here's a simple example of an expandable widget.
                """").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    new Expandable("Click to expand",
                        "This is the hidden content that appears when you expand the widget.")
                    """",Languages.Csharp)
                | new Box().Content(new Expandable("Click to expand", 
    "This is the hidden content that appears when you expand the widget."))
            )
            | new Markdown(
                """"
                ### Nested Expandables
                
                Create hierarchical structures by nesting expandable widgets.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new Expandable("Main Section", 
    Layout.Vertical().Gap(2)
        | Text.H4("Overview")
        | Text.Muted("This is the main content")
        | new Expandable("Subsection 1", "Details about subsection 1")
        | new Expandable("Subsection 2", "Details about subsection 2")
        | new Expandable("Subsection 3", "Details about subsection 3")
))),
                new Tab("Code", new CodeBlock(
                    """"
                    new Expandable("Main Section",
                        Layout.Vertical().Gap(2)
                            | Text.H4("Overview")
                            | Text.Muted("This is the main content")
                            | new Expandable("Subsection 1", "Details about subsection 1")
                            | new Expandable("Subsection 2", "Details about subsection 2")
                            | new Expandable("Subsection 3", "Details about subsection 3")
                    )
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("### Disabled, Open and Closed").OnLinkClick(onLinkClick)
            | new Callout("You can also disable an expandable, or set it to be open by default.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(2)
    | new Expandable("Normal", "This expandable works normally")
    | new Expandable("Disabled", "This expandable is disabled").Disabled()
    | new Expandable("Open", "This expandable is open").Open())),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(2)
                        | new Expandable("Normal", "This expandable works normally")
                        | new Expandable("Disabled", "This expandable is disabled").Disabled()
                        | new Expandable("Open", "This expandable is open").Open()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ### With Icon
                
                Add an icon to the expandable header to provide additional visual context.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(2)
    | new Expandable("Settings", "Configure your application preferences here.").Icon(Icons.Settings)
    | new Expandable("User Profile", "View and edit your profile information.").Icon(Icons.User)
    | new Expandable("Notifications", "Manage your notification preferences.").Icon(Icons.Bell))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(2)
                        | new Expandable("Settings", "Configure your application preferences here.").Icon(Icons.Settings)
                        | new Expandable("User Profile", "View and edit your profile information.").Icon(Icons.User)
                        | new Expandable("Notifications", "Manage your notification preferences.").Icon(Icons.Bell)
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new WidgetDocsView("Ivy.Expandable", "Ivy.ExpandableExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Expandable.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Form Sections",
                Vertical().Gap(4)
                | new Markdown("Organize [forms](app://onboarding/concepts/forms) into logical, collapsible sections.").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new SimpleFormExample())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class SimpleFormExample : ViewBase
                        {
                            public record PersonalInfo(string FirstName, string LastName, string Email, string Phone);
                            public record AddressInfo(string Street, string City, string State, string ZipCode);
                            public record UserPreferences(bool EmailNotifications, bool SmsNotifications, string Language, string Theme);
                        
                            public override object? Build()
                            {
                                var personalInfo = UseState(() => new PersonalInfo("", "", "", ""));
                                var addressInfo = UseState(() => new AddressInfo("", "", "", ""));
                                var preferences = UseState(() => new UserPreferences(false, false, "en", "light"));
                        
                                return Layout.Vertical().Gap(2)
                                    | new Expandable("Personal Information", personalInfo.ToForm())
                                    | new Expandable("Address", addressInfo.ToForm())
                                    | new Expandable("Preferences", preferences.ToForm());
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.LayoutApp), typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}


public class SimpleFormExample : ViewBase
{
    public record PersonalInfo(string FirstName, string LastName, string Email, string Phone);
    public record AddressInfo(string Street, string City, string State, string ZipCode);
    public record UserPreferences(bool EmailNotifications, bool SmsNotifications, string Language, string Theme);

    public override object? Build()
    {
        var personalInfo = UseState(() => new PersonalInfo("", "", "", ""));
        var addressInfo = UseState(() => new AddressInfo("", "", "", ""));
        var preferences = UseState(() => new UserPreferences(false, false, "en", "light"));

        return Layout.Vertical().Gap(2)
            | new Expandable("Personal Information", personalInfo.ToForm())
            | new Expandable("Address", addressInfo.ToForm())
            | new Expandable("Preferences", preferences.ToForm());
    }
}
