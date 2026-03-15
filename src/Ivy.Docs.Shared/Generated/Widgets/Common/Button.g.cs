using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Common;

[App(order:1, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/03_Common/01_Button.md", searchHints: ["click", "action", "submit", "cta", "interactive", "control"])]
public class ButtonApp(bool onlyBody = false) : ViewBase
{
    public ButtonApp() : this(false)
    {
    }
    public override object? Build()
    {
        var client = UseService<IClientProvider>();
        
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("button", "Button", 1), new ArticleHeading("basic-usage", "Basic Usage", 2), new ArticleHeading("semantic-variants", "Semantic Variants", 2), new ArticleHeading("styling--configuration", "Styling & Configuration", 2), new ArticleHeading("buttons-with-urls", "Buttons with URLs", 2), new ArticleHeading("faq", "Faq", 2), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Button").OnLinkClick(onLinkClick)
            | Lead("Create interactive buttons with multiple variants, states, sizes, and styling options for triggering actions in your Ivy [applications](app://onboarding/concepts/apps).")
            | new Markdown(
                """"
                The `Button` [widget](app://onboarding/concepts/widgets) is one of the most fundamental interactive elements in Ivy. It allows users to [trigger actions](app://onboarding/concepts/event-handlers) and [navigate](app://onboarding/concepts/navigation) through your project.
                
                ## Basic Usage
                
                Here's a simple example of a button that shows a [toast message](app://onboarding/concepts/clients) when clicked:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                var client = UseService<IClientProvider>();
                new Button("Click Me", onClick: _ => client.Toast("Hello!"));
                """",Languages.Csharp)
            | new Box().Content(new Button("Click Me", onClick: _ => client.Toast("Hello!")))
            | new Markdown(
                """"
                ## Semantic Variants
                
                The Button widget includes three new contextual variants to help communicate different types of actions to users: [Success, Warning, and Info](app://onboarding/concepts/theming). These variants complement the existing Primary, Secondary, Destructive, Outline, Ghost, and Link options.
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Horizontal()
    | new Button("Success").Success()
    | new Button("Warning").Warning()
    | new Button("Info").Info())),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Horizontal()
                        | new Button("Success").Success()
                        | new Button("Warning").Warning()
                        | new Button("Info").Info()
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Styling & Configuration
                
                Buttons offer extensive styling options including standard variants, states, border radius, and icon integration. Use [Align](app://api-reference/ivy/align) for icon position (e.g. `Align.Right`).
                """").OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(Layout.Vertical().Gap(4)
    | Text.P("Standard Variants").Large()
    | (Layout.Horizontal().Gap(4)
        | new Button("Primary")
        | new Button("Destructive").Destructive()
        | new Button("Secondary").Secondary()
        | new Button("Outline").Outline()
        | new Button("Ghost").Ghost()
        | new Button("Link").Link())
    | Text.P("States").Large()
    | (Layout.Horizontal().Gap(4)
        | new Button("Disabled").Disabled()
        | new Button("Loading").Loading()
        | new Button("Secondary Disabled").Secondary().Disabled())
    | Text.P("Border Radius").Large()
    | (Layout.Horizontal().Gap(4)
        | new Button("None").BorderRadius(BorderRadius.None)
        | new Button("Rounded").BorderRadius(BorderRadius.Rounded)
        | new Button("Full").BorderRadius(BorderRadius.Full))
    | Text.P("Icons").Large()
    | (Layout.Horizontal().Gap(4)
        | new Button("Save").Icon(Icons.Save)
        | new Button("Next").Icon(Icons.ArrowRight, Align.Right)
        | new Button().Icon(Icons.Settings).Ghost()))),
                new Tab("Code", new CodeBlock(
                    """"
                    Layout.Vertical().Gap(4)
                        | Text.P("Standard Variants").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Button("Primary")
                            | new Button("Destructive").Destructive()
                            | new Button("Secondary").Secondary()
                            | new Button("Outline").Outline()
                            | new Button("Ghost").Ghost()
                            | new Button("Link").Link())
                        | Text.P("States").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Button("Disabled").Disabled()
                            | new Button("Loading").Loading()
                            | new Button("Secondary Disabled").Secondary().Disabled())
                        | Text.P("Border Radius").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Button("None").BorderRadius(BorderRadius.None)
                            | new Button("Rounded").BorderRadius(BorderRadius.Rounded)
                            | new Button("Full").BorderRadius(BorderRadius.Full))
                        | Text.P("Icons").Large()
                        | (Layout.Horizontal().Gap(4)
                            | new Button("Save").Icon(Icons.Save)
                            | new Button("Next").Icon(Icons.ArrowRight, Align.Right)
                            | new Button().Icon(Icons.Settings).Ghost())
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown(
                """"
                ## Buttons with URLs
                
                Buttons can act as links by providing a [URL](app://onboarding/concepts/navigation). When a button has a URL, clicking it will navigate to that URL in the same tab by default. Use `.OpenInNewTab()` to override this behavior.
                """").OnLinkClick(onLinkClick)
            | new Callout(
                """"
                Buttons with URLs support [right-click actions](app://onboarding/concepts/navigation) like "Copy Link" and "Open in New Tab", providing a better user experience than programmatic navigation.
                """", icon:Icons.Info).OnLinkClick(onLinkClick)
            | Tabs( 
                new Tab("Demo", new Box().Content(    Layout.Horizontal().Gap(4)
        | new Button("Visit Ivy Docs")
            .Url("https://github.com/Ivy-Interactive/Ivy-Framework")
        | new Button("External Link").Secondary()
            .Url("https://github.com/Ivy-Interactive/Ivy-Framework")
            .OpenInNewTab()
            .Icon(Icons.ExternalLink, Align.Right)
        | new Button("Link Style").Link()
            .Url("https://github.com/Ivy-Interactive/Ivy-Framework"))),
                new Tab("Code", new CodeBlock(
                    """"
                        Layout.Horizontal().Gap(4)
                            | new Button("Visit Ivy Docs")
                                .Url("https://github.com/Ivy-Interactive/Ivy-Framework")
                            | new Button("External Link").Secondary()
                                .Url("https://github.com/Ivy-Interactive/Ivy-Framework")
                                .OpenInNewTab()
                                .Icon(Icons.ExternalLink, Align.Right)
                            | new Button("Link Style").Link()
                                .Url("https://github.com/Ivy-Interactive/Ivy-Framework")
                    """",Languages.Csharp))
            ).Height(Size.Fit()).Variant(TabsVariant.Content)
            | new Markdown("## Faq").OnLinkClick(onLinkClick)
            | new Expandable("What are the available ButtonVariant values in Ivy?",
                Vertical().Gap(4)
                | new Markdown(
                    """"
                    `ButtonVariant` has these values: `Primary`, `Destructive`, `Outline`, `Secondary`, `Success`, `Warning`, `Info`, `Ghost`, `Link`, `Inline`, `Ai`.
                    
                    Set via the `.Variant()` method or shortcut methods:
                    """").OnLinkClick(onLinkClick)
                | new CodeBlock(
                    """"
                    new Button("Save", handler).Variant(ButtonVariant.Primary)
                    // or use shortcut:
                    new Button("Save", handler).Primary()
                    
                    // Other shortcuts: .Secondary(), .Outline(), .Destructive(), .Ghost(), .Link(), .Inline(), .Ai()
                    """",Languages.Csharp)
                | new Markdown("**Important:** There is no `ButtonVariant.Default`. Use `ButtonVariant.Primary` instead.").OnLinkClick(onLinkClick)
            )
            | new WidgetDocsView("Ivy.Button", "Ivy.ButtonExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Button.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.AppsApp), typeof(Onboarding.Concepts.WidgetsApp), typeof(Onboarding.Concepts.EventHandlersApp), typeof(Onboarding.Concepts.NavigationApp), typeof(Onboarding.Concepts.ClientsApp), typeof(Onboarding.Concepts.ThemingApp), typeof(ApiReference.Ivy.AlignApp)]; 
        return article;
    }
}

