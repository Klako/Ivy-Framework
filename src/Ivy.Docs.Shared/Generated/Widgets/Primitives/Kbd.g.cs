using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Widgets.Primitives;

[App(order:11, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/02_Widgets/01_Primitives/11_Kbd.md", searchHints: ["keyboard", "shortcut", "key", "hotkey", "command", "keys"])]
public class KbdApp(bool onlyBody = false) : ViewBase
{
    public KbdApp() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("kbd", "Kbd", 1), new ArticleHeading("api", "API", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown("# Kbd").OnLinkClick(onLinkClick)
            | Lead("Display keyboard shortcuts and key combinations with proper styling to help users identify commands and improve documentation.")
            | new Markdown("The `Kbd` [widget](app://onboarding/concepts/widgets) displays keyboard shortcuts or key combinations with proper styling. It helps users identify key commands and improves documentation clarity.").OnLinkClick(onLinkClick)
            | (Vertical() 
                | new CodeBlock(
                    """"
                    Layout.Horizontal() |
                        new Kbd("Ctrl + C") |
                        new Kbd("Shift + Ctrl + C")
                    """",Languages.Csharp)
                | new Box().Content(Layout.Horizontal() | 
    new Kbd("Ctrl + C") | 
    new Kbd("Shift + Ctrl + C"))
            )
            | new WidgetDocsView("Ivy.Kbd", "Ivy.KbdExtensions", "https://github.com/Ivy-Interactive/Ivy-Framework/blob/main/src/Ivy/Widgets/Primitives/Kbd.cs")
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Onboarding.Concepts.WidgetsApp)]; 
        return article;
    }
}

