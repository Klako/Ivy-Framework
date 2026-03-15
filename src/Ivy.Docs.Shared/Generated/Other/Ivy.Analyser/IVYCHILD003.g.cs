using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYCHILD003.md")]
public class IVYCHILD003App(bool onlyBody = false) : ViewBase
{
    public IVYCHILD003App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivychild003-wrong-child-type-for-widget", "IVYCHILD003: Wrong Child Type for Widget", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYCHILD003: Wrong Child Type for Widget
                
                **Severity:** Error
                
                ## Description
                
                Some widgets restrict the types of children they accept via the `[ChildType]` attribute. Passing a child of an incompatible type via the `|` operator will throw `NotSupportedException` at runtime.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Wrong child type — triggers IVYCHILD003
                public override object? Build()
                {
                    return new SidebarMenu()
                        | new Button("Not a menu item"); // IVYCHILD003 — expects SidebarMenuItem
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Use the correct child type as specified by the widget:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Correct child type
                public override object? Build()
                {
                    return new SidebarMenu()
                        | new SidebarMenuItem("Home", Icons.Home);
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [Sidebar Layout](app://widgets/layouts/sidebar-layout)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Layouts.SidebarLayoutApp)]; 
        return article;
    }
}

