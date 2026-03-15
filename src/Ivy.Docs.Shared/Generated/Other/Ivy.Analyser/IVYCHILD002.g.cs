using System;
using Ivy;
using static Ivy.Layout;
using static Ivy.Text;

namespace Ivy.Docs.Shared.Apps.Other.Ivy.Analyser;

[App(order:0, documentSource:"https://github.com/Ivy-Interactive/Ivy-Framework/blob/2587-rename-appattribute-path-parameter-to-group/src/Ivy.Docs.Shared/Docs/05_Other/Ivy.Analyser/IVYCHILD002.md")]
public class IVYCHILD002App(bool onlyBody = false) : ViewBase
{
    public IVYCHILD002App() : this(false)
    {
    }
    public override object? Build()
    {
        var appDescriptor = this.UseService<AppDescriptor>();
        var onLinkClick = this.UseLinks();
        var article = new Article().ShowToc(!onlyBody).ShowFooter(!onlyBody).Previous(appDescriptor.Previous).Next(appDescriptor.Next).DocumentSource(appDescriptor.DocumentSource).OnLinkClick(onLinkClick).Headings(new List<ArticleHeading> { new ArticleHeading("ivychild002-adding-multiple-children-to-single-child-widget", "IVYCHILD002: Adding Multiple Children to Single-Child Widget", 1), new ArticleHeading("description", "Description", 2), new ArticleHeading("cause", "Cause", 2), new ArticleHeading("fix", "Fix", 2), new ArticleHeading("see-also", "See Also", 2), })
            | new global::Ivy.Docs.Shared.Internal.SmartSearchView()
            | new Markdown(
                """"
                # IVYCHILD002: Adding Multiple Children to Single-Child Widget
                
                **Severity:** Warning
                
                ## Description
                
                Some widgets (e.g., `Card`, `Sheet`) only support a single child. Adding multiple children via chained `|` operators will throw `NotSupportedException` at runtime.
                
                ## Cause
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ❌ Multiple children on a Card — triggers IVYCHILD002
                public override object? Build()
                {
                    return new Card()
                        | Text.Block("First")
                        | Text.Block("Second"); // IVYCHILD002
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## Fix
                
                Wrap multiple elements in a layout:
                """").OnLinkClick(onLinkClick)
            | new CodeBlock(
                """"
                // ✅ Single layout child containing multiple elements
                public override object? Build()
                {
                    return new Card()
                        | (Layout.Vertical()
                            | Text.Block("First")
                            | Text.Block("Second"));
                }
                """",Languages.Csharp)
            | new Markdown(
                """"
                ## See Also
                
                - [Card](app://widgets/common/card)
                - [Layouts](app://widgets/layouts/stack-layout)
                """").OnLinkClick(onLinkClick)
            ;
        // Build errors here indicates that one or more referenced apps don't exist. Check markdown links.
        Type[] _ = [typeof(Widgets.Common.CardApp), typeof(Widgets.Layouts.StackLayoutApp)]; 
        return article;
    }
}

