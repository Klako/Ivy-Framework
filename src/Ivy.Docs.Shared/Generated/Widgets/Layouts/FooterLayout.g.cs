using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Layouts;

[App(order:5, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/02_Layouts/05_FooterLayout.md", searchHints: ["layout", "footer", "sticky", "actions", "bottom", "fixed"])]
public class FooterLayoutApp(bool onlyBody = false) : ViewBase
{
    public FooterLayoutApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("footerlayout", "FooterLayout", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("form-with-footer-actions", "Form with Footer Actions", 2), new ArticleHeading("sheet-interface", "Sheet Interface", 2), new ArticleHeading("api", "API", 2), new ArticleHeading("examples", "Examples", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# FooterLayout").OnLinkClick(onLinkClick)
            | Lead("FooterLayout creates a layout with a fixed footer at the bottom and scrollable content above it. It's perfect for [forms](app://onboarding/concepts/forms), sheets, and any [interface](app://onboarding/concepts/views) where you need persistent action buttons or information while allowing the main content to scroll independently.")
            | new Markdown(
                """"
                The `FooterLayout` [widget](app://onboarding/concepts/widgets) is designed to keep important actions or information visible at the bottom of the view while allowing the main content to scroll freely above it. This pattern is commonly used in forms, modal dialogs, and sheet [interfaces](app://onboarding/concepts/views) where users need constant access to primary actions.
                
                ## Basic Usage
                
                Create a simple footer layout with content and footer elements:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new BasicFooterExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class BasicFooterExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                    
                            return Layout.Vertical()
                                | new Card("Main Section")
                                        | Text.P("Welcome to Ivy Framework")
                                | new FooterLayout(
                                    footer: new Button("Save", _ => client.Toast("Content saved!"))
                                        .Variant(ButtonVariant.Primary),
                                    content: Layout.Vertical()
                                        | Text.P("This is the main content area that demonstrates how content can scroll independently above the footer.")
                            );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Form with Footer Actions
                
                A common use case is creating forms with persistent action buttons. Use [Align](app://api-reference/ivy/align) for footer button alignment (e.g. `Align.Right`):
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new FormWithFooterExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class FormWithFooterExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var firstName = UseState("John");
                            var lastName = UseState("Doe");
                            var email = UseState("john.doe@example.com");
                            var bio = UseState("Software developer with 5+ years of experience...");
                    
                            return Layout.Vertical()
                                | new Card("Form Header")
                                | new FooterLayout(
                                    footer: Layout.Horizontal().Align(Align.Right)
                                        | new Button("Cancel", _ => client.Toast("Cancelled"))
                                        | new Button("Submit", _ => client.Toast("Form submitted"))
                                            .Variant(ButtonVariant.Primary),
                                    content: Layout.Vertical()
                                        | new Card(Layout.Vertical()
                                            | new TextInput(firstName, "First Name")
                                            | new TextInput(lastName, "Last Name")
                                            | new TextInput(email, "Email Address")
                                        ).Title("Personal Information")
                                );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Sheet Interface
                
                FooterLayout is commonly used in sheet interfaces for consistent action placement:
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(new SheetWithFooterExample())),
                new Tab("Code", new CodeBlock(
                    """"
                    public class SheetWithFooterExample : ViewBase
                    {
                        public override object? Build()
                        {
                            var client = UseService<IClientProvider>();
                            var title = UseState("Getting Started with Ivy Framework");
                            var content = UseState("Write your article content here...");
                    
                            return Layout.Vertical()
                                | new Card("Sheet Header")
                                        | Layout.Vertical()
                                            | Text.P("Article Editor")
                                            | Text.P("Create and edit your articles with ease").Small().Color(Colors.Gray)
                                | new FooterLayout(
                                    footer: Layout.Horizontal().Align(Align.Right)
                                        | new Button("Save Draft", _ => client.Toast("Draft saved"))
                                        | new Button("Publish", _ => client.Toast("Published!"))
                                            .Variant(ButtonVariant.Primary),
                                    content: Layout.Vertical()
                                        | new Card(Layout.Vertical()
                                            | new TextInput(title, "Article Title")
                                            | new TextInput(content, "Article Content").Variant(TextInputVariant.Textarea)
                                        ).Title("Article Details")
                                        | new Card("Article preview will appear here as you type...")
                                            .Title("Preview")
                                        | new Card("Meta description and keywords for search engines...")
                                            .Title("SEO Information")
                                );
                        }
                    }
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Callout("Use FooterLayout for multi-step forms, long questionnaires, and data entry interfaces.", icon:Icons.Info).OnLinkClick(onLinkClick)
            | new WidgetDocsView("Ivy.FooterLayout", null, "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Layouts/FooterLayout.cs")
            | new Markdown("## Examples").OnLinkClick(onLinkClick)
            | new Expandable("Complex Footer with Multiple Elements",
                Vertical().Gap(4)
                | new Markdown("Create sophisticated footers with various components:").OnLinkClick(onLinkClick)
                | Tabs( 
                    new Tab("Demo", new Box().Content(new ComplexFooterExample())),
                    new Tab("Code", new CodeBlock(
                        """"
                        public class ComplexFooterExample : ViewBase
                        {
                            public override object? Build()
                            {
                                var client = UseService<IClientProvider>();
                                var docTitle = UseState("Project Proposal");
                                var summary = UseState("This project aims to...");
                        
                                return Layout.Vertical()
                                    | new Card("Project Header")
                                            | Layout.Vertical()
                                                | Text.P("Document Editor")
                                                | Text.P("Comprehensive project management tool").Small().Color(Colors.Gray)
                                    | new FooterLayout(
                                        footer: Layout.Horizontal().Align(Align.Right)
                                            | new Badge("Draft").Variant(BadgeVariant.Secondary)
                                            | new Button("Save Draft", _ => client.Toast("Draft saved"))
                                            | new Button("Submit", _ => client.Toast("Submitted for review"))
                                                .Variant(ButtonVariant.Primary),
                                        content: Layout.Vertical()
                                            | new Card(Layout.Vertical()
                                                | new TextInput(docTitle, "Document Title")
                                                | new TextInput(summary, "Executive Summary").Variant(TextInputVariant.Textarea)
                                            ).Title("Document Information")
                                    );
                            }
                        }
                        """",Languages.Csharp))
                ).Height(Size.Fit()).Variant(TabsVariant.Content)
            )
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.FormsApp), typeof(Onboarding.Concepts.ViewsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(ApiReference.Ivy.AlignApp)]; 
        return article;
    }
}


public class BasicFooterExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        return Layout.Vertical()
            | new Card("Main Section")
                    | Text.P("Welcome to Ivy Framework")
            | new FooterLayout(
                footer: new Button("Save", _ => client.Toast("Content saved!"))
                    .Variant(ButtonVariant.Primary),
                content: Layout.Vertical()
                    | Text.P("This is the main content area that demonstrates how content can scroll independently above the footer.")
        );
    }
}

public class FormWithFooterExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var firstName = UseState("John");
        var lastName = UseState("Doe");
        var email = UseState("john.doe@example.com");
        var bio = UseState("Software developer with 5+ years of experience...");
        
        return Layout.Vertical()
            | new Card("Form Header")
            | new FooterLayout(
                footer: Layout.Horizontal().Align(Align.Right)
                    | new Button("Cancel", _ => client.Toast("Cancelled"))
                    | new Button("Submit", _ => client.Toast("Form submitted"))
                        .Variant(ButtonVariant.Primary),
                content: Layout.Vertical()
                    | new Card(Layout.Vertical()
                        | new TextInput(firstName, "First Name")
                        | new TextInput(lastName, "Last Name")
                        | new TextInput(email, "Email Address")
                    ).Title("Personal Information")
            );
    }
}

public class SheetWithFooterExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var title = UseState("Getting Started with Ivy Framework");
        var content = UseState("Write your article content here...");
        
        return Layout.Vertical()
            | new Card("Sheet Header")
                    | Layout.Vertical()
                        | Text.P("Article Editor")
                        | Text.P("Create and edit your articles with ease").Small().Color(Colors.Gray)
            | new FooterLayout(
                footer: Layout.Horizontal().Align(Align.Right)
                    | new Button("Save Draft", _ => client.Toast("Draft saved"))
                    | new Button("Publish", _ => client.Toast("Published!"))
                        .Variant(ButtonVariant.Primary),
                content: Layout.Vertical()
                    | new Card(Layout.Vertical()
                        | new TextInput(title, "Article Title")
                        | new TextInput(content, "Article Content").Variant(TextInputVariant.Textarea)
                    ).Title("Article Details")
                    | new Card("Article preview will appear here as you type...")
                        .Title("Preview")
                    | new Card("Meta description and keywords for search engines...")
                        .Title("SEO Information")
            );
    }
}

public class ComplexFooterExample : ViewBase
{
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        var docTitle = UseState("Project Proposal");
        var summary = UseState("This project aims to...");
        
        return Layout.Vertical()
            | new Card("Project Header")
                    | Layout.Vertical()
                        | Text.P("Document Editor")
                        | Text.P("Comprehensive project management tool").Small().Color(Colors.Gray)
            | new FooterLayout(
                footer: Layout.Horizontal().Align(Align.Right)
                    | new Badge("Draft").Variant(BadgeVariant.Secondary)
                    | new Button("Save Draft", _ => client.Toast("Draft saved"))
                    | new Button("Submit", _ => client.Toast("Submitted for review"))
                        .Variant(ButtonVariant.Primary),
                content: Layout.Vertical()
                    | new Card(Layout.Vertical()
                        | new TextInput(docTitle, "Document Title")
                        | new TextInput(summary, "Executive Summary").Variant(TextInputVariant.Textarea)
                    ).Title("Document Information")
            );
    }
}
