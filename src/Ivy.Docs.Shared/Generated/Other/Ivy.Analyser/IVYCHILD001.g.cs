using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYCHILD001.md")]
public class IVYCHILD001App(bool onlyBody = false) : ViewBase
{
    public IVYCHILD001App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivychild001-adding-children-to-leaf-widget", "IVYCHILD001: Adding Children to Leaf Widget", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYCHILD001: Adding Children to Leaf Widget
                
                **Severity:** Error
                
                ## Description
                
                Some widgets (e.g., `Button`, `Badge`, `DataTable`) are leaf widgets that do not support children. Attempting to add children via the `|` operator will throw `NotSupportedException` at runtime.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Adding a child to a Button — triggers IVYCHILD001
                public override object? Build()
                {
                    return new Button("Click me")
                        | Text.Block("child"); // IVYCHILD001
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Remove the child. If you need to compose content, use a layout widget instead:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Use a layout to group elements
                public override object? Build()
                {
                    return Layout.Vertical()
                        | new Button("Click me")
                        | Text.Block("Below the button");
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [Layouts](app://widgets/layouts/stack-layout)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Layouts.StackLayoutApp)]; 
        return article;
    }
}

